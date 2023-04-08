using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpViewer : MonoBehaviour
{
    private EnemyControll enemy;
    private Slider HPsilder;
    
    public void Setup(EnemyControll enemy)
    {
        this.enemy = enemy;
        TryGetComponent(out HPsilder);
    }


    // Update is called once per frame
    void Update()
    {
        HPsilder.value = enemy.Currenthp / enemy.MAXHP;
    }
}
