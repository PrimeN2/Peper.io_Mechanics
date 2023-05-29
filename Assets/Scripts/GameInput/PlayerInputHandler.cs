using Game.GameSpace;
using UnityEngine;

namespace Game.GameInput
{
    class PlayerInputHandler : MonoBehaviour, IInputHandler
    {
        private Direction _currentDirection;
        private Direction _appliedDirection;

		public Direction GetDirection()
        {
			_appliedDirection = _currentDirection;

			return _currentDirection;
        }


		private void Awake()
        {
            _currentDirection = Direction.Up;
            _appliedDirection = _currentDirection;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S) && !(_appliedDirection == Direction.Up))
                ChangeDirection(Direction.Down);
            else if (Input.GetKeyDown(KeyCode.A) && !(_appliedDirection == Direction.Right))
                ChangeDirection(Direction.Left);
            else if (Input.GetKeyDown(KeyCode.W) && !(_appliedDirection == Direction.Down))
                ChangeDirection(Direction.Up);
            else if (Input.GetKeyDown(KeyCode.D) && !(_appliedDirection == Direction.Left))
                ChangeDirection(Direction.Right);
        }

        private void ChangeDirection(Direction direction)
        {
            _currentDirection = direction;
        }
    }
}
