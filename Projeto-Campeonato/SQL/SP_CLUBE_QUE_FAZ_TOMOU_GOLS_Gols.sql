Use DB_Futebol;
GO

CREATE OR ALTER PROC CLUBE_QUE_TOMOU_MAIS_GOLS
AS
BEGIN
    SELECT TOP(1) a.Apelido, b.GolsContra
    FROM Clube a 
    JOIN Classificacao b 
    ON a.Clube_id = b.Clube_id
    ORDER BY b.GolsContra DESC;
END