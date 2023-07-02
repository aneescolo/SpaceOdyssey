using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest_Highscore : MonoBehaviour
{
    public static WebRequest_Highscore Instance;
    
    public Highscores highscore_data_webRequest;


    private void Awake()
    {
        Instance = this;
    }

    public void Escribir_Lista_Scores_en_JSON(Highscores highscore_data)
    {
        StartCoroutine(Corrutina_ESCRIBIR_LISTA_SCORES_EN_JSON(highscore_data));
    }

    public void Leer_JSON_Score_Y_Mostrar_Lista()
    {
        StartCoroutine(Corrutina_LEER_JSON_SCORES_Y_MOSTRAR_LISTA());
    }
    
    public void Leer_JSON_Score_Y_Crear_Lista(TMP_Text txt)
    {
        StartCoroutine(Corrutina_LEER_JSON_USUARIOS_Y_CREAR_USUARIO(txt));
    }

    [ContextMenu("Crear Lista SCORES VACIA")]
    public void Crear_Lista_Scores_Vacia()
    {
        StartCoroutine(Corrutina_Crear_Lista_Scores_Vacia(highscore_data_webRequest));
    }
    
    public void Leer_JSON_Score_Y_Editar_Lista(TMP_Text txt)
    {
        StartCoroutine(Corrutina_LEER_JSON_SCORES_Y_CANVIAR_USUARIO(txt));
    }
    
    private IEnumerator Corrutina_LEER_JSON_USUARIOS_Y_CREAR_USUARIO(TMP_Text txt)
    {
        UnityWebRequest web = UnityWebRequest.Get(Constantes.URL_FILES_JSON_APP_eSPORTS + Constantes.NOMBRE_FILE_HIGHSCORES + Constantes.SUFIJO_FILE);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            highscore_data_webRequest = JsonUtility.FromJson<Highscores>(web.downloadHandler.text);
            Game_Manager.instance.highscore_data = highscore_data_webRequest;
            Game_Manager.instance.Add_PlayerName(txt);
        }
        else
        {
            Debug.Log(Constantes.MENSAJE_ERROR);
        }
    }

    private IEnumerator Corrutina_ESCRIBIR_LISTA_SCORES_EN_JSON(Highscores score_data_webRequest)
    {
        WWWForm form = new WWWForm();
        form.AddField(Constantes.PARAM_FILE, Constantes.NOMBRE_FILE_HIGHSCORES);
        form.AddField(Constantes.PARAM_TEXT, JsonUtility.ToJson(score_data_webRequest));
        
        UnityWebRequest web = UnityWebRequest.Post(Constantes.URL_FILE_TO_WRITE, form);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.Log(Constantes.MENSAJE_ERROR);
        }
    }


    /// <summary>
    /// Método para leer los usuarios que están en el JSON para luego mostrar la lista
    /// datos introducidos por la UI
    /// </summary>
    /// <returns></returns>
    private IEnumerator Corrutina_LEER_JSON_SCORES_Y_MOSTRAR_LISTA()
    {
        UnityWebRequest web = UnityWebRequest.Get(Constantes.URL_FILES_JSON_APP_eSPORTS + Constantes.NOMBRE_FILE_HIGHSCORES + Constantes.SUFIJO_FILE);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            highscore_data_webRequest = JsonUtility.FromJson<Highscores>(web.downloadHandler.text);
            Game_Manager.instance.highscore_data = highscore_data_webRequest;
            Game_Manager.instance.Refresh_Score_List();
        }
        else
        {
            Debug.Log(Constantes.MENSAJE_ERROR);
        }
    }

    private IEnumerator Corrutina_Crear_Lista_Scores_Vacia(Highscores score_data_webRequest)
    {
        WWWForm form = new WWWForm();
        form.AddField(Constantes.PARAM_FILE, Constantes.NOMBRE_FILE_HIGHSCORES);
        form.AddField(Constantes.PARAM_TEXT, JsonUtility.ToJson(score_data_webRequest));
        
        UnityWebRequest web = UnityWebRequest.Post(Constantes.URL_FILE_TO_WRITE, form);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.Log(Constantes.MENSAJE_ERROR);
        }
    }

    private IEnumerator Corrutina_LEER_JSON_SCORES_Y_CANVIAR_USUARIO(TMP_Text txt)
    {
        UnityWebRequest web = UnityWebRequest.Get(Constantes.URL_FILES_JSON_APP_eSPORTS + Constantes.NOMBRE_FILE_HIGHSCORES + Constantes.SUFIJO_FILE);
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.ConnectionError && web.result != UnityWebRequest.Result.ProtocolError)
        {
            highscore_data_webRequest = JsonUtility.FromJson<Highscores>(web.downloadHandler.text);
            Game_Manager.instance.highscore_data = highscore_data_webRequest;
            Game_Manager.instance.Check_PlayerName(txt);
        }
        else
        {
            Debug.Log(Constantes.MENSAJE_ERROR);
        }
    }
}
