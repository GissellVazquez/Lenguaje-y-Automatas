using System.Windows.Forms;

namespace Grafos_Dijkstra2
{
    public partial class Form1 : Form
    {
        Grafo grafo;
        List<int> caminoMinimo = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void crear_Click(object sender, EventArgs e)
        {
            int CantidadNodos;
            if (!int.TryParse(boxnodos.Text, out CantidadNodos) || CantidadNodos <= 0)
            {

                MessageBox.Show("Ingresa un numero valido");
                return;
            }
            grafo = new Grafo();
            for (int i = 0; i < CantidadNodos; i++)
            {
                grafo.AgregarNodo(i);
            }


            cbxInicio.Items.Clear();
            cbxFin.Items.Clear();

            for (int i = 0; i < CantidadNodos; i++)
            {
                cbxInicio.Items.Add(i);
                cbxFin.Items.Add(i);
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
                if (fila.IsNewRow)  continue; 



                int origen = Convert.ToInt32(fila.Cells[0].Value);
                int destino = Convert.ToInt32(fila.Cells[1].Value);
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

            int inicio = Convert.ToInt32(cbxInicio.SelectedItem);
            int fin = Convert.ToInt32(cbxFin.SelectedItem);

            var resultado = grafo.Dijkstra(inicio, fin);
            if (resultado.camino.Count == 0)
            {
                MessageBox.Show("No existe camino entre los nodos");
                return;
            }
            caminoMinimo = resultado.camino;
            panel.Invalidate();

            // muestra camino
            tbxcamino.Text = string.Join(" → ", resultado.camino);

            // muestra peso total
            Resultadototal.Text = resultado.pesoTotal.ToString();
            panel.Invalidate(); //permite dibujar
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            if (grafo == null) return;

            Graphics g = e.Graphics;

            int radio = 20;
            int centroX = panel.Width / 2;
            int centroY = panel.Height / 2;
            int distancia = 100;

            Dictionary<int, Point> posiciones = new Dictionary<int, Point>();

            // posiciona nodos en círculo
            for (int i = 0; i < grafo.Nodos.Count; i++)
            {
                double angulo = 2 * Math.PI * i / grafo.Nodos.Count;
                int x = centroX + (int)(distancia * Math.Cos(angulo));
                int y = centroY + (int)(distancia * Math.Sin(angulo));

                posiciones[grafo.Nodos[i].Id] = new Point(x, y);
            }

            // dibujar aristas
            foreach (var nodo in grafo.Nodos)
            {
                foreach (var arista in nodo.Aristas)
                {
                    Point p1 = posiciones[nodo.Id];
                    Point p2 = posiciones[arista.Destino.Id];

                    Pen lapiz = Pens.Black;

                    // resaltar camino mínimo
                    for (int i = 0; i < caminoMinimo.Count - 1; i++)
                    {
                        if (caminoMinimo[i] == nodo.Id &&
                            caminoMinimo[i + 1] == arista.Destino.Id)
                        {
                            lapiz = new Pen(Color.Red, 3);
                            break;
                        }
                    }

                    g.DrawLine(lapiz, p1, p2);

                    // dibujar peso
                    int px = (p1.X + p2.X) / 2;
                    int py = (p1.Y + p2.Y) / 2;
                    g.DrawString(arista.Peso.ToString(), Font, Brushes.Blue, px, py);
                }
            }

            // dibuja nodos
            foreach (var nodo in grafo.Nodos)
            {
                Point p = posiciones[nodo.Id];

                g.FillEllipse(Brushes.LightBlue, p.X - radio, p.Y - radio, radio * 2, radio * 2);
                g.DrawEllipse(Pens.Black, p.X - radio, p.Y - radio, radio * 2, radio * 2);

                g.DrawString(nodo.Id.ToString(), Font, Brushes.Black, p.X - 5, p.Y - 5);
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
            conexiones.BackgroundColor = Color.FromArgb(20,20,40);
            conexiones.ForeColor = Color.WhiteSmoke;
            conexiones.GridColor = Color.WhiteSmoke;
            conexiones.BorderStyle = BorderStyle.None;
            conexiones.EnableHeadersVisualStyles = false;

            conexiones.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 60);
            conexiones.DefaultCellStyle.ForeColor = Color.White;

            conexiones.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 90);
            conexiones.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;

        }
    }

}
