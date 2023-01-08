using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedLine.Save
{
    /// <summary>
    /// Интерфейс IDataPersistence используется для связи полей вашего класса с полями из класса GameData
    /// </summary>
    public interface IDataPersistence
    {
        /// <summary>
        /// Метод LoadData используется для передачи значений из поля(ей) класса GameData в поле(я) вашего класса
        /// </summary>
        /// <param name="data"></param>
        public void LoadData(GameData data);
        /// <summary>
        /// Метод SaveData используется для передачи значений из поля(ей) вашего класса в поле(я) класса GameData
        /// </summary>
        /// <param name="data"></param>
        public void SaveData(ref GameData data);
    }
}
