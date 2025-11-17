
// Hier ggf. noch aufräumen ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFW_Project_011._2_G61_SQL_Bibliotheksverwaltung.Models
{
    // Repräsentiert einen Kunden aus der Tabelle 'kunde'.
    public class Kunde
    {
        // Eindeutige ID des Kunden (Spalte 'id').
        public int Id { get; set; }

        // 'stammdaten' ist in deinem Script ein freies Textfeld.
        // Hier könntest du z.B. Name, Adresse, Kontaktinfos als String speichern.
        public string Stammdaten { get; set; } = string.Empty;
    }
}
