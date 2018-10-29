-- MySQL Workbench Synchronization
-- Generated: 2018-10-29 15:22
-- Model: New Model
-- Version: 1.0
-- Project: Name of the project
-- Author: Xer0

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

ALTER SCHEMA `wetr`  DEFAULT CHARACTER SET utf8  DEFAULT COLLATE utf8_general_ci ;

CREATE TABLE IF NOT EXISTS `wetr`.`province` (
  `provinceId` INT(11) NOT NULL,
  `name` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`provinceId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `wetr`.`community` (
  `communityId` INT(11) NOT NULL,
  `name` VARCHAR(64) NOT NULL,
  `districtId` INT(11) NOT NULL,
  `provinceId` INT(11) NOT NULL,
  PRIMARY KEY (`communityId`, `districtId`, `provinceId`),
  INDEX `fk_community_district1_idx` (`districtId` ASC, `provinceId` ASC),
  CONSTRAINT `fk_community_district1`
    FOREIGN KEY (`districtId` , `provinceId`)
    REFERENCES `wetr`.`district` (`districtId` , `provinceId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `wetr`.`district` (
  `districtId` INT(11) NOT NULL,
  `name` VARCHAR(64) NULL DEFAULT NULL,
  `provinceId` INT(11) NOT NULL,
  PRIMARY KEY (`districtId`, `provinceId`),
  INDEX `fk_district_province_idx` (`provinceId` ASC),
  CONSTRAINT `fk_district_province`
    FOREIGN KEY (`provinceId`)
    REFERENCES `wetr`.`province` (`provinceId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `wetr`.`address` (
  `addressId` INT(11) NOT NULL AUTO_INCREMENT,
  `street` VARCHAR(64) NOT NULL,
  `house` VARCHAR(64) NOT NULL,
  `zip` VARCHAR(64) NOT NULL,
  `communityId` INT(11) NOT NULL,
  PRIMARY KEY (`addressId`, `communityId`),
  INDEX `fk_address_community1_idx` (`communityId` ASC),
  CONSTRAINT `fk_address_community1`
    FOREIGN KEY (`communityId`)
    REFERENCES `wetr`.`community` (`communityId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `wetr`.`user` (
  `userId` INT(11) NOT NULL AUTO_INCREMENT,
  `firstName` VARCHAR(64) NOT NULL,
  `lastName` VARCHAR(64) NOT NULL,
  `password` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`userId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `wetr`.`station` (
  `stationId` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  `longitude` FLOAT(11) NOT NULL,
  `latitude` FLOAT(11) NOT NULL,
  `stationTypeId` INT(11) NOT NULL,
  `address_addressId` INT(11) NOT NULL,
  PRIMARY KEY (`stationId`),
  INDEX `fk_station_stationType1_idx` (`stationTypeId` ASC),
  INDEX `fk_station_address1_idx` (`address_addressId` ASC),
  CONSTRAINT `fk_station_stationType1`
    FOREIGN KEY (`stationTypeId`)
    REFERENCES `wetr`.`stationType` (`stationTypeId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_station_address1`
    FOREIGN KEY (`address_addressId`)
    REFERENCES `wetr`.`address` (`addressId`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `wetr`.`stationType` (
  `stationTypeId` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`stationTypeId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `wetr`.`hasAccess` (
  `userId` INT(11) NOT NULL,
  `stationId` INT(11) NOT NULL,
  PRIMARY KEY (`userId`, `stationId`),
  INDEX `fk_user_has_station_station1_idx` (`stationId` ASC),
  INDEX `fk_user_has_station_user1_idx` (`userId` ASC),
  CONSTRAINT `fk_user_has_station_user1`
    FOREIGN KEY (`userId`)
    REFERENCES `wetr`.`user` (`userId`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_user_has_station_station1`
    FOREIGN KEY (`stationId`)
    REFERENCES `wetr`.`station` (`stationId`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `wetr`.`unit` (
  `unitId` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`unitId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `wetr`.`measurementType` (
  `measurementTypeId` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`measurementTypeId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `wetr`.`measurement` (
  `measurementId` INT(11) NOT NULL AUTO_INCREMENT,
  `value` FLOAT(11) NOT NULL,
  `timestamp` TIMESTAMP NOT NULL,
  `stationId` INT(11) NOT NULL,
  `unitId` INT(11) NOT NULL,
  `measurementTypeId` INT(11) NOT NULL,
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
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
