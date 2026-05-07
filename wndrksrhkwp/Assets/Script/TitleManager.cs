using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject BodFnel;
    public GameObject RkBodFnel;
    public GameObject Rkpen1Fnel;
    public GameObject Rkpen2Fnel;
    public GameObject Rkpen3Fnel;
    public GameObject Rkpen4Fnel;
    public GameObject Rkpen5Fnel;

    // --- 도움말/게시판 등은 그대로 유지 ---
    public void OpenHelp() => helpPanel.SetActive(true);
    public void CloseHelp() => helpPanel.SetActive(false);
    public void OpenBod() => BodFnel.SetActive(true);
    public void CloseBod() => BodFnel.SetActive(false);
    public void OpenRkBod() => RkBodFnel.SetActive(true);
    public void CloseRkBod() => RkBodFnel.SetActive(false);

    // --- 랭킹 패널 전용 닫기 함수 (내부적으로만 사용) ---
    private void CloseAllRkpans()
    {
        Rkpen1Fnel.SetActive(false);
        Rkpen2Fnel.SetActive(false);
        Rkpen3Fnel.SetActive(false);
        Rkpen4Fnel.SetActive(false);
        Rkpen5Fnel.SetActive(false);
    }

    // --- Rkpen 영역: 클릭 시 "싹 다 닫고 해당 패널만 토글" ---
    public void OpenRkpen1()
    {
        bool currentState = Rkpen1Fnel.activeSelf; // 현재 상태 기억
        CloseAllRkpans(); // 일단 모든 랭킹 패널을 다 끔
        Rkpen1Fnel.SetActive(!currentState); // 누른 것만 반전 (꺼져있었으면 켜짐)
    }

    public void OpenRkpen2()
    {
        bool currentState = Rkpen2Fnel.activeSelf;
        CloseAllRkpans();
        Rkpen2Fnel.SetActive(!currentState);
    }

    public void OpenRkpen3()
    {
        bool currentState = Rkpen3Fnel.activeSelf;
        CloseAllRkpans();
        Rkpen3Fnel.SetActive(!currentState);
    }

    public void OpenRkpen4()
    {
        bool currentState = Rkpen4Fnel.activeSelf;
        CloseAllRkpans();
        Rkpen4Fnel.SetActive(!currentState);
    }

    public void OpenRkpen5()
    {
        bool currentState = Rkpen5Fnel.activeSelf;
        CloseAllRkpans();
        Rkpen5Fnel.SetActive(!currentState);
    }

    // 기존 닫기 함수들도 이름은 유지 (혹시 쓰고 계실까봐)
    public void CloseRkpen1() => Rkpen1Fnel.SetActive(false);
    public void CloseRkpen2() => Rkpen2Fnel.SetActive(false);
    public void CloseRkpen3() => Rkpen3Fnel.SetActive(false);
    public void CloseRkpen4() => Rkpen4Fnel.SetActive(false);
    public void CloseRkpen5() => Rkpen5Fnel.SetActive(false);

    public void GameExit() => SceneManager.LoadScene("TitleScene");
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}