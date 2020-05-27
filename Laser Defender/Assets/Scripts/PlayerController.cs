using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 10.0f;
    public float padding = .5f;
    float xmin;
    float xmax;
    public float projectilespeed = 5f;
    public GameObject Laser;
    public float firingRate;
    public float Health = 300f;
    public AudioClip firing;



	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x+padding;
        xmax = rightmost.x-padding;

    }
    void FireMethod()
    {
        Vector3 buffer = transform.position + new Vector3(0, .75f, 0);
        GameObject beam = Instantiate(Laser, buffer, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectilespeed, 0);
        AudioSource.PlayClipAtPoint(firing, transform.position);
    }
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("FireMethod", 0.00001f, firingRate);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("FireMethod");
        }
		if(Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-speed *Time.deltaTime, 0, 0);

            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(speed *Time.deltaTime, 0, 0);

            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        //restricts the player to the gamespace
        float newx = Mathf.Clamp(transform.position.x,xmin,xmax);
        transform.position = new Vector3(newx, transform.position.y, transform.position.z);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectiles missle = collider.gameObject.GetComponent<Projectiles>();
        if (missle)
        {

            Health -= missle.GetDamage();
            missle.Hit();
            if (Health <= 0)
            {
                Die();
            }

        }
      
    }

    void Die()
    {
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadLevel("End Menu");
        Destroy(gameObject);
    }
}
