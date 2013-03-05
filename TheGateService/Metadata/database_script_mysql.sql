CREATE DATABASE IF NOT EXISTS `thegate`;

USE `thegate`;

SET autocommit=0;
START TRANSACTION;

DROP TABLE IF EXISTS `order_product`;
DROP TABLE IF EXISTS `product_shipment`;
DROP TABLE IF EXISTS `product`;
DROP TABLE IF EXISTS `shipment`;
DROP TABLE IF EXISTS `shipment_method`;
DROP TABLE IF EXISTS `orders`;
DROP TABLE IF EXISTS `order_status`;
DROP TABLE IF EXISTS `order_type`;
DROP TABLE IF EXISTS `customer`;

CREATE TABLE `customer`(
customer_id BIGINT PRIMARY KEY AUTO_INCREMENT,
first_name VARCHAR(30) NOT NULL,
last_name VARCHAR(30) NOT NULL,
address VARCHAR(256),
email varchar(128),
password CHAR(64)
);

CREATE TABLE `order_type`(
type_id INT PRIMARY KEY NOT NULL,
name VARCHAR(64) NOT NULL
);

CREATE TABLE `order_status`(
orderstatus_id INT PRIMARY KEY NOT NULL,
name VARCHAR(64) NOT NULL
);

CREATE TABLE `orders`(
order_id BIGINT PRIMARY KEY NOT NULL,
customer_id BIGINT NOT NULL,
order_type INT NOT NULL,
order_status INT NOT NULL,
FOREIGN KEY (customer_id) REFERENCES `customer`(customer_id),
FOREIGN KEY (order_status) REFERENCES `order_status`(orderstatus_id),
FOREIGN KEY (order_type) REFERENCES `order_type`(type_id)
);

CREATE TABLE `shipment_method`(
method_id INT PRIMARY KEY NOT NULL,
name VARCHAR(64)
);

CREATE TABLE `shipment`(
shipment_id BIGINT PRIMARY KEY NOT NULL,
order_id BIGINT NOT NULL,
shipment_method INT NOT NULL,
shipment_date DATETIME NOT NULL,
FOREIGN KEY (order_id) REFERENCES `orders`(order_id),
FOREIGN KEY (shipment_method) REFERENCES `shipment_method`(method_id)
);

CREATE TABLE `product`(
product_id BIGINT PRIMARY KEY NOT NULL,
name VARCHAR(256) NOT NULL,
description TEXT,
quantity INT,
price REAL
);

CREATE TABLE `product_shipment`(
product_id BIGINT NOT NULL,
shipment_id BIGINT NOT NULL,
quantity INT,
PRIMARY KEY (product_id, shipment_id),
FOREIGN KEY (product_id) REFERENCES `product`(product_id),
FOREIGN KEY (shipment_id) REFERENCES `shipment`(shipment_id)
);

CREATE TABLE `order_product`(
order_id BIGINT NOT NULL,
product_id BIGINT NOT NULL,
quantity INT,
PRIMARY KEY (order_id, product_id),
FOREIGN KEY (order_id) REFERENCES `orders`(order_id),
FOREIGN KEY (product_id) REFERENCES `product`(product_id)
);

COMMIT;
