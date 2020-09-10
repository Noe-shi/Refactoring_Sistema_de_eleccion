﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq; 

namespace BlocNotasToDatagridview
{
    public partial class Form1 : Form
    {
        //Instancia de la clase Leer
        Leer l = new Leer();
        //Alamcena la ruta del archivo .txt
        public string ARCHIVO = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Abre el openFileDialog y captura la ruta del bloc de notas'
        public void cargarArchivo()
        {
            try
            {
                this.openFileDialog1.ShowDialog();

                if (!string.IsNullOrEmpty(this.openFileDialog1.FileName))
                {
                    ARCHIVO = this.openFileDialog1.FileName;
                    l.lecturaArchivo(dataGridView1, ',', ARCHIVO);

                    dataGridView1.Columns[0].HeaderText = "Nombre";
                    dataGridView1.Columns[1].HeaderText = "Escuela";
                    dataGridView1.Columns[2].HeaderText = "Estado";

                    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9.75F, FontStyle.Bold);

                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[4].Visible = false;

                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    chk.HeaderText = "Elegir";
                    dataGridView1.Columns.Add(chk);



                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            cargarArchivo();
        }
        int contador = 0;
        string[] nombres = new string[20];


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) 
        {
            
                int posicion = -1;
                string nombre = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                bool encontradoNombre = false;
                if (contador > 0)
                {
                    for (int i = 0; i < contador; i++)
                    {
                        if (nombres[i].Equals(nombre))
                        {
                            nombres[i] = null;
                            contador--;
                            MessageBox.Show("" + contador);
                            encontradoNombre = true;
                            posicion = i;
                        }
                    }
                }
                if (encontradoNombre == false)
                {
                    nombres[contador] = nombre;
                    contador++;
                    MessageBox.Show("" + contador);
                }
                if (encontradoNombre)
                {
                    for (int j = posicion; j < contador; j++)
                    {
                        if (posicion != 19 && nombres[j + 1] != null)
                        {
                            String aux = nombres[j + 1];
                            nombres[j] = aux;
                        }
                    }
                }

            if (contador == 10)
            {
                btnTerminar.Visible = true;

            }
            else
            {
                btnTerminar.Visible = false;
            }

            

            //    if (contador < 10)
            //    {
            //        contador += 1;
            //        string a = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //        nombres[contador] = a;
            //        MessageBox.Show(""+contador);

            //        if (contador == 10)
            //        {
            //            MessageBox.Show("Usted ya ha seleccionado 10 participantes.");
            //            btnTerminar.Visible = true;
            //        }
            //if (contador > 10)
            //{
            //    contador -= 1;
            //    string b = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //    nombres[contador] = b;
            //}
            // }


        }


        Random rnd = new Random();
        List<int> listaNoRepetidos = new List<int>();
        int IntialCount;
        int contValor = 0;
        ParejasAleatorias Parejas = new ParejasAleatorias();
        int valor1 = 0;
        int valor2;

        int cont = 0;


        private void btnTerminar_Click(object sender, EventArgs e)
        {


            listaNoRepetidos.Clear();
            IntialCount = 1;
            var result = MessageBox.Show("A continuación se generá la lista de parejas con los " +
                "participantes seleccionados. ¿Desea continuar?", "Mensaje", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                while (IntialCount <= 10)
                {
                    //GENERAMOS EL NÚMERO ALEATORIO
                    rnd = new Random();
                    int aux = Convert.ToInt32(rnd.Next(0, contador));
                    //si no esta en la lista, lo anexamos y sí evitamos que salgan nombres
                    // o numero repetidos en el algoritmo
                    if (!listaNoRepetidos.Contains(aux))
                    {
                        listaNoRepetidos.Add(aux);
                        IntialCount++;
                        contValor++;
                        if (contValor == 1)
                        {
                            valor1 = aux;
                        }
                        if (contValor == 2)
                        {
                            valor2 = aux;
                            contValor = 0;
                            Parejas.TablaParejas.Rows.Add(nombres[valor1], nombres[valor2]);
                        }
                    }

                }
               
                Parejas.Show();
                
            }



        }

        private void dataGridView1_CellValeChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private bool encontrado(String[] nombres, String nombre, int contador)
        {
            if (contador!=0)
            {
                for (int i = 0; i < nombres.Length; i++)
                {
                    if (nombres[i].Equals(nombre))
                    {
                        nombres[i] = null;
                        MessageBox.Show("Eliminando a " + nombre);
                        organizar(nombres, i);
                        return true;
                    }
                }
            }
            nombres[contador] = nombre;
            MessageBox.Show("Agregando a "+ nombre);
            return false;
        }

        private void organizar(String[] nombres, int i)
        {
            for (int j = i; j<nombres.Length; j++)
            {
                if (i!=9 && nombres[i+1]!=null)
                {
                    String aux = nombres[i + 1];
                    nombres[i] = aux;
                }
            }
        }
    }
}
