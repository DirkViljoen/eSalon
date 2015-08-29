USE eSalon;

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

INSERT INTO `Client` VALUES(1,"Ms","Nanda","Nakai","079 227 1769","nandanakai@hotmail.com","1997-02-14",1,1,True,1,3);
INSERT INTO `Client` VALUES(2,"Miss", "Refiloe","Chaka","0828524512","RefiloeChaka@gmail.com","1994-03-02",1,0,False,2,1);
INSERT INTO `Client` VALUES(3,"Mr","Johan","Roux","0833365913","JohanRoux@gmail.com","1995-08-05",1,1,True,1,2);

INSERT INTO `Sub_Letter` VALUES(1,"Cat's nails","Rose", "Muller","0825673546","RoseMuller@gmail.com","2015-07-01",2500,True);
INSERT INTO `Sub_Letter` VALUES(2,"A&S waxing", "Amelia","Strydom","0614587598","Amelia205@gmail.com","2015-02-01",2500,True);
INSERT INTO `Sub_Letter` VALUES(3,"BrandNew","Charlotte", "Brand","0735238967","Charlotte87@gmail.com","2014-01-01",2500,False); 

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

INSERT INTO `Category` (`Category_id`, `Name`) VALUES (1, "Colour");
INSERT INTO `Category` (`Category_id`, `Name`) VALUES (2, "Cut");  
INSERT INTO `Category` (`Category_id`, `Name`) VALUES (3, "Wash"); 

INSERT INTO `Stock` VALUES (1, "Maybelline", "Shampoo", 150.00 , 150, True, 100, "6001234443", 1, 1);
INSERT INTO `Stock` VALUES (2, "Tresemme", "Conditioner", 275.00 , 150, True, 40, "6001233443", 2, 1);
INSERT INTO `Stock` VALUES (3, "Wella", "Hair Dye", 100.00 , 100, False, 10, "6053422344", 3, 2);  

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

