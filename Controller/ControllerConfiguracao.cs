using System;
using Model;

namespace Controller
{
    public class ControllerConfiguracao
    {
        public bool Salvar(Configuracao configuracao)
        {
            try
            {
                // Armazena configurações de conexão com o banco no arquivo de config do sistema.
                Properties.Settings.Default.ServidorBanco = configuracao.ServidorBanco;
                Properties.Settings.Default.NomeBanco = configuracao.NomeBanco;
                Properties.Settings.Default.UsuarioBanco = configuracao.UsuarioBanco;
                Properties.Settings.Default.SenhaBanco = configuracao.SenhaBanco;

                Properties.Settings.Default.Save();

                return true;
            }
            catch (Exception erro)
            {
                // Caso der erro, retorna para tela para mostrar o que rolou.
                throw new Exception(erro.Message);
            }
        }

        public Configuracao Carregar()
        {
            Configuracao configuracao = new Configuracao();
            configuracao.ServidorBanco = Properties.Settings.Default.ServidorBanco;
            configuracao.NomeBanco = Properties.Settings.Default.NomeBanco;
            configuracao.UsuarioBanco = Properties.Settings.Default.UsuarioBanco;
            configuracao.SenhaBanco = Properties.Settings.Default.SenhaBanco;

            return configuracao;
        }
    }
}
