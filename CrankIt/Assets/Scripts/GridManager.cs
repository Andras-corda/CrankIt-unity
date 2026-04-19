using UnityEngine;

public enum MachineType { Empty, Extractor, Belt, Factory, Output }

public class GridManager : MonoBehaviour
{
   public static GridManager Instance;

    [Header("Grid")]
    public int width = 8;
    public int height = 6;
    public float cellSize = 1f;

    public MachineType[,] grid;
    public GameObject[,] machineObj;

    [Header("Prefabs")]
    public GameObject extractorPrefab;
    public GameObject beltPrefab;
    public GameObject factoryPrefab;
    public GameObject outputPrefab;

    private void Awake() //Set Grid, Instance and MachinesObj by default at first
    {
        Instance = this;
        grid = new MachineType[width, height];
        machineObj = new GameObject[width, height];
    }

    public void PlaceMachine(int x, int y, MachineType type)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return;
        if (grid[x, y] != MachineType.Empty) return;

        grid[x, y] = type;

        GameObject prefab = type switch
        {
            MachineType.Extractor => extractorPrefab,
            MachineType.Belt => beltPrefab,
            MachineType.Factory => factoryPrefab,
            MachineType.Output => outputPrefab,
            _ => null
        };

        if (prefab != null)
        {
            Vector3 pos = GridToWorld(x, y);
            machineObj[x, y] = Instantiate(prefab, pos, Quaternion.identity);
        }
    }

    public void RemoveMachine(int x, int y)
    {
        if (grid[x, y] == MachineType.Empty) return;
        grid[x, y] = MachineType.Empty;
        if (machineObj[x, y] != null)
            Destroy(machineObj[x, y]);
    }

    public Vector3 GridToWorld(int x, int y)
    {
        return new Vector3(x * cellSize, y * cellSize, 0f);
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / cellSize);
        int y = Mathf.FloorToInt(worldPos.y / cellSize);
        return new Vector2Int(x, y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        for (int x = 0; x <= width; x++)
            Gizmos.DrawLine(new Vector3(x * cellSize, 0, 0), new Vector3(x * cellSize, height * cellSize, 0));
        for (int y = 0; y <= height; y++)
            Gizmos.DrawLine(new Vector3(0, y * cellSize, 0), new Vector3(width * cellSize, y * cellSize, 0));
    }
}
