using GestioneSpese.Core.Entities;
using GestioneSpese.Core.IRepository;
using GestioneSpeseEF.RepositoryEF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.RepositoryEF.RepositoryEF
{
    public class RepositorySpesaEF : ISpesaRepository
    {
        
        public bool Add(Spesa entity)
        {
            try
            {
                using (var ctx = new Context())
                {
                    ctx.Spese.Add(entity);
                    ctx.SaveChanges();
                }
                return true;
            }catch (Exception ex)
            {
                return false;
            }
            
        }

        public bool Delete(Spesa entity)
        {
            try
            {
                using (var ctx = new Context())
                {
                    
                    ctx.Spese.Remove(entity);
                    ctx.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Spesa> GetAll()
        {
            using(var ctx=new Context())
            {
                return ctx.Spese.Include(s=>s.Categoria).ToList();
            }
        }

       
        public Spesa GetById(int id)
        {
            using (var ctx = new Context())
            {
                return ctx.Spese.Find(id);
            }
        }

        public List<Spesa> GetSpeseUtente(string utente)
        {
           using(var ctx= new Context())
            {
                return ctx.Spese.Where(s=>s.Utente==utente).ToList();   
            }
        }

      

        public bool Update(Spesa entity)
        {
            using (var ctx = new Context())
            {
                ctx.Spese.Update(entity);
                return true;
            }
            return false;
        }
    }
}
