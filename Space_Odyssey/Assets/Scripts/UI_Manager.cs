using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = System.Numerics.Vector3;


public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    [Header("----- Game -----")]
    [SerializeField] private GameObject sun_intro;
    [SerializeField] private GameObject sun;

    [Header("----- Panels -----")]
    public GameObject gameover_panel;
    [SerializeField] private GameObject main_menu_game;
    [SerializeField] private GameObject options_game;

    [Header("----- Text -----")]
    public TMP_Text score_txt;
    [SerializeField] private TMP_Text ranking_txt;
    public List<Score> ranking_list = new List<Score>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Game_Manager.instance.Ranking_List();
        ranking_txt.text = Game_Manager.instance.scorelist_TMP.ToString();
    }

    public void Sun_Intro()
    {
        sun_intro.SetActive(false);
        sun.SetActive(true);
        Game_Manager.instance.StartGame();
    }

    public void ChangeSceneBtn()
    {
        StartCoroutine(ChangeScene());
    }
    
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.5f);
        if (SceneManager.GetActiveScene().name.Equals("Main_Menu"))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene(1);
        sun_intro.SetActive(false);
        Game_Manager.instance.Go_Game();
    }

    public void Game_Menu_Open()
    {
        main_menu_game.SetActive(true);
        //options_game.SetActive(false);
        Game_Manager.instance.Pause_Game();
    }
    
    public void Game_Options_Open()
    {
        main_menu_game.SetActive(false);
        options_game.SetActive(true);
        Game_Manager.instance.Pause_Game();
    }
    
    public void Game_Menu_Out()
    {
        main_menu_game.SetActive(false);
        //options_game.SetActive(false);
        Game_Manager.instance.Go_Game();
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }
}
