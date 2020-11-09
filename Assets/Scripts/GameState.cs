using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public EnemyManager enemyManager;
    public HomeBase homeBase;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = this.transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshGame()
    {
        homeBase.Restart();
        enemyManager.SpawnWave();
        gameObject.SetActive(false);
    }

    public void ShowRestart() {
        text.text = "Restart";
        gameObject.SetActive(true);
    }
}
