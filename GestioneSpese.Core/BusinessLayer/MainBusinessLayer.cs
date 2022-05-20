using GestioneSpese.Core.Entities;
using GestioneSpese.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.Core.BusinessLayer
{
    public class MainBusinessLayer :IBusinessLayer
    {
        private readonly ISpesaRepository spesaRepo;
        private readonly ICategoriaRepository categoriaRepo;
        public MainBusinessLayer(ISpesaRepository spesaRepository, ICategoriaRepository categoriaRepository)
        {
            spesaRepo = spesaRepository;
            categoriaRepo = categoriaRepository;
        }


        public bool AddSpesa(Spesa spesa)
        {
            return spesaRepo.Add(spesa);
        }
        public List<Spesa> GetSpeseApprovate()
        {
            return spesaRepo.GetAll().Where(s=>s.Approvato==true).ToList();
        }
        public List<Spesa> GetAllSpese()
        {
            return spesaRepo.GetAll();
        }
        public bool ApprovaSpesa(int id)
        {
            var spesaDaApprovare=spesaRepo.GetById(id);
            if(spesaDaApprovare != null)
            {
                spesaDaApprovare.Approvato=true;
                spesaRepo.Update(spesaDaApprovare);
                return true;
            }
            return false;
        }
        public List<Spesa> GetSpeseDaApprovare()
        {
            return spesaRepo.GetAll().Where(s=>s.Approvato==false).ToList();
        }
        public List<Categoria> GetAllCategorie()
        {
            return categoriaRepo.GetAll().ToList();
        }

        public bool DeleteSpesa(int id)
        {
            var spesaDaEliminare=spesaRepo.GetById(id);
            if (spesaDaEliminare == null)
            {
                return false;
            }
            spesaRepo.Delete(spesaDaEliminare);
            return true;
        }



        public List<Spesa> GetSpeseUtente(string utente)
        {
            return spesaRepo.GetSpeseUtente(utente);
        }

       

        public bool EsisteCategoria(int CategoriaId)
        {
           var categoriaEsistente = categoriaRepo.GetById(CategoriaId);
            if(categoriaEsistente == null){
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EsisteSpesa(int id)
        {
            var spesaEsistente = spesaRepo.GetById(id);
            if (spesaEsistente == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EsisteSpesaNonApprovata(int id)
        {
            var spesaEsistente = spesaRepo.GetById(id);
            if (spesaEsistente != null && spesaEsistente.Approvato==false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
