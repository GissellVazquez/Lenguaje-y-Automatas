using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafos_Dijkstra2
{
    public class Grafo
    {
        public List<Nodo> Nodos {  get; set; }
        public List<Arista>Aristas { get; set; }
        public Grafo() { 
        Nodos=new List<Nodo>();
            Aristas=new List<Arista>();
           
                }
        public void AgregarNodo (string id)
        {
            Nodos.Add(new Nodo(id));
        }
        public void AgregarArista(string origen, string destino, int peso)
        {
            Nodo nodoOrigen = Nodos.First(n=> n.Id == origen);
            Nodo nodoDestino = Nodos.First(n=> n.Id == destino);
           
            var arista = new Arista(nodoOrigen,nodoDestino, peso);
            nodoOrigen.Aristas.Add(arista);
            Aristas.Add(arista);
        }
        //Algoritmo metodo Dijstra
        public (List<string> camino, int pesoTotal) Dijkstra(string inicio, string fin)
        {
            var distancias = new Dictionary<string, int>();
            var anteriores = new Dictionary<string, string?>();
            var noVisitados = new List<Nodo>();

            foreach (var nodo in Nodos)
            {
                distancias[nodo.Id] = int.MaxValue;
                anteriores[nodo.Id] = null;
                noVisitados.Add(nodo);
            }
            distancias[inicio] = 0;
            while (noVisitados.Count > 0)
            {
                var actual = noVisitados
                    .OrderBy(n => distancias[n.Id])
                    .First();
                noVisitados.Remove(actual);

                if (actual.Id == fin)
                    break;
                foreach (var Arista in actual.Aristas)
                {
                    
                    int alternativa = distancias[actual.Id] + Arista.Peso;
                    if (alternativa < distancias[Arista.Destino.Id])
                    {
                        distancias[Arista.Destino.Id] = alternativa;
                        anteriores[Arista.Destino.Id] = actual.Id;
                    }

                }

            }
            if (distancias[fin] == int.MaxValue)
            {
                return (new List<string>(), 0);
            }

            //Reconstruir
            var camino = new List<string>();
            string? nodoActual = fin;
            while (nodoActual != null)
            {
                camino.Insert(0, nodoActual);
                nodoActual = anteriores[nodoActual];
            }
            return (camino, distancias[fin]);
        }

        //Metodo del arbol expandido 
        public (List<Arista> arbol, int pesoTotal) Kruskal()
        {
            var resultado = new List<Arista>();
            int pesoTotal = 0;

            // Ordenar aristas por peso
            var aristasOrdenadas = Aristas.OrderBy(a => a.Peso).ToList();

            // Estructura para evitar ciclos
            Dictionary<string, string> padre = new Dictionary<string, string>();

            foreach (var nodo in Nodos)
                padre[nodo.Id] = nodo.Id;

            string Encontrar(string nodo)
            {
                if (padre[nodo] != nodo)
                    padre[nodo] = Encontrar(padre[nodo]);
                return padre[nodo];
            }

            void Unir(string a, string b)
            {
                string raizA = Encontrar(a);
                string raizB = Encontrar(b);
                padre[raizB] = raizA;
            }

            foreach (var arista in aristasOrdenadas)
            {
                string origen = Encontrar(arista.Origen.Id);
                string destino = Encontrar(arista.Destino.Id);

                if (origen != destino)
                {
                    resultado.Add(arista);
                    pesoTotal += arista.Peso;
                    Unir(origen, destino);
                }
            }

            return (resultado, pesoTotal);
        }

    }
}
