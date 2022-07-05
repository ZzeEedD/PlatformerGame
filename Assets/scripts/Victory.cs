using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public static bool GameIsWon = false;
    public GameObject victoryMenuUI;
    public Player player;

    private void Start()
    {
        GameIsWon = false;
    }
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D win)
    {
        Player player = win.GetComponent<Player>();
        if (player != null)
        {
            Win();
        }
    }


    void Win()
    {
        victoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsWon = true;
        player.enabled = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
