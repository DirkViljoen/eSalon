-- Deletes the database if it exists
DROP DATABASE IF EXISTS eSalon;

-- CREATING
CREATE DATABASE eSalon;

USE eSalon;

CREATE TABLE `Province`(
	`Province_id` INT NULL AUTO_INCREMENT,
	`Name` VARCHAR(50),

	PRIMARY KEY(`Province_id`)
);

CREATE TABLE `City`(
	`City_id` INT NULL AUTO_INCREMENT,
	`Name` VARCHAR(50),
	`Province_id` INT,

	PRIMARY KEY(`City_id`),

	FOREIGN KEY(`Province_id`)
		REFERENCES `Province`(`Province_id`)
);

CREATE TABLE `Surburb`(
	`Surburb_id` INT NULL AUTO_INCREMENT,
	`Name` VARCHAR(50),
	`City_id` INT,

	PRIMARY KEY(`Surburb_id`),

	FOREIGN KEY( `City_id`)
		REFERENCES `City`( `City_id`)
);

CREATE TABLE `Address`(
	`Address_id` INT NULL AUTO_INCREMENT,
	`Line1` VARCHAR(50),
	`Line2` VARCHAR(50),
	`Surburb_id` INT,

	PRIMARY KEY(`Address_id`),

	FOREIGN KEY(`Surburb_id`)
		REFERENCES `Surburb`(`Surburb_id`)
);

CREATE TABLE `Notification_Method`(
	`NoticationMethod_ID` INT NULL AUTO_INCREMENT,
	`NotificationType` VARCHAR(10),

	PRIMARY KEY( `NoticationMethod_ID`)
);

CREATE TABLE `Client`(
	`Client_ID` INT NULL AUTO_INCREMENT,
	`Title` VARCHAR(5),
	`Name` VARCHAR(50),
	`Surname` VARCHAR(50),
	`ContactNumber` VARCHAR(15),
	`email` VARCHAR(100),
	`DateOfBirth` DATE,
	`Reminders` BOOLEAN,
	`Notifications` BOOLEAN,
	`Active` BOOLEAN,
	`NoticationMethod_ID` INT,
	`Address_ID` INT,

	PRIMARY KEY(`Client_ID` ),

	FOREIGN KEY(`NoticationMethod_ID` )
		REFERENCES `Notification_Method`(`NoticationMethod_ID` ),

	FOREIGN KEY(`Address_id`)
		REFERENCES `Address`(`Address_id`)
);

CREATE TABLE`Expense_Category`(
	`ExpenseCategory_ID` INT NULL AUTO_INCREMENT,
	`Name` VARCHAR(50),

	PRIMARY KEY(`ExpenseCategory_ID`)
);

CREATE TABLE `Payment_Method`(
		`PaymentMethod_ID` INT NULL AUTO_INCREMENT,
		`Name` VARCHAR(50),

		PRIMARY KEY(`PaymentMethod_ID`)
);

CREATE TABLE `Expense`(
	`Expense_ID` INT NULL AUTO_INCREMENT,
	`Name` VARCHAR(50),
	`Quantity` INT,
    `Date` DATE,
	`PricePerItem` DECIMAL(8,2),
	`ExpenseCategory_ID` INT,
	`PaymentMethod_ID` INT,

	PRIMARY KEY(`Expense_ID`),

	FOREIGN KEY(`ExpenseCategory_ID`)
		REFERENCES `Expense_Category`(`ExpenseCategory_ID`),


	FOREIGN KEY(`PaymentMethod_ID`)
		REFERENCES `Payment_Method`(`PaymentMethod_ID`)
);

CREATE TABLE `Sub_Letter`(
	`Sub_Letter_id` INT NULL AUTO_INCREMENT,
	`BusinessName` VARCHAR(50),
	`ContactFName` VARCHAR(50),
	`ContactLName` VARCHAR(50),
	`ContactNumber` VARCHAR(15),
	`ContactEmail`  VARCHAR(100),
	`DateTime` DATE,
	`Amount` VARCHAR(50),
	`Active` BOOLEAN,

	PRIMARY KEY(`Sub_Letter_id`)
);

CREATE TABLE `Sub_Letter_Payment`(
	`Sub_LetterPayment_id` INT NULL AUTO_INCREMENT, 
	`DateTime` DATETIME,
	`Amount` DECIMAL(8,2),
	`Sub_Letter_id` INT,
	`PaymentMethod_ID` INT,

	PRIMARY KEY (`Sub_LetterPayment_id`),

	FOREIGN KEY(  `Sub_Letter_id`)
		REFERENCES `Sub_Letter`(  `Sub_Letter_id`),

	FOREIGN KEY(`PaymentMethod_ID`)
		REFERENCES `Payment_Method`(`PaymentMethod_ID`)
);

CREATE TABLE `Company_Details`(
	`CompanyDetails_ID` INT NULL AUTO_INCREMENT,
	`Name` VARCHAR(50),
	`ContactDetails` VARCHAR(15),
	`email` VARCHAR(100),

	PRIMARY KEY(`CompanyDetails_ID`)
);

CREATE TABLE `Employee` (
	`Employee_ID` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50),
    `Surname` VARCHAR(50),
    `ContactNumber` VARCHAR(15),
    `email`VARCHAR(100),
    `Salary` DECIMAL(8,2),
    `Active` BOOLEAN,
    `Image` VARCHAR(200),
    `Address_ID` INT,
    
    PRIMARY KEY(`Employee_ID`),
    
    FOREIGN KEY(`Address_ID`)
          REFERENCES `Address`(`Address_id`)
);

CREATE TABLE `Employee_Leave` (
	`EmployeeLeave_ID` INT NOT NULL AUTO_INCREMENT,
    `StartDate` DATETIME,
    `StartTime` DATETIME,
    `EndDate` DATETIME,
    `EndTime`DATETIME,
    `Employee_ID` INT,
    
    PRIMARY KEY(`EmployeeLeave_ID`),
    
    FOREIGN KEY(`Employee_ID`)
          REFERENCES `Employee`(`Employee_ID`)
);

CREATE TABLE `Role` (
	`Role_ID` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50),
        
    PRIMARY KEY(`Role_ID`)
);

CREATE TABLE `User` (
	`User_ID` INT NOT NULL AUTO_INCREMENT,
    `Username` VARCHAR(30),
    `Password` VARCHAR(100),
    `Employee_ID` INT,
    `Role_ID` INT,
    `Active` BOOLEAN,
    
    PRIMARY KEY(`User_ID`),
    
    FOREIGN KEY(`Employee_ID`)    
		REFERENCES `Employee`(`Employee_ID`),
          
	FOREIGN KEY(`Role_ID`)  
		REFERENCES `Role`(`Role_ID`)
);

CREATE TABLE `Audit` (
	`Audit_ID` INT NOT NULL AUTO_INCREMENT,
    `DateTime` DateTime,
    `Action` VARCHAR(10),
    `Description` VarChar(300),
    `User_ID` INT,
   
    PRIMARY KEY(`Audit_ID`),
    
    FOREIGN KEY(`User_ID`)
          REFERENCES `User`(`User_ID`)
);

CREATE TABLE `Minor` (
	`Minor_ID` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50),
    
	PRIMARY KEY(`Minor_ID`)
   
);

CREATE TABLE `Major` (
	`Major_ID` INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50),
    PRIMARY KEY(`Major_ID`)
    
);

CREATE TABLE `Permission` (
	`Permission_ID` INT NOT NULL AUTO_INCREMENT,
    `Major_ID` INT,
    `Minor_ID` INT,
    
    PRIMARY KEY(`Permission_ID`),
    
    FOREIGN KEY(`Major_ID`)
          REFERENCES `Major`(`Major_id`),
          
	FOREIGN KEY(`Minor_ID`)
          REFERENCES `Minor`(`Minor_id`)
    
);

CREATE TABLE `Role_Permission` (
	`Role_ID` INT NOT NULL,
    `Permission_ID` INT NOT NULL,
    
    FOREIGN KEY(`Role_ID`)
          REFERENCES `Role`(`Role_id`),
	
    FOREIGN KEY(`Permission_ID`)
          REFERENCES `Permission`(`Permission_id`)
    
);

CREATE TABLE `Invoice` (
	`Invoice_ID` INT NOT NULL AUTO_INCREMENT,
    `DateTime` DateTime,
    `Discount` Decimal(8,2),
    `isPercentage` BOOLEAN,
    `Total` Decimal(8,2),
    `PaymentMethod_ID` INT,
    `Client_ID` INT,
    `Employee_ID` INT,
    
    PRIMARY KEY(`Invoice_ID`),
    
    FOREIGN KEY(`PaymentMethod_ID`)
          REFERENCES `Payment_Method`(`PaymentMethod_id`),
          
	FOREIGN KEY(`Client_ID`)
          REFERENCES `Client`(`Client_id`),
          
	FOREIGN KEY(`Employee_ID`)
          REFERENCES `Employee`(`Employee_id`)
);

CREATE TABLE `Voucher` (
	`Voucher_ID` INT NOT NULL AUTO_INCREMENT,
    `Amount` Decimal(8,2),
    `Barcode` VARCHAR(50),
    
    PRIMARY KEY(`Voucher_ID`)
    
);

CREATE TABLE `Voucher_Bought` (
	`Voucher_ID` INT NOT NULL,
    `Invoice_ID` INT NOT NULL,
    
    FOREIGN KEY(`Voucher_ID`)
          REFERENCES `Voucher`(`Voucher_ID`),
	
    FOREIGN KEY(`Invoice_ID`)
          REFERENCES `Invoice`(`Invoice_ID`)
    
);

CREATE TABLE `Voucher_Redeemed` (
	`Voucher_ID` INT NOT NULL,
    `Invoice_ID` INT NOT NULL,
    
    FOREIGN KEY(`Voucher_ID`)
          REFERENCES `Voucher`(`Voucher_ID`),
	
    FOREIGN KEY(`Invoice_ID`)
          REFERENCES `Invoice`(`Invoice_ID`)
    
);

CREATE TABLE `Special` (
	`Special_ID` INT NOT NULL AUTO_INCREMENT,
    `DateFrom` Date,
    `DateTo` Date,
    `Message` VARCHAR(200),
    
    PRIMARY KEY(`Special_ID`)
    
);

CREATE TABLE `Supplier` (
	`Supplier_ID` INT NULL AUTO_iNCREMENT,
    `Name` VARCHAR (50),
    `ContactNumber` VARCHAR(15),
    `Email` VARCHAR (100),
    `Active` BOOLEAN,
    
    PRIMARY KEY (`Supplier_ID`)
);

CREATE TABLE `Order` (
	`Order_id` INT NULL AUTO_INCREMENT,
    `DatePlaced` DATE,
    `DateReceived` DATE,
    `Supplier_ID` INT,
    
    PRIMARY KEY (`Order_id`),
    
    FOREIGN KEY (`Supplier_ID`)
		REFERENCES `Supplier` (`Supplier_ID`)
);

CREATE TABLE `Hair_Length` (
	`HairLength_id` INT NULL AUTO_INCREMENT,
    `Description` VARCHAR(50),
    
     PRIMARY KEY (`HairLength_id`)
);

CREATE TABLE `Category` (
	`Category_id` INT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50),
    
     PRIMARY KEY (`Category_id`)
);

