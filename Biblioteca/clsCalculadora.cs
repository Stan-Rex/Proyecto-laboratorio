using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca
{
    public class clsCalculadora
    {
        // Atributos privados para el estado y la memoria
        private double valorActual;
        private double memoria;
        //Puente entre los atributos privados para obtener y cambiar su valor
        public double ValorActual
        {
            get { return valorActual; }
            set { valorActual = value; }
        }

        public double Memoria
        {
            get { return memoria; }
        }
        //Inicializar las variables en 0 para que la calculadora empiece limpia
        public clsCalculadora()
        {   
            valorActual = 0;
            memoria = 0;
        }

        // --- OPERACIONES BÁSICAS ---
        public double Sumar(double a, double b) => a + b;
        public double Restar(double a, double b) => a - b;
        public double Multiplicar(double a, double b) => a * b;

        public double Dividir(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException("No se puede dividir por cero.");
            return a / b;
        }

        // --- FUNCIONES ESTÁNDAR (Visibles en la imagen) ---
        public double Porcentaje(double baseNum, double porcentaje) => (baseNum * porcentaje) / 100.0;

        public double Inverso(double numero)
        {
            if (numero == 0) throw new DivideByZeroException("No se puede dividir por cero.");
            return 1.0 / numero;
        }

        public double Cuadrado(double numero) => Math.Pow(numero, 2);

        public double RaizCuadrada(double numero)
        {
            if (numero < 0) throw new ArgumentException("Entrada no válida.");
            return Math.Sqrt(numero);
        }
        public double PositivoNegativo(double numero)
        {
            if (numero == 0) throw new ArgumentException("Entrada no valida.");
            return numero * -1;
        }
        // --- GESTIÓN DE MEMORIA (MC, MR, M+, M-, MS) ---
        public void MemoriaClear() => memoria = 0;

        public double MemoriaRead() => memoria;

        public void MemoriaStore(double valor) => memoria = valor;

        public void MemoriaAdd(double valor) => memoria += valor;

        public void MemoriaSubtract(double valor) => memoria -= valor;
    }
}
