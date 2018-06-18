using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe__DELUXE
{
    public class Spiel
    {
        private Spielfeld m_Spielfeld;
        public Spielfeld Spielfeld { get { return m_Spielfeld; } set { m_Spielfeld = value; } }

        private List<Spieler> m_Spieler;
        public List<Spieler> Spieler { get { return m_Spieler; } set { m_Spieler = value; } }

        private int cursorX;
        private int cursorY;

        public Spiel()
        {
            Start();
        }

        private void Start()
        {
            Spielfeld = new Spielfeld();
            Spieler = new List<Spieler>();
            Spieler.Add(new Spieler("X", true));
            Spieler.Add(new Spieler("O"));

            ZeichneSpielfeld();
            Feldauswahl();
            if (NeustartInitiieren())
                Start();
        }

        private void ZeichneSpielfeld()
        {
            Console.Clear();
            Console.WriteLine("   {0}   |   {1}   |   {2}   ", Spielfeld[0].Typ.ToString().Replace("Leer", " "), Spielfeld[1].Typ.ToString().Replace("Leer", " "), Spielfeld[2].Typ.ToString().Replace("Leer", " "));
            Console.WriteLine("----------------------");
            Console.WriteLine("   {0}   |   {1}   |   {2}   ", Spielfeld[3].Typ.ToString().Replace("Leer", " "), Spielfeld[4].Typ.ToString().Replace("Leer", " "), Spielfeld[5].Typ.ToString().Replace("Leer", " "));
            Console.WriteLine("----------------------");
            Console.WriteLine("   {0}   |   {1}   |   {2}   ", Spielfeld[6].Typ.ToString().Replace("Leer", " "), Spielfeld[7].Typ.ToString().Replace("Leer", " "), Spielfeld[8].Typ.ToString().Replace("Leer", " "));
            Console.WriteLine();
            Console.WriteLine("Dein Zug, Spieler " + Spieler.First(x => x.MeinZug).Zeichen);

            SetzeCursorNachKoordinaten();
        }

        private void Feldauswahl()
        {
            bool warteAufFeldwahl = true;
            do
            {
                ConsoleKeyInfo eingabe = Console.ReadKey();
                switch (eingabe.Key)
                {
                    case ConsoleKey.LeftArrow: VersucheNachLinksZuBewegen(); break;
                    case ConsoleKey.RightArrow: VersucheNachRechtsZuBewegen(); break;
                    case ConsoleKey.UpArrow: VersucheNachObenZuBewegen(); break;
                    case ConsoleKey.DownArrow: VersucheNachUntenZuBewegen(); break;
                    case ConsoleKey.Enter: if (VersucheZuSetzen()) warteAufFeldwahl = false; break;
                    default: continue;
                };
                SetzeCursorNachKoordinaten();
                ZeichneSpielfeld();
            } while (warteAufFeldwahl);

            if (!SpielZuEnde())
            {
                ZeichneSpielfeld();
                Spielerwechsel();
                Feldauswahl();
            }
        }

        private void Spielerwechsel()
        {
            foreach (Spieler spieler in Spieler)
                spieler.MeinZug = !spieler.MeinZug;
        }

        private void VersucheNachLinksZuBewegen()
        {
            if (cursorX > 0)
                cursorX--;
        }

        private void VersucheNachRechtsZuBewegen()
        {
            if (cursorX < 2)
                cursorX++;
        }

        private void VersucheNachObenZuBewegen()
        {
            if (cursorY > 0)
                cursorY--;
        }

        private void VersucheNachUntenZuBewegen()
        {
            if (cursorY < 2)
                cursorY++;
        }

        private void SetzeCursorNachKoordinaten()
        {
            int zielX = cursorX == 0 ? 3 : cursorX == 1 ? 11 : 18;
            int zielY = cursorY == 0 ? 0 : cursorY == 1 ? 2 : 4;

            Console.SetCursorPosition(zielX, zielY);
        }

        private bool VersucheZuSetzen()
        {
            if (FeldIstLeer())
            {
                Spielfeld.First(x => x.xCoordinate == cursorX && x.yCoordinate == cursorY).Typ
                    = Spieler.First(x => x.MeinZug).Zeichen.ToLower()
                    == "x" ? FeldTyp.X : FeldTyp.O;

                return true;
            }
            return false;
        }

        private bool FeldIstLeer()
        {
            return Spielfeld.First(x => x.xCoordinate == cursorX && x.yCoordinate == cursorY).Typ == FeldTyp.Leer;
        }

        private bool SpielZuEnde()
        {
            if (SiegbedingungenErfuellt())
                GewinnerFestgestellt();
            else if (LetztesFeld())
                UnentschiedenFestgestellt();
            else return false;

            return true;
        }

        private bool SiegbedingungenErfuellt()
        {
            return DreiInEinerSpalte() || DreiInEinerReihe() || DreiInEinerDiagonale();
        }

        private bool DreiInEinerSpalte()
        {
            for (int i = 0; i < 3; i++)
            {
                List<Feld> felder = Spielfeld.Where(x => x.xCoordinate == i).ToList();
                if (!felder.Any(x => x.Typ == FeldTyp.Leer))
                {
                    if (felder[0].Typ == felder[1].Typ && felder[0].Typ == felder[2].Typ)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DreiInEinerReihe()
        {
            for (int i = 0; i < 3; i++)
            {
                List<Feld> felder = Spielfeld.Where(x => x.yCoordinate == i).ToList();
                if (!felder.Any(x => x.Typ == FeldTyp.Leer))
                {
                    if (felder[0].Typ == felder[1].Typ && felder[0].Typ == felder[2].Typ)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DreiInEinerDiagonale()
        {
            if
            (
                Spielfeld.First(x => x.xCoordinate == 0 && x.yCoordinate == 0).Typ != FeldTyp.Leer &&
                Spielfeld.First(x => x.xCoordinate == 0 && x.yCoordinate == 0).Typ == Spielfeld.First(x => x.xCoordinate == 1 && x.yCoordinate == 1).Typ &&
                Spielfeld.First(x => x.xCoordinate == 0 && x.yCoordinate == 0).Typ == Spielfeld.First(x => x.xCoordinate == 2 && x.yCoordinate == 2).Typ
            )
                return true;
            else if
            (
                Spielfeld.First(x => x.xCoordinate == 2 && x.yCoordinate == 0).Typ != FeldTyp.Leer &&
                Spielfeld.First(x => x.xCoordinate == 2 && x.yCoordinate == 0).Typ == Spielfeld.First(x => x.xCoordinate == 1 && x.yCoordinate == 1).Typ &&
                Spielfeld.First(x => x.xCoordinate == 2 && x.yCoordinate == 0).Typ == Spielfeld.First(x => x.xCoordinate == 0 && x.yCoordinate == 2).Typ
            )
                return true;

            return false;
        }

        private bool LetztesFeld()
        {
            if (Spielfeld.Any(x => x.Typ == FeldTyp.Leer))
                return false;
            return true;
        }

        private void GewinnerFestgestellt()
        {
            Console.WriteLine("Herzlichen Glückwunsch, Spieler " + Spieler.First(x => x.MeinZug).Zeichen.ToString());
        }

        private void UnentschiedenFestgestellt()
        {
            Console.WriteLine("Unentschieden!!!");
        }

        private bool NeustartInitiieren()
        {
            Console.WriteLine("Neues Spiel starten? J/N");

            bool gueltigeEingabe = false;
            do
            {
                ConsoleKeyInfo eingabe = Console.ReadKey();
                switch (eingabe.Key)
                {
                    case ConsoleKey.J: gueltigeEingabe = true; return true;
                    case ConsoleKey.N: gueltigeEingabe = true; return false;
                    default: continue;
                };
            }
            while (!gueltigeEingabe);

            return false;
        }
    }
}