CREATE TABLE `Stock` (
    `Stock_id` INT NULL AUTO_INCREMENT,
    `BrandName` VARCHAR(50),
    `ProductName` VARCHAR(50),
    `Price` DECIMAL (8,2),
    `Size` INT,
    `Active` BOOLEAN,
    `Quantity` INT,
    `Barcode` VARCHAR (30),
    `Category_ID` INT,
    `Supplier_ID` INT,
    
    
    PRIMARY KEY (`Stock_id`),
    
	FOREIGN KEY (`Category_ID`) REFERENCES `Category` (`Category_id`),
    FOREIGN KEY (`Supplier_ID`) REFERENCES `Supplier` (`Supplier_ID`)
);

CREATE TABLE `Stock_History` (
	`StockHistory_id` INT NULL AUTO_INCREMENT,
    `Price` DECIMAL (8,2),
    `PriceDateFrom` DATE,
    `PriceDateTo` DATE,
    `Stock_ID` INT,
    
	PRIMARY KEY (`StockHistory_id`),
    
	FOREIGN KEY (`Stock_id`) REFERENCES `Stock` (`Stock_id`)
);

CREATE TABLE `Order_Line` (
	`OrderLine_id` INT NULL AUTO_INCREMENT,
	`Quantity` INT,
    `Stock_ID` INT,
    `Order_ID` INT,
    
    PRIMARY KEY (`OrderLine_id`),
    
    FOREIGN KEY (`Stock_id`) REFERENCES `Stock` (`Stock_id`),
    
    FOREIGN KEY (`Order_id`) REFERENCES `Order` (`Order_id`)
);

CREATE TABLE `Invoice_Stock_Line` (
	`InvoiceStockLine_id` INT NULL AUTO_INCREMENT,
    `Price` DECIMAL (8,2),
    `Quantity` INT,
    `StockHistory_id` INT,
    `Special_id` INT,
    `Invoice_id` INT,
    
    PRIMARY KEY (`InvoiceStockLine_id`),
    
    FOREIGN KEY (`Special_id`) REFERENCES `Special` (`Special_id`),
    
    FOREIGN KEY (`StockHistory_id`) REFERENCES `Stock_History` (`StockHistory_id`),
    
    FOREIGN KEY (`Invoice_id`) REFERENCES `Invoice` (`Invoice_id`)
);

CREATE TABLE `Special_Stock_Condition_Line` (
	`SStCL_id` INT NULL AUTO_INCREMENT,
    `scQuantity` INT,
    `Special_id` INT,
    `Stock_id` INT,
    
    
    PRIMARY KEY (`SStCL_id`),
    
    FOREIGN KEY (`Special_id`) REFERENCES `Special` (`Special_id`),
    
    FOREIGN KEY (`Stock_id`) REFERENCES `Stock` (`Stock_id`)
);

CREATE TABLE `Special_Stock_Result_Line` (
	`SStRL_id` INT NULL AUTO_INCREMENT,
    `DiscountAmount` DECIMAL (8,2),
    `IsPercentage` BOOLEAN,
    `Special_id` INT,
    `Stock_id` INT,
    
    PRIMARY KEY (`SStRL_id`),
    
    FOREIGN KEY (`Special_id`) REFERENCES `Special` (`Special_id`),
    
    FOREIGN KEY (`Stock_id`) REFERENCES `Stock` (`Stock_id`)
);

CREATE TABLE `Service` (
	`Service_id` INT NULL AUTO_INCREMENT,
    `Name` VARCHAR (50),
    `AdditionalInformation` VARCHAR(200),
    `Price` DECIMAL (8,2),
    `Active` BOOLEAN,
    
    PRIMARY KEY (`Service_id`)
);

CREATE TABLE `Special_Service_Condition_Line` (
	`SSCL_id` INT NULL AUTO_INCREMENT,
    `Quantity` INT,
    `Special_id` INT,
    `Service_id` INT,
    
    PRIMARY KEY (`SSCL_id`),
    
    FOREIGN KEY (`Special_id`) REFERENCES `Special` (`Special_id`),
    
    FOREIGN KEY (`Service_id`) REFERENCES `Service` (`Service_id`)
);

CREATE TABLE `Special_Service_Result_Line` (
	`SSRL_id` INT NULL AUTO_INCREMENT,
    `DiscountAmount` DECIMAL (8,2),
    `IsPercentage` BOOLEAN,
    `Special_id` INT,
    `Service_id` INT,
    
    PRIMARY KEY (`SSRL_id`),
    
    FOREIGN KEY (`Special_id`) REFERENCES `Special` (`Special_id`),
    
    FOREIGN KEY (`Service_id`) REFERENCES `Service` (`Service_id`)
);

CREATE TABLE `Hair_Length_Service` (
	`HairLengthService_id` INT NULL AUTO_INCREMENT,
    `Duration` INT,
    `HairLength_id` INT,
    `Service_id` INT,
    
	PRIMARY KEY (`HairLengthService_id`),
    
    FOREIGN KEY (`HairLength_id`) REFERENCES `Hair_Length` (`HairLength_id`),
    
    FOREIGN KEY (`Service_id`) REFERENCES `Service` (`Service_id`)
);

CREATE TABLE `Booking` (
	`Booking_id` INT NULL AUTO_INCREMENT,
    `DateTime` DATETIME,
    `Duration` INT,
    `Completed` BOOLEAN,
    `Active` BOOLEAN,
    `ReferenceNumber` VARCHAR (12),
    `Client_id` INT,
    `Employee_id` INT,
    `Invoice_id` INT,
    
    PRIMARY KEY (`Booking_id`),
    
    FOREIGN KEY (`Client_id`) REFERENCES `Client` (`Client_id`),
    
    FOREIGN KEY (`Employee_id`) REFERENCES `Employee` (`Employee_id`),
    
    FOREIGN KEY (`Invoice_id`) REFERENCES `Invoice` (`Invoice_id`)
);

CREATE TABLE `Booking_Service` (
	`Booking_id` INT,
    `HairLengthService_id` INT,
    
    FOREIGN KEY (`Booking_id`)
		REFERENCES `Booking` (`Booking_id`),
	
    FOREIGN KEY (`HairLengthService_id`)
		REFERENCES `Hair_Length_Service` (`HairLengthService_id`)
);

CREATE TABLE `Service_History` (
	`ServiceHistory_id` INT NULL AUTO_INCREMENT,
	`Price` DECIMAL (8,2),
    `PriceDateFrom` DATE,
    `PriceDateTo` DATE,
    `Service_id` INT,
    
	PRIMARY KEY (`ServiceHistory_id`),
    
    FOREIGN KEY (`Service_id`) REFERENCES `Service` (`Service_id`)
);

CREATE TABLE `Invoice_Service_Line` (
	`InvoiceServiceLine_id` INT NULL AUTO_INCREMENT,
    `Price` DECIMAL (8,2),
    `Quantity` INT,
    `ServiceHistory_id` INT,
    `Special_id` INT,
    `Invoice_id` INT,
    
    PRIMARY KEY (`InvoiceServiceLine_id`),
    
    FOREIGN KEY (`ServiceHistory_id`) REFERENCES `Service_History` (`ServiceHistory_id`),
    
    FOREIGN KEY (`Special_id`) REFERENCES `Special` (`Special_id`),
    
    FOREIGN KEY (`Invoice_id`) REFERENCES `Invoice` (`Invoice_id`)
);


-- Lookup tables
INSERT INTO `Province` VALUES(1,"Gauteng");
INSERT INTO `Province` VALUES(2,"Mpumalanga");
INSERT INTO `Province` VALUES(3,"Limpopo");

INSERT INTO `City` VALUES(1,"Pretoria",1);
INSERT INTO `City` VALUES(2,"Bombela",2);
INSERT INTO `City` VALUES(3,"Polokwane",3);

INSERT INTO `Surburb` VALUES(1,"Centurion",1);
INSERT INTO `Surburb` VALUES(2,"Hatfield",1);
INSERT INTO `Surburb` VALUES(3,"Riverside Park",1);
INSERT INTO `Surburb` VALUES(4,"Ivy Park",1);

INSERT INTO `Notification_Method` VALUES(1, "SMS");
INSERT INTO `Notification_Method` VALUES(2,"Email");

INSERT INTO `Payment_Method` VALUES (1,"Cash");
INSERT INTO `Payment_Method` VALUES (2,"EFT");
INSERT INTO `Payment_Method` VALUES (3,"Credit");
INSERT INTO `Payment_Method` VALUES (4,"Zapper");

INSERT INTO `Hair_Length` VALUES (1, "Long");
INSERT INTO `Hair_Length` VALUES (2, "Medium");  
INSERT INTO `Hair_Length` VALUES (3, "Short"); 

-- CRUD tables

INSERT INTO `Address` VALUES(1,"","268 West Avenue", 1);
INSERT INTO `Address` VALUES(2,"","553 Grosvenor", 2);
INSERT INTO `Address` VALUES(3,"Unit 15, Sliverlakes", "312 Maple street", 4);

INSERT INTO `Client` VALUES(1,"Ms","Nanda","Nakai","079 227 1769","nandanakai@hotmail.com","1997-02-14",1,1,True,2,3);
INSERT INTO `Client` VALUES(2,"Miss", "Refiloe","Chaka","082 852 4512","RefiloeChaka@gmail.com","1994-03-02",1,0,False,2,1);
INSERT INTO `Client` VALUES(3,"Mr","Johan","Roux","083 336 5913","JohanRoux@gmail.com","1995-08-05",1,1,True,2,2);

INSERT INTO `Sub_Letter` VALUES(1,"Cat's nails","Rose", "Muller","0825673546","RoseMuller@gmail.com","2015-07-01",2500,True);
INSERT INTO `Sub_Letter` VALUES(2,"A&S waxing", "Amelia","Strydom","0614587598","Amelia205@gmail.com","2015-02-01",2500,True);
INSERT INTO `Sub_Letter` VALUES(3,"BrandNew","Charlotte", "Brand","0735238967","Charlotte87@gmail.com","2014-01-01",2500,False); 

INSERT INTO `Company_Details`(`Name`,`ContactDetails`,`email`) VALUES("Salon Re-design","072 842 9882","susan_roux@yahoo.com");

INSERT INTO `Employee`(`Name`,`Surname`,`ContactNumber`,`email`,`Salary`,`Active`,`Address_ID`) VALUES("Susan","Kruger","0728429882","susan_roux21@yahoo.com","5000",True, 1);
INSERT INTO `Employee`(`Name`,`Surname`,`ContactNumber`,`email`,`Salary`,`Active`,`Address_ID`) VALUES("Cerese","Bouwer","0824569858","cerese@yahoo.com","5000",True, 1);
INSERT INTO `Employee`(`Name`,`Surname`,`ContactNumber`,`email`,`Salary`,`Active`,`Address_ID`) VALUES("Grieta","Goosen","0824788787","grieta@yahoo.com","5000",True, 1);

INSERT INTO `Employee_Leave` VALUES(1,"2015-10-01 08:00",null,"2015-10-10 17:00",null, 1);

INSERT INTO `Role` VALUES (1, "Admin");
INSERT INTO `Role` VALUES (2, "Stylist");

INSERT INTO `User` VALUES (1, "Admin", "00410064006d0069006e", 1, 1, TRUE);
INSERT INTO `User` VALUES (2, "Dirk", "004400690072006b", 2, 2, TRUE);
INSERT INTO `User` VALUES (3, "Johan", "004a006f00680061006e", 3, 2, TRUE);

