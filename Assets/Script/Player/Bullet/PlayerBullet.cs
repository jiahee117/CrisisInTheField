using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public PlayerAction action;
    public float moveSpeed;
    public Vector3 direction;
    public float timer;
    public Vector3 range;
    public float scale;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        range = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {

        Fly();
        
        if (Mathf.Abs(range.y)>3*scale || Mathf.Abs(range.x) > 3* scale) //  if bullet fly range over 3
        {
            Destroy();
        }
        //timer += Time.deltaTime;
    }

    void Fly()
    {
        Vector3 distance = new Vector3(direction.x, direction.y, 0) * moveSpeed;
        transform.position += distance;
        range += distance;
    }

    private void Destroy()
    {
        
            Destroy(gameObject);
            action.bulletTotal--;
       
    }
}
