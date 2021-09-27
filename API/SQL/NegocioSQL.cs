using CSU.API.NFE.RetornaNFE.Entidades;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CSU.API.NFE.RetornaNFE.SQL
{
    public class NegocioSQL
    {
        #region [Propriedades]

        private string _connectionStringSQL;

        #endregion

        #region [Construtores]

        /// <summary>
        /// Construtor
        /// </summary>
        public NegocioSQL(IConfiguration iconfiguration)
        {
            try
            {
                _connectionStringSQL = iconfiguration.GetConnectionString("Default");
            }
            catch (Exception erro)
            {
                throw erro;
            }
        }

        #endregion

        #region [ConsultaNFE]

        /// <summary>
        /// CONSULTA 01 – ConsultaNFE
        /// </summary>
        /// <param name="mesBusca"></param>
        /// <returns>Lista de NFE'S</returns>
        public List<EstruturaNFE> ConsultaNFE(int mesBusca)
        {
            List<EstruturaNFE> ListaNotasFiscais = new List<EstruturaNFE>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionStringSQL))
                {
                    SqlCommand cmd = new SqlCommand("SP_RetornaNF_Por_Mes", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter _mesBusca = cmd.Parameters.AddWithValue("@MesBusca", mesBusca);
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ListaNotasFiscais.Add(new EstruturaNFE
                        {
                            COD_NOTA = rdr[0].ToString(),
                            COD_VENDA = rdr[1].ToString(),
                            DESTINATARIO_REMETENTE = rdr[2].ToString(),
                            DT_EMISSAO = rdr[3].ToString(),
                            DT_SAIDA_ENTRADA = rdr[4].ToString(),
                            NUM_NOTA = rdr[5].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na RetornaNFE – (SQL_SERVER) " + ex.Message);
            }

            return ListaNotasFiscais;
        }

        #endregion
    }
}