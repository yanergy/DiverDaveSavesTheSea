using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMove : MonoBehaviour
{
    public int minCoins;
    public int maxCoins;
    private float maxHorizontal = 12;
    private float speed = 0.1f;
    private float movement = 1;
    public bool isSick = false;

    void Start()
    {
        speed = Random.Range(0.05f, 0.15f);
        movement = (Random.Range(0, 100) > 50) ? -1 : 1;
    }

    void Update()
    {
        if (isSick)
        {
            GetComponent<SpriteRenderer>().color = new Color32(136, 255, 150, 255);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
    }

    void FixedUpdate()
    {
        if (transform.position.x > maxHorizontal) movement = -1;
        if (transform.position.x < -maxHorizontal) movement = 1;

        transform.position += Vector3.right * speed * movement;
        transform.localScale = new Vector3(movement, 1, 1);
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
