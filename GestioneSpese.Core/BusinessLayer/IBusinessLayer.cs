using GestioneSpese.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.Core.BusinessLayer
{
    public interface IBusinessLayer
    {
        List<Spesa> GetSpeseApprovate();
        List<Spesa> GetSpeseUtente(string utente);
        List<Categoria> GetAllCategorie();
        bool EsisteCategoria(int CategoriaId);
        bool AddSpesa(Spesa spesa);
        List<Spesa> GetAllSpese();
        List<Spesa> GetSpeseDaApprovare();
        bool DeleteSpesa(int id);
        bool ApprovaSpesa(int id);
       
        bool EsisteSpesa(int id);
        bool EsisteSpesaNonApprovata(int id);
    }
}

