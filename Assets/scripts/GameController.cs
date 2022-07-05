using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private DeathScreen deathScreen; 
    private void Start()
    {
        StartCoroutine(StartRouitine());
    }
    public void LoseGame()
    {
        StartCoroutine(LoseRoutine());
    }

    private IEnumerator StartRouitine()
    {
        yield return StartCoroutine(deathScreen.Fade(false));
        deathScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
    public void ReturntoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private IEnumerator LoseRoutine()
    {
        Time.timeScale = 0.4f;
        deathScreen.gameObject.SetActive(true);
        yield return StartCoroutine(deathScreen.Fade(true));

        StartCoroutine(deathScreen.StartButtons());
    }
}
