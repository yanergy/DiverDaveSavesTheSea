using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float maxHorizontalPosition = 12;
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject RangeIndicator;
    [SerializeField]
    private GameObject popupMenu;
    [SerializeField]
    private CreatureSpawner spawner;
    [SerializeField]
    private ParticleSystem swimParticles;


    Rigidbody2D rb;
    Vector2 movePosition;
    Vector2 movement;
    bool isDragging;
    bool mouseDown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mouseDown = Input.GetMouseButton(0);
    }

    void MovePlayer()
    {
        if (mouseDown)
        {
            movement = -(rb.position - (Vector2)cam.ScreenToWorldPoint(Input.mousePosition));
            swimParticles.gameObject.SetActive(true);
        }
        else
        {
            movement = Vector2.zero;
            swimParticles.gameObject.SetActive(false);
        }

        if (movement.sqrMagnitude > 0.01f)
        {
            movePosition = rb.position + movement.normalized * speed;
            movePosition.x = Mathf.Clamp(movePosition.x, -maxHorizontalPosition, maxHorizontalPosition);
            movePosition.y = Mathf.Clamp(movePosition.y, ((spawner.currentArea + 1) * -20) - spawner.areaOffset, 0);
            rb.rotation = RotateTowardsMouse();
        }

        if (movement.x > 0)
            transform.localScale = new Vector2(-1, 1);
        else if (movement.x < 0)
            transform.localScale = new Vector2(1, 1);

        rb.MovePosition(movePosition);
    }

    void FixedUpdate()
    {
        MovePlayer();
        MoveCamera();
    }

    float RotateTowardsMouse()
    {
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;

        return angle;
    }

    void MoveCamera()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(0, movePosition.y, -10), 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fish")
        {
            int minCoins = other.GetComponent<CreatureMove>().minCoins;
            int maxCoins = other.GetComponent<CreatureMove>().maxCoins;
            bool isSick = other.GetComponent<CreatureMove>().isSick;

            GetComponent<AudioSource>().Play();

            Transform popup = Instantiate(popupMenu).transform;
            popup.position = other.transform.position;

            int coins = Random.Range(minCoins, maxCoins + 1);

            popup.GetComponent<PopupController>().coins = coins * (isSick ? 2 : 1);
            popup.GetComponent<PopupController>().add = isSick;

            Destroy(other.gameObject);
        }
        else if (other.tag == "Trash")
        {
            int minCoins = other.GetComponent<TrashScript>().minCoins;
            int maxCoins = other.GetComponent<TrashScript>().maxCoins;
            
            GetComponent<AudioSource>().Play();

            Transform popup = Instantiate(popupMenu).transform;
            popup.position = other.transform.position;

            int coins = Random.Range(minCoins, maxCoins + 1);
            popup.GetComponent<PopupController>().coins = coins;
            popup.GetComponent<PopupController>().add = true;

            Destroy(other.gameObject);
        }
    }
}
