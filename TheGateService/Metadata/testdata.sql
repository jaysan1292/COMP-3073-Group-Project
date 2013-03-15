SET autocommit=0;
START TRANSACTION;

-- Drop everything from all tables
DELETE FROM `OrderProduct`;
DELETE FROM `ProductShipment`;
DELETE FROM `Product`;
DELETE FROM `Shipment`;
DELETE FROM `ShipmentMethod`;
DELETE FROM `Orders`;
DELETE FROM `OrderStatus`;
DELETE FROM `OrderType`;
DELETE FROM `Customer`;

-- Reset auto-increment counters
ALTER TABLE `Customer`  AUTO_INCREMENT=0;
ALTER TABLE `Orders`    AUTO_INCREMENT=0;
ALTER TABLE `Shipment`  AUTO_INCREMENT=0;
ALTER TABLE `Product`   AUTO_INCREMENT=0;

-- Insert the actual data

-- Random name generator: http://www.behindthename.com/random/, http://names.igopaygo.com/people
-- Random address generator: http://names.igopaygo.com/street/north_american_address
-- Passwords are '123456' SHA256 hashed
INSERT INTO Customer (FirstName,LastName,Address,Email,Password) VALUES
    ('Jian','Nakano',NULL,'jnakano@example.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    ('Veda','Votaw','4586 Dusty Village','vvotaw@example.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    ('Josh','Percival','1432 Rocky Zephyr Corner',NULL,'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    ('Daisuke','Kato',NULL,'dkato@example.jp','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    ('Melissa','Owens','4287 Silver Circle','mowens@example.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    ('Phượng','Nguyen','5879 Emerald Arbor','phungu@example.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    ('Marian','Kipling','24 Fallen Edge','mkipling@example.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    ('John','Smith','123 Addressable Edge','jsmith@example.com','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    ('EunJung','Kang','3318 Rustic Way','kkangju@example.kr','8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92');

-- TODO: I totally don't remember what was supposed to go in this table
INSERT INTO OrderType VALUES
    (1);

INSERT INTO OrderStatus VALUES
    (1, 'Pending'),
    (2, 'Shipped'),
    (3, 'Delivered'),
    (4, 'Lost');

-- INSERT INTO Orders (CustomerId,OrderType,OrderStatus) VALUES

INSERT INTO ShipmentMethod VALUES
    (1, 'Courier'),
    (2, 'Air Mail'),
    (3, 'Post'),
    (4, 'EMS');

-- INSERT INTO Shipment VALUES

-- TODO: What were we selling again?
-- INSERT INTO Product(Name,Description,Quantity,Price) VALUES

-- INSERT INTO ProductShipment VALUES

-- INSERT INTO OrderProduct VALUES

COMMIT;
