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
    `Password` VARCHAR(50),
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
    `Action` VarChar(300),
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
    `Barcode` VARCHAR (10),
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
    `Duration` INT,
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
    `DateTime` DATE,
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
    `Service_id` INT,
    
    FOREIGN KEY (`Booking_id`)
		REFERENCES `Booking` (`Booking_id`),
	
    FOREIGN KEY (`Service_id`)
		REFERENCES `Service` (`Service_id`)
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