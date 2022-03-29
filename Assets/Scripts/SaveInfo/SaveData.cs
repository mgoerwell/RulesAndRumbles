using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

//internal storage of save data while game is running
public class SaveData : MonoBehaviour {
    public static int highLevel; //Level Tracker

    //method that retrieves user progress (save Data)
    public static void LoadSave()
    {
        //checks to see if we have an existing save
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            //create a stream reader to deserialize
            BinaryFormatter bf = new BinaryFormatter();
            //create file stream
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            //read save data into correct format
            Save save = (Save)bf.Deserialize(file);
            //close file
            file.Close();

            //apply file data to internal storage
            highLevel = save.HighestUnlockedLevel;

        } else {
            //assume fresh save
            highLevel = 1;
        }
    }

    //method that stores user progress to file
    public static void Save() {
        //create a stream reader to serialize
        BinaryFormatter bf = new BinaryFormatter();
        //create file stream
        FileStream fs = File.Create(Application.persistentDataPath + "/gamesave.save");
        //generate save
        Save save = new Save();
        save.HighestUnlockedLevel = highLevel;
        //store save in file
        bf.Serialize(fs, save);
        fs.Close();
        Debug.Log("Data Saved");
    }

    public static string DeleteSave() {
        string result = "No Save Found.";

        if (File.Exists(Application.persistentDataPath + "/gamesave.save")) {
            File.Delete(Application.persistentDataPath + "/gamesave.save");
            highLevel = 1;
            result = "Save Data Reset!";
        }

        return result;
    }

    private void Awake() {
        LoadSave();
    }

    private void Start() {
        
        SceneManager.LoadScene("Main_Menu");
    }


    //handler for when application ends
    void OnApplicationQuit() {
        Save();
    }
}
