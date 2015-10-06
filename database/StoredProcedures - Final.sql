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
        IN iimage BLOB,
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
        IN iimage BLOB,
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