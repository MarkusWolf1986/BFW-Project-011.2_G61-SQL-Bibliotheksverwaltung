
// Hier ggf. noch aufräumen ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BFW_Project_011._2_G61_SQL_Bibliotheksverwaltung.Models
{
    // Repräsentiert einen Genre-Eintrag aus der Tabelle 'genre'.
    public class Genre
    {
        // Eindeutige ID des Genres (Spalte 'id').
        public int Id { get; set; }

        // Bezeichnung des Genres (Spalte 'name'), z.B. "Krimi", "Roman" etc.
        public string Name { get; set; } = string.Empty;
    }
}
