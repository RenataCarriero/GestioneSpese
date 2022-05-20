using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.Core.IRepository
{
    public interface IRepository<T>
    {

        //quelli in comune
        List<T> GetAll();
        T GetById(int id);
        
        
    }
}
