using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTBattle : MonoBehaviour
{

    public Transform boss;
    public Transform next;
    public Transform[] spawnPoints;
    public float speed;
    public float distance;

    private void Start()
    {
        next = spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    void Update()
    {
        boss.position = Vector3.MoveTowards(boss.position, next.position, speed * Time.deltaTime);

        distance = Vector3.Distance(boss.position, next.position);

        if (distance <= 0.1f)
        {
            Debug.Log("NEXT");
            next = spawnPoints[Random.Range(0, spawnPoints.Length)];
        }


    }
}
