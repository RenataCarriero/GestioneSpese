using GestioneSpese.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestioneSpese.Core.IRepository
{
    public interface ISpesaRepository : IRepository<Spesa>
    {
       List<Spesa> GetSpeseUtente(string utente);         
       
       bool Delete(Spesa entity);
       bool Add(Spesa entity);
       bool Update(Spesa entity);
    }
}
