using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.GameSpace
{
    public class Gamefield : MonoBehaviour
    {
        public Dictionary<Vector2Int, Cell> Cells { get; private set; }

		[SerializeField] private int _width;
		[SerializeField] private int _height;

		private AreaFiller _areaFiller;

		[Inject]
		private void Construct(AreaFiller areaFiller)
		{
			Cells = new Dictionary<Vector2Int, Cell>();

			_areaFiller = areaFiller;
		}

		public void Initialize()
		{
			gameObject.transform.localScale = new Vector3(_height / 10, 1, _width / 10);

			for (int x = 0, i = 0; x < _height; x++)
			{
				for (int y = 0; y < _width; y++, i++)
				{
					Vector2Int currentCell = new Vector2Int(x, y);

					Cells.Add(currentCell,
						new Cell(null, CellState.Empty, currentCell, Vector3.one * 0.5f, this, _areaFiller));
				}
			}
		}

		public Cell GetStartCell() => Cells[CalculatePosition()];

		private Vector2Int CalculatePosition()
		{
			Vector2Int position = new Vector2Int(Random.Range(2, 98), Random.Range(2, 98));

			while (Cells[position].Owner != null)
			{
				if (position.x < 50 && position.y < 50)
					position += Vector2Int.one * 2;
				else if (position.x > 50 && position.y < 50)
					position += new Vector2Int(-2, 2);
				else if (position.x < 50 && position.y > 50)
					position += new Vector2Int(2, -2);
				else if (position.x > 50 && position.y > 50)
					position -= new Vector2Int(2, 2);
			}

			return position;
		}
    }
}
