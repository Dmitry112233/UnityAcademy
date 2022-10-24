using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class MeshGeneratorUtil : MonoBehaviour
    {
        public GameObject emptyPlatformPrefab;

        public GameObject GeneratePlatform(Vector3 position, float sizeX, float sizeZ, float gap, Func<float, float, float, Vector3[]> generateVertices, bool isMain)
        {
            GameObject mp = Instantiate(emptyPlatformPrefab, position, Quaternion.identity);

            var mpMeshFilter = mp.GetComponent<MeshFilter>();
            mpMeshFilter.mesh = new Mesh();
            mpMeshFilter.mesh.vertices = generateVertices(gap, sizeX, sizeZ);
            mpMeshFilter.mesh.triangles = GenerateTriangles();
            mpMeshFilter.mesh.RecalculateNormals();
            mp.GetComponent<MeshCollider>().sharedMesh = mpMeshFilter.mesh;
            if (isMain)
            {
                mp.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                mp.GetComponent<Rigidbody>().useGravity = true;
            }
            return mp;
        }

        public GameObject GenerateInitPlatform(Vector3 initZTransformPosition)
        {
            GameObject mp = Instantiate(emptyPlatformPrefab, initZTransformPosition, Quaternion.identity);
            var mpMeshFilter = mp.GetComponent<MeshFilter>();
            mpMeshFilter.mesh = new Mesh();
            mpMeshFilter.mesh.vertices = GenerateVerticesInit();
            mpMeshFilter.mesh.triangles = GenerateTriangles();
            mpMeshFilter.mesh.RecalculateNormals();
            mp.GetComponent<MeshCollider>().sharedMesh = mpMeshFilter.mesh;

            return mp;
        }

        public Vector3[] GenerateVerticesPositiveGapZ(float gap, float sizeX, float sizeZ)
        {
            return new Vector3[]
            {
            new Vector3(sizeX / 2f, 0.05f, (sizeZ - gap)/2f),
            new Vector3(sizeX / 2f, 0.05f, -1 * (sizeZ - gap)/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, (sizeZ - gap)/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, -1 * (sizeZ - gap)/2f),

            new Vector3(sizeX / 2f, -0.05f, -1f * (sizeZ - gap)/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, -1f * (sizeZ - gap)/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, (sizeZ - gap)/2f),
            new Vector3(sizeX / 2f, -0.05f, (sizeZ - gap)/2f),
            };
        }

        public Vector3[] GenerateVerticesPositiveGapX(float gap, float sizeX, float sizeZ)
        {
            return new Vector3[]
            {
            new Vector3((sizeX - gap)/2f, 0.05f, sizeZ / 2f),
            new Vector3((sizeX - gap)/2f, 0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * (sizeX - gap)/2f, 0.05f, sizeZ / 2f),
            new Vector3(-1f *(sizeX - gap)/2f, 0.05f, -1f * sizeZ / 2f),

            new Vector3((sizeX - gap)/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * (sizeX - gap)/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * (sizeX - gap)/2f, -0.05f, sizeZ / 2f),
            new Vector3((sizeX - gap)/2f, -0.05f, sizeZ / 2f),
            };
        }

        public Vector3[] GenerateVerticesNegativeGapZ(float gap, float sizeX, float sizeZ)
        {
            return new Vector3[]
            {
            new Vector3(sizeX / 2f, 0.05f, (sizeZ + gap)/2f),
            new Vector3(sizeX / 2f, 0.05f, -1f * (sizeZ + gap)/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, (sizeZ + gap)/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, -1f * (sizeZ + gap)/2f),

            new Vector3(sizeX / 2f, -0.05f, -1f * (sizeZ + gap)/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, -1f * (sizeZ + gap)/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, (sizeZ + gap)/2f),
            new Vector3(sizeX / 2f, -0.05f, (sizeZ + gap)/2f),
            };
        }

        public Vector3[] GenerateVerticesNegativeGapX(float gap, float sizeX, float sizeZ)
        {
            return new Vector3[]
            {
             new Vector3((sizeX + gap)/2f, 0.05f, sizeZ / 2f),
            new Vector3((sizeX + gap)/2f, 0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * (sizeX + gap)/2f, 0.05f, sizeZ / 2f),
            new Vector3(-1f * (sizeX + gap)/2f, 0.05f, -1f * sizeZ / 2f),

            new Vector3((sizeX + gap)/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * (sizeX + gap)/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * (sizeX + gap)/2f, -0.05f, sizeZ / 2f),
            new Vector3((sizeX + gap)/2f, -0.05f, sizeZ / 2f),
            };
        }

        public Vector3[] GenerateVerticesPartialZ(float gap, float sizeX, float sizeZ)
        {
            return new Vector3[]
            {
            new Vector3(sizeX / 2f, 0.05f, gap/2f),
            new Vector3(sizeX / 2f, 0.05f, -1f * gap/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, gap/2f),
            new Vector3(-1f * sizeX / 2f, 0.05f, -1f * gap/2f),

            new Vector3(sizeX / 2f, -0.05f, -1f * gap/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, -1f * gap/2f),
            new Vector3(-1f * sizeX / 2f, -0.05f, gap/2f),
            new Vector3(sizeX / 2f, -0.05f, gap/2f),
            };
        }

        public Vector3[] GenerateVerticesPartialX(float gap, float sizeZ, float sizeX)
        {
            return new Vector3[]
            {
            new Vector3(gap/2f, 0.05f, sizeZ / 2f),
            new Vector3(gap/2f, 0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * gap/2f, 0.05f, sizeZ / 2f),
            new Vector3(-1f * gap/2f, 0.05f, -1f * sizeZ / 2f),

            new Vector3(gap/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * gap/2f, -0.05f, -1f * sizeZ / 2f),
            new Vector3(-1f * gap/2f, -0.05f, sizeZ / 2f),
            new Vector3(gap/2f, -0.05f, sizeZ / 2f),
            };
        }

        public Vector3[] GenerateVerticesInit()
        {
            return new Vector3[]
            {
            new Vector3(0.5f, 0.05f, 0.5f),
            new Vector3(0.5f, 0.05f, -0.5f),
            new Vector3(-0.5f, 0.05f, 0.5f),
            new Vector3(-0.5f, 0.05f, -0.5f),

            new Vector3(0.5f, -0.05f, -0.5f),
            new Vector3(-0.5f, -0.05f, -0.5f),
            new Vector3(-0.5f, -0.05f, 0.5f),
            new Vector3(0.5f, -0.05f, 0.5f),
            };
        }

        public int[] GenerateTriangles()
        {
            return new int[] { 0, 1, 3, 0, 3, 2, 3, 1, 4, 3, 4, 5, 3, 5, 6, 6, 2, 3, 5, 4, 7, 6, 5, 7, 0, 2, 7, 2, 6, 7, 0, 4, 1, 0, 7, 4 };
        }
    }
}
