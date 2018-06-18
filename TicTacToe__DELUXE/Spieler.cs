using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe__DELUXE
{
    public class Spieler
    {
        private string m_Zeichen;
        public string Zeichen { get { return m_Zeichen; } set { m_Zeichen = value; } }

        private bool m_MeinZug;
        public bool MeinZug { get { return m_MeinZug; } set { m_MeinZug = value; } }

        public Spieler(string _zeichen, bool _meinZug = false)
        {
            m_Zeichen = _zeichen;
            m_MeinZug = _meinZug;
        }
    }
}