INSERT INTO `Supplier` VALUES (1, "John", 08001235674, "maybelline@cosmetics.com", True);
INSERT INTO `Supplier` VALUES (2, "Lizzy" , 0834567653, "tresemme@webmail.co.za", True);
INSERT INTO `Supplier` VALUES (3, "Davis", 0125674323, "Wella@hair.com", False);

INSERT INTO `Category` (`Category_id`, `Name`) VALUES (1, "Colour");
INSERT INTO `Category` (`Category_id`, `Name`) VALUES (2, "Cut");  
INSERT INTO `Category` (`Category_id`, `Name`) VALUES (3, "Wash"); 

INSERT INTO `Stock` VALUES (1, "Maybelline", "Shampoo", 150.00 , 150, True, 100, "10581643", 1, 1);
INSERT INTO `Stock` VALUES (2, "Tresemme", "Conditioner", 275.00 , 150, True, 40, "90492853", 2, 1);
INSERT INTO `Stock` VALUES (3, "Wella", "Hair Dye", 100.00 , 100, False, 10, "6053422344", 3, 2);  
INSERT INTO `Stock` VALUES (4, "Maybelline", "Shampoo", 150.00 , 150, True, 100, "10581643", 1, 1);
INSERT INTO `Stock` VALUES (5, "Tresemme", "Conditioner", 275.00 , 150, True, 40, "90492853", 2, 1);
INSERT INTO `Stock` VALUES (6, "Wella", "Hair Dye", 100.00 , 100, False, 10, "6053422344", 3, 2);  

INSERT INTO `Stock_History` VALUES (1,100,"2014-01-01","2014-06-01",1);
INSERT INTO `Stock_History` VALUES (2,150,"2014-06-01",null,1);
INSERT INTO `Stock_History` VALUES (3,275,"2014-01-01",null,2);
INSERT INTO `Stock_History` VALUES (4,100,"2014-01-01",null,3);

INSERT INTO `Service` VALUES (1, "Hair Cut", "Normal hair cut for females", 160.00, True);
INSERT INTO `Service` VALUES (2, "Hair Cut", "Normal hair cut for males", 120.00, True);
INSERT INTO `Service` VALUES (3, "Full colour", "Full hair colour", 300.00, True);

INSERT INTO `Service_History` VALUES (1,150,null,null,1);
INSERT INTO `Service_History` VALUES (2,170,null,null,2);
INSERT INTO `Service_History` VALUES (3,200,null,null,3);
    
INSERT INTO `Hair_Length_Service` VALUES (1, 30, 1, 1);
INSERT INTO `Hair_Length_Service` VALUES (2, 45, 2, 1);
INSERT INTO `Hair_Length_Service` VALUES (3, 50, 3, 1); 
INSERT INTO `Hair_Length_Service` VALUES (4, 10, 1, 2);
INSERT INTO `Hair_Length_Service` VALUES (5, 15, 2, 2);
INSERT INTO `Hair_Length_Service` VALUES (6, 20, 3, 2); 
INSERT INTO `Hair_Length_Service` VALUES (7, 60, 1, 3);
INSERT INTO `Hair_Length_Service` VALUES (8, 70, 2, 3);
INSERT INTO `Hair_Length_Service` VALUES (9, 80, 3, 3); 

INSERT INTO `Invoice` VALUES (1,"2015-08-20",50,false,300,3,1,1);

INSERT INTO `Invoice_Service_Line` VALUES (1,160,1,1,null,1);

INSERT INTO `Invoice_Stock_Line` VALUES (1,35,4,1,null,1);

INSERT INTO `Booking` VALUES (1,"2015-07-22 08:00:00",30,true,1,"booking1",1,1,1);
INSERT INTO `Booking` VALUES (2,"2015-08-22 08:00:00",30,false,1,"booking2",1,1,null);
INSERT INTO `Booking` VALUES (3,"2015-09-04 08:30:00",60,false,1,"booking3",2,1,null);

INSERT INTO `Order` VALUES (1, "2015-01-01", "2012-01-01", 1);

INSERT INTO `Voucher` VALUES (1, 500, "1");

INSERT INTO `eSalon`.`Major` (`Major_ID`, `Name`) VALUES ('1', 'Clients');
INSERT INTO `eSalon`.`Major` (`Major_ID`, `Name`) VALUES ('2', 'Bookings');
INSERT INTO `eSalon`.`Major` (`Major_ID`, `Name`) VALUES ('3', 'Sales');
INSERT INTO `eSalon`.`Major` (`Major_ID`, `Name`) VALUES ('4', 'Services');
INSERT INTO `eSalon`.`Major` (`Major_ID`, `Name`) VALUES ('5', 'Suppliers');
INSERT INTO `eSalon`.`Major` (`Major_ID`, `Name`) VALUES ('6', 'Employess');
INSERT INTO `eSalon`.`Major` (`Major_ID`, `Name`) VALUES ('7', 'Sub-letters');
INSERT INTO `eSalon`.`Major` (`Major_ID`, `Name`) VALUES ('8', 'Administration');
INSERT INTO `eSalon`.`Major` (`Major_ID`, `Name`) VALUES ('9', 'Reports');

INSERT INTO `eSalon`.`Minor` (`Minor_ID`, `Name`) VALUES ('1', 'Add');
INSERT INTO `eSalon`.`Minor` (`Minor_ID`, `Name`) VALUES ('2', 'Update');
INSERT INTO `eSalon`.`Minor` (`Minor_ID`, `Name`) VALUES ('3', 'Delete');
INSERT INTO `eSalon`.`Minor` (`Minor_ID`, `Name`) VALUES ('4', 'View');
INSERT INTO `eSalon`.`Minor` (`Minor_ID`, `Name`) VALUES ('5', 'Full');

INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('1', '1', '1');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('2', '1', '2');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('3', '1', '3');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('4', '2', '1');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('5', '2', '2');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('6', '2', '3');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('7', '3', '1');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('8', '3', '2');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('9', '3', '3');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('10', '4', '1');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('11', '4', '2');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('12', '4', '3');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('13', '5', '1');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('14', '5', '2');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('15', '5', '3');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('16', '6', '1');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('17', '6', '2');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('18', '6', '3');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('19', '7', '1');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('20', '7', '2');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('21', '7', '3');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('22', '8', '5');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('23', '9', '5');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('24', '1', '4');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('25', '2', '4');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('26', '3', '4');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('27', '4', '4');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('28', '5', '4');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('29', '6', '4');
INSERT INTO `eSalon`.`Permission` (`Permission_ID`, `Major_ID`, `Minor_ID`) VALUES ('30', '7', '4');

INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '1');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '2');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '3');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '4');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '5');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '6');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '7');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '8');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '9');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '10');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '11');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '12');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '13');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '14');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '15');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '16');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '17');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '18');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '19');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '20');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '21');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '22');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '23');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '24');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '25');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '26');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '27');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '28');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '29');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('1', '30');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '1');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '2');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '3');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '4');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '5');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '6');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '7');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '8');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '9');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '10');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '11');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '12');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '13');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '14');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '15');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '24');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '25');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '26');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '27');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '28');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '29');
INSERT INTO `eSalon`.`Role_Permission` (`Role_ID`, `Permission_ID`) VALUES ('2', '30');

-- Employee
    
    DELIMITER //
    CREATE PROCEDURE spEmployee_Leave_ID
    (
        IN id INT
    )
    BEGIN
        SELECT 
			* 
        FROM 
			`Employee_Leave`
        WHERE 
			`Employee_ID` = id;
    END //
    DELIMITER ;
    
-- Hair Length Service

    DELIMITER //
    CREATE PROCEDURE spHairLengthService_Search
    (

    )
    BEGIN
        SELECT 
			hls.*
		FROM 
			`Hair_Length_Service` hls;
    END //
    DELIMITER ;
    
-- Lookups

    DELIMITER //
    CREATE PROCEDURE spPayment_Method_All
    ()
    BEGIN
        SELECT * FROM `Payment_Method`;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spProvinces_Read
    ()
    BEGIN
        SELECT * FROM `Province`;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spCities_Filtered
    (IN id INT)
    BEGIN
        SELECT *
        FROM `City`
        WHERE `Province_id` = id;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spSuburbs_Filtered
    (IN id INT)
    BEGIN
        SELECT *
        FROM `Surburb`
        WHERE `City_id` = id;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spNotificationMethods_Read
    ()
    BEGIN
        SELECT *
        FROM `Notification_Method`;
    END //
    DELIMITER ;
    
    DELIMITER //
    CREATE PROCEDURE spHairLength_Read
    ()
    BEGIN
        SELECT *
        FROM `Hair_Length`;
    END //
    DELIMITER ;

-- Sub-letters

    DELIMITER //
    CREATE PROCEDURE Sub_Letter_Search
    (IN bName VARCHAR(50))
    BEGIN
        SELECT * FROM `Sub_Letter`
        WHERE `BusinessName` LIKE bName
         AND `Active` = true;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE Sub_Letter_get
    (IN id INT)
    BEGIN
        SELECT * FROM `Sub_Letter`
        WHERE `Sub_Letter_id` = id
         AND `Active` = true
        ORDER BY `BusinessName`;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE Sub_Letter_All
    ()
    BEGIN
        SELECT *
        FROM `Sub_Letter`
        WHERE `Active` = true
        ORDER BY `BusinessName`;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE Sub_Letter_Add
    (
        IN sBName VARCHAR(50),
            IN sCFName VARCHAR(50),
            IN sCLName VARCHAR(50),
            IN sCNumber VARCHAR(15),
        IN sCEmail VARCHAR(100),
        IN sDateTime DATE,
            IN sAmount DECIMAL(8,2)

    )
    BEGIN
        INSERT
            INTO `Sub_Letter`(
                `BusinessName`,
                `ContactFName`,
                `ContactLName`,
                `ContactNumber`,
                `ContactEmail`,
                `DateTime`,
                `Amount`,
                `Active`)
            VALUES
                (sBName, sCFName, sCLName, sCNumber, sCEmail, sDateTime, sAmount, True);
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE Sub_Letter_Update
    (
        IN sID INT,
        IN sBName VARCHAR(50),
            IN sCFName VARCHAR(50),
            IN sCLName VARCHAR(50),
            IN sCNumber VARCHAR(15),
        IN sCEmail VARCHAR(100),
        IN sDateTime DATE,
            IN sAmount DECIMAL(8,2)

    )
    BEGIN
        UPDATE `Sub_Letter`
            SET
                `BusinessName` = sBName,
                `ContactFName` = sCFName,
                `ContactLName` = sCLName,
                `ContactNumber` = sCNumber,
                `ContactEmail` = sCEmail,
                `DateTime` = sDateTime,
                `Amount` = sAmount,
                `Active` = true
            WHERE
                `Sub_Letter_id` = sID;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE Sub_Letter_Delete
    (
        IN sID INT
    )
    BEGIN
        UPDATE `Sub_Letter`
            SET
                `Active` = false
            WHERE
                `Sub_Letter_id` = sID;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spSub_Letter_Payments_Read
    (
        IN id INT
    )
    BEGIN
        SELECT * FROM `Sub_Letter_Payment`
        WHERE `Sub_Letter_id` = id
        ORDER BY `DateTime`;
    END //
    DELIMITER ;

