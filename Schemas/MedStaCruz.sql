DROP DATABASE IF EXISTS MedStaCruz;
CREATE DATABASE MedStaCruz CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

DROP USER IF EXISTS 'msc'@'localhost';
CREATE USER 'msc'@'localhost' IDENTIFIED BY 'msc';
GRANT ALL PRIVILEGES ON MedStaCruz.* TO 'msc'@'localhost';
FLUSH PRIVILEGES;

USE MedStaCruz;


-- - - Tablas Para Administrador de Contenidos - - - - - - - - - - - - - - - - - - - - - - - - - - -
-- Representa a los usuarios del administrador de contenidos.

CREATE TABLE RoleType (
  Id      INT UNSIGNED AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Name    VARCHAR(100) NOT NULL UNIQUE,
  Label   VARCHAR(100) NOT NULL
);


CREATE TABLE Users (
  Id                  INT UNSIGNED AUTO_INCREMENT NOT NULL PRIMARY KEY,
  UserName            VARCHAR(15) NOT NULL,
  Name                VARCHAR(50) NOT NULL DEFAULT '',
  FirstSurname        VARCHAR(50) NOT NULL DEFAULT '',
  LastSurname         VARCHAR(50) NOT NULL DEFAULT '',
  Email               VARCHAR(50) NOT NULL UNIQUE,
  Password            VARCHAR(256) NOT NULL,
  RegistrationDate    DATETIME NOT NULL DEFAULT NOW(),
  CONSTRAINT uk_NM UNIQUE(UserName, Email)
);

CREATE TABLE AssignedRoles (
  Id                  INT UNSIGNED AUTO_INCREMENT NOT NULL PRIMARY KEY,
  User                INT UNSIGNED NOT NULL, -- DEFAULT NOT NULL 
  Role                VARCHAR(100) NOT NULL,
  CONSTRAINT fk_Role FOREIGN KEY(Role) REFERENCES RoleType(Name),
  CONSTRAINT fk_User FOREIGN KEY(User) REFERENCES Users(Id),
  CONSTRAINT uk_UR UNIQUE(User, Role)
);

-- - - - Tablas Para Catalogo de Productos - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

CREATE TABLE CategoryType (
  Id            INT UNSIGNED AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Name          VARCHAR(100) NOT NULL UNIQUE,
  Label         VARCHAR(100) NOT NULL,
  ParentId      INT UNSIGNED NOT NULL DEFAULT 0
);


CREATE TABLE CatalogueType (
  Id      INT UNSIGNED AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Name    VARCHAR(15) NOT NULL UNIQUE
);


-- Representa toda la tabla de productos que se mostrarán en la página

CREATE TABLE Products (
  Id                INT UNSIGNED AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Category          VARCHAR(100) NOT NULL,
  Catalogue         VARCHAR(15) NOT NULL,
  Name              VARCHAR(100) NOT NULL,
  Model             VARCHAR(100) NOT NULL,
  Brand             VARCHAR(100) NOT NULL,
  Description       TEXT NOT NULL,
  Stock             INT UNSIGNED NOT NULL,
  Price             INT UNSIGNED NOT NULL,
  Cost              INT UNSIGNED NOT NULL DEFAULT 0,
  Discount          INT UNSIGNED NOT NULL DEFAULT 0,
  Featured          BOOLEAN NOT NULL DEFAULT false,
  ImgPath           VARCHAR(100) NOT NULL DEFAULT './img/no_image.jpg',
  CONSTRAINT fk_SPType FOREIGN KEY(Category) REFERENCES CategoryType(Name),
  CONSTRAINT fk_CType FOREIGN KEY(Catalogue) REFERENCES CatalogueType(Name),
  CONSTRAINT uk_Details UNIQUE(Name, Model, Brand)
);


-- - - Tablas Para Carrito de Compras  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

CREATE TABLE ReservationType (
  Id      INT UNSIGNED AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Name    VARCHAR(15) NOT NULL UNIQUE,
  Label   VARCHAR(30) NOT NULL
);


-- Historial de Estados de la reservación
-- (Se realizo de esta manera para agregar la posibilidad de rastrear los estados de la reservación)
CREATE TABLE ReservationStatus (
  Id            INT UNSIGNED AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Code          VARCHAR(15) NOT NULL,
  StatusDate    DATETIME NOT NULL DEFAULT NOW(),
  CONSTRAINT fk_Status FOREIGN KEY(Code) REFERENCES ReservationType(Name)
);


-- Reservacion de compra
CREATE TABLE Reservations (
  Id              INT UNSIGNED AUTO_INCREMENT NOT NULL PRIMARY KEY,
  StatusHistory   INT UNSIGNED NOT NULL UNIQUE,
  CONSTRAINT fk_History FOREIGN KEY(StatusHistory) REFERENCES ReservationStatus(Id)
);


-- Lista de compras del usuario.
CREATE TABLE CartItems (
  Id              INT UNSIGNED AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Product         INT UNSIGNED NOT NULL,
  Reservation     INT UNSIGNED NOT NULL,
  Quantity        INT UNSIGNED NOT NULL,
  TotalPrice      INT UNSIGNED NOT NULL,
  CONSTRAINT fk_Reservation FOREIGN KEY(Reservation) REFERENCES Reservations(Id),
  CONSTRAINT fk_Product FOREIGN KEY(Product) REFERENCES Products(Id),
  CONSTRAINT uk_ProdR UNIQUE(Product, Reservation)
);


-- - - Tablas Para Información y Contacto de Sucursales  - - - - - - - - - - - - - - - - - - - - - -

-- Ubicación de Sucursales
CREATE TABLE StoreLocations (
  Id              INT UNSIGNED AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Name            VARCHAR(100) NOT NULL UNIQUE,
  Address         VARCHAR(100) NOT NULL DEFAULT '',
  Email           VARCHAR(100) NOT NULL DEFAULT '',
  Telephone       VARCHAR(15) NOT NULL DEFAULT '',
  FacebookUser    VARCHAR(50) NOT NULL DEFAULT '',
  TwitterUser     VARCHAR(50) NOT NULL DEFAULT '',
  WhatsAppPhone   VARCHAR(50) NOT NULL DEFAULT ''
);


-- Información de la Empresa
CREATE TABLE CompanyInformation (
  Description   TEXT NOT NULL,
  Mission       TEXT NOT NULL,
  Vision        TEXT NOT NULL,
  LogoPath      VARCHAR(100) NOT NULL DEFAULT './img/no_image.jpg'
);
