using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
   public static void Save(levelStorage input)
   {
       BinaryFormatter coder=new BinaryFormatter();
       string path=Application.persistentDataPath+"/hem.ninja";
       FileStream file=new FileStream(path,FileMode.Create);
       levelData data=new levelData(input);
       coder.Serialize(file,data);
       file.Close();
   }
   public static levelData StoredItem()
   {
       string path=Application.persistentDataPath+"/hem.ninja";
       if(File.Exists(path))
       {
           BinaryFormatter decoder=new BinaryFormatter();
           FileStream file=new FileStream(path, FileMode.Open);
           levelData data=decoder.Deserialize(file) as levelData;
           file.Close();
           return data;
       }
       else
       {
           return null;
       }
   }
}
