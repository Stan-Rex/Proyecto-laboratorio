using Biblioteca;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Escritorio
{
    public partial class frmDigitoVerificador : Form
    {
        bool editando = false;
        public frmDigitoVerificador()
        {
            InitializeComponent();
        }
        private bool ValidarCamposObligatorios()
        {
            errorProvider1.Clear();

            var camposObligatorios = new Dictionary<TextBox, string>
            {
                { txtNombres, "El nombre es obligatorio." },
                { txtApellidos, "El apellido es obligatorio." },
            };

            TextBox primerCampoVacio = null;

            foreach (var campo in camposObligatorios)
            {
                if (string.IsNullOrWhiteSpace(campo.Key.Text))
                {
                    errorProvider1.SetError(campo.Key, campo.Value);
                    if (primerCampoVacio == null)
                    {
                        primerCampoVacio = campo.Key;
                    } 
                }
            }

            if (primerCampoVacio != null)
            {
                primerCampoVacio.Focus();
                MessageBox.Show(
                    "Por favor, completa todos los campos obligatorios.",
                    "Campos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            return true;
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string cedula = txtCedula.Text;
            string apellidos = txtApellidos.Text;
            string nombres = txtNombres.Text;
            string fechaNacimiento = dtpFechaNacimiento.Value.ToShortDateString();
            string genero = cbxGenero.Text;

            bool yaExiste = false;

            if (cedula.Length == 10)
            {
                // Recorrer las filas del DataGridView
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    // Ignorar la fila nueva de edición si está habilitada en la tabla
                    if (fila.IsNewRow) continue;

                    // Obtener el valor de la celda especificada (por nombre o índice de columna)
                    string valorExistente = fila.Cells["colCedula"].Value?.ToString();

                    // Comparar el valor ignorando mayúsculas/minúsculas
                    if (string.Equals(valorExistente, cedula, StringComparison.OrdinalIgnoreCase))
                    {
                        yaExiste = true;
                        break; // Si lo encuentra, detiene la búsqueda
                    }
                }
                if (!ValidarCamposObligatorios())
                {
                    return;
                }
                // Validar el resultado
                if (yaExiste)
                {
                    MessageBox.Show("El registro ya existe.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    dataGridView1.Rows.Add(cedula, apellidos, nombres, fechaNacimiento, genero);
                    MessageBox.Show("Registro agregado con exito.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtApellidos.Enabled = false;
                    txtNombres.Enabled = false;
                    dtpFechaNacimiento.Enabled = false;
                    cbxGenero.Enabled = false;

                    btnIngresar.Enabled = false;

                    txtCedula.Text = "";
                    txtApellidos.Text = "";
                    txtNombres.Text = "";
                    dtpFechaNacimiento.Value = DateTime.Today;
                    cbxGenero.Text = "";
                }
            }
            else
            {
                MessageBox.Show("La cedula no es valida.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string cedula = txtCedula.Text.Trim(); // El dato que deseas verificar
            bool yaExiste = false;
            
            if (cedula.Length == 10)
            {
                // Recorrer las filas del DataGridView
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    // Ignorar la fila nueva de edición si está habilitada en la tabla
                    if (fila.IsNewRow) continue;

                    // Obtener el valor de la celda especificada (por nombre o índice de columna)
                    string valorExistente = fila.Cells["colCedula"].Value?.ToString();

                    // Comparar el valor ignorando mayúsculas/minúsculas
                    if (valorExistente == cedula)
                    {
                        yaExiste = true;

                        txtApellidos.Text = fila.Cells["colApellidos"].Value?.ToString();
                        txtNombres.Text = fila.Cells["colNombres"].Value?.ToString();

                        string fechaTexto = fila.Cells["colFechaNacimiento"].Value?.ToString();
                        if (DateTime.TryParse(fechaTexto, out DateTime fechaConvertida))
                        {
                            dtpFechaNacimiento.Value = fechaConvertida;
                        }
                        else
                        {
                            MessageBox.Show("El formato de la fecha no es válido.");
                        }

                        cbxGenero.Text = fila.Cells["colGenero"].Value?.ToString();

                        break; // Si lo encuentra, detiene la búsqueda
                    }
                }

                // Validar el resultado
                if (yaExiste)
                {
                    MessageBox.Show("Registro encontrado con exito.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnEditar.Enabled = true;
                    btnEliminar.Enabled = true;
                }
                else
                {
                    DialogResult resultado = MessageBox.Show("El registro no existe. ¿Desea agregar un nuevo registro?", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (resultado == DialogResult.OK)
                    {
                        txtApellidos.Enabled = true;
                        txtNombres.Enabled = true;
                        dtpFechaNacimiento.Enabled = true;
                        cbxGenero.Enabled = true;
                        btnIngresar.Enabled = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("La cedula no es valida.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si es un número o la tecla de borrar (Backspace), se permite
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                // Se bloquea cualquier otro carácter
                e.Handled = true;
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void frmDigitoVerificador_Load(object sender, EventArgs e)
        {
            txtApellidos.Enabled = false;
            txtNombres.Enabled = false;
            dtpFechaNacimiento.Enabled = false;
            cbxGenero.Enabled = false;

            btnGuardar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnIngresar.Enabled = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (editando == false)
            {
                txtCedula.Text = "";
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
       
            txtApellidos.Text = "";
            txtNombres.Text = "";
            dtpFechaNacimiento.Value = DateTime.Today;
            cbxGenero.Text = "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            editando = true;
            txtCedula.Enabled = false;
            txtApellidos.Enabled = true;
            txtNombres.Enabled = true;
            dtpFechaNacimiento.Enabled = true;
            cbxGenero.Enabled = true;

            btnGuardar.Enabled = true;
            btnConsultar.Enabled = false;
            btnIngresar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string cedula = txtCedula.Text.Trim(); // El dato que deseas verificar

            string apellidos = txtApellidos.Text;
            string nombres = txtNombres.Text;
            string fechaNacimiento = dtpFechaNacimiento.Value.ToShortDateString();
            string genero = cbxGenero.Text;

            if (cedula.Length == 10)
            {
                // Recorrer las filas del DataGridView
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    // Ignorar la fila nueva de edición si está habilitada en la tabla
                    if (fila.IsNewRow) continue;

                    if (!ValidarCamposObligatorios())
                    {
                        return;
                    }

                    // Obtener el valor de la celda especificada (por nombre o índice de columna)
                    string valorExistente = fila.Cells["colCedula"].Value?.ToString();

                    // Comparar el valor ignorando mayúsculas/minúsculas
                    if (string.Equals(valorExistente, cedula, StringComparison.OrdinalIgnoreCase))
                    {
                        fila.Cells["colApellidos"].Value = apellidos;
                        fila.Cells["colNombres"].Value = nombres;
                        fila.Cells["colFechaNacimiento"].Value = fechaNacimiento;
                        fila.Cells["colGenero"].Value = genero;

                        MessageBox.Show("Registro editado con exito.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        editando = false;

                        txtCedula.Enabled = true;
                        txtApellidos.Enabled = false;
                        txtNombres.Enabled = false;
                        dtpFechaNacimiento.Enabled = false;
                        cbxGenero.Enabled = false;

                        btnGuardar.Enabled = false;
                        btnEditar.Enabled = false;
                        btnConsultar.Enabled = true;

                        txtCedula.Text = "";
                        txtApellidos.Text = "";
                        txtNombres.Text = "";
                        dtpFechaNacimiento.Value = DateTime.Today;
                        cbxGenero.Text = "";

                        break; // Si lo encuentra, detiene la búsqueda
                    }
                }
            }
            else
            {
                MessageBox.Show("El registro no es valido.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string cedula = txtCedula.Text.Trim();

            if (cedula.Length != 10)
            {
                MessageBox.Show("La cedula debe contener 10 digitos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Desea eliminar este registro?.", "Atencion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            
            if(resultado == DialogResult.OK)
            {
                bool encontrado = false;
                
                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow fila = dataGridView1.Rows[i];

                    if (fila.IsNewRow) continue;

                    string valorExistente = fila.Cells["colCedula"].Value?.ToString();

                    if (valorExistente == cedula)
                    {
                        dataGridView1.Rows.Remove(fila);
                        encontrado = true;
                        break;
                    }
                }
                if (encontrado)
                {
                    MessageBox.Show("Registro eliminado con éxito.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCedula.Clear();
                    txtApellidos.Clear();
                    txtNombres.Clear();
                    dtpFechaNacimiento.Value = DateTime.Today;
                    cbxGenero.Text = "";

                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                }
                else
                {
                    MessageBox.Show("No se encontró ningún registro con esa cédula.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
