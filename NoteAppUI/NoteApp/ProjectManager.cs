﻿using System;
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
        private static readonly string _file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NoteApp\\NoteApp.notes";
        /// <summary>
        /// Метод сохранения данных в файл
        /// </summary>
        /// <param name="notes"></param>
        public static void SaveToFile(Project notes)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NoteApp"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NoteApp");
            }
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
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NoteApp"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\NoteApp");
            }
            if (!File.Exists(_file))
            {
                return new Project();
            }
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
