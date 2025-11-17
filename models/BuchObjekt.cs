
// Hier ggf. noch aufräumen ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFW_Project_011._2_G61_SQL_Bibliotheksverwaltung.Models
{
    // Repräsentiert ein konkretes Exemplar eines Buches (Tabelle 'buch_objekte').
    // Beispiel: Du hast das Buch "Herr der Ringe" vielleicht dreimal im Regal.
    // Jedes Exemplar ist ein eigenes 'BuchObjekt'.
    public class BuchObjekt
    {
        // Eindeutige ID dieses Buch-Exemplars (Spalte 'id').
        public int Id { get; set; }

        // Verknüpfung zur Tabelle 'buecher' (Spalte 'buch_id').
        // Hier steht, zu welchem Buch-Titel dieses Exemplar gehört.
        public int BuchId { get; set; }

        // Status des Exemplars (Spalte 'status'), z.B.:
        // "verfügbar", "ausgeliehen", "reserviert", …
        public string Status { get; set; } = string.Empty;
    }
}
