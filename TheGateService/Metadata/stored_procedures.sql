SET autocommit=0;
START TRANSACTION;

DELIMITER //

-- ----------------------
-- CUSTOMER ORDER HISTORY
-- ----------------------

-- gets order data for a given customer

-- CREATE PROCEDURE getOrders @UserID BIGINT INPUT AS
-- SELECT  Product.Name AS 'Product', OrderProduct.Quantity, OrderStatus.Name AS 'Status', Shipment.ShipmentDate AS 'Date Shipped', ShipmentMethod.Name AS 'Shipment Method'
-- FROM User LEFT JOIN Orders ON User.UserID = Orders.UserID
-- JOIN OrderStatus ON Orders.OrderStatus = OrderStatus.OrderStatusID
-- JOIN OrderProduct ON Orders.OrderID = OrderProduct.OrderID
-- JOIN Product ON OrderProduct.ProductID = Product.ProductID
-- JOIN Shipment ON Orders.OrderID = Shipment.OrderID
-- JOIN ProductShipment ON Shipment.ShipmentID = ProductShipment.ShipmentID
-- JOIN ShipmentMethod ON Shipment.ShipmentMethod = ShipmentMethod.MethodID
-- WHERE User.UserId = @UserID

-- -------------
-- SHOPPING CART
-- -------------

-- View data

-- CREATE PROCEDURE viewCart @UserID BIGINT INPUT AS
-- SELECT  Product.Name AS 'Product', ShoppingCart.Quantity, Product.Price
-- FROM User LEFT JOIN Orders ON User.UserID = Orders.UserID
-- JOIN OrderStatus ON Orders.OrderStatus = OrderStatus.OrderStatusID
-- JOIN Product ON OrderProduct.ProductID = Product.ProductID
-- WHERE UserId = @UserID;


-- Update Quantity

-- CREATE PROCEDURE updateCart @Quantity INT INPUT, @CartID BIGINT INPUT AS
-- UPDATE ShoppingCart
-- SET quantity = @Quantity
-- WHERE CartId = @CartID


-- Add Product to Cart

-- CREATE PROCEDURE addCart @Quantity INT INPUT, @ProductID BIGINT INPUT, @UserID BIGINT INPUT AS
-- INSERT INTO ShoppingCart(UserId, ProductId, Quantity)
-- VALUES (@UserID, @ProductID, @Quantity)


-- Remove Product from Cart

-- CREATE PROCEDURE removeCart @CartID BIGINT INPUT AS
-- DELETE FROM ShoppingCart
-- WHERE CartId = @CartID


-- ****FOR LATER**** - Remove product from cart and add information to orders and orderproduct tables

-- --------------------------
-- PRODUCT TABLE MANIPULATION
-- --------------------------

-- Add product
DROP PROCEDURE IF EXISTS AddProduct;
CREATE PROCEDURE AddProduct (IN ProdName VARCHAR(256), IN ProdDesc TEXT,
                             IN ProdQuantity INT, IN ProdPrice REAL,
                             IN ProdFeatured BOOLEAN, IN ProdShowcase BOOLEAN,
                             OUT NewId BIGINT)
BEGIN
    INSERT INTO Product (Name, Description, Quantity, Price, Featured, Showcase) VALUES
        (ProdName, ProdDesc, ProdQuantity, ProdPrice, ProdFeatured, ProdShowcase);
    SET NewId = LAST_INSERT_ID();
END //

-- Delete product
DROP PROCEDURE IF EXISTS DeleteProduct;
CREATE PROCEDURE DeleteProduct (ProdId BIGINT)
BEGIN
    DELETE FROM Product
    WHERE ProductId = ProdId;
END //

-- Get product by ID
DROP PROCEDURE IF EXISTS GetProductById;
CREATE PROCEDURE GetProductById (ProdID BIGINT)
BEGIN
    SELECT ProductId, Name, Description, Quantity, Price, Featured, Showcase
    FROM Product
    WHERE ProductId = ProdID;
