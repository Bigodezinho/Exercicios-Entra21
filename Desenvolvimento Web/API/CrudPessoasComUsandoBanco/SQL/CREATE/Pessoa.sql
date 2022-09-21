CREATE TABLE Pessoa
(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY(Id),
	Nome varchar(255) NOT NULL,
	Cpf varchar(11) NOT NULL,
	DataNascimento datetime NULL,
);




