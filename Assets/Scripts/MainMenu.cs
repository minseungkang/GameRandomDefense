using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnClickStartGame()
    {
    	SceneManager.LoadScene("SampleScene");
    }

    public void OnClickExitGame()
    {
    	Application.Quit();
    }

}
