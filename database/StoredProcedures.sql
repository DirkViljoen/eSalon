USE eSalon;

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