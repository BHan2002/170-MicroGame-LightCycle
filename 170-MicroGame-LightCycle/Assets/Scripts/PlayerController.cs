using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour
{   [SerializeField] private GameObject trailPrefab;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float moveCooldown = 0.10f;
    [SerializeField] private GridRenderer gridRenderer;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private ScoreHandler scoreHandler;

    private Vector3 currentDirection = Vector3.forward;
    private Vector3 nextDirection = Vector3.forward;

    private float moveTimer;

    private HashSet<Vector2Int> occupiedCells = new HashSet<Vector2Int>();
    private bool isDead = false;
    
    void Start()
    {
        SnapToGrid();
    }

    void Update()
    {
        if (isDead) return;
        
        ReadTurnInput();

        moveTimer += Time.deltaTime;

        if (moveTimer >= moveCooldown)
        {
            moveTimer = 0f;
            MoveOneCell();
        }

        // If player presses R, restart the game
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            gameManager.RestartGame();
        }
    }

    void MoveOneCell()
    {
        Vector2Int currentCell = GetGridPosition();

        Instantiate(trailPrefab, transform.position, Quaternion.identity);
        occupiedCells.Add(currentCell);

        currentDirection = nextDirection;
        Vector3 nextPosition = transform.position + currentDirection * cellSize;
        Vector2Int nextCell = WorldToGrid(nextPosition);

        if (IsDeathCell(nextCell))
        {
            // Shake the screen, play a sound, and turn the screen slowly red;
            // Then delay and activate the lose screen
            StartCoroutine(DeathSequence(30.0f));
            Die();
            return;
        }

        transform.position = nextPosition;
        SnapToGrid();
        scoreHandler.AddScore(10);
    }

    bool IsDeathCell(Vector2Int cell)
    {
        if (cell.x < 0 || cell.x >= gridRenderer.width) return true;
        if (cell.y < 0 || cell.y >= gridRenderer.height) return true;
        if (occupiedCells.Contains(cell)) return true;

        return false;
    }

    void Die()
    {
        isDead = true;
        
        gameManager.SetGameState(GameManager.GameState.Lost);
        Time.timeScale = 0f;
    }

    Vector2Int GetGridPosition()
    {
        return WorldToGrid(transform.position);
    }

    Vector2Int WorldToGrid(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / cellSize);
        int z = Mathf.RoundToInt(position.z / cellSize);
        return new Vector2Int(x, z);
    }
    
    void ReadTurnInput()
    {
        Vector3 desiredDirection = currentDirection;

        if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
            desiredDirection = Vector3.forward;
        else if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
            desiredDirection = Vector3.back;
        else if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
            desiredDirection = Vector3.left;
        else if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
            desiredDirection = Vector3.right;

        // Stop user input if trying to reverse direction immediately
        if (desiredDirection != currentDirection && desiredDirection != -currentDirection)
            nextDirection = desiredDirection;
    }

    void SnapToGrid()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x / cellSize) * cellSize;
        position.y = Mathf.Round(position.y / cellSize) * cellSize;
        position.z = Mathf.Round(position.z / cellSize) * cellSize;
        transform.position = position;
    }

    IEnumerator DeathSequence(float delay)
    {
        // Implement screen shake, sound, and red tint here
        // For example:
        // - Shake: Randomly offset the camera position for a short duration
        // - Sound: Play a death sound effect
        // - Red Tint: Overlay a semi-transparent red image on the screen and fade it in

        yield return new WaitForSeconds(delay); // Wait for the specified delay before showing the lose screen
    }
}