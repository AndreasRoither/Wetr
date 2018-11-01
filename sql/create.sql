-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema wetr
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema wetr
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `wetr` DEFAULT CHARACTER SET utf8 ;
USE `wetr` ;

-- -----------------------------------------------------
-- Table `wetr`.`country`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`country` (
  `countryId` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`countryId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`province`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`province` (
  `provinceId` INT NOT NULL,
  `name` VARCHAR(64) NOT NULL,
  `countryId` INT NOT NULL,
  PRIMARY KEY (`provinceId`),
  INDEX `fk_province_country1_idx` (`countryId` ASC),
  CONSTRAINT `fk_province_country1`
    FOREIGN KEY (`countryId`)
    REFERENCES `wetr`.`country` (`countryId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`district`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`district` (
  `districtId` INT NOT NULL,
  `name` VARCHAR(64) NOT NULL,
  `provinceId` INT NOT NULL,
  PRIMARY KEY (`districtId`),
  INDEX `fk_district_province1_idx` (`provinceId` ASC),
  CONSTRAINT `fk_district_province1`
    FOREIGN KEY (`provinceId`)
    REFERENCES `wetr`.`province` (`provinceId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`community`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`community` (
  `communityId` INT NOT NULL,
  `name` VARCHAR(64) NOT NULL,
  `districtId` INT NOT NULL,
  PRIMARY KEY (`communityId`),
  INDEX `fk_community_district1_idx` (`districtId` ASC),
  CONSTRAINT `fk_community_district1`
    FOREIGN KEY (`districtId`)
    REFERENCES `wetr`.`district` (`districtId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`address`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`address` (
  `addressId` INT NOT NULL AUTO_INCREMENT,
  `street` VARCHAR(512) NOT NULL,
  `house` VARCHAR(16) NOT NULL,
  `zip` VARCHAR(16) NOT NULL,
  `communityId` INT NOT NULL,
  PRIMARY KEY (`addressId`),
  INDEX `fk_address_community1_idx` (`communityId` ASC),
  CONSTRAINT `fk_address_community1`
    FOREIGN KEY (`communityId`)
    REFERENCES `wetr`.`community` (`communityId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`user` (
  `userId` INT NOT NULL AUTO_INCREMENT,
  `firstName` VARCHAR(64) NOT NULL,
  `lastName` VARCHAR(64) NOT NULL,
  `password` VARCHAR(64) NOT NULL,
  `email` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`userId`),
  UNIQUE INDEX `email_UNIQUE` (`email` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`stationType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`stationType` (
  `stationTypeId` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`stationTypeId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`station`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`station` (
  `stationId` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  `longitude` DECIMAL(11,8) NOT NULL,
  `latitude` DECIMAL(10,8) NOT NULL,
  `stationTypeId` INT NOT NULL,
  `addressId` INT NOT NULL,
  `userId` INT NOT NULL,
  PRIMARY KEY (`stationId`),
  INDEX `fk_station_stationType1_idx` (`stationTypeId` ASC),
  INDEX `fk_station_address1_idx` (`addressId` ASC),
  INDEX `fk_station_user1_idx` (`userId` ASC),
  CONSTRAINT `fk_station_stationType1`
    FOREIGN KEY (`stationTypeId`)
    REFERENCES `wetr`.`stationType` (`stationTypeId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_station_address1`
    FOREIGN KEY (`addressId`)
    REFERENCES `wetr`.`address` (`addressId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_station_user1`
    FOREIGN KEY (`userId`)
    REFERENCES `wetr`.`user` (`userId`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`unit`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`unit` (
  `unitId` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`unitId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`measurementType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`measurementType` (
  `measurementTypeId` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`measurementTypeId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`measurement`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`measurement` (
  `measurementId` INT NOT NULL AUTO_INCREMENT,
  `value` DOUBLE NOT NULL,
  `timestamp` TIMESTAMP NOT NULL,
  `stationId` INT NOT NULL,
  `unitId` INT NOT NULL,
  `measurementTypeId` INT NOT NULL,
  PRIMARY KEY (`measurementId`, `stationId`),
  INDEX `fk_measurement_station1_idx` (`stationId` ASC),
  INDEX `fk_measurement_unit1_idx` (`unitId` ASC),
  INDEX `fk_measurement_measurementType1_idx` (`measurementTypeId` ASC),
  CONSTRAINT `fk_measurement_station1`
    FOREIGN KEY (`stationId`)
    REFERENCES `wetr`.`station` (`stationId`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_measurement_unit1`
    FOREIGN KEY (`unitId`)
    REFERENCES `wetr`.`unit` (`unitId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_measurement_measurementType1`
    FOREIGN KEY (`measurementTypeId`)
    REFERENCES `wetr`.`measurementType` (`measurementTypeId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`permission`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`permission` (
  `permissionId` INT NOT NULL,
  `name` VARCHAR(64) NOT NULL,
  `description` TEXT NULL,
  PRIMARY KEY (`permissionId`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `wetr`.`hasPermission`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `wetr`.`hasPermission` (
  `permissionId` INT NOT NULL,
  `userId` INT NOT NULL,
  PRIMARY KEY (`permissionId`, `userId`),
  INDEX `fk_permission_has_user_user1_idx` (`userId` ASC),
  INDEX `fk_permission_has_user_permission1_idx` (`permissionId` ASC),
  CONSTRAINT `fk_permission_has_user_permission1`
    FOREIGN KEY (`permissionId`)
    REFERENCES `wetr`.`permission` (`permissionId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_permission_has_user_user1`
    FOREIGN KEY (`userId`)
    REFERENCES `wetr`.`user` (`userId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
