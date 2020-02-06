using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TuringComplementoBinario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtCadena_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void SoloNumeros(KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números 
            if (Char.IsDigit(e.KeyChar))
            {
                
                if(e.KeyChar >= '2' && e.KeyChar <= '9')
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan 
                e.Handled = true;
            }
        }

        private void txtCadena_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtComplemento_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decodificar(txtCadena.Text);
        }


        //Declaración de las variables
        public static String cadena, cadena2, aux, reader;
        public static int longitud;
        public String complemento = "";
        public String[] arreglo;
        public int apuntador = 1;

        //Metodo decodificar se encarga de crear el arreglo que contendrá la cadena
        public void decodificar(String dec)
        {
            cadena = txtCadena.Text;
            cadena2 = "#" + cadena + "#";
            longitud = cadena2.Length;
            arreglo = new String[longitud];
            for (int i = 0; i < longitud; i++)
            {
                arreglo[i] = "" + cadena2[i];
            }

            e0();
        }

        //El metodo e0 representa al estado 0, el estado inicial
        public void e0()
        {
            aux = arreglo[apuntador];
            if (aux.Equals("0"))
            {
                arreglo[apuntador] = "1";
                moverDerecha();
                e1();
            }
            else if (aux.Equals("1"))
            {
                arreglo[apuntador] = "0";
                moverDerecha();
                e1();
            }
            else if (aux.Equals("#"))
            {
                arreglo[apuntador] = "#";
                moverIzquierda();
                MessageBox.Show("Cadena vacia");
            }
            else
            {
                rechazar(aux);
            }

        }

        //El metodo e1 representa al estado 1
        public void e1()
        {
            aux = arreglo[apuntador];
            if (aux.Equals("0"))
            {
                arreglo[apuntador] = "1";
                moverDerecha();
                e1();
            }
            else if (aux.Equals("1"))
            {
                arreglo[apuntador] = "0";
                moverDerecha();
                e1();
            }
            else if (aux.Equals("#"))
            {
                arreglo[apuntador] = "#";
                moverIzquierda();
                e2();
            }
            else
            {
                rechazar(aux);
            }
        }

        //El metodo e2 representa al estado 2
        public void e2()
        {
            aux = arreglo[apuntador];
            if (aux.Equals("0"))
            {
                arreglo[apuntador] = "0";
                moverIzquierda();
                e2();
            }
            else if (aux.Equals("1"))
            {
                arreglo[apuntador] = "1";
                moverIzquierda();
                e2();
            }
            else if (aux.Equals("#"))
            {
                arreglo[apuntador] = "#";
                moverDerecha();
                e3();
            }
            else
            {
                rechazar(aux);
            }
        }

        //El metodo e3 representa al estado 3, el estado de aceptación
        public void e3()
        {

            for (int i = 1; i < longitud - 1; i++)
            {
                complemento = complemento + arreglo[i];
            }
            txtComplemento.Text = complemento;
        }



        //Mueve el puntero de la MT a la derecha
        public void moverDerecha()
        {
            apuntador++;
        }

        //Mueve el puntero de la MT a la izquierda
        public void moverIzquierda()
        {
            apuntador = apuntador - 1;
        }

        //Si entra en este estado, significa que se encontró un caracter no valido
        public void rechazar(String noval)
        {
            MessageBox.Show("Caracter no valido: " + noval);
        }


    }
}
