using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditordeGrafos
{
    [Serializable()]

    public class Graph : List<NodeP>
    {
        private bool edgeNamesVisible;
        private bool edgeWeightVisible;
        private bool edgeIsDirected;
        private int nodeRadio;
        private int nodeBorderWidth;
        private int edgeWidth;
        private Color edgeColor;
        private Color nodeColor;
        private Color nodeBorderColor;
        private bool letter;
        private string name;
        public List<Edge> edgesList;
        public List<NodeP> list_nodo;
        public string Lista;
        public List<Edge> list_avance;
        public List<Edge> list_retroceso;
        public List<Edge> list_cruce;
        public List<Edge> list_bosque_abarcador;
        public int cont;
        public List<Edge> recorrido_arista;
        public Edge ab;
        public List<NodeP> list_arbol_solo;
        public List<Edge> EdgesList
        {
            get { return edgesList; }
            set { edgesList = value; }
        }

        public bool Letter
        {
            get { return letter; }
            set { letter = value; }
        }

        public bool EdgeIsDirected
        {
            get { return edgeIsDirected; }
            set { edgeIsDirected = value; }
        }

        public int EdgeWidth
        {
            get { return edgeWidth; }
            set { edgeWidth = value; }
        }

        public Color NodeBorderColor
        {
            get { return nodeBorderColor; }
            set { nodeBorderColor = value; }
        }

        public int NodeBorderWidth
        {
            get { return nodeBorderWidth; }
            set { nodeBorderWidth = value; }
        }

        public Color EdgeColor
        {
            get { return edgeColor; }
            set { edgeColor = value; }
        }

        public Color NodeColor
        {
            get { return nodeColor; }
            set { nodeColor = value; }
        }

        public int NodeRadio
        {
            get { return nodeRadio; }
            set { nodeRadio = value; }
        }

        public bool EdgeNamesVisible
        {
            get { return edgeNamesVisible; }
            set { edgeNamesVisible = value; }
        }

        public bool EdgeWeightVisible
        {
            get { return edgeWeightVisible; }
            set { edgeWeightVisible = value; }
        }


        public Graph()
        {
            EdgesList = new List<Edge>();
            edgeColor = Color.Black;
            letter = true;
            edgeNamesVisible = false;
            edgeWeightVisible = false;
            nodeColor = Color.White;
            nodeRadio = 30;
            nodeBorderWidth = 1;
            edgeWidth = 1;
            nodeBorderColor = Color.Black;
        }

        public Graph(Graph gr)
        {

            EdgesList = new List<Edge>();
            edgeColor = gr.EdgeColor;
            nodeColor = gr.nodeColor;
            nodeRadio = gr.NodeRadio;
            Edge k = new Edge();
            NodeP aux1, aux2;

            nodeBorderWidth = 1;
            edgeWidth = 1;
            nodeBorderColor = Color.Black;
            edgeNamesVisible = gr.EdgeNamesVisible;
            edgeWeightVisible = gr.EdgeWeightVisible;
            letter = gr.Letter;

            foreach (NodeP n in gr)
            {
                this.Add(new NodeP(n));
            }

            foreach (NodeP n in gr)
            {
                aux1 = Find(delegate (NodeP bu) { if (bu.Name == n.Name) return true; else return false; });
                foreach (NodeR rel in n.relations)
                {
                    aux2 = Find(delegate (NodeP je) { if (je.Name == rel.Up.Name) return true; else return false; });
                    aux1.InsertRelation(aux2, EdgesList.Count, false);
                }
            }
            //Agregar Aristas 
            foreach (Edge ar in gr.EdgesList)
            {
                aux1 = Find(delegate (NodeP bu) { if (bu.Name == ar.Source.Name) return true; else return false; });
                aux2 = Find(delegate (NodeP bu) { if (bu.Name == ar.Destiny.Name) return true; else return false; });
                k = new Edge(aux1, aux2, ar.Name)
                {
                    Weight = ar.Weight
                };
                //Manda llamar la funcion para añadir la arista
                AddEdge(k);
            }
        }

        public void AddNode(NodeP n)
        {
            Add(n);
        }

        public void AddEdge(Edge A)
        {
            EdgesList.Add(A);
        }

        public void RemoveEdge(Edge ar)
        {
            NodeR rel;
            rel = ar.Source.relations.Find(delegate (NodeR np) { if (np.Up.Name == ar.Destiny.Name) return true; else return false; });

            if (rel != null)
            {
                ar.Source.relations.Remove(rel);
                ar.Source.Degree--;
                ar.Destiny.Degree--;
                ar.Source.DegreeEx--;
                ar.Destiny.DegreeIn--;
            }
            if (!edgeIsDirected)
            {
                rel = ar.Destiny.relations.Find(delegate (NodeR np) { if (np.Up.Name == ar.Source.Name) return true; else return false; });

                if (rel != null)
                {
                    ar.Destiny.relations.Remove(rel);
                    ar.Destiny.DegreeEx--;
                    ar.Source.DegreeIn--;
                }
            }
            EdgesList.Remove(ar);
        }

        public void RemoveNode(NodeP rem)
        {
            NodeR rel;
            List<Edge> remove;
            remove = new List<Edge>();

            foreach (NodeP a in this)
            {
                rel = a.relations.Find(delegate (NodeR np) { if (np.Up.Name == rem.Name) return true; else return false; });
                if (rel != null)
                {
                    a.relations.Remove(rel);
                    a.Degree--;
                    a.DegreeEx--;
                    if (!edgeIsDirected || edgeIsDirected)
                    {
                        a.DegreeIn--;
                    }
                }
            }
            remove = EdgesList.FindAll(delegate (Edge ar) { if (ar.Source.Name == rem.Name || ar.Destiny.Name == rem.Name) return true; else return false; });
            if (remove != null)
                foreach (Edge re in remove)
                {
                    EdgesList.Remove(re);
                }
            this.Remove(rem);
        }

        // Regresa si dos list_nodo está conectados
        public NodeR Connected(NodeP a, NodeP b)
        {
            for (int i = 0; i < a.relations.Count; i++)
            {
                if (a.relations[i].Up == b)
                {
                    return a.relations[i];
                }
            }
            return null;
        }

        // Regresa la arista entre dos list_nodo que si se sabe que tiene aristas
        public Edge GetEdge(NodeP a, NodeP b)
        {
            for (int i = 0; i < this.EdgesList.Count; i++)
            {
                if (this.EdgesList[i].Source.Name == a.Name && this.EdgesList[i].Destiny.Name == b.Name ||
                        this.EdgesList[i].Source.Name == b.Name && this.EdgesList[i].Destiny.Name == a.Name)
                {
                    return (EdgesList[i]);
                }
            }
            return (null);
        }

        // Deselecciona todos los list_nodo
        public void UnselectAllNodes()
        {
            for (int k = 0; k < Count; k++)
            {
                this[k].Visited = false;
            }
        }

        // Deselecciona todas las aristas
        public void UnselectAllEdges()
        {
            foreach (Edge ed in EdgesList)
            {
                ed.Visited = false;
            }
        }

        // Verifica si el grafo es regular
        public bool IsRegular()
        {
            foreach (NodeP np in this)
            {
                if (np.Degree < Count - 1)
                {
                    return false;
                }
            }
            return true;
        }

        // Verifica que el grafo no dirigido este conectado
        public bool IsConnectedU()
        {
            foreach (NodeP np in this)
            {
                if (np.Degree == 0)
                {
                    return false;
                }
            }
            return true;
        }

        // Verifica que todas las aristas estén visitadas
        public bool AllEdgesVisited()
        {
            foreach (Edge ed in EdgesList)
            {
                if (ed.Visited == false)
                {
                    return false;
                }
            }
            return true;
        }

        // Verifica que todos los list_nodo estén visitados
        public bool AllNodesVisited()
        {
            foreach (NodeP np in this)
            {
                if (np.Visited == false)
                {
                    return false;
                }
            }
            return true;
        }

        /**
         * Realiza un recorrido en profundidad
         * 
         */
        private HashSet<string> recorridoProfundidad(NodeP raiz, out List<string> recorrido)
        {
            var visitados = new HashSet<string>();
            var pila = new Stack<string>();
            recorrido = new List<string>();

            pila.Push(raiz.Name);
            
            while (pila.Count > 0)
            {
                // Desencola al nodo padre.
                var root = pila.Pop();

                // Obtiene el nombre de los nodos adyacentes y filtra aquellos nodos
                // sin visitar
                var relaciones = from edge in EdgesList
                          where edge.Source.Name == root
                          where !visitados.Contains(edge.Destiny.Name)
                          orderby edge.Destiny.Name descending
                          select edge.Destiny.Name;

                // Agrega al recorrido al nodo, en caso de no haber sido visitado.
                if (!visitados.Contains(root))
                    recorrido.Add(root);

                // Agrega a los nodos visitados al nodo.
                visitados.Add(root);


                // Agrega todos los nodos relacionados a la cola.
                relaciones.ToList().ForEach((nodo) => pila.Push(nodo));
            }

            return visitados;
        }

        /**
         * Realiza un recorrido en amplitud
         */
        private HashSet<string> recorridoAmplitud(NodeP raiz, out List<string> recorrido)
        {
            var visitados = new HashSet<string>();
            var cola = new Queue<string>();
            recorrido = new List<string>();

            cola.Enqueue(raiz.Name);

            while (cola.Count > 0)
            {
                // Desencola al nodo padre.
                var root = cola.Dequeue();

                // Obtiene el nombre de los nodos adyacentes y filtra aquellos nodos
                // sin visitar
                var relaciones = from edge in EdgesList
                                 where edge.Source.Name == root
                                 where !visitados.Contains(edge.Destiny.Name)
                                 select edge.Destiny.Name;

                // Agrega al recorrido al nodo, en caso de no haber sido visitado.
                if (!visitados.Contains(root))
                    recorrido.Add(root);

                // Agrega a los nodos visitados al nodo.
                visitados.Add(root);


                // Agrega todos los nodos relacionados a la cola.
                relaciones.ToList().ForEach((nodo) => cola.Enqueue(nodo));
            }

            return visitados;
        }

        /**
         * Obtiene el bosquee abarcador por profundidad.
         */
        public void busquedaProfundidad()
        {
            // Conjunto, para los nodos que han sido visitados.
            var visitados = new HashSet<string>();

            foreach (var nodo in this)
            { 
                if (visitados.Contains(nodo.Name))
                    continue;

                // Obtiene los nodos recorridos en profundidad.
                var nodosRecorridos = recorridoProfundidad(nodo, out var lista);

                // Elimina del recorrido aquellos nodos que ya estén
                // en el conjunto de visitados.
                lista.RemoveAll((string nodoRecorrido) => visitados.Contains(nodoRecorrido));

                // Elimina del recorrido aquellos nodos que ya estén
                // en el conjunto de visitados.
                visitados.UnionWith(nodosRecorridos);
                MessageBox.Show(string.Join(",",lista.ToArray()));
            }
        }

        public void adf_algorithm(NodeP node, int rec, int res, Boolean arbol_activo)
        {
            int index = -1;
            List<NodeP> visitado = new List<NodeP>();
            List<String> nombres_list = new List<string>();
            List<NodeR> lista_ord_rel = new List<NodeR>();
            
            for (int i = 0; i < Count; i++)
            {
                if (this[i].name_number() == node.name_number())
                {
                    node = this[i];
                    this[i].Visited = true;
                    this[i].Num_rec = rec;
                    this[i].Num_res = res;
                    index = i;
                }
            }
            rec++;
            list_nodo.Add(this[index]);

            if (node.relations.Count == 0 && arbol_activo == false)
            {
                list_arbol_solo.Add(node);
            }
            else
            {
                Boolean solo = true;
                foreach (var item in node.relations)
                {
                    if (!item.Up.Visited)
                    {
                        solo = false;
                        break;
                    }
                }
                if (solo && arbol_activo == false)
                {
                    list_arbol_solo.Add(node);
                }
            }

            foreach (NodeR item in node.relations)
            {
                nombres_list.Add(item.Up.Name);
            }
            nombres_list.Sort();


            foreach (String name in nombres_list)
            {
                lista_ord_rel.Add(this[index].relations.Find(i => i.Up.Name.Contains(name)));
            }

            foreach (NodeR item in lista_ord_rel)
            {
                if (item.Up.Visited == false)
                {
                    recorrido_arista.Add(ab = new Edge(0, node, item.Up, res.ToString()));
                    adf_algorithm(item.Up, rec, res, true);
                }
            }


        }

        /**
         * Obtiene el bosquee abarcador por amplitud / anchura.
         */
        public void bpfrecorrido()
        {
            // Mensaje final:
            string resultado = "Resultado experimental: \n\n";

            // Conjunto, para los nodos que han sido visitados.
            var visitados = new HashSet<string>();

            foreach (var nodo in this)
            {
                if (visitados.Contains(nodo.Name))
                    continue;

                // Obtiene los nodos recorridos en profundidad.
                var nodosRecorridos = recorridoProfundidad(nodo, out var lista);

                // Elimina del recorrido aquellos nodos que ya estén
                // en el conjunto de visitados.
                lista.RemoveAll((string nodoRecorrido) => visitados.Contains(nodoRecorrido));

                // Elimina del recorrido aquellos nodos que ya estén
                // en el conjunto de visitados.
                visitados.UnionWith(nodosRecorridos);

                resultado += string.Join(",", lista.ToArray()) + "\n";
            }

            resultado += "Resultado actual de Foyo: \n\n";
            
            UnselectAllNodes();
            list_avance = new List<Edge>();
            list_retroceso = new List<Edge>();
            list_cruce = new List<Edge>();
            list_bosque_abarcador = new List<Edge>();
            list_nodo = new List<NodeP>();
            list_arbol_solo = new List<NodeP>();
            int j = 1;
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Visited == false)
                {
                    bosque_abarcador(this[i], 1, j++, false);
                }
            }

            foreach (Edge edge in list_bosque_abarcador)
            {
                for (int i = 0; i < list_avance.Count; i++)
                {
                    if (list_avance[i].Source.Name == edge.Source.Name && list_avance[i].Destiny.Name == edge.Destiny.Name)
                        list_avance.RemoveAt(i);
                }
            }

            imprimeBosqueAbarcador(j, resultado);
        }

        public void imprimeBosqueAbarcador(int j, string resExperimental)
        {
            String recorrido = "";

            if (list_retroceso.Count > 0)
            {
                recorrido = recorrido + "Retroceso = ";
                foreach (var retroceso in list_retroceso)
                {
                    recorrido = recorrido + " (" + retroceso.Source.Name + "," + retroceso.Destiny.Name + ") ";
                }
                recorrido = recorrido + "\n";
            }
            if (list_cruce.Count > 0)
            {
                recorrido = recorrido + "Cruce = ";
                foreach (var cruce in list_cruce)
                {
                    recorrido = recorrido + " (" + cruce.Source.Name + "," + cruce.Destiny.Name + ") ";
                }
                recorrido = recorrido + "\n";
            }
            if (list_avance.Count > 0)
            {
                recorrido = recorrido + "Avance = ";
                foreach (var avance in list_avance)
                {
                    recorrido = recorrido + " (" + avance.Source.Name + "," + avance.Destiny.Name + ") ";
                }
                recorrido = recorrido + "\n";
            }
            MessageBox.Show(resExperimental + recorrido);
        }

        public void bosque_abarcador(NodeP nodo, int rec, int n, Boolean arbol_activo)
        {
            int index = -1;
            List<String> nombres_list = new List<string>();
            List<NodeR> lista_ord_rel = new List<NodeR>();
            List<NodeP> visitado = new List<NodeP>();
            for (int i = 0; i < Count; i++)
            {
                if (this[i].name_number() == nodo.name_number())
                {
                    nodo = this[i];
                    this[i].Visited = true;
                    this[i].Num_rec = rec;
                    this[i].Num_res = n;
                    index = i;
                }
            }
            rec++;
            list_nodo.Add(this[index]);

            if (nodo.relations.Count == 0 && arbol_activo == false)
            {
                list_arbol_solo.Add(nodo);
            }
            else
            {
                Boolean solo = true;
                foreach (var item in nodo.relations)
                {
                    if (!item.Up.Visited)
                    {
                        solo = false;
                        break;
                    }
                }
                if (solo && arbol_activo == false)
                {
                    list_arbol_solo.Add(nodo);
                }
            }





            foreach (NodeR item in nodo.relations)
            {
                nombres_list.Add(item.Up.Name);
            }
            nombres_list.Sort();

            foreach (String name in nombres_list)
            {
                lista_ord_rel.Add(this[index].relations.Find(i => i.Up.Name.Contains(name)));
            }

            foreach (NodeR item in lista_ord_rel)
            {
                if (item.Up.Visited == false)
                {
                    list_bosque_abarcador.Add(ab = new Edge(0, nodo, item.Up, "Arr" + n.ToString()));
                    bosque_abarcador(item.Up, rec, n, true);
                }
                if (item.Up.Num_rec - nodo.Num_rec > 0 && item.Up.Num_res == nodo.Num_res)
                    list_avance.Add(ab = new Edge(0, nodo, item.Up, "Avance"));
                if (item.Up.Num_rec - nodo.Num_rec < 0 && item.Up.Num_res == nodo.Num_res)
                    list_retroceso.Add(ab = new Edge(0, nodo, item.Up, "Retroceso"));
                if (item.Up.Num_res != nodo.Num_res)
                    list_cruce.Add(ab = new Edge(0, nodo, item.Up, "Cruce"));
            }

        }

        public void euler()
        {
            List<Edge> list_aristas_euler = new List<Edge>();
            List<NodeP> list_nodo_euler = new List<NodeP>();
            string recorrido;
            NodeP d;
            this.UnselectAllEdges();
            if (ExisteCircuitoEuler())
            {
                circuitoEuler(list_aristas_euler, this.First(), this.First(), false, list_nodo_euler);
                recorrido = stringRecorrido(list_nodo_euler);
                MessageBox.Show("Circuito: \n" + recorrido);
            }
            else
            {
                if (ExisteCaminoEuler())
                {
                    d = GetOddDegNode();
                    caminoEuler(list_aristas_euler, d, list_nodo_euler);
                    recorrido = stringRecorrido(list_nodo_euler);
                    MessageBox.Show("Camino: \n" + recorrido);
                }
                else
                {
                    MessageBox.Show("GRAFO no contiene CIRCUITO ni CAMINO");
                }
            }
        }

        private bool circuitoEuler(List<Edge> circuito_aristas, NodeP origen, NodeP vis, bool band, List<NodeP> nodosRecorridos)
        {
            Edge circuito = new Edge();
            nodosRecorridos.Add(vis);
            foreach (NodeR r in vis.relations)
            {
                circuito = this.GetEdge(vis, r.Up);
                if (!circuito.Visited)
                {
                    circuito.Visited = true;
                    circuito_aristas.Add(circuito);
                    band = circuitoEuler(circuito_aristas, origen, r.Up, band, nodosRecorridos);
                    if (circuito_aristas.Count != this.edgesList.Count)
                    {
                        circuito_aristas.Remove(circuito);
                        nodosRecorridos.RemoveAt(nodosRecorridos.Count - 1);
                        circuito.Visited = false;
                        band = false;
                    }
                    else
                    {
                        if (!band)
                        {
                            circuito_aristas.Remove(circuito);
                            nodosRecorridos.Remove(vis);
                            circuito.Visited = false;
                        }
                        return band;
                    }
                }
            }
            if (vis == origen)
                band = true;
            else
                nodosRecorridos.Remove(vis);

            return band;
        }

        private void caminoEuler(List<Edge> camino_aristas, NodeP w, List<NodeP> recorrido)
        {
            Edge camino = new Edge();
            recorrido.Add(w);
            foreach (NodeR item in w.relations)
            {
                camino = this.GetEdge(w, item.Up);
                if (!camino.Visited)
                {
                    camino_aristas.Add(camino);
                    camino.Visited = true;
                    caminoEuler(camino_aristas, item.Up, recorrido);
                    if (camino_aristas.Count != this.edgesList.Count)
                    {
                        camino_aristas.Remove(camino);
                        recorrido.RemoveAt(recorrido.Count - 1);
                        camino.Visited = false;
                    }
                    else
                        return;
                }
            }
        }

        private bool ExisteCircuitoEuler()
        {
            bool circuitoE = true;

            foreach (NodeP p in this)
            {
                if (!this.EdgeIsDirected)
                {
                    if (p.Degree % 2 != 0)
                    {
                        circuitoE = false;
                        break;
                    }
                }
                else
                {
                    if ((p.DegreeEx - p.DegreeIn) % 2 != 0)
                    {
                        circuitoE = false;
                        break;
                    }
                }
            }

            return circuitoE;
        }

        private bool ExisteCaminoEuler()
        {
            bool caminoE = true;
            int countOddNodes = 0;

            foreach (NodeP p in this)
            {
                if (!this.EdgeIsDirected)
                {
                    if (p.Degree % 2 != 0)
                    {
                        countOddNodes++;
                    }
                }
                else
                {
                    if ((p.DegreeEx - p.DegreeIn) % 2 != 0)
                    {
                        countOddNodes++;
                    }
                }
                caminoE = (countOddNodes == 2) ? true : false;
            }

            return caminoE;
        }

        private string stringRecorrido(List<NodeP> nodes)
        {
            string recorrido = "";
            int cicl = 0;
            foreach (NodeP node in nodes)
            {
                cicl++;
                recorrido += node.Name;
                if (nodes.Last() != node || cicl < nodes.Count)
                {
                    recorrido += " -> ";
                }
            }
            return recorrido;
        }

        private NodeP GetOddDegNode()
        {
            NodeP Odd = new NodeP();

            foreach (NodeP p in this)
            {
                if (!this.EdgeIsDirected)
                {
                    if (p.Degree % 2 != 0)
                    {
                        Odd = p;
                        break;
                    }
                }
                else
                {
                    if ((p.DegreeEx - p.DegreeIn) % 2 != 0 && p.DegreeEx > p.DegreeIn)
                    {
                        Odd = p;
                        break;
                    }
                }
            }

            return Odd;
        }

        /**
         * Construye el árbol de expansión mínima de Prim.
         *
         */
        public void prim()
        {
            // Conjunto de vértices, que componen al árbol abarcador.
            var T = new HashSet<Edge>();

            // Conjunto de vértices que son visitados, al construir 
            // el árbol.
            var U = new HashSet<string>(){ this[0].Name };

            // Conjunto de vértices del grafo.
            var V = new HashSet<string>(from node in this select node.Name);

            // Lista de aristas auxiliar (para no alterar las referencias)
            // que usa el grafo.
            var E = new List<Edge>(from edge in EdgesList select edge);
            
            // Cadena para retornar el resultado.
            string cad = "";

            // Mientras U != V
            while (!U.SetEquals(V))
            {
                // Obtiene la arista más corta, tal que su nodo 
                // origen esté en U y su nodo destino esté en la
                // substracción de V y U (V - U)
                var aristas = from edge in E
                            where U.Contains(edge.Source.Name) 
                            where V.Except(U).Contains(edge.Destiny.Name) 
                            orderby edge.Weight ascending
                            select edge;

                // Obtiene la arista más corta.
                var arista = aristas.First();
                T.Add(arista);
                U.Add(arista.Destiny.Name);

                // Elimina la arista agregada y asigna el nodo origen<->destino
                // siguiente
                E.Remove(arista);

                foreach (var relacion in E)
                {
                    // En caso de que el destino, sea el nodo v,
                    // se asigna al nodo origen el nodo destino 
                    // y al nodo destino al nodo origen.
                    if (relacion.Destiny.Name == arista.Destiny.Name)
                    {
                        var temporal = relacion.Source;
                        relacion.Source = relacion.Destiny;
                        relacion.Destiny = temporal;
                    }
                }
            }

            foreach (var item in T)
            {
                cad += "(" + item.Source.Name + ", " + item.Destiny.Name + ")\n";
                
            }

            MessageBox.Show(cad);
        }
    }
}
