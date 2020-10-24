using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class PlayerBinarySave : MonoBehaviour
{
    public static void SavePlayerData(Player player)
    {
        // Reference to a binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        // Location to save
        string path = Application.dataPath + "/playerSave.savy";
        // Create file at file path
        FileStream stream = new FileStream(path, FileMode.Create);

        //convert to a class we can actually save
        PlayerData data = new PlayerData(player);

        // Serialize player and save it to file
        formatter.Serialize(stream, data);
        // Close the file
        stream.Close();

    }

    public static PlayerData LoadPlayerData()
    {
        // Location to load from
        string path = Application.dataPath + "/playerSave.savy";
        // if we have a file at that path
        if(File.Exists(path))
        {
            // Get the binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            // Read the data from the path
            FileStream stream = new FileStream(path, FileMode.Open);
            // Deserialize back to usable variable
            PlayerData data = (PlayerData)formatter.Deserialize(stream);// as Player;


            // Close the file
            stream.Close();

            return data;
        }
        return null;
    }

}
