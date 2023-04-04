using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over : MonoBehaviour
{
    [Header("----- Panels -----")]
    [SerializeField] GameObject video;
    [SerializeField] TMP_Text score_txt;
    [SerializeField] TMP_Text highscore_txt;


    private void Start()
    {
        StartCoroutine(Panel_Over());
    }

    IEnumerator Panel_Over()
    {
        yield return new WaitForSeconds(4);
        video.SetActive(false);
        score_txt.text = $"{PlayerPrefs.GetInt("Score")}";
        highscore_txt.text = $"{PlayerPrefs.GetInt("Highscore")}";
    }

    public void Play()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
