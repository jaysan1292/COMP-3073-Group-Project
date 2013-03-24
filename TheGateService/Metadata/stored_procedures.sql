-- CUSTOMER ORDER HISTORY

-- gets order data for a given customer

-- CREATE PROCEDURE getOrders @CustomerID BIGINT INPUT AS
-- SELECT  Product.Name AS 'Product', OrderProduct.Quantity, OrderStatus.Name AS 'Status', Shipment.ShipmentDate AS 'Date Shipped', ShipmentMethod.Name AS 'Shipment Method'
-- FROM User LEFT JOIN Orders ON User.UserID = Orders.UserID
-- JOIN OrderStatus ON Orders.OrderStatus = OrderStatus.OrderStatusID
-- JOIN OrderProduct ON Orders.OrderID = OrderProduct.OrderID
-- JOIN Product ON OrderProduct.ProductID = Product.ProductID
-- JOIN Shipment ON Orders.OrderID = Shipment.OrderID
-- JOIN ProductShipment ON Shipment.ShipmentID = ProductShipment.ShipmentID
-- JOIN ShipmentMethod ON Shipment.ShipmentMethod = ShipmentMethod.MethodID
-- WHERE User.UserId = @UserID




		-- SHOPPING CART

-- View data

-- CREATE PROCEDURE viewCart @CustomerID BIGINT INPUT AS
-- SELECT  Product.Name AS 'Product', ShoppingCart.Quantity, Product.Price
-- FROM User LEFT JOIN Orders ON User.UserID = Orders.UserID
-- JOIN OrderStatus ON Orders.OrderStatus = OrderStatus.OrderStatusID
-- JOIN Product ON OrderProduct.ProductID = Product.ProductID
-- WHERE CustomerId = @CustomerID;


-- Update Quantity

-- CREATE PROCEDURE updateCart @Quantity INT INPUT, @CartID BIGINT INPUT AS
-- UPDATE ShoppingCart
-- SET quantity = @Quantity
-- WHERE CartId = @CartID


-- Add Product to Cart

-- CREATE PROCEDURE addCart @Quantity INT INPUT, @ProductID BIGINT INPUT, @CustomerID BIGINT INPUT AS
-- INSERT INTO ShoppingCart(CustomerId, ProductId, Quantity)
-- VALUES (@CustomerID, @ProductID, @Quantity)


-- Remove Product from Cart

-- CREATE PROCEDURE removeCart @CartID BIGINT INPUT AS
-- DELETE FROM ShoppingCart
-- WHERE CartId = @CartID


-- ****FOR LATER**** - Remove product from cart and add information to orders and orderproduct tables




		-- PRODUCT TABLE MANIPULATION

-- Add product

-- CREATE PROCEDURE addProduct @Name VARCHAR(256) INPUT, @Desc TEXT INPUT, @Quantity INT INPUT, @Price REAL INPUT AS
-- INSERT INTO Product (Name, Description, Quantity, Price)
-- VALUES (@Name, @Desc, @Quantity, @Price);


-- Delete product

-- CREATE PROCEDURE deleteProduct @VariableID BIGINT INPUT AS
-- DELETE FROM Product
-- WHERE productID = @VariableID


-- View product by ID

-- CREATE PROCEDURE viewProductById @ProductID BIGINT INPUT AS
-- SELECT Product, Name, Description, Quantity, Price FROM Product
-- WHERE productID = @ProductID;


-- View all products

-- CREATE PROCEDURE viewAllProducts AS
-- SELECT Product, Name, Description, Quantity, Price FROM Product


-- Update Product

-- CREATE PROCEDURE UpdateProduct @ProductID BIGINT INPUT, @Name VARCHAR(256) INPUT, @Desc TEXT INPUT, @Quantity INT INPUT, @Price REAL INPUT AS
-- UPDATE Product
-- SET Name = @Name, Quantity = @Quantity, Description = @Desc, Price = @Price
-- WHERE ProductId = @ProductID



		--USER INFORMATION

-- Get password, FirstName, and LastName

-- CREATE PROCEDURE getUser @Email Varchar(128) INPUT AS
-- SELECT password, FirstName, LastName FROM User
-- WHERE email = @Email;


-- Get user credentials

-- SELECT UserType.Name FROM User
-- JOIN UserType ON User.Type = UserType.TypeID;


-- Add New User

-- CREATE PROCEDURE addUser @FirstName VARCHAR(30) INPUT, @LastName VARCHAR(30) INPUT, @Email VARCHAR(128) INPUT, @Password CHAR(64) INPUT, @Address VARCHAR(256) INPUT AS
-- INSERT INTO User (FirstName, LastName, Address, Email, Password)
-- VALUES (@FirstName, @LastName, @Address, @Email, @Password)


-- Update User

-- CREATE PROCEDURE updateUser @UserID BIGINT INPUT, @FirstName VARCHAR(30) INPUT, @LastName VARCHAR(30) INPUT, @Email VARCHAR(128) INPUT, @Password CHAR(64) INPUT, @Address VARCHAR(256) INPUT AS
-- UPDATE User
-- SET FirstName = @FirstName, LastName = @LastName, Address = @Address, Email = @Email
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