-- CLIENT

    DELIMITER //
    CREATE PROCEDURE spClient_Read_Search
    (
        IN fname VARCHAR(50),
        IN lname VARCHAR(50)
    )
    BEGIN
        SELECT * FROM `Client`
        WHERE `Name` Like fname
        AND `Surname` Like lname
        AND `Active` = 1
        ORDER BY `Name`;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spClient_Read_ID
    (
        IN id INT
    )
    BEGIN
        SELECT * FROM `Client`
        WHERE `Client_ID` = id;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spClient_Create
    (
        IN title VARCHAR(5),
        IN fname VARCHAR(50),
        IN lname VARCHAR(50),
        IN contactNumber VARCHAR(15),
        IN contactEmail VARCHAR(100),
        IN dateOfBirth DATE,
        IN reminders BOOLEAN,
        IN notifications BOOLEAN,
        IN notificationMethod_ID INT,
        IN address_ID INT
    )
    BEGIN
        DECLARE insertId  INT;
        INSERT
            INTO `Client`(
                `Title`,
                `Name`,
                `Surname`,
				`ContactNumber`,
                `email`,
                `DateOfBirth`,
                `Reminders`,
                `Notifications`,
                `Active`,
                `NoticationMethod_ID`,
                `Address_ID`
                            )
            VALUES
                (title, fname, lname, contactNumber, contactEmail, dateOfBirth, reminders, notifications, true, notificationMethod_ID, address_ID);
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spClient_Update
    (

        IN iclient_ID INT,
        IN ititle VARCHAR(5),
            IN ifname VARCHAR(50),
            IN ilname VARCHAR(50),
            IN icontactNumber VARCHAR(15),
            IN icontactEmail VARCHAR(100),
            IN idateOfBirth DATE,
            IN ireminders BOOLEAN,
            IN inotifications BOOLEAN,
            IN inotificationMethod_ID INT,
            IN iaddress_ID INT
    )
    BEGIN
        UPDATE
            `Client`
        SET
            `Title` = ititle,
            `Name`= ifname,
            `Surname`= ilname,
            `ContactNumber` = icontactNumber,
            `email` = icontactEmail,
            `DateOfBirth` = idateOfBirth,
            `Reminders` = ireminders,
            `Notifications` = inotifications,
            `NoticationMethod_ID`= inotificationMethod_ID,
            `Address_ID` = iaddress_ID
        WHERE
            `Client_id` = iclient_ID;

    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spClient_Delete
    (
        IN cID INT
    )
    BEGIN
        UPDATE `Client`
            SET
                `Active` = false
            WHERE
                `Client_id` = cID;
    END //
    DELIMITER ;

-- Address

    DELIMITER //
    CREATE PROCEDURE spAddress_Read
    (
        IN id INT
    )
    BEGIN
        SELECT
            a.`Line1`,
            a.`Line2`,
                    a.`Surburb_id`,
                    s.`City_id`,
                    c.`Province_id`
        FROM
                    `Address` a
                LEFT OUTER JOIN
            `Surburb` s ON (a.`Surburb_id` = s.`Surburb_id`)
                LEFT OUTER JOIN
            `City` c ON (s.`City_id` = c.`City_id`)
        WHERE a.`Address_ID` = id;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spAddress_Create
    (
        IN line1 VARCHAR(50),
            IN line2 VARCHAR(50),
            IN suburb_id INT
    )
    BEGIN
        DECLARE insertId  INT;
        INSERT
            INTO `Address`(
                `Line1`,
                `Line2`,
                `Surburb_id`
            )
            VALUES
                (line1, line2, suburb_id);

        SET insertId = LAST_INSERT_ID();
            SELECT insertId;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spAddress_Update
    (
        IN iaddress_ID INT,
        IN iline1 VARCHAR(50),
        IN iline2 VARCHAR(50),
        IN isuburb_id INT
    )
    BEGIN
        UPDATE
            `Address`
        SET
            `Line1` = iline1,
            `Line2` = iline2,
            `Surburb_id` = isuburb_id
        WHERE
            `Address_id` = iaddress_ID;
    END //
    DELIMITER ;

-- Client service history

    DELIMITER //
    CREATE PROCEDURE spClient_Service_History
    (
        IN iclientID INT
    )
    BEGIN
        SELECT
            i.`DateTime`,
            e.`Name` AS 'employeeFName',
            e.`Surname`AS 'employeeLName',
            s.`Name` AS 'service'
        FROM
            `Client` c,
            `Invoice` i,
            `Employee` e,
            `Service` s,
            `Service_History` sh,
            `Invoice_Service_Line` isl
        WHERE
            c.`Client_ID` = iclientID
            AND
            c.`Client_ID` = i.`Client_ID`
            AND
            isl.`Invoice_id` = i.`Invoice_ID`
            AND
            isl.`ServiceHistory_id` = sh.`ServiceHistory_id`
            AND
            sh.`Service_id` = s.`Service_id`
            AND
            e.`Employee_ID` = i.`Employee_ID`
        ;
    END //
    DELIMITER ;

-- Client product history

    DELIMITER //
    CREATE PROCEDURE spClient_Product_History
    (
        IN iclientID INT
    )
    BEGIN
        SELECT
            i.`DateTime`,
            e.`Name` AS 'employeeFName',
            e.`Surname`AS 'employeeLName',
            s.`BrandName` AS 'stockBName',
            s.`ProductName` AS 'stockPName',
            isl.`Quantity`
        FROM
            `Client` c,
            `Invoice` i,
            `Employee` e,
            `Stock` s,
            `Stock_History` sh,
            `Invoice_Stock_Line` isl
        WHERE
            c.`Client_ID` = iclientID
            AND
            c.`Client_ID` = i.`Client_ID`
            AND
            isl.`Invoice_id` = i.`Invoice_ID`
            AND
            isl.`StockHistory_id` = sh.`StockHistory_id`
            AND
            sh.`Stock_id` = s.`Stock_id`
            AND
            e.`Employee_ID` = i.`Employee_ID`;
    END //
    DELIMITER ;

-- Bookings

    DELIMITER //
    CREATE PROCEDURE spBooking_Read_ID
    (
        IN id INT
    )
    BEGIN
        SELECT 
			b.*, 
            c.`Title` as 'clientTitle',
            c.`Name` as 'clientFName', 
            c.`Surname` as 'clientLName',
            e.`Name` as 'employeeFName',
            e.`Surname` as 'employeeLName'
		FROM 
			`Booking` b, 
            `Client` c, 
            `Employee` e
        WHERE 
			`Booking_ID` = id
            AND
            b.`Client_id` = c.`Client_id`
            AND
            b.`Employee_id` = e.`Employee_id`
            AND
            b.`Active` = true;
    END //
    DELIMITER ;
    

	DELIMITER //
    CREATE PROCEDURE spBooking_Read_Search
    (
        IN fname VARCHAR(50),
        IN lname VARCHAR(50),
        IN reference VARCHAR(50)
    )
    BEGIN
        SELECT 
			b.*,  
            c.`Title` as 'clientTitle', 
            c.`Name` as 'clientFName', 
            c.`Surname` as 'clientLName',
            e.`Name` as 'employeeFName',
            e.`Surname` as 'employeeLName'
        FROM 
			`Booking` b, 
            `Client` c, 
            `Employee` e
        WHERE 
			(
				b.`ReferenceNumber` = reference
				OR
                (
					c.`Name` Like fname
					AND
					c.`Surname` Like lname
                )
			)
            AND
            b.`Client_id` = c.`Client_id`
            AND
            b.`Employee_id` = e.`Employee_id`
            AND
            b.`Active` = true;
    END //
    DELIMITER ;
    
	DELIMITER //
    CREATE PROCEDURE spBooking_Create
    (
        IN iDateTime DATETIME,
		IN iDuration INT,
		IN iCompleted BOOLEAN,
		IN iActive BOOLEAN,
		IN iReference VARCHAR(12),
		IN iClient_ID INT,
		IN iEmployee_ID INT
	)
    BEGIN
		DECLARE insertId  INT;
        
        INSERT 
			INTO `Booking`
				(
				`DateTime`,
				`Duration`,
				`Completed`,
				`Active`,
				`ReferenceNumber`,
				`Client_id`,
				`Employee_id`,
				`Invoice_id`
				)
			VALUES
				(
                iDateTime,
				iDuration,
				iCompleted,
				iActive,
				iReference,
				iClient_ID,
				iEmployee_ID,
				null
                );

        SET insertId = LAST_INSERT_ID();
            SELECT insertId;
    END //
    DELIMITER ;
    
	DELIMITER //
    CREATE PROCEDURE spBooking_Update
    (

        IN iBooking_ID INT,
        IN iDateTime DATETIME,
		IN iDuration INT,
		IN iCompleted BOOLEAN,
		IN iActive BOOLEAN,
		IN iReference VARCHAR(12),
		IN iEmployee_ID INT,
		IN iInvoice_ID INT
	)
    BEGIN
        UPDATE
            `Booking`
        SET
            `DateTime` = iDateTime,
            `Duration`= iDuration,
            `Completed`= iCompleted,
            `Active` = iActive,
            `ReferenceNumber` = iReference,
            `Employee_id` = iEmployee_ID,
            `Invoice_id` = iInvoice_ID
        WHERE
            `Booking_id` = iBooking_ID;
    END //
    DELIMITER ;
    
	DELIMITER //
    CREATE PROCEDURE spBooking_Delete
    (
        IN iBooking_ID INT
	)
    BEGIN
        UPDATE
            `Booking`
        SET
            `Active` = false
        WHERE
            `Booking_id` = iBooking_ID;
    END //
    DELIMITER ;
    
    DELIMITER //
    CREATE PROCEDURE spBooking_Services
    (
        IN bid INT
	)
    BEGIN
        SELECT
            bs.*, 
            hls.`HairLength_id` AS hlid, 
            hls.`Service_id`AS sid, 
            hls.`HairLengthService_id` AS hlsid,
            hls.`Duration` AS duration,
            s.`Price` AS price
        FROM 
			`Booking_Service` bs, `Hair_Length_Service` hls, `Service` s
        WHERE
            bs.`Booking_id` = bid
            AND
            hls.`HairLengthService_id` = bs.`HairLengthService_id`
            AND
            hls.`Service_id` = s.`Service_id`;
    END //
    DELIMITER ;
    
    -- booking services
    
	DELIMITER //
    CREATE PROCEDURE spBookingServices_Create
    (
        IN bid INT,
        IN hlsid INT
	)
    BEGIN
        INSERT 
			INTO 
				`Booking_Service`
			VALUES
				(
                bid,
				hlsid
                );
    END //
    DELIMITER ;
    
    DELIMITER //
    CREATE PROCEDURE spBookingServices_Delete
    (
        IN bid INT
	)
    BEGIN
        DELETE 
			FROM 
				`Booking_Service`
			WHERE
				`Booking_id` = bid;
    END //
    DELIMITER ;
    
