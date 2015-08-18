USE eSalon;

DROP TABLE IF EXISTS `Employee`;

CREATE TABLE `Employee` (
	Employee_ID INT NOT NULL AUTO_INCREMENT,
    `eName` VARCHAR(50),
    `eSurname` VARCHAR(50),
    `eContactNumber` VARCHAR(15),
    `eemail`VARCHAR(50),
    `eSalary` DECIMAL(8,2),
    `eActive` BOOLEAN,
    `eAddress_ID` INT,
    
    PRIMARY KEY(Employee_ID),
    
    FOREIGN KEY(`eAddress_ID`)
          REFERENCES `Address`(`aAddress_id`)
    
);

INSERT INTO `Employee`(eName,eSurname,eContactNumber,eemail,eSalary,eActive, eAddress_ID) VALUES("Susan","Kruger","0728429882","susan_roux21@yahoo.com","5000",True, 1);
INSERT INTO `Employee`(eName,eSurname,eContactNumber,eemail,eSalary,eActive, eAddress_ID) VALUES("Cerese","Bouwer","0824569858","cerese@yahoo.com","5000",True, 1);
INSERT INTO `Employee`(eName,eSurname,eContactNumber,eemail,eSalary,eActive, eAddress_ID) VALUES("Grieta","Goosen","0824788787","grieta@yahoo.com","5000",True, 1);

CREATE TABLE `Employee_Leave` (
	EmployeeLeave_ID INT NOT NULL AUTO_INCREMENT,
    `StartDate` DATETIME,
    `StartTime` DATETIME,
    `EndDate` DATETIME,
    `EndTime`DATETIME,
    `Employee_ID` INT,
    
    PRIMARY KEY(EmployeeLeave_ID),
    
    FOREIGN KEY(`Employee_ID`)
          REFERENCES `Employee`(`Employee_ID`)
);

CREATE TABLE `Role` (
	Role_ID INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50),
        
    PRIMARY KEY(Role_ID)
);

INSERT INTO `Role` VALUES (1, "Admin");
INSERT INTO `Role` VALUES (2, "Stylist");


CREATE TABLE `User` (
	User_ID INT NOT NULL AUTO_INCREMENT,
    `Username` VARCHAR(50),
    `Password` VARCHAR(50),
    `Employee_ID` INT,
    `Role_ID` INT,
    `Active` BOOLEAN,
    
    PRIMARY KEY(User_ID),
    
    FOREIGN KEY(`Employee_ID`)    
		REFERENCES `Employee`(`Employee_ID`),
          
	FOREIGN KEY(`Role_ID`)  
		REFERENCES `Role`(`Role_ID`)
);

INSERT INTO `User` VALUES (1, "Admin", "Admin", 1, 1, TRUE);
INSERT INTO `User` VALUES (2, "Dirk", "Dirk", 2, 2, TRUE);
INSERT INTO `User` VALUES (3, "Johan", "Johan", 3, 2, TRUE);

CREATE TABLE `Audit` (
	Audit_ID INT NOT NULL AUTO_INCREMENT,
    `DateTime` DateTime,
    `Action` VarChar(300),
    `User_ID` INT,
   
    PRIMARY KEY(Audit_ID),
    
    FOREIGN KEY(`User_ID`)
          REFERENCES `User`(`User_ID`)
);

CREATE TABLE `Minor` (
	Minor_ID INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50),
	PRIMARY KEY(Minor_ID)
   
);

CREATE TABLE `Major` (
	Major_ID INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50),
    PRIMARY KEY(Major_ID)
    
);

CREATE TABLE `Permission` (
	Permission_ID INT NOT NULL AUTO_INCREMENT,
    `Major_ID` INT,
    `Minor_ID` INT,
    
    PRIMARY KEY(Permission_ID),
    
    FOREIGN KEY(`Major_ID`)
          REFERENCES `Major`(`Major_id`),
          
	FOREIGN KEY(`Minor_ID`)
          REFERENCES `Minor`(`Minor_id`)
    
);

CREATE TABLE `Role_Permission` (
	Role_ID INT NOT NULL,
    Permission_ID INT NOT NULL,
    
    FOREIGN KEY(`Role_ID`)
          REFERENCES `Role`(`Role_id`),
	
    FOREIGN KEY(`Permission_ID`)
          REFERENCES `Permission`(`Permission_id`)
    
);

CREATE TABLE `Invoice` (
	Invoice_ID INT NOT NULL AUTO_INCREMENT,
    `DateTime` DateTime,
    `PaymentMethod` Decimal(8,2),
    `isPercentage` INT,
    `Total` Decimal(8,2),
    `PaymentMethod_ID` INT,
    `Client_ID` INT,
    `Employee_ID` INT,
    
    PRIMARY KEY(Invoice_ID),
    
    FOREIGN KEY(`PaymentMethod_ID`)
          REFERENCES `Payment_Method`(`pPayment_Method_id`),
          
	FOREIGN KEY(`Client_ID`)
          REFERENCES `Client`(`cClient_id`),
          
	FOREIGN KEY(`Employee_ID`)
          REFERENCES `Employee`(`Employee_id`)
);

CREATE TABLE `Voucher` (
	Voucher_ID INT NOT NULL AUTO_INCREMENT,
    `Amount` Decimal(8,2),
    `Barcode` VARCHAR(50),
    
    PRIMARY KEY(Voucher_ID)
    
);

CREATE TABLE `Voucher_Bought` (
	Voucher_ID INT NOT NULL,
    Invoice_ID INT NOT NULL,
    
    FOREIGN KEY(`Voucher_ID`)
          REFERENCES `Voucher`(`Voucher_ID`),
	
    FOREIGN KEY(`Invoice_ID`)
          REFERENCES `Invoice`(`Invoice_ID`)
    
);

CREATE TABLE `Voucher_Redeemed` (
	Voucher_ID INT NOT NULL,
    Invoice_ID INT NOT NULL,
    
    FOREIGN KEY(`Voucher_ID`)
          REFERENCES `Voucher`(`Voucher_ID`),
	
    FOREIGN KEY(`Invoice_ID`)
          REFERENCES `Invoice`(`Invoice_ID`)
    
);

CREATE TABLE `Special` (
	Special_ID INT NOT NULL AUTO_INCREMENT,
    `DateFrom` Date,
    `DateTo` Date,
    `Message` VARCHAR(200),
    
    PRIMARY KEY(Special_ID)
    
);