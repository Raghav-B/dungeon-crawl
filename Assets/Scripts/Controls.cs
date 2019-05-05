using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
//using AnimatorController = UnityEditorInternal.AnimatorController;

public class Controls : MonoBehaviour
{
   string current_directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    public Sprite front_movement;
    public Sprite back_movement;
    public Sprite left_movement;
    public Sprite right_movement;
    public Sprite stationary;

    public AnimatorController front_animator;
    public AnimatorController back_animator;
    public AnimatorController left_animator;
    public AnimatorController right_animator;

    public GameObject shop_panel;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Quiz")
        {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (collision.gameObject.tag == "Cave")
        {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (collision.gameObject.tag == "Shop")
        {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Quiz")
        {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                StreamWriter write_cur_quiz_file = new StreamWriter(current_directory + "\\loaded_quiz");
                write_cur_quiz_file.WriteLine(collision.gameObject.name);
                write_cur_quiz_file.Close();
                SceneManager.LoadSceneAsync("QuizScreen", LoadSceneMode.Single);
            }
        }
        else if (collision.gameObject.tag == "Cave")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadSceneAsync("BossBattle", LoadSceneMode.Single);
            }
        }
        else if (collision.gameObject.tag == "Shop")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                shop_panel.SetActive(true);
                in_shop = true;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public Rigidbody2D character;
    public float movement_speed = 20;
    public GameObject pause_menu;
    bool in_pause_menu = false;
    bool in_shop = false;
    
    // Start is called before the first frame update
    void Start()
    {
        pause_menu.SetActive(false);
        shop_panel.SetActive(false);
    }

    void movement()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            in_pause_menu = !in_pause_menu;
        }
        if (in_pause_menu == true)
        {
            pause_menu.SetActive(true);
        } else
        {
            pause_menu.SetActive(false);
        }


        float y_vel = 0;
        float x_vel = 0;

        if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = back_movement;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = back_animator;
            y_vel = movement_speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = front_movement;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = front_animator;
            y_vel = -movement_speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = left_movement;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = left_animator;
            x_vel = -movement_speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = right_movement;
            gameObject.GetComponent<Animator>().runtimeAnimatorController = right_animator;
            x_vel = movement_speed;
        } else
        {
            gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
        }
        
        character.velocity = new Vector3(x_vel, y_vel, 0);

        y_vel = 0;
        x_vel = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (in_shop == false)
        {
            movement();
        } else
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                in_shop = false;
                shop_panel.SetActive(false);
            }
        }
           
    }
}
