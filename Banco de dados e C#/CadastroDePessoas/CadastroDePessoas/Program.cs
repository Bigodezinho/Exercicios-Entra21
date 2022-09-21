using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CadastroDePessoas
{
    class Program
    {
        static string connection = @"Data Source=ITELABD18\SQLEXPRESS;Initial Catalog=AulasCsharp;Integrated Security=True;";
        static void AtualizarPessoa(Pessoa pessoa) 
        {
            try
            {
                var query = @"UPDATE Pessoa 
                              SET Nome = @nome, Cpf = @cpf, Rg = @rg, 
                              Naturalidade = @naturalidade ,DataNascimento = @dataNascimento
                              WHERE Id =@id";
                using (var sql = new SqlConnection(connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nome", pessoa.Nome);
                    command.Parameters.AddWithValue("@cpf", pessoa.Cpf);
                    command.Parameters.AddWithValue("@rg", pessoa.Rg);
                    command.Parameters.AddWithValue("@dataNascimento", pessoa.DataNascimento);
                    command.Parameters.AddWithValue("@naturalidade", pessoa.Naturalidade);
                    command.Parameters.AddWithValue("@id", pessoa.Id);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Pessoa atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        static void SalvarPessoa(Pessoa pessoa) 
        {
            try
            {
                var query = "insert into Pessoa " +
                          "(Nome, Cpf, Rg, DataNascimento, Naturalidade) " +
                          "values (@nome,@cpf,@rg,@dataNascimento,@naturalidade)";
                using (var sql = new SqlConnection(connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nome", pessoa.Nome);
                    command.Parameters.AddWithValue("@cpf", pessoa.Cpf);
                    command.Parameters.AddWithValue("@rg", pessoa.Rg);
                    command.Parameters.AddWithValue("@dataNascimento", pessoa.DataNascimento);
                    command.Parameters.AddWithValue("@naturalidade", pessoa.Naturalidade);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Pessoa cadastrada com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        static List<Pessoa> EncontrarPessoa(string termo) 
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            SqlDataReader resultado;
            try 
            {
                var query = @"SELECT Id, Nome, Cpf, Rg, DataNascimento, Naturalidade FROM Pessoa
                                      WHERE Nome like CONCAT('%',@termo,'%') OR Cpf like CONCAT('%',@termo,'%')
                                            OR Rg like CONCAT('%',@termo,'%') or Naturalidade like CONCAT('%',@termo,'%')";
                using (var sql = new SqlConnection(connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@termo", termo);
                    command.Connection.Open();
                    resultado = command.ExecuteReader();

                    while (resultado.Read())
                    {
                        pessoas.Add(new Pessoa(resultado.GetInt32(resultado.GetOrdinal("Id")),
                                               resultado.GetString(resultado.GetOrdinal("Nome")),
                                               resultado.GetString(resultado.GetOrdinal("Cpf")),
                                               resultado.GetString(resultado.GetOrdinal("Rg")),
                                               resultado.GetString(resultado.GetOrdinal("Naturalidade")),
                                               resultado.GetDateTime(resultado.GetOrdinal("DataNascimento"))));
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Erro: "+ex.Message);
            }
            return pessoas;
        }
        static List<Pessoa> BuscarTodas() 
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            SqlDataReader resultado;
            var query = @"SELECT Id, Nome, Cpf, Rg, DataNascimento, Naturalidade FROM Pessoa";
            try 
            {
                using (var sql = new SqlConnection(connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Connection.Open();
                    resultado = command.ExecuteReader();

                    while (resultado.Read())
                    {
                        pessoas.Add(new Pessoa(resultado.GetInt32(resultado.GetOrdinal("Id")),
                                               resultado.GetString(resultado.GetOrdinal("Nome")),
                                               resultado.GetString(resultado.GetOrdinal("Cpf")),
                                               resultado.GetString(resultado.GetOrdinal("Rg")),
                                               resultado.GetString(resultado.GetOrdinal("Naturalidade")),
                                               resultado.GetDateTime(resultado.GetOrdinal("DataNascimento"))));
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
            return pessoas;
        }
        static void RemoverPessoa(Pessoa pessoa) 
        {
            try
            {
                var query = @"DELETE FROM Pessoa WHERE Id = @id";
                using (var sql = new SqlConnection(connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);                    
                    command.Parameters.AddWithValue("@id", pessoa.Id);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Pessoa removida com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        static void MostrarListagem(List<Pessoa> pessoas) 
        {
            foreach (Pessoa p in pessoas)
            {
                Console.WriteLine("========Inicio========");
                Console.WriteLine("Nome: " + p.Nome);
                Console.WriteLine("CPF: " + p.Cpf);
                Console.WriteLine("Rg: " + p.Rg);
                Console.WriteLine("Telefone: " + p.Telefone);
                Console.WriteLine("Endereço: " + p.Endereco);
                Console.WriteLine("Idade: " + p.Idade);
                Console.WriteLine("Naturalidade: " + p.Naturalidade);
                Console.WriteLine("Data de Nascimento: " + p.DataNascimento);
                Console.WriteLine("========Fim========");
            }
        }

        static void Main(string[] args)
        {
            int opcao = Convert.ToInt32(Console.ReadLine());
            while (opcao != 0) 
            {
                if (opcao == 1) 
                {
                    Console.WriteLine("Insira o Nome:");
                    string nome = Console.ReadLine();
                    Console.WriteLine("Insira o CPF:");
                    string cpf = Console.ReadLine();
                    Console.WriteLine("Insira o Rg:");
                    string rg = Console.ReadLine();
                    Console.WriteLine("Insira o Telefone:");
                    string telefone = Console.ReadLine();
                    Console.WriteLine("Insira o Endereço:");
                    string endereco = Console.ReadLine();
                    Console.WriteLine("Insira o Data de nascimento:");
                    DateTime dataDeNascimento = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Insira o Naturalidade:");
                    string naturalidade = Console.ReadLine();

                    Pessoa pessoa = new Pessoa(nome, cpf, rg, telefone, endereco, dataDeNascimento, naturalidade);
                    SalvarPessoa(pessoa);
                }
                if (opcao == 2) 
                {
                    try 
                    {                        
                        Console.WriteLine("========Listagem========");
                        MostrarListagem(BuscarTodas());                        
                    }
                    catch (Exception ex) 
                    {

                    }
                }
                if (opcao == 3) 
                {
                    Console.WriteLine("Buscar pessoa:");
                    Console.WriteLine("Insira um termo para busca:");
                    string termo = Console.ReadLine();                    
                    Pessoa pessoaEncontrada;
                    List<Pessoa> pessoasEncontradas = EncontrarPessoa(termo);
                    while (pessoasEncontradas.Count() > 1 || pessoasEncontradas.Count() == 0 )
                    {
                        if(pessoasEncontradas.Count() == 0)
                        {
                            Console.WriteLine("Nenhuma pessoa encontrada. Insira um termo válido");
                            termo = Console.ReadLine();
                            pessoasEncontradas = EncontrarPessoa(termo);
                        }
                        else if(pessoasEncontradas.Count() > 1) 
                        {
                            Console.WriteLine("Mais de uma pessoa encontrada. Insira um termo mais preciso:");
                            termo = Console.ReadLine();
                            pessoasEncontradas = EncontrarPessoa(termo);
                        }                        
                    }                    
                    
                    pessoaEncontrada = pessoasEncontradas.First();
                    Console.WriteLine("Insira o Nome:");
                    pessoaEncontrada.Nome = Console.ReadLine();
                    Console.WriteLine("Insira o CPF:");
                    pessoaEncontrada.Cpf = Console.ReadLine();
                    Console.WriteLine("Insira o Rg:");
                    pessoaEncontrada.Rg = Console.ReadLine();
                    Console.WriteLine("Insira o Telefone:");
                    pessoaEncontrada.Telefone = Console.ReadLine();
                    Console.WriteLine("Insira o Endereço:");
                    pessoaEncontrada.Endereco = Console.ReadLine();
                    Console.WriteLine("Insira o Data de nascimento:");
                    pessoaEncontrada.DataNascimento = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Insira o Naturalidade:");
                    pessoaEncontrada.Naturalidade = Console.ReadLine();
                    AtualizarPessoa(pessoaEncontrada);
                }
                if (opcao == 4) 
                {
                    Console.WriteLine("Buscar pessoa:");
                    Console.WriteLine("Insira um termo para busca:");
                    string termo = Console.ReadLine();
                    Pessoa pessoaEncontrada;
                    List<Pessoa> pessoasEncontradas = EncontrarPessoa(termo);
                    while (pessoasEncontradas.Count() > 1 || pessoasEncontradas.Count() == 0)
                    {
                        if (pessoasEncontradas.Count() == 0)
                        {
                            Console.WriteLine("Nenhuma pessoa encontrada. Insira um termo válido");
                            termo = Console.ReadLine();
                            pessoasEncontradas = EncontrarPessoa(termo);
                        }
                        else if (pessoasEncontradas.Count() > 1)
                        {
                            Console.WriteLine("Mais de uma pessoa encontrada. Insira um termo mais preciso:");
                            termo = Console.ReadLine();
                            pessoasEncontradas = EncontrarPessoa(termo);
                        }
                    }
                    pessoaEncontrada = pessoasEncontradas.First();
                    RemoverPessoa(pessoaEncontrada);
                }
                opcao = Convert.ToInt32(Console.ReadLine());
            }
            //string connection = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=false;Initial Catalog=master;";          
        }
    }
}
