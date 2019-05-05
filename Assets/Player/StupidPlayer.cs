using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidPlayer : MonoBehaviour
{
    public int health;
    public float coinspeed;
    public GameObject fireball;
    public float posx, posy;


    void TakeDamage(int damage)
    {
        health -= damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject temp = GameObject.Instantiate(fireball);
            temp.transform.SetPositionAndRotation(new Vector3(posx, posy, 0), temp.transform.rotation);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(coinspeed, 0);

        }
    }
}
