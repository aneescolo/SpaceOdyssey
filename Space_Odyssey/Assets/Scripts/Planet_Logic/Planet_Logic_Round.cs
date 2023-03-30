using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Planet_Logic_Round : MonoBehaviour
{
    [Header("----- Orbit -----")]
    [SerializeField] private GameObject sun;
    public float speed;
    [SerializeField] private Vector3 direction;
    private float timer;

    [Header("----- Attributes Variables -----")] 
    public int lives;

    private void Start()
    {
        speed = 30;
        lives = 6;
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
            //gameObject.GetComponentInChildren<Animator>().SetTrigger("2");
        }
        else if (lives == 2)
        {
            //gameObject.GetComponentInChildren<Animator>().SetTrigger("1");
        }
        else if (lives <= 0)
        {
            //gameObject.GetComponentInChildren<Animator>().SetTrigger("0");
            //Trigger at end animation
            Game_Manager.instance.GameOver();
        }
    }
}
