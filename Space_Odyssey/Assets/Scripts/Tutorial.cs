using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject panel3;
    [SerializeField] private GameObject panel4;

    public void OpenPanel2()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
    
    public void OpenPanel3()
    {
        panel2.SetActive(false);
        panel3.SetActive(true);
    }
    
    public void OpenPanel4()
    {
        panel3.SetActive(false);
        panel4.SetActive(true);
    }
    
    public void OpenGameScene()
    {
        panel4.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
