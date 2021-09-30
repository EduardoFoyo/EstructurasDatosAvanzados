using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditordeGrafos
{
    public partial class AtributosGrafo : Form
    {
        public AtributosGrafo(Graph graph)
        {
            InitializeComponent();
            int cont_grado_grafo = 0;

            List<Edge> lista = graph.edgesList;
            foreach (var dato in lista)
            {
                lblAristas.Text = lblAristas.Text + dato.Name + " = (" +  dato.Source.Name + " , "+ dato.Destiny.Name + ")\r";
            }

            foreach (NodeP nodo in graph)
            {
                lblNodos.Text = lblNodos.Text + nodo.Name + "\r";
                if (graph.EdgeIsDirected)
                {
                    lblGrado.Text = lblGrado.Text + "-" + nodo.DegreeIn + " // ";
                    lblGrado.Text = lblGrado.Text + "+" + nodo.DegreeEx + "\r";
                    cont_grado_grafo += nodo.DegreeIn;
                    cont_grado_grafo += nodo.DegreeEx;
                }
                else
                {
                    lblGrado.Text = lblGrado.Text + nodo.Degree + "\r";
                    cont_grado_grafo += nodo.relations.Count;
                }
            }   
            lblGradoGrafo.Text = cont_grado_grafo.ToString();
        }
    }
}
