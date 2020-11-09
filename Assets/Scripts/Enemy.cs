﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
  private Waypoints[] navPoints;
  private Transform target;
  private Vector3 direction;
  public float amplify = 1;
  private int index = 0;
  private bool move = true;
  private Purse purse;
  public int currentHealth = 100;
  private int startingHealth;
  public int cashPoints = 100;
  private HealthBar healthBar;

  public UnityEvent DeathEvent;

    private AudioSource audioSource;
    public AudioClip deathSound;

    // Start is called before the first frame update
    private void Start()
    {
        this.audioSource = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<AudioSource>();
    }

    public void StartEnemy(Waypoints[] navigationalPath)
  {
    navPoints = navigationalPath;
    purse = GameObject.FindGameObjectWithTag("Purse").GetComponent<Purse>();
    healthBar = GetComponentInChildren<HealthBar>();
    startingHealth = currentHealth;
    //Place our enemy at the start point
    transform.position = navPoints[index].transform.position;
    NextWaypoint();
    
    //Move towards the next waypoint
    //Retarget to the following waypoint when we reach our current waypoint
    //Repeat through all of the waypoints until you reach the end
  }

  // Update is called once per frame
  void Update()
  {
    if (move)
    {
      transform.Translate(direction.normalized * Time.deltaTime * amplify);

      if ((transform.position - target.position).magnitude < .1f)
      {
        NextWaypoint();
      }
    }

  }

  private void NextWaypoint()
  {
    if (index < navPoints.Length - 1)
    {
      index += 1;
      target = navPoints[index].transform;
      direction = target.position - transform.position;
    }
    else
    {
      move = false;
    }
  }

  public void TakeDamage(int amountDamage)
  {
        currentHealth -= amountDamage;
        if (currentHealth < 0)
        {
            purse.AddCash(cashPoints); //add cash to purse
            this.audioSource.PlayOneShot(this.deathSound);
            DeathEvent.Invoke();    ///notify towers that I am killed
      
            Destroy(this.gameObject); //Get rid of object
        }
        else
        {
            healthBar.Damage(currentHealth, startingHealth);
        }
  }



}
