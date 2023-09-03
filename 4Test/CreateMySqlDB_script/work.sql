-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

--
-- Set default database
--
USE `4create`;

--
-- Create table `work`
--
CREATE TABLE work (
  id int NOT NULL AUTO_INCREMENT,
  comany int NOT NULL,
  employee int NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 13,
AVG_ROW_LENGTH = 1820,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_0900_ai_ci,
ROW_FORMAT = DYNAMIC;

--
-- Create foreign key
--
ALTER TABLE work
ADD CONSTRAINT FK_work_company FOREIGN KEY (comany)
REFERENCES company (id);

--
-- Create foreign key
--
ALTER TABLE work
ADD CONSTRAINT FK_work_employee FOREIGN KEY (employee)
REFERENCES employee (id);