using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe__DELUXE
{
    public enum FeldTyp
    {
        Leer = 0,
        X = 1,
        O = 2
    }

    public class Feld
    {
        private int m_xCoordinate;
        public int xCoordinate { get { return m_xCoordinate; } set { m_xCoordinate = value; } }

        private int m_yCoordinate;
        public int yCoordinate { get { return m_yCoordinate; } set { m_yCoordinate = value; } }

        private FeldTyp m_Typ;
        public FeldTyp Typ { get { return m_Typ; } set { m_Typ = value; } }

        public Feld(int _x, int _y)
        {
            m_xCoordinate = _x;
            m_yCoordinate = _y;
            Typ = FeldTyp.Leer;
        }
    }
}
