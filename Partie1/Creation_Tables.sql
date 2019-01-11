USE `ESCAPADE`; 

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Table `Client`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Client` (
  `NumeroClient` INT NOT NULL,
  `Nom` VARCHAR(45) NULL,
  `Prenom` VARCHAR(45) NULL,
  `Adresse` VARCHAR(45) NULL,
  `NumeroTel` INT NULL,
  `email` VARCHAR(30) NULL,
  PRIMARY KEY (`NumeroClient`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Séjour`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Séjour` (
  `idSejour` VARCHAR(45) NOT NULL,
  `Theme` VARCHAR(3) NULL,
  `Date` DATETIME NULL,
  `Hebergement (par API)` VARCHAR(45) NULL,
  PRIMARY KEY (`idSejour`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Voiture`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Voiture` (
  `Immatriculation` VARCHAR(10) NOT NULL,
  `Marque` VARCHAR(15) NOT NULL,
  `Modele` VARCHAR(15) NOT NULL,
  `Type` VARCHAR(20) NOT NULL,
  `Place` VARCHAR(2) NULL,
  `est_verif` TINYINT NULL,
  `est_dispo` TINYINT NULL,
  `est_rendu` TINYINT NULL,
  PRIMARY KEY (`Immatriculation`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Reserve`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Reserve` (
  `NumeroRésa` INT NOT NULL,
  `Client_NumeroClient` INT NOT NULL,
  `Séjour_idSejour` VARCHAR(45) NOT NULL,
  `Voiture_Immatriculation` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`NumeroRésa`),
  INDEX `fk_Reserve_Client1_idx` (`Client_NumeroClient` ASC),
  INDEX `fk_Reserve_Séjour1_idx` (`Séjour_idSejour` ASC),
  INDEX `fk_Reserve_Voiture1_idx` (`Voiture_Immatriculation` ASC),
  CONSTRAINT `fk_Reserve_Client1`
    FOREIGN KEY (`Client_NumeroClient`)
    REFERENCES `Client` (`NumeroClient`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Reserve_Séjour1`
    FOREIGN KEY (`Séjour_idSejour`)
    REFERENCES `Séjour` (`idSejour`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Reserve_Voiture1`
    FOREIGN KEY (`Voiture_Immatriculation`)
    REFERENCES `Voiture` (`Immatriculation`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Parking`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Parking` (
  `CodeParking` VARCHAR(3) NOT NULL,
  `Nom` VARCHAR(30) NOT NULL,
  `Adresse` VARCHAR(45) NOT NULL,
  `CodePostal` INT NOT NULL,
  `Ville` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`CodeParking`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Controleur`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Controleur` (
  `idControleur` INT NOT NULL,
  `Nom` VARCHAR(20) NULL,
  `Prenom` VARCHAR(20) NULL,
  PRIMARY KEY (`idControleur`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Entretien`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Entretien` (
  `idEntretien` INT NOT NULL,
  `Motif` VARCHAR(20) NULL,
  `Date` DATETIME NULL,
  `Voiture_Immatriculation` VARCHAR(10) NOT NULL,
  `Controleur_idControleur` INT NOT NULL,
  INDEX `fk_Entretien_Voiture1_idx` (`Voiture_Immatriculation` ASC),
  INDEX `fk_Entretien_Controleur1_idx` (`Controleur_idControleur` ASC),
  PRIMARY KEY (`idEntretien`),
  CONSTRAINT `fk_Entretien_Voiture1`
    FOREIGN KEY (`Voiture_Immatriculation`)
    REFERENCES `Voiture` (`Immatriculation`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Entretien_Controleur1`
    FOREIGN KEY (`Controleur_idControleur`)
    REFERENCES `Controleur` (`idControleur`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Stationne`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Stationne` (
  `Voiture_Immatriculation` VARCHAR(10) NOT NULL,
  `Parking_CodeParking` VARCHAR(3) NOT NULL,
  INDEX `fk_Stationne_Parking1_idx` (`Parking_CodeParking` ASC),
  CONSTRAINT `fk_Stationne_Voiture1`
    FOREIGN KEY (`Voiture_Immatriculation`)
    REFERENCES `Voiture` (`Immatriculation`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Stationne_Parking1`
    FOREIGN KEY (`Parking_CodeParking`)
    REFERENCES `Parking` (`CodeParking`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;
