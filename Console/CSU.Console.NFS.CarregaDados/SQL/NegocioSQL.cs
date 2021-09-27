using CSU.Console.NFS.CarregaDados.Entidades;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Text;

namespace CSU.Console.NFS.CarregaDados.SQL
{
    public class NegocioSQL
    {
        private string _connectionStringSQL;

        public NegocioSQL(IConfiguration iconfiguration)
        {
            _connectionStringSQL = iconfiguration.GetConnectionString("Default");
        }

        /// <summary>
        /// Consulta 1 – Busca Notas Fiscais (SP_RetornaNF_Por_Mes)
        /// </summary>
        /// <param name="MesBusca"></param>
        public List<NotaFiscal> RetornaNFE(int mesBusca)
        {
            List<NotaFiscal> ListaNotasFiscais = new List<NotaFiscal>();

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
                        ListaNotasFiscais.Add(new NotaFiscal
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
    }
}
