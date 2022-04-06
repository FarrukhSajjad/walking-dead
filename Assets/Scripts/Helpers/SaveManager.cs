using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveManager
 {
     public static void Save<T>(string saveName, T saveData)
     {
        var jsonString = JsonConvert.SerializeObject(saveData);
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(jsonString);
        var base64String = Convert.ToBase64String(bytesToEncode);
            
        var path = $"{Application.persistentDataPath}/{saveName}.save";
        var file = new FileStream(path, FileMode.Create);
        var formatter = new BinaryFormatter();
        formatter.Serialize(file, base64String);
        file.Close();
      }

      public static T Load<T>(string saveName)
      {
         var path = $"{Application.persistentDataPath}/{saveName}.save";
         if (!File.Exists(path))
         {
             return default;
         }
            
         var formatter = new BinaryFormatter();
         var file = File.Open(path, FileMode.Open);
         try
         {
            var base64String = (string) formatter.Deserialize(file);
            byte[] bytesToDecode = Convert.FromBase64String(base64String);
            var jsonString = Encoding.UTF8.GetString(bytesToDecode);
            var saveData = JsonConvert.DeserializeObject<T>(jsonString);
            file.Close();
            return saveData;
         }
         catch
         {
             file.Close();
             return default;
         }
    }
}