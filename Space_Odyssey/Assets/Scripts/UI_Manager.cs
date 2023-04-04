using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject porfile_panel;

    [Header("----- Text -----")]
    public TMP_Text score_txt;
    public TMP_Text highscore_int;
    [SerializeField] private TMP_Text player_name_text;
    
    private void Awake()
    {
        instance = this;
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

    public void Player_Name_Save()
    {
        Game_Manager.instance.player_name = player_name_text.text;
    }
    
    public void Player_Name_Load()
    {
        player_name_text.text = Game_Manager.instance.player_name;
    }
    
    public void Game_Menu_Out()
    {
        main_menu_game.SetActive(false);
        options_game.SetActive(false);
        Game_Manager.instance.Go_Game();
    }

    public void OpenOptionsMenu()
    {
        main_menu_game.SetActive(false);
        options_game.SetActive(true);
    }
    
    public void CloseOptionsMenu()
    {
        main_menu_game.SetActive(true);
        options_game.SetActive(false);
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }
}
