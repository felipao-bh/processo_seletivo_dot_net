
using Microsoft.Extensions.Configuration;
using System;
using System.IO;


namespace CSU.Console.NFS.CarregaDados
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CSU.Console.NFS.CarregaDados.CarregaDados carregaDados = new CSU.Console.NFS.CarregaDados.CarregaDados();

                carregaDados.Processar();
            }
            catch (Exception ex)
            {                
                Environment.Exit(999);
            }
        }
    }
}
