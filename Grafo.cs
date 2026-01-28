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
        public void AgregarNodo (int id)
        {
            Nodos.Add(new Nodo(id));
        }
        public void AgregarArista(int origen, int destino, int peso)
        {
            Nodo nodoOrigen = Nodos.First(n=> n.Id == origen);
            Nodo nodoDestino = Nodos.First(n=> n.Id == destino);
           
            var arista = new Arista(nodoOrigen,nodoDestino, peso);
            nodoOrigen.Aristas.Add(arista);
        }
        //Algoritmo metodo Dijstra
        public (List<int> camino, int pesoTotal) Dijkstra(int inicio, int fin)
        {
            var distancias = new Dictionary<int, int>();
            var anteriores = new Dictionary<int, int?>();
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
                    if (distancias[actual.Id] == int.MaxValue) continue;
                    int alternativa = distancias[actual.Id] + Arista.Peso;
                    {
                        distancias[Arista.Destino.Id] = alternativa;
                        anteriores[Arista.Destino.Id] = actual.Id;
                    }

                }

            }
            if (distancias[fin] == int.MaxValue)
            {
                return (new List<int>(), 0);
            }

            //Reconstruir
            var camino = new List<int>();
            int? nodoActual = fin;
            while (nodoActual != null)
            {
                camino.Insert(0, nodoActual.Value);
                nodoActual = anteriores[nodoActual.Value];
            }
            return (camino, distancias[fin]);
        }
    }
}
