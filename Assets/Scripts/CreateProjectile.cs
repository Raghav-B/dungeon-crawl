using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateProjectile : MonoBehaviour
{
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject temp = Instantiate(projectile);
            temp.transform.SetPositionAndRotation(new Vector3(-10, -4, 0), transform.rotation);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(200, 0);
        }
    }
}
