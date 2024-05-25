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

EXEC ADICIONAR_CLUBE 'Residencial Vitta FC', 'Furadeiras', '2018-07-01';
EXEC ADICIONAR_CLUBE 'Santa Angelina FC', 'Vovôs da villa', '1820-01-01';
EXEC ADICIONAR_CLUBE 'Parque São Paulo SA', 'Quebrada Boys', '1990-12-01';
EXEC ADICIONAR_CLUBE 'Clube de Regatas Damah', 'Sueter Salmão', '2015-10-13';
EXEC ADICIONAR_CLUBE 'Tropa do Santana', 'Old Schools', '1912-05-12';