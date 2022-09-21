CREATE TABLE Telefone
(
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY (Id),
	DDD varchar(255) NOT NULL,
	Numero varchar(255) NOT NULL,	
	IdPessoa int NOT NULL,
	CONSTRAINT FK_IdPessoa_Telefone FOREIGN KEY (IdPessoa)
	REFERENCES Pessoa(Id)
);