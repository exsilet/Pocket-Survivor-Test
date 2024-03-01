using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CodeBase.Hero;
using CodeBase.Infrastructure.StaticData.Item;
using CodeBase.UI.Form;
using UnityEngine;

namespace CodeBase.SaveData
{
    public static class BinarySavingSystem
    {
        public static void SavePlayer(HeroHealth hero, ViewInventory viewInventory, EquipmentForThePlayer equipPlayer)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.bat";
            FileStream stream = new FileStream(path, FileMode.Create);

            DataBase dataBase = new DataBase(hero, viewInventory, equipPlayer);
        
            formatter.Serialize(stream, dataBase);
            stream.Close();
        }
    
        public static DataBase LoadPlayer()
        {
            string path = Application.persistentDataPath + "/player.bat";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                DataBase dataBase = formatter.Deserialize(stream) as DataBase;
                stream.Close();
            
                return dataBase;
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }
    }
}