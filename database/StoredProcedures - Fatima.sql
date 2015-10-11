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
    IN oOrderLine_id  	INT,
	IN oDateReceived  DATE,
	IN oQuantity  INT
)
BEGIN  
	UPDATE `Order` SET 
		DateReceived = oDateReceived
	WHERE Order_id = oOrder_id;

	UPDATE `Order_Line` SET 
		Quantity = oQuantity
	WHERE OrderLine_id = oOrderLine_id;
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
BEGIN  Update `Order`
	SET 
		`Active` = false
	WHERE 
		oOrder_id = oOrder_id;
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
	Quantity = rQuantity
	
WHERE OrderLine_id = rOrderLine_id

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
		rOrderLine_id = rOrderLine_id;
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
		in sname varchar(50),
        in bname varchar(50),
        in pname varchar(50)
    )
    BEGIN
        SELECT * FROM `Stock`
        WHERE `Active` = true;
    END //
    DELIMITER ;

-- -- UPDATE
DELIMITER //
create procedure sp_Update_Stock
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
BEGIN  UPDATE `Stock` SET 
	BrandName = sBrandName,
	ProductName = sProductName,
	Price = sPrice,
	Size = sSize,
	Quantity = sQuantity,
	Barcode = sBarcode,
	Supplier_id = sSupplier_id
	
WHERE sStock_id = sStock_id

;  END //
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
INSERT INTO `Stock` (BrandName, ProductName, Price, Size, 
						Active, Quantity, Barcode, category_id, supplier_id)
VALUES(sBrandName, sProductName, sPrice, sSize, 
						true, sQuantity, sBarcode, cCategory_id, sSupplier_id)

;  END //
DELIMITER ;

-- -- DELETE
DELIMITER //
create procedure sp_Delete_Stock
(
	IN sStock_id   INT
)
BEGIN  Update `stock`
	SET 
		`Active` = false
	WHERE 
		sStock_id = sStock_id;
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
	IN hPriceDateFrom    DATE,
	IN hPriceDateTo    DATE,
	IN sStock_id INT
)
BEGIN  UPDATE `Stock_History` SET 
	Price = hPrice,
	PriceDateFrom = hPriceDateFrom,
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
	IN hPriceDateTo    DATE,
	IN sStock_id INT
)
BEGIN 
INSERT INTO `Stock_History` (Price, PriceDateFrom, PriceDateTo, Stock_id)
VALUES(hPrice, hPriceDateFrom, hPriceDateTo, sStock_id)

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