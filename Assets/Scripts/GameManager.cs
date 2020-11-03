using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    private int enemyCount = 0;
    public int birdCount = 5;
    public Text birdCounterText;
    public Text enemyCounterText;

    void Awake()
    {
        EventManager.StartListening("InsertEnemy", InsertEnemy);
        EventManager.StartListening("BirdFired", BirdFired);
        EventManager.StartListening("RemoveEnemy", RemoveEnemy);
    }

    // Start is called before the first frame update
    void Start()
    {
        RedrawHud();
    }

    void OnDisabled()
    {
        EventManager.StopListening("InsertEnemy", InsertEnemy);
        EventManager.StopListening("BirdFired", BirdFired);
        EventManager.StopListening("RemoveEnemy", RemoveEnemy);
    }

    void CheckGameState()
    {

        // Checa se o numero de passarinhos acabou
        if (birdCount <= 0)
        {
            if ( enemyCount > 0)
            {
                EventManager.TriggerEvent("GameOver");
            }
        }
        

        if (enemyCount <= 0)
        {
            EventManager.TriggerEvent("GameWon");
            Debug.Log("GameWon");
        }

        RedrawHud();
    }

    void BirdFired()
    {
        birdCount -= 1;
        CheckGameState();
    }

    void RemoveEnemy()
    {
        enemyCount -= 1;
        CheckGameState();
    }

    void InsertEnemy()
    {
        enemyCount += 1;
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
