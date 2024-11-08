-- init-db.sql

-- Check if the database exists and create it if it doesn't
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'HangfireTest')
BEGIN
    CREATE DATABASE HangfireTest;
END
GO
