CREATE TABLE Endereco
(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY (Id),
	Rua varchar(255) NOT NULL,
	Numero varchar(255) NOT NULL,
	Complemento varchar(255) NOT NULL,		
	IdPessoa int NOT NULL,
	CONSTRAINT FK_IdPessoa_Endereco FOREIGN KEY (IdPessoa)
	REFERENCES Pessoa(Id)
);