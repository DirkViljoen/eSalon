USE eSalon;

DELIMITER //
CREATE PROCEDURE Sub_Letter_Search
(IN bName VARCHAR(50))
BEGIN
  SELECT * FROM `Sub_Letter`
  WHERE `sBusinessName` LIKE bName
   AND `sActive` = true;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE Sub_Letter_get
(IN id INT)
BEGIN
  SELECT * FROM `Sub_Letter`
  WHERE `sSub_Letter_id` = id
   AND `sActive` = true
  ORDER BY `sBusinessName`;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE Sub_Letter_All
()
BEGIN
  SELECT * 
  FROM `Sub_Letter`
  WHERE `sActive` = true
  ORDER BY `sBusinessName`;
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
			sBusinessName,
			sContactFName,
			sContactLName,
			sContactNumber,
			sContactEmail,
			sDateTime,
			sAmount,
			sActive) 
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
			sBusinessName = sBName,
			sContactFName = sCFName,
			sContactLName = sCLName,
			sContactNumber = sCNumber,
			sContactEmail = sCEmail,
			sDateTime = sDateTime,
			sAmount = sAmount,
			sActive = true
		WHERE
			sSub_Letter_id = sID;
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
			sActive = false
		WHERE
			sSub_Letter_id = sID;
END // 
DELIMITER ;