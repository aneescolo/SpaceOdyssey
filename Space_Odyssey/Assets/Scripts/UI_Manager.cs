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
    [SerializeField] private GameObject main_menu_game;
    [SerializeField] private GameObject options_game;
    [SerializeField] private GameObject instruction_1_game;
    [SerializeField] private GameObject instruction_2_game;
    [SerializeField] private GameObject instruction_3_game;
    public GameObject porfile_panel;

    [Header("----- Text -----")]
    public TMP_Text score_txt;
    public TMP_Text highscore_int;
    [SerializeField] private TMP_Text player_name_text;
    [SerializeField] private TMP_Text intro_name_text;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            PlayerPrefs.SetString("PlayerName", "Player");
        }
        PlayerPrefs.SetString("PlayerName", "Player");

        Player_Name_Load();
        Debug.Log(PlayerPrefs.GetString("PlayerName"));
    }

    public void BUTTONREPLACE()
    {
        PlayerPrefs.SetString("PlayerName", "Player");
        Debug.Log(PlayerPrefs.GetString("PlayerName"));
    }

    public void Sun_Intro()
    {
        sun_intro.SetActive(false);

        if (PlayerPrefs.GetString("PlayerName") == "Player")
        {
            instruction_1_game.SetActive(true);
            Game_Manager.instance.Pause_Game();
        }
        else
        {
            sun.SetActive(true);
            Game_Manager.instance.StartGame();
            porfile_panel.SetActive(true);
        }
    }
    
    public void Sun_Intro_2()
    {
        instruction_1_game.SetActive(false); 
        instruction_2_game.SetActive(true); 
    }
    
    public void Sun_Intro_3()
    {
        instruction_2_game.SetActive(false); 
        instruction_3_game.SetActive(true);
    }

    public void StartGameIntro()
    {
        Player_Name_Add();

        Game_Manager.instance.Go_Game();
        sun.SetActive(true);
        instruction_3_game.SetActive(false);
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
        WebRequest_Highscore.Instance.Leer_JSON_Score_Y_Editar_Lista(player_name_text);
    }
    
    public void Player_Name_Add()
    {
        WebRequest_Highscore.Instance.Leer_JSON_Score_Y_Crear_Lista(intro_name_text);
    }
    
    private void Player_Name_Load()
    {
        player_name_text.text = PlayerPrefs.GetString("PlayerName");
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

    public void SceneLoad(int scene_number)
    {
        SceneManager.LoadScene(scene_number);
    }
}
