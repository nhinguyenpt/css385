using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public static void SaveState (Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.save";
        FileStream fs = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(fs, data);
        fs.Close();
    }

    public static PlayerData LoadState()
    {
        string path = Application.persistentDataPath + "/player.save";

        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream (path, FileMode.Open);

        PlayerData data = formatter.Deserialize(fs) as PlayerData;
        fs.Close ();

        return data;
    }

}
