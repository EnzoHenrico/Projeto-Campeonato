Use DB_Futebol;
GO

CREATE OR ALTER PROC ESTATISTICAS_CAMPEAO
AS
BEGIN
    SELECT TOP(1)
        b.Nome,
        b.Apelido,
        b.DataCriacao, 
        a.Vitorias, 
        a.Derrotas, 
        a.Empates, 
        a.Pontuacao, 
        a.GolsPro,
        a.GolsContra,
        a.SaldoGols 
    FROM Classificacao a 
    JOIN Clube b 
    ON a.Clube_id = b.Clube_id
    ORDER BY a.Pontuacao DESC;
END