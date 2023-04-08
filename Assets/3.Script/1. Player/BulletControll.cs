using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    [SerializeField] private StageData stage_Date;
    //private Player_Score player_Score;
    private float destroyWeight = 2f;

    private void Awake()
    {
        //Player Ã£¾Æ¼­ playerScore ³Ö¾îÁÜ
        //GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player_Score);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyControll>().TakeDamgage(1f);
            Destroy(gameObject);
        }

        else if (collision.CompareTag("Enemy2"))
        {
            collision.GetComponent<EnemyControll_2>().TakeDamage(1f);
            Destroy(gameObject);
        }


        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerControll>().TakeDamage(1f);
            Destroy(gameObject);
        }
    
    }//ÃÑ¾ËÀÌ¶û Ãæµ¹

    private void LateUpdate()
    {
        if (transform.position.y < stage_Date.LimitMin.y - destroyWeight ||
            transform.position.y > stage_Date.LimitMax.y + destroyWeight ||
            transform.position.x < stage_Date.LimitMin.x - destroyWeight ||
            transform.position.x > stage_Date.LimitMax.x + destroyWeight)
        {
            Destroy(gameObject);
        }        
    }
}
