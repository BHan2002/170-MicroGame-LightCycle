using UnityEngine;

// Script to manage the grid and it's cells, including creating the grid and handling cell interactions
public class Grid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // TODO: 2D array of cells {'safe', 'warning','falling', 'gone'} and instantiate the cell prefabs based on the state of each cell
    // configurable grid size 20x20, cell size 1 unit.
    private Cell[,] cells;
    private int gridWidth = 20;
    private int gridHeight = 20;
    private float cellSize = 1f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
