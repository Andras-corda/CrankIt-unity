using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;
    public MachineType selectedMachine = MachineType.Extractor;

    Camera cam;

    private void Awake()
    {
        Instance = this;
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;
            Vector2Int cell = GridManager.Instance.WorldToGrid(worldPos);
            GridManager.Instance.PlaceMachine(cell.x, cell.y, selectedMachine);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;
            Vector2Int cell = GridManager.Instance.WorldToGrid(worldPos);
            GridManager.Instance.RemoveMachine(cell.x, cell.y);
        }
    }

    public void SelectMachine(int type)
    {
        selectedMachine = (MachineType)type;
    }
}
