using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace NoteApp
{
    /// <summary>
    /// Класс "Менеджер Проекта", отвечающий за загрузку и выгрузку данных из файла
    /// </summary>
    public static class ProjectManager
    {
        /// <summary>
        /// Константа, указывающая путь к файлу
        /// </summary>
        private  const string _file = @"C:\Users\v1rtu\OneDrive\Документы\NoteApp\NoteApp.notes";
        /// <summary>
        /// Метод сохранения данных в файл
        /// </summary>
        /// <param name="notes"></param>
        public static void SaveToFile(Project notes)
        {
            JsonSerializer serializer = new JsonSerializer();
            //Открываем поток для записи в файл с указанием пути
            using (StreamWriter sw = new StreamWriter(_file))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                //Вызываем сериализацию и передаем объект, который хотим сериализовать
                serializer.Serialize(writer, notes);
            }
        }
        /// <summary>
        /// Метод загрузки данных из файла
        /// </summary>
        /// <returns></returns>
        public static Project LoadFromFile()
        {
            JsonSerializer serializer = new JsonSerializer();
            //Открываем поток для чтения из файла с указанием пути
            using (StreamReader sr = new StreamReader(_file))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                //Вызываем десериализацию и явно преобразуем результат в целевой тип данных
                return (Project)serializer.Deserialize<Project>(reader);
            }
        }
    }
}
