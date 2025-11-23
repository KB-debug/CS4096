using UnityEngine;
using System.Collections.Generic;

public class OfficeGenerator : MonoBehaviour
{
    [Header("Room Generation")]
    public GameObject[] roomPrefabs;
    public int gridWidth = 6;
    public int gridHeight = 2;
    public float spacingBuffer = 1f;
    public int seed = 12345;

    [Header("Corridor Settings")]
    public GameObject corridorPrefab; // optional
    public float doorConnectDistance = 1.5f; // max distance to count as connected
    public bool disableDoorColliders = true;

    private GameObject[,] roomGrid;

    void Start()
    {
        Random.InitState(seed);
        GenerateRooms();
        ConnectDoors();
    }

    void GenerateRooms()
    {
        roomGrid = new GameObject[gridWidth, gridHeight];
        float currentZ = 0f;

        for (int z = 0; z < gridHeight; z++)
        {
            float currentX = 0f;
            float maxRowDepth = 0f;

            for (int x = 0; x < gridWidth; x++)
            {
                GameObject roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
                Vector2 roomSize = GetRoomSize(roomPrefab);

                Vector3 pos = new Vector3(currentX + roomSize.x / 2, 0, currentZ + roomSize.y / 2);
                GameObject room = Instantiate(roomPrefab, pos, Quaternion.identity, transform);
                room.name = $"Room_{x}_{z}";

                roomGrid[x, z] = room;

                currentX += roomSize.x + spacingBuffer;
                if (roomSize.y > maxRowDepth)
                    maxRowDepth = roomSize.y;
            }

            currentZ += maxRowDepth + spacingBuffer;
        }
    }

    void ConnectDoors()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                GameObject room = roomGrid[x, z];
                if (!room) continue;

                // East neighbor
                if (x < gridWidth - 1 && roomGrid[x + 1, z])
                {
                    TryConnect(room, roomGrid[x + 1, z], "Door_East", "Door_West");
                }

                // North neighbor
                if (z < gridHeight - 1 && roomGrid[x, z + 1])
                {
                    TryConnect(room, roomGrid[x, z + 1], "Door_North", "Door_South");
                }
            }
        }
    }

    void TryConnect(GameObject roomA, GameObject roomB, string doorAName, string doorBName)
    {
        Transform doorA = roomA.transform.Find(doorAName);
        Transform doorB = roomB.transform.Find(doorBName);

        

        float dist = Vector3.Distance(doorA.position, doorB.position);

        // only connect if doors are close enough
        if (dist <= doorConnectDistance)
        {
           

            if (corridorPrefab)
            {
                Vector3 midpoint = (doorA.position + doorB.position) / 2f;
                Quaternion rotation = Quaternion.LookRotation(doorB.position - doorA.position);
                Instantiate(corridorPrefab, midpoint, rotation, transform);
            }

            
        }
        
    }

    Vector2 GetRoomSize(GameObject prefab)
    {
        Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return Vector2.one;

        Bounds bounds = new Bounds(renderers[0].bounds.center, Vector3.zero);
        foreach (Renderer r in renderers)
            bounds.Encapsulate(r.bounds);

        return new Vector2(bounds.size.x, bounds.size.z);
    }
}
