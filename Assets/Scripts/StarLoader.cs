using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string score = SaveManager.LoadLevelScore(this.gameObject.name);
        
        foreach(Text textChild in GetComponentsInChildren<Text>())
        {
            if (textChild.gameObject.name == "Stars")
                textChild.text = score;
        }

    }
}
