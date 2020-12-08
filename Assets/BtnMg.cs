using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnMg : MonoBehaviour
{
    [SerializeField] GameObject panel;
  
    public void ClosePanel()
    {
        SceneManager.LoadScene("Min");
    }

    public void SetPanel()
    {
        panel.SetActive(true);
    }
  

    public void TitelSceen()
    {
        SceneManager.LoadScene("Titel");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Game");
    }
}
