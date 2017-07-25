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
        /// <summary>
        /// Método que carregará a lista de Deputados para o Bot poder manipula-la
        /// </summary>
        /// <returns></returns>
        public List<Models.Deputado> RetornaListaDeputados()
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
                if (doc!=null)
                    doc = null;

                if (deputadosRoot != null)
                    deputadosRoot = null;

            }

        }
    }
}
