using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //string of the file path
        //"c:/windows/something/player.sav"
        //string path = Application.persistentDataPath + "/player.sav";
        string path = Application.dataPath + "/player.sav";

        //opens the file
        FileStream stream = new FileStream(path, FileMode.Create);

        //creating the data to be saved
        PlayerData data = new PlayerData(player);

        //saving the data to the file
        formatter.Serialize(stream, data);

        //closing the file
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.dataPath + "/player.sav";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