END //

-- Get all products
DROP PROCEDURE IF EXISTS GetAllProducts;
CREATE PROCEDURE GetAllProducts ()
BEGIN
    SELECT ProductId, Name, Description, Quantity, Price, Featured, Showcase
    FROM Product;
END //

-- Get featured products
DROP PROCEDURE IF EXISTS GetFeaturedProducts;
CREATE PROCEDURE GetFeaturedProducts ()
BEGIN
    SELECT ProductId, Name, Description, Quantity, Price, Featured, Showcase
    FROM Product
    WHERE Featured = 1;
END //

-- Get showcase products
DROP PROCEDURE IF EXISTS GetShowcaseProducts;
CREATE PROCEDURE GetShowcaseProducts ()
BEGIN
    SELECT ProductId, Name, Description, Quantity, Price, Featured, Showcase
    FROM Product
    WHERE Showcase = 1;
END //

-- Update Product
DROP PROCEDURE IF EXISTS UpdateProduct;
CREATE PROCEDURE UpdateProduct(IN ProdId BIGINT, IN ProdName VARCHAR(256),
                               IN ProdDesc TEXT, IN ProdQuantity INT, IN ProdPrice REAL,
                               IN ProdFeatured BOOLEAN, IN ProdShowcase BOOLEAN)
BEGIN
    UPDATE Product
    SET
        Name        = ProdName,
        Quantity    = ProdQuantity,
        Description = ProdDesc,
        Price       = ProdPrice,
        Featured    = ProdFeatured,
        Showcase    = ProdShowcase
    WHERE
        ProductId = ProdId;
END //

-- ----------------
-- USER INFORMATION
-- ----------------

-- Get User info
DROP PROCEDURE IF EXISTS GetUserInfo;
CREATE PROCEDURE GetUserInfo (IN UserId BIGINT)
BEGIN
    SELECT UserId, FirstName, LastName, Address, Email, Type
    FROM User
    WHERE User.UserId = UserId;
END //

-- Get password, FirstName, and LastName

DROP PROCEDURE IF EXISTS GetUserPassword;
CREATE PROCEDURE GetUserPassword (IN Email VARCHAR(128))
BEGIN
    SELECT Password
    FROM User
    WHERE User.Email = Email;
END //

-- Get user credentials

-- SELECT UserType.Name FROM User
-- JOIN UserType ON User.Type = UserType.TypeID;


-- Add New User

-- CREATE PROCEDURE addUser @Type INT INPUT, @FirstName VARCHAR(30) INPUT, @LastName VARCHAR(30) INPUT, @Email VARCHAR(128) INPUT, @Password CHAR(64) INPUT, @Address VARCHAR(256) INPUT AS
-- INSERT INTO User (Type, FirstName, LastName, Address, Email, Password)
-- VALUES (@Type, @FirstName, @LastName, @Address, @Email, @Password)


-- Update User

-- CREATE PROCEDURE updateUser @Type INT INPUT, @UserID BIGINT INPUT, @FirstName VARCHAR(30) INPUT, @LastName VARCHAR(30) INPUT, @Email VARCHAR(128) INPUT, @Password CHAR(64) INPUT, @Address VARCHAR(256) INPUT AS
-- UPDATE User
-- SET Type = @Type, FirstName = @FirstName, LastName = @LastName, Address = @Address, Email = @Email
-- WHERE UserId = @UserID


-- Remove User

-- CREATE PROCEDURE removeUser @UserID BIGINT INPUT AS
-- DELETE FROM User
-- WHERE UserId = @UserID



-- Delete Order if OrderStatus is still 'pending'

-- DELETE * FROM Orders
-- JOIN OrderStatus ON Orders.OrderStatus = OrderStatus.OrderStatusID
-- JOIN OrderType ON Orders.OrderType = OrderType.TypeID
-- WHERE Orderstatus = 1 AND Orders.OrderID = OrderVariable;

DELIMITER ;

COMMIT;
