using CSU.API.NFE.RetornaNFE.SQL;
using CSU.API.NFE.RetornaNFE.Estruturas;
using CSU.API.NFE.RetornaNFE.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CSU.API.NFE.RetornaNFE.Controllers
{
    [ApiController]
    public class RetornaNFE : ControllerBase
    {
        protected readonly NegocioSQL negocioSQL;
        private static IConfiguration _iconfiguration;        

        public RetornaNFE(IHttpClientFactory clientFactory)
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();

            negocioSQL = new NegocioSQL(_iconfiguration);            
        }
         

        [Route("/v1/NFE/ConsultaNFE/{mesBusca}")]
        [HttpGet]
        public IActionResult ConsultaNFE(int mesBusca)
        {

            EstruturaErro estruturaErro = new EstruturaErro();

            try
            {
                List<EstruturaNFE> listaNotasFiscais = new List<EstruturaNFE>();
                listaNotasFiscais = negocioSQL.ConsultaNFE(mesBusca);

                if (listaNotasFiscais.Count == 0)
                {
                    var retorno = CriarEstruturaRetorno(1, "NFE0001", "Não foi encontrada Nota fiscal para o mês escolhido");
                    return retorno;
                }             
                

                HttpResponseMessage result = new HttpResponseMessage();                

                result.StatusCode = HttpStatusCode.OK;

                return Ok(listaNotasFiscais);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw ex;
            }
            catch (Exception ex)     
            {
                throw ex;
            }
        }

        #region [+] Métodos privados


        /// <summary>
        /// Retorna estrutura erro
        /// </summary>
        /// <param name="indicadorErro"></param>
        /// <param name="codigoErro"></param>
        /// <param name="descricaoErro"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private IActionResult CriarEstruturaRetorno(short indicadorErro, string codigoErro, string descricaoErro, short statusCode = 500)
        {
            var estruturaRetorno = new EstruturaErro
            {
                IndicadorErro = indicadorErro,
                CodigoErro = codigoErro,
                DescricaoErro = descricaoErro
            };

            return StatusCode(statusCode, estruturaRetorno);
        }

        private static string FormataMensagemErro(Exception erro)
        {
            return "[" + DateTime.Now.ToString("dd/MM/yyyy-HH:mm:ss") + "] - " + erro.InnerException == null ? erro.Message : erro.Message + " - Inner Exception: " + erro.InnerException;
        }

        #endregion
    }
}