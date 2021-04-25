using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Slider DepthIndicator;
    [SerializeField]
    private TextMeshProUGUI DepthText;
    [SerializeField]
    private TextMeshProUGUI coinText;
    public int coins;

    void Start()
    {

    }

    void Update()
    {
        DepthIndicator.value = Mathf.Abs(player.position.y);
        DepthText.text = (Mathf.Round(Mathf.Abs(player.position.y)*100)/100).ToString("0.00") + "m";

        coinText.text = coins.ToString();
    }
}
