-- CUSTOMER ORDER HISTORY

-- gets order data for a given customer
-- SELECT  Product.Name AS 'Product', OrderProduct.Quantity, OrderStatus.Name AS 'Status', Shipment.ShipmentDate AS 'Date Shipped', ShipmentMethod.Name AS 'Shipment Method'
-- FROM User LEFT JOIN Orders ON User.UserID = Orders.UserID
-- JOIN OrderStatus ON Orders.OrderStatus = OrderStatus.OrderStatusID
-- JOIN OrderProduct ON Orders.OrderID = OrderProduct.OrderID
-- JOIN Product ON OrderProduct.ProductID = Product.ProductID
-- JOIN Shipment ON Orders.OrderID = Shipment.OrderID
-- JOIN ProductShipment ON Shipment.ShipmentID = ProductShipment.ShipmentID
-- JOIN ShipmentMethod ON Shipment.ShipmentMethod = ShipmentMethod.MethodID

-- SHOPPING CART

-- View data
-- SELECT  Product.Name AS 'Product', ShoppingCart.Quantity, Product.Price
-- FROM User LEFT JOIN Orders ON User.UserID = Orders.UserID
-- JOIN OrderStatus ON Orders.OrderStatus = OrderStatus.OrderStatusID
-- JOIN Product ON OrderProduct.ProductID = Product.ProductID
-- WHERE CustomerId = VariableID;

-- Add product

-- Delete product

-- Update Quantity
-- UPDATE 




-- gets thumbnail product information - may add image to DB later
-- SELECT ProductID, Name, Description, Quantity, Price FROM Product;

-- gets all product information by ID - may add image to DB later
-- SELECT Product, Name, Description, Quantity, Price FROM Product
-- WHERE productID = IDVariable;

-- gets password, FirstName, and LastName for a given user
-- SELECT password, FirstName, LastName FROM User
-- WHERE email = emailVariable;

-- gets user credentials
-- SELECT UserType.Name FROM User
-- JOIN UserType ON User.Type = UserType.TypeID;

-- update quantity for a given product
-- UPDATE Product
-- SET quantity = quantityVariable
-- WHERE ProductID = IDVariable;

-- Delete Order if OrderStatus is still 'pending'
-- DELETE * FROM Orders
-- JOIN OrderStatus ON Orders.OrderStatus = OrderStatus.OrderStatusID
-- JOIN OrderType ON Orders.OrderType = OrderType.TypeID
-- WHERE Orderstatus = 1 AND Orders.OrderID = OrderVariable;