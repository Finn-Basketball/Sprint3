using System.Collections.Generic;
using AntMe.Deutsch;

namespace AntMe.Player.Finn
{
    /// <summary>
    /// Diese Datei enthält die Beschreibung für deine Ameise. Die einzelnen Code-Blöcke 
    /// (Beginnend mit "public override void") fassen zusammen, wie deine Ameise in den 
    /// entsprechenden Situationen reagieren soll. Welche Befehle du hier verwenden kannst, 
    /// findest du auf der Befehlsübersicht im Wiki.
    /// 
    /// Wenn du etwas Unterstützung bei der Erstellung einer Ameise brauchst, findest du
    /// in den AntMe!-Lektionen ein paar Schritt-für-Schritt Anleitungen.
    ///
    /// Link zum Wiki: https://wiki.antme.net
    /// </summary>
    [Spieler(
        Volkname = "Finn",   // Hier kannst du den Namen des Volkes festlegen
        Vorname = "",       // An dieser Stelle kannst du dich als Schöpfer der Ameise eintragen
        Nachname = ""       // An dieser Stelle kannst du dich als Schöpfer der Ameise eintragen
    )]

    /// Kasten stellen "Berufsgruppen" innerhalb deines Ameisenvolkes dar. Du kannst hier mit
    /// den Fähigkeiten einzelner Ameisen arbeiten. Wie genau das funktioniert kannst du der 
    /// Lektion zur Spezialisierung von Ameisen entnehmen.
    [Kaste(
        Name = "Kämpfer",                  // Name der Berufsgruppe
        AngriffModifikator = 1,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = 1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 0,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = -1,                // Tragkraft einer Ameise
        ReichweiteModifikator = 1,          // Ausdauer einer Ameise
        SichtweiteModifikator = -1           // Sichtweite einer Ameise


    )]
    [Kaste(
        Name = "Träger",                  // Name der Berufsgruppe
        AngriffModifikator = -1,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = -1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 0,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = 2,                // Tragkraft einer Ameise
        ReichweiteModifikator = 1,          // Ausdauer einer Ameise
        SichtweiteModifikator = 0           // Sichtweite einer Ameise


    )]
    [Kaste(
        Name = "Späher",                  // Name der Berufsgruppe
        AngriffModifikator = -1,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = -1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 1,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = -1,                // Tragkraft einer Ameise
        ReichweiteModifikator = 1,          // Ausdauer einer Ameise
        SichtweiteModifikator = 2           // Sichtweite einer Ameise


    )]


    [Kaste(
        Name = "Verteidiger",                  // Name der Berufsgruppe
        AngriffModifikator = 2,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = 1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 0,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = -1,                // Tragkraft einer Ameise
        ReichweiteModifikator = -1,          // Ausdauer einer Ameise
        SichtweiteModifikator = 0           // Sichtweite einer Ameise


    )]

    [Kaste(
        Name = "Anführer",                  // Name der Berufsgruppe
        AngriffModifikator = -1,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = 0, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = 0,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = -1,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = 0,                // Tragkraft einer Ameise
        ReichweiteModifikator = 0,          // Ausdauer einer Ameise
        SichtweiteModifikator = 2           // Sichtweite einer Ameise
        )]
    public class yurrKlasse : Basisameise
    {
        public yurrKlasse() { }

        // Kasten
        #region Rollen
        private Zucker letzterZucker = null;
        private Obst letzterObst = null;
        private Markierung letzterHinweis = null;

        private Spielobjekt letzterFundort = null;
        private bool gehtHeimMitFund = false;
        private bool kehrtZurückZumFundort = false;
        private bool warImBau = false;
       

        public string[] KasteNamen => new[] { "Kämpfer", "Träger", "Späher", "Verteidiger", "Anführer" };

