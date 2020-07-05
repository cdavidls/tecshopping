using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;

namespace View
{
    public partial class FrmConsultaVendas : Form
    {
        public FrmConsultaVendas()
        {
            InitializeComponent();
            cbxFiltro.SelectedItem = "Todas";
        }

        void Carregar()
        {
            ControllerVenda controllerVenda = new ControllerVenda();

            dgvDados.DataSource = controllerVenda.Carregar(cbxFiltro.SelectedItem.ToString(), dtpInicio.Value.ToShortDateString(), dtpFim.Value.ToShortDateString());

        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            Carregar();
        }

        private void dtpFim_ValueChanged(object sender, EventArgs e)
        {
            Carregar();
        }

        private void cbxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carregar();
        }

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            if (dgvDados.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Você precisa selecionar uma venda caso queira cancelar!");
            }
            else
            {
                int codigoVenda = Convert.ToInt32(dgvDados.CurrentRow.Cells["codigo"].Value);
                string status = dgvDados.CurrentRow.Cells["ativo"].Value.ToString();

                if (status == "Venda Cancelada")
                {
                    MessageBox.Show("Esta venda já está cancelada!");
                    return;
                }

                DialogResult result = MessageBox.Show("Deseja realmente cancelar essa venda?", "Cancelar Venda", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ControllerVenda controllerVenda = new ControllerVenda();
                    bool retorno = controllerVenda.Cancelar(codigoVenda);

                    if (retorno == true)
                    {
                        MessageBox.Show("Venda Cancelada com sucesso!");
                        Carregar();
                    }
                }
            }
        }
    }
}
