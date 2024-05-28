Use DB_Futebol;
GO

-- Insere clube no campeonato e na tabela de classificação
CREATE OR ALTER PROC ADICIONAR_CLUBE (
    @Nome VARCHAR(50),
    @Apelido VARCHAR(20),
    @DataCriacao DATE
)
AS
BEGIN
    DECLARE @id INT;
    INSERT INTO Clube VALUES(@Nome, @Apelido, @DataCriacao);
    SELECT @id = Clube_id FROM Clube WHERE Nome = @Nome;  
    INSERT INTO Classificacao VALUES(@id, 0, 0, 0, 0, 0, 0, 0);
END