-- 3.1	Přidání vězně – zjistí, zda je možné daného vězně přidat do dané cely, a pokud ano, tak jej přidá a zvýší počet obsazených míst cely o jedno
CREATE OR ALTER PROCEDURE insertPrisoner(
	@firstName VARCHAR(50),
	@lastName VARCHAR(50),
	@gender CHAR(1),
	@birthDate DATE,
	@punishmentStartDate DATE,
	@punishmentEndDate DATE,
	@Cell_cell_id INT
) AS
BEGIN
	DECLARE
		@prisoner_id INT,
		@Cell_occupied INT,
		@Cell_capacity INT;
	
	SELECT @Cell_occupied = occupied, @Cell_capacity = capacity FROM Cell WHERE cell_id = @Cell_cell_id;
	
	IF ((@Cell_occupied + 1) > @Cell_capacity)
		RAISERROR('Cell "%s" is already full. You cannot add more prisoners there!', 16, 1, @Cell_cell_id);
	ELSE
		SELECT @prisoner_id = (MAX(prisoner_id) + 1) FROM Prisoner;
		
		BEGIN TRANSACTION transactionInsertPrisoner
			BEGIN TRY
				INSERT INTO Prisoner VALUES(@prisoner_id, @firstName, @lastName, @gender, @birthDate, @punishmentStartDate, @punishmentEndDate, 0, @Cell_cell_id);
				
				UPDATE Cell SET occupied = occupied + 1 WHERE cell_id = @Cell_cell_id;
				
				COMMIT TRANSACTION transactionInsertPrisoner;
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION transactionInsertPrisoner
			END CATCH;
END;

-- 3.3	Aktualizace vězně – zjistí, zda je možné daného vězně (pokud ještě nebyl propuštěn) přesunout do dané cely, a pokud ano, tak jej přesune a zvýší počet obsazených míst nové cely o jedno a zároveň sníží počet míst staré cely o jedno a přidá záznam do historie obsazení cel
CREATE OR ALTER PROCEDURE updatePrisoner(
	@prisoner_id INT,
	@firstName VARCHAR(50),
	@lastName VARCHAR(50),
	@gender CHAR(1),
	@birthDate DATE,
	@punishmentStartDate DATE,
	@punishmentEndDate DATE,
	@NewCell_cell_id INT
) AS
BEGIN
	DECLARE
		@released CHAR(1),
		@OldCell_cell_id INT,
		@Cell_occupied INT,
		@Cell_capacity INT;

	SELECT @released = released, @OldCell_cell_id = Cell_cell_id FROM Prisoner WHERE prisoner_id = @prisoner_id;

	BEGIN TRANSACTION transactionUpdatePrisoner
		BEGIN TRY
			IF (@OldCell_cell_id = @NewCell_cell_id OR @released = '1')
				UPDATE Prisoner SET firstName = @firstName, lastName = @lastName, gender = @gender, birthDate = @birthDate, punishmentStartDate = @punishmentStartDate, punishmentEndDate = @punishmentEndDate WHERE prisoner_id = @prisoner_id;
			ELSE
				SELECT @Cell_occupied = occupied, @Cell_capacity = capacity FROM Cell WHERE cell_id = @NewCell_cell_id;
		
				IF ((@Cell_occupied + 1) > @Cell_capacity)
					RAISERROR('Cell "%s" is already full. You cannot add more prisoners there!', 16, 1, @NewCell_cell_id);
				ELSE
					EXECUTE insertPrisonerCellHistory @prisoner_id, @OldCell_cell_id;
		
					UPDATE Cell SET occupied =
					CASE
						WHEN cell_id = @OldCell_cell_id THEN occupied - 1
						WHEN cell_id = @NewCell_cell_id THEN occupied + 1
						ELSE occupied
					END;
		
					UPDATE Prisoner SET firstName = @firstName, lastName = @lastName, gender = @gender, birthDate = @birthDate, punishmentStartDate = @punishmentStartDate, punishmentEndDate = @punishmentEndDate, Cell_cell_id = @NewCell_cell_id WHERE prisoner_id = @prisoner_id;
			
			COMMIT TRANSACTION transactionUpdatePrisoner;
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION transactionUpdatePrisoner
		END CATCH;
END;

-- 3.4	Propustit vězně – vězeň bude mít nově aktivní vlastnost propuštěn a zároveň se sníží počet obsazených míst cely ve které byl naposledy o jedno a přidá záznam do historie obsazení cel a všechny naplánované návštěvy tohoto vězně se změní na nepovolené
CREATE OR ALTER PROCEDURE releasePrisoner(
	@prisoner_id INT
) AS
BEGIN
	DECLARE
		@currentDate DATE = CAST(GETDATE() AS DATE),
		@Cell_cell_id INT;

	SELECT @Cell_cell_id = Cell_cell_id FROM Prisoner WHERE prisoner_id = @prisoner_id;

	BEGIN TRANSACTION transactionReleasePrisoner
		BEGIN TRY
			EXECUTE insertPrisonerCellHistory @prisoner_id, @Cell_cell_id;
		
			UPDATE Cell SET occupied = occupied - 1 WHERE cell_id = @Cell_cell_id;
		
			UPDATE Visit SET allowed = 0 WHERE Prisoner_prisoner_id = @prisoner_id AND visitDate > @currentDate;
	
			UPDATE Prisoner SET released = '1' WHERE prisoner_id = @prisoner_id;
			
			COMMIT TRANSACTION transactionReleasePrisoner;
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION transactionReleasePrisoner
		END CATCH;
