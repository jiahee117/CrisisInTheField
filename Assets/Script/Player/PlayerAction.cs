using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public int bulletTotal;
    public bool canShoot;
    Transform FirePoint;
    Object playerBullet;
    Camera mainCamera;
    public Vector3 moveDirectionFirePoint;
    public Vector3 moveDirectionPlayer;

    // Start is called before the first frame update
    void Start()
    {
        FirePoint = transform.Find("FirePoint");
        playerBullet = Resources.Load("Player/bullet");
        mainCamera = Camera.main;

    }

    

    // Update is called once per frame
    void Update()
    {
        if(bulletTotal < 10) //if bullet less than 10
        {
            canShoot = true;
        }else canShoot = false;
        moveDirectionFirePoint = mainCamera.ScreenToWorldPoint(Input.mousePosition) - FirePoint.transform.position; //get player forward direction
        moveDirectionPlayer = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }


    public void Fire()
    {
        if (canShoot)
        {
            GameObject bullet = Instantiate(playerBullet, FirePoint.transform.position, Quaternion.identity) as GameObject;

            
            PlayerBullet plyrBullet = bullet.GetComponent<PlayerBullet>();
            plyrBullet.direction = moveDirectionFirePoint.normalized;
            plyrBullet.action = this;
            plyrBullet.scale = transform.localScale.x;
            bulletTotal++;
        }
        
    }

}
