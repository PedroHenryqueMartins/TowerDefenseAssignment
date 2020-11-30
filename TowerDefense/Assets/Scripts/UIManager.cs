using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text scoreTxt;
    public Text enemiesLftTxt;
    public Text enemiesKldTxt;

    public int enemiesKilled;
    public int enemiesLeft;
    public int score;


    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.Register<UIManager>(this);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreTxt.text = "Score: " + score.ToString();
        enemiesLftTxt.text = "Enemies Left: " + enemiesLeft.ToString();
        enemiesKldTxt.text = "Enemies Killed: " + enemiesKilled.ToString();
    }


}
