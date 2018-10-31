
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema wetr
-- -----------------------------------------------------

DROP TABLE IF EXISTS `wetr`.`hasPermission` ;
DROP TABLE IF EXISTS `wetr`.`permission` ;
DROP TABLE IF EXISTS `wetr`.`hasAccess` ;
DROP TABLE IF EXISTS `wetr`.`measurement` ;
DROP TABLE IF EXISTS `wetr`.`unit` ;
DROP TABLE IF EXISTS `wetr`.`measurementType` ;
DROP TABLE IF EXISTS `wetr`.`station` ;
DROP TABLE IF EXISTS `wetr`.`stationType` ;
DROP TABLE IF EXISTS `wetr`.`user` ;
DROP TABLE IF EXISTS `wetr`.`address` ;
DROP TABLE IF EXISTS `wetr`.`community` ;
DROP TABLE IF EXISTS `wetr`.`district` ;
DROP TABLE IF EXISTS `wetr`.`province` ;
DROP TABLE IF EXISTS `wetr`.`country` ;

DROP SCHEMA IF EXISTS `wetr` ;