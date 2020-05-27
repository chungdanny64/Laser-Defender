using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public float health = 150;
    public GameObject projectile;
    public float projectilespeed = 5;
    public float ShotsPerSecond = 0.5f;
    public int value = 50;
    private ScoreKeeper scorekeeper;
    public AudioClip firingback;
    public AudioClip Death;

    void Start()
    {
        scorekeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
    void Update()
    {
        float probability = Time.deltaTime * ShotsPerSecond;
        if(Random.value < probability)
        {
            Fire();
            
        }
        


    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectiles missle = collider.gameObject.GetComponent<Projectiles>();
        if(missle)
        {
            
            health -= missle.GetDamage();
            missle.Hit();
            if(health <= 0)
            {
                AudioSource.PlayClipAtPoint(Death, transform.position);
                Destroy(gameObject);
               
                scorekeeper.Score(value);
            }
            
        }
    }

    void Fire()
    {
       // Vector3 startPosition = transform.position + new Vector3(0, -.75f, 0); dont need offset because we used the new matrix setting in unity 
        GameObject enemyBeam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        enemyBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectilespeed, 0);
        AudioSource.PlayClipAtPoint(firingback, transform.position);
    }

   

}
