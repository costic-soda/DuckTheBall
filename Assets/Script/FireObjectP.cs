using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObjectP : MonoBehaviour
{
    public FireBullet FireGameObject;
    public float timeTospawnBullet = 2f;
    public PlayerAnimation player;
    float time;
    List<GameObject> bulletList;
 
    private void Start()
    {
        bulletList = new List<GameObject>();
        for (int i = 0; i < 8; i++)
        {
            FireBullet objBullet = (FireBullet)Instantiate(FireGameObject);
            objBullet.gameObject.SetActive(false);
            //transform.GetChild(0).localScale;
            bulletList.Add(objBullet.gameObject);
        }
      
    }
    void Fire()
    {
        if (player.isAlive == false)
            return;


        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeInHierarchy)
            {
                bulletList[i].transform.position = transform.position;
                bulletList[i].transform.rotation = transform.rotation;
                bulletList[i].transform.localScale = FireGameObject.transform.localScale;
                bulletList[i].SetActive(true);
            

                break;
            }
        }

    }

    private void Update()
    {

        if (GameManager.instance.score >= 6)
        {
            timeTospawnBullet = 1.5f;
        }

        if (GameManager.instance.score >= 15)
        {
            timeTospawnBullet = 1f;
        }

        time += Time.deltaTime;

        if (time > timeTospawnBullet)
        {     
                Fire();
            
                time = 0;
               }
    }
}
