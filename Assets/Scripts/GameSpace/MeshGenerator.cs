using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSpace
{
    [RequireComponent(typeof(MeshFilter))]
    public class MeshGenerator : MonoBehaviour
    {
        [SerializeField] private MeshFilter _filter;

		private List<Vector3> _vertices = new List<Vector3>();
        private List<int> _triangles = new List<int>();

		public void Init()
		{
			ResetBlocks();
			_filter = GetComponent<MeshFilter>();
		}

		public void RecreateBlocks(Vector3[] blocks)
        {
			ResetBlocks();

			foreach (var blockPosition in blocks) 
                GenerateBlock(blockPosition);
		}

		public void GenerateBlock(Vector3 position)
        {
            //GenerateRightSide(position);
            //GenerateLeftSide(position);
            //GenerateBackSide(position);
            //GenerateFrontSide(position);
            GenerateTopSide(position);

			ConstructMesh();
		}

		public void GenerateCustomBlock(Vector3 position)
		{
			//GenerateRightSide(position);
			//GenerateLeftSide(position);
			//GenerateBackSide(position);
			//GenerateFrontSide(position);
			GenerateTopSide(position);

			ConstructMesh();
		}

		private void ResetBlocks()
		{
			_vertices.Clear();
			_triangles.Clear();
		}

		private void ConstructMesh()
        {
            var mesh = new Mesh
            {
                vertices = _vertices.ToArray(),
                triangles = _triangles.ToArray()
            };

            mesh.RecalculateNormals();

			_filter.mesh = mesh;
        }

        private void GenerateRightSide(Vector3 position)
        {
            _vertices.Add(new Vector3(0, 0, 0) + position);
            _vertices.Add(new Vector3(0, 0, 1) + position);
            _vertices.Add(new Vector3(0, 0.1f, 0) + position);
            _vertices.Add(new Vector3(0, 0.1f, 1) + position);

            FillTriangles();
        }
        private void GenerateLeftSide(Vector3 position)
        {
            _vertices.Add(new Vector3(1, 0, 0) + position);
            _vertices.Add(new Vector3(1, 0.1f, 0) + position);
            _vertices.Add(new Vector3(1, 0, 1) + position);
            _vertices.Add(new Vector3(1, 0.1f, 1) + position);

            FillTriangles();
        }

        private void GenerateBackSide(Vector3 position)
        {
            _vertices.Add(new Vector3(0, 0, 0) + position);
            _vertices.Add(new Vector3(0, 0.1f, 0) + position);
            _vertices.Add(new Vector3(1, 0, 0) + position);
            _vertices.Add(new Vector3(1, 0.1f, 0) + position);

            FillTriangles();
        }

        private void GenerateFrontSide(Vector3 position)
        {
            _vertices.Add(new Vector3(0, 0, 1) + position);
            _vertices.Add(new Vector3(1, 0, 1) + position);
            _vertices.Add(new Vector3(0, 0.1f, 1) + position);
            _vertices.Add(new Vector3(1, 0.1f, 1) + position);

            FillTriangles();
        }

        private void GenerateTopSide(Vector3 position)
        {
            _vertices.Add(new Vector3(0, 0.1f, 0) + position);
            _vertices.Add(new Vector3(0, 0.1f, 1) + position);
            _vertices.Add(new Vector3(1, 0.1f, 0) + position);
            _vertices.Add(new Vector3(1, 0.1f, 1) + position);

            FillTriangles();
        }

        private void FillTriangles()
        {
            _triangles.Add(_vertices.Count - 4);
            _triangles.Add(_vertices.Count - 3);
            _triangles.Add(_vertices.Count - 2);

            _triangles.Add(_vertices.Count - 3);
            _triangles.Add(_vertices.Count - 1);
            _triangles.Add(_vertices.Count - 2);
        }
	}
}
