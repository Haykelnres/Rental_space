USE master
go 
create database TCDB

USE TCDB
GO
create table Arendator
(
	Arendator_id int IDENTITY CONSTRAINT Arendator_Arendator_id PRIMARY KEY,
	Arendator_name varchar(50) NOT NULL,
	Arendator_phone varchar(20) NOT NULL,
	Arendator_passport varchar(15) not null,
	Arendator_firm_name varchar(100)not null
)
--================--
USE TCDB
GO
create table Worker
(
	Worker_id int IDENTITY CONSTRAINT Worker_Worker_id PRIMARY KEY,
	Worker_name varchar(50) NOT NULL,
	Worker_log varchar(20) NOT NULL,
	Worker_password varchar(20) NOT NULL	
)
--================--
USE TCDB
GO
create table Room
(
	Room_id int IDENTITY CONSTRAINT Taxation_type_Bet_id PRIMARY KEY,
	Room_level varchar(5) NOT NULL,--этаж 1,2,3
	Room_level_amount int NOT NULL,--1, 2
	Room_square varchar(10) NOT NULL,
	Room_WC varchar(3) NOT NULL,-- да нет
	Room_windows varchar(3) NOT NULL,--да нет
	
	Room_WIFI varchar(3) NOT null,
	Room_comment varchar(100) NOT null,
	Room_status varchar(20) NOT null,
)

--================--
USE TCDB
GO
create table Dogovor
(
	Dogovor_id int IDENTITY CONSTRAINT Tax_doc_Doc_id PRIMARY KEY,
	Dogovor_create date NOT NULL,
	Dogovor_end date NOT NULL,
	Dogovor_cost money NOT NULL,
	Arendator_name varchar(100) NOT NULL,
	Room_id int NOT NULL,
	Dogovor_payment varchar(20) NOT NULL,--нал/безнал
	Dogovor_status varchar(10) NOT NULL,--активен/неактивен
	Arendator_id int NOT NULL,
	Worker_id int not null
)
alter table Dogovor 
ADD FOREIGN KEY (Arendator_id) REFERENCES Arendator(Arendator_id)
alter table Dogovor 
ADD FOREIGN KEY (Room_id) REFERENCES Room(Room_id)
alter table Dogovor 
ADD FOREIGN KEY (Worker_id) REFERENCES Worker(Worker_id)
alter table Dogovor add  check (Dogovor_end > GETDATE());