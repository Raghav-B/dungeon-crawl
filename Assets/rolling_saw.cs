using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolling_saw : MonoBehaviour
{
    public float mSpeed;
    public float rotateSpeed;
    public GameObject saw_parent;
    public int damage = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
    gameObject.transform.Rotate(0.0f, 0.0f, rotateSpeed * Time.deltaTime);
    saw_parent.GetComponent<Rigidbody2D>().velocity = new Vector2(mSpeed, 0.0f);
    }
}
