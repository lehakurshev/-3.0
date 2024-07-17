CREATE DATABASE "football players";

-- Переключение на созданную БД
\c "football players"


CREATE TABLE players (
    id INT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(1000) NOT NULL,
    surname VARCHAR(1000) NOT NULL,
    team_name VARCHAR(1000),
    gender VARCHAR(50) NOT NULL,
    date_of_birth DATE NOT NULL,
    country VARCHAR(50) NOT NULL
);


INSERT INTO players (name, surname, team_name, gender, date_of_birth, country)
VALUES ('Mario', 'Balotelli', 'Brescia', 'Man', '1990-08-12', 'Italy'),
       ('Leonardo', 'Bonucci', 'Milan', 'Man', '1987-05-01', 'Italy'),
       ('Fabio', 'Cannavaro', 'Guangzhou Evergrande', 'Man', '1973-09-13', 'Italy'),
       ('Paolo', 'Maldini', 'Milan', 'Man', '1968-06-26', 'Italy'),
       ('Landon', 'Donovan', 'Los Angeles Galaxy', 'Man', '1982-03-04', 'USA'),
       ('Clint', 'Dempsey', 'Seattle Sounders', 'Man', '1983-03-09', 'USA'),
       ('Christian', 'Pulisic', 'Chelsea', 'Man', '1998-09-18', 'USA'),
       ('Michael', 'Bradley', 'Toronto FC', 'Man', '1987-07-31', 'USA'),
       ('Jozy', 'Altidore', 'Toronto FC', 'Man', '1989-11-06', 'USA'),
       ('Matt', 'Miazga', 'Alavés', 'Man', '1995-07-19', 'USA'),
       ('Tim', 'Howard', 'Memphis 901 FC', 'Man', '1979-03-06', 'USA'),
       ('Christian', 'Vieri', 'Retired', 'Man', '1973-07-12', 'Italy'),
       ('Andrea', 'Belotti', 'Torino', 'Man', '1993-12-20', 'Italy'),
       ('Federico', 'Chiesa', 'Juventus', 'Man', '1997-10-25', 'Italy'),
       ('Sebastian', 'Giovinco', 'Al-Hilal', 'Man', '1987-01-26', 'Italy'),
       ('Victor', 'Osimhen', 'Napoli', 'Man', '1998-12-29', 'Italy');

