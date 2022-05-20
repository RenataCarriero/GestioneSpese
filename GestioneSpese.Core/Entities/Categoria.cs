using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.Core.Entities
{
  
    public class Categoria
    {
        public int Id { get; set; }
        public string NomeCategoria { get; set; }
        public ICollection<Spesa> Spese { get; set; } = new List<Spesa>();
        public override string ToString()
        {
            return $" Id: {Id} - Categoria: {NomeCategoria}";
        }
    }
}
