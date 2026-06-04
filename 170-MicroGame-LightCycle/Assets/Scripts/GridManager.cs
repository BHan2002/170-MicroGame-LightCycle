using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    [SerializeField] private GridRenderer gridRenderer;

    [SerializeField] private float startDelay = 3f;
    [SerializeField] private float warningTime = 1.5f;
    [SerializeField] private float fallingTime = 0.75f;
    [SerializeField] private float timeBetweenCells = 0.5f;

    void Start()
    {
        if (gridRenderer == null)
        {
            gridRenderer = FindFirstObjectByType<GridRenderer>();
        }

        StartCoroutine(DisappearCellsOverTime());
    }

    IEnumerator DisappearCellsOverTime()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            int x = Random.Range(0, gridRenderer.width);
            int z = Random.Range(0, gridRenderer.height);

            gridRenderer.SetCellState(x, z, CellState.Warning);

            yield return new WaitForSeconds(warningTime);

            gridRenderer.SetCellState(x, z, CellState.Falling);

            yield return new WaitForSeconds(fallingTime);

            gridRenderer.SetCellState(x, z, CellState.Gone);

            yield return new WaitForSeconds(timeBetweenCells);
        }
    }
}