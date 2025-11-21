// Diese using-Direktiven binden Namespaces ein, die wir gleich verwenden.
// 'System' brauchen wir z.B. für grundlegende Typen und Exceptions.
using System;
// 'System.Collections.Generic' brauchen wir für List<T>, also Listen von Objekten.
using System.Collections.Generic;
// Dieser Namespace kommt aus dem NuGet-Paket MySql.Data und enthält die
// Klassen für den MySQL-Zugriff (MySqlConnection, MySqlCommand, usw.).
using MySql.Data.MySqlClient;
// Dieser using verweist auf deine Model-Klassen (Autor, Buch, usw.),
// damit wir diese Typen hier bequem verwenden können.
using BFW_Project_011._2_G61_SQL_Bibliotheksverwaltung.Models;

// Der Namespace ordnet die Klasse logisch deinem Projekt zu.
// '.Data' signalisiert: Hier liegen Klassen für den Datenzugriff (Data Access Layer).
namespace BFW_Project_011._2_G61_SQL_Bibliotheksverwaltung.Data
{
    // Die Klasse 'Database' kapselt alles rund um den direkten Datenbankzugriff.
    // Von außen rufst du z.B. Methoden wie 'GetAlleAutoren()' auf, ohne dich
    // jedes Mal selbst um Verbindungen und SQL-Befehle kümmern zu müssen.
    public class Database
    {
        // Diese Konstante enthält den Verbindungs-String zur MySQL-Datenbank.
        // 'const' bedeutet: Der Wert kann zur Laufzeit nicht mehr geändert werden.
        //
        // WICHTIG:
        //  - Database=...  -> hier trägst du GENAU den Datenbanknamen ein,
        //                    den du in phpMyAdmin links im Baum siehst.
        //  - Uid=...       -> MySQL-Benutzer (bei MAMP meist 'root').
        //  - Pwd=...       -> Passwort (bei MAMP oft 'root' oder leer).
        //
        // Beispiel:
        //   "Server=localhost;Port=3306;Database=bfw_project_0112_g61_bibliothek;Uid=root;Pwd=root;";
        //
        // Du hast das hier bereits auf deinen Datenbanknamen angepasst.
        private const string ConnectionString =
            "Server=localhost;Port=3306;Database=bfw-project-011.2_g61-sql-bibliotheksverwaltung;Uid=root;Pwd=root;";

        // Diese private Hilfsmethode erzeugt eine neue MySqlConnection
        // mit dem zentral definierten ConnectionString.
        // Sie wird nur innerhalb der Database-Klasse benutzt.
        private MySqlConnection CreateConnection()
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            return connection;
        }

        // Diese Methode ist ein kleiner Hilfstest, um zu prüfen,
        // ob die Verbindung grundsätzlich funktioniert.
        // Du kannst sie z.B. in Form1 kurz aufrufen und schauen,
        // ob 'true' zurückkommt oder 'false'.
        public bool TestConnection()
        {
            // Wir verwenden ein try/catch, um Verbindungsfehler abzufangen.
            try
            {
                // 'using' sorgt dafür, dass die Verbindung am Ende des Blocks
                // automatisch geschlossen und freigegeben wird, auch bei Fehlern.
                using (MySqlConnection connection = CreateConnection())
                {
                    // Öffnet die Verbindung zur Datenbank.
                    connection.Open();
                    // Wenn 'Open()' ohne Exception durchläuft, war der Test erfolgreich.
                    return true;
                }
            }
            catch (Exception)
            {
                // Wenn beim Öffnen eine Exception auftritt (z.B. falsches Passwort,
                // Datenbank nicht erreichbar), fangen wir sie hier ab und melden 'false'.
                return false;
            }
        }

        // ---------------------------------------------------------------
        // 1. AUTOR – Hilfslisten und Insert
        // ---------------------------------------------------------------

