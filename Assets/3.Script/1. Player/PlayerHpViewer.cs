using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpViewer : MonoBehaviour
{
    [SerializeField] private Slider sliderHP;
    [SerializeField] private PlayerControll player;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
        //TryGetComponent(out sliderHP);
    }

    // Update is called once per frame
    void Update()
    {
        sliderHP.value = player.CurrentHP / player.MAXHP;
    }
}
