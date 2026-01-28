using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafos_Dijkstra2
{
    public class Nodo
    {
        public int Id {  get; set; }
        public List<Arista> Aristas { get; set; }
        public Nodo (int id)
        {
            Id = id;
            Aristas= new List<Arista>();
        }
    }
}
