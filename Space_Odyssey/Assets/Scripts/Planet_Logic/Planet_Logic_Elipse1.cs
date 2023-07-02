using System.Collections;
using UnityEngine;

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
    [SerializeField] private int scene_num;
    [SerializeField] private ParticleSystem explotion_particle;

    [Header("----- Music Variables -----")] 
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip touch;
    [SerializeField] AudioClip speed;

    private void Start()
    {
        angularSpeed = 0.5f;
        lives = 10;
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
