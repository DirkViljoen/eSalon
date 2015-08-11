USE eSalon;

DELIMITER //
CREATE PROCEDURE Sub_Letter_Search
(IN id INT)
BEGIN
  SELECT * FROM `Sub_Letter`
  WHERE sSub_Letter_id = id;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE Sub_Letter_All
()
BEGIN
  SELECT * FROM `Sub_Letter`;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE Sub_Letter_Add
(
	IN sDateTime DATE,
    IN sAmount DECIMAL(8,2),
    IN sEmail VARCHAR(100),
    IN sContactNumber VARCHAR(15)
)   
BEGIN
	INSERT INTO `Sub_Letter`(sDateTime,sAmount,sEmail,sContactNumber,sActive) VALUES(sDateTime,sAmount,sEmail,sContactNumber,TRUE);
END // 
DELIMITER ;
