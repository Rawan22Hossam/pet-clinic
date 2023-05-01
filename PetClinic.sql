CREATE DATABASE PetClinic
USE PetClinic


CREATE TABLE users(
ID INT PRIMARY KEY,
username VARCHAR(20) NOT NULL UNIQUE,
[password] VARCHAR(20) NOT NULL
);

CREATE TABLE appointments(
ID INT PRIMARY KEY,
[when] DATETIME NOT NULL UNIQUE,
[status] BIT /*reserved 1 not reserved 0*/
);

CREATE TABLE reservations(
ID INT PRIMARY KEY,
username VARCHAR(20) NOT NULL FOREIGN KEY (username) REFERENCES users,
appointment DATETIME NOT NULL FOREIGN KEY ([when]) REFERENCES appointments
);

CREATE PROCEDURE GetAvailabileAppointments
AS
BEGIN
	SELECT * FROM appointments WHERE [status] = 0
END

CREATE PROCEDURE GetUsernameAvailability
@username VARCHAR(20) NOT NULL
AS
BEGIN
	SELECT  username FROM users WHERE [username] LIKE @username
END

CREATE PROCEDURE Register
@username VARCHAR(20),
@password VARCHAR(20)
AS
BEGIN
	INSERT INTO users VALUES ((SELECT COALESCE(MAX(id), 0) + 1 FROM users), @username, @password)
END

CREATE PROCEDURE UpdatePassword
@username VARCHAR(20),
@password VARCHAR(20)
AS
BEGIN
	UPDATE users SET [password] = @password WHERE username = @username
END

CREATE PROCEDURE DeleteUser
@username INT
AS
BEGIN
	DELETE FROM users WHERE username = @username
END

/**/

CREATE PROCEDURE AddAppointment
@when DATETIME
AS
BEGIN
	INSERT INTO appointments VALUES ((SELECT COALESCE(MAX(id), 0) + 1 FROM appointments), @when, 0)
END


CREATE PROCEDURE DeleteAppointments
@when DATETIME
AS
BEGIN
	DELETE FROM appointments WHERE [when] = @when
END

CREATE PROCEDURE ReserveAppointments
@username VARCHAR(20),
@appointment DATETIME
AS
BEGIN
	INSERT INTO reservations VALUES ((SELECT COALESCE(MAX(id), 0) + 1 FROM reservations), @username, @appointment)
	IF @@error = 0
		UPDATETABLE appointments SET [status] = 1 WHERE [when] = @appointment
	ELSE
		ROLLBACK
END

CREATE PROCEDURE [Login]
@username VARCHAR(20),
@password VARCHAR(20)
AS
BEGIN
	SELECT * FROM users WHERE [username] = @username AND [password] = @password
END