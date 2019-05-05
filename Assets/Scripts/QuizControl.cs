using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Reflection;
using System;

public class QuizControl : MonoBehaviour
{
    string current_directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    string quiz_name;

    public GameObject correct;
    public GameObject wrong;
    public GameObject end_screen;

    public int num_questions;
    int num_correct = 0;

    public GameObject question_complete_bar;
    float cur_question_bar_length;
    public Text question_num_text;
    public Text question;
    public GameObject option1;
    public GameObject option2;
    public GameObject option3;
    public GameObject option4;

    public GameObject time_left_bar;
    float time_left = 500;
    float time_increment;

    public GameObject hp_bar;
    float cur_hp_bar_length;
    float hp_increment;

    List<List<string>> questions;
    List<int> answers;
    int cur_question = 0;

    bool start_question = true;
    bool has_ended = false;

    // Start is called before the first frame update
    void Start()
    {
        StreamReader read_quiz_name = new StreamReader(current_directory + "\\loaded_quiz");
        quiz_name = read_quiz_name.ReadLine();
        read_quiz_name.Close();

        StreamReader read_quiz_data = new StreamReader(current_directory + "\\quizzes\\" + quiz_name);
        string temp_read_quiz_data;

        questions = new List<List<string>>();
        answers = new List<int>();

        // Reading vars from file and storing them nicely in lists and shit
        while ((temp_read_quiz_data = read_quiz_data.ReadLine()) != null)
        {
            string[] separated_input = temp_read_quiz_data.Split('|');

            foreach (var x in separated_input)
            {
                Debug.Log(x.ToString());
            }

            answers.Add(Convert.ToInt32(separated_input[1]));
            List<string> question_info_list = new List<string>();

            for (int i = 2; i < 7; i++)
            {
                question_info_list.Add(separated_input[i]);
            }

            questions.Add(question_info_list);
        }

        foreach (var y in questions)
        {
            Debug.Log(y.ToString());
        }

        Application.targetFrameRate = 60;

        correct.SetActive(false);
        wrong.SetActive(false);

        num_questions = questions.Count;

        cur_question++;
        question_num_text.text = "Question: " + cur_question + " of " + num_questions;
        cur_question--;

        hp_increment = 230 / num_questions;
        cur_question_bar_length = -230 + hp_increment;
        cur_hp_bar_length = 0;

        hp_bar.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        question_complete_bar.GetComponent<RectTransform>().offsetMax = new Vector2(-230 + hp_increment, 0);

        start_question = true;

        end_screen.SetActive(false);
    }

    void is_correct(GameObject option)
    {
        cur_question++;
        cur_question_bar_length += hp_increment;
        cur_question++;
        num_correct++;

        option.GetComponent<Image>().color = new Color(0, 255, 0);

        if (cur_question <= num_questions)
        {
            question_num_text.text = "Question: " + cur_question + " of " + num_questions;
        }
        else
        {
            question_num_text.text = "Quiz completed.";
        }

        cur_question--;
        question_complete_bar.GetComponent<RectTransform>().offsetMax = new Vector2(cur_question_bar_length, 0);
        correct.SetActive(true);
        start_question = false;
        StartCoroutine(wait_function());    
    }

    void is_wrong(GameObject option)
    {
        cur_question++;
        cur_hp_bar_length -= hp_increment;
        cur_question_bar_length += hp_increment;

        option.transform.parent.GetChild(answers[cur_question] - 1).GetComponent<Image>().color = new Color(0, 255, 0);

        cur_question++;
        if (cur_question <= num_questions)
        {
            question_num_text.text = "Question: " + cur_question + " of " + num_questions;
        }
        else
        {
            question_num_text.text = "Quiz completed.";
        }

        cur_question--;
        hp_bar.GetComponent<RectTransform>().offsetMax = new Vector2(cur_hp_bar_length, 0);
        question_complete_bar.GetComponent<RectTransform>().offsetMax = new Vector2(cur_question_bar_length, 0);
        wrong.SetActive(true);
        start_question = false;
        StartCoroutine(wait_function());
    }

    IEnumerator wait_function()
    {
        yield return new WaitForSeconds(1);
        wrong.SetActive(false);
        correct.SetActive(false);
        start_question = true;
    }

    void FixedUpdate()
    {
        if (start_question == true)
        {
            if (cur_question == num_questions)
            {
                if (num_correct == 0)
                {
                    end_screen.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Beaten but not defeated, you rise up once again to continue your journey!";
                    end_screen.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "You scored " + num_correct + " out of " + num_questions + " correct!";
                }
                else if (num_correct == num_questions)
                {
                    end_screen.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "There are only so many things standing in your way now.";
                    end_screen.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "You scored " + num_correct + " out of " + num_questions + " correct!";

                    StreamWriter save_file_writer = new StreamWriter(current_directory + "\\save", true);
                    save_file_writer.WriteLine("|" + quiz_name + "|A");
                    save_file_writer.Close();
                }
                else
                {
                    end_screen.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "As you turn and face the path that lies ahead, you acknowledge how far you've come," +
                        "but the road you travel has yet to come to an end.";
                    end_screen.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "You scored " + num_correct + " out of " + num_questions + " correct!";
                }

                end_screen.SetActive(true);
                has_ended = true;
            }
            else
            {
                question.text = questions[cur_question][0];
                option1.transform.GetChild(0).GetComponent<Text>().text = questions[cur_question][1];
                option2.transform.GetChild(0).GetComponent<Text>().text = questions[cur_question][2];
                option3.transform.GetChild(0).GetComponent<Text>().text = questions[cur_question][3];
                option4.transform.GetChild(0).GetComponent<Text>().text = questions[cur_question][4];

                option1.GetComponent<Image>().color = new Color(255, 255, 255);
                option2.GetComponent<Image>().color = new Color(255, 255, 255);
                option3.GetComponent<Image>().color = new Color(255, 255, 255);
                option4.GetComponent<Image>().color = new Color(255, 255, 255);

                time_left_bar.GetComponent<RectTransform>().sizeDelta = new Vector2(time_left, 30);
                time_left -= 0.5f; // Increase to make countdown faster
                if (time_left <= 0)
                {
                    is_wrong(option1);
                    time_left = 500;
                }

                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    if (answers[cur_question] == 1)
                    {
                        is_correct(option1);
                    }
                    else
                    {
                        option1.transform.GetComponent<Image>().color = new Color(255, 0, 0);
                        is_wrong(option1);
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    if (answers[cur_question] == 2)
                    {
                        is_correct(option2);
                    }
                    else
                    {
                        option2.transform.GetComponent<Image>().color = new Color(255, 0, 0);
                        is_wrong(option2);
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Alpha3))
                { 
                    if (answers[cur_question] == 3)
                    {
                        is_correct(option3);
                    }
                    else
                    {
                        option3.transform.GetComponent<Image>().color = new Color(255, 0, 0);
                        is_wrong(option3);
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    if (answers[cur_question] == 4)
                    {
                        is_correct(option4);
                    }
                    else
                    {
                        option4.transform.GetComponent<Image>().color = new Color(255, 0, 0);
                        is_wrong(option4);
                    }
                }
            }
        }

        if (has_ended == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Unloaded");
                SceneManager.LoadSceneAsync("GameScreen", LoadSceneMode.Single);
                SceneManager.UnloadSceneAsync("QuizScreen");
            }
        }
    }
}
