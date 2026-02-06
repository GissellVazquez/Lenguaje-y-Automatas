namespace Grafos_Dijkstra2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panel = new Panel();
            label1 = new Label();
            boxnodos = new TextBox();
            crear = new Button();
            conexiones = new DataGridView();
            Nodo = new DataGridViewTextBoxColumn();
            Nodo2 = new DataGridViewTextBoxColumn();
            peso = new DataGridViewTextBoxColumn();
            agregarConexion = new Button();
            labelinicio = new Label();
            labelFin = new Label();
            cbxInicio = new ComboBox();
            cbxFin = new ComboBox();
            calcular = new Button();
            labelcamino = new Label();
            labelpeso = new Label();
            tbxcamino = new TextBox();
            Resultadototal = new Label();
            limpiar = new Button();
            btnKruskal = new Button();
            ((System.ComponentModel.ISupportInitialize)conexiones).BeginInit();
            SuspendLayout();
            // 
            // panel
            // 
            panel.BackColor = Color.WhiteSmoke;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Location = new Point(617, 9);
            panel.Margin = new Padding(4);
            panel.Name = "panel";
            panel.Size = new Size(581, 815);
            panel.TabIndex = 0;
            panel.Paint += panel_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Location = new Point(61, 118);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(161, 25);
            label1.TabIndex = 1;
            label1.Text = "Numero de Nodos";
            // 
            // boxnodos
            // 
            boxnodos.Location = new Point(246, 116);
            boxnodos.Margin = new Padding(4);
            boxnodos.Name = "boxnodos";
            boxnodos.Size = new Size(155, 31);
            boxnodos.TabIndex = 2;
            // 
            // crear
            // 
            crear.BackColor = Color.FromArgb(30, 30, 60);
            crear.FlatStyle = FlatStyle.Flat;
            crear.Location = new Point(61, 172);
            crear.Margin = new Padding(4);
            crear.Name = "crear";
            crear.Padding = new Padding(5);
            crear.Size = new Size(195, 46);
            crear.TabIndex = 3;
            crear.Text = "Generar Grafo";
            crear.UseVisualStyleBackColor = false;
            crear.Click += crear_Click;
            // 
            // conexiones
            // 
            conexiones.BackgroundColor = Color.FromArgb(30, 30, 60);
            conexiones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            conexiones.Columns.AddRange(new DataGridViewColumn[] { Nodo, Nodo2, peso });
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            conexiones.DefaultCellStyle = dataGridViewCellStyle1;
            conexiones.EditMode = DataGridViewEditMode.EditOnEnter;
            conexiones.EnableHeadersVisualStyles = false;
            conexiones.GridColor = SystemColors.MenuText;
            conexiones.Location = new Point(61, 245);
            conexiones.Margin = new Padding(4);
            conexiones.Name = "conexiones";
            conexiones.RowHeadersWidth = 51;
            conexiones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            conexiones.Size = new Size(428, 199);
            conexiones.TabIndex = 4;
            // 
            // Nodo
            // 
            Nodo.HeaderText = "Origen ";
            Nodo.MinimumWidth = 6;
            Nodo.Name = "Nodo";
            Nodo.Width = 125;
            // 
            // Nodo2
            // 
            Nodo2.HeaderText = "Destino";
            Nodo2.MinimumWidth = 6;
            Nodo2.Name = "Nodo2";
            Nodo2.Width = 125;
            // 
            // peso
            // 
            peso.HeaderText = "Peso";
            peso.MinimumWidth = 6;
            peso.Name = "peso";
            peso.Width = 125;
            // 
            // agregarConexion
            // 
            agregarConexion.BackColor = Color.FromArgb(30, 30, 60);
            agregarConexion.FlatStyle = FlatStyle.Flat;
            agregarConexion.Location = new Point(61, 464);
            agregarConexion.Margin = new Padding(4);
            agregarConexion.Name = "agregarConexion";
            agregarConexion.Padding = new Padding(5);
            agregarConexion.Size = new Size(195, 45);
            agregarConexion.TabIndex = 5;
            agregarConexion.Text = "Registrar Conexion";
            agregarConexion.UseVisualStyleBackColor = false;
            agregarConexion.Click += agregarConexion_Click;
            // 
            // labelinicio
            // 
            labelinicio.AutoSize = true;
            labelinicio.Location = new Point(61, 558);
            labelinicio.Margin = new Padding(4, 0, 4, 0);
            labelinicio.Name = "labelinicio";
            labelinicio.Size = new Size(54, 25);
            labelinicio.TabIndex = 6;
            labelinicio.Text = "Inicio";
            // 
            // labelFin
            // 
            labelFin.AutoSize = true;
            labelFin.Location = new Point(61, 615);
            labelFin.Margin = new Padding(4, 0, 4, 0);
            labelFin.Name = "labelFin";
            labelFin.Size = new Size(35, 25);
            labelFin.TabIndex = 7;
            labelFin.Text = "Fin";
            // 
            // cbxInicio
            // 
            cbxInicio.FormattingEnabled = true;
            cbxInicio.Location = new Point(116, 553);
            cbxInicio.Margin = new Padding(4);
            cbxInicio.Name = "cbxInicio";
            cbxInicio.Size = new Size(125, 33);
            cbxInicio.TabIndex = 8;
            // 
            // cbxFin
            // 
            cbxFin.FormattingEnabled = true;
            cbxFin.Location = new Point(116, 599);
            cbxFin.Margin = new Padding(4);
            cbxFin.Name = "cbxFin";
            cbxFin.Size = new Size(125, 33);
            cbxFin.TabIndex = 9;
            // 
            // calcular
            // 
            calcular.BackColor = Color.FromArgb(30, 30, 60);
            calcular.FlatStyle = FlatStyle.Flat;
            calcular.Location = new Point(271, 567);
            calcular.Margin = new Padding(4);
            calcular.Name = "calcular";
            calcular.Padding = new Padding(5);
            calcular.Size = new Size(282, 46);
            calcular.TabIndex = 10;
            calcular.Text = "Calcular Camino Minimo";
            calcular.UseVisualStyleBackColor = false;
            calcular.Click += calcular_Click;
            // 
            // labelcamino
            // 
            labelcamino.AutoSize = true;
            labelcamino.Location = new Point(42, 685);
            labelcamino.Margin = new Padding(4, 0, 4, 0);
            labelcamino.Name = "labelcamino";
            labelcamino.Size = new Size(73, 25);
            labelcamino.TabIndex = 11;
            labelcamino.Text = "Camino";
            // 
            // labelpeso
            // 
            labelpeso.AutoSize = true;
            labelpeso.Location = new Point(42, 776);
            labelpeso.Margin = new Padding(4, 0, 4, 0);
            labelpeso.Name = "labelpeso";
            labelpeso.Size = new Size(49, 25);
            labelpeso.TabIndex = 12;
            labelpeso.Text = "Peso";
            // 
            // tbxcamino
            // 
            tbxcamino.Location = new Point(132, 682);
            tbxcamino.Margin = new Padding(4);
            tbxcamino.Name = "tbxcamino";
            tbxcamino.Size = new Size(421, 31);
            tbxcamino.TabIndex = 13;
            // 
            // Resultadototal
            // 
            Resultadototal.AutoSize = true;
            Resultadototal.Location = new Point(132, 776);
            Resultadototal.Margin = new Padding(4, 0, 4, 0);
            Resultadototal.Name = "Resultadototal";
            Resultadototal.Size = new Size(132, 25);
            Resultadototal.TabIndex = 14;
            Resultadototal.Text = "Resultado Total";
            // 
            // limpiar
            // 
            limpiar.BackColor = Color.FromArgb(30, 30, 60);
            limpiar.FlatStyle = FlatStyle.Flat;
            limpiar.Location = new Point(429, 756);
            limpiar.Margin = new Padding(4);
            limpiar.Name = "limpiar";
            limpiar.Padding = new Padding(5);
            limpiar.Size = new Size(124, 45);
            limpiar.TabIndex = 15;
            limpiar.Text = "Limpiar ";
            limpiar.UseVisualStyleBackColor = false;
            limpiar.Click += limpiar_Click;
            // 
            // btnKruskal
            // 
            btnKruskal.BackColor = Color.FromArgb(30, 30, 60);
            btnKruskal.FlatStyle = FlatStyle.Flat;
            btnKruskal.Location = new Point(271, 513);
            btnKruskal.Margin = new Padding(4);
            btnKruskal.Name = "btnKruskal";
            btnKruskal.Padding = new Padding(5);
            btnKruskal.Size = new Size(282, 46);
            btnKruskal.TabIndex = 16;
            btnKruskal.Text = "Calcular Arbol de Expansion Minima";
            btnKruskal.UseVisualStyleBackColor = false;
            btnKruskal.Click += btnKruskal_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 40);
            ClientSize = new Size(1207, 846);
            Controls.Add(btnKruskal);
            Controls.Add(limpiar);
            Controls.Add(Resultadototal);
            Controls.Add(tbxcamino);
            Controls.Add(labelpeso);
            Controls.Add(labelcamino);
            Controls.Add(calcular);
            Controls.Add(cbxFin);
            Controls.Add(cbxInicio);
            Controls.Add(labelFin);
            Controls.Add(labelinicio);
            Controls.Add(agregarConexion);
            Controls.Add(conexiones);
            Controls.Add(crear);
            Controls.Add(boxnodos);
            Controls.Add(label1);
            Controls.Add(panel);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4);
            Name = "Form1";
            Padding = new Padding(5);
            Text = "Metodo Dijkstrs";
            TransparencyKey = Color.Navy;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)conexiones).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel;
        private Label label1;
        private TextBox boxnodos;
        private Button crear;
        private DataGridView conexiones;
        private Button agregarConexion;
        private Label labelinicio;
        private Label labelFin;
        private ComboBox cbxInicio;
        private ComboBox cbxFin;
        private Button calcular;
        private Label labelcamino;
        private Label labelpeso;
        private TextBox tbxcamino;
        private Label Resultadototal;
        private Button limpiar;
        private DataGridViewTextBoxColumn Nodo;
        private DataGridViewTextBoxColumn Nodo2;
        private DataGridViewTextBoxColumn peso;
        private Button btnKruskal;
    }
}
