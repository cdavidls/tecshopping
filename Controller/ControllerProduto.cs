using System;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace Controller
{
    public class ControllerProduto
    {
        public int Persistir(Produto produto, string tipo)
        {
            // Resgata a conexão com o banco de dados.
            AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();
            try
            {
                string instrucao = "";
                if (tipo == "cadastrar")
                {
                    instrucao = "INSERT INTO Produto (codigobarras, descricao, unidademedida, qtdminima, qtdmaxima, qtdatual, custounitario, percentuallucro, precovenda, ativo) VALUES (@codigobarras, @descricao, @unidademedida, @qtdminima, @qtdmaxima, @qtdatual, @custounitario, @percentuallucro, @precovenda, @ativo); SELECT SCOPE_IDENTITY();";
                }
                if (tipo == "atualizar")
                {
                    instrucao = "UPDATE Produto SET codigobarras=@codigobarras, descricao=@descricao, unidademedida=@unidademedida, qtdminima=@qtdminima, qtdmaxima=@qtdmaxima, qtdatual=@qtdatual, custounitario=@custounitario, percentuallucro=@percentuallucro, precovenda=@precovenda, ativo=@ativo WHERE codigo=@codigo";
                }

                SqlCommand command = new SqlCommand(instrucao, acessoDados.Conectar());
                if (tipo == "atualizar")
                {
                    command.Parameters.AddWithValue("@codigo", produto.Codigo);
                }
                command.Parameters.AddWithValue("@codigobarras", produto.CodigoBarras);
                command.Parameters.AddWithValue("@descricao", produto.Descricao);
                command.Parameters.AddWithValue("@unidademedida", produto.UnidadeMedida);
                command.Parameters.AddWithValue("@qtdminima", produto.QtdMinima);
                command.Parameters.AddWithValue("@qtdmaxima", produto.QtdMaxima);
                command.Parameters.AddWithValue("@qtdatual", produto.QtdAtual);
                command.Parameters.AddWithValue("@custounitario", produto.CustoUnitario);
                command.Parameters.AddWithValue("@percentuallucro", produto.PercentualLucro);
                command.Parameters.AddWithValue("@precovenda", produto.PrecoVenda);
                command.Parameters.AddWithValue("@ativo", produto.Ativo);

                // Recupera o código do produto cadastrado.
                return Convert.ToInt32(command.ExecuteNonQuery());
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                acessoDados.Fechar();
            }
        }

        public DataTable Carregar(string descricao, string ativo)
        {
            string instrucao;
            AcessoDadosSqlServer acessoDados = new AcessoDadosSqlServer();

            if (ativo == "Todos")
                instrucao = "SELECT * FROM Produto WHERE descricao LIKE '%" + descricao + "%'";
            else
                instrucao = "SELECT * FROM Produto WHERE descricao LIKE '%" + descricao + "%' AND ativo = '" + ativo + "'";
            try
            {                
                SqlCommand command = new SqlCommand(instrucao, acessoDados.Conectar());
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch(Exception erro)
            {
                throw erro;
            }
            finally
            {
                acessoDados.Fechar();
            }
        }
    }
}
