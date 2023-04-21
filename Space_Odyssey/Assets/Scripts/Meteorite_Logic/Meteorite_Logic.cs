using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meteorite_Logic : MonoBehaviour
{
    [Header("----- Orbit -----")] 
    
    [SerializeField] private Vector3 direction;
    [SerializeField] private GameObject rotationCenter;
    public float speed;
    private float timer;

    public int value;

    private float angle;
    private int force_impact;

    [SerializeField] AudioClip touch;
    [SerializeField] AudioClip point;
    
    [SerializeField] private ParticleSystem explotion_particle;

    private void Start()
    {
        rotationCenter = GameObject.Find("Sun");

        angle = Random.Range(50, 75);
        
        if (gameObject.name.Equals("basic_meteorite") || gameObject.name.Equals("ice_meteorite"))
        {
            force_impact = 1;
        }
        else if (gameObject.name.Equals("green_meteorite"))
        {
            force_impact = 3;
        }
    }
    
    void Update()
    {
        Vector3 direction = rotationCenter.transform.position - transform.position;
        direction = Quaternion.Euler(0, 0, angle) * direction;
        float distanceThisFrame = speed * Time.deltaTime;
    
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        explotion_particle.Play();
        
        if (col.GetComponent<CircleCollider2D>().CompareTag("Sun"))
        {
            Sound_Manager.instance.PlaySoundEffect(point);
            Game_Manager.instance.AddMeteoriteCount(value);
            StartCoroutine(Destroy());
        }
        else if (col.GetComponent<Planet_Logic_Round>())
        {
            Sound_Manager.instance.PlaySoundEffect(touch);
            col.GetComponent<Planet_Logic_Round>().lives -= force_impact;
            StartCoroutine(Destroy());
        }
        else if (GetComponent<Planet_Logic_Elipse1>())
        {
            Sound_Manager.instance.PlaySoundEffect(touch);
            col.GetComponent<Planet_Logic_Elipse1>().lives -= force_impact;
            StartCoroutine(Destroy());
        }
        else if (col.GetComponent<Planet_Logic_Elipse2>())
        {
            Sound_Manager.instance.PlaySoundEffect(touch);
            col.GetComponent<Planet_Logic_Elipse2>().lives -= force_impact;
            StartCoroutine(Destroy());
        }
        else
        {
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
