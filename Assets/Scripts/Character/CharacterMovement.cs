using Game.GameInput;
using Game.GameSpace;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Character
{
    public class CharacterMovement : MonoBehaviour, ICellOwner
    {
        public event Action OnTargetCellReached;

		[SerializeField] private float _speed;

		private IInputHandler _input;
		private Cell _targetCell;

		public (int top, int bot, int left, int right) Borders { get; set; } = (100, 0, 100, 0);
        public List<Cell> CapturedCells { get; set; } = new List<Cell>();
        public TrailGenerator Generator { get; private set; }

        public Vector2Int Position => _targetCell.FieldPosition;
		public bool OnOwnTerritory { get; set; }

        [Inject]
        private void Construct(IInputHandler input, TrailGenerator generator)
        {
			CapturedCells = new List<Cell>();
			_targetCell = null;
			OnOwnTerritory = true;

            _input = input;
            Generator = generator;
		}

        public void Initialize(Cell startCell)
        {
            transform.position = startCell.Position;

			startCell.TakeCell(this);

			_targetCell = startCell.NextCellInDirection(_input.GetDirection());

		}

        private void Update()
        {
            if (_targetCell == null)
                return;

            if (transform.position == _targetCell.Position)
            {
				_targetCell.HandleCellState(this);
				_targetCell = _targetCell.NextCellInDirection(_input.GetDirection());
            }

			MoveToTarget();
            OnTargetCellReached?.Invoke();
		}

        public void Dispose()
        {
            Destroy(Generator.gameObject);
            Destroy(gameObject);
        }

        private void MoveToTarget()
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _targetCell.Position, _speed * Time.deltaTime);
        }
    }
}
