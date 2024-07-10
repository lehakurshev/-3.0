CREATE TYPE gender AS ENUM ('Man', 'Woman');
CREATE TYPE country AS ENUM ('Russia', 'USA', 'Italy');
CREATE TABLE player (
    id INT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(1000) NOT NULL,
    surname VARCHAR(1000) NOT NULL,
    team_name VARCHAR(1000),
    gender gender NOT NULL,
    date_of_birth DATE NOT NULL,
    country country NOT NULL
);




CREATE TABLE easy_player (
    id INT NOT NULL GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(1000) NOT NULL,
    surname VARCHAR(1000) NOT NULL,
    team_name VARCHAR(1000),
    gender VARCHAR(50) NOT NULL,
    date_of_birth DATE NOT NULL,
    country VARCHAR(50) NOT NULL
);
