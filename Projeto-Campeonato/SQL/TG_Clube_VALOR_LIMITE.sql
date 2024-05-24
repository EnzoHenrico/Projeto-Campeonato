CREATE OR ALTER TRIGGER VALOR_LIMITE
ON [DB_Futebol].dbo.Clube
INSTEAD OF INSERT
AS
    DECLARE 
        @Nome VARCHAR(50),
        @Apelido VARCHAR(20),
        @DataCriacao DATE

    IF (SELECT COUNT(*) FROM Clube) < 5
    BEGIN
        SELECT @Nome = Nome, @Apelido = Apelido, @DataCriacao = DataCriacao FROM inserted;
        INSERT INTO Clube VALUES(@Nome, @Apelido, @DataCriacao);
    END
    ELSE
    BEGIN
        RAISERROR('Impossível inserir: Limite de clubes atingido.', 1, 1);
    END
