using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;

namespace RedLine.Save
{
    /// <summary>
    /// Класс SaveSystem - ядро системы сохранения RedLine. Он отвечает за миграцию данных из класса GameData в классы реализующие
    /// интерфейс IDataPersistence и обратно. А также контролирует, когда происходит сохранение и загрузка данных.
    /// </summary>
    public class SaveSystem : MonoBehaviour
    {
        //Хранит единственный экземпляр класса GameData
        private GameData _gameData;
        //Хранит все реализации интерфейса IDataPersistence
        private List<IDataPersistence> _dataPersistenceObjects;
        //Хранит единственный экземпляр класса FileDataHandler
        private FileDataHandler _dataHandler = new FileDataHandler();

        //Проверка, что это единстввеный экземпляр класса
        public static SaveSystem instance { get; private set; }
        private void Awake()
        {
            if (instance != null)
                Debug.LogError("Found more than one Save System in the scene.");
            instance = this;
            _dataPersistenceObjects = FindAllDataPersistenceObjects();
        }

        //По сути лишняя функция
        public void NewGame()
        {
            LoadGame();
            SaveGame();
        }

        /// <summary>
        /// Функция LoadGame вызывает метод загрузки данных в поля экземпляра класса GameData(если такого сохранения нет, создает новый объект типа GameData)
        /// и реализацию метода LoadData из интерфеса IDataPersistence
        /// </summary>
        public void LoadGame()
        {
            _gameData = _dataHandler.Load();

            if (_gameData == null)
            {
                Debug.Log("No data was found.");
                _gameData = new GameData();
            }

            foreach (IDataPersistence dataPersistence in _dataPersistenceObjects)
                dataPersistence.LoadData(_gameData);

        }
        /// <summary>
        /// Функция SaveGame вызывает метод созранения данных из полей экземпляра класса GameData
        /// и реализацию метода SaveData из интерфеса IDataPersistence
        /// </summary>
        public void SaveGame()
        {
            foreach (IDataPersistence dataPersistence in _dataPersistenceObjects)
                dataPersistence.SaveData(ref _gameData);

            _dataHandler.Save(_gameData);
        }


        // Работает только с включенными объектами на сцене
        /// <summary>
        /// Функция FindAllDataPersistenceObjects ищет все реализации интерфейса IDataPersistence на сцене
        /// </summary>
        /// <returns></returns>
        private List<IDataPersistence> FindAllDataPersistenceObjects()
       {
            IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistences);
       }
    }
}

