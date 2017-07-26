using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    public class RootGastos
    {
        public Dado[] dados { get; set; }
        public Link[] links { get; set; }
    }

    public class Dado
    {
        public string ano { get; set; }
        public string mes { get; set; }
        public string tipoDespesa { get; set; }
        public string idDocumento { get; set; }
        public string tipoDocumento { get; set; }
        public string dataDocumento { get; set; }
        public string numDocumento { get; set; }
        public string valorDocumento { get; set; }
        public string urlDocumento { get; set; }
        public string nomeFornecedor { get; set; }
        public string cnpjCpfFornecedor { get; set; }
        public string valorLiquido { get; set; }
        public string valorGlosa { get; set; }
        public string numRessarcimento { get; set; }
        public string idLote { get; set; }
        public string parcela { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

}
