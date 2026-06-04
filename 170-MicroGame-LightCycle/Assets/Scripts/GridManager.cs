using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    [SerializeField] private GridRenderer gridRenderer;

    [SerializeField] private float startDelay = 3f;
    [SerializeField] private float warningTime = 1.5f;
    [SerializeField] private float fallingTime = 0.75f;
    [SerializeField] private float timeBetweenCells = 0.5f;
    [SerializeField] private float totalTime = 30f;
    [SerializeField] private float warningDuration = 1.5f;
    [SerializeField] private float fallingDuration = 0.75f;

    IEnumerator Start()
    {
        if (gridRenderer == null)
        {
            gridRenderer = FindFirstObjectByType<GridRenderer>();
        }
        yield return null; // wait a frame for GridRenderer to initialize
        // Random cells will start disappearing after a short delay, and then continue indefinitely
        // StartCoroutine(DisappearCellsOverTime());
        StartCoroutine(SpiralCollapse());
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

    IEnumerator SpiralCollapse()
    {
        List<Vector2Int> spiral = GenerateSpiralOrder(
            gridRenderer.width,
            gridRenderer.height
        );

        float delayBetweenCells =
            (totalTime - warningDuration - fallingDuration) / (spiral.Count - 1);

        for (int i = 0; i < spiral.Count; i++)
        {
            Vector2Int cell = spiral[i];

            StartCoroutine(CollapseCell(cell.x, cell.y));

            yield return new WaitForSeconds(delayBetweenCells);
        }
    }

    IEnumerator CollapseCell(int x, int z)
    {
        gridRenderer.SetCellState(x, z, CellState.Warning);

        yield return new WaitForSeconds(warningDuration);

        gridRenderer.SetCellState(x, z, CellState.Falling);

        yield return new WaitForSeconds(fallingDuration);

        gridRenderer.SetCellState(x, z, CellState.Gone);
    }
    List<Vector2Int> GenerateSpiralOrder(int width, int height)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        int left = 0;
        int right = width - 1;
        int bottom = 0;
        int top = height - 1;

        while (left <= right && bottom <= top)
        {
            for (int x = left; x <= right; x++)
                result.Add(new Vector2Int(x, bottom));

            bottom++;

            for (int z = bottom; z <= top; z++)
                result.Add(new Vector2Int(right, z));

            right--;

            if (bottom <= top)
            {
                for (int x = right; x >= left; x--)
                    result.Add(new Vector2Int(x, top));

                top--;
            }

            if (left <= right)
            {
                for (int z = top; z >= bottom; z--)
                    result.Add(new Vector2Int(left, z));

                left++;
            }
        }

        return result;
    }
}