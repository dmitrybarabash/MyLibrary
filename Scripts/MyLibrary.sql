-- Создаем базу данных

IF DB_ID('MyLibrary') IS NOT NULL
    DROP DATABASE MyLibrary;
GO

CREATE DATABASE MyLibrary;
GO


-- Делаем созданную базу данных текущей

USE MyLibrary
GO


-- Создаем таблицы

--IF OBJECT_ID('Books') IS NOT NULL
--    DROP TABLE Books;
--IF OBJECT_ID('Authors') IS NOT NULL
--    DROP TABLE Authors;
--IF OBJECT_ID('Presses') IS NOT NULL
--    DROP TABLE Presses;
--GO

CREATE TABLE Authors
(
	Id bigint IDENTITY NOT NULL PRIMARY KEY,
	Name nvarchar(50)
);
GO

CREATE TABLE Presses
(
	Id bigint IDENTITY NOT NULL PRIMARY KEY,
	Name nvarchar(50)
);
GO

CREATE TABLE Books
(
	Id bigint IDENTITY NOT NULL PRIMARY KEY,
	AuthorFk bigint,
	Name nvarchar(100),
	Pages int CONSTRAINT ChkPages CHECK (Pages > 0 AND Pages < 5000),
	Price money CONSTRAINT ChkPrice CHECK (Price <= 20000),
	PressFk bigint,

	FOREIGN KEY (AuthorFk) REFERENCES Authors(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	FOREIGN KEY (PressFk) REFERENCES Presses(Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO


-- Заполняем таблицы

SET IDENTITY_INSERT Authors ON
INSERT INTO Authors (Id, Name) VALUES (1, N'Братья Стругацкие');
INSERT INTO Authors (Id, Name) VALUES (2, N'Айзек Азимов');
INSERT INTO Authors (Id, Name) VALUES (3, N'Роналд Руел Толкиен');
INSERT INTO Authors (Id, Name) VALUES (4, N'Стивен Кинг');
INSERT INTO Authors (Id, Name) VALUES (5, N'Джордж Мартин');
SET IDENTITY_INSERT Authors OFF
GO

SET IDENTITY_INSERT Presses ON
INSERT INTO Presses (Id, Name) VALUES (1, N'АСТ');
INSERT INTO Presses (Id, Name) VALUES (2, N'Эксмо');
INSERT INTO Presses (Id, Name) VALUES (3, N'Росмен');
SET IDENTITY_INSERT Presses OFF
GO

SET IDENTITY_INSERT Books ON
INSERT INTO Books (Id, AuthorFk, Name, Pages, Price, PressFk) VALUES (1, 1, N'Обитаемый остров', 500, 129.55, 1);
INSERT INTO Books (Id, AuthorFk, Name, Pages, Price, PressFk) VALUES (2, 1, N'Жук в муравейнике', 400, 95.5, 1);
INSERT INTO Books (Id, AuthorFk, Name, Pages, Price, PressFk) VALUES (3, 1, N'Волны гасят ветер', 300, 90, 1);
INSERT INTO Books (Id, AuthorFk, Name, Pages, Price, PressFk) VALUES (4, 2, N'Основание', 450, 150, 2);
INSERT INTO Books (Id, AuthorFk, Name, Pages, Price, PressFk) VALUES (5, 2, N'Я, робот', 250, 85.99, 2);
INSERT INTO Books (Id, AuthorFk, Name, Pages, Price, PressFk) VALUES (6, 2, N'Стальные пещеры', 500, 99.99, 2);
INSERT INTO Books (Id, AuthorFk, Name, Pages, Price, PressFk) VALUES (7, 3, N'Хоббит, или Туда и обратно', 150, 79.99, 3);
INSERT INTO Books (Id, AuthorFk, Name, Pages, Price, PressFk) VALUES (8, 3, N'Властелин колец', 2000, 800, 3);
SET IDENTITY_INSERT Books OFF
GO


-- Проверка

SELECT *
FROM Authors;
GO

SELECT *
FROM Presses;
GO

SELECT *
FROM Books;
GO

SELECT A.Name, B.Name, B.Pages, B.Price, P.Name
FROM Authors A, Presses P, Books B
WHERE B.AuthorFk IS NOT NULL AND B.PressFk IS NOT NULL
      AND A.Id = B.AuthorFk AND P.Id = B.PressFk;
GO
