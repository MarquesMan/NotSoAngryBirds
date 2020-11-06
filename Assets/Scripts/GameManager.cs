using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    private int enemyCount = 0, 
                totalEnemyCount = 0;
    public int birdCount = 5;
    private int birdsAlive = 0;
    public Text birdCounterText;
    public Text enemyCounterText;

    void Awake()
    {
        EventManager.StartListening("InsertEnemy", InsertEnemy);
        EventManager.StartListening("BirdFired", BirdFired);
        EventManager.StartListening("BirdKilled", BirdKilled);
        EventManager.StartListening("RemoveEnemy", RemoveEnemy);
    }

    // Start is called before the first frame update
    void Start()
    {
        birdsAlive = birdCount;
        RedrawHud();
    }

    void OnDisabled()
    {
        EventManager.StopListening("InsertEnemy", InsertEnemy);
        EventManager.StopListening("BirdFired", BirdFired);
        EventManager.StopListening("BirdKilled", BirdKilled);
        EventManager.StopListening("RemoveEnemy", RemoveEnemy);
    }

    private void BirdKilled()
    {
        birdsAlive -= 1;
        CheckGameState();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Rigidbody2D>().isKinematic)
            Destroy(other.gameObject);
    }

    void CheckGameState()
    {

        // Checa se o numero de passarinhos acabou
        if (birdsAlive <= 0)
        {
            if ( enemyCount > 0)
            {
                SaveManager.SaveLevelScore(SceneManager.GetActiveScene().name, ( 1.0f - (enemyCount*1.0f)/totalEnemyCount) );
                SaveManager.SaveGame();
                EventManager.TriggerEvent("GameOver");
            }
        }
        

        if (enemyCount <= 0)
        {
            SaveManager.SaveLevelScore(SceneManager.GetActiveScene().name, 1.0f);
            SaveManager.SaveGame();

            EventManager.TriggerEvent("GameWon");
            Debug.Log("GameWon");
        }

        RedrawHud();
    }

    void BirdFired()
    {
        birdCount -= 1;

        if (birdCount <= 0)
            EventManager.TriggerEvent("NoMoreBirds");

        RedrawHud();
        
    }

    void RemoveEnemy()
    {
        enemyCount -= 1;
        CheckGameState();
    }

    void InsertEnemy()
    {
        enemyCount += 1;
        totalEnemyCount += 1;
        RedrawHud();
    }

    void RedrawHud()
    {
        if (birdCounterText)
            birdCounterText.text = "x " + birdCount.ToString();

        if (enemyCounterText)
            enemyCounterText.text = enemyCount.ToString() + " x";
        
    }

}
