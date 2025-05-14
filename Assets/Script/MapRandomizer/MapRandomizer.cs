using System.Linq;
using UnityEngine;

public class MapRandomizer : MonoBehaviour
{
    [SerializeField] protected GameObject[] wallPrefabs;
    [SerializeField] protected Transform mapArea;
    [SerializeField] protected int numberOfWalls = 10;
    [SerializeField] protected Vector2 mapSize = new Vector2(25, 20);

    private GameObject[] currentWalls;

    public void RandomizeMap()
    {
        if (currentWalls != null)
        {
            foreach (var wall in currentWalls)
                Destroy(wall);
        }

        currentWalls = new GameObject[numberOfWalls];

        for (int i = 0; i < numberOfWalls; i++)
        {
            GameObject prefab = wallPrefabs[Random.Range(0, wallPrefabs.Length)];
            Vector3 position = new Vector3(
                Random.Range(-mapSize.x / 2, mapSize.x / 2),
                0,
                Random.Range(-mapSize.y / 2, mapSize.y / 2)
            );

            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90f, 0);

            GameObject wall = Instantiate(prefab, position, rotation, mapArea);
            currentWalls[i] = wall;
        }
    }
}
