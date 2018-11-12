using BücherContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BücherHelper
{
    public class BuchDBSpeicher : IBuchSpeicher
    {
        //Datenbank anlegen: Views->Server Explorer->RechtsKlick auf DataConnections->Create New SQL Server Database
        //-> "(localdb)\mssqllocaldb" als Server Name eintragen
        //https://docs.microsoft.com/en-us/visualstudio/data-tools/create-a-simple-data-application-by-using-adonet?view=vs-2017

        const string Connection_String = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Heinz;Integrated Security=True;Pooling=False";

        public List<IFavorisierbaresBook> Laden()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Connection_String))
                {
                    using (SqlCommand command = new SqlCommand("Select * FROM TBL_Favorit", connection))
                    {
                        connection.Open();
                        var reader = command.ExecuteReader();
                        List<IFavorisierbaresBook> books = new List<IFavorisierbaresBook>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string isbn = reader["ISBN"].ToString();
                                string title = reader["Titel"].ToString();
                                string authors = reader["Autoren"].ToString();
                                string previewlink = reader["Vorschaulink"].ToString();

                                books.Add(new Book()
                                {
                                    IstFavorit = true,
                                    volumeInfo = new Volumeinfo()
                                    {
                                        previewLink = previewlink,
                                        title = title,
                                        authors = authors.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries),
                                        industryIdentifiers = new Industryidentifier[]
                                        {
                                            new Industryidentifier()
                                            {
                                                identifier = isbn
                                            }
                                        }
                                    }
                                });
                            }
                            return books;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Fehler bei der DB: {exp.Message}\nWurde die Datenbank schon angelegt?");
                return null;
            }
            return null;
        }

        public bool Speichern(IFavorisierbaresBook buch)
        {
            using (SqlConnection connection = new SqlConnection(Connection_String))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO TBL_Favorit (ISBN,Titel,Autoren,Vorschaulink) VALUES (@isbn,@titel, @autoren, @vorschaulink);";
                        command.Parameters.AddWithValue("@isbn", buch.ISBN);
                        command.Parameters.AddWithValue("@titel", buch.Titel);
                        command.Parameters.AddWithValue("@autoren", buch.AutorenAlsString);
                        command.Parameters.AddWithValue("@vorschaulink", buch.VorschauLink);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Fehler bei der DB: {exp.Message}\nWurde die Datenbank schon angelegt?");
                    return false;
                }
            }
        }

        public bool Entfernen(IFavorisierbaresBook buch)
        {
            using (SqlConnection connection = new SqlConnection(Connection_String))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM TBL_Favorit WHERE ISBN = @isbn;";
                        command.Parameters.AddWithValue("@isbn", buch.ISBN);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Fehler bei der DB: {exp.Message}\nWurde die Datenbank schon angelegt?");
                    return false;
                }
            }
        }
    }
}
