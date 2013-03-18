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

-- If I am unsure of this function by my next run through this, I will remove it. - James
INSERT INTO OrderType VALUES
    (1),
    (2),
    (3),
    (4);

INSERT INTO OrderStatus VALUES
    (1, 'Pending'),
    (2, 'Shipped'),
    (3, 'Delivered'),
    (4, 'Lost');

INSERT INTO Orders (CustomerId,OrderType,OrderStatus) VALUES
    (1,1,2),
    (1,3,4),
    (2,2,3),
    (8,1,2),
    (4,3,3),
    (9,4,1),
    (7,2,1),
    (5,4,1);

INSERT INTO ShipmentMethod VALUES
    (1, 'Courier'),
    (2, 'Air Mail'),
    (3, 'Post'),
    (4, 'EMS');

-- (orderID, shipmentMethod, shipmentDate)
 INSERT INTO Shipment VALUES
     (1,3,23/03/2013 12:39:00),
     (2,4,17/12/2012 08:14:00),
     (3,1,29/02/2013 04:57:00),
     (4,3,12/01/2013 21:42:00),
     (5,4,09/03/2013 17:18:00),
     (6,2,17/11/2012 07:27:00),
     (7,2,18/02/2013 15:49:00),
     (8,2,23/01/2013 14:28:00);


-- I will improve this later, just shit text data. - James

INSERT INTO Product(Name,Description,Quantity,Price) VALUES
    ('Video Card', 'Video card for PC', 37, $159.99),
    ('MB','Motherboard',15,),
    ('Case','Computer Case',44,),
    ('Monitor','Computer Monitor',1,),
    ('PS','Power Supply',6,),
    ('KB','Keyboard',29,),
    ('Mouse','Mouse',31,);

-- (productID, ShipmentID, quantity)
INSERT INTO ProductShipment VALUES
    (1,1,5),
    (7,2,12),
    (3,3,1),
    (5,4,3),
    (1,5,7),
    (2,6,2),
    (6,7,4),
    (4,8,9);

-- (orderId, productID, quantity)
-- INSERT INTO OrderProduct VALUES
    (1,1,5),
    (2,7,12),
    (3,3,1),
    (4,5,3),
    (5,1,7),
    (6,2,2),
    (7,6,4),
    (8,4,9);

COMMIT;
