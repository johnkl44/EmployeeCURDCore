CREATE DATABASE EmployeeDB

USE EmployeeDB

CREATE TABLE tblEmployee
(
ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
FirstName NVARCHAR(100) NOT NULL,
LastName NVARCHAR(100) NOT NULL,
DOB DATE NOT NULL,
Email NVARCHAR(100) NOT NULL,
Salary FLOAT NOT NULL
)

SELECT * FROM tblEmployee

--- Read All Employees

CREATE PROCEDURE SPR_Employee
AS
BEGIN
	SELECT ID,FirstName,LastName,DOB,Email,Salary FROM tblEmployee WITH(NOLOCK)
END

EXEC SPR_Employee

-- Get by id

CREATE PROCEDURE SPI_EmployeeByID
(
@ID INT
)
AS
BEGIN
	SELECT ID,FirstName,LastName,DOB,Email,Salary FROM tblEmployee WITH(NOLOCK)
	WHERE ID= @ID
END 

-- Insert

ALTER PROCEDURE SPI_Employee
(
@FirstName NVARCHAR(100),
@LastName NVARCHAR(100),
@DOB DATE,
@Email NVARCHAR(100),
@Salary FLOAT
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		INSERT INTO tblEmployee(FirstName,LastName,DOB,Email,Salary)
		VALUES(@FirstName,@LastName,@DOB,@Email,@Salary)
		COMMIT TRAN
	END TRY
	BEGIN CATCH
			ROLLBACK TRAN
	END CATCH
END 

-- Update

CREATE PROCEDURE SPU_Employee
(
@ID INT,
@FirstName NVARCHAR(100),
@LastName NVARCHAR(100),
@DOB DATE,
@Email NVARCHAR(100),
@Salary FLOAT
)
AS
BEGIN
DECLARE @RowCount INT = 0
	BEGIN TRY
		SET @RowCount=(SELECT COUNT(1) FROM tblEmployee WITH (NOLOCK) WHERE ID=@ID)
		IF @RowCount > 0
			BEGIN
				BEGIN TRAN
					UPDATE tblEmployee 
					SET 
					FirstName = @FirstName,
					LastName = @LastName,
					DOB = @DOB,
					Email = @Email,
					Salary = @Salary
					WHERE ID =@ID
				COMMIT TRAN
			END
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END 

--- delete

CREATE PROCEDURE SPD_Employee
(
@ID INT
)
AS
BEGIN
DECLARE @RowCount INT = 0
	BEGIN TRY
		SET @RowCount=(SELECT COUNT(1) FROM tblEmployee WITH (NOLOCK) WHERE ID=@ID)
		IF @RowCount > 0
			BEGIN
				BEGIN TRAN
					DELETE FROM tblEmployee 
					WHERE ID =@ID
				COMMIT TRAN
			END
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH
END 

