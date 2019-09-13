--using System;
--using System.Collections.Generic;
--using System.Text;

--namespace StockPortfolio
--{
--    class sqlscript
--    {
	
--Create table Stock
--(
--	StockID int primary key not null IDENTITY (1,1),
--	CompanyName nvarchar(max) not null,
--	Symbol nvarchar(10) not null,
--	MarketCap nvarchar(max) not null,
--	IpoYear nvarchar(max) null,
--	Sector nvarchar(max) not null
--)
--alter table Stock add constraint UK__Symbol UNIQUE (Symbol)

--Create table CashAccount
--(
--	AcctID int not null Primary key identity (1,1),
--	CashBalance decimal (18,6) not null
--)

--Create table CashTransaction
--(
--    CashTransactionID int not null Primary key identity (1,1),
--    AcctID int not null,
--    Amount decimal (18,6) not null
--)

--create table Portfolio
--(
--	PortfolioID smallint not null primary key identity (1,1) ,
--	AcctID int not null,
--	PortfolioName nvarchar(max) not null,
--	ProfitLossYTD decimal (18,6),
--	ProfitLossDaily decimal (18,6),
--	StockWorth decimal (18,6),
--	--CashBalance decimal (18,6),
--	PortfolioWorth decimal (18,6)
--)

--Create table DailyRecord
--(
--	DailyRecordID smallint not null primary key identity (1,1),
--	StockID int not null,
--	Symbol nvarchar(10) not null,
--	RecordDate Date not null,
--	OpenPrice decimal(18,6) not null,
--	DailyHigh decimal(18,6) not null,
--	DailyLow decimal(18,6) not null,
--	ClosePrice decimal(18,6) not null,
--	AdjustedClose decimal(18,6) not null,
--	Volume decimal(18,6) not null,
--	DividendYield decimal(18,6) not null,
--	High52Week decimal(18,6) null,
--	Low52Week decimal(18,6) null,
--	OverNightChange decimal(18,6) null,
--	DailyChange decimal(18,6) null,
--	VolatilityRating decimal(18,6) null,
--)

--Create table TransactionRecord
--(
--	RecordID int not null primary key identity (1,1),
--	StockID int not null,
--	DateTime datetime not null,
--	Price decimal(18,6) not null,
--	Quantity decimal (18,6) not null,
--	Fees decimal (18,6) not null,
--	PortfolioID int not null
--)

--create table AccountLoginInfo
--(
--    AcctID int not null primary key identity (1,1),
--    Username NVARCHAR(max) not null,
--    Password nvarchar(max) not null
--)

--alter table TransactionRecord add constraint FK__TransactionRecord__Stock__ForStockID foreign key (StockID) references Stock(StockID) on delete cascade
--alter table DailyRecord add constraint FK__DailyRecord__Stock__ForStockID foreign key (StockID) references Stock(StockID) on delete cascade
--alter table DailyRecord add constraint FK__DailyRecord__Stock__ForSymbol foreign key (Symbol) references Stock(Symbol)
--alter table Portfolio add constraint FK_Portfolio_AccountLoginInfo_ForAcctID foreign key (AcctID) references AccountLoginInfo (AcctID) on delete cascade
--alter table CashAccount add constraint FK_CashAccount_AccountLoginInfo_ForAcctID foreign key (AcctID) references AccountLoginInfo (AcctId)
--alter table CashTransaction add constraint FK_CashTransaction_AccountLoginInfo_ForAcctID foreign key (AcctID) references AccountLoginInfo (AcctID) on delete cascade

--insert  into AccountLoginInfo (Username, Password)
--values ('john', 'password')

--insert into Portfolio (AcctID, PortfolioName)
--values ('1', 'test')

--insert into Portfolio(AcctID, PortfolioName)
--values ('2', 'test1')

--    }
--}
