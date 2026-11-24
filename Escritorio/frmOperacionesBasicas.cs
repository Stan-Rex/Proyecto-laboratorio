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
    public partial class frmOperacionesBasicas : Form
    {
        public frmOperacionesBasicas()
        {
            InitializeComponent();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            clsOperaciones operaciones = new clsOperaciones();

            double numero1 = Convert.ToDouble(txtNum1.Text);
            double numero2 = Convert.ToDouble(txtNum2.Text);

            try
            {
                double suma = operaciones.Sumar(numero1, numero2);
                double resta = operaciones.Restar(numero1, numero2);
                double multiplicacion = operaciones.Multiplicar(numero1, numero2);
                double division = operaciones.Dividir(numero1, numero2);

                string mensaje =
                $"Suma: {suma}\n" +
                $"Resta: {resta}\n" +
                $"Multiplicacion: {multiplicacion}\n" +
                $"Division: {division}";

                MessageBox.Show(mensaje, "Resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("No se puede dividir por 0", "Resultados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNum1.Text = "";
            txtNum2.Text = "";
            txtNum1.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
