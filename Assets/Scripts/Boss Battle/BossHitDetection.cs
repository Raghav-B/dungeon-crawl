using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitDetection : MonoBehaviour
{
    public float boss_hp = 1000f;
    public float projectile_damage = 100f;
    public GameObject health_bar;
    float health_bar_length = 200;
    float health_reduction_increment;
    public GameObject boss;
    public GameObject win_screen;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health_bar_length -= health_reduction_increment;
            gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(health_bar_length, 30);

            Destroy(collision.gameObject);

            if (health_bar_length <= 0) {
                Destroy(gameObject);
                win_screen.SetActive(true);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        boss_hp = 1000f;
        health_reduction_increment = boss_hp / projectile_damage;
        Debug.Log(health_reduction_increment);
        win_screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
