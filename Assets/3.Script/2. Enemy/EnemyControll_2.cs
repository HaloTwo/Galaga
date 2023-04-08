using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll_2 : MonoBehaviour
{

    //�ִϸ��̼�
    private Animator animator;
    //�÷��̾� ����
    [SerializeField] private PlayerControll player;
    //����
    private SpriteRenderer spriteRenderer;
    //����
    private Player_Score player_score;
    //������ ó��
    [SerializeField] private Movement2D movement2D;
    [SerializeField] private StageData stagedata;
    [SerializeField] private Weapon weapon;


    Vector3 moveDirection = new Vector3(1f, -0.5f, 0);
    //���ó��
    public bool isDie = false;
    //Hp����-----------------------------
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
        //���� ������ Player �±��� playerControll�� Player_Score Ŭ������ �����´�.
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player_score);
        GameObject.FindGameObjectWithTag("Player").TryGetComponent(out player);
    }


    //Enemy�� �� ���� �Ǿ��� ��
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



    //�÷��̾�� �浹 ���� ��
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !isDie)
        {
            player.TakeDamage(1f);
            OnDie();
        }
    }

    //���Ͱ� �������� ���� ��
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

    //���� �����
    IEnumerator Disable_co()
    {
        //WaitUntill �ൿ�� ���������� ��ٸ�,  �ִϸ��̼��� ������ ���� ������(=1.0f)&& ���� �ִϸ��̼��� Enemy_Explosion�� ��
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
