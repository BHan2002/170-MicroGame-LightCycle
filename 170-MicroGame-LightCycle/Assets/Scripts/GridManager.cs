using UnityEngine;

// Script to manage the grid and it's cells, including creating the grid and handling cell interactions
public class Grid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // TODO: 2D array of cells {'safe', 'warning','falling', 'gone'} and instantiate the cell prefabs based on the state of each cell
    // configurable grid size 20x20, cell size 1 unit.
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;
    [SerializeField] private float cellSize = 1f;

    CellState[,] grid; // 2D array to hold the state of each cell

    GridRenderer gridRenderer;
    
    // Static colors — promote to a ScriptableObject later if you want
    static readonly Color COL_SAFE    = new Color(0.00f, 0.60f, 0.80f, 1f);
    static readonly Color COL_WARNING = new Color(1.00f, 0.10f, 0.10f, 1f);
    static readonly Color COL_FALLING = new Color(1.00f, 0.40f, 0.00f, 1f);
    static readonly Color COL_TRAIL   = new Color(0.00f, 1.00f, 0.80f, 1f);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
