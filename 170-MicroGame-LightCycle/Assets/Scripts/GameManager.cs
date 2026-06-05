using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // controls state, restart reloads scene or resets grid
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private TextMeshProUGUI winFinalScoreText;
    [SerializeField] private TextMeshProUGUI loseFinalScoreText;
    public enum GameState
    {
        Playing,
        Won,
        Lost,
        Menu,


    }
    
    private GameState currentState;
    void Start()
    {
        // Start the game in the menu state, waiting for player input to begin
        Time.timeScale = 1f;
        SetGameState(GameState.Menu);

    }

    public void SetGameState(GameState newState)
    {
        currentState = newState;

        menuScreen.SetActive(newState == GameState.Menu);
        gameUI.SetActive(newState == GameState.Playing);
        winScreen.SetActive(newState == GameState.Won);
        loseScreen.SetActive(newState == GameState.Lost);

        Time.timeScale = newState == GameState.Playing ? 1f : 0f;

        if (playerController != null)
            playerController.enabled = newState == GameState.Playing;
    }
    public void StartGame()
    {
        SetGameState(GameState.Playing);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void WinGame()
    {
        
        winFinalScoreText.text = "Final Score: " + scoreHandler.GetScore();
        SetGameState(GameState.Won);
        
    }

    public void LoseGame()
    {
        
        loseFinalScoreText.text = "Final Score: " + scoreHandler.GetScore();
        // activate final score text on lose screen
        SetGameState(GameState.Lost);
       
    }



}
