using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Escritorio
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void calculadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCalculadora calculadora = new frmCalculadora();
            calculadora.MdiParent = this;
            calculadora.Show();
        }
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAutenticacion autenticacion = new frmAutenticacion();
            autenticacion.Show();
            this.Hide();
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAcercaDe ayuda = new frmAcercaDe();
            ayuda.MdiParent = this;
            ayuda.Show();
        }

        private void digitoVerificadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDigitoVerificador digitoVerificador = new frmDigitoVerificador();
            digitoVerificador.MdiParent = this;
            digitoVerificador.Show();
        }

        private void operacionesBasicasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOperacionesBasicas opBasicas = new frmOperacionesBasicas();
            opBasicas.MdiParent = this;
            opBasicas.Show();
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
