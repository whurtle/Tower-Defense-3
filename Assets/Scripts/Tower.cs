using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
  public List<Enemy> currentEnemies;
  public Enemy currentTarget;

  public GameObject turret;
  private LineRenderer lineRenderer;

  void Start()
  {
        this.turret = this.transform.GetChild(0).gameObject;
        lineRenderer = GetComponent<LineRenderer>();
        currentEnemies = new List<Enemy>();
  }

  void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
  }

  void Update()
  {
    if (currentTarget)
    {
      lineRenderer.SetPosition(0, turret.transform.position);
      lineRenderer.SetPosition(1, currentTarget.transform.position);
    }
  }

  void OnTriggerEnter(Collider collider)
  {
        if(collider.gameObject.tag == "Enemy")
        {
            Enemy newEnemy = collider.GetComponent<Enemy>();

            currentEnemies.Add(newEnemy);

            newEnemy.DeathEvent.AddListener(delegate { BookKeeping(newEnemy); });

            EvaluateTarget(newEnemy);
        }
  }

  void OnTriggerExit(Collider collider)
  {
    Enemy enemyLeaving = collider.GetComponent<Enemy>();

    //enemyLeaving.DeathEvent.RemoveListener(delegate { BookKeeping(enemyLeaving);}); //unsubscribing to the DeathEvent for this enemy .... don't care anymore :(

    currentEnemies.Remove(enemyLeaving);  // clean up book
    EvaluateTarget(enemyLeaving);

  }

  private void BookKeeping(Enemy enemy)
  {
    currentEnemies.Remove(enemy);
    EvaluateTarget(enemy);

  }


  private void EvaluateTarget(Enemy enemy)
  {
    if (currentTarget == enemy)
    {
      currentTarget = null;
      lineRenderer.enabled = false;
    }


    if (currentTarget == null)
    {
      currentTarget = currentEnemies[0];
      lineRenderer.enabled = true;
    }
  }
}
