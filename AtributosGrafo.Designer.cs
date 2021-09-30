namespace EditordeGrafos
{
    partial class AtributosGrafo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNodos = new System.Windows.Forms.Label();
            this.grpBoxAtriNodo = new System.Windows.Forms.GroupBox();
            this.grpBoxAtriArista = new System.Windows.Forms.GroupBox();
            this.lblAristas = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblGrado = new System.Windows.Forms.Label();
            this.lblGradoGrafo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grpBoxAtriNodo.SuspendLayout();
            this.grpBoxAtriArista.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNodos
            // 
            this.lblNodos.Location = new System.Drawing.Point(6, 16);
            this.lblNodos.Name = "lblNodos";
            this.lblNodos.Size = new System.Drawing.Size(41, 298);
            this.lblNodos.TabIndex = 0;
            // 
            // grpBoxAtriNodo
            // 
            this.grpBoxAtriNodo.Controls.Add(this.lblNodos);
            this.grpBoxAtriNodo.Location = new System.Drawing.Point(12, 12);
            this.grpBoxAtriNodo.Name = "grpBoxAtriNodo";
            this.grpBoxAtriNodo.Size = new System.Drawing.Size(53, 317);
            this.grpBoxAtriNodo.TabIndex = 1;
            this.grpBoxAtriNodo.TabStop = false;
            this.grpBoxAtriNodo.Text = "Nodos";
            // 
            // grpBoxAtriArista
            // 
            this.grpBoxAtriArista.Controls.Add(this.lblAristas);
            this.grpBoxAtriArista.Location = new System.Drawing.Point(255, 12);
            this.grpBoxAtriArista.Name = "grpBoxAtriArista";
            this.grpBoxAtriArista.Size = new System.Drawing.Size(237, 317);
            this.grpBoxAtriArista.TabIndex = 2;
            this.grpBoxAtriArista.TabStop = false;
            this.grpBoxAtriArista.Text = "Aristas";
            // 
            // lblAristas
            // 
            this.lblAristas.Location = new System.Drawing.Point(6, 16);
            this.lblAristas.Name = "lblAristas";
            this.lblAristas.Size = new System.Drawing.Size(225, 298);
            this.lblAristas.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblGrado);
            this.groupBox1.Location = new System.Drawing.Point(71, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(74, 317);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grado";
            // 
            // lblGrado
            // 
            this.lblGrado.Location = new System.Drawing.Point(6, 16);
            this.lblGrado.Name = "lblGrado";
            this.lblGrado.Size = new System.Drawing.Size(62, 298);
            this.lblGrado.TabIndex = 0;
            // 
            // lblGradoGrafo
            // 
            this.lblGradoGrafo.Location = new System.Drawing.Point(12, 16);
            this.lblGradoGrafo.Name = "lblGradoGrafo";
            this.lblGradoGrafo.Size = new System.Drawing.Size(80, 16);
            this.lblGradoGrafo.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblGradoGrafo);
            this.groupBox2.Location = new System.Drawing.Point(151, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(98, 35);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Grado del Grafo";
            // 
            // AtributosGrafo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 341);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBoxAtriArista);
            this.Controls.Add(this.grpBoxAtriNodo);
            this.Name = "AtributosGrafo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Atributos";
            this.grpBoxAtriNodo.ResumeLayout(false);
            this.grpBoxAtriArista.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNodos;
        private System.Windows.Forms.GroupBox grpBoxAtriNodo;
        private System.Windows.Forms.GroupBox grpBoxAtriArista;
        private System.Windows.Forms.Label lblAristas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblGrado;
        private System.Windows.Forms.Label lblGradoGrafo;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}