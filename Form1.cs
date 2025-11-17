// Diese using-Direktiven binden Namespaces ein, also "Namensräume",
// in denen fertige Klassen und Funktionen liegen, die wir benutzen können.

// Basisnamenraum von .NET mit grundlegenden Typen wie 'String', 'Int32', 'Exception' usw.
using System;
// Sammlungstypen wie List<T>, Dictionary<TKey, TValue> usw.
using System.Collections.Generic;
// Für Komponentenmodell, z.B. Designer-Unterstützung (lassen wir vorerst drin).
using System.ComponentModel;
// Für DataSet, DataTable usw. (könnte später noch gebraucht werden).
using System.Data;
// Für Zeichenfunktionen, Bitmaps, Farben usw. (WinForms-Grafik)
using System.Drawing;
// Für LINQ-Abfragen (Abfrage-Syntax auf Sammlungen); aktuell nicht zwingend nötig,
// aber schadet nicht.
using System.Linq;
// Für Zeichenkettenbearbeitung, Encoding usw.
using System.Text;
// Für asynchrone Programmierung (Tasks); aktuell nicht genutzt, aber ok.
using System.Threading.Tasks;
// Alles rund um Windows-Forms-Steuerelemente (Form, Button, TextBox, …)
using System.Windows.Forms;

// Dieser using-Namespace macht die Klasse 'Database' aus dem Ordner 'Data'
// in dieser Datei bekannt, sodass wir 'Database' direkt verwenden können,
// ohne jedes Mal den kompletten Namespace davor schreiben zu müssen.
using BFW_Project_011._2_G61_SQL_Bibliotheksverwaltung.Data;

namespace BFW_Project_011._2_G61_SQL_Bibliotheksverwaltung
{
    // 'partial' bedeutet, dass die Klasse 'Form1' in mehrere Dateien
    // aufgeteilt ist: hier ist der Teil mit deinem Code,
    // im 'Form1.Designer.cs' liegt der vom Designer erzeugte Teil.
    // ': Form' heißt: Form1 erbt von der WinForms-Basisklasse 'Form'
    // und ist damit ein ganz normales Fenster.
    public partial class Form1 : Form
    {
        // Dies ist der Konstruktor von Form1. Er wird genau einmal aufgerufen,
        // wenn du irgendwo 'new Form1()' schreibst (z.B. in Program.cs).
        public Form1()
        {
            // Diese Methode wird im vom Designer erzeugten Teil definiert.
            // Sie legt alle Steuerelemente an (Buttons, Textboxen, etc.)
            // und setzt ihre Eigenschaften (Position, Größe, Text, ...).
            InitializeComponent();

            // Hier registrieren wir unser eigenes Ereignis-Handling für das 'Load'-Ereignis.
            // 'this.Load' ist das Ereignis, das ausgelöst wird, wenn das Formular
            // vollständig geladen wurde und gleich angezeigt wird.
            // 'Form1_Load' ist der Name der Methode, die aufgerufen werden soll,
            // wenn dieses Ereignis eintritt.
            this.Load += Form1_Load;
        }

        // Diese Methode wird automatisch aufgerufen, wenn das Formular
        // geladen wurde (also kurz bevor es sichtbar wird).
        // Der 'sender' ist das Objekt, das das Ereignis ausgelöst hat (hier das Formular selbst),
        // 'e' enthält zusätzliche Event-Informationen (hier nicht weiter genutzt).
        private void Form1_Load(object sender, EventArgs e)
        {
            // Erzeugt ein neues Objekt unserer Database-Klasse.
            // Über dieses Objekt sprechen wir mit der MySQL-Datenbank.
            var db = new Database();

            // Ruft die TestConnection-Methode auf.
            // Sie versucht, eine Verbindung aufzubauen und liefert:
            //   true  -> Verbindung erfolgreich geöffnet
            //   false -> beim Öffnen ist ein Fehler aufgetreten
            bool ok = db.TestConnection();

            // Je nach Ergebnis zeigen wir eine passende Meldung an.
            if (ok)
            {
                // Falls die Verbindung erfolgreich war, informieren wir dich kurz.
                MessageBox.Show(
                    "Datenbankverbindung erfolgreich!",   // Text in der Nachricht
                    "Info"                                // Titelleiste des Fensters
                );
            }
            else
            {
                // Falls die Verbindung fehlgeschlagen ist, weisen wir dich darauf hin.
                // Typische Ursachen:
                //  - MAMP/MySQL ist nicht gestartet
                //  - ConnectionString in Database.cs hat falschen DB-Namen / Benutzer / Passwort
                MessageBox.Show(
                    "Datenbankverbindung fehlgeschlagen.\n" +
                    "Bitte ConnectionString und MAMP-Server prüfen.",
                    "Fehler"
                );
            }
        }
    }
}
