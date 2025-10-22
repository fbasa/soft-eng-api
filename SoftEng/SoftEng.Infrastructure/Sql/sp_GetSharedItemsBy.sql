CREATE OR ALTER PROCEDURE sp_GetSharedItemsBy
    @Type NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Name
    FROM SharedTable
    WHERE Type = @Type
    ORDER BY Name;
END
