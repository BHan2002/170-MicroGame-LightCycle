using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   [SerializeField] private GameObject trailPrefab;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float moveCooldown = 0.12f;

    private Vector3 currentDirection = Vector3.forward;
    private Vector3 nextDirection = Vector3.forward;

    private float moveTimer;
    
    void Start()
    {
        SnapToGrid();
    }

    void Update()
    {
        ReadTurnInput();

        moveTimer += Time.deltaTime;

        if (moveTimer >= moveCooldown)
        {
            moveTimer = 0f;

        // leave trail at the current grid cell
        if (trailPrefab != null)
        {
            Instantiate(trailPrefab, transform.position, Quaternion.identity);
        }

            currentDirection = nextDirection;
            transform.position += currentDirection * cellSize;
            SnapToGrid();
        }

        // Mark the current position with a trail
        // For simplicity, we can just instantiate a cube at the current position
        
    }
    
    void ReadTurnInput()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame)
            nextDirection = Vector3.forward;
        else if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
            nextDirection = Vector3.back;
        else if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
            nextDirection = Vector3.left;
        else if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
            nextDirection = Vector3.right;
    }

    void SnapToGrid()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Round(position.x / cellSize) * cellSize;
        position.y = Mathf.Round(position.y / cellSize) * cellSize;
        position.z = Mathf.Round(position.z / cellSize) * cellSize;
        transform.position = position;
    }
}