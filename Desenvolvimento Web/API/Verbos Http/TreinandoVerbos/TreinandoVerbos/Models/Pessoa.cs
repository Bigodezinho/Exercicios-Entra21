using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreinandoVerbos.Models
{
    public class Pessoa
    {
        private static int _Id;      
        public int Id 
        {
            get;set;
        }
        public string Nome { get; set; }        
        public int Idade { get; set; }
        public string Cpf { get; set; }
        public Pessoa() 
        {
            _Id++;
            Id = _Id;
        }
        //public Endereco Endereco { get; set; }
        //public List<Telefone> Telefones { get; set; }
    }
}
