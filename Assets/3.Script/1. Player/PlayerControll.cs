using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControll : MonoBehaviour
{
    //player Hp관련 변수들
    private float MaxHp = 3;
    private float currentHp;
    public float MAXHP => MaxHp;
    public float CurrentHP => currentHp;
    //---------------------

    //움직임
    [SerializeField] private Movement2D movement2D;
    //무기
    [SerializeField] private Weapon weapon;
    //피격 모션
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Player_Score player_Score;
    [SerializeField] private StageData stagedata;



    private void Awake()
    {
        //movement2D = transform.GetComponent<Movement2D>();
        //weapon = transform.GetComponent<Weapon>();
        TryGetComponent(out movement2D);
        TryGetComponent(out weapon);
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out player_Score);
        //hp 초기화
        currentHp = MaxHp;
    }

    void Start()
    {
        if (movement2D.moveSpeed <= 0f)
        {
            movement2D.moveSpeed = 5f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(x,y,0));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            weapon.startFire();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            weapon.stopFire();
        }

    }
    private void LateUpdate()
    {
        //플레이어가 화면 범위 바깥으로 나가지 못하도록 설정
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, stagedata.LimitMin.x, stagedata.LimitMax.x),
           Mathf.Clamp(transform.position.y, stagedata.LimitMin.y, stagedata.LimitMax.y),
           0);
    }

    //다른 객체가 플레이어에게 주는 메소드
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        StopCoroutine(hitColorAnimation());
        StartCoroutine(hitColorAnimation());
        if (currentHp <= 0)
        {
            Debug.Log("player Die..............");
            OnDie();
        }
    }

    //플레이어 사망 메소드
    private void OnDie()
    {
        player_Score.Save_Score();
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }

    private IEnumerator hitColorAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
