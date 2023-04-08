using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll_2 : MonoBehaviour
{

    //애니메이션
    private Animator animator;
    //플레이어 정보
    [SerializeField] private PlayerControll player;
    //색상
    private SpriteRenderer spriteRenderer;
    //점수
    private Player_Score player_score;
    //움직임 처리
    [SerializeField] private Movement2D movement2D;
    [SerializeField] private StageData stagedata;
    [SerializeField] private Weapon weapon;


    Vector3 moveDirection = new Vector3(1f, -0.5f, 0);
    //사망처리
    public bool isDie = false;
    //Hp관련-----------------------------
    private float MaxHP = 5f;
    private float CurrentHP;
    public float maxHp => MaxHP;
    public float currentHp => CurrentHP;
    //------------------------------------

    private void Awake()
    {
        TryGetComponent(out weapon);
        TryGetComponent(out movement2D);
        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);
        //현재 씬에서 Player 태그의 playerControll과 Player_Score 클래스를 가져온다.
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player_score);
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);
    }


    //Enemy가 재 생성 되었을 때
    private void OnEnable()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        isDie = false;
        CurrentHP = maxHp;
    }


    private void Update()
    {
        float ti = Time.time;

            if (transform.position.x >= stagedata.LimitMax.x || transform.position.x <= stagedata.LimitMin.x)
            {
                  moveDirection.x = -moveDirection.x;           
            
            }
        movement2D.MoveTo(new Vector3(moveDirection.x, -0.3f, 0));


        Debug.Log(ti);

        if (ti % 1f == 0)
        {
            weapon.startFire();
        }


    }



    //플레이어와 충돌 했을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !isDie)
        {
            player.TakeDamage(1f);
            OnDie();
        }
    }

    //몬스터가 데미지를 입을 때
    public void TakeDamage(float Damage)
    {
        CurrentHP -= Damage;

        StartCoroutine(HitAnimation_co());
        StopCoroutine(HitAnimation_co());
        if (CurrentHP <= 0)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            player_score.Set_Score(5);
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

    //몬스터 사망시
    IEnumerator Disable_co()
    {
        //WaitUntill 행동이 끝날때까지 기다림,  애니메이션이 끝까지 동작 했을때(=1.0f)&& 현재 애니메이션이 Enemy_Explosion일 때
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Explosion"));
        gameObject.SetActive(false);
    }

    IEnumerator HitAnimation_co()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

}
