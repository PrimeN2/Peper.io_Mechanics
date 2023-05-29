using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.GameSpace
{
	public class TrailGenerator : MonoBehaviour
	{
		[SerializeField] private MeshGenerator _contestedBlocksGenerator;
		[SerializeField] private MeshGenerator _busyBlocksGenerator;

		[SerializeField] private Vector3 _characterOffset;

		private List<Vector3> _busyBlocksPositions = new List<Vector3>();
		private List<Vector3> _contestedBlocksPositions = new List<Vector3>();

		[Inject]
		private void Construct()
		{
			_contestedBlocksGenerator.Init();
			_busyBlocksGenerator.Init();

			_busyBlocksPositions = new List<Vector3>();
			_contestedBlocksPositions = new List<Vector3>();
		}

		public void DeleteBusyBlock(Vector3 position)
		{
			_busyBlocksPositions.Remove(position - _characterOffset);
			_busyBlocksGenerator.RecreateBlocks(_busyBlocksPositions.ToArray());
		}

		public void DeleteContestedBlock(Vector3 position)
		{
			_contestedBlocksPositions.Remove(position - _characterOffset);	
			_contestedBlocksGenerator.RecreateBlocks(_contestedBlocksPositions.ToArray());
		}

		public void GenerateBusyBlock(Vector3 position)
		{
			_busyBlocksGenerator.GenerateBlock(position - _characterOffset);
			_busyBlocksPositions.Add(position - _characterOffset);
		}

		public void GenerateContestedBlock(Vector3 position)
		{
			_contestedBlocksGenerator.GenerateBlock(position - _characterOffset);
			_contestedBlocksPositions.Add(position - _characterOffset);
		}
	}
}

