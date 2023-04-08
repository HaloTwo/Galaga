using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    //[SerializeField] private int monsterlife = 5;
    private Animator animator;
    private PlayerControll player;
    private SpriteRenderer spriteRenderer;
    private Player_Score player_score;

    public bool isDie = false;

    //에너미 HP관련 변수

    private float MaxHP = 2f;
    private float CurrentHP;

    public float MAXHP => MaxHP;
    public float Currenthp => CurrentHP;

    //------------------------------------

    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
        TryGetComponent(out spriteRenderer);
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player_score);
        //현재 씬에 있는 Player태그의 playerControll을 불러온다.
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();
        //근데 PlayerControll가 없으면?
        //trygetComponent -> 반환값은 bool, 이게 GetComponent보다 20배 빠름
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);

        //이런 방식도 있음
        //GameObject player_2 = GameObject.FindGameObjectWithTag("Player");
        //
        //if (!player_2.TryGetComponent(out player))
        //{
        //    player_2.AddComponent<PlayerControll>();
        //}
    }
    private void OnEnable()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        isDie = false;
        CurrentHP = MAXHP;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Player") && !isDie)
        {
            player.TakeDamage(1f);
            OnDie();
        }
    }



    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.CompareTag("bullet"))
    //    {
    //         Destroy(col.gameObject);
    //        monsterlife--;
    //        if (monsterlife == 0)
    //        {
    //            monsterlife = 0;
    //            startDie();
    //        }
    //    }
    //
    //}

    public void TakeDamgage(float Damage)
    {
        CurrentHP -= Damage;

        StartCoroutine(HitAnimation_co());
        StopCoroutine(HitAnimation_co());

        if (CurrentHP <= 0)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            player_score.Set_Score(3);
            OnDie();
        }
    }



    public void OnDie()
    {
        if (!isDie)
        {
            isDie = true;
            animator.SetTrigger("Enemy_Die");
            StartCoroutine(Disable_co());
        }

    }

    IEnumerator Disable_co()
    {
        Debug.Log("시작");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Explosion"));
        gameObject.SetActive(false);

    }

    private IEnumerator HitAnimation_co()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }



    //private IEnumerator Die_co()
    //{
    //    if (monsterlife == 0)
    //    {
    //        Debug.Log("시작");
    //        animator.SetTrigger("Enemy_Die");
    //        yield return new WaitForSeconds(1f);
    //        monsterlife = 5;
    //        gameObject.SetActive(false);
    //        Debug.Log("1초 후 ...");
    //    }
    //}
    //public void startDie()
    //{
    //    StartCoroutine("Die_co");
    //}
    //public void stopDie()
    //{
    //    StopCoroutine("Die_co");
    //}
}
