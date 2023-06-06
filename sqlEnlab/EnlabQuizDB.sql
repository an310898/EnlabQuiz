USE master;
GO
DROP DATABASE IF EXISTS EnlabQuizDB;
GO
CREATE DATABASE EnlabQuizDB;
GO
USE EnlabQuizDB;
GO

CREATE TABLE Quiz
(
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    Question NVARCHAR(100),
    Answer NVARCHAR(1000),
    Correct NVARCHAR(100)
);

CREATE TABLE PlayQuiz
(
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    StartTime DATETIME
        DEFAULT GETDATE(),
    EndTime DATETIME NULL
);

CREATE TABLE HistoryPlay
(
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    PlayId INT
        FOREIGN KEY REFERENCES dbo.PlayQuiz (Id),
    QuizId INT
        FOREIGN KEY REFERENCES dbo.Quiz (Id),
    Choice VARCHAR(100)
);

GO


INSERT INTO dbo.Quiz
(
    Question,
    Answer,
    Correct
)
VALUES
(   'What time is it?<br />It’s 6.15 – a _________ past six.', -- Question - nvarchar(100)
    '["fifteen","fourth","half","quarter"]',                   -- Answer - nvarchar(100)
    'quarter');

INSERT INTO dbo.Quiz
(
    Question,
    Answer,
    Correct
)
VALUES
(   'They never argue and they enjoy spending time together. = They _________.',                    -- Question - nvarchar(100)
    '["get on very well","relationship very good","like themselves very much","relate very well"]', -- Answer - nvarchar(100)
    'get on very well');

INSERT INTO dbo.Quiz
(
    Question,
    Answer,
    Correct
)
VALUES
(   'What time do you go to _________ every day?', -- Question - nvarchar(100)
    '["workplace","office","work","job"]',         -- Answer - nvarchar(100)
    'work');

INSERT INTO dbo.Quiz
(
    Question,
    Answer,
    Correct
)
VALUES
(   'Excuse me, I think you’ve _________ a mistake in our bill.', -- Question - nvarchar(100)
    '["given","done","had","made"]',                              -- Answer - nvarchar(100)
    'made');

INSERT INTO dbo.Quiz
(
    Question,
    Answer,
    Correct
)
VALUES
(   'Our teacher doesn’t _________ us use mobile phones in class.', -- Question - nvarchar(100)
    '["allow","make","forbid","let"]',                              -- Answer - nvarchar(100)
    'let');

INSERT INTO dbo.Quiz
(
    Question,
    Answer,
    Correct
)
VALUES
(   '‘Happy’ is the _________ of ‘sad’.',           -- Question - nvarchar(100)
    '["oppositive","opposed","opposite","oppose"]', -- Answer - nvarchar(100)
    'opposite');

INSERT INTO dbo.Quiz
(
    Question,
    Answer,
    Correct
)
VALUES
(   'tired –> exhausted <br/>
small –> tiny <br/>
angry –> _________'                                                    , -- Question - nvarchar(100)
'["irritated","vexed","furious","annoyed"]',                             -- Answer - nvarchar(100)
'furious');

INSERT INTO dbo.Quiz
(
    Question,
    Answer,
    Correct
)
VALUES
(   'friendly –> unfriendly <br/>
honest –> dishonest <br/>
polite –> _________'                                                              , -- Question - nvarchar(100)
'["unpolite","dispolite","impolite","inpolite"]',                                   -- Answer - nvarchar(100)
'impolite');

INSERT INTO dbo.Quiz
(
    Question,
    Answer,
    Correct
)
VALUES
(   'Her hair isn’t completely straight – it’s slightly _________.', -- Question - nvarchar(100)
    '["bent","waved","curl","wavy"]',                                -- Answer - nvarchar(100)
    'wavy');

INSERT INTO dbo.Quiz
(
    Question,
    Answer,
    Correct
)
VALUES
(   'Do you live in a house or _________?',               -- Question - nvarchar(100)
    '["a building","a home","a village","an apartment"]', -- Answer - nvarchar(100)
    'an apartment');


GO

CREATE PROCEDURE EndPlay @PlayId INT
AS
BEGIN
    UPDATE dbo.PlayQuiz
    SET EndTime = GETDATE()
    WHERE Id = @PlayId;

    SELECT PQ.Id AS PlayId,
           PQ.StartTime,
           PQ.EndTime,
           (
               SELECT HP.Id AS HistoryId,
                      HP.PlayId,
                      HP.QuizId,
                      HP.Choice,
                      Q.Id AS QuestId,
                      Q.Question,
                      Q.Answer,
                      Q.Correct,
                      IIF(HP.Choice = Q.Correct, (SELECT 1), (SELECT 0)) AS IsCorrect
               FROM dbo.HistoryPlay HP
                   JOIN dbo.Quiz Q
                       ON HP.QuizId = Q.Id
               WHERE HP.PlayId = PQ.Id
               FOR JSON PATH
           ) AS Choice
    FROM dbo.PlayQuiz PQ
    WHERE PQ.Id = @PlayId;
END;