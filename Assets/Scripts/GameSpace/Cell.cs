using UnityEditor;
using UnityEngine;

namespace Game.GameSpace
{
    public class Cell
    {
        public CellState State { get; private set; }
        public ICellOwner Owner { get; private set; }
        public Vector3 Position { get => new Vector3(_position.x, 0, _position.y) + _offset; }
        public Vector2Int FieldPosition { get => _position; }

		private AreaFiller _areaFiller;

		private Vector2Int _position;
        private Vector3 _offset;

        private readonly Gamefield _field;

        public Cell(ICellOwner owner, CellState state, Vector2Int position, 
			Vector3 offset, Gamefield field, AreaFiller areaFiller)
        {
            Owner = owner;
            State = state;
            _position = position;
            _field = field;
            _offset = offset;
			_areaFiller = areaFiller;
		}

        public void HandleCellState(ICellOwner owner)
        {
			SetBordersFor(owner);

			bool isThisOwner = Owner == owner;

			switch (State)
			{
				case CellState.Empty:
					ContestCell(owner);
					owner.OnOwnTerritory = false;
					break;

				case CellState.Contested:
					if (isThisOwner)
					{
						owner.Dispose();
					}
					else
					{
						ContestCell(owner);
						owner.OnOwnTerritory = false;
					}
					break;

				case CellState.Busy:
					if (isThisOwner)
					{
						if (!owner.OnOwnTerritory)
							_areaFiller.FillAreaIn(owner.Borders, owner);

						owner.OnOwnTerritory = true;
					}
					else
					{
						ContestCell(owner);
						owner.OnOwnTerritory = false;
					}
					break;
			}
		}

        public void TakeCell(ICellOwner owner)
        {
			Owner?.Generator.DeleteContestedBlock(Position);

            Owner = owner;
            State = CellState.Busy;

			Owner.Generator.GenerateBusyBlock(Position);
			Owner.CapturedCells.Add(this);
        }

		public void ContestCell(ICellOwner owner)
        {
			Owner?.Generator.DeleteBusyBlock(Position);

			Owner = owner;
            State = CellState.Contested;

			owner.Generator.GenerateContestedBlock(Position);
			Owner.CapturedCells.Add(this);
		}

        public Cell NextCellInDirection(Direction direction)
        { 
            if (direction == Direction.Down)
				return GetCellBy(_position + new Vector2Int(1, 0));
            else if (direction == Direction.Left)
				return GetCellBy(_position - new Vector2Int(0, 1));
            else if (direction == Direction.Up)
				return GetCellBy(_position - new Vector2Int(1, 0));
            else if (direction == Direction.Right)
                return GetCellBy(_position + new Vector2Int(0, 1));

			return this;
        }

        private Cell GetCellBy(Vector2Int position)
        {
            Cell cell;

            if (_field.Cells.TryGetValue(position, out cell))
			    return cell;
            return this;
		}

		private void SetBordersFor(ICellOwner owner)
		{
            var borders = owner.Borders;

			owner.Borders = (Mathf.Min(FieldPosition.x, borders.top),
				Mathf.Max(FieldPosition.x, borders.bot),
				Mathf.Min(FieldPosition.y, borders.left),
				Mathf.Max(FieldPosition.y, borders.right));
		}
	}

    public enum CellState
    {
        Empty = 0,
        Contested = 1,
        Busy = 2,
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
