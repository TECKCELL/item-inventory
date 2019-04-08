USE SCANDEV
GO
CREATE TABLE #CSVTest
(Id INT,
EAN13 NVARCHAR(max))
GO
BULK
INSERT #CSVTest
FROM 'C:\Users\TEKI\source\repos\dev.scan_back\dev.scan_back\UploadedFiles\SCANS.txt'
WITH
(
CODEPAGE  = 'RAW',
FIELDTERMINATOR = ';',
ROWTERMINATOR = '\n'
)
GO
DECLARE @EAN13 nvarchar(max)
DECLARE @ImportId INT
SET @ImportId = ABS(Checksum(NewID()) % 1000)+1
DECLARE @ClientId int
--DECLARE @id int
SET @ClientId = (SELECT ID FROM [dbo].[Clients])
--SET @Id = (SELECT distinct  ImportId FROM inserted)
SET IDENTITY_INSERT dbo.Imports ON;
INSERT INTO [dbo].[Imports](Id,[ClientId]
           ,[Type]
           ,[Date])
		   VALUES(@ImportId,@ClientId,2,GETDATE())

DECLARE EAN CURSOR
LOCAL STATIC READ_ONLY FORWARD_ONLY
FOR
SELECT EAN13 FROM #CSVTest

OPEN EAN
FETCH NEXT FROM EAN INTO @EAN13 
WHILE @@FETCH_STATUS = 0
BEGIN 
--Check the content of the table.
INSERT INTO SCANS(ImportId,Ean13)
VALUES(@ImportId,@EAN13) 
    FETCH NEXT FROM EAN  INTO @EAN13
END
CLOSE EAN 
DEALLOCATE EAN
  


--Drop the table to clean up database.
DROP TABLE #CSVTest
GO