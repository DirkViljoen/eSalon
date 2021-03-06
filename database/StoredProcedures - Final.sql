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
    


    
    