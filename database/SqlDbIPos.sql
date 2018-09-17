--USE master;
--ALTER DATABASE [IPOS] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--DROP DATABASE [IPOS] ;
--Create  database [IPOS];
Use IPOS;

--Create table
---Product
DROP TABLE IF EXISTS Products;
create table Products(
	ID bigint,
	[Name] nvarchar(200),
	Product_Category_ID int,
	Base_Product_Code varchar(50),
	Min_Quota int,
	Max_Quota int,
	constraint Products_P primary key(ID)
);
---Product_Group: Nhóm sản phẩm (1 sản phẩm được tạo thành từ nhiều loại sp khác)

DROP TABLE IF EXISTS Product_Categories;
create table Product_Categories(
	ID bigint,
	[Name] nvarchar(200),
	Parent_ID int
	constraint Product_Group_P primary key(ID)	
);
---Product_Image
DROP TABLE IF EXISTS Product_Image;
create table Product_Image(
	 ID bigint,
	 Product_ID bigint,
	 Contents text,
	 Number int,
	 constraint Product_Image_P primary key(ID)
);
---Product_Unit
DROP TABLE IF EXISTS Product_Unit;
Create table Product_Unit(
	Product_Code varchar(50),
	Product_ID bigint,
	[Name] nvarchar(200),
	Sell_Price decimal(18,0),
	Original_Price decimal(18,0),
	Base_Product_Code varchar(50),
	QuantityPerUnit int,
	constraint Product_Unit_P primary key(Product_Code)
);
---Transaction_Detail
DROP TABLE IF EXISTS Transaction_Detail;
create table Transaction_Detail(
	Transaction_Code varchar(50),
	Product_Code varchar(50),
	Quantity int,
	Discount decimal(10,0),
	Price decimal(18,0),
	Total decimal(18,0),
	constraint Transaction_Detail_P primary key(Transaction_Code,Product_Code)
);
---Transaction_Type_Group
DROP TABLE IF EXISTS Transaction_Type_Group;
create table Transaction_Type_Group(
	ID bigint,
	Type varchar(50),
	[Name] nvarchar(200),
	constraint Transaction_Type_Group_P primary key(ID)
);
---Transaction
----Transaction type: B-Bill, BR- Bill return, M Merchandise, MR - Merchandise return, D - Destroy, P - Payment
DROP TABLE IF EXISTS [Transaction];
create table [Transaction](
	code varchar(50),
	[Type] varchar(50),
	Type_Group_ID int,
	Created_Time date,
	Created_User varchar(50),
	Partner_ID bigint,
	Shop_ID bigint,
	Discount decimal(18,0),
	Note nvarchar(500),
	constraint Transaction_P primary key(Code)
);
---Transaction_Payment
----Payment type: M - Cash, C - Card, D - Dept
DROP TABLE IF EXISTS Transaction_Payment;
create table Transaction_Payment(
	ID bigint,
	Transaction_Code varchar(50),
	[Type] varchar(50),
	Amount decimal(18)
	constraint Transaction_Payment_P primary key(ID)
);
---Shop
----Partner type: C - Customer, S - Supplier
DROP TABLE IF EXISTS Shop;
create table Shop(
	ID bigint,
	[Name] nvarchar(200),
	Phone varchar(50),
	Email varchar(50),
	[Address] nvarchar(200),
	[Status] varchar(1),
	constraint Shop_P primary key(ID)
);
---Partner
DROP TABLE IF EXISTS [Partner];
create table [Partner](
	ID bigint,
	[Name] nvarchar(200),
	Phone varchar(50),
	Email varchar(50),
	[Address] nvarchar(200),
	Created_Time date,
	Created_User nvarchar(50),
	[Type] varchar(1),
	constraint Partner_P primary key(ID)
);
---Users
DROP TABLE IF EXISTS Users;
create table Users(
	Account varchar(50),
	[Password] varchar(100),
	[Name] nvarchar(200),
	Phone varchar(50),
	Email varchar(50),
	[Address] nvarchar(200),
	Birthday int,
	Role_ID bigint,
	Login_Time_Retrict bit,
	[Status] varchar(1),
	constraint Users_P primary key(Account)
);
---Role
DROP TABLE IF EXISTS [Role];
create table [Role](
	ID bigint,
	[Name] nvarchar(200),
	constraint Role_P primary key(ID)
);
---User_Permission
DROP TABLE IF EXISTS User_Permission;
create table User_Permission(
	User_Account varchar(50),
	Shop_ID bigint,
	Permission_Code varchar(50),
	Has_Permission bit,
	constraint User_Permission_P primary key(User_Account,Shop_ID,Permission_Code)
);
---Role_Permission
DROP TABLE IF EXISTS Role_Permission;
create table Role_Permission(
	Role_ID bigint,
	Permission_Code varchar(50),
	Has_Permission bit,
	constraint Role_Permission_P primary key(Role_ID,Permission_Code)
);
---Permission
DROP TABLE IF EXISTS Permission;
create table Permission(
	Code varchar(50),
	[Name] nvarchar(50),
	Parent_Code varchar(50),
	constraint Permission_P primary key(Code)
);