-- Vouchers

    DELIMITER //
    CREATE PROCEDURE spVoucher_Read
    (
        IN barcode VARCHAR(50)
	)
    BEGIN
        SELECT 
			*
		FROM 
			`Voucher`
		WHERE
			`Voucher_ID` = barcode;
    END //
    DELIMITER ;
    
    DELIMITER //
    CREATE PROCEDURE spVoucher_Create
    (
        IN iamount DECIMAL(8,2),
        IN ibarcode VARCHAR(50)
	)
    BEGIN
		DECLARE insertId INT;
        
        INSERT INTO
			`Voucher` (`Amount`, `Barcode`)
		VALUES
			(iamount, ibarcode);
            
		SET insertId = LAST_INSERT_ID();
            SELECT insertId;
    END //
    DELIMITER ;
    
    DELIMITER //
    CREATE PROCEDURE spVoucher_Update
    (
		IN id INT,
        IN iamount DECIMAL(8,2)
	)
    BEGIN
        UPDATE
			`Voucher`
		SET
			`Amount` = iamount
		WHERE
			`Voucher_ID` = id;
    END //
    DELIMITER ;
    
    DELIMITER //
    CREATE PROCEDURE sp_audit_create
    (
		IN uid INT,
        IN acti VARCHAR(10),
        IN myQuery VARCHAR(300)
	)
    BEGIN
        INSERT 
			INTO
				`Audit`
                (`DateTime`,`Action`, `Description`,`User_ID`)
			VALUES
				(NOW(), acti ,myQuery, uid);
    END //
    DELIMITER ;
    
    DELIMITER //
    CREATE PROCEDURE sp_audit_Search
    (
		IN iuser VARCHAR(50),
        IN acti VARCHAR(10),
        IN dt datetime
	)
    BEGIN
        SELECT 
			a.*, u.Username
		FROM 
			Audit a, `User` u
		WHERE
			a.`User_ID` = u.`User_ID`
            AND
            u.Username Like iuser
            AND
            a.`Action` like acti
            AND
            a.`DateTime` > dt;
    END //
    DELIMITER ;
    
    -- ---- ---- ---- ----- USER -- ---- ---- ---- -----
use esalon;    

-- -- DELETE
DELIMITER //
create procedure sp_Delete_User
(
	IN uUser_id	int
)
BEGIN  Update `User`
	SET 
		`Active` = false
	WHERE 
		User_id = uUser_id;
END //
DELIMITER ;
-- ---- ---- ---- ROLE -- ---- ---- ---- ---- ----
-- -- READ
    DELIMITER //
    CREATE PROCEDURE spRole_Read
    (
        IN rName VARCHAR(40)
    )
    BEGIN
        SELECT * FROM `Role`
        WHERE `Name` = rName;
    END //
    DELIMITER ;  

-- ---- ---- ----- SUPPLIER -- ---- ---- ---- ---- ---
-- --READ
    DELIMITER //
    CREATE PROCEDURE spSupplier_Read
    (
        IN sSupplier_id INT
    )
    BEGIN
        SELECT * FROM `Supplier`
        WHERE `Supplier_id` = sSupplier_id;
    END //
    DELIMITER ;
    
-- -- UPDATE
DELIMITER //
create procedure sp_Update_Supplier
(
	IN sSupplier_id  	INT,
	IN sName 		VARCHAR(50),
	IN sContactNumber VARCHAR(15),
	IN sEmail 	VARCHAR(100),
    IN sActive BOOLEAN
)
BEGIN  UPDATE `Supplier` SET 
	`Name` = sName,
	`ContactNumber` = sContactNumber,
	`Email` = sEmail,
	Active = sActive
WHERE `Supplier_id` = sSupplier_id; 
END //
DELIMITER ;

-- -- INSERT
DELIMITER //
create procedure sp_Insert_Supplier 
(
	IN sName 		VARCHAR(50),
	IN sContactNumber VARCHAR(15),
	IN sEmail 	VARCHAR(100),
    IN sActive BOOLEAN
)
BEGIN 
INSERT INTO `Supplier` (`Name`, `ContactNumber`, `Email`, `Active`)
VALUES(sName, sContactNumber, sEmail, true); 
END //
DELIMITER ;

-- -- DELETE
DELIMITER //
create procedure sp_Delete_Supplier
(
	IN sSupplier_id  	INT
)
BEGIN  Update `Supplier`
	SET 
		`Active` = false
	WHERE 
		sSupplier_id = sSupplier_id;
END //
DELIMITER ;

-- ---- ---- ---- -- ORDER -- ---- ---- ---- ---

-- --READ
    DELIMITER //
    CREATE PROCEDURE spOrder_Read
    (
        IN oOrder_ID INT
    )
    BEGIN
        SELECT * FROM `Order`
        WHERE `Order_id` = oOrder_ID;
    END //
    DELIMITER ;
    
-- -- SEARCH
	DELIMITER //
    CREATE PROCEDURE spOrder_Search
    (
		in sid INT,
        in dateTo DATE,
        in dateFrom DATE
    )
    BEGIN
        SELECT * FROM `Order`
        WHERE 
			`Supplier_ID` = sid
            AND
			`DatePlaced` >= dateFrom 
			AND
            `DatePlaced` <= dateTo;
    END //
    DELIMITER ;
    
-- -- UPDATE
DELIMITER //
create procedure sp_Update_Order
(
	IN oOrder_id  	INT,
	IN oDateReceived  DATE
)
BEGIN  
	UPDATE `Order` SET 
		DateReceived = oDateReceived
	WHERE Order_id = oOrder_id;
 END //
DELIMITER ;

-- -- INSERT
DELIMITER //
create procedure sp_Insert_Order
(

	IN oDatePlaced  	DATE,
	IN oDateReceived  DATE,
	IN sSupplier_id 	INT
)
BEGIN 
	DECLARE insertId INT;
		INSERT INTO `Order` (`DatePlaced`, `DateReceived`, `Supplier_id`)
		VALUES(oDatePlaced, oDateReceived, sSupplier_id);  
	SET insertId = LAST_INSERT_ID();
				SELECT insertId;
END //
DELIMITER ;

-- -- DELETE
DELIMITER //
create procedure sp_Delete_Order
(
	IN oOrder_id  	INT
)
BEGIN  
DELETE from `Order_Line` 
	WHERE 
		`Order_id` = oOrder_id;
delete 
	FROM `Order`
	WHERE 
		`Order_id` = oOrder_id;
END //
DELIMITER ;

-- ---- ---- ---- --- ORDER LINE -- ---- ---- ---- -----
-- -- READ
    DELIMITER //
    CREATE PROCEDURE spOrderLine_Read
    (
        IN rOrderLine_id INT
    )
    BEGIN
        SELECT * FROM `Order_Line`
        WHERE `OrderLine_id` = rOrderLine_id;
    END //
    DELIMITER ;

-- -- UPDATE
DELIMITER //
create procedure sp_Update_Order_Line
(
	IN rOrderLine_id  INT,
	IN rQuantity INT,
	IN sStock_id INT,
	IN oOrder_id INT
)
BEGIN  UPDATE `Order_Line` SET 
	`Quantity` = rQuantity
	
WHERE `OrderLine_id` = rOrderLine_id

;  END //
DELIMITER ;

-- -- INSERT
DELIMITER //
create procedure sp_Insert_Order_Line
(
	IN rQuantity INT,
	IN sStock_id INT,
	IN oOrder_id INT
)
BEGIN 
INSERT INTO `Order_Line` (Quantity, Stock_id, Order_id)
VALUES(rQuantity, sStock_id, oOrder_id)

;  END //
DELIMITER ;

-- -- DELETE
DELIMITER //
create procedure sp_Delete_Order_Line
(
	IN rOrderLine_id  INT
)
BEGIN  
DELETE from `Order_Line` 
	WHERE 
		`OrderLine_id` = rOrderLine_id;
END //
DELIMITER ;

-- ---- ---- ----- HAIR LENGTH -- ---- ---- ---- ---
-- --READ
--    DELIMITER //
--    CREATE PROCEDURE spHairLength_Read
--    (
--        IN hHairLength_id INT
--    )
--    BEGIN
--        SELECT * FROM `Hair_Length`
--        WHERE `HairLength_id` = hHairLength_id;
--    END //
--    DELIMITER ;
    
-- -- INSERT
	DELIMITER //
	create procedure sp_Insert_Hair_Length
	(
		IN hDescription  	VARCHAR(50)
	)
	BEGIN 
	INSERT INTO `Hair_Length` (`Description`)
	VALUES(hDescription)
	;  END //
	DELIMITER ;


-- ---- ---- ---- --- CATEgoRY -- ---- ---- ---- --
    DELIMITER //
    CREATE PROCEDURE spCategory_Read
    (
        IN cCategory_id INT
    )
    BEGIN
        SELECT * FROM `Category`
        WHERE `Category_id` = cCategory_id;
    END //
    DELIMITER ;
    

-- ---- ---- ---- ----- STOCK -- ---- ---- ---- ---- ----
-- -- READ    

    DELIMITER //
    CREATE PROCEDURE spStock_Read
    (
        IN sStock_id INT
    )
    BEGIN
        SELECT * FROM `Stock`
        WHERE `Stock_id` = sStock_id;
    END //
    DELIMITER ;
-- -- search
DELIMITER //
    CREATE PROCEDURE spStock_Search
    (
		in sname int,
        in bname varchar(50),
        in pname varchar(50)
    )
    BEGIN
        SELECT * FROM `Stock`
        WHERE (`Supplier_ID` = sname
        OR `BrandName` = bname
        OR `ProductName` = pname)
        AND `Active` = true;
    END //
    DELIMITER ;

-- -- UPDATE
DELIMITER //
create procedure esalon.sp_Update_Stock
(
	IN sStock_id   INT,
	IN sBrandName    	VARCHAR(50),
	IN sProductName   VARCHAR(50),
	IN sPrice    	DECIMAL(8, 2),
	IN sSize   	INT,
	IN sQuantity    	INT,
	IN sBarcode   VARCHAR(10),
	IN sSupplier_id INT
)
BEGIN  
	UPDATE `Stock` SET 
		`BrandName` = sBrandName,
		`ProductName` = sProductName,
		`Price` = sPrice,
		`Size` = sSize,
		`Quantity` = sQuantity,
		`Barcode` = sBarcode,
		`Supplier_id` = sSupplier_id
	
	WHERE `Stock_id` = sStock_id;

	UPDATE `stock_history`
		SET 
			`PriceDateTo` = current_date()
		WHERE
			`Stock_id` = sStock_id
				AND
			`PriceDateTo` IS NULL;
				
		INSERT 
			INTO `stock_history` 
				(`Price`, `PriceDateFrom`, `PriceDateTo`, `Stock_ID`)
			VALUES
				(sPrice, current_date(), null, sStock_id);
END //
DELIMITER ;

-- -- RECONCILE
DELIMITER //
create procedure esalon.sp_Reconcile_Stock
(
	IN sStock_id   INT,
	IN sSum    INT
)
BEGIN  
	UPDATE `Stock` SET 
		`Quantity` = (`Quantity` - sSum)
	WHERE `Stock_id` = sStock_id;

END //
DELIMITER ;

-- -- INSERT
DELIMITER //
create procedure sp_Insert_Stock
(
	IN sBrandName    	VARCHAR(50),
	IN sProductName   VARCHAR(50),
	IN sPrice    	DECIMAL(8, 2),
	IN sSize   	INT,
	IN sActive   BOOLEAN,
	IN sQuantity    	INT,
	IN sBarcode   VARCHAR(10),
	IN cCategory_id INT,
	IN sSupplier_id INT
)
BEGIN 
	DECLARE insertId  INT;
    
	INSERT INTO `Stock` (BrandName, ProductName, Price, Size, 
							Active, Quantity, Barcode, category_id, supplier_id)
	VALUES(sBrandName, sProductName, sPrice, sSize, 
							true, sQuantity, sBarcode, cCategory_id, sSupplier_id);
	SET insertId = LAST_INSERT_ID();
		SELECT insertId;
        
	INSERT 
			INTO `Stock_History` 
				(`Price`, `PriceDateFrom`, `PriceDateTo`, `Stock_ID`)
			VALUES
				(sPrice, current_date(), null, insertId);
