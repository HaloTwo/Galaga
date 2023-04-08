using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd_Score : MonoBehaviour
{
    [SerializeField]private Text Score;
    public int score;

    void Start()
    {
        Score.text = "Scroe :" + PlayerPrefs.GetInt("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
