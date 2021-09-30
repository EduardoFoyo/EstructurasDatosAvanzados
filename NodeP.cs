using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditordeGrafos{
    [Serializable()]

    public class NodeP{
        private bool visited;
        private bool selected;
        private int degree;
        private int degreeIn;
        private int degreeEx;
        private int num_rec;
        private int num_res;
        private string name; /// tomar este 
        private Point position;
        private Color color;
        public List<NodeR> relations;

        public Point Position { 
            get { return position; } 
            set { position = value; } 
        }

        public string Name {
            get { return name; } 
            set { name = value; } 
        }
        public int Degree {
            get { return degree; } 
            set { degree = value; } 
        }

        public Color Color{
            get { return color; } 
            set { color=value; }
        }

        public int DegreeIn {
            get { return degreeIn; } 
            set { degreeIn = value; } 
        }
        public int DegreeEx {
            get { return degreeEx; } 
            set { degreeEx = value; } 
        }
        public int Num_rec
        {
            get { return num_rec; }
            set { num_rec = value; }
        }
        public int Num_res
        {
            get { return num_res; }
            set { num_res = value; }
        }
        public bool Selected{
            get { return selected; }
            set { selected=value; }
        }
        public bool Visited { 
            get { return visited; } 
            set { visited = value; } 
        }

        public int name_number()
        {
            int letra = 0;
            switch (this.name)
            {
                case "A":
                    letra = 0;
                    break;
                case "B":
                    letra = 1;
                    break;
                case "C":
                    letra = 2;
                    break;
                case "D":
                    letra = 3;
                    break;
                case "E":
                    letra = 4;
                    break;
                case "F":
                    letra = 5;
                    break;
                case "G":
                    letra = 6;
                    break;
                case "H":
                    letra = 7;
                    break;
                case "I":
                    letra = 8;
                    break;
                case "J":
                    letra = 9;
                    break;
                case "K":
                    letra = 10;
                    break;
                case "L":
                    letra = 11;
                    break;
                case "M":
                    letra = 12;
                    break;
                case "N":
                    letra = 13;
                    break;
                case "O":
                    letra = 14;
                    break;
                case "P":
                    letra = 15;
                    break;
                case "Q":
                    letra = 16;
                    break;
                case "R":
                    letra = 17;
                    break;
                case "S":
                    letra = 18;
                    break;
                case "T":
                    letra = 19;
                    break;
                case "U":
                    letra = 20;
                    break;
                case "V":
                    letra = 21;
                    break;
                case "W":
                    letra = 22;
                    break;
                case "X":
                    letra = 23;
                    break;
                case "Y":
                    letra = 24;
                    break;
                case "Z":
                    letra = 25;
                    break;
                default:
                    break;
            }
            return letra;
        }

        #region constructores

        public NodeP(){

        }

        public NodeP(NodeP co){
            position = co.Position;
            name = co.Name;
            relations = new List<NodeR>();
            degree = co.Degree;
            degreeEx = co.DegreeEx;
            degreeIn = co.DegreeIn;
            color = co.Color;
            selected = false;
        }

        public NodeP(Point p, char n){
            position = p;
            name = n.ToString();
            relations = new List<NodeR>();
            degree = 0;
            color = Color.White;
            selected = false;
        }

        #endregion
        #region operaciones

        public void InsertRelation(NodeP newRel, int num, bool isDirected){
            Degree++;
            if(isDirected){
                DegreeEx++;
                newRel.DegreeIn++;
            }

            relations.Add(new NodeR(newRel, "e" + num.ToString()));
        }

        public void RemoveRelation(NodeR delRel, bool isDirected) {
            Degree--;
            if (isDirected) {
                delRel.Up.DegreeIn--;
                this.degreeEx--;
            }
            relations.Remove(delRel);
        }

        #endregion
    }
}
