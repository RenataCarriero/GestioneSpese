using System;
using System.Data;
using System.Data.SqlClient;

internal static class DisconnectedMode
{
    static string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpese;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    internal static void InserisciSpesa()
    {
        DataSet dataset = new DataSet();
        using SqlConnection conn = new SqlConnection(connectionStringSQL);


        try
        {
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
                Console.WriteLine("Connessi al db");
            else
                Console.WriteLine("NON connessi al db");


            var adapter = InizializzaAdapter(conn);
            adapter.Fill(dataset, "Spesa");

            conn.Close();
            Console.WriteLine("Connessione chiusa");

            Console.WriteLine("---- Inserire un nuova Spesa ----");
            Console.Write("Data: ");
            DateTime dataSpesa = DateTime.Parse(Console.ReadLine());
            Console.Write("Descrizione: ");
            string descrizione = Console.ReadLine();
            Console.Write("Utente: ");
            string utente = Console.ReadLine();
            Console.Write("Importo: ");
            decimal importo = decimal.Parse(Console.ReadLine());
            Console.Write("Categoria (id): ");
            int idCategoria = int.Parse(Console.ReadLine()); //TODO: recupero id da nome categoria


            DataRow nuovaRiga = dataset.Tables["Spesa"].NewRow();
            nuovaRiga["Descrizione"] = descrizione;
            nuovaRiga["DataSpesa"] = dataSpesa;
            nuovaRiga["Utente"] = utente;
            nuovaRiga["Approvato"] = 0;
            nuovaRiga["Importo"] = importo;
            nuovaRiga["CategoriaId"] = idCategoria;

            dataset.Tables["Spesa"].Rows.Add(nuovaRiga); //aggiunto la mia nuova riga nel dataset

            //riconciliazione e quindi vero salvataggio del dato sul db
            adapter.Update(dataset, "Spesa");
            Console.WriteLine("Database Aggiornato");

        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Errore SQL: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Errore generico: {ex.Message}");
        }
        finally
        {
            conn.Close();
        }

    }

    private static SqlDataAdapter InizializzaAdapter(SqlConnection conn)
    {
        SqlDataAdapter adapter = new SqlDataAdapter();

        //SELECT (serve al metodo FILL)
        adapter.SelectCommand = new SqlCommand("Select * from Spesa", conn);
        adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

        //INSERT
        adapter.InsertCommand = GeneraInsertCommand(conn);
        return adapter;
    }

    private static SqlCommand GeneraInsertCommand(SqlConnection conn)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "Insert into Spesa values (@data, @descr, @utente, @importo, @approvato, @categoria)";

        cmd.Parameters.Add(new SqlParameter("@descr", SqlDbType.NVarChar, 500, "Descrizione"));
        cmd.Parameters.Add(new SqlParameter("@data", SqlDbType.DateTime, 0, "DataSpesa"));
        cmd.Parameters.Add(new SqlParameter("@utente", SqlDbType.NVarChar, 100, "Utente"));
        cmd.Parameters.Add(new SqlParameter("@importo", SqlDbType.Decimal, 100, "Importo"));
        cmd.Parameters.Add(new SqlParameter("@approvato", SqlDbType.Bit, 0, "Approvato"));
        cmd.Parameters.Add(new SqlParameter("@categoria", SqlDbType.Int, 0, "CategoriaId"));


        return cmd;
    }
}
