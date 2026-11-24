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
    public partial class frmCalculadora : Form
    {
        clsCalculadora calc = new clsCalculadora();
        public double primerNumero = 0;
        public string operador = "";
        public bool limpiarPantalla = false;

        public frmCalculadora()
        {
            InitializeComponent();
            txtPantalla.Text = "0";
        }
        private void IngresarNumero(object sender, EventArgs e)
        {
            Button boton = (Button)sender;

            if (txtPantalla.Text == "0" || limpiarPantalla)
            {
                txtPantalla.Text = boton.Text; //Escribir un nuevo numero
                limpiarPantalla = false;
            }
            else
            {
                txtPantalla.Text += boton.Text; //Concatenar numeros
            }
        }
        private void IngresarOperador(object sender, EventArgs e)
        {
            Button boton = (Button)sender;

            primerNumero = Convert.ToDouble(txtPantalla.Text);

            operador = boton.Text;
            txtPantalla.Text = operador;

            limpiarPantalla = true;
        }
        private void btnIgual_Click(object sender, EventArgs e)
        {   
            double segundoNumero = 0;
            double resultado = 0;

            //Validar en los casos donde no se necesite un segundo numero
            if (double.TryParse(txtPantalla.Text, out double numero))
            {
                segundoNumero = numero;
            }

            switch (operador)
            {
                case "+":
                    resultado = calc.Sumar(primerNumero, segundoNumero);
                    break;
                case "-":
                    resultado = calc.Restar(primerNumero, segundoNumero);
                    break;
                case "X":
                    resultado = calc.Multiplicar(primerNumero, segundoNumero);
                    break;
                case "/":
                    resultado = calc.Dividir(primerNumero, segundoNumero);
                    break;
                case "%":
                    resultado = calc.Porcentaje(primerNumero, segundoNumero);
                    break;
                case "1/x":
                    resultado = calc.Inverso(primerNumero);
                    break;
                case "x²":
                    resultado = calc.Cuadrado(primerNumero);
                    break;
                case "²√x":
                    resultado = calc.RaizCuadrada(primerNumero);
                    break;
                case "+/-":
                    resultado = calc.PositivoNegativo(primerNumero);
                    break;
            }

            txtPantalla.Text = resultado.ToString();
        }
        private void btnComa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPantalla.Text))
            {
                txtPantalla.Text = "0,";
                return;
            }
            if (!txtPantalla.Text.Contains(","))
            {
                txtPantalla.Text += ",";
            }
        }
        private void btnBorrarEntrada_Click(object sender, EventArgs e)
        {
            txtPantalla.Text = "0";
            limpiarPantalla = true;
        }
        private void btnBorrarTodo_Click(object sender, EventArgs e)
        {
            txtPantalla.Text = "0";
            primerNumero = 0;
            operador = "";
            limpiarPantalla = true;
        }
        private void btnRetroceso_Click(object sender, EventArgs e)
        {
            if (txtPantalla.Text.Length > 1)
            {
                txtPantalla.Text = txtPantalla.Text.Substring(0, txtPantalla.Text.Length - 1);
            }
            else
            {
                txtPantalla.Text = "0";
            }
        }
        private void frmCalculadora_Load(object sender, EventArgs e)
        {

        }
        private void btnGuardarMemoria_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtPantalla.Text, out double numero))
            {
                calc.MemoriaStore(numero);
            }
        }
        private void btnRecuperarMemoria_Click(object sender, EventArgs e)
        {
            txtPantalla.Text = calc.MemoriaRead().ToString();
        }

        private void btnSumarMemoria_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtPantalla.Text, out double numero))
            {
                calc.MemoriaAdd(numero);
            }
        }

        private void btnRestarMemoria_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtPantalla.Text, out double numero))
            {
                calc.MemoriaSubtract(numero);
            }
        }
        private void btnLimpiarMemoria_Click(object sender, EventArgs e)
        {
            calc.MemoriaClear();
        }
    }
}
