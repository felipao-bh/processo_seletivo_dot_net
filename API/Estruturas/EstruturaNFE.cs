using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSU.API.NFE.RetornaNFE.Entidades
{
    public class EstruturaNFE
    {
        /// <summary>
        /// Estrutura de dados Nota Fiscal para manuseio dos dados.
        /// </summary>
        public string COD_NOTA { get; set; }
        public string COD_VENDA { get; set; }
        public string DESTINATARIO_REMETENTE { get; set; }
        public string DT_EMISSAO { get; set; }
        public string DT_SAIDA_ENTRADA { get; set; }
        public string NUM_NOTA { get; set; }
        public string VALOR_TOTAL_PRODUTOS { get; set; }
        public string VALOR_TOTAL_NOTA { get; set; }
        public string TRANS_FRETE { get; set; }
        public string NUMERO_RECIBO { get; set; }
    }
}
