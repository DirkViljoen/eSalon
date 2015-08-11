-- Deletes the database if it exists

DROP DATABASE IF EXISTS eSalon;



-- CREATING



CREATE DATABASE eSalon;



USE eSalon;

-- -Green part---


CREATE TABLE `Province`(
         pProvince_id INT NULL AUTO_INCREMENT,
         PName VARCHAR(50),
         
         PRIMARY KEY(pProvince_id)
);

INSERT INTO `Province`(pProvince_id,pName) VALUES(1,"Gauteng");
INSERT INTO `Province`(pProvince_id,pName) VALUES(2,"Mpumalanga");
INSERT INTO `Province`(pProvince_id,pName) VALUES(3,"Limpopo");

-------
CREATE TABLE `City`(
         cCity_id INT NULL AUTO_INCREMENT,
         cName VARCHAR(50),

         pProvince_id INT,
         
         PRIMARY KEY(cCity_id),
  
         FOREIGN KEY(pProvince_id)
         REFERENCES `Province`(pProvince_id)
);

INSERT INTO `City`(cCity_id,cName) VALUES(1,"Pretoria");
INSERT INTO `City`(cCity_id,cName) VALUES(2,"Bombela");
INSERT INTO `City`(cCity_id,cName) VALUES(3,"Polokwane");

-------
CREATE TABLE `Surburb`(
         sSurburb_id INT NULL AUTO_INCREMENT,
         sName VARCHAR(50),
         
         cCity_id INT,

         PRIMARY KEY(sSurburb_id),

         FOREIGN KEY( cCity_id)
         REFERENCES `City`( cCity_id)
);

INSERT INTO `Surburb`(sSurburb_id,sName) VALUES(1,"Centurion");
INSERT INTO `Surburb`(sSurburb_id,sName) VALUES(2,"Hatfield");
INSERT INTO `Surburb`(sSurburb_id,sName) VALUES(3,"Riverside Park");
INSERT INTO `Surburb`(sSurburb_id,sName) VALUES(4,"Ivy Park");

------
CREATE TABLE `Address`(
         aAddress_id INT NULL AUTO_INCREMENT,
         aLine1 VARCHAR(50),
         aLine2 VARCHAR(50),
         
         sSurburb_id INT,

         PRIMARY KEY(aAddress_id),
         
         FOREIGN KEY(sSurburb_id)
         REFERENCES `Surburb`(sSurburb_id)
);

INSERT INTO `Address`(aAddress_id,aLine1,aLine2) VALUES(1,"268 West Avenue","Centurion");
INSERT INTO `Address`(aAddress_id,aLine1,aLine2) VALUES(2,"553 Grosvenor","Hatfield");
INSERT INTO `Address`(aAddress_id,aLine1,aLine2) VALUES(3,"312 Maple street","Ivy Park");

------
CREATE TABLE `Notification_Method`(
         nNotication_Method_id INT NULL AUTO_INCREMENT,
         nNotificationType VARCHAR(10),

         PRIMARY KEY( nNotication_Method_id)
);

INSERT INTO `Notification_Method`(nNotication_Method_id, nNotificationType ) VALUES(1, "SMS");
INSERT INTO `Notification_Method`(nNotication_Method_id, nNotificationType ) VALUES(2,"Email");

-- -
CREATE TABLE `Client`(
          cClient_id INT NULL AUTO_INCREMENT,
          cTitle VARCHAR(5),
          cName VARCHAR(50),
          cSurname VARCHAR(50),
          cContactNumber VARCHAR(15),
          cEmail VARCHAR(100),
          cDateOfBirth DATE,
          cReminders BOOLEAN,
          cNotifications BOOLEAN,
          cActive BOOLEAN,

          nNotication_Method_id INT,
          aAddress_id INT,

          PRIMARY KEY(cClient_id ),

          FOREIGN KEY(nNotication_Method_id )
          REFERENCES `Notification_Method`(nNotication_Method_id ),

          
          FOREIGN KEY(aAddress_id)
          REFERENCES `Address`(aAddress_id)
);

