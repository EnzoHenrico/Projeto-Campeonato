Use DB_Futebol;
GO

CREATE OR ALTER PROC REGISTRAR_PARTIDA 
    @Mandante_id INT,
    @Visitante_id INT,
    @GolsMandante INT,
    @GolsVisitante INT
AS
BEGIN    
    -- Insere a partida na tabela de partidas
    IF (@Mandante_id = @Visitante_id)
    BEGIN
            RAISERROR('Erro ao criar partida: Um clube não pode jogar contra ele mesmo.', 1, 1);
    END
    ELSE
    BEGIN
        INSERT INTO Partida VALUES(@Mandante_id, @Visitante_id, @GolsMandante, @GolsVisitante);
    END

    -- Atualiza a tabela de pontuação dos times
    IF (@GolsMandante = @GolsVisitante)
    BEGIN
        -- Mandante empate
        UPDATE Classificacao 
        SET Pontuacao = Pontuacao + 1,
            SaldoGols = SaldoGols + (@GolsMandante - @GolsVisitante),
            GolsPro = GolsPro + @GolsMandante,
            GolsContra = GolsContra + @GolsVisitante,
            Empates = Empates + 1
        WHERE Clube_id = @Mandante_id

        -- Visitante empate
        UPDATE Classificacao 
        SET Pontuacao = Pontuacao + 1,
            SaldoGols = SaldoGols + (@GolsVisitante - @GolsMandante),
            GolsPro = GolsPro + @GolsVisitante,
            GolsContra = GolsContra + @GolsMandante,
            Empates = Empates + 1
        WHERE Clube_id = @Visitante_id
    END
    IF (@GolsMandante > @GolsVisitante)
    BEGIN
        -- Mandante Vitória
        UPDATE Classificacao 
        SET Pontuacao = Pontuacao + 3,
            SaldoGols = SaldoGols + (@GolsMandante - @GolsVisitante),
            GolsPro = GolsPro + @GolsMandante,
            GolsContra = GolsContra + @GolsVisitante,
            Vitorias = Vitorias + 1
        WHERE Clube_id = @Mandante_id

        -- Visitante Derrota
        UPDATE Classificacao 
        SET SaldoGols = SaldoGols + (@GolsVisitante - @GolsMandante),
            GolsPro = GolsPro + @GolsVisitante,
            GolsContra = GolsContra + @GolsMandante,
            Derrotas = Derrotas + 1
        WHERE Clube_id = @Visitante_id
    END
    IF (@GolsMandante < @GolsVisitante)
    BEGIN
        -- Mandante Derrota
        UPDATE Classificacao 
        SET SaldoGols = SaldoGols + (@GolsMandante - @GolsVisitante),
            GolsPro = GolsPro + @GolsMandante,
            GolsContra = GolsContra + @GolsVisitante,
            Derrotas = Derrotas + 1
        WHERE Clube_id = @Mandante_id

        -- Visitante Vitória
        UPDATE Classificacao 
        SET Pontuacao = Pontuacao + 5,
            SaldoGols = SaldoGols + (@GolsVisitante - @GolsMandante),
            GolsPro = GolsPro + @GolsVisitante,
            GolsContra = GolsContra + @GolsMandante,
            Vitorias = Vitorias + 1
        WHERE Clube_id = @Visitante_id
    END
END

-- EXEC REGISTRAR_PARTIDA 1, 2, 3, 1;
-- EXEC REGISTRAR_PARTIDA 1, 3, 2, 0;
-- EXEC REGISTRAR_PARTIDA 1, 4, 1, 4;
-- EXEC REGISTRAR_PARTIDA 1, 5, 6, 0;

-- EXEC REGISTRAR_PARTIDA 2, 1, 3, 0;
-- EXEC REGISTRAR_PARTIDA 2, 3, 2, 0;
-- EXEC REGISTRAR_PARTIDA 2, 4, 5, 2;
-- EXEC REGISTRAR_PARTIDA 2, 5, 1, 1;

-- EXEC REGISTRAR_PARTIDA 3, 1, 1, 5;
-- EXEC REGISTRAR_PARTIDA 3, 2, 1, 7;
-- EXEC REGISTRAR_PARTIDA 3, 4, 7, 0;
-- EXEC REGISTRAR_PARTIDA 3, 5, 2, 1;

-- EXEC REGISTRAR_PARTIDA 4, 1, 0, 5;
-- EXEC REGISTRAR_PARTIDA 4, 2, 0, 4;
-- EXEC REGISTRAR_PARTIDA 4, 3, 2, 0;
-- EXEC REGISTRAR_PARTIDA 4, 5, 1, 8;

-- EXEC REGISTRAR_PARTIDA 5, 1, 2, 3;
-- EXEC REGISTRAR_PARTIDA 5, 2, 7, 0;
-- EXEC REGISTRAR_PARTIDA 5, 3, 1, 1;
-- EXEC REGISTRAR_PARTIDA 5, 4, 0, 1;

-- SELECT * FROM Partida;