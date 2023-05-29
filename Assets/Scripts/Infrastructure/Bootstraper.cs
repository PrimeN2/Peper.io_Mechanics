using Game.Character;
using Game.GameSpace;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure
{
	public class Bootstraper : MonoBehaviour
	{
		private Gamefield _gamefield;
		private CharacterMovement _player;
		private Follower _playerFollower;

		[Inject]
		private void Construct(Gamefield gamefield, CharacterMovement player, Follower playerFollower)
		{
			_gamefield = gamefield;
			_player = player;
			_playerFollower = playerFollower;
		}

		private void Start()
		{
			InitEnvironment();
		}

		private void InitEnvironment()
		{
			_gamefield.Initialize();
			_player.Initialize(_gamefield.GetStartCell());
			_playerFollower.SetTarget(_player.transform);
		}
	}
}
