﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Group
{
  public GameObject enemyType;
  [Range(0,50)] public int amountOfEnemies;
  [Range(0,5)] public float timeDelayBetweenEnemies;

  public Group(GameObject enemyType, int amountOfEnemies, float timeDelayBetweenEnemies)
  {
    this.enemyType = enemyType;
    this.amountOfEnemies = amountOfEnemies;
    this.timeDelayBetweenEnemies = timeDelayBetweenEnemies;
  }
}

[System.Serializable]
public struct Wave
{
  public Group[] enemyGroups;
}


public class EnemyManager : MonoBehaviour
{
 //Figure out what enemy waves look like and how to handle them (e.g. spawning, how many, wait time in between)
 

 //Pass navigational path
 public Waypoints[] navPoints;
 public Wave enemyWave;
 //public GameObject easyEnemy;
 //public GameObject hardEnemy;


 void Start()
 {
  
  }

 public void SpawnWave()
 {

   foreach (Group group in enemyWave.enemyGroups)
   {
     StartCoroutine(SpawnGroup(group));
     
   }
 }

 IEnumerator SpawnGroup(Group enemyGroup)
 {
   int i = 0;
   while (enemyGroup.amountOfEnemies > 0)
   {
     GameObject spawnedEnemy = Instantiate(enemyGroup.enemyType);
     spawnedEnemy.GetComponent<Enemy>().StartEnemy(navPoints);
     spawnedEnemy.name = $"{enemyGroup.enemyType.ToString()} {i}";
     enemyGroup.amountOfEnemies--;
     i++;
     yield return new WaitForSeconds(enemyGroup.timeDelayBetweenEnemies);
     
   }
   
  }

}
