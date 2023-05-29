using UnityEngine;

namespace Game.GameSpace
{
	public class AreaFiller
	{
		private Gamefield _gamefield;

		public AreaFiller(Gamefield gamefield)
		{
			_gamefield = gamefield;
		}

		public void FillAreaIn(
			(int top, int bot, int left, int right) borders, ICellOwner owner)
		{
			for (int row = borders.top; row <= borders.bot; row++)
			{
				int bordersOnRight = CountBordersOnRight(owner, 0, borders.right, row);
				bool hasBorderOnLeft = false;

				for (int column = borders.left; column <= borders.right; column++)
				{
					var currentCell = _gamefield.Cells[new Vector2Int(row, column)];

					if (currentCell.Owner == owner)
					{
						bordersOnRight--;

						if (currentCell.State == CellState.Contested)
							currentCell.TakeCell(owner);

						continue;
					}

					if (hasBorderOnLeft == false)
						hasBorderOnLeft = HasBorderOnLeft(owner, column, borders.left, row);
					if (hasBorderOnLeft && bordersOnRight > 0)
					{
						if (HasBroderOnTop(owner, row, borders.top, column) &&
						HasBorderOnBot(owner, row, borders.bot, column))
						{
							currentCell.TakeCell(owner);
						}
					}
				}
			}
		}

		private bool HasBroderOnTop(ICellOwner owner, int start, int last, int column)
		{
			for (int row = start; row >= last - 1; row--)
			{
				var currentCell = _gamefield.Cells[new Vector2Int(row, column)];

				if (currentCell.Owner == owner)
					return true;
			}

			return false;
		}

		private bool HasBorderOnBot(ICellOwner owner, int start, int last, int column)
		{
			for (int row = start; row <= last + 1; row++)
			{
				var currentCell = _gamefield.Cells[new Vector2Int(row, column)];

				if (currentCell.Owner == owner)
					return true;
			}
			return false;
		}

		private int CountBordersOnRight(ICellOwner owner, int start, int last, int row)
		{
			int amount = 0;

			for (int column = start; column <= last + 1; column++)
			{
				var currentCell = _gamefield.Cells[new Vector2Int(row, column)];

				if (currentCell.Owner == owner)
					amount++;
			}
			return amount;
		}

		private bool HasBorderOnLeft(ICellOwner owner, int start, int last, int row)
		{
			for (int column = start; column >= last - 1; column--)
			{
				var currentCell = _gamefield.Cells[new Vector2Int(row, column)];

				if (currentCell.Owner == owner)
					return true;
			}

			return false;
		}
	}
}