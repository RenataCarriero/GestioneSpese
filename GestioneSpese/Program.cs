// See https://aka.ms/new-console-template for more information
using GestioneSpese;

Console.WriteLine("=== Gestione Spese ===");

bool quit = false;
do
{
    Console.WriteLine($"============= Menu =============");
    Console.WriteLine();
    Console.WriteLine("[ 1 ] - Inserisci nuova Spesa\n");
    Console.WriteLine("[ 2 ] - Approva Spesa\n");
    Console.WriteLine("[ 3 ] - Elimina Spesa\n");
    Console.WriteLine("[ 4 ] - Mostra Spese Approvate\n");
    Console.WriteLine("[ 5 ] - Mostra Spese di un Utente\n");
    Console.WriteLine("[ 6 ] - Mostra totale Spese per categoria\n");
    Console.WriteLine("[ q ] - QUIT\n");


    // scelta utente
    Console.Write("> ");
    string scelta = Console.ReadLine();
    Console.WriteLine();

    switch (scelta)
    {
        case "1":
            // Inserisci Spesa
            DisconnectedMode.InserisciSpesa();
            break;
        case "2":
            // Approva Spesa
            Console.WriteLine("Elenco Spese");
            ConnectedMode.VisualizzaTutteLeSpese();
            ConnectedMode.ApprovaSpesa();
            break;
        case "3":
            // Cancella Spesa
            ConnectedMode.VisualizzaTutteLeSpese();
            ConnectedMode.DeleteSpesaById();
            break;
        case "4":
            // Spese Approvate
            ConnectedMode.VisualizzaSpeseApprovate();
            break;
        case "5":
            // Spese di un utente
            ConnectedMode.ElencoSpesePerUtente();
            break;
        case "6":
            // Totale Spese per categoria
            ConnectedMode.TotaleSpesePerCategoria();
            break;
        case "q":
            quit = true;
            break;
        default:
            Console.WriteLine("Comando sconosciuto.");
            break;
    }

} while (!quit);
