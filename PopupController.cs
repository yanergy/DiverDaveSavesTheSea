using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupController : MonoBehaviour
{
    public bool add;
    public int coins;
    void Start()
    {
        if (add == true)
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "+" + coins.ToString();
            GameObject.FindGameObjectWithTag("CoinsText").GetComponent<UIController>().coins += coins;
        }
        else
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "-" + coins.ToString();
            GameObject.FindGameObjectWithTag("CoinsText").GetComponent<UIController>().coins -= coins;
        }
        Destroy(gameObject, 1);
    }
}
