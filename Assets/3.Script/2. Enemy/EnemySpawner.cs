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

    //�ڷ�ƾ ����� ����
    private IEnumerator spawnEnemy_co() 
    {
        WaitForSeconds wfs = new WaitForSeconds(spawnTime);
        //���������� newŰ���带 ����ؼ� �����ϴ� ��� ������ �� ������ ������ ����� �ȴ�.
        //�׷���~���� WaitForSecound ĳ���Ͽ� ��� �� ���Դϴ�.
        /* 
         * ĳ���̶� ?
         * ��ǻ�ÿ��� �����ð� �ɸ��� �۾��� ����� �����ؼ� 
         * �ð��� ����� �����ϴ� ���
         */

        enemy = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            enemy[i] = Instantiate(Enemy, Poolpsition, Quaternion.identity);         
        }
        yield return wfs;
        //5�� �����Ѵ�.
        lastSpawnTime = 0;
    }



    void Update()
    {

        //����� �������ϴ°� ���� ���ӸŴ��� ��� ����

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
