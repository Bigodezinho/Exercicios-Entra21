using CrudPessoaContato.Dtos;
using CrudPessoaContato.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CrudPessoaContato.Repositories
{
    public class PessoaRepository
    {
        private readonly string _connection = @"Data Source=ITELABD18\SQLEXPRESS;Initial Catalog=Cadastro;Integrated Security=True;";
        public bool SalvarPessoa(Pessoa pessoa, Endereco endereco, List<Telefone> telefones) 
        {
            int IdPessoaCriada = -1;
            try
            {
                var query = @"INSERT INTO Pessoa 
                              (Nome, Cpf, DataNascimento) 
                              OUTPUT Inserted.Id
                              VALUES (@nome,@cpf,@dataNascimento)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@nome", pessoa.Nome);
                    command.Parameters.AddWithValue("@cpf", pessoa.Cpf);                    
                    command.Parameters.AddWithValue("@dataNascimento", pessoa.DataNascimento);                    
                    command.Connection.Open();
                    IdPessoaCriada = (int)command.ExecuteScalar();
                }

                SalvarEndereco(endereco, IdPessoaCriada);

                SalvarTelefone(telefones, IdPessoaCriada);

                Console.WriteLine("Pessoa cadastrada com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }
        private void SalvarTelefone(List<Telefone> telefones, int IdPessoa) 
        {
            try
            {
                foreach (var telefone in telefones) 
                {
                    var query = @"INSERT INTO Telefone 
                              (DDD, Numero, IdPessoa)                               
                              VALUES (@ddd,@numero,@idPessoa)";
                    using (var sql = new SqlConnection(_connection))
                    {
                        SqlCommand command = new SqlCommand(query, sql);
                        command.Parameters.AddWithValue("@ddd", telefone.DDD);
                        command.Parameters.AddWithValue("@numero", telefone.Numero);
                        command.Parameters.AddWithValue("@idPessoa", IdPessoa);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine("Telefone cadastrado com sucesso.");
                }                               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);                
            }
        }
        private void SalvarEndereco(Endereco endereco, int IdPessoa) 
        {
            try
            {
                var query = @"INSERT INTO Endereco 
                              (Rua, Numero, Complemento, IdPessoa)                               
                              VALUES (@rua,@numero,@complemento, @idPessoa)";
                using (var sql = new SqlConnection(_connection))
                {
                    SqlCommand command = new SqlCommand(query, sql);
                    command.Parameters.AddWithValue("@rua", endereco.Rua);
                    command.Parameters.AddWithValue("@numero", endereco.Numero);
                    command.Parameters.AddWithValue("@complemento", endereco.Complemento);
                    command.Parameters.AddWithValue("@idPessoa", IdPessoa);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Endereço cadastrado com sucesso.");                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);                
            }
        }

        public List<PessoaDto> BuscarPorNome(string nome) 
        {
            List<PessoaDto> pessoasEncontradas;
            try
            {
                var query = @"SELECT Id, Nome, Cpf, DataNascimento FROM Pessoa
                                      WHERE Nome like CONCAT('%',@nome,'%')";
                
                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new 
                    {
                        nome
                    };
                    pessoasEncontradas = connection.Query<PessoaDto>(query, parametros).ToList();
                }

                pessoasEncontradas.ForEach(e => 
                {
                    e.Endereco = BuscarEnderecoPessoa(e.Id);
                    e.Telefones = BuscarTelefonesPessoa(e.Id);
                });

                return pessoasEncontradas;

                //maneira antiga
                //using (var sql = new SqlConnection(_connection))
                //{
                //    SqlCommand command = new SqlCommand(query, sql);
                //    command.Parameters.AddWithValue("@nome", nome);
                //    command.Connection.Open();
                //    resultado = command.ExecuteReader();

                //    while (resultado.Read())
                //    {
                //        pessoas.Add(new Pessoa(resultado.GetInt32(resultado.GetOrdinal("Id")),
                //                               resultado.GetString(resultado.GetOrdinal("Nome")),
                //                               resultado.GetString(resultado.GetOrdinal("Cpf")),                                                                                              
                //                               resultado.GetDateTime(resultado.GetOrdinal("DataNascimento"))));
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }            
        }
        private EnderecoDto BuscarEnderecoPessoa(int idPessoa) 
        {

            try 
            {
                var query = @"SELECT * FROM Endereco
                                      WHERE IdPessoa = @idPessoa";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idPessoa
                    };
                    return connection.QueryFirstOrDefault<EnderecoDto>(query, parametros);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Erro: "+ex.Message);
                return null;
            }
        }
        private List<TelefoneDto> BuscarTelefonesPessoa(int idPessoa) 
        {
            try
            {
                var query = @"SELECT * FROM Telefone
                                      WHERE IdPessoa = @idPessoa";

                using (var connection = new SqlConnection(_connection))
                {
                    var parametros = new
                    {
                        idPessoa
                    };
                    return connection.Query<TelefoneDto>(query, parametros).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }

        }
        public List<PessoaDto> BuscarTodos()
        {
            List<PessoaDto> pessoasEncontradas;
            try
            {
                var query = @"SELECT Id, Nome, Cpf, DataNascimento FROM Pessoa";

                using (var connection = new SqlConnection(_connection))
                {
                    pessoasEncontradas = connection.Query<PessoaDto>(query).ToList();
                }

                pessoasEncontradas.ForEach(e =>
                {
                    e.Endereco = BuscarEnderecoPessoa(e.Id);
                    e.Telefones = BuscarTelefonesPessoa(e.Id);
                });

                return pessoasEncontradas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return null;
            }
        }
    }
}