        // Diese Methode liest alle Einträge aus der Tabelle 'autor' und gibt sie
        // als Liste von 'Autor'-Objekten zurück.
        public List<Autor> GetAlleAutoren()
        {
            // Hier legen wir eine leere Liste an, in die wir nach und nach
            // jeden gelesenen Autor einfügen.
            List<Autor> autoren = new List<Autor>();

            // Wieder ein 'using'-Block für die Verbindung, damit sie sauber
            // geöffnet und anschließend wieder geschlossen wird.
            using (MySqlConnection connection = CreateConnection())
            {
                // Verbindung zur Datenbank herstellen.
                connection.Open();

                // Dies ist der SQL-Text, den wir ausführen möchten.
                // Er fragt alle Spalten 'id' und 'name' aus der Tabelle 'autor' ab.
                const string sql = "SELECT id, name FROM autor";

                // MySqlCommand repräsentiert unseren SQL-Befehl,
                // der auf der geöffneten Verbindung ausgeführt wird.
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    // ExecuteReader führt den SELECT-Befehl aus und gibt ein
                    // 'MySqlDataReader'-Objekt zurück, mit dem wir Zeile für Zeile
                    // durch das Ergebnis iterieren können.
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Solange 'Read()' true zurückgibt, gibt es noch eine weitere Zeile.
                        while (reader.Read())
                        {
                            // Für jede Zeile erzeugen wir ein neues 'Autor'-Objekt
                            // und füllen seine Eigenschaften mit den Daten aus der aktuellen Zeile.
                            Autor autor = new Autor
                            {
                                // Hier lesen wir die Spalte 'id' als int aus.
                                // 'GetOrdinal("id")' liefert die Spaltenposition,
                                // 'GetInt32(...)' liest an dieser Position eine Ganzzahl.
                                Id = reader.GetInt32(reader.GetOrdinal("id")),

                                // Hier lesen wir die Spalte 'name' als string aus.
                                Name = reader.GetString(reader.GetOrdinal("name"))
                            };

                            // Das fertige Autor-Objekt fügen wir der Ergebnisliste hinzu.
                            autoren.Add(autor);
                        }
                    }
                }
            }

            // Wenn wir alle Zeilen verarbeitet haben, geben wir die
            // gefüllte Liste von Autoren an den Aufrufer zurück.
            return autoren;
        }

        // Diese Methode fügt einen neuen Autor in die Tabelle 'autor' ein.
        // Parameter:
        //   autor -> Das Autor-Objekt, das gespeichert werden soll. Wichtig ist hier vor allem 'autor.Name'.
        // Rückgabewert:
        //   Die neue, von der Datenbank vergebene ID (Primary Key) des Autors.
        public int InsertAutor(Autor autor)
        {
            // 'using' sorgt dafür, dass die Verbindung nach der Benutzung
            // automatisch wieder geschlossen und freigegeben wird – auch wenn
            // unterwegs eine Exception geworfen wird.
            using (MySqlConnection connection = CreateConnection())
            {
                // Öffnet die Verbindung zur Datenbank.
                connection.Open();

                // Dies ist der SQL-Befehl, den wir ausführen wollen.
                // Wir fügen in die Tabelle 'autor' einen neuen Datensatz ein
                // und setzen nur die Spalte 'name'.
                //
                // 'VALUES (@name)' bedeutet: Wir verwenden einen Parameter '@name',
                // den wir im Code noch mit einem Wert befüllen.
                //
                // 'SELECT LAST_INSERT_ID();' ist eine MySQL-Funktion, die uns
                // die ID des zuletzt eingefügten Datensatzes auf dieser Verbindung
                // zurückgibt. Genau das brauchen wir, um die neue Autor-ID zu erfahren.
                const string sql = "INSERT INTO autor (name) VALUES (@name); SELECT LAST_INSERT_ID();";

                // MySqlCommand repräsentiert unseren SQL-Befehl und weiß,
                // dass er auf der geöffneten 'connection' ausgeführt werden soll.
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    // Hier binden wir den Wert für den Parameter '@name' an den Befehl.
                    // 'autor.Name' ist der Name, den wir in die DB schreiben wollen.
                    //
                    // 'AddWithValue("@name", autor.Name)' sorgt dafür, dass MySQL diesen
                    // Wert sicher als Parameter bekommt (verhindert z.B. SQL-Injection).
                    command.Parameters.AddWithValue("@name", autor.Name);

                    // ExecuteScalar führt den SQL-Befehl aus und gibt den Wert aus
                    // der ersten Spalte der ersten Zeile des Ergebnisses zurück.
                    //
                    // In unserem SQL-Befehl ist das der Rückgabewert von
                    // 'SELECT LAST_INSERT_ID();', also die neue ID.
                    object result = command.ExecuteScalar();

                    // 'result' ist vom Typ object, wir wollen aber einen int.
                    // Convert.ToInt32 versucht, den Wert in einen int umzuwandeln.
                    int newId = Convert.ToInt32(result);

                    // Wir schreiben die neue ID direkt in das übergebene 'autor'-Objekt.
                    // So weiß der Aufrufer nach dem Insert, welche ID der Autor jetzt hat.
                    autor.Id = newId;

                    // Und geben die ID zusätzlich als Rückgabewert zurück.
                    return newId;
                }
            }
        }

        // ---------------------------------------------------------------
        // 2. GENRE – Hilfsliste
        // ---------------------------------------------------------------

        // Diese Methode liest alle Einträge aus der Tabelle 'genre' und gibt sie
        // als Liste von 'Genre'-Objekten zurück.
        public List<Genre> GetAlleGenres()
        {
            List<Genre> genres = new List<Genre>();

            using (MySqlConnection connection = CreateConnection())
            {
                connection.Open();

                const string sql = "SELECT id, name FROM genre";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Genre genre = new Genre
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Name = reader.GetString(reader.GetOrdinal("name"))
                            };

                            genres.Add(genre);
                        }
                    }
                }
            }

            return genres;
        }

        // ---------------------------------------------------------------
        // 3. KUNDE – Hilfsliste
        // ---------------------------------------------------------------

        // Diese Methode liest alle Einträge aus der Tabelle 'kunde' und gibt sie
        // als Liste von 'Kunde'-Objekten zurück.
        public List<Kunde> GetAlleKunden()
        {
            List<Kunde> kunden = new List<Kunde>();

            using (MySqlConnection connection = CreateConnection())
            {
                connection.Open();

                const string sql = "SELECT id, stammdaten FROM kunde";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Kunde kunde = new Kunde
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Stammdaten = reader.GetString(reader.GetOrdinal("stammdaten"))
                            };

                            kunden.Add(kunde);
                        }
                    }
                }
            }

            return kunden;
        }

        // ---------------------------------------------------------------
        // 4. BÜCHER – Kernfunktionen (CRUD + einfache Suche)
        // ---------------------------------------------------------------

        // Diese Methode liest alle Bücher aus der Tabelle 'buecher' und gibt sie
        // als Liste von 'Buch'-Objekten zurück.
        // Hinweis: Hier holen wir nur die reinen Buchdaten (inkl. AutorId, GenreId),
        // die Anzeige der Autor-/Genre-Namen passiert später im Frontend.
        public List<Buch> GetAlleBuecher()
        {
            List<Buch> buecher = new List<Buch>();

            using (MySqlConnection connection = CreateConnection())
            {
                connection.Open();

                const string sql = "SELECT id, titel, autor_id, genre_id, preis, isbn, neu_erscheinung FROM buecher";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Buch buch = new Buch
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Titel = reader.GetString(reader.GetOrdinal("titel")),
                                AutorId = reader.GetInt32(reader.GetOrdinal("autor_id")),
                                GenreId = reader.GetInt32(reader.GetOrdinal("genre_id")),
                                Preis = reader.GetDecimal(reader.GetOrdinal("preis")),
                                Isbn = reader.IsDBNull(reader.GetOrdinal("isbn"))
                                    ? string.Empty
                                    : reader.GetString(reader.GetOrdinal("isbn")),
                                NeuErscheinung = reader.GetBoolean(reader.GetOrdinal("neu_erscheinung"))
                            };

                            buecher.Add(buch);
                        }
                    }
                }
            }

            return buecher;
        }

        // Fügt ein neues Buch in die Tabelle 'buecher' ein.
        // Rückgabewert: die neue Buch-ID.
        public int InsertBuch(Buch buch)
        {
            using (MySqlConnection connection = CreateConnection())
            {
                connection.Open();

                const string sql =
                    "INSERT INTO buecher (titel, autor_id, genre_id, preis, isbn, neu_erscheinung) " +
                    "VALUES (@titel, @autor_id, @genre_id, @preis, @isbn, @neu_erscheinung); " +
                    "SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@titel", buch.Titel);
                    command.Parameters.AddWithValue("@autor_id", buch.AutorId);
                    command.Parameters.AddWithValue("@genre_id", buch.GenreId);
                    command.Parameters.AddWithValue("@preis", buch.Preis);
                    command.Parameters.AddWithValue("@isbn", buch.Isbn);
                    command.Parameters.AddWithValue("@neu_erscheinung", buch.NeuErscheinung);

                    object result = command.ExecuteScalar();
                    int newId = Convert.ToInt32(result);
                    buch.Id = newId;
                    return newId;
                }
            }
        }

        // Aktualisiert ein bestehendes Buch in der Tabelle 'buecher'.
        // Rückgabewert: true, wenn genau ein Datensatz betroffen war, sonst false.
        public bool UpdateBuch(Buch buch)
        {
            using (MySqlConnection connection = CreateConnection())
            {
                connection.Open();

                const string sql =
                    "UPDATE buecher " +
                    "SET titel = @titel, autor_id = @autor_id, genre_id = @genre_id, " +
                    "    preis = @preis, isbn = @isbn, neu_erscheinung = @neu_erscheinung " +
                    "WHERE id = @id;";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@titel", buch.Titel);
                    command.Parameters.AddWithValue("@autor_id", buch.AutorId);
                    command.Parameters.AddWithValue("@genre_id", buch.GenreId);
                    command.Parameters.AddWithValue("@preis", buch.Preis);
                    command.Parameters.AddWithValue("@isbn", buch.Isbn);
                    command.Parameters.AddWithValue("@neu_erscheinung", buch.NeuErscheinung);
                    command.Parameters.AddWithValue("@id", buch.Id);

                    int affected = command.ExecuteNonQuery();
                    return affected == 1;
                }
            }
        }

        // Löscht ein Buch aus der Tabelle 'buecher'.
        // Rückgabewert: true, wenn genau ein Datensatz gelöscht wurde.
        public bool DeleteBuch(int buchId)
        {
            using (MySqlConnection connection = CreateConnection())
            {
                connection.Open();

                const string sql = "DELETE FROM buecher WHERE id = @id;";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", buchId);

                    int affected = command.ExecuteNonQuery();
                    return affected == 1;
                }
            }
        }

        // Einfache Suchfunktion: findet Bücher anhand eines Suchbegriffs im Titel.
        // Hinweis: Du kannst hier später erweitern (z.B. auch nach Autorname),
        // aber für den Anfang reicht die Suche im Titel.
        public List<Buch> SucheBuecherNachTitel(string suchbegriff)
        {
            List<Buch> buecher = new List<Buch>();

            using (MySqlConnection connection = CreateConnection())
            {
                connection.Open();

                // Wir verwenden LIKE mit Platzhaltern.
                const string sql =
                    "SELECT id, titel, autor_id, genre_id, preis, isbn, neu_erscheinung " +
                    "FROM buecher " +
                    "WHERE titel LIKE @pattern;";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    // '%xyz%' findet den Suchbegriff an beliebiger Stelle im Titel.
                    string pattern = "%" + suchbegriff + "%";
                    command.Parameters.AddWithValue("@pattern", pattern);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Buch buch = new Buch
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Titel = reader.GetString(reader.GetOrdinal("titel")),
                                AutorId = reader.GetInt32(reader.GetOrdinal("autor_id")),
                                GenreId = reader.GetInt32(reader.GetOrdinal("genre_id")),
                                Preis = reader.GetDecimal(reader.GetOrdinal("preis")),
                                Isbn = reader.IsDBNull(reader.GetOrdinal("isbn"))
                                    ? string.Empty
                                    : reader.GetString(reader.GetOrdinal("isbn")),
                                NeuErscheinung = reader.GetBoolean(reader.GetOrdinal("neu_erscheinung"))
                            };

                            buecher.Add(buch);
                        }
                    }
                }
            }

            return buecher;
        }

        // ---------------------------------------------------------------
        // 5. BUCH_OBJEKTE – Verfügbare Exemplare
        // ---------------------------------------------------------------

        // Diese Methode liefert alle Buch-Exemplare (buch_objekte), die aktuell
        // den Status 'verfügbar' haben.
        public List<BuchObjekt> GetVerfuegbareBuchObjekte()
        {
            List<BuchObjekt> buchObjekte = new List<BuchObjekt>();

            using (MySqlConnection connection = CreateConnection())
            {
                connection.Open();

                // Wir gehen davon aus, dass in der Spalte 'status' ein Text wie
                // 'verfügbar' oder 'ausgeliehen' steht.
                const string sql = "SELECT id, buch_id, status FROM buch_objekte WHERE status = 'verfügbar';";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BuchObjekt buchObjekt = new BuchObjekt
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                BuchId = reader.GetInt32(reader.GetOrdinal("buch_id")),
                                Status = reader.GetString(reader.GetOrdinal("status"))
                            };

                            buchObjekte.Add(buchObjekt);
                        }
                    }
                }
            }

            return buchObjekte;
        }

        // ---------------------------------------------------------------
        // 6. VERLEIH – Buch verleihen und zurückgeben
        // ---------------------------------------------------------------

        // Verleiht ein Buch-Exemplar an einen Kunden.
        // - Es wird ein Eintrag in der Tabelle 'verleih' angelegt.
        // - Der Status des entsprechenden buch_objektes wird auf 'ausgeliehen' gesetzt.
        // Rückgabewert: true, wenn beide Operationen erfolgreich waren.
        public bool VerleiheBuch(int buchObjektId, int kundeId, DateTime ausleihdatum, DateTime vorraussRueckgabedatum)
        {
            using (MySqlConnection connection = CreateConnection())
            {
                connection.Open();

                // Wir benutzen eine Transaktion, damit entweder beide
                // Schritte funktionieren (INSERT + UPDATE) oder keiner.
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1) Verleih-Eintrag anlegen
                        const string insertSql =
                            "INSERT INTO verleih (buch_objekte_id, kunde_id, vorrauss_rückgabedatum, tatsach_rückgabedatum, ausleihdatum) " +
                            "VALUES (@buch_objekte_id, @kunde_id, @vorrauss, NULL, @ausleihdatum);";

                        using (MySqlCommand insertCommand = new MySqlCommand(insertSql, connection, transaction))
                        {
                            insertCommand.Parameters.AddWithValue("@buch_objekte_id", buchObjektId);
                            insertCommand.Parameters.AddWithValue("@kunde_id", kundeId);
                            insertCommand.Parameters.AddWithValue("@vorrauss", vorraussRueckgabedatum);
                            insertCommand.Parameters.AddWithValue("@ausleihdatum", ausleihdatum);

                            insertCommand.ExecuteNonQuery();
                        }

                        // 2) Status des Buch-Objekts auf 'ausgeliehen' setzen
                        const string updateSql =
                            "UPDATE buch_objekte SET status = 'ausgeliehen' WHERE id = @id;";

                        using (MySqlCommand updateCommand = new MySqlCommand(updateSql, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@id", buchObjektId);
                            updateCommand.ExecuteNonQuery();
                        }

                        // Wenn alles geklappt hat, Transaktion bestätigen.
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        // Bei Fehler: Transaktion zurückrollen.
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        // Markiert einen Verleihvorgang als zurückgegeben.
        // - Das Rückgabedatum in 'verleih' wird gesetzt.
        // - Der Status des entsprechenden buch_objektes wird wieder auf 'verfügbar' gesetzt.
        // Rückgabewert: true, wenn alles erfolgreich war.
        public bool RueckgabeBuch(int verleihId, DateTime tatsachRueckgabedatum)
        {
            using (MySqlConnection connection = CreateConnection())
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1) Zuerst herausfinden, welches buch_objekt betroffen ist.
                        int buchObjektId = -1;

                        const string selectSql =
                            "SELECT buch_objekte_id FROM verleih WHERE id = @verleihId;";

                        using (MySqlCommand selectCommand = new MySqlCommand(selectSql, connection, transaction))
                        {
                            selectCommand.Parameters.AddWithValue("@verleihId", verleihId);
                            object result = selectCommand.ExecuteScalar();

                            if (result == null)
                            {
                                // Kein passender Verleih gefunden.
                                transaction.Rollback();
                                return false;
                            }

                            buchObjektId = Convert.ToInt32(result);
                        }

                        // 2) Verleih-Datensatz mit tatsächlichem Rückgabedatum aktualisieren.
                        const string updateVerleihSql =
                            "UPDATE verleih SET tatsach_rückgabedatum = @rueckgabeDatum WHERE id = @verleihId;";

                        using (MySqlCommand updateVerleihCommand = new MySqlCommand(updateVerleihSql, connection, transaction))
                        {
                            updateVerleihCommand.Parameters.AddWithValue("@rueckgabeDatum", tatsachRueckgabedatum);
                            updateVerleihCommand.Parameters.AddWithValue("@verleihId", verleihId);
                            updateVerleihCommand.ExecuteNonQuery();
                        }

                        // 3) Status des Buch-Objekts wieder auf 'verfügbar' setzen.
                        const string updateBuchObjektSql =
                            "UPDATE buch_objekte SET status = 'verfügbar' WHERE id = @buchObjektId;";

                        using (MySqlCommand updateBuchObjektCommand = new MySqlCommand(updateBuchObjektSql, connection, transaction))
                        {
                            updateBuchObjektCommand.Parameters.AddWithValue("@buchObjektId", buchObjektId);
                            updateBuchObjektCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
