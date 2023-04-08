using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private GameObject Enemy;
    [SerializeField]private StageData stagedata;
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject enemyHPPrefabs;
    [SerializeField] private Transform CanvasTransform;
    private GameObject[] enemy;
    public int count = 50;
    private int current_index = 0;

    private Vector2 Poolpsition = new Vector2(10f, 0);
    private float lastSpawnTime;

    private void Awake()
    {

        StartCoroutine(spawnEnemy_co());
    }

    //코루틴 방식의 생성
    private IEnumerator spawnEnemy_co() 
    {
        WaitForSeconds wfs = new WaitForSeconds(spawnTime);
        //지속적으로 new키워드를 사용해서 생성하는 경우 모조리 다 가비지 수집의 대상이 된다.
        //그래서~나는 WaitForSecound 캐싱하여 사용 할 것입니다.
        /* 
         * 캐싱이란 ?
         * 컴퓨팅에서 오랜시간 걸리는 작업의 결과를 저장해서 
         * 시간과 비용을 절감하는 기법
         */

        enemy = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            enemy[i] = Instantiate(Enemy, Poolpsition, Quaternion.identity);         
        }
        yield return wfs;
        //5개 생성한다.
        lastSpawnTime = 0;
    }



    void Update()
    {

        //사망시 생성안하는거 아직 게임매니저 없어서 안함

        if (Time.time >= lastSpawnTime + spawnTime)
        {
            lastSpawnTime = Time.time;

            enemy[current_index].SetActive(false);
            enemy[current_index].SetActive(true);

            float positionX = Random.Range(stagedata.LimitMin.x, stagedata.LimitMax.x);
            Vector3 position = new Vector3(positionX, stagedata.LimitMax.y + 1f, 0f);

            enemy[current_index].transform.position = position;
            SpawnEnemyHP(enemy[current_index].GetComponent<EnemyControll>());
            current_index++;

            if (current_index == count)
            {
                current_index = 0;
            }

            {

            }

        }

    }
    private void SpawnEnemyHP(EnemyControll enemy)
    {
        GameObject silderClone = Instantiate(enemyHPPrefabs);

        silderClone.transform.SetParent(CanvasTransform);
        silderClone.transform.localScale = Vector3.one;


        silderClone.GetComponent<EnemyHpPositionSetter>().Setup(enemy.gameObject);
        silderClone.GetComponent<EnemyHpViewer>().Setup(enemy);
    }
}
