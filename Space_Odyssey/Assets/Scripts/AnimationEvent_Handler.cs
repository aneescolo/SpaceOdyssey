using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvent_Handler : MonoBehaviour
{
    [SerializeField] private GameObject sun;
    [SerializeField] private GameObject mainmenu_btns;
    [SerializeField] private GameObject options_txt;
    [SerializeField] private GameObject options_out_btn;
    [SerializeField] private GameObject ranking_txt;
    [SerializeField] private GameObject ranking_out_btn;
    [SerializeField] private GameObject game_structure;
    [SerializeField] private GameObject game_profile;

    public void SunTriggerIn()
    {
        sun.GetComponent<Animator>().SetTrigger("In");
    }
    
    public void SunTriggerOut()
    {
        sun.GetComponent<Animator>().SetTrigger("Out");
    }
    
    public void SunTriggerInOptions()
    {
        sun.GetComponent<Animator>().SetTrigger("In_Options");
    }
    
    public void SunTriggerInRanking()
    {
        sun.GetComponent<Animator>().SetTrigger("In_Ranking");
    }

    public void MainMenuBtnTriggerIn()
    {
        mainmenu_btns.GetComponent<Animator>().SetTrigger("In");
    }
    
    public void MainMenuBtnTriggerOutOptions()
    {
        mainmenu_btns.GetComponent<Animator>().SetTrigger("Out_Options");
    }
    
    public void MainMenuBtnTriggerOutRanking()
    {
        mainmenu_btns.GetComponent<Animator>().SetTrigger("Out_Ranking");
    }
    
    public void MainMenuBtnTriggerPlay()
    {
        mainmenu_btns.GetComponent<Animator>().SetTrigger("Play");
    }
    
    public void OptionsTxtTriggerIn()
    {
        options_txt.GetComponent<Animator>().SetTrigger("In");
    }
    
    public void OptionsTxtTriggerOut()
    {
        options_txt.GetComponent<Animator>().SetTrigger("Out");
    }
    
    public void RankingTxtTriggerIn()
    {
        ranking_txt.GetComponent<Animator>().SetTrigger("In");
    }
    
    public void RankingTxtTriggerOut()
    {
        ranking_txt.GetComponent<Animator>().SetTrigger("Out");
    }
    
    public void OptionsOut_BtnTriggerIn()
    {
        options_out_btn.GetComponent<Animator>().SetTrigger("In");
    }
    
    public void OptionsOut_BtnTriggerOut()
    {
        options_out_btn.GetComponent<Animator>().SetTrigger("Out");
    }
    
    public void RankingOut_BtnTriggerIn()
    {
        ranking_out_btn.GetComponent<Animator>().SetTrigger("In");
    }
    
    public void RankingOut_BtnTriggerOut()
    {
        ranking_out_btn.GetComponent<Animator>().SetTrigger("Out");
    }
    
    public void GameScene_TriggerIn()
    {
        game_structure.GetComponent<Animator>().SetTrigger("In");
    }
    
    public void GameScene_TriggerOut()
    {
        game_structure.GetComponent<Animator>().SetTrigger("Out");
    }

    public void SunIntro()
    {
        UI_Manager.instance.Sun_Intro();
    }
    
    public void GameProfileOn()
    {
        game_profile.SetActive(true);
    }
    
    public void GameProfileOff()
    {
        game_profile.SetActive(false);
    }
    
    public void Pause_Game()
    {
        if (SceneManager.GetActiveScene().name.Equals("Game_Scene"))
        {
            Time.timeScale = 0;
        }
    }
    
    public void Go_Game()
    {
        Time.timeScale = 1;
    }
}
