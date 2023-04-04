using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Planet_Logic_Elipse1 : MonoBehaviour
{
    [Header("----- Orbit -----")] 
    private float timer;
    [SerializeField] private Transform rotationCenter;

    private float rotationRadius = 6f;
    public float angularSpeed = 2f;
    
    private float posX, posY, angle;
    
    [Header("----- Attributes Variables -----")] 
    public int lives;
    
    [Header("----- Music Variables -----")] 
    [SerializeField] AudioClip touch;
    [SerializeField] AudioClip explosion;
    [SerializeField] private VideoClip gameover_clip;
    public GameObject video_player;

    private void Start()
    {
        angularSpeed = 0.5f;
        lives = 6;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        posX = rotationCenter.position.x + Mathf.Cos (angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin (angle) * rotationRadius/1.5f;
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed;
    }

    private void OnMouseDown()
    {
        Game_Manager.instance.planet = gameObject;
        Game_Manager.instance.SpeedPlanetChange();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlanetLifeCheck();
        
        if (col.GetComponent<CircleCollider2D>().CompareTag("Round"))
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
            Sound_Manager.instance.PlaySoundEffect(touch);
            gameObject.GetComponentInChildren<Animator>().SetTrigger("1");
        }
        else if (lives == 2)
        {
            Sound_Manager.instance.PlaySoundEffect(touch);
            gameObject.GetComponentInChildren<Animator>().SetTrigger("2");
        }
        else if (lives == 0)
        {
            Sound_Manager.instance.PlaySoundEffect(explosion);
            video_player.SetActive(true);
            StartCoroutine(Game_Over());
            video_player.GetComponent<VideoPlayer>().clip = gameover_clip;
            Game_Manager.instance.GameOver();
        }
    }
    
    IEnumerator Game_Over()
    {
        yield return new WaitForSeconds(0.5f);
        video_player.SetActive(false);
    }
}
