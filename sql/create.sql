SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS `province`;
DROP TABLE IF EXISTS `district`;
DROP TABLE IF EXISTS `community`;
DROP TABLE IF EXISTS `address`;
DROP TABLE IF EXISTS `station`;
DROP TABLE IF EXISTS `stationtype`;
DROP TABLE IF EXISTS `user`;
DROP TABLE IF EXISTS `measurement`;
DROP TABLE IF EXISTS `measurementtype`;
DROP TABLE IF EXISTS `unit`;
DROP TABLE IF EXISTS `hasAccess`;
SET FOREIGN_KEY_CHECKS = 1;

CREATE TABLE `province` (
    `provinceId` INTEGER NOT NULL,
    `name` VARCHAR(60) NOT NULL,
    PRIMARY KEY (`provinceId`)
);

CREATE TABLE `district` (
    `districtId` INTEGER NOT NULL,
    `provinceId` INTEGER NOT NULL,
    `name` VARCHAR(60) NOT NULL,
    PRIMARY KEY (`districtId`, `provinceId`)
);

CREATE TABLE `community` (
    `communityId` INTEGER NOT NULL,
    `provinceId` INTEGER NOT NULL,
    `districtId` INTEGER NOT NULL,
    `name` VARCHAR(60) NOT NULL,
    PRIMARY KEY (`communityId`, `provinceId`, `districtId`)
);

CREATE TABLE `address` (
    `addressId` INTEGER NOT NULL,
    `provinceId` INTEGER NOT NULL,
    `districtId` INTEGER NOT NULL,
    `communityId` INTEGER NOT NULL,
    `street` VARCHAR(60) NOT NULL,
    `house` VARCHAR(10) NOT NULL,
    `zip` VARCHAR(10) NOT NULL,
    PRIMARY KEY (`addressId`, `provinceId`, `districtId`, `communityId`)
);

CREATE TABLE `station` (
    `stationId` INTEGER NOT NULL,
    `addressId` INTEGER NOT NULL,
    `stationTypeId` INTEGER NOT NULL,
    `name` VARCHAR(60) NOT NULL,
    `longitude` FLOAT NOT NULL,
    `latitude` FLOAT NOT NULL,
    PRIMARY KEY (`stationId`)
);

CREATE TABLE `stationtype` (
    `stationTypeId` INTEGER NOT NULL,
    `name` VARCHAR(60) NOT NULL,
    PRIMARY KEY (`stationTypeId`)
);

CREATE TABLE `user` (
    `userId` INTEGER NOT NULL,
    `firstName` VARCHAR(60) NOT NULL,
    `lastName` VARCHAR(60) NOT NULL,
    `password` VARCHAR(60) NOT NULL,
    PRIMARY KEY (`userId`)
);

CREATE TABLE `measurement` (
    `measurementId` INTEGER NOT NULL,
    `stationId` INTEGER NOT NULL,
    `unitId` INTEGER NOT NULL,
    `measurementTypeId` INTEGER NOT NULL,
    `value` FLOAT NOT NULL,
    `timestamp` TIMESTAMP NOT NULL,
    PRIMARY KEY (`measurementId`)
);

CREATE TABLE `measurementtype` (
    `measurementTypeId` INTEGER NOT NULL,
    `name` INTEGER NOT NULL,
    PRIMARY KEY (`measurementTypeId`)
);

CREATE TABLE `unit` (
    `unitId` INTEGER NOT NULL,
    `name` INTEGER NOT NULL,
    PRIMARY KEY (`unitId`)
);

CREATE TABLE `hasAccess` (
    `stationId` INTEGER NOT NULL,
    `userId` INTEGER NOT NULL,
    PRIMARY KEY (`stationId`, `userId`)
);

ALTER TABLE `district` ADD FOREIGN KEY (`provinceId`) REFERENCES `province`(`provinceId`);
ALTER TABLE `community` ADD FOREIGN KEY (`provinceId`) REFERENCES `province`(`provinceId`);
ALTER TABLE `community` ADD FOREIGN KEY (`districtId`) REFERENCES `district`(`districtId`);
ALTER TABLE `address` ADD FOREIGN KEY (`provinceId`) REFERENCES `province`(`provinceId`);
ALTER TABLE `address` ADD FOREIGN KEY (`districtId`) REFERENCES `district`(`districtId`);
ALTER TABLE `address` ADD FOREIGN KEY (`communityId`) REFERENCES `community`(`communityId`);
ALTER TABLE `station` ADD FOREIGN KEY (`stationTypeId`) REFERENCES `stationtype`(`stationTypeId`);
ALTER TABLE `station` ADD FOREIGN KEY (`addressId`) REFERENCES `address`(`addressId`);
ALTER TABLE `measurement` ADD FOREIGN KEY (`stationId`) REFERENCES `station`(`stationId`);
ALTER TABLE `measurement` ADD FOREIGN KEY (`measurementTypeId`) REFERENCES `measurementtype`(`measurementTypeId`);
ALTER TABLE `measurement` ADD FOREIGN KEY (`unitId`) REFERENCES `unit`(`unitId`);
ALTER TABLE `hasAccess` ADD FOREIGN KEY (`stationId`) REFERENCES `station`(`stationId`);
ALTER TABLE `hasAccess` ADD FOREIGN KEY (`userId`) REFERENCES `user`(`userId`);