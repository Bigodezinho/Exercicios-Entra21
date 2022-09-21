using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroDePessoas
{
    public class Pessoa
    {
        public int Id { get;set; }
        public int Idade
        {
            get
            {
                var idade = DateTime.Now.Year - DataNascimento.Year;
                return idade;
            }
        }
        public string Nome
        {
            get;
            set;
        }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Naturalidade { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pessoa() { }
        public Pessoa(string nome, string cpf, string rg, string telefone, string endereco, DateTime dataNascimento, string naturalidade)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Telefone = telefone;
            Endereco = endereco;
            DataNascimento = dataNascimento;
            Naturalidade = naturalidade;
        }

        public Pessoa(int id, string nome, string cpf, string rg, string naturalidade, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Naturalidade = naturalidade;
            DataNascimento = dataNascimento;
        }
    }
}
