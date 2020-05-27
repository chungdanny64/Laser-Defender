using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 6f;
    private bool movingRight = true;
    private float xmax;
    private float xmin;
    public float spawnDelay = 0.5f;

    // Use this for initialization
    void Start()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftedge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightedge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xmax = rightedge.x;
        xmin = leftedge.x;
        SpawnEnemies();
       
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0);
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        double RightEdgeOfFormation = transform.position.x + (0.5 * width);
        double LeftEdgeOfFormation = transform.position.x - (0.5 * width);
        if (LeftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }
        else if (RightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }
        if (AllMembersDead())
        {
            Debug.Log("Empty Formation");
            SpawnUntilFull();
        }
    }
    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform) //checks to see if there is one at the position
        {
            if (childPositionGameObject.childCount == 0) //if it is equal to 0 then it means that there are none left
            {
                return childPositionGameObject;
            }
        }
        return null; // there are still some in the position
    }
    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount >0)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
        
    }
    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition) //if the count is equal to 0 in the positions it sapwns more 
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if(NextFreePosition()) // makes sure that it doesnt keep spawning if the positions arent full 
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
        

    }
}
