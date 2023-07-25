-- 3.1	Přidání vězně – zjistí, zda je možné daného vězně přidat do dané cely, a pokud ano, tak jej přidá a zvýší počet obsazených míst cely o jedno
CREATE OR ALTER PROCEDURE insertPrisoner(
	@firstName VARCHAR(50),
	@lastName VARCHAR(50),
	@gender CHAR(1),
	@birthDate DATE,
	@punishmentStartDate DATE,
	@punishmentEndDate DATE,
	@cell_id INT
) AS
BEGIN
	DECLARE
		@prisoner_id INT,
		@cell_occupied INT,
		@cell_capacity INT;
	
	SELECT @cell_occupied = occupied, @cell_capacity = capacity FROM Cell WHERE cell_id = @cell_id;
	
	IF ((@cell_occupied + 1) > @cell_capacity)
		BEGIN
			RAISERROR('Cell "%s" is already full. You cannot add more prisoners there!', 16, 1, @cell_id);
		END;
	ELSE
		BEGIN
			SELECT @prisoner_id = (MAX(prisoner_id) + 1) FROM Prisoner;
			
			BEGIN TRANSACTION transactionInsertPrisoner
				BEGIN TRY
					INSERT INTO Prisoner VALUES(@prisoner_id, @firstName, @lastName, @gender, @birthDate, @punishmentStartDate, @punishmentEndDate, 0, @cell_id);
					
					UPDATE Cell SET occupied = occupied + 1 WHERE cell_id = @cell_id;
					
					COMMIT TRANSACTION transactionInsertPrisoner;
				END TRY
				BEGIN CATCH
					ROLLBACK TRANSACTION transactionInsertPrisoner
				END CATCH;
		END;
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
	@newCell_id INT
) AS
BEGIN
	DECLARE
		@released CHAR(1),
		@oldCell_id INT,
		@cell_occupied INT,
		@cell_capacity INT;

	SELECT @released = released, @oldCell_id = cell_id FROM Prisoner WHERE prisoner_id = @prisoner_id;

	BEGIN TRANSACTION transactionUpdatePrisoner
		BEGIN TRY
			IF (@oldCell_id = @newCell_id OR @released = '1')
				BEGIN
					UPDATE Prisoner SET firstName = @firstName, lastName = @lastName, gender = @gender, birthDate = @birthDate, punishmentStartDate = @punishmentStartDate, punishmentEndDate = @punishmentEndDate WHERE prisoner_id = @prisoner_id;
				END;
			ELSE
				BEGIN
					SELECT @cell_occupied = occupied, @cell_capacity = capacity FROM Cell WHERE cell_id = @newCell_id;
					
					IF ((@cell_occupied + 1) > @cell_capacity)
						BEGIN
							RAISERROR('Cell "%s" is already full. You cannot add more prisoners there!', 16, 1, @newCell_id);
						END;
					ELSE
						BEGIN
							UPDATE Cell SET occupied =
							CASE
								WHEN cell_id = @oldCell_id THEN occupied - 1
								WHEN cell_id = @newCell_id THEN occupied + 1
								ELSE occupied
							END;
							
							UPDATE Prisoner SET firstName = @firstName, lastName = @lastName, gender = @gender, birthDate = @birthDate, punishmentStartDate = @punishmentStartDate, punishmentEndDate = @punishmentEndDate, cell_id = @newCell_id WHERE prisoner_id = @prisoner_id;
						END;
				END;
			
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
		@cell_id INT;

	SELECT @cell_id = cell_id FROM Prisoner WHERE prisoner_id = @prisoner_id;

	BEGIN TRANSACTION transactionReleasePrisoner
		BEGIN TRY
			UPDATE Cell SET occupied = occupied - 1 WHERE cell_id = @cell_id;
		
			UPDATE Visit SET allowed = 0 WHERE prisoner_id = @prisoner_id AND visitDate > @currentDate;
	
			UPDATE Prisoner SET released = '1' WHERE prisoner_id = @prisoner_id;
			
			COMMIT TRANSACTION transactionReleasePrisoner;
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION transactionReleasePrisoner
		END CATCH;
END;

-- 5.1	Přidání návštěvy – zjistí, zda je možné danou návštěvu přidat na základě toho, zda je její návštěvník povolen a navštívený vězeň ještě nebyl propuštěn, a pokud tyto podmínky platí, tak se daná návštěva přidá a aktualizuje se aktivita daného návštěvníka
CREATE OR ALTER PROCEDURE insertVisit(
	@visitDate DATE,
	@allowed CHAR(1),
	@prisoner_id INT,
	@visitor_id INT
) AS
BEGIN
	DECLARE
		@visit_id INT,
		@prisoner_released CHAR(1),
		@visitor_forbidden CHAR(1);

	SELECT @prisoner_released = released FROM Prisoner WHERE prisoner_id = @prisoner_id;
	
	SELECT @visitor_forbidden = forbidden FROM Visitor	WHERE visitor_id = @visitor_id;

	IF (@prisoner_released = 0 AND @visitor_forbidden = 0)
		BEGIN
			SELECT @visit_id = (MAX(visit_id) + 1) FROM Visit;
			
			BEGIN TRANSACTION transactionInsertVisit
				BEGIN TRY
					INSERT INTO Visit VALUES(@visit_id, @visitDate, @allowed, @prisoner_id, @visitor_id);
		
					COMMIT TRANSACTION transactionInsertVisit;
				END TRY
				BEGIN CATCH
					ROLLBACK TRANSACTION transactionInsertVisit
				END CATCH;
		END;
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
			UPDATE Visit SET allowed = 0 WHERE visitor_id = @visitor_id AND visitDate > @currentDate;
			
			UPDATE Visitor SET forbidden = '1' WHERE visitor_id = @visitor_id;
			
			COMMIT TRANSACTION transactionForbidVisitor;
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION transactionForbidVisitor
		END CATCH;
END;