USE SCANDEV
GO
CREATE TABLE #CSVTest
(Id INT,Label NVARCHAR(MAX),
EAN13 NVARCHAR(MAX),Price DECIMAL(18,2),categorie NVARCHAR(MAX))

GO
BULK
INSERT #CSVTest
FROM 'C:\Users\TEKI\source\repos\dev.scan_back\dev.scan_back\UploadedFiles\DataClient.txt'
WITH
(
CODEPAGE  = 'RAW',
FIELDTERMINATOR = ';',
ROWTERMINATOR = '\n'
)
GO
DECLARE @EAN13 nvarchar(max)
DECLARE @Label nvarchar(max)
DECLARE @Price decimal(18,2)
DECLARE @categorie nvarchar(max)
DECLARE @ImportId INT
SET @ImportId = ABS(Checksum(NewID()) % 10000)+1
DECLARE @ClientId int
--DECLARE @id int
SET @ClientId = (SELECT ID FROM [dbo].[Clients])
--SET @Id = (SELECT distinct  ImportId FROM inserted)
SET IDENTITY_INSERT dbo.Imports ON;
INSERT INTO [dbo].[Imports](Id,[ClientId]
           ,[Type]
           ,[Date])
		   VALUES(@ImportId,@ClientId,1,GETDATE())

DECLARE EAN CURSOR
LOCAL STATIC READ_ONLY FORWARD_ONLY
FOR
SELECT EAN13, Label,Price,categorie FROM #CSVTest

OPEN EAN
FETCH NEXT FROM EAN INTO @EAN13, @Label,@Price,@categorie
WHILE @@FETCH_STATUS = 0
BEGIN 
--Check the content of the table.
INSERT INTO assorts(ImportId,Ean13,Label,Price,categorie)
VALUES(@ImportId,@EAN13,@Label,@Price,@categorie) 
    FETCH NEXT FROM EAN  INTO @EAN13, @Label,@Price,@categorie
END
CLOSE EAN 
DEALLOCATE EAN
  


--Drop the table to clean up database.
DROP TABLE #CSVTest
GO