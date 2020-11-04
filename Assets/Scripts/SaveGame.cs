using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class SaveGame
{
    // Chave: Nome da Cena(Nivel) Valor: Score do jogador(string)
    private Dictionary<string, string> levelScore = new Dictionary<string, string>();

    public void SaveLevelScore(string levelName, float levelScoreValue)
    {
        StringBuilder finalScore = new StringBuilder("☆☆☆☆☆");
        
        for (int i = 0; i < (int) (levelScoreValue*5); ++i) 
            finalScore[i] = '★';

        levelScore[levelName] = finalScore.ToString();
    }   

    public string LoadLevelScore(string levelName)
    {
        string starts = "☆☆☆☆☆";

        if (levelScore.ContainsKey(levelName))
            starts = levelScore[levelName];

        return starts;
    }
}
