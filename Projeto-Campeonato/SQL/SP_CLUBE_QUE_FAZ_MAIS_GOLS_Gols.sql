Use DB_Futebol;
GO

CREATE OR ALTER PROC CLUBE_QUE_FEZ_MAIS_GOLS
AS
BEGIN
    SELECT TOP(1) a.Apelido, b.GolsPro
    FROM Clube a 
    JOIN Classificacao b 
    ON a.Clube_id = b.Clube_id
    ORDER BY b.GolsPro DESC;
END