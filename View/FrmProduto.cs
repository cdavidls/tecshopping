using System;
using System.Windows.Forms;
using Model;
using Controller;
using System.Data;

namespace View
{
    public partial class FrmProduto : Form
    {
        Produto produto = new Produto();


        public FrmProduto()
        {
            InitializeComponent();
            //Carrega o filtro todos por padrão.
            cbxFiltro.SelectedItem = "Todos";
            
            CarregarProdutos();          
        }

        void CarregarDadosProduto()
        {
            // Montamos o objeto Produto com as informações para enviar para a controller.          
            if (!string.IsNullOrWhiteSpace(txtCodigo.Text))
                produto.Codigo = Convert.ToInt32(txtCodigo.Text);

            produto.CodigoBarras = txtCodigoBarras.Text;
            produto.Descricao = txtDescricao.Text;
            produto.UnidadeMedida = cbxUnidadeMedida.SelectedItem.ToString();
            produto.QtdMinima = (int)nudQtdeMinima.Value;
            produto.QtdAtual = (int)nudQtdeAtual.Value;
            produto.QtdMaxima = (int)nudQtdeMaxima.Value;
            produto.CustoUnitario = Convert.ToDouble(txtValorUnitario.Text);
            produto.PercentualLucro = Convert.ToDouble(txtPercentualLucro.Text);
            produto.PrecoVenda = Convert.ToDouble(txtPreco.Text);
            //IF ternário.
            produto.Ativo = ckbAtivo.Checked ? "Sim" : "Não";
        }



        private void btnNovo_Click(object sender, EventArgs e)
        {
            if (btnNovo.Text == "Novo")
            {
                LimparCampos();
                btnNovo.Text = "Salvar";
                btnNovo.Image = Properties.Resources.tick;
                gbxInformacoesProduto.Enabled = true;
                btnAtualizar.Enabled = false;
                txtCodigoBarras.Focus();
                gbxProdutosCadastrados.Enabled = false;
                nudQtdeAtual.Enabled = true;
            }
            else
            {
                if (Validar())
                {
                    CarregarDadosProduto();

                    // Enviar para a controller salvar no banco de dados.
                    ControllerProduto controllerProduto = new ControllerProduto();
                    int codigo = controllerProduto.Persistir(produto, "cadastrar");

                    if (codigo > 0)
                    {
                        MessageBox.Show("PRODUTO CADASTRADO COM SUCESSO!");
                        LimparCampos();
                        CarregarProdutos();
                    }
                }
            }
        }

        void CarregarProdutos()
        {
            try
            {
                ControllerProduto controllerProduto = new ControllerProduto();
                DataTable dt = new DataTable();
                dt = controllerProduto.Carregar(txtPesquisa.Text, cbxFiltro.SelectedItem.ToString());
                dgvDados.DataSource = dt;
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            CarregarProdutos();
        }

        private void cbxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarProdutos();
        }

