DROP DATABASE IF EXISTS fitness_hero;
CREATE DATABASE fitness_hero;

USE fitness_hero;

CREATE TABLE IF NOT EXISTS user (
    id INT NOT NULL AUTO_INCREMENT,
    uuid VARCHAR(36) NOT NULL,
    first_name VARCHAR(24) NOT NULL,
    last_name VARCHAR(24) NOT NULL,
    email VARCHAR(128) UNIQUE NOT NULL,
    password VARBINARY(128) NOT NULL,
    password_salt VARBINARY(128) NOT NULL,
    created_at DATETIME NOT NULL,
    auth_token VARCHAR(4096),
    PRIMARY KEY( id )
);

CREATE TABLE IF NOT EXISTS nutrient (
    id INT NOT NULL AUTO_INCREMENT,
    proteins FLOAT NOT NULL,
    carbohydrates FLOAT NOT NULL,
    fats FLOAT NOT NULL,
    PRIMARY KEY( id )
);

CREATE TABLE IF NOT EXISTS food (
    id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(64) NOT NULL,
    brand VARCHAR(64),
    barcode VARCHAR(44),
    nutrient_id INT NOT NULL,
    PRIMARY KEY( id ),
    FOREIGN KEY( nutrient_id ) REFERENCES nutrient( id )
);

CREATE TABLE IF NOT EXISTS food_diary (
    id INT NOT NULL AUTO_INCREMENT,
    meal ENUM('breakfast', 'lunch', 'dinner', 'snacks'),
    grams INT NOT NULL,
    user_id INT NOT NULL,
    food_id INT NOT NULL,
    date DATE NOT NULL,
    PRIMARY KEY( id ),
    FOREIGN KEY( user_id ) REFERENCES user( id ),
    FOREIGN KEY( food_id ) REFERENCES food( id )
);