END //
DELIMITER ;

-- -- DELETE
DELIMITER //
create procedure esalon.sp_Delete_Stock
(
	IN sStock_id   INT
)
BEGIN  Update `stock`
	SET 
		`Active` = false
	WHERE 
		sStock_id = Stock_id;
END //
DELIMITER ;
-- ---- ---- ---- STOCK HISTORY -- ---- ---- ---- ---- --
    DELIMITER //
    CREATE PROCEDURE spStockHistory_Read
    (
        IN hStockHistory_id INT
    )
    BEGIN
        SELECT * FROM `Stock_History`
        WHERE `StockHistory_id` = hStockHistory_id;
    END //
    DELIMITER ;
    
-- -- UPDATE
DELIMITER //
create procedure sp_Update_Stock_History
(
	IN hStockHistory_id  INT,
	IN hPrice     	DECIMAL(8, 2),
	IN hPriceDateTo    DATE,
	IN sStock_id INT
)
BEGIN  UPDATE `Stock_History` SET 
	Price = hPrice,
	PriceDateTo = hPriceDateTo
	
WHERE StockHistory_id = hStockHistory_id

;  END //
DELIMITER ;

-- -- INSERT
DELIMITER //
create procedure sp_Insert_Stock_History
(
	IN hPrice     	DECIMAL(8, 2),
	IN hPriceDateFrom    DATE,
	IN sStock_id INT
)
BEGIN 
INSERT INTO `Stock_History` (Price, PriceDateFrom, Stock_id)
VALUES(hPrice, hPriceDateFrom, sStock_id)

;  END //
DELIMITER ;


-- ---- ---- ---- -- INVOICE STOCK LINE -- ---- ---- ---- --
    DELIMITER //
    CREATE PROCEDURE spInvoiceStockLine_Read
    (
        IN iInvoice_id INT
    )
    BEGIN
        SELECT * FROM `Invoice_Stock_Line`
        WHERE `Invoice_id` = iInvoice_id;
    END //
    DELIMITER ;

-- -- INSERT
DELIMITER //
create procedure sp_Insert_Invoice_Stock_Line
(

	IN iPrice DECIMAL(8, 2),
	IN iQuantity INT,
	IN pSpecial_id INT,
	IN hStockHistory_id INT,
    IN sSpecial_id INT,
	IN iInvoice_id INT
)
BEGIN 
INSERT INTO `Invoice_Stock_Line` (Price, Quantity, Special_id, StockHistory_id, Special_id, Invoice_id)
VALUES(iPrice, iQuantity, pSpecial_id, hStockHistory_id, sSpecial_id, iInvoice_id)

;  END //
DELIMITER ;

-- ---- ---- ---- ---- SPECIAL STOCK CONDITION LINE -- ---- ---- ---- -----
-- --READ
    DELIMITER //
    CREATE PROCEDURE spSpecial_Stock_Condition_Line_Read
    (
        IN _sstcl_id INT
    )
    BEGIN
        SELECT * FROM `Special_Stock_Condition_Line`
        WHERE `sstcl_id` = _sstcl_id;
    END //
    DELIMITER ;

-- -- INSERT
DELIMITER //
create procedure sp_Insert_Special_Stock_Condition_Line
(
	
	IN _scQuantity INT,
	IN sSpecial_id INT,
	IN sStock_id INT
)
BEGIN 
INSERT INTO `Special_Stock_Condition_Line` (scQuantity, Special_id, Stock_id)
VALUES(_scQuantity, sSpecial_id, sStock_id)

;  END //
DELIMITER ;


-- ---- ---- -- SPECIAL STOCK RESULT LINE -- ---- ---- ---- --

-- --READ
    DELIMITER //
    CREATE PROCEDURE spSpecial_Stock_Result_Line_Read
    (
        IN _sstrl_id INT
    )
    BEGIN
        SELECT * FROM `Special_Stock_Result_Line`
        WHERE `ssRl_id` = _sstrl_id;
    END //
    DELIMITER ;

-- -- INSERT
DELIMITER //
create procedure sp_Insert_Special_Stock_Result_Line
(
	
	IN srDiscountAmount DECIMAL (8,2),
	IN srIsPercentage BOOLEAN,
	IN sSpecial_id INT,
	IN sStock_id INT
)
BEGIN 
INSERT INTO `Special_Stock_Result_Line` (DiscountAmount, IsPercentage, Special_id, Stock_id)
VALUES(srDiscountAmount, srIsPercentage, sSpecial_id, sStock_id)

;  END //
DELIMITER ;


-- ---- ----- AUDIT -- ---- ---- ---- ---
-- -- read
    DELIMITER //
    CREATE PROCEDURE spAudit_Read
    (
        IN id INT
    )
    BEGIN
        SELECT * FROM `Audit`
        WHERE `Audit_ID` = id;
    END //
    DELIMITER ;
    
-- -- INSERT
DELIMITER //
create procedure sp_Insert_Audit
(

	IN aDateTime DATETIME,
	IN aAction VARCHAR(300),
	IN aUser_ID INT
)
BEGIN 
INSERT INTO `Audit` (`DateTime`, `Action`, `User_ID`)
VALUES(aDateTime, aAction, aUser_ID)

;  END //
DELIMITER ;

-- ---- ---- ---- ---- ---- ---- ---- ---- ---- MINOR -- ---- ---- ---- ----

-- -- VIEW

    DELIMITER //
    CREATE PROCEDURE sp_Insert_Minor_Read
    ()
    BEGIN
        SELECT * FROM `Minor`;
    END //
    DELIMITER ;
-- ---- ---- ----- MAJOR -- ---- ---- ---- --

DELIMITER //
    CREATE PROCEDURE sp_Insert_Major_Read
    ()
    BEGIN
        SELECT * FROM `Major`;
    END //
    DELIMITER ;
    
-- ---- ---- ----- PERRMISIONS -- ---- ---- ---- ---
-- -- read
    DELIMITER //
    CREATE PROCEDURE spPermissions_Read
    (
        IN id INT
    )
    BEGIN
        SELECT * FROM `Permission`
        WHERE `Permission_ID` = id;
    END //
    DELIMITER ;

-- ---- ---- ---- -- ROLE PERMISIONS -- ---- ---- ---- ---- --
-- -- read
    DELIMITER //
    CREATE PROCEDURE spRolePermissions_Read
    (
        IN id INT
    )
    BEGIN
        SELECT * FROM `Role_Permission`
        WHERE `Role_ID` = id;
    END //
    DELIMITER ;
    
-- -- UPDATE
DELIMITER //
create procedure sp_Update_Role_Permission
(
	IN rRole_ID  INT,
	IN rPermission_ID INT
)	
BEGIN  
UPDATE `Role_Permission` SET 
	Permission_ID = rPermission_ID
    
WHERE Role_ID = rRole_ID; 
END //
DELIMITER ;

-- -- INSERT
DELIMITER //
create procedure sp_Insert_Role_Permission
(
	IN rPermission_ID INT
)
BEGIN 
INSERT INTO `Role_Permission` (Permission_ID)
VALUES(rPermission_ID)

;  END //
DELIMITER ;

-- -- DELETE
DELIMITER //
create procedure sp_Delete_Role_Permission
(
	IN Role_ID  INT
)
BEGIN  DELETE FROM `Role_Permission`
WHERE Role_ID = rRole_ID; 
END //
DELIMITER ;
-- ---- ---- ---- ---INVOICE -- ---- ---- ---- --

    DELIMITER //
    CREATE PROCEDURE spInvoice_Read
    (
        IN id INT
    )
    BEGIN
        SELECT * FROM `Invoice`
        WHERE `Invoice_id` = id;
    END //
    DELIMITER ;


-- -- -- -- -- -- -- REPORTS -- -- -- -- -- -- -- -- --

-- -- STOCK LEVEL
DELIMITER //
    CREATE PROCEDURE spStockLevel()
    BEGIN
        SELECT BrandName, ProductName, Quantity FROM `Stock`
        WHERE Active = true;
    END //
DELIMITER ;

-- -- EXPENSE REPORT
DELIMITER //
    CREATE PROCEDURE spExpenseReport
    (
		IN dateFrom date,
        IN dateTo date
    )
    BEGIN
        SELECT e.`Name`, e.`Quantity`, e.`PricePerItem`, c.`Name` as Category
        FROM `expense` e
			LEFT OUTER JOIN `expense_category` c
				ON e.`ExpenseCategory_ID` = c.`ExpenseCategory_ID`
		 WHERE 
			e.`Date` > dateFrom
			AND
			e.`Date` < dateTo;
    END //
DELIMITER ;

-- -- INCOME REPORT
DELIMITER //
    CREATE PROCEDURE spIncomeReport_Iservices
    (
		IN dateFrom date,
        IN dateTo date
        
    )
    BEGIN
        SELECT i.`DateTime`, isl.`Quantity`, sh.`Price`, s.`Name` as iName
        FROM `invoice` i, `invoice_service_line` isl, `service` s, `service_history` sh
        WHERE 
			i.Invoice_ID = isl.Invoice_id
            AND
			isl.`ServiceHistory_id` = sh.`ServiceHistory_id`
            AND
            sh.`Service_id` = s.`Service_id`
            AND
            i.`DateTime` > dateFrom
            AND
            i.`DateTime` < dateTo;

    END //
DELIMITER ;

DELIMITER //
    CREATE PROCEDURE spIncomeReport_Istock
    (
		IN dateFrom date,
        IN dateTo date
        
    )
    BEGIN
        SELECT i.`DateTime`, isl.`Quantity`, isl.`Price`, s.`ProductName` as iName
        FROM `invoice` i, `invoice_stock_line` isl, `stock` s, `stock_history` sh
        WHERE 
			i.Invoice_ID = isl.Invoice_id
            AND
			isl.`StockHistory_id` = sh.`StockHistory_id`
            AND
            sh.`Stock_ID` = s.`Stock_id`
            AND
            i.`DateTime` > dateFrom
            AND
            i.`DateTime` < dateTo;

    END //
DELIMITER ;

DELIMITER //
    CREATE PROCEDURE spSubletterPayment
    (
		IN dateFrom date,
        IN dateTo date
        
    )
    BEGIN
        SELECT s.`BusinessName` as iName, sp.`DateTime`, sp.`Amount` as Quantity
        FROM `sub_letter` s, `sub_letter_payment` sp
        WHERE 
			sp.`Sub_Letter_id` = s.`Sub_Letter_id`
            AND
            sp.`DateTime` > dateFrom
            AND
            sp.`DateTime` < dateTo;

    END //
DELIMITER ;

-- -- CLIENT REPORT
DELIMITER //
    CREATE PROCEDURE spClient()
    BEGIN
        SELECT `DateTime`, `Client_id`
        FROM `booking`
        ORDER BY `Client_id`;

    END //
DELIMITER ;

