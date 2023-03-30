using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    
    public GameObject planet;
    
    [SerializeField] private GameObject spawnpoint_basic;
    [SerializeField] private GameObject spawnpoint_ice;
    [SerializeField] private GameObject spawnpoint_green;
    
    [Header("----- Planet Speed -----")]
    [SerializeField] private TMP_Text fast;
    [SerializeField] private TMP_Text slow;
    [SerializeField] private bool faster;
    
    [Header("----- Meteorite -----")]
    [SerializeField] private TMP_Text score;
    private int meteorite_count;
    
    [Header("----- Web Request -----")]
    public List<Score> scorelist_TMP = new List<Score>();
    public Scores score_data;

    public int meteoriteCount
    {
        get { return meteorite_count; }
        set { meteorite_count = value; }
    }


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        faster = true;
        ChangeTextSpeed();
    }

    public void StartGame()
    {
        spawnpoint_basic.SetActive(true);
        spawnpoint_ice.SetActive(true);
        spawnpoint_green.SetActive(true);
    }

    public void Pause_Game()
    {
        Time.timeScale = 0;
    }
    
    public void Go_Game()
    {
        Time.timeScale = 1;
    }

    #region Planet

    private void ChangeTextSpeed()
    {
        if (faster)
        {
            fast.color = Color.red;
            slow.color = Color.white;
        }
        else
        {
            fast.color = Color.white;
            slow.color = Color.red; 
        }
    }

    public void SpeedPlanetChange()
    {
        if (planet.tag.Equals("Round"))
        {
            if (faster && planet.GetComponent<Planet_Logic_Round>().speed != 50)
            {
                planet.GetComponent<Planet_Logic_Round>().speed += 20;
                faster = false;
                ChangeTextSpeed();
            }
            else if (!faster && planet.GetComponent<Planet_Logic_Round>().speed != 10)
            {
                planet.GetComponent<Planet_Logic_Round>().speed -= 20;
                faster = true;
                ChangeTextSpeed();
            }
            
            planet = null;
        }
        else if (planet.tag.Equals("Elipse") && planet.GetComponent<Planet_Logic_Elipse1>())
        {
            if (faster && planet.GetComponent<Planet_Logic_Elipse1>().angularSpeed != 0.8)
            {
                planet.GetComponent<Planet_Logic_Elipse1>().angularSpeed += 0.3f;
                faster = false;
                ChangeTextSpeed();
            }
            else if (!faster && planet.GetComponent<Planet_Logic_Elipse1>().angularSpeed != 0.2)
            {
                planet.GetComponent<Planet_Logic_Elipse1>().angularSpeed -= 0.3f;
                faster = true;
                ChangeTextSpeed();
            }
            
            planet = null;
        }
        else if (planet.tag.Equals("Elipse") && planet.GetComponent<Planet_Logic_Elipse2>())
        {
            if (faster && planet.GetComponent<Planet_Logic_Elipse2>().angularSpeed != 1.5)
            {
                planet.GetComponent<Planet_Logic_Elipse2>().angularSpeed += 0.5f;
                faster = false;
                ChangeTextSpeed();
            }
            else if (!faster && planet.GetComponent<Planet_Logic_Elipse2>().angularSpeed != 0.5)
            {
                planet.GetComponent<Planet_Logic_Elipse2>().angularSpeed -= 0.5f;
                faster = true;
                ChangeTextSpeed();
            }

            planet = null;
        }
    }
    
    #endregion

    #region Meteorite

    public void AddMeteoriteCount(int valuetoadd)
    {
        meteorite_count += valuetoadd;
        score.text = $"{meteorite_count}";
    }

    #endregion

    public void GameOver()
    {
        Pause_Game();
        UI_Manager.instance.gameover_panel.SetActive(true);
        UI_Manager.instance.score_txt.text = $"{meteorite_count}";
        WebRequest_Scores.Instance.Leer_JSON_Score_Y_Crear_Lista();
    }
    
    #region WebRequest
    
    public void Create_ScoreList()
    {
        scorelist_TMP = new List<Score>();
           
        Score new_score = new Score();
        new_score.score = meteorite_count;

        if (score_data.scorelist.Length == 0)
        {
            scorelist_TMP.Add(new_score);
        }
        else
        {
            scorelist_TMP.Add(new_score);
            foreach (Score score in score_data.scorelist)
            {
                scorelist_TMP.Add(score);
            }
        }
        scorelist_TMP = scorelist_TMP.OrderByDescending(o => o.score).ToList();
        if (score_data.scorelist.Length > 10)
        {
            scorelist_TMP.RemoveRange(10, scorelist_TMP.Count - 10);
        }
        score_data.scorelist = scorelist_TMP.ToArray();
        WebRequest_Scores.Instance.Escribir_Lista_Scores_en_JSON(score_data);
    }

    public void Ranking_List()
    {
        foreach (Score score in score_data.scorelist)
        {
            scorelist_TMP.Add(score);
        }
    }

    #endregion
}
