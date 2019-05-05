using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myride : MonoBehaviour
{
    public GameObject leaderboard;
    public GameObject credits;
    public GameObject main;
    public GameObject stats;
    public GameObject continuegame;

    // Start is called before the first frame update
    void Start()
    {
    leaderboard.SetActive(false);
    credits.SetActive(false);
    main.SetActive(true);
    stats.SetActive(false);
    //continuegame.SetActive(false);
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
