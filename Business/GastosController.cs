using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class GastosController
    {
        /// <summary>
        /// Busca as despesas do Ano corrente para um determinado deputado
        /// </summary>
        /// <param name="idDeputado"></param>
        /// <returns></returns>
        public async Task<string> BuscarDespesas()
        {
            string PaginaBusca = string.Empty;

            RootGastos model = null;
            List<Dado> DespesasDeputado = null;
            try
            {
                PaginaBusca = $"{Opcoes.UrlBaseAPI}/api/v2/deputados/{Opcoes.Instance.DeputadoEscolhido.ideCadastro}/despesas?ano={DateTime.Now.Year}&pagina=1&itens=100";
                DespesasDeputado = new List<Dado>();

                do
                {
                    using (HttpClient client = new HttpClient())
                    {
                        //Criação dos cabeçalhos da requisição
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.GetAsync(PaginaBusca);

                        if (response.IsSuccessStatusCode)
                        {
                            //Deserializa a lista e adiciona os conteudos recuperados a uma lista
                            model = JsonConvert.DeserializeObject<RootGastos>(await response.Content.ReadAsStringAsync());
                            DespesasDeputado.AddRange(model.dados.ToList());

                            //Pega a Url da ultima página, para saber quando parar de realizar as requisições
                            PaginaBusca = model.links.Where(x => x.rel == "next").FirstOrDefault()?.href;
                        }
                    }

                    //Colocada a condição de parada até que não tenha mais next, ou seja, chegou-se a na ultima página
                } while (model.links.Where(x => x.rel == "next").Count() > 0);

                return MontaRetornoDespesasDeputado(DespesasDeputado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (model != null)
                    model = null;

                if (DespesasDeputado != null)
                    DespesasDeputado = null;

            }

        }

        /// <summary>
        /// Monta uma string de retorno a ser exibida na tela
        /// </summary>
        /// <param name="despesas"></param>
        /// <returns></returns>
        private string MontaRetornoDespesasDeputado(List<Dado> despesas)
        {
            StringBuilder builder = null;
            try
            {

                //Soma os valores baseado no mes
                var somaMensal = despesas.GroupBy(x => x.mes)
                    .Select(group => new { soma = group.Sum(y => Convert.ToDecimal(y.valorLiquido.Replace(".", ","))), mes = group.Key })
                    .OrderBy(x => x.mes).ToList();

                builder = new StringBuilder();
                builder.AppendLine($"---Despesas do Deputado {Opcoes.Instance.DeputadoEscolhido.nomeParlamentar}---{Environment.NewLine}");

                //Percorre cada Mes para montar o retorno
                for (int indice = 0; indice < somaMensal.Count; indice++)
                    builder.AppendLine($"{indice + 1}) Mês: {RetornaMesAno(somaMensal[indice].mes)} - Valor: R$ {somaMensal[indice].soma}{Environment.NewLine}");

                return builder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (builder != null)
                    builder = null;
            }
        }

        /// <summary>
        /// Retorna o mes especificado
        /// </summary>
        /// <param name="mes"></param>
        /// <returns></returns>
        private string RetornaMesAno(string mes)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(mes));
        }


    }
}
