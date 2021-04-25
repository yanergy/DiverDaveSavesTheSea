using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private CreatureSpawner spawner;
    [SerializeField]
    private UIController controller;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private bool shopIsOpen;
    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField]
    private int startPrice1;
    [SerializeField]
    private int rebirthPrice;
    private int price1;

    private void Update()
    {
        price1 = startPrice1 * (spawner.currentArea + 1);
        priceText.text = "$" + price1.ToString();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!shopIsOpen)
            {
                canvasGroup.alpha = 1;
                shopIsOpen = true;
            }
            else if (shopIsOpen)
            {
                canvasGroup.alpha = 0;
                shopIsOpen = false;
            }
        }
    }

    public void UpgradeDepth()
    {
        if (controller.coins >= price1 && spawner.currentArea < spawner.Creatures.Length - 1)
        {
            spawner.currentArea++;
            controller.coins -= price1;
        }
    }

    public void Rebirth()
    {
        if (controller.coins >= rebirthPrice)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void BackToMenu() {
            SceneManager.LoadScene(0);
    }
}