END;

-- 4.1	Přidání záznamu – přidá záznam do historie obsazení cel
CREATE OR ALTER PROCEDURE insertPrisonerCellHistory(
	@Prisoner_prisoner_id INT,
	@Cell_cell_id INT
) AS
BEGIN
	DECLARE
		@currentDate DATE = CAST(GETDATE() AS DATE),
		@prisonerCellHistory_id INT,
		@startDate DATE;

	SELECT @prisonerCellHistory_id = (MAX(prisonerCellHistory_id) + 1) FROM PrisonerCellHistory;

	SELECT @startDate =
	CASE
		WHEN COUNT(endDate) > 0 THEN MAX(endDate)
		ELSE punishmentStartDate END
	FROM Prisoner
		LEFT JOIN PrisonerCellHistory ON Prisoner_prisoner_id = prisoner_id
	WHERE prisoner_id = @Prisoner_prisoner_id
	GROUP BY punishmentStartDate;

	INSERT INTO PrisonerCellHistory VALUES(@prisonerCellHistory_id, @startDate, @currentDate, @Cell_cell_id, @Prisoner_prisoner_id);
END;

-- 5.1	Přidání návštěvy – zjistí, zda je možné danou návštěvu přidat na základě toho, zda je její návštěvník povolen a navštívený vězeň ještě nebyl propuštěn, a pokud tyto podmínky platí, tak se daná návštěva přidá a aktualizuje se aktivita daného návštěvníka
CREATE OR ALTER PROCEDURE insertVisit(
	@visitDate DATE,
	@Prisoner_prisoner_id INT,
	@Visitor_visitor_id INT
) AS
BEGIN
	DECLARE
		@visit_id INT,
		@Prisoner_released CHAR(1),
		@Visitor_forbidden CHAR(1);

	SELECT @Prisoner_released = released FROM Prisoner WHERE prisoner_id = @Prisoner_prisoner_id;
	
	SELECT @Visitor_forbidden = forbidden FROM Visitor	WHERE visitor_id = @Visitor_visitor_id;

	IF (@Prisoner_released = 0 AND @Visitor_forbidden = 0)
		SELECT @visit_id = (MAX(visit_id) + 1) FROM Visit;
		
		BEGIN TRANSACTION transactionInsertVisit
			BEGIN TRY
				INSERT INTO Visit VALUES(@visit_id, @visitDate, '2', @Prisoner_prisoner_id, @Visitor_visitor_id);
		
				EXECUTE updateVisitorActivity @Visitor_visitor_id;
				
				COMMIT TRANSACTION transactionInsertVisit;
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION transactionInsertVisit
			END CATCH;
END;

-- 6.4	Aktualizace aktivity návštěvníka – na základě datumu poslední naplánované návštěvy (v rámci 30 dnů od aktuálního data do minulosti a budoucnosti) dojde k zaznamenání aktivity/neaktivity návštěvníka
CREATE OR ALTER PROCEDURE updateVisitorActivity(
	@visitor_id INT
) AS
BEGIN
	DECLARE
		@currentDate DATE = GETDATE(),
		@Visit_count INT;

	SELECT @Visit_count = COUNT(*)
	FROM Visit
	WHERE Visitor_visitor_id = @visitor_id AND visitDate BETWEEN DATEADD(DAY,-30,@currentDate) AND DATEADD(DAY,30,@currentDate);

	UPDATE Visitor SET active =
	CASE
		WHEN @Visit_count > 0 THEN 1
		ELSE 0 END
	WHERE visitor_id = @visitor_id;
END;

-- 6.5	Zakázání návštěvníka – návštěvník bude mít nově aktivní vlastnost zakázán a zároveň se všechny naplánované návštěvy tohoto návštěvníka změní na nepovolené
CREATE OR ALTER PROCEDURE forbidVisitor(
	@visitor_id INT
) AS
BEGIN
	DECLARE
		@currentDate DATE = GETDATE();

	BEGIN TRANSACTION transactionForbidVisitor
		BEGIN TRY
			UPDATE Visit SET allowed = 0 WHERE Visitor_visitor_id = @visitor_id AND visitDate > @currentDate;
			
			UPDATE Visitor SET forbidden = '1' WHERE visitor_id = @visitor_id;
			
			COMMIT TRANSACTION transactionForbidVisitor;
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION transactionForbidVisitor
		END CATCH;
END;