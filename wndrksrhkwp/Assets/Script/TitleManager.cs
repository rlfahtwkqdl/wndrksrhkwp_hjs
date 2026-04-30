using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject helpPanel;
    public GameObject BodFnel;


    public void GameStart()
    {
        SceneManager.LoadScene("Stage_1");
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

    public void OpenBod()
    {
        BodFnel.SetActive(true);
    }

    public void CloseBod()
    {
        BodFnel.SetActive(false);
    }

    public void GameExit()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
        // 실제 빌드된 게임 종료
        Application.Quit();

        // 유니티 에디터 테스트용 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

       
    }
}