using System.Collections;
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
    [SerializeField] private int scene_num;
    [SerializeField] private ParticleSystem explotion_particle;

    [Header("----- Music Variables -----")] 
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip touch;
    [SerializeField] AudioClip speed_s;

    private void Start()
    {
        speed = 30;
        lives = 10;
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
        explotion_particle.Play();
        Sound_Manager.instance.PlaySoundEffect(touch);
        PlanetLifeCheck();
        
        if (col.GetComponent<CircleCollider2D>().CompareTag("Elipse") || col.GetComponent<CircleCollider2D>().CompareTag("Round"))
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
        if (lives == 6)
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("1");
        }
        else if (lives == 4)
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("2");
        }
        else if (lives == 0)
        {
            Sound_Manager.instance.PlaySoundEffect(explosion);
            WebRequest_Scores.Instance.Leer_JSON_Score_Y_Crear_Lista();
            Game_Manager.instance.Highscore();
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        UI_Manager.instance.SceneLoad(scene_num);
    }
}
