using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerData
{
    class SaveDataFormat
    {
        public SaveDataFormat()
        {
            
        }

        public SaveDataFormat(SaveDataFormat saveDataFormat)
        {

        }
    }

    public static readonly PlayerData instance = new PlayerData();

    SaveDataFormat saveData;

    #region Save/Load

    private void SaveData()
    {
        if (File.Exists(GetPath()))
        {
            File.Delete(GetPath());
        }

        using (FileStream fs = new FileStream(GetPath(), FileMode.Create, FileAccess.Write))
        {
            string data = JsonConvert.SerializeObject(saveData);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
            fs.Write(bytes, 0, bytes.Length);
        }
    }

    public void NewGameData()
    {
        saveData = new SaveDataFormat();
        SaveData();
    }

    public bool LoadData()
    {
        if (!File.Exists(GetPath()))
        {
            return false;
        }

        using (FileStream fs = new FileStream(GetPath(), FileMode.Open, FileAccess.Read))
        {
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);
                
            string data = System.Text.Encoding.UTF8.GetString(bytes);
            saveData = new SaveDataFormat(JsonConvert.DeserializeObject<SaveDataFormat>(data));
        }

        return true;

        // TODO: 클라우드에서 불러오기
    }

    private static string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "save0");
    }

    #endregion
}