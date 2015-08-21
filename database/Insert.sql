USE eSalon;

INSERT INTO `Province`(`Province_id`, `Name`) VALUES(1,"Gauteng");
INSERT INTO `Province`(`Province_id`, `Name`) VALUES(2,"Mpumalanga");
INSERT INTO `Province`(`Province_id`, `Name`) VALUES(3,"Limpopo");

INSERT INTO `City`(`City_id`,`Name`,`Province_id`) VALUES(1,"Pretoria",1);
INSERT INTO `City`(`City_id`,`Name`,`Province_id`) VALUES(2,"Bombela",2);
INSERT INTO `City`(`City_id`,`Name`,`Province_id`) VALUES(3,"Polokwane",3);

INSERT INTO `Surburb`(`Surburb_id`,`Name`,`City_id`) VALUES(1,"Centurion",1);
INSERT INTO `Surburb`(`Surburb_id`,`Name`,`City_id`) VALUES(2,"Hatfield",1);
INSERT INTO `Surburb`(`Surburb_id`,`Name`,`City_id`) VALUES(3,"Riverside Park",1);
INSERT INTO `Surburb`(`Surburb_id`,`Name`,`City_id`) VALUES(4,"Ivy Park",1);

INSERT INTO `Address`(`Address_id`,`Line1`,`Line2`) VALUES(1,"268 West Avenue","Centurion");
INSERT INTO `Address`(`Address_id`,`Line1`,`Line2`) VALUES(2,"553 Grosvenor","Hatfield");
INSERT INTO `Address`(`Address_id`,`Line1`,`Line2`) VALUES(3,"312 Maple street","Ivy Park");

INSERT INTO `Notification_Method`(`NoticationMethod_ID`, `NotificationType` ) VALUES(1, "SMS");
INSERT INTO `Notification_Method`(`NoticationMethod_ID`, `NotificationType` ) VALUES(2,"Email");

INSERT INTO `Client` (`Client_ID`,`Title`,`Name`,`Surname`,`ContactNumber`,`email`,`DateOfBirth`,`Reminders`,`Notifications`,`Active`) VALUES(1,"Ms","Nanda","Nakai","0792271769","nandanakai@hotmail.com","1997-02-14",1,1,True);
INSERT INTO `Client` (`Client_ID`,`Title`,`Name`,`Surname`,`ContactNumber`,`email`,`DateOfBirth`,`Reminders`,`Notifications`,`Active`) VALUES(2,"Miss", "Refiloe","Chaka","0828524512","RefiloeChaka@gmail.com","1994-03-02",2,2,False);
INSERT INTO `Client` (`Client_ID`,`Title`,`Name`,`Surname`,`ContactNumber`,`email`,`DateOfBirth`,`Reminders`,`Notifications`,`Active`) VALUES(3,"Mr","Johan","Roux","0833365913","JohanRoux@gmail.com","1995-08-05",1,1,True);

INSERT INTO `Payment_Method`(`PaymentMethod_ID`,`Name`) VALUES (1,"Cash");
INSERT INTO `Payment_Method`(`PaymentMethod_ID`,`Name`) VALUES (2,"EFT");
INSERT INTO `Payment_Method`(`PaymentMethod_ID`,`Name`) VALUES (3,"Credit");

INSERT INTO `Sub_Letter`(`Sub_Letter_id`,`BusinessName`,`ContactFName`,`ContactLName`,`ContactNumber`,`ContactEmail`,`DateTime`,`Amount`,`Active`) VALUES(1,"Cat's nails","Rose", "Muller","0825673546","RoseMuller@gmail.com","2015-07-01",2500,True);
INSERT INTO `Sub_Letter`(`Sub_Letter_id`,`BusinessName`,`ContactFName`,`ContactLName`,`ContactNumber`,`ContactEmail`,`DateTime`,`Amount`,`Active`) VALUES(2,"A&S waxing", "Amelia","Strydom","0614587598","Amelia205@gmail.com","2015-02-01",2500,True);
INSERT INTO `Sub_Letter`(`Sub_Letter_id`,`BusinessName`,`ContactFName`,`ContactLName`,`ContactNumber`,`ContactEmail`,`DateTime`,`Amount`,`Active`) VALUES(3,"BrandNew","Charlotte", "Brand","0735238967","Charlotte87@gmail.com","2014-01-01",2500,False); 

INSERT INTO `Company_Details`(`Name`,`ContactDetails`,`email`) VALUES("Salon Re-design","072 842 9882","susan_roux@yahoo.com");

INSERT INTO `Employee`(`Name`,`Surname`,`ContactNumber`,`email`,`Salary`,`Active`,`Address_ID`) VALUES("Susan","Kruger","0728429882","susan_roux21@yahoo.com","5000",True, 1);
INSERT INTO `Employee`(`Name`,`Surname`,`ContactNumber`,`email`,`Salary`,`Active`,`Address_ID`) VALUES("Cerese","Bouwer","0824569858","cerese@yahoo.com","5000",True, 1);
INSERT INTO `Employee`(`Name`,`Surname`,`ContactNumber`,`email`,`Salary`,`Active`,`Address_ID`) VALUES("Grieta","Goosen","0824788787","grieta@yahoo.com","5000",True, 1);

INSERT INTO `Role` VALUES (1, "Admin");
INSERT INTO `Role` VALUES (2, "Stylist");

INSERT INTO `User` VALUES (1, "Admin", "Admin", 1, 1, TRUE);
INSERT INTO `User` VALUES (2, "Dirk", "Dirk", 2, 2, TRUE);
INSERT INTO `User` VALUES (3, "Johan", "Johan", 3, 2, TRUE);

INSERT INTO `Supplier` VALUES (1, "John", 08001235674, "maybelline@cosmetics.com", True);
INSERT INTO `Supplier` VALUES (2, "Lizzy" , 0834567653, "tresemme@webmail.co.za", True);
INSERT INTO `Supplier` VALUES (3, "Davis", 0125674323, "Wella@hair.com", False);

INSERT INTO `Hair_Length` VALUES (1, "Long");
INSERT INTO `Hair_Length` VALUES (2, "Medium");  
INSERT INTO `Hair_Length` VALUES (3, "Short"); 

INSERT INTO `Category` (`Category_id`, `Name`) VALUES (1, "Colour");
INSERT INTO `Category` (`Category_id`, `Name`) VALUES (2, "Cut");  
INSERT INTO `Category` (`Category_id`, `Name`) VALUES (3, "Wash"); 

INSERT INTO `Stock` VALUES (1, "Maybelline", "Shampoo", 150.00 , 150, True, 100, "6001234443", 1, 1);
INSERT INTO `Stock` VALUES (2, "Tresemme", "Conditioner", 275.00 , 150, True, 40, "6001233443", 2, 1);
INSERT INTO `Stock` VALUES (3, "Wella", "Hair Dye", 100.00 , 100, False, 10, "6053422344", 3, 2);  

INSERT INTO `Service` VALUES (1, "Hair Cut", "Short Hair Cut for females", 30, 150.00, True);
INSERT INTO `Service` VALUES (2, "Hair Cut", "Medium Hair Cut for females", 35, 170.00, True);
INSERT INTO `Service` VALUES (3, "Hair Treatment", "Treatment for medium male hair", 60, 200.00, True);
    
INSERT INTO `Hair_Length_Service` VALUES (1, 30, 1, 1);
INSERT INTO `Hair_Length_Service` VALUES (2, 45, 2, 1);
INSERT INTO `Hair_Length_Service` VALUES (3, 50, 3, 1); 