        private void dgvDados_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Você precisa selecionar um produto da lista!");
            }
            else
            {
                txtCodigo.Text = dgvDados.CurrentRow.Cells["codigo"].Value.ToString();
                txtCodigoBarras.Text = dgvDados.CurrentRow.Cells["codigobarras"].Value.ToString();
                txtDescricao.Text = dgvDados.CurrentRow.Cells["descricao"].Value.ToString();
                cbxUnidadeMedida.SelectedItem = dgvDados.CurrentRow.Cells["unidademedida"].Value.ToString();
                nudQtdeMinima.Value = (int)dgvDados.CurrentRow.Cells["qtdminima"].Value;
                nudQtdeAtual.Value = (int)dgvDados.CurrentRow.Cells["qtdatual"].Value;
                nudQtdeMaxima.Value = (int)dgvDados.CurrentRow.Cells["qtdmaxima"].Value;
                txtValorUnitario.Text = dgvDados.CurrentRow.Cells["custounitario"].Value.ToString();
                txtPercentualLucro.Text = dgvDados.CurrentRow.Cells["percentuallucro"].Value.ToString();
                txtPreco.Text = dgvDados.CurrentRow.Cells["precovenda"].Value.ToString();

                if (dgvDados.CurrentRow.Cells["ativo"].Value.ToString() == "Sim")
                    ckbAtivo.Checked = true;
                else
                    ckbAtivo.Checked = false;

                btnAtualizar.Enabled = true;
            }
        }

        bool Validar()
        {
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
            {
                errorProvider.SetError(txtCodigoBarras, "Informe o código de barras!");
                errorProvider.SetIconPadding(txtCodigoBarras, -20);
                txtCodigoBarras.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                errorProvider.SetError(txtDescricao, "Informe a descrição do produto!");
                errorProvider.SetIconPadding(txtDescricao, -20);
                txtDescricao.Focus();
                return false;
            }
            if (cbxUnidadeMedida.SelectedIndex < 0)
            {
                errorProvider.SetError(cbxUnidadeMedida, "Selecione a unidade de medida!");
                errorProvider.SetIconPadding(cbxUnidadeMedida, -40);
                cbxUnidadeMedida.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtValorUnitario.Text))
            {
                errorProvider.SetError(txtValorUnitario, "Informe o valor unitário!");
                errorProvider.SetIconPadding(txtValorUnitario, -20);
                txtValorUnitario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                errorProvider.SetError(txtPreco, "Informe o preço!");
                errorProvider.SetIconPadding(txtPreco, -20);
                txtPreco.Focus();
                return false;
            }      
            
            if(nudQtdeMinima.Value > nudQtdeMaxima.Value)
            {
                errorProvider.SetError(nudQtdeMaxima, "Você não pode cadastrar uma quantidade mínima superior a quantidade máxima");
                errorProvider.SetIconPadding(nudQtdeMaxima, -20);
                nudQtdeMaxima.Focus();
                return false;
            }

            return true;
        }

        void CalcularPercentual()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtValorUnitario.Text) && !string.IsNullOrEmpty(txtPreco.Text))
                {
                    txtPercentualLucro.Text = ((Convert.ToDouble(txtPreco.Text) - Convert.ToDouble(txtValorUnitario.Text)) / Convert.ToDouble(txtValorUnitario.Text) * 100).ToString("F2");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Digite apenas números! Erro: " + erro.Message);
            }
        }

        private void txtValorUnitario_TextChanged(object sender, EventArgs e)
        {
            CalcularPercentual();
        }

        private void txtPreco_TextChanged(object sender, EventArgs e)
        {
            CalcularPercentual();
        }

        void LimparCampos()
        {
            btnNovo.Text = "Novo";
            gbxInformacoesProduto.Enabled = false;
            txtCodigo.Text = "";
            txtCodigoBarras.Text = "";
            txtDescricao.Text = "";
            cbxUnidadeMedida.SelectedIndex = -1;
            nudQtdeMinima.Value = 0;
            nudQtdeMaxima.Value = 0;
            nudQtdeAtual.Value = 0;
            txtValorUnitario.Text = "";
            txtPreco.Text = "";
            txtPercentualLucro.Text = "";
            txtPesquisa.Focus();
            ckbAtivo.Checked = false;          
            gbxProdutosCadastrados.Enabled = true;

            btnNovo.Enabled = true;
            btnAtualizar.Enabled = false;
            btnAtualizar.Text = "Atualizar";

            btnNovo.Image = Properties.Resources.add;
            btnAtualizar.Image = Properties.Resources.page_white_edit;
            lblLembrete.Visible = false;

        }

        private void FrmProduto_Load(object sender, EventArgs e)
        {
            txtPesquisa.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (btnAtualizar.Text == "Atualizar")
            {
                btnAtualizar.Text = "Salvar";
                btnAtualizar.Image = Properties.Resources.tick;
                gbxInformacoesProduto.Enabled = true;
                btnNovo.Enabled = false;
                txtCodigoBarras.Focus();
                gbxProdutosCadastrados.Enabled = false;
                nudQtdeAtual.Enabled = false;
                lblLembrete.Visible = true;
            }
            else
            {
                if (Validar())
                {
                    CarregarDadosProduto();

                    // Enviar para a controller salvar no banco de dados.
                    ControllerProduto controllerProduto = new ControllerProduto();
                  int codigo = controllerProduto.Persistir(produto, "atualizar");

                    if (codigo > 0)
                    {
                        MessageBox.Show("PRODUTO ATUALIZADO COM SUCESSO!");
                        LimparCampos();
                        CarregarProdutos();
                    }
                }
            }
        }
    }
}
