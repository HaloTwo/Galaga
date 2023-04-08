using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]private GameObject Player_Bullet;
    [SerializeField]private float Attack_Rate = 0.3f;
    private Vector3 vecter = new Vector3(0, 1f, 0);
    private Vector3 player_locate;



    private void Update()
    {
        player_locate = transform.position + vecter;
    }


    //¿Ã∞« æ»æ∏
    public void TryAttack()
    {
        Instantiate(Player_Bullet, transform.position, Quaternion.identity);
    }

    private IEnumerator TryAttack_co()
    {
        while (true)
        {
            Instantiate(Player_Bullet, player_locate, Quaternion.identity);
            yield return new WaitForSeconds(Attack_Rate);
        }
    }




    public void startFire()
    {
        StartCoroutine("TryAttack_co");
    }
    public void stopFire()
    {
        StopCoroutine("TryAttack_co");
    }

}
