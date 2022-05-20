using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpese
{
    internal static class ConnectedMode
    {
        static string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpese;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static void VisualizzaTutteLeSpese()
        {
            using SqlConnection connessione = new SqlConnection(connectionStringSQL);
            try
            {
                connessione.Open();

                string query = "select * from Spesa join Categoria on Spesa.CategoriaId=Categoria.CategoriaId";

                //istanziare SQLCommand 
                SqlCommand comando = new SqlCommand();
                comando.Connection = connessione;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = query;

                SqlDataReader reader = comando.ExecuteReader();

                Console.WriteLine("------Spese-------");
                while (reader.Read())
                {
                    var id = (int)reader["SpesaId"];
                    var dataSpesa = (DateTime)reader["DataSpesa"];
                    var descrizione = (string)reader["Descrizione"];
                    var utente = (string)reader["Utente"];
                    var importo = (decimal)reader["Importo"];
                    var approvato = (bool)reader["Approvato"];
                    var categoria = (string)reader["Categoria"];

                    var siNo = (approvato == true) ? "si" : "no";
                    Console.WriteLine($"{id} - {dataSpesa.ToShortDateString()} - {descrizione} - {utente} - {importo} euro - categ: {categoria} - Approvato: {siNo}");
                }

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
                connessione.Close();
            }

        }

        internal static void TotaleSpesePerCategoria()
        {
            using SqlConnection connessione = new SqlConnection(connectionStringSQL);
            try
            {
                connessione.Open();

                string query = "select Categoria.Categoria, sum(Spesa.Importo) as [Totale] from Spesa join Categoria on Spesa.CategoriaId=Categoria.CategoriaId group by Categoria.Categoria";

                //istanziare SQLCommand 
                SqlCommand comando = new SqlCommand();
                comando.Connection = connessione;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = query;


                SqlDataReader reader = comando.ExecuteReader();

                Console.WriteLine($"------Riepilogo spese per Categoria-------");
                while (reader.Read())
                {
                    var categoria = (string)reader["Categoria"];
                    var importoTot = (decimal)reader["Totale"];                    
                                        
                    Console.WriteLine($"{categoria} - Totale (euro): {importoTot}");
                }

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
                connessione.Close();
            }

        }

        internal static void VisualizzaSpeseApprovate()
        {
            using SqlConnection connessione = new SqlConnection(connectionStringSQL);
            try
            {
                connessione.Open();

                string query = "select * from Spesa join Categoria on Spesa.CategoriaId=Categoria.CategoriaId where Spesa.Approvato=1";

                //istanziare SQLCommand 
                SqlCommand comando = new SqlCommand();
                comando.Connection = connessione;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = query;
               

                SqlDataReader reader = comando.ExecuteReader();

                Console.WriteLine($"------Spese Approvate-------");
                while (reader.Read())
                {
                    var id = (int)reader["SpesaId"];
                    var dataSpesa = (DateTime)reader["DataSpesa"];
                    var descrizione = (string)reader["Descrizione"];
                    var utente = (string)reader["Utente"];
                    var importo = (decimal)reader["Importo"];
                    var approvato = (bool)reader["Approvato"];
                    var categoria = (string)reader["Categoria"];

                    var siNo = (approvato == true) ? "si" : "no";
                    Console.WriteLine($"{id} - {dataSpesa.ToShortDateString()} - {descrizione} - {utente} - {importo} euro - categ: {categoria} - Approvato: {siNo}");
                }

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
                connessione.Close();
            }

        }

        public static void ElencoSpesePerUtente()
        {
            using SqlConnection connessione = new SqlConnection(connectionStringSQL);
            try
            {
                Console.WriteLine("Di quale utente vuoi vedere le spese?");
                string user= Console.ReadLine(); //TODO: controllare se è un utente esistente

                connessione.Open();

                string query = "select * from Spesa join Categoria on Spesa.CategoriaId=Categoria.CategoriaId where Spesa.utente=@Utente";

                //istanziare SQLCommand 
                SqlCommand comando = new SqlCommand();
                comando.Connection = connessione;
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = query;
                comando.Parameters.AddWithValue("@Utente", user);

                SqlDataReader reader = comando.ExecuteReader();

                Console.WriteLine($"------Spese dell'utente {user}-------");
                while (reader.Read())
                {
                    var id = (int)reader["SpesaId"];
                    var dataSpesa = (DateTime)reader["DataSpesa"];
                    var descrizione = (string)reader["Descrizione"];
                    var utente = (string)reader["Utente"];
                    var importo = (decimal)reader["Importo"];
                    var approvato = (bool)reader["Approvato"];
                    var categoria = (string)reader["Categoria"];

                    var siNo = (approvato == true) ? "si" : "no";
                    Console.WriteLine($"{id} - {dataSpesa.ToShortDateString()} - {descrizione} - {utente} - {importo} euro - categ: {categoria} - Approvato: {siNo}");
                }

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
                connessione.Close();
            }

        }

        internal static void ApprovaSpesa()
        {
            using SqlConnection connessione = new SqlConnection(connectionStringSQL);
            try
            {
                Console.WriteLine("Quale spesa vuoi approvare?");
                int idDaApprovare = int.Parse(Console.ReadLine()); //TODO: controllare se è un idSpesa esistente

                connessione.Open();

                string deleteSQL = "update Spesa set Approvato=1 where SpesaId=@id";

                SqlCommand deleteCommand = connessione.CreateCommand();
                deleteCommand.CommandText = deleteSQL;
                deleteCommand.Parameters.AddWithValue("@id", idDaApprovare);

                int righeModificate = deleteCommand.ExecuteNonQuery();

                if (righeModificate >= 1)
                    Console.WriteLine($"{righeModificate} Spesa Approvata");
                else
                    Console.WriteLine("OOOOOOPS ...qualcosa non torna!");

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
                connessione.Close();
            }
        }




        public static void DeleteSpesaById()
        {
            using SqlConnection connessione = new SqlConnection(connectionStringSQL);
            try
            {
                Console.WriteLine("Quale spesa vuoi eliminare? inserisci un id");
                int idDaEliminare = int.Parse(Console.ReadLine()); //TODO: controllare se è un idSpesa esistente

                connessione.Open();

                string deleteSQL = "delete from Spesa where SpesaId=@id";

                SqlCommand deleteCommand = connessione.CreateCommand();
                deleteCommand.CommandText = deleteSQL;
                deleteCommand.Parameters.AddWithValue("@id", idDaEliminare);

                int righeCancellate = deleteCommand.ExecuteNonQuery();

                if (righeCancellate >= 1)
                    Console.WriteLine($"{righeCancellate} riga/righe eliminate correttamente");
                else
                    Console.WriteLine("OOOOOOPS ...qualcosa non torna!");

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
                connessione.Close();
            }
        }
    }
}
