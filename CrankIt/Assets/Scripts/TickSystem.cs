using UnityEngine;
using System.Collections.Generic;

public class TickSystem : MonoBehaviour
{
    public float tickRate = 1f;
    public GameObject resourcePrefab;
    public Transform resourceParent;

    List<Resource> resources = new List<Resource>();

    void Start()
    {
        InvokeRepeating(nameof(Tick), tickRate, tickRate);
    }

    void Tick()
    {
        var grid = GridManager.Instance.grid;
        int w = GridManager.Instance.width;
        int h = GridManager.Instance.height;

        // Extractors spawn raw resources
        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
            {
                if (grid[x, y] == MachineType.Extractor)
                    SpawnResource(x, y);
            }

        // Move resources on belts (right → priorité simple)
        var toMove = new List<Resource>(resources);
        foreach (var r in toMove)
        {
            int nx = r.gridX + 1;
            int ny = r.gridY;
            if (nx >= w) continue;

            MachineType next = grid[nx, ny];

            if (next == MachineType.Belt)
            {
                r.MoveTo(nx, ny);
            }
            else if (next == MachineType.Factory && r.type == ResourceType.Raw)
            {
                r.SetProcessed();
                r.MoveTo(nx, ny);
            }
            else if (next == MachineType.Output)
            {
                resources.Remove(r);
                Destroy(r.gameObject);
                GameManager.Instance.AddScore();
            }
        }

        // Nettoyage ressources orphelines sur extracteur
        resources.RemoveAll(r => r == null);
    }

    void SpawnResource(int x, int y)
    {
        // Max 1 ressource par case
        if (resources.Exists(r => r.gridX == x && r.gridY == y)) return;

        GameObject go = Instantiate(resourcePrefab, resourceParent);
        Resource res = go.GetComponent<Resource>();
        res.Init(x, y, ResourceType.Raw);
        resources.Add(res);
    }
}
