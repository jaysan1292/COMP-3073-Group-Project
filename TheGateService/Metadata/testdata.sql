SET autocommit=0;
START TRANSACTION;

-- Drop everything from all tables
DELETE FROM OrderProduct;
DELETE FROM ProductShipment;
DELETE FROM Product;
DELETE FROM Shipment;
DELETE FROM ShipmentMethod;
DELETE FROM Orders;
DELETE FROM OrderStatus;
DELETE FROM OrderType;
DELETE FROM User;
DELETE FROM UserType;

-- Reset auto-increment counters
ALTER TABLE User     AUTO_INCREMENT = 1;
ALTER TABLE Orders   AUTO_INCREMENT = 1;
ALTER TABLE Shipment AUTO_INCREMENT = 1;
ALTER TABLE Product  AUTO_INCREMENT = 1;

-- Insert the actual data

INSERT INTO UserType VALUES
    (1, 'User'),
    (2, 'BasicEmployee'),
    (3, 'Shipping'),
    (4, 'Administrator');

INSERT INTO OrderType VALUES
    (1, 'Point-of-Sale'),
    (2, 'Online');

INSERT INTO OrderStatus VALUES
    (1, 'Pending'),
    (2, 'Shipped'),
    (3, 'Delivered'),
    (4, 'Lost'),
    (5, 'Closed'); -- Completed; used for point-of-sale orders

INSERT INTO ShipmentMethod VALUES
    (1, 'Courier'),
    (2, 'Air Mail'),
    (3, 'Post'),
    (4, 'EMS');

-- Random name generator: http://www.behindthename.com/random/, http://names.igopaygo.com/people
-- Random address generator: http://names.igopaygo.com/street/north_american_address
-- Passwords are '123456' SHA256 hashed
INSERT INTO User (Type,FirstName,LastName,Address,Email,Password) VALUES
    (1, 'Jian',      'Nakano',   NULL,                       'jnakano@example.com',  '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    (1, 'Veda',      'Votaw',    '4586 Dusty Village',       'vvotaw@example.com',   '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    (2, 'Josh',      'Percival', '1432 Rocky Zephyr Corner', 'jperciv@example.com',  '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    (1, 'Daisuke',   'Kato',     NULL,                       'dkato@example.jp',     '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    (2, 'Melissa',   'Owens',    '4287 Silver Circle',       'mowens@example.com',   '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    (1, 'Phuong',    'Nguyen',   '5879 Emerald Arbor',       'phungu@example.com',   '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    (1, 'Marian',    'Kipling',  '24 Fallen Edge',           'mkipling@example.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    (4, 'John',      'Smith',    '123 Addressable Edge',     'jsmith@example.com',   '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'),
    (1, 'EunJung',   'Kang',     '3318 Rustic Way',          'kkangju@example.kr',   '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92');

INSERT INTO Orders (UserId,OrderType,OrderStatus) VALUES
    (1, 1, 5),
    (1, 2, 4),
    (2, 2, 3),
    (8, 1, 5),
    (4, 1, 5),
    (9, 2, 1),
    (7, 1, 5),
    (5, 2, 1),
    (6, 2, 2),
    (4, 1, 5),
    (2, 2, 3),
    (7, 1, 4),
    (3, 2, 2),
    (8, 1, 5),
    (9, 1, 4),
    (1, 1, 1),
    (5, 2, 3);

INSERT INTO Shipment (OrderId,ShipmentMethod,ShipmentDate) VALUES
    (1, 3, '2013-03-23 12:39:00'),
    (2, 4, '2012-12-17 08:14:00'),
    (3, 1, '2013-02-28 04:57:00'),
    (4, 3, '2013-01-12 21:42:00'),
    (5, 4, '2013-03-09 17:18:00'),
    (6, 2, '2012-11-17 07:27:00'),
    (7, 2, '2013-02-18 15:49:00'),
    (8, 2, '2013-01-23 14:28:00');

-- I will improve this later, just shit text data. - James
INSERT INTO Product (Name,Description,Quantity,Price,Featured,Showcase) VALUES
    ('Video Card',            'Video card for PC',                                                               37,    159.99, 1, 1),
    ('MB',                    'Motherboard',                                                                     15,     89.99, 1, 0),
    ('Case',                  'Computer Case',                                                                   44,     59.99, 1, 1),
    ('Monitor',               'Computer Monitor',                                                                1,      99.99, 1, 1),
    ('PS',                    'Power Supply',                                                                    6,      39.99, 0, 0),
    ('KB',                    'Keyboard',                                                                        29,     24.99, 1, 0),
    ('Mouse',                 'Mouse',                                                                           31,     59.99, 1, 0),
    ('64GB RAM',              'High performance DDR9001 RAM module',                                             3,     499.99, 0, 0),
    ('NVidia Something 8008', "It's a friggin' video card, 500MHz.",                                             43,    123.62, 0, 0),
    ('AMD 53243',             '900MHz card, 2 DVI Ports, blah, blah, blah.',                                     4,    1100.81, 0, 0),
    ('9001 GeForce',          "It's over 9000!!!",                                                               12,    100.19, 1, 0),
    ('RAT 75',                "We've run out of ideas so it hovers and has a microwave.",                        9,    5479.11, 0, 0),
    ('435 MB Galactico',      '1333Mhz DDR3 Supported, 16x PCIx ...',                                            1,     549.72, 0, 1),
    ('95" Monitor',           "When 94 just isn't enough. Features a never-before-seen 40960x23040 resolution!", 10,  24400.28, 1, 0),
    ('200 MW PS',             'When you need to run your super computer, 200 MegaWatts are the answer!',         14, 204674.65, 1, 1);


INSERT INTO ProductShipment (ProductId,ShipmentId,Quantity) VALUES
    (1, 1, 5),
    (1, 5, 7),
    (2, 6, 2),
    (3, 3, 1),
    (4, 8, 9),
    (5, 4, 3),
    (6, 7, 4),
    (7, 2, 12);

INSERT INTO OrderProduct (OrderId,ProductId,Quantity) VALUES
    (1, 1, 5),
    (2, 7, 12),
    (3, 3, 1),
    (4, 5, 3),
    (5, 1, 7),
    (6, 2, 2),
    (7, 6, 4),
    (8, 4, 9);

INSERT INTO ShoppingCart(UserId,ProductId,Quantity) VALUES
    (9, 7,  1),
    (9, 6,  1),
    (9, 3,  1),
    (9, 1,  1),
    (8, 10, 1),
    (8, 14, 1),
    (8, 15, 1),
    (6, 8,  1),
    (6, 15, 1),
    (6, 4,  2);

COMMIT;
