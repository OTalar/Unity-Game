using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour
{
    public Text mainText, scoreText;
    public Button tryAgainButton, mainMenuButton;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        // buttonElemental.onClick.AddListener(elementalClicked);
        mainMenuButton.onClick.AddListener(mainMenuButtonClicked);
        tryAgainButton.onClick.AddListener(tryAgainButtonClicked);
    }


    public void setMainText(string text)
    {
        mainText.text = text;
        setScore();
    }

    void setScore()
    {
        scoreText.text = "Final Score: " + player.score;
    }

    void mainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void tryAgainButtonClicked()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
