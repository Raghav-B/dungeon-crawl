using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject fireball;
    public float fireball_speed = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject temp = Instantiate(fireball);
            temp.transform.SetPositionAndRotation(new Vector3(-11, -4, 0), temp.transform.rotation);
            temp.transform.GetComponent<Rigidbody2D>().velocity = new Vector2(fireball_speed, 0);
        }
    }
}
