using System.Windows.Forms;

namespace Grafos_Dijkstra2
{
    public partial class Form1 : Form
    {
        Grafo grafo;
        List<string> caminoMinimo = new List<string>();
        List<Arista> arbolKruskal = new List<Arista>();

        public Form1()
        {
            InitializeComponent();
        }

        private void crear_Click(object sender, EventArgs e)
        {
            int cantidadNodos;
            if (!int.TryParse(boxnodos.Text, out cantidadNodos) || cantidadNodos <= 0)
            {
                MessageBox.Show("Ingresa un número válido");
                return;
            }
            grafo = new Grafo();

            for (int i = 0; i < cantidadNodos; i++)
            {
                string idNodo = ((char)('A' + i)).ToString();
                grafo.AgregarNodo(idNodo);
            }

            cbxInicio.Items.Clear();
            cbxFin.Items.Clear();

            foreach (var nodo in grafo.Nodos)
            {
                cbxInicio.Items.Add(nodo.Id);
                cbxFin.Items.Add(nodo.Id);
            }

            panel.Invalidate();
            MessageBox.Show("Grafo creado correctamente");

        }

        private void agregarConexion_Click(object sender, EventArgs e)
        {
            if (grafo == null)
            {
                MessageBox.Show("Primero crea el grafo");
                return;
            }
            foreach (DataGridViewRow fila in conexiones.Rows)
            {
                if (fila.IsNewRow) continue;



                string origen = fila.Cells[0].Value.ToString();
                string destino = fila.Cells[1].Value.ToString();
                int peso = Convert.ToInt32(fila.Cells[2].Value);

                grafo.AgregarArista(origen, destino, peso);
            }

            MessageBox.Show("Conexión agregada correctamente");
            panel.Invalidate();
        }

        private void calcular_Click(object sender, EventArgs e)
        {
            if (grafo == null)
            {
                MessageBox.Show("Primero crea el grafo");
                return;
            }

            if (cbxInicio.SelectedItem == null || cbxFin.SelectedItem == null)
            {
                MessageBox.Show("Selecciona nodo inicio y nodo fin");
                return;
            }

            string inicio = cbxInicio.SelectedItem.ToString();
            string fin = cbxFin.SelectedItem.ToString();

            var resultado = grafo.Dijkstra(inicio, fin);

            if (resultado.camino.Count == 0)
            {
                MessageBox.Show("No existe camino entre los nodos");
                return;
            }

            caminoMinimo = resultado.camino;
            arbolKruskal.Clear(); // por si antes se usó Kruskal

            tbxcamino.Text = string.Join(" → ", resultado.camino);
            Resultadototal.Text = resultado.pesoTotal.ToString();

            panel.Invalidate();
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            if (grafo == null) return;

            Graphics g = e.Graphics;

            int radio = 20;
            int centroX = panel.Width / 2;
            int centroY = panel.Height / 2;
            int distancia = 120;

            Dictionary<string, Point> posiciones = new Dictionary<string, Point>();

            // Posicionar nodos en círculo
            for (int i = 0; i < grafo.Nodos.Count; i++)
            {
                double angulo = 2 * Math.PI * i / grafo.Nodos.Count;
                int x = centroX + (int)(distancia * Math.Cos(angulo));
                int y = centroY + (int)(distancia * Math.Sin(angulo));

                posiciones[grafo.Nodos[i].Id] = new Point(x, y);
            }

            // Dibujar TODAS las aristas normales
            foreach (var arista in grafo.Aristas)
            {
                Point p1 = posiciones[arista.Origen.Id];
                Point p2 = posiciones[arista.Destino.Id];

                Pen lapiz = Pens.Black;

                // Resaltar Dijkstra
                for (int i = 0; i < caminoMinimo.Count - 1; i++)
                {
                    if (caminoMinimo[i] == arista.Origen.Id &&
                        caminoMinimo[i + 1] == arista.Destino.Id)
                    {
                        lapiz = new Pen(Color.Red, 3);
                        break;
                    }
                }

                g.DrawLine(lapiz, p1, p2);

                int px = (p1.X + p2.X) / 2;
                int py = (p1.Y + p2.Y) / 2;
                g.DrawString(arista.Peso.ToString(), Font, Brushes.Blue, px, py);
            }

            // Dibujar Kruskal (verde)
            foreach (var arista in arbolKruskal)
            {
                Point p1 = posiciones[arista.Origen.Id];
                Point p2 = posiciones[arista.Destino.Id];

                g.DrawLine(new Pen(Color.Green, 3), p1, p2);
            }

            // Dibujar nodos
            foreach (var nodo in grafo.Nodos)
            {
                Point p = posiciones[nodo.Id];

                g.FillEllipse(Brushes.LightBlue, p.X - radio, p.Y - radio, radio * 2, radio * 2);
                g.DrawEllipse(Pens.Black, p.X - radio, p.Y - radio, radio * 2, radio * 2);
                g.DrawString(nodo.Id, Font, Brushes.Black, p.X - 6, p.Y - 6);
            }
        }

        private void limpiar_Click(object sender, EventArgs e)
        {
            grafo = null;
            caminoMinimo.Clear();
            tbxcamino.Clear();
            boxnodos.Clear();
            Resultadototal.Text = " ";
            cbxInicio.SelectedIndex = -1;
            cbxFin.SelectedIndex = -1;
            conexiones.Rows.Clear();
            panel.Invalidate();

            MessageBox.Show("Sistema Reiniciado:)");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conexiones.BackgroundColor = Color.FromArgb(20, 20, 40);
            conexiones.ForeColor = Color.WhiteSmoke;
            conexiones.GridColor = Color.WhiteSmoke;
            conexiones.BorderStyle = BorderStyle.None;
            conexiones.EnableHeadersVisualStyles = false;

            conexiones.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 60);
            conexiones.DefaultCellStyle.ForeColor = Color.White;

            conexiones.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 90);
            conexiones.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

        }

        private void btnKruskal_Click(object sender, EventArgs e)
        {
            if (grafo == null)
            {
                MessageBox.Show("Primero crea el grafo");
                return;
            }

            var resultado = grafo.Kruskal();

            tbxcamino.Text = string.Join(" → ",
            resultado.arbol.Select(a => $"{a.Origen.Id}-{a.Destino.Id} ({a.Peso})"));

            Resultadototal.Text = resultado.pesoTotal.ToString();

            panel.Invalidate();
        }
    }

}
