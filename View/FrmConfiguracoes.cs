using System;
using System.Windows.Forms;
using Model;
using Controller;

namespace View
{
    public partial class FrmConfiguracoes : Form
    {
        public FrmConfiguracoes()
        {
            InitializeComponent();
            Carregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (btnEditar.Text == "Editar")
            {
                btnEditar.Text = "Salvar";
                btnEditar.Image = Properties.Resources.tick;
                pnlDados.Enabled = true;
            }
            else
            {
                if (Validar())
                {
                    Configuracao configuracao = new Configuracao();
                    configuracao.ServidorBanco = txtServidor.Text.Trim();
                    configuracao.NomeBanco = txtBanco.Text.Trim();
                    configuracao.UsuarioBanco = txtUsuario.Text.Trim();
                    configuracao.SenhaBanco = txtSenha.Text.Trim();

                    ControllerConfiguracao controllerConfiguracao = new ControllerConfiguracao();

                    try
                    {
                        bool retorno = controllerConfiguracao.Salvar(configuracao);

                        if (retorno == true)
                        {
                            MessageBox.Show("Configurações salva com sucesso!");
                            Close();
                        }
                    }
                    catch (Exception erro)
                    {
                        MessageBox.Show("Não foi possível salvar as configurações. Detalhes: " + erro.Message);
                    }
                }
            }
        }

        bool Validar()
        {
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtServidor.Text))
            {
                errorProvider.SetError(txtServidor, "Informe o servidor do banco de dados!");
                errorProvider.SetIconPadding(txtServidor, -20);
                txtServidor.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBanco.Text))
            {
                errorProvider.SetError(txtBanco, "Informe o nome do banco de dados!");
                errorProvider.SetIconPadding(txtBanco, -20);
                txtBanco.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                errorProvider.SetError(txtUsuario, "Informe o nome do usuário com acesso ao banco de dados!");
                errorProvider.SetIconPadding(txtUsuario, -20);
                txtUsuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                errorProvider.SetError(txtSenha, "Informe a senha do usuário com acesso ao banco de dados!");
                errorProvider.SetIconPadding(txtSenha, -20);
                txtSenha.Focus();
                return false;
            }

            return true;
        }

        void Carregar()
        {
            ControllerConfiguracao controllerConfiguracao = new ControllerConfiguracao();
            Configuracao configuracao = new Configuracao();
            configuracao = controllerConfiguracao.Carregar();

            txtServidor.Text = configuracao.ServidorBanco;
            txtBanco.Text = configuracao.NomeBanco;
            txtUsuario.Text = configuracao.UsuarioBanco;
            txtSenha.Text = configuracao.SenhaBanco;
        }
    }
}
