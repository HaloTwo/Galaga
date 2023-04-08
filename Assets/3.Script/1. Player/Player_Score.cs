using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Score : MonoBehaviour
{
    private int player_Score = 0;
    public int Player_score => player_Score;

    [SerializeField] private Text Score_Text;


    // Start is called before the first frame update
    private void Awake()
    {
        player_Score = 0;
    }

    public void Set_Score(int plusScore)
    {
        player_Score += plusScore;
        Score_Text.text = "Score : " + player_Score;
    }
    public void Save_Score()
    {
        PlayerPrefs.SetInt("Score", player_Score);
        PlayerPrefs.GetInt("Score", player_Score);
        PlayerPrefs.Save();
    }
}
