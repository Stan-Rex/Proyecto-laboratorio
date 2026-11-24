using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteca;

namespace Escritorio
{
    public partial class frmAutenticacion : Form
    {
        public frmAutenticacion()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            clsAutenticacion login = new clsAutenticacion();
            string usuarioIngresado = txtUsuario.Text;
            string clave = txtContraseña.Text;

            if (login.Autenticacion(usuarioIngresado, clave))
            {
                frmPrincipal formPrincipal = new frmPrincipal();
                formPrincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAutenticacion_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }
    }
}
