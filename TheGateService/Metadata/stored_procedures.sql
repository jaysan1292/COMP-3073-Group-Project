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
DROP PROCEDURE IF EXISTS GetCart //
CREATE PROCEDURE GetCart (IN UserId BIGINT)
BEGIN
    SELECT
        Product.ProductId,
        Product.Name,
        Product.Description,
        Product.Price,
        ShoppingCart.Quantity
    FROM
        ShoppingCart INNER JOIN Product ON ShoppingCart.ProductId = Product.ProductId
    WHERE
        ShoppingCart.UserId = UserId;
END //

-- Update Quantity
DROP PROCEDURE IF EXISTS UpdateCart //
CREATE PROCEDURE UpdateCart (IN UserId BIGINT, IN ProductId BIGINT, IN NewQuantity INT)
BEGIN
    DECLARE Exist INT;

    -- Check if the product already exists in this user's shopping cart
    SELECT COUNT(*) INTO Exist FROM ShoppingCart s WHERE s.UserId = UserId AND s.ProductId = ProductId;

    IF Exist = 0 THEN
        -- If the product doesn't exist in the user's cart, add it
        CALL AddToCart(UserId, ProductId, NewQuantity);
    ELSEIF NewQuantity = 0 THEN
        -- If the new quantity is 0, just remove it from the shopping cart.
        CALL RemoveFromCart(UserId, ProductId);
    ELSE
        -- Just update the existing product in the cart
        UPDATE ShoppingCart
        SET Quantity = NewQuantity
        WHERE ShoppingCart.UserId = UserId AND ShoppingCart.ProductId = ProductId;
    end IF;
END //

-- Add Product to Cart
DROP PROCEDURE IF EXISTS AddToCart //
CREATE PROCEDURE AddToCart (IN UserId BIGINT, IN ProductId BIGINT, IN Quantity INT)
BEGIN
    DECLARE Exist INT;
    DECLARE NewQuantity INT;

    -- Check if the product already exists in this user's shopping cart
    SELECT COUNT(*) INTO Exist FROM ShoppingCart s WHERE s.UserId = UserId AND s.ProductId = ProductId;

    IF Exist = 0 THEN
        -- This product doesn't yet exist in the user's shopping cart.
        -- But first, check if we're putting in a negative quantity, and if so, don't do anything.
        IF Quantity > 0 THEN
            INSERT INTO ShoppingCart VALUES (UserId, ProductId, Quantity);
        END IF;
    ELSE
        -- This product already exists in the user's shopping cart.
        -- Check how many of this item will be in the shopping cart after updating (Yes, quantity can be negative)
        -- If it is less than zero, remove the item from the cart, otherwise update the quantity
        SELECT s.Quantity INTO NewQuantity FROM ShoppingCart s WHERE s.UserId = UserId AND s.ProductId = ProductId;

        -- Right now, NewQuantity == the current quantity of this product currently in the cart.
        -- Increment it by the amount to add.
        SET NewQuantity = NewQuantity + Quantity;

        IF NewQuantity <= 0 THEN
            CALL RemoveFromCart(UserId, ProductId);
        ELSE
            UPDATE ShoppingCart s
            SET s.Quantity = NewQuantity
            WHERE s.UserId = UserId AND s.ProductId = ProductId;
        END IF;
    END IF;
END //

-- Remove Product from Cart
DROP PROCEDURE IF EXISTS RemoveFromCart //
CREATE PROCEDURE RemoveFromCart (IN UserId BIGINT, IN ProductId BIGINT)
BEGIN
    DELETE FROM ShoppingCart WHERE ShoppingCart.UserId = UserId AND ShoppingCart.ProductId = ProductId;
END //

-- Get the number of items in the shopping cart
DROP PROCEDURE IF EXISTS GetCartItemCount //
CREATE PROCEDURE GetCartItemCount (IN UserId BIGINT)
BEGIN
    SELECT COUNT(*) AS ItemsInCart
    FROM ShoppingCart
    WHERE ShoppingCart.UserId = UserId;
END //

-- ****FOR LATER**** - Remove product from cart and add information to orders and orderproduct tables

-- --------------------------
-- PRODUCT TABLE MANIPULATION
-- --------------------------

-- Add product
DROP PROCEDURE IF EXISTS AddProduct //
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
DROP PROCEDURE IF EXISTS DeleteProduct //
CREATE PROCEDURE DeleteProduct (ProdId BIGINT)
BEGIN
    DELETE FROM Product
    WHERE ProductId = ProdId;
END //

-- Get product by ID
DROP PROCEDURE IF EXISTS GetProductById //
CREATE PROCEDURE GetProductById (ProdID BIGINT)
BEGIN
    SELECT ProductId, Name, Description, Quantity, Price, Featured, Showcase
    FROM Product
    WHERE ProductId = ProdID;
END //

-- Get all products
DROP PROCEDURE IF EXISTS GetAllProducts //
CREATE PROCEDURE GetAllProducts ()
BEGIN
    SELECT ProductId, Name, Description, Quantity, Price, Featured, Showcase
    FROM Product;
END //

-- Get featured products
DROP PROCEDURE IF EXISTS GetFeaturedProducts //
CREATE PROCEDURE GetFeaturedProducts ()
BEGIN
    SELECT ProductId, Name, Description, Quantity, Price, Featured, Showcase
    FROM Product
    WHERE Featured = 1;
END //

-- Get showcase products
DROP PROCEDURE IF EXISTS GetShowcaseProducts //
CREATE PROCEDURE GetShowcaseProducts ()
BEGIN
    SELECT ProductId, Name, Description, Quantity, Price, Featured, Showcase
    FROM Product
    WHERE Showcase = 1;
END //

-- Update Product
DROP PROCEDURE IF EXISTS UpdateProduct //
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

-- Typeahead Search
DROP PROCEDURE IF EXISTS TypeaheadSearch //
CREATE PROCEDURE TypeaheadSearch(IN Query TEXT)
BEGIN
    SELECT *
    FROM Product
    WHERE Name LIKE CONCAT('%', Query, '%');
END //

-- ----------------
-- USER INFORMATION
-- ----------------

-- Get User info
DROP PROCEDURE IF EXISTS GetUserInfo //
CREATE PROCEDURE GetUserInfo (IN UserId BIGINT)
BEGIN
    SELECT UserId, FirstName, LastName, Address, Email, Type
    FROM User
    WHERE User.UserId = UserId;
END //

-- Get password, FirstName, and LastName

DROP PROCEDURE IF EXISTS GetUserPassword //
CREATE PROCEDURE GetUserPassword (IN Email VARCHAR(128))
BEGIN
    SELECT UserId, Password
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