-- -- STOCK TRENDS
DELIMITER //
    CREATE PROCEDURE spStockTrends_sold
    (
		IN dateFrom date,
        IN dateTo date,
        IN productName VARCHAR(50)
        
    )
    BEGIN
        SELECT i.`DateTime` as DateSold, isl.`Quantity`, s.`ProductName` 
        FROM `invoice` i, `invoice_stock_line` isl, `stock` s, `stock_history` sh
        WHERE 
			isl.`StockHistory_id` = sh.`StockHistory_id`
            AND
            sh.`Stock_id` = s.`Stock_id`
            AND
            i.`DateTime` > dateFrom
            AND
            i.`DateTime` < dateTo
            AND
            s.`ProductName` LIKE productName;

    END //
DELIMITER ;

DELIMITER //
    CREATE PROCEDURE spStockTrends_bought
    (
		IN dateFrom date,
        IN dateTo date,
        IN productName VARCHAR(50)
        
    )
    BEGIN
        SELECT o.`DateReceived` as DateBought, ol.`Quantity`, s.`ProductName`
        FROM `order` o, `order_line` ol, `stock` s
        WHERE 
			ol.`Order_ID` = o.`Order_id`
            AND
            ol.`Stock_ID` = s.`Stock_id`
            AND
            o.`DateReceived` > dateFrom
            AND
            o.`DateReceived` < dateTo
            AND
            s.`ProductName` LIKE productName;

    END //
DELIMITER ;
-- -- EMPLOYEE INCOME
DELIMITER //
    CREATE PROCEDURE spEployeeIncome
    (
        IN EName VARCHAR(50),
        IN ESurname VARCHAR(50)
        
    )
    BEGIN
        SELECT `Name`,`Surname`, `Employee_ID`
        FROM `employee`
        WHERE `Name` LIKE EName
        AND
        `Surname` LIKE ESurname;

    END //
DELIMITER ;

DELIMITER //
    CREATE PROCEDURE spIncomeReport_services
    (
		IN dateFrom date,
        IN dateTo date
        
    )
    BEGIN
        SELECT i.`DateTime` as incomeDate, isl.`Quantity`, isl.`Price`, s.`Name`, e. `Employee_ID` 
        FROM `invoice` i, `invoice_service_line` isl, `service` s, `service_history` sh, `employee` e
        WHERE 
			isl.`ServiceHistory_id` = sh.`ServiceHistory_id`
            AND
            e.`Employee_ID` = i.`Employee_ID`
            AND
            sh.`Service_id` = s.`Service_id`
            AND
            i.`DateTime` > dateFrom
            AND
            i.`DateTime` < dateTo;

    END //
DELIMITER ;

DELIMITER //
    CREATE PROCEDURE spIncomeReport_stock
    (
		IN dateFrom date,
        IN dateTo date
        
    )
    BEGIN
        SELECT i.`DateTime` as incomeDate, isl.`Quantity`, isl.`Price`, s.`ProductName`, e. `Employee_ID` 
        FROM `invoice` i, `invoice_stock_line` isl, `stock` s, `stock_history` sh, `employee` e
        WHERE 
			isl.`StockHistory_id` = sh.`StockHistory_id`
            AND
            e.`Employee_ID` = i.`Employee_ID`
            AND
            sh.`Stock_ID` = s.`Stock_id`
            AND
            i.`DateTime` > dateFrom
            AND
            i.`DateTime` < dateTo;

    END //
DELIMITER ;

-- ------------------------------------------------------------------------------
-- This file contains the final stored procedures for the eSalon system, DO NOT make changes without
-- full system functionality tests.
-- ------------------------------------------------------------------------------

Use esalon;

-- Bookings

	DELIMITER //
    CREATE PROCEDURE spEmployeeBookings_Read_ID
    (
        IN id INT
    )
    BEGIN
        SELECT 
			b.*, 
            c.`Name` as 'clientFName', 
            c.`Surname` as 'clientLName',
            e.`Name` as 'employeeFName',
            e.`Surname` as 'employeeLName'
		FROM 
			`Booking` b, 
            `Client` c, 
            `Employee` e
        WHERE 
			e.`Employee_id` = id
            AND
            b.`Client_id` = c.`Client_id`
            AND
            b.`Employee_id` = e.`Employee_id`
            AND
            b.`Active` = true;
    END //
    DELIMITER ;

-- Employees

    DELIMITER //
    CREATE PROCEDURE spEmployee_Read_Search
    (
        IN fname VARCHAR(50),
        IN lname VARCHAR(50),
        IN irole VARCHAR(30)
    )
    BEGIN
        SELECT
			e.*, r.`Name` as "Role", CONCAT(a.`Line2`,", ",a.`Line1`) as "Address"
		FROM
			`Employee`e
				LEFT OUTER JOIN `User` u
					ON e.`Employee_ID` = u.`Employee_ID`
				LEFT OUTER JOIN `Role` r
					ON u.`Role_ID` = r.`Role_ID`
				LEFT OUTER JOIN `Address` a
					ON a.`Address_id`  = e.`Address_ID`
        WHERE
			e.`Name` Like fname
			AND
			e.`Surname` Like lname
			AND
            r.`Name` Like irole
            AND
            e.`Active` = true
        ORDER BY
			e.`Name`;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spEmployee_Read_ID
    (
        IN id INT
    )
    BEGIN
        SELECT
			e.*, u.User_ID
        FROM
			`Employee` e 
				LEFT OUTER JOIN `User` u 
					ON (e.Employee_ID = u.Employee_ID)
        WHERE
			e.`Employee_ID` = id;
    END //
    DELIMITER ;

	DELIMITER //
    CREATE PROCEDURE spEmployee_Create
    (
        IN ifname VARCHAR(50),
		IN ilname VARCHAR(50),
		IN icnumber VARCHAR(15),
		IN icemail VARCHAR(100),
        IN isalary DECIMAL(8,2),
        IN iimage VARCHAR(200),
		IN iaddressid INT
    )
    BEGIN
		DECLARE insertId  INT;
        
        INSERT
            INTO `Employee`(
                `Name`,
                `Surname`,
                `ContactNumber`,
                `email`,
                `Salary`,
                `Active`,
                `Image`,
                `Address_ID`)
            VALUES
                (ifname, ilname, icnumber, icemail, isalary, true, iimage, iaddressid);
                
		SET insertId = LAST_INSERT_ID();
            SELECT insertId;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spEmployee_Update
    (
        IN iid INT,
        IN ifname VARCHAR(50),
        IN ilname VARCHAR(50),
        IN icnumber VARCHAR(15),
        IN icemail VARCHAR(100),
        IN isalary DECIMAL(8,2),
        IN iactive BOOLEAN,
        IN iimage VARCHAR(200),
        IN iaddressid INT
    )
    BEGIN
        UPDATE `Employee`
            SET
                `Name` = ifname,
                `Surname` = ilname,
                `ContactNumber` = icnumber,
                `email` = icemail,
                `Salary` = isalary,
                `Active` = iactive,
                `Image` = iimage,
                `Address_ID` = iaddressid
            WHERE
                `Employee_ID` = iid;
    END //
    DELIMITER ;

    DELIMITER //
    CREATE PROCEDURE spEmployee_Delete
    (
        IN iid INT
    )
    BEGIN
        UPDATE `Employee`
            SET
                `Active` = false
            WHERE
                `Employee_ID` = iid;
		UPDATE `User`
            SET
                `Active` = false
            WHERE
                `Employee_ID` = iid;
    END //
    DELIMITER ;
    
-- expenses

-- -- INSERT
	DELIMITER //
	create procedure sp_Insert_Expense
	(
		IN iName VARCHAR(50),
		IN iQuantity INT,
        IN iDate DATETIME,
		IN iPrice DECIMAL(8,2),
		IN iCategory INT,
        IN iPMethod INT
	)
	BEGIN 
		DECLARE insertId  INT;
        
		INSERT INTO `Expense` 
			(`Name`, `Quantity`, `Date`, `PricePerItem`, `ExpenseCategory_ID`, `PaymentMethod_ID`)
		VALUES(iName, iQuantity, iDate, iPrice, iCategory, iPMethod);
        
        SET insertId = LAST_INSERT_ID();
        SELECT insertId;
	END // 
	DELIMITER ;
    
-- Invoice

	DELIMITER //
    CREATE PROCEDURE sp_Insert_Invoice
    (
        IN iDateTime DATETIME,
		IN iDiscount DECIMAL(8,2),
		IN iIsPerc BOOLEAN,
		IN iTotal DECIMAL(8,2),
        IN iPaymentMethod INT,
        IN icid INT,
		IN ieid INT,
        IN ibid INT
    )
    BEGIN
		DECLARE insertId  INT;
        
        INSERT
            INTO `Invoice`(
                `DateTime`,
                `Discount`,
                `isPercentage`,
                `Total`,
                `PaymentMethod_ID`,
                `Client_ID`,
                `Employee_ID`)
            VALUES
                (iDateTime, iDiscount, iIsPerc, iTotal, iPaymentMethod, icid, ieid);
                
		SET insertId = LAST_INSERT_ID();
        
        UPDATE `Booking` 
			SET
				`Invoice_id` = insertId
			WHERE
				`Booking_id` = ibid;
                
		SELECT insertId;
    END //
    DELIMITER ;

	DELIMITER //
    CREATE PROCEDURE sp_Insert_Invoice_Service
    (
        IN iPrice DECIMAL(8,2),
		IN iQuantity INT,
		IN ishid INT,
		IN ispid INT,
        IN iiid INT
    )
    BEGIN
		DECLARE insertId  INT;
        
        INSERT
            INTO `Invoice_Service_Line`(
                `Price`,
                `Quantity`,
                `ServiceHistory_id`,
                `Special_id`,
                `Invoice_id`)
            VALUES
                (iPrice, iQuantity, ishid, ispid, iiid);
                
		SET insertId = LAST_INSERT_ID();                
		SELECT insertId;
    END //
    DELIMITER ;
    
    DELIMITER //
    CREATE PROCEDURE sp_Insert_Invoice_Stock
    (
        IN iPrice DECIMAL(8,2),
		IN iQuantity INT,
		IN ishid INT,
		IN ispid INT,
        IN iiid INT
    )
    BEGIN
		DECLARE insertId  INT;
        
        INSERT
            INTO `Invoice_Stock_Line`(
                `Price`,
                `Quantity`,
                `StockHistory_id`,
                `Special_id`,
                `Invoice_id`)
            VALUES
                (iPrice, iQuantity, ishid, ispid, iiid);
                
		SET insertId = LAST_INSERT_ID(); 
        
        UPDATE `Stock`
			SET 
				`Quantity` = `Quantity` - iQuantity
            WHERE
				`Stock_id` = (
							SELECT 
								`Stock_ID` 
							FROM 
								`Stock_History` 
							WHERE 
								`StockHistory_id` = ishid);
				
		SELECT insertId;
    END //
    DELIMITER ;

-- Roles

	DELIMITER //
    CREATE PROCEDURE spRoles_Lookup
    (
    )
    BEGIN
        SELECT
			*
        FROM
			`Role`;
    END //
    DELIMITER ;
    
