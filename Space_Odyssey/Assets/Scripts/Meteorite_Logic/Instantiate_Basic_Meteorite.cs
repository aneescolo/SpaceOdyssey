using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Instantiate_Basic_Meteorite : MonoBehaviour
{
    [Header("----- Spawn Variables -----")]
    private float spawnTimer = 0.1f;
    private float timer = 0f;
    private int waveNumber = 0;
    private int checkpoint;

    [Header("----- Meteorite -----")] 
    [SerializeField] private GameObject prefab_Meteorite;

    private void Start()
    {
        prefab_Meteorite.GetComponent<Meteorite_Logic>().value = 1;
    }

    void Update ()
    {
        timer += Time.deltaTime;
        if(timer >= spawnTimer)
        {
            Spawner();
        }
    }
 
    void Spawner()
    {
        //Switches are cleaner than using many ifs when checking one variable.
        switch(waveNumber)
        {
            case 0://If waveNumber == 0
                checkpoint = 10;
                prefab_Meteorite.GetComponent<Meteorite_Logic>().speed = 3;
                //Spawn first wave
                Instantiate(prefab_Meteorite, gameObject.transform);
                spawnTimer = Random.Range(7f, 10f);
                break;//End switch
            case 1:
                checkpoint = 18;
                prefab_Meteorite.GetComponent<Meteorite_Logic>().speed = 4;
                //Spawn second wave
                Instantiate(prefab_Meteorite, gameObject.transform);
                spawnTimer = Random.Range(5f, 7f);
                break;
            case 2:
                checkpoint = 25;
                prefab_Meteorite.GetComponent<Meteorite_Logic>().speed = 4;
                Instantiate(prefab_Meteorite, gameObject.transform);
                spawnTimer = Random.Range(5f, 7f);
                break;
            case 3:
                checkpoint = 32;
                prefab_Meteorite.GetComponent<Meteorite_Logic>().speed = 4;
                Instantiate(prefab_Meteorite, gameObject.transform);
                spawnTimer = Random.Range(4f, 5f);
                break;
            case 4:
                checkpoint = 45;
                prefab_Meteorite.GetComponent<Meteorite_Logic>().speed = 5;
                Instantiate(prefab_Meteorite, gameObject.transform);
                spawnTimer = Random.Range(4f, 5f);
                break;
            case 5:
                checkpoint = 55;
                prefab_Meteorite.GetComponent<Meteorite_Logic>().speed = 5;
                Instantiate(prefab_Meteorite, gameObject.transform);
                spawnTimer = Random.Range(5f, 7f);
                break;
            case 6:
                prefab_Meteorite.GetComponent<Meteorite_Logic>().speed = 6;
                Instantiate(prefab_Meteorite, gameObject.transform);
                spawnTimer = Random.Range(3f, 5f);
                break;
        }

        if (Game_Manager.instance.meteoriteCount >= checkpoint && waveNumber != 6)
        {
            ++waveNumber; //Increment by 1. Same as:  waveNumber = waveNumber + 1;
        }

        timer = 0f;
    }
}
