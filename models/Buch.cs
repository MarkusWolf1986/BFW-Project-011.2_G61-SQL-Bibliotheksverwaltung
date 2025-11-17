
// Hier ggf. noch aufräumen ...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFW_Project_011._2_G61_SQL_Bibliotheksverwaltung.Models
{
    // Repräsentiert ein Buch aus der Tabelle 'buecher'.
    public class Buch
    {
        // Eindeutige ID des Buches (Spalte 'id').
        public int Id { get; set; }

        // Titel des Buches (Spalte 'titel').
        public string Titel { get; set; } = string.Empty;

        // Fremdschlüssel auf 'autor.id' (Spalte 'autor_id').
        // Enthält die ID des Autors, zu dem dieses Buch gehört.
        public int AutorId { get; set; }

        // Fremdschlüssel auf 'genre.id' (Spalte 'genre_id').
        public int GenreId { get; set; }

        // Preis des Buches (Spalte 'preis', DECIMAL(6,3)).
        // 'decimal' eignet sich gut für Geldbeträge.
        public decimal Preis { get; set; }

        // ISBN-Nummer (Spalte 'isbn').
        // Mit 'string?' erlaubst du, dass der Wert auch mal null sein kann.
        // ALT (macht den Fehler):
        // public string? Isbn { get; set; }

        // NEU (ohne Nullable-Feature):
        public string Isbn { get; set; } = string.Empty;  // Standard: leerer String statt null


        // Gibt an, ob es sich um eine Neuerscheinung handelt (Spalte 'neu_erscheinung').
        // MySQL-BOOLEAN wird in C# als bool abgebildet.
        public bool NeuErscheinung { get; set; }
    }
}
