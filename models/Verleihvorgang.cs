
// Hier ggf. noch aufräumen ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFW_Project_011._2_G61_SQL_Bibliotheksverwaltung.Models
{
    // Repräsentiert einen Verleihvorgang aus der Tabelle 'verleih'.
    // Also das Ereignis: "Kunde X leiht Buch-Exemplar Y vom Datum A bis Datum B".
    public class Verleihvorgang
    {
        // Eindeutige ID des Verleihvorgangs (Spalte 'id').
        public int Id { get; set; }

        // Verknüpfung auf ein konkretes Buch-Exemplar (Spalte 'buch_objekte_id').
        // Damit weißt du, welches einzelne Exemplar ausgeliehen wurde.
        public int BuchObjektId { get; set; }

        // Verknüpfung auf einen Kunden (Spalte 'kunde_id').
        // Dieser Kunde hat das Buch ausgeliehen.
        public int KundeId { get; set; }

        // Voraussichtliches Rückgabedatum (Spalte 'vorrauss_rueckgabedatum').
        // Laut DB-Definition NOT NULL → hier als 'DateTime' ohne '?'
        public DateTime VorraussRueckgabedatum { get; set; }

        // Tatsächliches Rückgabedatum (Spalte 'tatsach_rueckgabedatum').
        // In deinem Script ist das auch NOT NULL – realistisch wäre es optional,
        // solange das Buch noch nicht zurückgegeben wurde.
        public DateTime TatsachRueckgabedatum { get; set; }

        // Datum, an dem das Buch ausgeliehen wurde (Spalte 'ausleihdatum').
        public DateTime Ausleihdatum { get; set; }
    }
}
