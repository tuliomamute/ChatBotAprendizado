using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Opcoes
    {
        private Opcoes() { }
        private static Opcoes _instance = null;

        public static Opcoes Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Opcoes();

                return _instance;
            }
        }

        public Deputado DeputadoEscolhido { get; set; }
        public const string UrlBaseAPI = "https://dadosabertos.camara.leg.br";
    }
}
