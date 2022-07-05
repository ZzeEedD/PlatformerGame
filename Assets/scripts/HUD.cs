using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static bool GameIsWon = false;
    public GameObject UIHUD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Victory.GameIsWon == true)
        {
            UIHUD.SetActive(false);
        }
    }
}
