using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using UnityEngine.UI;
using System.Linq;

public class GameControl : MonoBehaviour
{
    string current_directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    int seed;

    System.Random rng;
    int num_quizzes;

    int x_min = 10;
    int y_min = -2;
    int x_max = 35;
    int y_max = -42;

    public GameObject quiz_template;
    public GameObject shop_template;
    public GameObject boss_cave_template;
    public GameObject character;

    bool previous_save_found = false;

    List<string> files_read;

    struct object_pos
    {
        public int x_pos;
        public int y_pos;
    }

    bool Position_finder(int x_pos, int y_pos, List<object_pos> position_history)
    {
        foreach (var pos in position_history)
        {
            if (x_pos == pos.x_pos && y_pos == pos.y_pos)
            {
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        files_read = new List<string>();
        DirectoryInfo cur_directory_info = new DirectoryInfo(current_directory + "\\quizzes\\");
        IEnumerable file_list = cur_directory_info.EnumerateFiles();
        foreach (var file in file_list)
        {
            files_read.Add(file.ToString().Replace(current_directory + "\\quizzes\\", ""));
        }

        try
        {
            StreamReader read_save_file = new StreamReader(current_directory + "\\save");
            seed = Convert.ToInt32(read_save_file.ReadLine());
            rng = new System.Random(seed);
            previous_save_found = true;
            Debug.Log("Previous save found!");
            read_save_file.Close();
        }
        catch
        {
            System.Random seed_rng = new System.Random();
            seed = seed_rng.Next();
            rng = new System.Random(seed);

            StreamWriter write_save_file = new StreamWriter(current_directory + "\\save", true);
            write_save_file.WriteLine(seed);
            write_save_file.Close();

            previous_save_found = false;
        }

        List<object_pos> position_history;
        position_history = new List<object_pos>();
        num_quizzes = files_read.Count;
        Debug.Log("Quizzes detected: " + num_quizzes);

        int x_pos;
        int y_pos;

        for (int i = 0; i < num_quizzes; i++)
        {
            x_pos = rng.Next(x_min, x_max);
            y_pos = rng.Next(y_max, y_min);

            if (position_history.Count != 0)
            {
                while (Position_finder(x_pos, y_pos, position_history) == true)
                {
                    x_pos = rng.Next(x_min, x_max);
                    y_pos = rng.Next(y_max, y_min);
                }
            }

            object_pos temp_pos = new object_pos();
            temp_pos.x_pos = x_pos;
            temp_pos.y_pos = y_pos;
            position_history.Add(temp_pos);

            GameObject temp = GameObject.Instantiate(quiz_template);
            temp.transform.SetPositionAndRotation(new Vector3(x_pos, y_pos, 0), temp.transform.rotation);
            temp.name = files_read[i];
            temp.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "Would you like to start a quiz on " + temp.name + "?";
            Debug.Log(temp.name);

            if (previous_save_found == true)
            {
                string check_score_string = File.ReadAllText(current_directory + "//save");
                if (check_score_string.Contains(temp.name + "|A")) {
                    temp.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }

        object_pos extra_objects_pos = new object_pos();

        // Creating cave placement
        x_pos = rng.Next(x_min, x_max);
        y_pos = rng.Next(y_max, y_min);
        while (Position_finder(x_pos, y_pos, position_history) == true)
        {
            x_pos = rng.Next(x_min, x_max);
            y_pos = rng.Next(y_max, y_min);
        }
        GameObject temp_cave = GameObject.Instantiate(boss_cave_template);
        temp_cave.transform.SetPositionAndRotation(new Vector3(x_pos, y_pos, 0), temp_cave.transform.rotation);
        extra_objects_pos.x_pos = x_pos;
        extra_objects_pos.y_pos = y_pos;
        position_history.Add(extra_objects_pos);

        // Creating shop placement
        x_pos = rng.Next(x_min, x_max);
        y_pos = rng.Next(y_max, y_min);
        while (Position_finder(x_pos, y_pos, position_history) == true)
        {
            x_pos = rng.Next(x_min, x_max);
            y_pos = rng.Next(y_max, y_min);
        }
        GameObject temp_shop = GameObject.Instantiate(shop_template);
        temp_shop.transform.SetPositionAndRotation(new Vector3(x_pos, y_pos, 0), temp_shop.transform.rotation);
        extra_objects_pos.x_pos = x_pos;
        extra_objects_pos.y_pos = y_pos;
        position_history.Add(extra_objects_pos);

        // Determining character placement
        x_pos = rng.Next(x_min, x_max);
        y_pos = rng.Next(y_max, y_min);
        while (Position_finder(x_pos, y_pos, position_history) == true)
        {
            x_pos = rng.Next(x_min, x_max);
            y_pos = rng.Next(y_max, y_min);
        }
        character.transform.SetPositionAndRotation(new Vector3(x_pos, y_pos, 0), character.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
