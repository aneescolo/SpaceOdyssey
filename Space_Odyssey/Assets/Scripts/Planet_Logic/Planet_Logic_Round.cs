using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Planet_Logic_Round : MonoBehaviour
{
    [Header("----- Orbit -----")]
    [SerializeField] private GameObject sun;
    public float speed;
    [SerializeField] private Vector3 direction;
    private float timer;

    [Header("----- Attributes Variables -----")] 
    public int lives;
    [SerializeField] private int scene_num;
    
    [Header("----- Music Variables -----")] 
    [SerializeField] AudioClip explosion;
    [SerializeField] private VideoClip gameover_clip;
    public GameObject video_player;
    public GameObject porfile_panel;

    private void Start()
    {
        speed = 30;
        lives = 5;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        transform.RotateAround(sun.transform.position, direction, speed*Time.deltaTime);
    }

    private void OnMouseDown()
    {
        Game_Manager.instance.planet = gameObject;
        Game_Manager.instance.SpeedPlanetChange();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlanetLifeCheck();
        
        if (col.GetComponent<CircleCollider2D>().CompareTag("Elipse"))
        {
            if (timer >= 1)
            {
                lives -= 1;
                timer = 0;
            }
        }
        
        PlanetLifeCheck();
    }
    
    private void PlanetLifeCheck()
    {
        if (lives == 4)
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("1");
        }
        else if (lives == 2)
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("2");
        }
        else if (lives == 0)
        {
            Sound_Manager.instance.PlaySoundEffect(explosion);
            WebRequest_Scores.Instance.Leer_JSON_Score_Y_Crear_Lista();
            Game_Manager.instance.Highscore();
            UI_Manager.instance.SceneLoad(scene_num);
        }
    }
}
