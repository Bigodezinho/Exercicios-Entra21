using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreinandoVerbos.Models;
using TreinandoVerbos.ViewModels;

namespace TreinandoVerbos.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private static readonly List<Pessoa> pessoas = new List<Pessoa>();
        
        [HttpPost]
        public IActionResult Save(Pessoa pessoa)
        {
            if (pessoa == null)
                return NoContent();

            pessoas.Add(pessoa);

            return Ok("Adicionado com sucesso!");
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            if (pessoas == null || !pessoas.Any())
                return NotFound(new { mensage = $"Lista vazia." });

            return Ok(pessoas);
        }
        //sem viewModel
        [HttpPut]
        public IActionResult AtualizarComParametro(Pessoa pessoa, string nome)
        {
            if (pessoa == null)
                return NoContent();

            var pEncontrada = pessoas.FirstOrDefault(x => x.Nome.Contains(nome));

            if (pEncontrada == null)
                return NotFound("Não há nenhum registro com esse nome.");

            pEncontrada.Nome = pessoa.Nome;
            pEncontrada.Cpf = pessoa.Cpf;
            pEncontrada.Idade = pessoa.Idade;

            return Ok(pEncontrada);
        }
        //com viewModel
        [HttpPut]
        public IActionResult Atualizar(AtualizarPessoaViewModel model)
        {
            if (model == null)
                return NoContent();
            if (model.Atualizar == null)
                return NoContent();
            if (model.Encontrar == null)
                return NoContent();

            var pEncontrada = pessoas.FirstOrDefault(x=> x.Nome == model.Encontrar.Nome);

            if (pEncontrada == null)
                return NotFound("Não há nenhum registro com esse nome.");

            pEncontrada.Nome = model.Atualizar.Nome;
            pEncontrada.Cpf = model.Atualizar.Cpf;
            pEncontrada.Idade = model.Atualizar.Idade;

            return Ok(pEncontrada);
        }

        [HttpDelete]
        public IActionResult Remover(string nome) 
        {
            if (string.IsNullOrEmpty(nome))
                return NoContent();

            var pessoa = pessoas.FirstOrDefault(x=> x.Nome.Contains(nome));

            if (pessoa == null)
                return NotFound();

            pessoas.Remove(pessoa);
            return Ok("Removido com sucesso!");
        }
        //[HttpPost]
        //public IActionResult Save(Pessoa pessoa, bool validar) 
        //{
        //    if (validar)
        //    {
        //        return Ok(new JsonResult(new
        //        {
        //            sucesso = false,
        //            mensagem = "Pessoa informada está vazia"
        //        }));
        //    }

        //    pessoas.Add(pessoa);

        //    return Ok(new JsonResult(new
        //    {
        //        sucesso = false,
        //        mensagem = "Pessoa cadastrada"
        //    }));
        //}

        //[HttpPost]
        //public IActionResult SaveWithPhone([FromBody] CadastrarPessoaViewModel pessoaViewModel)
        //{
        //    if (pessoaViewModel == null)
        //        return new JsonResult(new { sucesso = false, mensagem = "Não há dados"  });

        //    pessoas.Add(pessoaViewModel.Pessoa);

        //    pessoaViewModel.Telefones.ForEach(e=> telefones.Add(e));

        //    return Ok();
        //}

    }
}
