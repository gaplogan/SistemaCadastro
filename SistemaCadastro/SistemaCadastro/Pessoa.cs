using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCadastro
{
    class Pessoa
    {
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string EstadoCivil { get; set; }

        // Tipo list pra poder adicionar ou remover dinamicamente
        public List<string> Telefone { get; set; }
        public bool CasaPropria { get; set; }
        public bool Veiculo { get; set; }
        public char Sexo { get; set; }
    }
}
