USE eSalon;

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