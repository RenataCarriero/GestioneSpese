using GestioneSpese.Core.BusinessLayer;
using GestioneSpese.Core.Entities;
using GestioneSpese.RepositoryEF.RepositoryEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese.Client
{
    internal static class Menu
    {
        private static readonly IBusinessLayer bl = new MainBusinessLayer(new RepositorySpesaEF(), new RepositoryCategoriaEF());
        internal static void Start()
        {

            /*
            • L 'utente deve poter:
            - Inserire nuove Spese
            - Approvare una spesa
            - Cancellare una spesa
            - Visualizzare
            - l'elenco delle Spese Approvate
            - L'elenco delle Spese di uno specifico Utente
            - Il totale delle Spese per Categoria
            */

            bool continua = true;
            Console.WriteLine("Benvenuto nella gestione interna delle spese della tua Famiglia!\n");
            while (continua)
            {
                Console.WriteLine("\nPremi: ");
                Console.WriteLine("[1] Per inserire nuove spese.");
                Console.WriteLine("[2] Per approvare una spesa.");
                Console.WriteLine("[3] Per cancellare una spesa.");
                Console.WriteLine("[4] Per visualizzare l'elenco delle spese approvate.");
                Console.WriteLine("[5] Per visualizzare l'elenco delle spese di un certo utente.");
                Console.WriteLine("[6] Per visualizzare il totale delle spese per categoria");
                Console.WriteLine("[0] Per uscire");
                int scelta;
                do
                {
                    Console.WriteLine("Fai la tua scelta tra le possibili opzioni: ");
                } while (!(int.TryParse(Console.ReadLine(), out scelta) && scelta >= 0 && scelta <= 7));

                switch (scelta)
                {
                    case 1:
                        InserisciSpesa();
                        break;
                    case 2:
                        ApprovaSpesa();
                        break;
                    case 3:
                        CancellaSpesa();
                        break;
                    case 4:
                        VisualizzaSpeseApprovate();
                        break;
                    case 5:
                        VisualizzareSpeseUtente();
                        break;
                    case 6:
                        //TODO
                        break;
                    case 0:
                        Console.WriteLine("Ciao!!");
                        continua = false;
                        break;
                    default:
                        Console.WriteLine("Scelta non valida, attenzione!");
                        break;
                }
            }
        }

        private static void CancellaSpesa()
        {
            VisualizzaSpese();
            int id;
            Console.Write("Quale spesa vuoi cancellare? Inserisci l'id: ");
            while (!(int.TryParse(Console.ReadLine(), out id) && bl.EsisteSpesa(id)))
            {
                Console.WriteLine("Valore errato. Riprova:");
            }

            bool esito = bl.DeleteSpesa(id);

            if (esito)
                Console.WriteLine("Spesa cancellata correttamente!");
            else
                Console.WriteLine("Oops, abbiamo trovato un problema!");

        }

        private static void VisualizzaSpese()
        {
            List<Spesa> spese = bl.GetAllSpese();
            if (spese.Count == 0)
            {
                Console.WriteLine("Non ci sono spese");
            }
            else
            {
                foreach (var item in spese)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }


        private static void ApprovaSpesa()
        {
            VisualizzaSpeseDaApprovare();
            int id;
            Console.Write("Quale spesa vuoi approvare? Inserisci l'id: ");
            while (!(int.TryParse(Console.ReadLine(), out id) && bl.EsisteSpesaNonApprovata(id)))
            {
                Console.WriteLine("Valore errato. Riprova:");
            }
            
            bool esito = bl.ApprovaSpesa(id);

            if (esito)
                Console.WriteLine("Spesa approvata correttamente!");
            else
                Console.WriteLine("Oops, abbiamo trovato un problema!");

        }

        private static void VisualizzaSpeseDaApprovare()
        {
            List<Spesa> speseDaApprovare = bl.GetSpeseDaApprovare();
            if (speseDaApprovare.Count == 0)
            {
                Console.WriteLine("Non ci sono spese da approvare!");
            }
            else
            {
                foreach (var item in speseDaApprovare)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }

        private static void InserisciSpesa()
        {

            Console.WriteLine("Per quale categoria desideri aggiungere una spesa?");
            VisualizzaCategorie();
            int idCat;
            Console.Write("Inserire l'Id della categoria: ");
            while (!(int.TryParse(Console.ReadLine(), out idCat) && bl.EsisteCategoria(idCat)==true))
            {
                Console.WriteLine("Valore errato. Categoria inesistente. Riprova:");
            }
            DateTime data;
            Console.Write("Inserire data: ");
            while (!(DateTime.TryParse(Console.ReadLine(), out data)))
            {
                Console.WriteLine("Data non valida! Riprova");
            }
            Console.Write("Inserisci descrizione: ");
            string descrizione = Console.ReadLine();

            decimal importo;
            Console.Write("Inserire importo: ");
            while (!(decimal.TryParse(Console.ReadLine(), out importo) && importo > 0))
            {
                Console.WriteLine("Valore errato! Riprova");
            }
            Console.Write("Inserisci utente: ");
            string utente = Console.ReadLine();

            Spesa spesa = new Spesa();

            spesa.CategoriaId = idCat;
            spesa.Data = data;
            spesa.Descrizione = descrizione;
            spesa.Importo = importo;
            spesa.Utente = utente;
            //spesa.Approvato = false;

            bool esito = bl.AddSpesa(spesa);

            if (esito)
            {
                Console.WriteLine("Spesa aggiunta correttamente! Ecco un riepilogo della spesa appena inserita:");
                Console.WriteLine(spesa.ToString());
            }
            else
                Console.WriteLine("Oops, abbiamo trovato un problema!");
        }

        private static void VisualizzaCategorie()
        {
            List<Categoria> categorie = bl.GetAllCategorie();
            if (categorie.Count == 0)
            {
                Console.WriteLine("Non ci sono categorie");
            }
            else
            {
                foreach (var item in categorie)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }

        private static void VisualizzareSpeseUtente()
        {
            Console.WriteLine("Di quale utente vuoi vedere le spese?");
            string utente = Console.ReadLine();
            List<Spesa> speseUtente = bl.GetSpeseUtente(utente);
            foreach (var item in speseUtente)
            {
                Console.WriteLine(item.ToString());
            }
        }


        private static void VisualizzaSpeseApprovate()
        {
            List<Spesa> speseApprovate = bl.GetSpeseApprovate();
            if (speseApprovate.Count == 0)
            {
                Console.WriteLine("Non abbiamo spese approvate");
            }
            else
            {
                foreach (var item in speseApprovate)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
    }
}
