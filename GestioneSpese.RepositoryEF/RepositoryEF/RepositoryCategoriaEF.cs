using GestioneSpese.Core.Entities;
using GestioneSpese.Core.IRepository;
using GestioneSpeseEF.RepositoryEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.RepositoryEF.RepositoryEF
{
    public class RepositoryCategoriaEF : ICategoriaRepository
    {
      
        public List<Categoria> GetAll()
        {
            using(var ctx= new Context())
            {
                return ctx.Categorie.ToList();
            }
        }

        public Categoria GetById(int id)
        {
            using (var ctx = new Context())
            {
                return ctx.Categorie.Find(id);
            }
        }

    }
}
