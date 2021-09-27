using CSU.Console.NFS.CarregaDados.Entidades;
using CSU.Console.NFS.CarregaDados.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace CSU.Console.NFS.CarregaDados
{
    public class CarregaDados
    {
        #region [ Propriedades ]

        /// <summary>
        /// Nome do arquivo de log gerado
        /// </summary>
        public static string NomeArquivoLog { get; set; }

        /// <summary>
        /// Nome do arquivo de erro gerado
        /// </summary>
        public static string NomeArquivoErro { get; set; }
        private static string pathRetorno { get; set; }

        private static IConfiguration _iconfiguration;

        #endregion

        #region [ Construtor ]

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public CarregaDados()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
                     _iconfiguration = builder.Build();

                NomeArquivoLog = _iconfiguration["arquivoLog"].ToString();
                pathRetorno = _iconfiguration["pathRetorno"].ToString();
            }
            catch (Exception erro)
            {
                GravarLog(erro.Message);
                Environment.Exit(999);
            }
        }
        #endregion

        public void Processar()
        {
            string xml = "";
            System.IFormatProvider cultureUS = new System.Globalization.CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            // Inicio do processamento
            try
            {
                GravarLog("INICIO PROCESSAMENTO");

                GravarLog("CRIANDO CONEXÂO COM BASE DE DADOS");
                NegocioSQL negocioSQL = new NegocioSQL(_iconfiguration);
                
                GravarLog("EXECUTANDO Consulta NFE");
                List<NotaFiscal> NotasFiscais = negocioSQL.RetornaNFE(12);

                if (NotasFiscais.Count < 1)
                {
                    GravarLog("Consulta NFE não retornou nenhum registro");
                }

                var dataBase = Convert.ToDateTime(DateTime.Now).ToString("yyyyMM");

                xml = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
                xml += $"<dataBase>{dataBase}</dataBase>";

                foreach (var item in NotasFiscais)
                {                    
                    xml += $"<codnota>{item.COD_NOTA}</codnota>";
                    xml += $"<codvenda>{item.COD_VENDA}</codvenda>";
                    xml += $"<destinatarioremetente>{item.DESTINATARIO_REMETENTE}</destinatarioremetente>";
                    xml += $"<dtemissao>{item.DT_EMISSAO}</dtemissao>";
                    xml += $"<dtsaidaentrada>{item.DT_SAIDA_ENTRADA}</dtsaidaentrada>";
                    xml += $"<numnota>{item.NUM_NOTA}</numnota>";
                }               

                File.WriteAllText(pathRetorno + $"nfe" + ".xml", xml);

                GravarLog("PROCESSAMENTO FINALIZADO COM SUCESSO");
                Environment.Exit(0);

            }
            catch (Exception ex)
            {
                GravarLog(ex.Message);
                Environment.Exit(999);
            }
        }

        #region [ Gravar Log ]
        public void GravarLog(string mensagem)
        {
            try
            {
                using (var arquivo = new StreamWriter(NomeArquivoLog, true))
                    arquivo.WriteLine("[" + DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss") + "] - " + mensagem);
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        #endregion
    }
}
