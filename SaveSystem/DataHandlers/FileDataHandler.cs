using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace RedLine.Save
{
    /// <summary>
    /// Class FileDataHandler writes data from class GameData to disk
    /// Класс FileDataHandler записывает данные из класса GameData на диск
    /// </summary>
    public class FileDataHandler
    {
        //путь сохранения на диск
        private static string dataDirPath;
        private static string dataFileName = "game.save";
        private static string path;

        /// <summary>
        /// Enter the file name without the extension.
        /// Вводите название диска без расширения
        /// </summary>
        public string FileName { get { return dataFileName; } set { dataFileName = value + ".save"; } }

        /// <summary>
        /// Функция Save сохраняет данные на диск из полей класса GameData
        /// </summary>
        /// <param name="gameData"></param>
        public void Save(GameData gameData)
        {
            dataDirPath = Application.persistentDataPath + "/save";
            path = Path.Combine(dataDirPath, dataFileName);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(stream, gameData);
                }
            }
            catch(Exception e)
            {
                Debug.LogError($"Error occured when trying to save data to file: {path} \n {e}");
            }
        }

        /// <summary>
        /// Функция Load загружает данные из диска в поля класса GameData
        /// </summary>
        /// <returns></returns>
        public GameData Load()
        {
            dataDirPath = Application.persistentDataPath + "/save";
            path = Path.Combine(dataDirPath, dataFileName);

            if (File.Exists(path))
            {
                try
                {
                    GameData gameData = null;
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                       gameData = formatter.Deserialize(stream) as GameData;
                    }

                    return gameData;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error occured when trying to load data from file: {path} \n {e}");
                    return null;
                }
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }
    }
}
