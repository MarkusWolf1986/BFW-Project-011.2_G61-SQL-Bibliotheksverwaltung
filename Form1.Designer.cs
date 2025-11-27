// Chaty sagt zu dieser Datei unter anderem:
//
// Diese Datei wurde vom Windows-Forms-Designer automatisch generiert.
// Hier macht man zu 99 % keine Änderungen von Hand,
// da der Designer sonst durcheinanderkommt.
//
// Also am besten nichts hier ändern!
//
// AUSNAHME (Lernzweck):
// Wenn man versteht, wie der Designer arbeitet, darf man sich den Code ansehen.
// Wir fügen hier als Beispiel eine "Überschrift" (Label) hinzu.
// In der Praxis würdest du das Label im Designer platzieren;
// dann schreibt Visual Studio diesen Code automatisch.

namespace BFW_Project_011._2_G61_SQL_Bibliotheksverwaltung
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// Alle Controls (Buttons, Labels, Grids, ...) werden in components verwaltet,
        /// damit sie später sauber entsorgt werden können.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // NEU: Das ist unser Label, das als Überschrift dient.
        // 'private' bedeutet: Nur innerhalb dieser Klasse sichtbar.
        private System.Windows.Forms.Label lblUeberschrift;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            // Wenn dispose aufgerufen wird und wir verwaltete Ressourcen entsorgen sollen...
            if (disposing && (components != null))
            {
                // ...dann räumen wir die Komponenten sauber auf.
                components.Dispose();
            }

            // Danach rufen wir die Basisklasse auf, damit sie auch aufräumen kann.
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            // Diese Zeile legt den "components"-Container an.
            this.components = new System.ComponentModel.Container();

            // ------------------------------------------------------------
            // NEU: Überschrift-Label erstellen und konfigurieren
            // ------------------------------------------------------------

            // Wir erzeugen ein neues Label-Objekt.
            this.lblUeberschrift = new System.Windows.Forms.Label();

            // AutoSize = true bedeutet: Das Label passt seine Größe automatisch
            // an den Text an (praktisch für Überschriften).
            this.lblUeberschrift.AutoSize = true;

            // Font: Schriftart, Größe und Stil (Bold für Überschrift).
            // "Segoe UI" ist eine gängige Windows-Schrift.
            this.lblUeberschrift.Font = new System.Drawing.Font(
                "Segoe UI",                      // Schriftart
                18F,                             // Schriftgröße
                System.Drawing.FontStyle.Bold,   // Fett
                System.Drawing.GraphicsUnit.Point // Einheit (Standard)
            );

            // Location: Position auf dem Formular (X, Y).
            // (12, 9) ist typisch "oben links mit etwas Abstand".
            this.lblUeberschrift.Location = new System.Drawing.Point(12, 9);

            // Name: interner Name, damit du es im Code ansprechen könntest.
            this.lblUeberschrift.Name = "lblUeberschrift";

            // TabIndex: Reihenfolge beim Tabben durch Controls.
            // Für eine Überschrift nicht super wichtig, aber der Designer setzt es immer.
            this.lblUeberschrift.TabIndex = 0;

            // Text: Das, was angezeigt wird (unsere Überschrift).
            this.lblUeberschrift.Text = "Bibliotheksverwaltung";

            // ------------------------------------------------------------
            // Form (Fenster) konfigurieren
            // ------------------------------------------------------------

            // AutoScaleMode: Windows Forms skaliert UI je nach DPI/Font.
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            // ClientSize: Größe des Inhaltsbereichs (ohne Rahmen/Titelleiste).
            this.ClientSize = new System.Drawing.Size(800, 450);

            // Text: Fenster-Titel in der Titelleiste.
            this.Text = "Bibliotheksverwaltung";

            // ------------------------------------------------------------
            // Controls zum Formular hinzufügen (wichtig!)
            // Ohne Controls.Add(...) sieht man das Label nicht.
            // ------------------------------------------------------------
            this.Controls.Add(this.lblUeberschrift);
        }

        #endregion
    }
}
