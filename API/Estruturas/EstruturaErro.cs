namespace CSU.API.NFE.RetornaNFE.Estruturas
{
    public class EstruturaErro
    {

        /// <summary>
        /// Atributo para indicar qual o tipo de erro:        
        /// </summary>
        public short IndicadorErro { get; set; }

        /// <summary>
        /// Atributo com o código do erro que ocorreu.         
        /// </summary>
        public string CodigoErro { get; set; }

        /// <summary>
        /// Atributo com a descrição do erro que ocorreu.         
        /// </summary>
        public string DescricaoErro { get; set; }
    }
}