using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.Core.Entities
{
    public class Spesa
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descrizione { get; set; }
        public decimal Importo { get; set; }
        public bool Approvato { get; set; } = false;

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }   
        
        public string Utente { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - Descrizione: {Descrizione} - Data: {Data.ToShortDateString()} - Importo: {Importo} euro" +
                $" - Approvato: {Approvato}";
        }
    }
}
