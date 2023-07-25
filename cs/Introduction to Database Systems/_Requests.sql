--SKUPINA 1
/*1;1;5;
Výpis všech věznic seřazených podle jejich typu od nejvíce po nejméně zabezpečenou.*/
SELECT *
FROM Prison
ORDER BY PrisonType_prisonType_id DESC

/*1;2;36;
Výpis všech zaměstnanců od nejstaršího po nejmladšího.*/
SELECT *
FROM Employee
ORDER BY YEAR(birthDate), MONTH(birthDate), DAY(birthDate)

/*1;3;1;
Vypíše průměrnou kapacitu cely.*/
SELECT AVG(CAST(capacity AS FLOAT)) AS avgCapacity
FROM Cell

/*1;4;1;
Vypíše nejmenší kapacitu cely.*/
SELECT MIN(capacity) AS minCapacity
FROM Cell

--SKUPINA 2
/*2;1;2;
Výpis všech ředitelů věznice Praha(1) nebo Brno(2).*/
SELECT firstName, lastName
FROM Employee
WHERE (Prison_prison_id = 1 OR Prison_prison_id = 2) AND warden = 1

/*2;2;1;
Výpis všech věznic jejichž typ není ani 1 ani 2.*/
SELECT Prison.prison_id, Prison.address, Prison.city, Prison.PrisonType_prisonType_id
FROM Prison
WHERE PrisonType_prisonType_id NOT IN(1, 2)

/*2;3;5;
Výpis všech vězňů s příjmením začínajícím na S.*/
SELECT DISTINCT lastName
FROM Prisoner
WHERE lastName LIKE 'S%'
ORDER BY lastName

/*2;4;1;
V rámci věznice Praha(1) vypíše průměrnou kapacitu cel zaokrouhelnou dolů.*/
SELECT Prison_prison_id, FLOOR(AVG(CAST(capacity AS FLOAT))) AS avgCellCapacity
FROM Cell
WHERE Prison_prison_id = 1
GROUP BY Prison_prison_id

--SKUPINA 3
/*3;1;1;
Výpis všech prázdných cel.*/
SELECT cell_id
FROM Cell
WHERE Cell.cell_id NOT IN(SELECT Cell_cell_id FROM Prisoner)

/*3;2;1;
Výpis všech prázdných cel.*/
SELECT cell_id
FROM Cell
WHERE NOT EXISTS(SELECT * FROM Prisoner WHERE Cell.cell_id = Prisoner.Cell_cell_id)

/*3;3;1;
Výpis všech prázdných cel.*/
SELECT cell_id
FROM Cell
WHERE cell_id != ALL(SELECT Cell_cell_id FROM Prisoner)

/*3;4;1;
Výpis všech prázdných cel.*/
SELECT cell_id
FROM Cell
EXCEPT (SELECT Cell_cell_id FROM Prisoner)

--SKUPINA 4
/*4;1;1;
Vypíše nejvyšší kapacitu cely.*/
SELECT MAX(capacity) AS maxCapacity
FROM Cell

/*4;2;42;
Vypíše počet změn cely u jednotlivých vězňů.*/
SELECT firstName, lastName, COALESCE(COUNT(prisonerCellHistory_id),0) AS prisonerChangeCellCount
FROM Prisoner
	LEFT JOIN PrisonerCellHistory ON Prisoner.prisoner_id = PrisonerCellHistory.Prisoner_prisoner_id
GROUP BY firstName, lastName

/*4;3;42;
Vypíše počet návštěv jednotlivých vězňů.*/
SELECT Prisoner.firstName, Prisoner.lastName, COALESCE(COUNT(visit_id),0) AS visitCount
FROM Prisoner
	LEFT JOIN Visit ON Prisoner.prisoner_id = Visit.Prisoner_prisoner_id
GROUP BY Prisoner.firstName, Prisoner.lastName

/*4;4;2;
Vypíše věznice, které mají kapacitu vyšší než 15.*/
SELECT Cell.Prison_prison_id, SUM(capacity) AS sumCapacity
FROM Cell
GROUP BY Cell.Prison_prison_id
HAVING SUM(capacity) > 15

--SKUPINA 5
/*5;1;8;
Vypíše všechny zaměstnance, kteří někdy změnili celu některému z vězňů.*/
SELECT Employee.*
FROM Employee
	JOIN PrisonerCellHistory ON PrisonerCellHistory.Employee_employee_id = Employee.employee_id
ORDER BY Employee.employee_id

/*5;2;8;
Vypíše všechny zaměstnance, kteří někdy změnili celu některému z vězňů.*/
SELECT Employee.*
FROM Employee
WHERE Employee.employee_id IN(SELECT PrisonerCellHistory.Employee_employee_id FROM PrisonerCellHistory)
ORDER BY Employee.employee_id

/*5;3;5;
Vypíše věznice a k nim jednotlivé počty zaměstnanců.*/
SELECT Prison.prison_id, COUNT(*) AS prisonEmployeeCount
FROM Prison
	LEFT JOIN Employee ON Employee.Prison_prison_id = Prison.prison_id
GROUP BY Prison.prison_id

/*5;4;1;
Vypíše návštěvy vězňů, kteří jsou mladší 25 let.*/
SELECT Visit.firstName, Visit.lastName
FROM Visit
	LEFT JOIN Prisoner ON Visit.Prisoner_prisoner_id = Prisoner.prisoner_id
WHERE DATEDIFF(YEAR, Prisoner.birthDate, GETDATE()) < 25
GROUP BY Visit.firstName, Visit.lastName

--SKUPINA 6
/*6;1;1;
Vypíše počet zaměstnankyň ve věznici jejíž ředitelem je žena.*/
SELECT p.prison_id,
	(
		SELECT COUNT(Employee.employee_id)
		FROM Employee
		WHERE Employee.gender = 'F' AND Employee.Prison_prison_id = p.prison_id
	) AS womanEmployeeCount
FROM Prison p
	JOIN Employee ON Employee.Prison_prison_id = p.prison_id AND Employee.gender = 'F' AND Employee.warden = 1

/*6;2;1;
Vypíše vězně, kteří mají nejvyšší počet návštěv.*/
SELECT Prisoner.prisoner_id, Prisoner.firstName, Prisoner.lastName, COUNT(Visit.visit_id) AS visitCount
FROM Prisoner
	JOIN Visit ON Visit.Prisoner_prisoner_id = Prisoner.prisoner_id
GROUP BY Prisoner.prisoner_id, Prisoner.firstName, Prisoner.lastName
HAVING COUNT(Visit.visit_id) >= ALL(
	SELECT COUNT(Visit.visit_id)
	FROM Prisoner
		JOIN Visit ON Visit.Prisoner_prisoner_id = Prisoner.prisoner_id
	GROUP BY Prisoner.prisoner_id
)