        public override string BestimmeKaste(Dictionary<string, int> anzahl)
        {
            int gesamt = 0;
            foreach (var paar in anzahl)
                gesamt += paar.Value;

            if (gesamt % 6 == 0)
            {
                Denke("Ich bin Anführer!");
                return "Anführer";
            }
            if (gesamt % 5 == 0)
            {
                Denke("Ich bin Verteidiger!");
                return "Verteidiger";
            }


            int nummer = gesamt % 12;
            if (nummer < 4)
            {
                Denke("Ich bin Kämpfer!");
                return "Kämpfer";
            }
            if (nummer < 8)
            {
                Denke("Ich bin Träger!");
                return "Träger";
            }

            Denke("Ich bin Späher!");
            return "Späher";
        }
        #endregion Rollen
       


        #region Nahrung_Markierung 
       

        

        




        public void Sieht(Nahrung nahrung)
        {
            if (Kaste == "Träger" && AktuelleLast == 0)
            {
                GeheZuZiel(nahrung);
                Denke("Ich bringe das ins Lager!");
            }
            else if (Kaste == "Späher")
            {
                SprüheMarkierung(0); // 0 = Nahrungsspur
                Denke("Hier ist was! Markiere die Stelle.");
            }
        }

        public void ZielErreicht(Nahrung nahrung)
        {
            if (Kaste == "Träger")
            {
                Nimm(nahrung);
                GeheZuBau();
            }
        }

        public override void ZielErreicht(Obst obst)
        {
            if (Kaste == "Träger")
            {
                Nimm(obst);
                letzterObst = obst;
                Denke("Obst gesehen und gespeichert!");

                GeheZuBau();
            }
        }

        public override void ZielErreicht(Zucker zucker)
        {
            if (Kaste == "Träger")
            {


                Nimm(zucker);
                letzterZucker = zucker;
                Denke("Zucker gesehen und gespeichert!");

                SprüheMarkierung((int)0, 100);
                Denke("Zucker gefunden! Nimm es mit!");

                if (!(Ziel is Wanze))
                {
                    if (AktuelleLast == 0 && Ziel == null)
                    {
                        GeheGeradeaus();
                        GeheZuBau();
                    }
                }
            }
        }



        public void RiechtMarkierung(int typ)
        {
            if (typ == 0 && AktuelleLast == 0 && Ziel == null)
            {
                GeheGeradeaus();
                Denke("Ich folge einer Markierung!");
            }
        }

        





        public enum Markierungstyp
        {
            Zucker, // typ 0 = zucker
            Obst, // typ 1 = obst
            Feind // typ 2 feind
        }

        //zucke gesehen
        public override void Sieht(Zucker zucker)
        {
            SprüheMarkierung((int)Markierungstyp.Zucker);
            letzterZucker = zucker;
            Denke("Zucker gesehen und gespeichert!");
            Nimm(zucker);
            GeheZuBau();


        }
        public override void Sieht(Obst obst)
        {
            SprüheMarkierung((int)Markierungstyp.Zucker);
            letzterObst = obst;
            Denke("Obst gesehen und gespeichert!");
            Nimm(obst);
            GeheZuBau();


        }
        #endregion Nahrung_Markierung

