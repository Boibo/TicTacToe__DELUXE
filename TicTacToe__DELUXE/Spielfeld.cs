using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe__DELUXE
{
    public class Spielfeld : List<Feld>
    {
        public Spielfeld()
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    Add(new Feld(j, i));
                }
            }
        }

        new private void Add(Feld _item)
        {
            base.Add(_item);
        }
    }
}
