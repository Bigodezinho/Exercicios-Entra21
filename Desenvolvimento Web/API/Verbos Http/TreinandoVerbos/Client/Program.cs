using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("Hello World!");

            int opcao = Convert.ToInt32(Console.ReadLine());

            while (opcao != 0) 
            {
                if(opcao == 1) 
                {
                    var resultado = BuscarTodos();
                    //mostra os dados na tela
                    foreach (var item in resultado) 
                    {
                        Console.WriteLine("=====================================");
                        Console.WriteLine("Id: " + item.Id);
                        Console.WriteLine("Nome: "+item.Nome);
                        Console.WriteLine("CPF: "+item.Cpf);
                        Console.WriteLine("Idade: " + item.Idade);
                        Console.WriteLine("=====================================");
                    }
                }
                
                if(opcao == 2) 
                {
                    Console.WriteLine("Informe os dados da pessoa:");

                    var pessoa = new Pessoa() 
                    {
                        Cpf = Console.ReadLine(),
                        Idade = Convert.ToInt32(Console.ReadLine()),
                        Nome = Console.ReadLine()
                    };

                    Salvar(pessoa);
                }

                if(opcao == 3)
                {
                    Console.WriteLine("Informe o nome da pessoa para remover:");
                    string nome = Console.ReadLine();
                    Remover(nome);
                }

                if(opcao == 4)
                {
                    Console.WriteLine("Informe o nome da pessoa para atualizar:");
                    string nome = Console.ReadLine();

                    Console.WriteLine("Informe os novos dados da pessoa:");

                    var pessoa = new Pessoa()
                    {
                        Cpf = Console.ReadLine(),
                        Idade = Convert.ToInt32(Console.ReadLine()),
                        Nome = Console.ReadLine()
                    };

                    Atualizar(nome, pessoa);
                }

                opcao = Convert.ToInt32(Console.ReadLine());
            }
        }
        public static List<PessoaDto> BuscarTodos()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            //Busca todos os clientes dentro da api;
            try
            {
                //monta a request para a api;
                response = httpClient.GetAsync("https://localhost:44373/pessoas/buscartodos").Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine(resultado);
                    return new List<PessoaDto>();
                }
                //converte os dados recebidos e retorna eles como objetos do C#;
                var objetoDesserializado = JsonConvert.DeserializeObject<List<PessoaDto>>(resultado);

                return objetoDesserializado;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PessoaDto>();
            }
        }
        public static void Salvar(Pessoa pessoa) 
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            var json = JsonConvert.SerializeObject(pessoa);

            try
            {
                //monta a request para a api;
                response = httpClient.PostAsync("https://localhost:44373/pessoas/save", new StringContent(json, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();

                var resultado = response.Content.ReadAsStringAsync().Result;

                //converte os dados recebidos e retorna eles como objetos do C#;
                
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);                
            }
        }
        public static void Remover(string nome) 
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            try
            {
                //monta a request para a api;
                response = httpClient.DeleteAsync($"https://localhost:44373/pessoas/remover?nome={nome}").Result;
                
                var resultado = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine(resultado);
                }
                //converte os dados recebidos e retorna eles como objetos do C#;

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void Atualizar(string nome, Pessoa pessoa) 
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response;

            try
            {
                var json = JsonConvert.SerializeObject(pessoa);
                //monta a request para a api;
                response = httpClient.PutAsync($"https://localhost:44373/pessoas/atualizarcomparametro?nome={nome}", new StringContent(json, Encoding.UTF8, "application/json")).Result;

                var resultado = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine(resultado);
                }

                //converte os dados recebidos e retorna eles como objetos do C#;

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
