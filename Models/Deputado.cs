using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    public class Rootobject
    {
        public Deputados deputados { get; set; }
    }

    public class Deputados
    {
        public Deputado[] deputado { get; set; }
    }

    public class Deputado
    {
        public string ideCadastro { get; set; }
        public string codOrcamento { get; set; }
        public string condicao { get; set; }
        public string matricula { get; set; }
        public string idParlamentar { get; set; }
        public string nome { get; set; }
        public string nomeParlamentar { get; set; }
        public string urlFoto { get; set; }
        public string sexo { get; set; }
        public string uf { get; set; }
        public string partido { get; set; }
        public string gabinete { get; set; }
        public string anexo { get; set; }
        public string fone { get; set; }
        public string email { get; set; }
    }

}
