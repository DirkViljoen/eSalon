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