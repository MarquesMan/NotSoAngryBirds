using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveManager : MonoBehaviour
{

    // Instancia do SaveManager
    private static SaveManager saveManager;

    private SaveGame saveGameObject;

    // Obter a instancia
    public static SaveManager instance
    {
        get
        {
            if (!saveManager)
            {
                saveManager = FindObjectOfType(typeof(SaveManager)) as SaveManager;

                if (!saveManager)
                {
                    Debug.LogError("There needs to be one active SaveManger script on a GameObject in your scene.");
                }
                else
                {
                    saveManager.Init();
                }
            }

            return saveManager;
        }
    }

    private void Init()
    {
        saveGameObject = LoadGame();
    }

    private static SaveGame LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/savegame.save"))
        {
            Debug.Log("Loading Save Game");
            // Carregue do armazenamento
            // Cria o formatodor de binario
            BinaryFormatter bf = new BinaryFormatter();
            // Carrega o arquivo por meio de stream
            FileStream file = File.Open(Application.persistentDataPath + "/savegame.save", FileMode.Open);
            // Deserializa o arquivo do armazenamento para SaveGame de novo
            SaveGame save = (SaveGame) bf.Deserialize(file);
            file.Close();

            return save;
        }
        else
        {
            // Crie um novo jogo
            Debug.Log("Creating New Game");
            return new SaveGame();
        }
    }

    public static void SaveGame()
    {
        Debug.Log("Saving the Game");
        // Salvar o arquivo no armazenamento
        // Cria o formatodor de binario

        BinaryFormatter bf = new BinaryFormatter();
        // Cria o arquivo por meio de stream
        FileStream file = File.Create(Application.persistentDataPath + "/savegame.save");
        // Serializa o objeto SaveGame
        bf.Serialize(file, instance.saveGameObject);
        // Fecha o arquivo depois de escrito
        file.Close();
    }

    public static string LoadLevelScore(string levelName)
    {
        return instance.saveGameObject.LoadLevelScore(levelName);
    }

    public static void SaveLevelScore(string levelName, float levelScore)
    {
        instance.saveGameObject.SaveLevelScore(levelName, levelScore);
    }
}
