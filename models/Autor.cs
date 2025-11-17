
// 'using System;' wird für grundlegende .NET-Typen verwendet.
// Hier brauchen wir es zwar noch nicht unbedingt, aber es ist
// eine gute Standardvorgabe für jede Klasse.

// Hier später noch ggf. aufräumen ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Der Namespace ordnet deine Klassen logisch deinem Projekt zu.
// '.Models' zeigt: Diese Klassen gehören zur Model-Schicht (Daten-Modelle).
namespace BFW_Project_011._2_G61_SQL_Bibliotheksverwaltung.Models
{
    // 'public' bedeutet: Diese Klasse kann von überall im Projekt benutzt werden.
    // 'class' definiert einen neuen Datentyp namens 'Autor'.
    public class Autor
    {
        // Dies ist die ID des Autors, entspricht der Spalte 'id' in der Tabelle 'autor'.
        // 'int' ist ein ganzzahliger Typ.
        // 'get; set;' sind Auto-Properties: damit kannst du den Wert lesen und setzen.
        public int Id { get; set; }

        // Der Name des Autors, entspricht der Spalte 'name' in der DB.
        // 'string' ist eine Text-Eigenschaft.
        // '= string.Empty;' setzt einen Standardwert (leerer String),
        // damit 'Name' nicht null ist, wenn du ein neues Autor-Objekt erzeugst.
        public string Name { get; set; } = string.Empty;
    }
}
