using Game.Character;
using Game.GameInput;
using Game.GameSpace;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure
{
	public class EnvironmentInstaller : MonoInstaller
	{
		[SerializeField] private Transform _environment;
		[SerializeField] private CharacterMovement _playerPrefab;
		[SerializeField] private Gamefield _gamefield;
		[SerializeField] private PlayerInputHandler _playerInputHandler;
		[SerializeField] private TrailGenerator _generator;
		[SerializeField] private Follower _playerFollower;

		public override void InstallBindings()
		{
			BindInputService();
			BindTrailGenerator();
			BindPlayer();
			BindAreaFillerService();
			BindPlayerFollowerService();
			BindGamefield();
		}

		private void BindAreaFillerService()
		{
			Container
				.Bind<AreaFiller>()
				.AsSingle();
		}

		private void BindPlayerFollowerService()
		{
			Container
				.Bind<Follower>()
				.FromInstance(_playerFollower)
				.AsSingle();
		}

		private void BindGamefield()
		{
			Container
				.Bind<Gamefield>()
				.FromInstance(_gamefield)
				.AsSingle();
		}

		private void BindPlayer()
		{
			var player = Container
				.InstantiatePrefabForComponent<CharacterMovement>(_playerPrefab, _environment);

			Container
				.Bind<CharacterMovement>()
				.FromInstance(player)
				.AsSingle();
		}

		private void BindTrailGenerator()
		{
			Container
				.Bind<TrailGenerator>()
				.FromInstance(_generator)
				.AsSingle();
		}

		private void BindInputService()
		{
			Container
				.Bind<IInputHandler>()
				.To<PlayerInputHandler>()
				.FromInstance(_playerInputHandler)
				.AsSingle();
		}
	}
}
