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
        // Bitte den Platzhalter 'DEIN_DATENBANKNAME' unten durch deinen echten
        // Datenbanknamen ersetzen.
        private const string ConnectionString =
            "Server=localhost;Port=3306;Database=bfw-project-011.2_g61-sql-bibliotheksverwaltung;Uid=root;Pwd=root;";

        // Diese Methode ist ein kleiner Hilfstest, um zu prüfen,
        // ob die Verbindung grundsätzlich funktioniert.
        // Du kannst sie z.B. in Form1 kurz aufrufen und schauen,
        // ob 'true' zurückkommt oder eine Exception geworfen wird.
        public bool TestConnection()
        {
            // Wir verwenden ein try/catch, um Verbindungsfehler abzufangen.
            try
            {
                // 'using' sorgt dafür, dass die Verbindung am Ende des Blocks
                // automatisch geschlossen und freigegeben wird, auch bei Fehlern.
                using (var connection = new MySqlConnection(ConnectionString))
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

        // Diese Methode liest alle Einträge aus der Tabelle 'autor' und gibt sie
        // als Liste von 'Autor'-Objekten zurück.
        public List<Autor> GetAlleAutoren()
        {
            // Hier legen wir eine leere Liste an, in die wir nach und nach
            // jeden gelesenen Autor einfügen.
            var autoren = new List<Autor>();

            // Wieder ein 'using'-Block für die Verbindung, damit sie sauber
            // geöffnet und anschließend wieder geschlossen wird.
            using (var connection = new MySqlConnection(ConnectionString))
            {
                // Verbindung zur Datenbank herstellen.
                connection.Open();

                // Dies ist der SQL-Text, den wir ausführen möchten.
                // Er fragt alle Spalten 'id' und 'name' aus der Tabelle 'autor' ab.
                const string sql = "SELECT id, name FROM autor";

                // MySqlCommand repräsentiert unseren SQL-Befehl,
                // der auf der geöffneten Verbindung ausgeführt wird.
                using (var command = new MySqlCommand(sql, connection))
                {
                    // ExecuteReader führt den SELECT-Befehl aus und gibt ein
                    // 'MySqlDataReader'-Objekt zurück, mit dem wir Zeile für Zeile
                    // durch das Ergebnis iterieren können.
                    using (var reader = command.ExecuteReader())
                    {
                        // Solange 'Read()' true zurückgibt, gibt es noch eine weitere Zeile.
                        while (reader.Read())
                        {
                            // Für jede Zeile erzeugen wir ein neues 'Autor'-Objekt
                            // und füllen seine Eigenschaften mit den Daten aus der aktuellen Zeile.

                            var autor = new Autor
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
            using (var connection = new MySqlConnection(ConnectionString))
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
                using (var command = new MySqlCommand(sql, connection))
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

    }
}
