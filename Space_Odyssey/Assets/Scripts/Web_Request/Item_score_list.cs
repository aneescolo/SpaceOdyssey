using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item_score_list : MonoBehaviour
{
    public string player_name;
    public int score;

    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player_name;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{score}";
    }
}
