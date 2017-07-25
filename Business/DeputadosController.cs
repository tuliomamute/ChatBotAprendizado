using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;

namespace Business
{
    public class DeputadosController
    {
        public List<Models.Deputado> ListaDeputados { get; private set; }
        public DeputadosController()
        {
            ListaDeputados = RetornaListaDeputados();
        }
        /// <summary>
        /// Método que carregará a lista de Deputados para o Bot poder manipula-la
        /// </summary>
        /// <returns></returns>
        private List<Models.Deputado> RetornaListaDeputados()
        {
            string json = "";
            XmlDocument doc = null;
            Models.Rootobject deputadosRoot = null;

            try
            {
                doc = new XmlDocument();

                using (var webservice = new Deputados.DeputadosSoapClient())
                    doc.LoadXml(webservice.ObterDeputados().OuterXml);

                json = JsonConvert.SerializeXmlNode(doc);
                json = json.Replace("\"@xmlns\":\"\",", string.Empty);

                deputadosRoot = JsonConvert.DeserializeObject<Models.Rootobject>(json);
                return deputadosRoot.deputados.deputado.ToList();
            }
            finally
            {
                if (doc != null)
                    doc = null;

                if (deputadosRoot != null)
                    deputadosRoot = null;

            }

        }

        /// <summary>
        /// Busca na lista de deputados, a partir do nome digitado
        /// </summary>
        /// <param name="NomeDeputado">Nome do deputado escolhido</param>
        /// <returns>Primeiro objeto encontrado que atende as necessidades</returns>
        public Models.Deputado RetornaDeputadoEscolhido(string NomeDeputado)
        {
            return ListaDeputados
                .Where(x => x.nomeParlamentar.ToUpper().Trim() == NomeDeputado.ToUpper().Trim() || x.nome.ToUpper().Trim() == NomeDeputado.ToUpper().Trim())
                .FirstOrDefault();
        }

    }



}
