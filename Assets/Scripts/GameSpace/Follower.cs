using UnityEngine;

namespace Game
{
	public class Follower : MonoBehaviour
	{
		[SerializeField] private Vector3 _offset;

		private Transform _target;

		public void SetTarget(Transform target)
		{
			_target = target;
			transform.position = new Vector3(_target.position.x, 0, target.position.z) + _offset;
		}

		private void LateUpdate()
		{
			if (_target == null)
				return;

			transform.position = _target.position + _offset;
		}
	}

}
