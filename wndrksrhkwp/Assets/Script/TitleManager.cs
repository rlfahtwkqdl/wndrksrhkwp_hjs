using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject helpPanel;


    public void GameStart()
    {
        SceneManager.LoadScene("Stage");
    }

    // Update is called once per frame
    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }

    public void GameExit()
    {
        SceneManager.LoadScene("TitleScene");
    }
}