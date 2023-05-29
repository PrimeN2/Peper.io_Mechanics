using System.Collections.Generic;

namespace Game.GameSpace
{
    public interface ICellOwner
    {
        List<Cell> CapturedCells { get; set; }

        TrailGenerator Generator { get; }

        (int top, int bot, int left, int right) Borders { get; set; }

        bool OnOwnTerritory { get; set; }

        void Dispose();
	}
}
