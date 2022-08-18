using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
public static class SaveSystem
{
    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "Continue";
        FileStream stream;
        if (File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Open);
        }
        else
        {
            stream = new FileStream(path, FileMode.Create);
        }
        List<Deck> decks = DeckSelector.Instance.AvailableDecks;
        Save save = new Save(decks);
        formatter.Serialize(stream, save);
        stream.Close();
    }

    public static void LoadGame()
    {
        string path = Application.persistentDataPath + "Continue";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Save save = formatter.Deserialize(stream) as Save;
            DeckSelector.Instance.LoadDecks(save.SaveToList());
            stream.Close();
        }
        else
        {
            Debug.LogWarning("NO SAVE DATA FOUND");
        }
    }

    public static bool SaveExists()
    {
        string path = Application.persistentDataPath + "Continue";
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "Continue";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}