-- Services
	
    -- --READ by search
	DELIMITER //
    CREATE PROCEDURE spService_Search
    (
        IN sname VARCHAR(50)
    )
    BEGIN
        SELECT 
			s.*
		FROM 
			`Service` s
        WHERE 
			s.`Name` like sname
            AND
            s.`Active` = true
        ORDER BY 
			s.`Name`;
    END //
    DELIMITER ;

	-- --READ by ID
    DELIMITER //
    CREATE PROCEDURE spService_Read
    (
        IN id INT
    )
    BEGIN
        SELECT 
			s.*
		FROM 
			`Service` s
        WHERE 
			s.`Service_id` = id
            AND
            s.`Active` = true;
    END //
    DELIMITER ;
    
    -- --READ service duration
    DELIMITER //
    CREATE PROCEDURE spService_Read_Duration
    (
        IN sid INT,
        IN hlid INT
    )
    BEGIN
        SELECT 
			hls.*
		FROM 
			`Hair_Length_Service` hls
        WHERE 
			hls.`Service_id` = sid
            AND
            hls.`HairLength_id` = hlid;
    END //
    DELIMITER ;
    
	-- -- INSERT
	DELIMITER //
	CREATE PROCEDURE sp_Insert_Service
	(
		IN sname	VARCHAR(50),
		IN sinfo	VARCHAR(300),
        IN sprice 	DOUBLE(8,2),
        IN durS		INT,
        IN durM		INT,
        IN durL		INT
	)
	BEGIN 
		DECLARE insertId INT;
		
		INSERT 
			INTO `Service` 
				(`Name`, `AdditionalInformation`, `Price`, `Active`)
			VALUES
				(sname, sinfo, sprice, True);         
				
		SET insertId = LAST_INSERT_ID();
			SELECT insertId;
				
		INSERT 
			INTO `Service_History` 
				(`Price`, `PriceDateFrom`, `PriceDateTo`, `Service_id`)
			VALUES
				(sprice, current_date(), null, insertId);
		
        INSERT 
			INTO `Hair_Length_Service` 
				(`Duration`, `HairLength_id`, `Service_id`)
			VALUES
				(durS, 3, insertId),
                (durM, 2, insertId),
                (durL, 1, insertId);
            
	END //
	DELIMITER ;
    
	-- -- UPDATE
	DELIMITER //
	CREATE PROCEDURE sp_Update_Service
	(
		IN sid		INT,
		IN sname	VARCHAR(50),
		IN sinfo	VARCHAR(300),
        IN sprice 	DOUBLE(8,2),
        IN durS		INT,
        IN durM		INT,
        IN durL		INT
	)
	BEGIN
    
		UPDATE `Service` 
			SET
				`Name` = sname,
				`AdditionalInformation` = sinfo,
				`Price` = sprice
			WHERE
				`Service_id` = sid;
				
		UPDATE `Service_History`
			SET 
				`PriceDateTo` = current_date()
			WHERE
				`Service_id` = sid
					AND
				`PriceDateTo` IS NULL;
				
		INSERT 
			INTO `Service_History` 
				(`Price`, `PriceDateFrom`, `PriceDateTo`, `Service_id`)
			VALUES
				(sprice, current_date(), null, sid);
                
		UPDATE `Hair_Length_Service`
			SET 
				`Duration` = durS
			WHERE
				`Service_id` = sid
					AND
				`HairLength_id` = 3;
                
		UPDATE `Hair_Length_Service`
			SET 
				`Duration` = durM
			WHERE
				`Service_id` = sid
					AND
				`HairLength_id` = 2;
                
		UPDATE `Hair_Length_Service`
			SET 
				`Duration` = durL
			WHERE
				`Service_id` = sid
					AND
				`HairLength_id` = 1;
            
	END //
	DELIMITER ;
    
	DELIMITER //
    CREATE PROCEDURE spService_Delete
    (
        IN sid INT
    )
    BEGIN
        UPDATE `Service`
            SET
                `Active` = false
            WHERE
                `Service_ID` = sid;
    END //
    DELIMITER ;
    
    DELIMITER //
    CREATE PROCEDURE spServiceMap
    (
    
    )
    BEGIN
    SELECT 
		hls.HairLengthService_id as hlsid, 
		sh.ServiceHistory_id as shid
	FROM 
		eSalon.Service_History sh, 
		eSalon.Hair_Length_Service hls
	WHERE 
		sh.Service_id = hls.Service_id
		AND
		sh.PriceDateTo is null;
    END //
    DELIMITER ;
    
    DELIMITER //
    CREATE PROCEDURE spProductMap
    (
    
    )
    BEGIN
    SELECT 
		sh.Stock_ID as sid, 
		sh.StockHistory_id as shid
	FROM 
		eSalon.Stock_History sh
	WHERE
		sh.PriceDateTo is null;
    END //
    DELIMITER ;
    
-- Supplier
	-- -- SEARCH
	DELIMITER //
    CREATE PROCEDURE spSupplier_Search
    (
        IN sname VARCHAR(50)
    )
    BEGIN
        SELECT 
			*
		FROM 
			`Supplier` sup
        WHERE 
			sup.`Name` Like sname
            AND
            sup.`Active` = true;
    END //
    DELIMITER ;
    
-- User
	-- -- READ
    DELIMITER //
    CREATE PROCEDURE spUser_Read
    (
        IN uUsername VARCHAR(40)
    )
    BEGIN
        SELECT * FROM `User`
        WHERE `Username` = uUsername;
    END //
    DELIMITER ;  
    
    -- -- READ-ID
    DELIMITER //
    CREATE PROCEDURE spUser_Read_ID
    (
        IN id INT
    )
    BEGIN
        SELECT * FROM `User`
        WHERE `User_ID` = id;
    END //
    DELIMITER ; 
    
	-- -- INSERT
	DELIMITER //
	create procedure sp_Insert_User 
	(
		IN uUsername	VARCHAR(40),
		IN uPassword	BLOB,
		IN uEmployee_ID INT,
		IN uRole_ID INT
	)
	BEGIN 
	INSERT INTO `User` 
		(`Username`, `Password`, `Employee_ID`, `Role_ID`, `Active`)
	VALUES(uUsername,uPassword, uEmployee_ID, uRole_ID, True);
	END //
	DELIMITER ;
    
    -- -- UPDATE
	DELIMITER //
	create procedure sp_Update_User 
	(
		IN uUser_id	INT,
		IN uUsername	VARCHAR(40),
		IN uEmployee_ID INT,
		IN uRole_ID INT
	)
	BEGIN  

	UPDATE `User` SET 
		Username = uUsername,
		Employee_ID = uEmployee_ID,
		Role_ID = uRole_ID
		
	WHERE User_id = uUser_id;

	END //
	DELIMITER ;
    
    -- -- UPDATE Password
	DELIMITER //
	create procedure sp_Update_User_pwd 
	(
		IN uUser_id	INT,
		IN pwd	VARCHAR(40)
	)
	BEGIN  

	UPDATE `User` SET 
		`Password` = pwd
	WHERE User_id = uUser_id;

	END //
	DELIMITER ;
    
	-- -- Compare Login Details
	DELIMITER //
	create procedure esalon.sp_login_compare 
	(
		IN usrName	VARCHAR(50),
		IN pwd	VARCHAR(40)
	)
	BEGIN  

	SELECT
		u.User_ID as uid, u.Role_ID as rid, e.Name as uname 
	FROM
		User u, Employee e
	WHERE
		u.`Username` = usrName
        AND
        u.`Password` = pwd
        AND
        u.`Active` = true
        AND
        u.`Employee_ID` = e.`Employee_ID`;

	END //
	DELIMITER ;
    
	-- -- Get logged in users details
	DELIMITER //
	create procedure esalon.sp_getUserDetails 
	(
		IN id	INT
	)
	BEGIN  

	SELECT
		u.User_ID as uid, u.Role_ID as rid, e.Name as uname, e.Image as image
	FROM
		User u, Employee e
	WHERE
		u.`User_ID` = id
        AND
        u.`Employee_ID` = e.`Employee_ID`;

	END //
	DELIMITER ;
    
    DELIMITER //
	create procedure esalon.sp_getUserAccess 
	(
		IN rid INT,
        IN major INT,
        IN minor INT
	)
	BEGIN  

	SELECT
		p.Permission_ID
	FROM
		Permission p, Role_Permission rp
	WHERE
		p.Permission_ID = rp.Permission_ID
        AND
        rp.Role_ID = rid
        AND
        p.Major_ID = major
        AND
        p.Minor_ID = minor;

	END //
	DELIMITER ;
    
-- Permissions

	DELIMITER //
	create procedure esalon.sp_getRolePermissions
	(
		IN rid INT
	)
	BEGIN 
		SELECT 
			p.Permission_ID as pid
		FROM
			Permission p, Role_Permission rp
		WHERE
			p.Permission_ID = rp.Permission_ID
			AND
			rp.Role_ID = rid;
	END //
	DELIMITER ;

	DELIMITER //
	create procedure esalon.sp_getPermissions
	(

	)
	BEGIN 
		SELECT 
			p.Permission_ID as pid, 
			mj.`Name` as Major, 
			mj.Major_ID as mjid, 
			mn.`Name` as Minor, 
			mn.Minor_ID as mnid
		FROM
			Permission p, Major mj, Minor mn
		WHERE
			p.Major_ID = mj.Major_ID
			AND
			p.Minor_ID = mn.Minor_ID
		ORDER BY 
			mj.Major_ID, mn.Minor_ID;

	END //
	DELIMITER ;
    
	DELIMITER //
	create procedure esalon.sp_getRoles
	(

	)
	BEGIN 
		SELECT 
			r.Role_ID as rid, r.`Name` as Role, p.Permission_ID as pid
		FROM
			Role r, Role_Permission rp, Permission p, Major mj, Minor mn
		WHERE
			r.Role_ID = rp.Role_ID
			AND
			rp.Permission_ID = p.Permission_ID
			AND
			p.Major_ID = mj.Major_ID
			AND
			p.Minor_ID = mn.Minor_ID;

	END //
	DELIMITER ;
    
    -- -- INSERT
	DELIMITER //
	create procedure spSub_Letter_Payments_Create 
	(
		IN isid	INT,
		IN idate	DATETIME,
		IN iamount DECIMAL(8,2),
		IN ipid INT
	)
	BEGIN 
	INSERT INTO `Sub_Letter_Payment` 
		(`DateTime`, `Amount`, `Sub_Letter_id`, `PaymentMethod_ID`)
	VALUES(idate , iamount, isid, ipid);
	END //
	DELIMITER ;
    
-- Role

	-- -- INSERT
	DELIMITER //
	create procedure sp_Insert_Role 
	(
		IN rname VARCHAR(50)
	)
	BEGIN 
    DECLARE insertId INT;
    
	INSERT INTO `Role` 
		(`Name`)
	VALUES
		(rname);
        
	SET insertId = LAST_INSERT_ID();
			SELECT insertId;
	END //
	DELIMITER ;
    
    
    
    DELIMITER //
	create procedure sp_Update_Role 
	(
		IN rid	INT,
		IN iname VARCHAR(50)
	)
	BEGIN  

	UPDATE `Role` SET 
		`Name` = iname
	WHERE Role_ID = rid;

	END //
	DELIMITER ;
    
    DELIMITER //
	create procedure sp_Delete_RolePermissions
	(
		IN rid	INT
	)
	BEGIN  

	DELETE FROM Role_Permission
	WHERE Role_ID = rid;

	END //
	DELIMITER ;
    
	-- -- INSERT
	DELIMITER //
	create procedure sp_Insert_RolePermission 
	(
		IN rid INT,
        IN pid INT
	)
	BEGIN 
	INSERT INTO `Role_Permission`
	VALUES
		(rid,pid);
	END //
	DELIMITER ;
    