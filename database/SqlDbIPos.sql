--Drop database [IPOS];
Create  database [IPOS];
Use IPOS;

--Create table
---Product
create table Products(
	Code varchar(50),
	[Name] nvarchar(200),
	Product_Category_ID int,
	Base_Unit_ID int,
	constraint Products_P primary key(Code)
);
---Product_Group: Nhóm sản phẩm (1 sản phẩm được tạo thành từ nhiều loại sp khác)
create table Product_Categories(
	ID int,
	[Name] nvarchar(200),
	Parent_ID int
	constraint Product_Group_P primary key(ID)	
);
---Product_Image
create table Product_Image(
	 ID int,
	 Product_Code varchar(50),
	 Contents text,
	 Number int,
	 constraint Product_Image_P primary key(ID)
);
---Product_Unit
Create table Product_Unit(
	ID int,
	Product_Code varchar(50),
	[Name] nvarchar(200),
	Sell_Price decimal(18,0),
	Original_Price decimal(18,0),
	Base_Unit_ID int,
	Number int,
	constraint Product_Unit_P primary key(ID)
);
---Transaction_Detail
create table Transaction_Detail(
	Transaction_Code varchar(50),
	Product_Unit_ID int,
	Number int,
	Price decimal(18,0),
	Total decimal(18,0),
	constraint Transaction_Detail_P primary key(Transaction_Code,Product_Unit_ID)
);
---Transaction_Type_Group
create table Transaction_Type_Group(
	ID int,
	Type varchar(50),
	[Name] nvarchar(200),
	constraint Transaction_Type_Group_P primary key(ID)
);
---Transaction
----Transaction type: B-Bill, BR- Bill return, M Merchandise, MR - Merchandise return, D - Destroy, P - Payment
create table [Transaction](
	code varchar(50),
	[Type] varchar(50),
	Type_Group_ID int,
	Created_Time date,
	Created_User varchar(50),
	Partner_ID int,
	Shop_ID int,
	Discount decimal(18,0),
	Note nvarchar(500),
	constraint Transaction_P primary key(Code)
);
---Transaction_Payment
----Payment type: M - Cash, C - Card, D - Dept
create table Transaction_Payment(
	ID int,
	Transaction_Code varchar(50),
	[Type] varchar(50),
	Amount decimal(18)
	constraint Transaction_Payment_P primary key(ID)
);
---Shop
----Partner type: C - Customer, S - Supplier
create table Shop(
	ID int,
	[Name] nvarchar(200),
	Phone varchar(50),
	Email varchar(50),
	[Address] nvarchar(200),
	[Status] varchar(1),
	constraint Shop_P primary key(ID)
);
---Partner
create table [Partner](
	ID int,
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
create table Users(
	Account varchar(50),
	[Password] varchar(100),
	[Name] nvarchar(200),
	Phone varchar(50),
	Email varchar(50),
	[Address] nvarchar(200),
	Birthday int,
	Role_ID int,
	Login_Time_Retrict bit,
	[Status] varchar(1),
	constraint Users_P primary key(Account)
);
---Role
create table [Role](
	ID int,
	[Name] nvarchar(200),
	constraint Role_P primary key(ID)
);
---User_Permission
create table User_Permission(
	User_Account varchar(50),
	Shop_ID int,
	Permission_Code varchar(50),
	Has_Permission bit,
	constraint User_Permission_P primary key(User_Account,Shop_ID,Permission_Code)
);
---Role_Permission
create table Role_Permission(
	Role_ID int,
	Permission_Code varchar(50),
	Has_Permission bit,
	constraint Role_Permission_P primary key(Role_ID,Permission_Code)
);
---Permission
create table Permission(
	Code varchar(50),
	[Name] nvarchar(50),
	Parent_Code varchar(50),
	constraint Permission_P primary key(Code)
);
