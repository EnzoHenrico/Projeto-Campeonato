Use DB_Futebol;
GO

CREATE OR ALTER PROC MAIOR_NUMERO_GOLS_POR_CLUBE
AS
BEGIN
    SELECT (SELECT Apelido FROM Clube WHERE Clube.Clube_id = a.ClubeVisitante_id) As Apelido, 
    CASE WHEN 
        MAX(a.GolsVisitante) > (
            SELECT MAX(b.GolsMandante) FROM partida b WHERE a.ClubeVisitante_id = b.ClubeMandante_id GROUP BY b.ClubeMandante_id
        )
    THEN MAX(a.GolsVisitante) 
    ELSE (
        SELECT MAX(b.GolsMandante) FROM partida b WHERE a.ClubeVisitante_id = b.ClubeMandante_id GROUP BY b.ClubeMandante_id
    )
    END AS TotalGols
    FROM partida a
    GROUP BY a.ClubeVisitante_id
END