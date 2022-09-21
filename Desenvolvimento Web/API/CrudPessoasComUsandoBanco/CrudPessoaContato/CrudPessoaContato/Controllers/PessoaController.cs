using CrudPessoaContato.Models;
using CrudPessoaContato.Repositories;
using CrudPessoaContato.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudPessoaContato.Controllers
{
    [Route("[controller]/[action]")]
    //[Produces("application/json")]
    [ApiController]
    public class PessoaController : ControllerBase
    {        
        private readonly PessoaRepository _pessoaRepository;

        public PessoaController() 
        {
            _pessoaRepository = new PessoaRepository();
        }       

        [HttpPost]
        public IActionResult Salvar(SalvarPessoaViewModel salvarPessoaViewModel ) 
        {
            if (salvarPessoaViewModel == null)
                return Ok("Não foram informados dados");

            if (salvarPessoaViewModel.Pessoa == null)
                return Ok("Dados da pessoa não informados.");

            if (salvarPessoaViewModel.Endereco == null)
                throw new ArgumentNullException($"campo {nameof(salvarPessoaViewModel.Endereco)} vazio ou nulo.");

            if(salvarPessoaViewModel.Telefones == null || !salvarPessoaViewModel.Telefones.Any())
                throw new ArgumentNullException($"campo {nameof(salvarPessoaViewModel.Telefones)} vazio ou nulo.");

            var resultado = _pessoaRepository.SalvarPessoa(salvarPessoaViewModel.Pessoa, 
                                                           salvarPessoaViewModel.Endereco, 
                                                           salvarPessoaViewModel.Telefones);

            if (resultado) return Ok("Pessoa cadastrada com sucesso.");

            return Ok("Houve um problema ao salvar. Pessoa não cadastrada.");
        }

        [HttpPost]
        public IActionResult BuscarPorNome(string nome) 
        {
            var resultado = _pessoaRepository.BuscarPorNome(nome);

            if (resultado == null || !resultado.Any())
                return NotFound("Nenhum registro encontrado com o nome informado.");

            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult BuscarTodos() 
        {
            var resultado = _pessoaRepository.BuscarTodos();

            if (resultado == null)
                return NotFound();

            return Ok(resultado);
        }      
    }
}
