Create Database ADOExample;

Use ADOExample;

Create Table Products
(
	ID int identity (1,1) PRIMARY KEY,
	Name varchar(100) not null,
	Value money not null
);

Create Table Orders
(
	ID int identity (1,1) Primary key,
	Total money not null
);

Create Table ProductsOrders
(
	IDProduct int not null,
	IDOrder int not null,
	Constraint PK_ProductsOrders Primary Key (IDProduct, IDOrder),
	Constraint FK_Products Foreign Key (IDProduct) References Products (ID),
	Constraint FK_Orders Foreign Key (IDOrder) References Orders (ID),
);