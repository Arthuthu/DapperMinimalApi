IF not exists (SELECT 1 FROM dbo.[User])
BEGIN
	INSERT INTO dbo.[User] (FirstName, LastName)
	VALUES ('Tim', 'Corey'), ('Art', 'D'), ('John', 'Smith')
END