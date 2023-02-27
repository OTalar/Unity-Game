using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Button exitButton, startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startGame);
        exitButton.onClick.AddListener(exitGame);
    }
    void startGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
    void exitGame()
    {
        Application.Quit();
    }

}
