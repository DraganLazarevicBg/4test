-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

--
-- Set default database
--
USE `4create`;

--
-- Create table `systemlog`
--
CREATE TABLE systemlog (
  id int NOT NULL AUTO_INCREMENT,
  resourceType varchar(200) NOT NULL,
  resourceIdentifier int NOT NULL,
  createdAt datetime NOT NULL,
  event varchar(255) NOT NULL,
  attributes longtext NOT NULL,
  comment varchar(4000) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 4,
AVG_ROW_LENGTH = 2340,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci,
ROW_FORMAT = DYNAMIC;