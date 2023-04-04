using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public string player_name;
    [SerializeField] private TMP_Text score;
    private int meteorite_count;

    [Header("----- Web Request -----")]
    public List<Score> scorelist_TMP = new List<Score>();
    public Scores score_data;
    public GameObject content_score_list;
    public Item_score_list item_score_list;

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
        if (SceneManager.GetActiveScene().Equals(1))
        {
            faster = true;
            ChangeTextSpeed();
        }
    }

    public void StartGame()
    {
        player_name = $"Player";
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

    #region WebRequest
    
    public void Create_ScoreList()
    {
        scorelist_TMP = new List<Score>();
           
        Score new_score = new Score();
        new_score.player_name = player_name;
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

    public void Refresh_Score_List()
    {
        /// Netejar la llista primer per a no duplicar la informació
        Clean_Teams_List();
        Debug.Log("clean");
        /// Recorrem la llista de Json per a recollir la informació
        /// No la temporal ja que la informació no seria correcte
        foreach (Score score in score_data.scorelist)
        {
            Debug.Log("score");
            Item_score_list _item_teams_list;
            _item_teams_list = Instantiate(item_score_list, content_score_list.transform);
            _item_teams_list.player_name = score.player_name;
            _item_teams_list.score = score.score;
        }
    }
    

    /// Destrueix els elements de la llista
    public void Clean_Teams_List()
    {
        foreach (Transform child in content_score_list.transform)
        {
            Destroy(child.gameObject);
        }
    }

    #endregion
    
    public void Highscore()
    {
        PlayerPrefs.SetInt("Score", meteorite_count);
        
        if (PlayerPrefs.HasKey("Highscore"))
        {
            if (meteorite_count >= PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", meteorite_count);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Score", meteorite_count);
        }
    }
}