INSERT INTO `Client` (cClient_id,cTitle,cName,cSurname,cContactNumber,cEmail,cDateOfBirth,cReminders,cNotifications,cActive) VALUES(1,"Ms","Nanda","Nakai","0792271769","nandanakai@hotmail.com","1997-02-14",1,1,True);
INSERT INTO `Client` (cClient_id,cTitle,cName,cSurname,cContactNumber,cEmail,cDateOfBirth,cReminders,cNotifications,cActive) VALUES(2,"Miss", "Refiloe","Chaka","0828524512","RefiloeChaka@gmail.com","1994-03-02",2,2,False);
INSERT INTO `Client` (cClient_id,cTitle,cName,cSurname,cContactNumber,cEmail,cDateOfBirth,cReminders,cNotifications,cActive) VALUES(3,"Mr","Johan","Roux","0833365913","JohanRoux@gmail.com","1995-08-05",1,1,True);
--------
CREATE TABLE`Expense_Category`(
          eExpense_Category_id INT NULL AUTO_INCREMENT,
          eName VARCHAR(50),
          
          PRIMARY KEY(eExpense_Category_id )
);
--------
CREATE TABLE `Payment_Method`(
          pPayment_Method_id INT NULL AUTO_INCREMENT,
          pName VARCHAR(50),

         PRIMARY KEY(pPayment_Method_id)
);
INSERT INTO `Payment_Method`(pPayment_Method_id,pName) VALUES (1,"Cash");
INSERT INTO `Payment_Method`(pPayment_Method_id,pName) VALUES (2,"EFT");
INSERT INTO `Payment_Method`(pPayment_Method_id,pName) VALUES (3,"Credit");

----
CREATE TABLE `Expense`(
          eExpense_id INT NULL AUTO_INCREMENT,
          eName VARCHAR(50),
          eQuantity INT,
          ePricePerItem DECIMAL(8,2),

          
          eExpense_Category_id INT,
          pPayment_Method_id INT,

          PRIMARY KEY(eExpense_id),

          FOREIGN KEY(eExpense_Category_id)
          REFERENCES `Expense_Category`(eExpense_Category_id),

          
          FOREIGN KEY(pPayment_Method_id)
          REFERENCES `Payment_Method`(pPayment_Method_id)
);
--------
CREATE TABLE `Sub_Letter`(
          sSub_Letter_id INT NULL AUTO_INCREMENT,
          sDateTime DATE,
          sAmount VARCHAR(50),
          sEmail  VARCHAR(100),
          sContactNumber VARCHAR(15),
          sActive BOOLEAN,

          PRIMARY KEY(sSub_Letter_id)
);

INSERT INTO `Sub_Letter`(sSub_Letter_id,sDateTime,sAmount,sEmail,sContactNumber,sActive) VALUES(1,"2015-07-01",2500,"RoseMuller@gmail.com","0825673546",True);
INSERT INTO `Sub_Letter`(sSub_Letter_id,sDateTime,sAmount,sEmail,sContactNumber,sActive) VALUES(2,"2015-02-01",2500,"Amelia205@gmail.com","0614587598",False);
INSERT INTO `Sub_Letter`(sSub_Letter_id,sDateTime,sAmount,sEmail,sContactNumber,sActive) VALUES(3,"2014-01-01",2500,"Charlotte87@gmail.com","0735238967",False); 

------
CREATE TABLE `Sub_Letter_Payment`(
          sSub_Letter_Payment_id INT NULL AUTO_INCREMENT, 
          sDateTime DATE,
          sAmount DECIMAL(8,2),
               
          sSub_Letter_id INT,
          pPayment_Method_id INT,

          PRIMARY KEY (sSub_Letter_Payment_id),

          FOREIGN KEY(  sSub_Letter_id)
          REFERENCES `Sub_Letter`(  sSub_Letter_id),

          FOREIGN KEY(pPayment_Method_id)
          REFERENCES `Payment_Method`(pPayment_Method_id)
);

------
CREATE TABLE `Company_Details`(
          cCompany_Details_id INT NULL AUTO_INCREMENT,
          cName VARCHAR(50),
          cContact_Details VARCHAR(15),
          cEmail VARCHAR(100),

          PRIMARY KEY(  cCompany_Details_id)
);
INSERT INTO `Company_Details`(cName,cContact_Details,cEmail) VALUES("Salon Re-design","072 842 9882","susan_roux@yahoo.com");

CREATE TABLE `Employee` (
	Employee_ID INT NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(50),
    `Surname` VARCHAR(50),
    `Contact Number` VARCHAR(15),
    `email`VARCHAR(50),
    `Salary` DECIMAL(8,2),
    `Active` BOOLEAN,
    `Address_ID` INT,
    
    PRIMARY KEY(Employee_ID),
    
    FOREIGN KEY(`Address_ID`)
          REFERENCES `Address`(`aAddress_id`)
    
)