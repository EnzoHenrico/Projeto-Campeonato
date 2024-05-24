Use DB_Futebol;
GO

CREATE TABLE Clube(
    Clube_id INT IDENTITY(1,1) PRIMARY KEY, 
    Nome VARCHAR(50) UNIQUE,
    Apelido VARCHAR(20) UNIQUE,
    DataCriacao DATE,
) 
GO

CREATE TABLE Partida(
    ClubeMandante_id INT,
    ClubeVisitante_id INT,
    GolsMandante INT,
    GolsVisitante INT,
    PRIMARY KEY (ClubeMandante_id, ClubeVisitante_id),
    CONSTRAINT FK_Mandante FOREIGN KEY (ClubeMandante_id)
    REFERENCES Clube (Clube_id), 
    CONSTRAINT FK_Visitante FOREIGN KEY (ClubeVisitante_id)
    REFERENCES Clube (Clube_id),
)
GO

CREATE TABLE Classificacao(
    Clube_id INT, 
    Vitorias INT NOT NULL,
    Derrotas INT NOT NULL,
    Empates INT NOT NULL,
    Pontuacao INT NOT NULL,
    GolsPro INT NOT NULL,
    GolsContra INT NOT NULL,
    SaldoGols INT NOT NULL,
    PRIMARY KEY (Clube_id),
    CONSTRAINT FK_Clube FOREIGN KEY (Clube_id)
    REFERENCES Clube (Clube_id), 
)
GO

-- DROP TABLE Classificacao;
-- DROP TABLE Partida;
-- DROP TABLE Clube;

-- DELETE FROM Clube;
-- DELETE FROM Partida;
-- DELETE FROM Classificacao;


SELECT * FROM Clube;
SELECT * FROM Partida;
SELECT * FROM Classificacao;