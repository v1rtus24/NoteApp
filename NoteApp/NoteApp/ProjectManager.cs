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
	/// ����� "�������� �������", ���������� �� �������� � �������� ������ �� �����
	/// </summary>
    public static class ProjectManager
    {
        /// <summary>
        /// ���������� � ������ ���� �� ��������� � �����>. 
        /// </summary>
        public static string FilePath { get; set; } = Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData) + @"\NoteApp\NoteApp.notes";

        /// <summary>
        /// ����� ���������� ������ � ����
        /// </summary>
        /// <param name="notes"></param>
        public static void SaveToFile(Project notes,string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            JsonSerializer serializer = new JsonSerializer();
            //��������� ����� ��� ������ � ���� � ��������� ����
            using (StreamWriter sw = new StreamWriter(filePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                //�������� ������������ � �������� ������, ������� ����� �������������
                serializer.Serialize(writer, notes);
            }
        }
        
        /// <summary>
        /// ����� �������� ������ �� �����
        /// </summary>
        /// <returns></returns>
        public static Project LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new Project();
            }
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                //��������� ����� ��� ������ �� ����� � ��������� ����
                using (StreamReader sr = new StreamReader(filePath))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    //�������� �������������� � ���� ����������� ��������� � ������� ��� ������
                    return (Project)serializer.Deserialize<Project>(reader);
                }
            }
            catch
            {
                return new Project();
            }
        }
    }
}
