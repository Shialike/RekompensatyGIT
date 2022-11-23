
CREATE TABLE [User] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] text  NOT NULL,
    [Surname] text  NOT NULL,
	PRIMARY KEY ([Id] ASC)
);


-- Creating table 'HuntedAnimals'
CREATE TABLE "HuntedAnimal" (
	"Id"	uniqueidentifier NOT NULL,
	"UserId"	uniqueidentifier NOT NULL,
	"HuntDate"	datetime NOT NULL,
	"Weight"	float NOT NULL,
	"PricePerKilo"	decimal(18, 0) NOT NULL,
	"AnimalTypeId"	uniqueidentifier NOT NULL,
	"RevenueValue"	REAL DEFAULT '0.2',
	"HuntingArea"	INTEGER NOT NULL,
	PRIMARY KEY("Id" ASC)
);


-- Creating table 'AnimalTypes'
CREATE TABLE [AnimalType] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] text  NOT NULL,
	PRIMARY KEY ([Id] ASC)
);

--Run UPDATER after creating DB