        #region Tick
        public override void Tick()
        {
            if (AktuelleLast > 0)
            {
                // Fortlaufende Markierung auf dem Heimweg
                SprüheMarkierung(0, 50);  // Typ 0 = Nahrungsspur, 50 Ticks sichtbar
                GeheZuBau();
                Denke("Ich markiere den Rückweg zum Bau!");
                return;
            }

            if (Kaste == "Anführer")
            {
                // 1. Wenn Ameise Nahrung trägt → gehe nach Hause & markiere
                if (AktuelleLast > 0 && gehtHeimMitFund)
                {
                    SprüheMarkierung(0, 50);
                    GeheZuBau();
                    Denke("Ich gehe heim und markiere den Weg!");
                    return;
                }

                // 2. Wenn Ameise im Bau war → jetzt zurück zum Fundort
                if (warImBau && letzterFundort != null)
                {
                    warImBau = false;
                    kehrtZurückZumFundort = true;
                    SprüheMarkierung(1, 50);
                    GeheZuZiel(letzterFundort);
                    Denke("Ich kehre zum Fundort zurück!");
                    return;
                }

                // 3. Wenn Ameise auf dem Rückweg → markiere erneut
                if (kehrtZurückZumFundort && Ziel != null)
                {
                    SprüheMarkierung(1, 50); // zweite Spur zurück zum Fund
                    Denke("Ich markiere zurück zum Fund!");
                }

                // 4. Sonst: normales Verhalten
                if (Ziel != null)
                {

                    GeheGeradeaus();
                    Denke("Ich suche etwas für mein Team.");
                }

                return;
            }


            if (Kaste == "Träger")
            {
                if (AktuelleLast == 0 && Ziel == null)
                {
                    if (letzterHinweis != null)
                    {
                        GeheZuZiel(letzterHinweis);
                        Denke("Ich folge dem Anführer!");
                        return;
                    }

                }
            }

            if (AktuelleLast > 0 && Ziel == null)
            {
                GeheZuBau();
                Denke("Ich bringe Nahrung heim!");
                return;
            }

            if (Ziel == null && AktuelleLast == 0)
            {
                if (letzterZucker != null)
                {
                    GeheZuZiel(letzterZucker);
                    Denke("Ich laufe direkt zum Zucker, ohne Umweg!");
                }
                else if (letzterObst != null)
                {
                    GeheZuZiel(letzterObst);
                    Denke("Ich laufe direkt zum Obst, ohne Umweg!");
                }

                if (Kaste == "Verteidiger")
                {
                    // Patrouilliere im Kreis um den Bau
                    if (Ziel == null)
                    {
                        if (Zufall.Zahl(0, 2) == 0)
                            DreheUmWinkel(90); // Patrouilliert
                        else
                            DreheUmWinkel(-90);

                        GeheGeradeaus();
                        Denke("Ich verteidige den Bau!");
                    }

                    return; // Nicht weiter nachdenken
                }


            }
        }

        #endregion Tick


        #region Müde


        public override void Wartet()
        {
            GeheGeradeaus();
        }







        private int müdeZähler = 0;

        public override void WirdMüde()
        {
            int reichweiteProzent = (int)(Reichweite * 100);

            // Wenn Reichweite unter 33%
            if (reichweiteProzent < 33)
            {
                if (EntfernungZuBau > Reichweite)
                {
                    müdeZähler++;

                    Denke("Zurückgelegt: " + ZurückgelegteStrecke + " | Reichweite: " + reichweiteProzent + "%");

                    if (müdeZähler >= 2)
                    {
                        Denke("Zu müde! Ich gehe zurück zum Bau.");
                        GeheZuBau();
                        müdeZähler = 0;
                    }
                }
            }
            else
            {
                müdeZähler = 0;
            }
        }














        #endregion



        #region Kommunikation

        public override void RiechtFreund(Markierung markierung)
        {


            if (Ziel == null)
            {
                GeheZuZiel(markierung);
            }
        }








        public override void SiehtFreund(Ameise ameise)
        {
            if (ameise.GetType() == this.GetType())
            {
                Denke("Hallo, Kamerad!");
            }

        }

        public override void SiehtVerbündeten(Ameise ameise)
        {
        }

        #endregion

        #region Kampf


        public override void SiehtFeind(Ameise ameise)
        {
            if (ameise.GetType() != this.GetType())
            {
                GreifeAn(ameise);
                Denke("Feind entdeckt  Angriff!");
            }
        }

        public void Sieht(Wanze wanze)
        {
            if (Kaste == "Kämpfer" || Kaste == "Verteidiger")
            {
                GreifeAn(wanze);
                Denke("Wanze gesichtet – Angriff!");
            }
            else
            {
                // Andere weichen aus
                if (Zufall.Zahl(0, 2) == 0)
                    DreheUmWinkel(-90);
                else
                    DreheUmWinkel(90);

                GeheGeradeaus();
                Denke("Ich bin kein Kämpfer – ich weiche aus!");
            }
        }






        public override void WirdAngegriffen(Ameise ameise)
        {
        }


        public override void WirdAngegriffen(Wanze wanze)
        {
            if (AktuelleEnergie < MaximaleEnergie / 2)
            {
                GeheZuBau();
            }
        }

        #endregion

        
    }
}








