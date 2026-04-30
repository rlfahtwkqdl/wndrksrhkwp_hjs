using TMPro;
using UnityEngine;

public class ScoreTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI Stage_1;
    public TextMeshProUGUI Stage_2;
    public TextMeshProUGUI Stage_3;
    public TextMeshProUGUI Stage_4;
    public TextMeshProUGUI Stage_5;

    void Start()
    {
        Stage_1.text = "STAGE 1 : " + HighScore.Load(1).ToString();
        Stage_2.text = "STAGE 2 : " + HighScore.Load(2).ToString();
        Stage_3.text = "STAGE 3 : " + HighScore.Load(3).ToString();
        Stage_4.text = "STAGE 4 : " + HighScore.Load(4).ToString();
        Stage_5.text = "STAGE 5 : " + HighScore.Load(5).ToString();
    }

}
