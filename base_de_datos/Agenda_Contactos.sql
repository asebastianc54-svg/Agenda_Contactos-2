CREATE DATABASE IF NOT EXISTS agenda_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE agenda_db;

CREATE TABLE IF NOT EXISTS contacto (
    DNI VARCHAR(20) NOT NULL PRIMARY KEY,
    Apellido VARCHAR(100) NOT NULL,
    Nombres VARCHAR(100) NOT NULL,
    Calle VARCHAR(150) NOT NULL,
    Depto VARCHAR(20) NULL,
    Piso VARCHAR(20) NULL,
    Ciudad VARCHAR(100) NOT NULL,
    Telefono VARCHAR(30) NOT NULL,
    Email VARCHAR(150) NOT NULL,
    CuilCuit VARCHAR(20) NULL,
    FechaAlta DATE NOT NULL,
    EstadoCivil VARCHAR(50) NULL,
    Nacionalidad VARCHAR(80) NULL,
    Provincia VARCHAR(100) NULL,
    CodigoPostal VARCHAR(20) NULL,
    Barrio VARCHAR(100) NULL,
    TelefonoAlternativo VARCHAR(30) NULL,
    RedSocialInstagram VARCHAR(120) NULL,
    ProfesionOcupacion VARCHAR(120) NULL,
    EmpresaLugarTrabajo VARCHAR(150) NULL,
    NivelEstudios VARCHAR(100) NULL,
    Estado VARCHAR(10) NOT NULL DEFAULT 'ACTIVO',
    MetodoPagoPreferido VARCHAR(80) NULL,
    Observaciones TEXT NULL
);
