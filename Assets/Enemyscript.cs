using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyscript : MonoBehaviour
{

    public float hp;
    public float damage;
    public float rotateSpeed, mSpeed;
    public GameObject saw;
    public float posx, posy;
    int count = 0;

    public void OnColliderEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Player")
        {
            Debug.Log(hp + "\n");
            if (hp <= 0)
            {
                Destroy(gameObject);
            }

            hp -= damage;

        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    //    if (Input.GetKey(KeyCode.T)) {
    //        Destroy(gameObject);
    //}

        count++;
        if (count%100== 0)
        {
            GameObject temp = GameObject.Instantiate(saw);
            temp.transform.SetPositionAndRotation(new Vector3(posx, posy, 0), temp.transform.rotation);
            gameObject.transform.Rotate(0.0f, 0.0f, rotateSpeed * Time.deltaTime);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(mSpeed, 0.0f);
        }
    }
}
