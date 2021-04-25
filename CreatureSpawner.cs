using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject[] Creatures;
    public GameObject[] Trash;
    [SerializeField]
    private float maxHorizontalSpawn = 12;
    [SerializeField]
    private float maxCreaturesPerArea = 10;
    [SerializeField]
    private float SickPercentage = 50;
    public float areaOffset = 5;
    private float timer;
    public Vector2 areaCoords;
    public int currentArea;

    void Start()
    {
        for (int i = 0; i < Creatures.Length; i++)
        {
            GameObject obj = new GameObject();
            obj.name = i.ToString();
            obj.transform.parent = transform;
        }
        for (int i = 0; i < maxCreaturesPerArea; i++)
        {

            SpawnInArea(0, 0, 25, 0);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;

            for (int i = 0; i <= currentArea; i++)
            {
                SpawnInArea(i, i, 25, i);
            }
        }
    }

    void SpawnInArea(int CrID1, int CrID2, int TrashPercentage, int AreaID)
    {
        if (transform.GetChild(AreaID).childCount < maxCreaturesPerArea)
        {
            if (Random.Range(0, 100) <= TrashPercentage)
            {
                Transform trash = Spawn(Trash[CrID2], AreaID).transform;
                trash.Rotate(0, 0, Random.Range(0, 360));
            }
            else
            {
                GameObject fish = Spawn(Creatures[CrID1], AreaID);
                bool isSick = (Random.Range(0, 100) > SickPercentage) ? true : false;
                fish.GetComponent<CreatureMove>().isSick = isSick;
            }
        }
    }

    Vector2 AreaIDToCoords(int AreaID)
    {
        return new Vector2((AreaID * -20) - areaOffset, ((AreaID + 1) * -20) - areaOffset);
    }

    GameObject Spawn(GameObject objectToSpawn, int AreaID)
    {
        float randX = Random.Range(-maxHorizontalSpawn, maxHorizontalSpawn);
        float randY = Random.Range(AreaIDToCoords(AreaID).x, AreaIDToCoords(AreaID).y);

        Transform creature = GameObject.Instantiate(objectToSpawn).transform;
        creature.position = new Vector2(randX, randY);
        creature.parent = transform.GetChild(AreaID);

        return creature.gameObject;
    }
}
