Use DB_Futebol;
GO

CREATE OR ALTER PROC PARTIDA_COM_MAIS_GOLS
AS
BEGIN
    SELECT TOP(1) 
        b.Apelido AS Mandante, 
        c.Apelido AS Visitante, 
        a.GolsMandante,
        a.GolsVisitante,
        a.GolsMandante + a.GolsVisitante AS TotalGols
    FROM Partida a
    JOIN Clube b
    ON a.ClubeMandante_id = b.Clube_id
    JOIN Clube c
    ON  a.ClubeVisitante_id = c.Clube_id
    ORDER BY (GolsMandante + GolsVisitante) DESC
END