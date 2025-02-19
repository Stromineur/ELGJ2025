using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private List<GameObject> tutolides;
    
    private int tutorialIndex = 0;
    
    public void StartGame()
    {
        SceneManager.LoadScene("Main_Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LaunchTutorial()
    {
        mainMenuPanel.SetActive(false);
        tutorialPanel.SetActive(true);
        tutorialIndex = 0;
        tutolides[tutorialIndex].SetActive(true);
    }
    public void ScrollInTutorial(int index)
    {
        if (tutorialIndex < 0)
        {
            ReturnToMainMenu();
            return;
        }
        
        if (tutorialIndex >= 0 && tutorialIndex < tutolides.Count)
        {
            tutolides[tutorialIndex].SetActive(false);
        }

        tutorialIndex += index;

        if (tutorialIndex >= tutolides.Count)
        {
            StartGame();
            return;
        }
        
        if (tutorialIndex < 0)
        {
            mainMenuPanel.SetActive(true);
            tutorialPanel.SetActive(false);
            tutorialIndex = 0;
            return;
        }

        tutolides[tutorialIndex].SetActive(true);
    }

    private void ReturnToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        tutorialPanel.SetActive(false);
        tutorialIndex = 0;
    }
}
