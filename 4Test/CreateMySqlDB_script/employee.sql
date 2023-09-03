-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

--
-- Set default database
--
USE `4create`;

--
-- Create table `employee`
--
CREATE TABLE employee (
  id int NOT NULL AUTO_INCREMENT,
  title enum ('developer', 'manager', 'tester') NOT NULL,
  email varchar(500) NOT NULL,
  createdAt datetime DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 22,
AVG_ROW_LENGTH = 1170,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci,
ROW_FORMAT = DYNAMIC;