using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.UI;



public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject InstructionPanel;

    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        if (CreditsPanel != null)
        {
            StartPanel.SetActive(false);
            CreditsPanel.SetActive(true);
        }

   
    }

    public void BackButton()
    {
        if (StartPanel != null)
        {
            StartPanel.SetActive(true);
            CreditsPanel.SetActive(false);
        }

    }

    public void InstructionButton()
    {
        if (InstructionPanel != null)
        {
            InstructionPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}


