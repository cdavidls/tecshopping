using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class AcessoDadosSqlServer
    {
        // String de conexão com o banco de dados.
        string parametrosConexao = string.Format(@"Data Source={0}; Initial Catalog={1};User ID={2}; Password={3}",
            Properties.Settings.Default.ServidorBanco,
            Properties.Settings.Default.NomeBanco,
            Properties.Settings.Default.UsuarioBanco,
            Properties.Settings.Default.SenhaBanco);


        SqlConnection conexao = null;

        public SqlConnection Conectar()
        {           
            try
            {
                conexao = new SqlConnection(parametrosConexao);
                conexao.Open();
                return conexao;
            }
            catch (Exception erro) 
            {
                conexao.Close();
                throw erro;                
            }
        }

        public void Fechar()
        {
            conexao.Close();
        }

    }
}
