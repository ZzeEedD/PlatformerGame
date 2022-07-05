using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuButton;

    private bool isRestart;
    private float alpha;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        alpha = 1;
        restartButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    public IEnumerator Fade(bool toVisible)
    {
        float step = toVisible ? 0.1f : -0.1f;
        int endValue = toVisible ? 1 : 0;
        while (alpha != endValue)
        {
            alpha += step;
            canvasGroup.alpha = alpha;
            if (alpha < 0)
            {
                alpha = 0;
            }
            else if (alpha > 1)
            {
                alpha = 1;
            }
            yield return new WaitForSeconds(0.08f);
        }
    }

    public IEnumerator StartButtons()
    {
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        Image image = restartButton.GetComponent<Image>();

        while (!isRestart)
        {
            image.enabled = false;
            yield return new WaitForSeconds(0.5f);
            image.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
