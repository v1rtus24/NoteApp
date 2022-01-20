using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NoteApp;
using System.Reflection;

namespace NoteApp.UnitTests
{
    [TestFixture]
    public class ProjectManagerTest
    {
        /// <summary>
        /// Расположение сборки, которая содержит код, исполняемый в данный момент.
        /// </summary>
        private static readonly string LocalPath = Assembly.GetExecutingAssembly().Location;

        /// <summary>
        /// Информация о каталогах пути, по которому расположена сборка.
        /// </summary>
        private static readonly string DirectoryInformation = Path.GetDirectoryName(LocalPath) + @"\TestData";

        /// <summary>
        /// Задаёт путь к дефолтному файлу
        /// </summary>
        private static readonly string DefaultFilePath  = Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData) + @"\NoteApp\NoteApp.notes";

        private static readonly string ExceptedFileName = DirectoryInformation + @"\exceptedProject.json";

        [Test(Description = "Сохранение проекта в файл")]
        public void Test_SaveToFile_CorrectProject_CorrectSavedFile()
        {
            // Setup
            var exceptedProject = new Project();
            exceptedProject = CreateExceptedProject();
            var expectedFileContent = File.ReadAllText(ExceptedFileName);

            // Act
            var actualFileName = DirectoryInformation + @"\actualProject.json";
            ProjectManager.SaveToFile(exceptedProject, actualFileName);
            var actualFileContent = File.ReadAllText(actualFileName);

            // Assert
            Assert.IsTrue(Directory.Exists(DirectoryInformation), "Папка для хранения тестового файла не создана");
            Assert.IsTrue(File.Exists(actualFileName), "Файл для сохранения тестового файла не создан");
            Assert.AreEqual(expectedFileContent, actualFileContent);
        }

        [Test(Description = "Сохранение проекта в несуществующий файл")]
        public void Test_SaveToFile_WithoutDirectory_CorrectSavedFile()
        {
            // Setup
            var exceptedProject = new Project();
            exceptedProject = CreateExceptedProject();
            var expectedFileContent = File.ReadAllText(ExceptedFileName);

            if (Directory.Exists(DirectoryInformation + @"\actualProject.json"))
            {
                Directory.Delete(DirectoryInformation + @"\actualProject.json", true);
            }

            // Act
            var actualFileName = DirectoryInformation + @"\actualProject.json";
            ProjectManager.SaveToFile(exceptedProject, actualFileName);
            var actualFileContent = File.ReadAllText(actualFileName);

            // Assert
            Assert.IsTrue(Directory.Exists(DirectoryInformation), "Папка для хранения тестового файла не создана");
            Assert.IsTrue(File.Exists(actualFileName), "Файл для сохранения тестового файла не создан");
            Assert.AreEqual(expectedFileContent, actualFileContent);
        }

        [Test(Description = "Загрузка  сохраненного файла проекта")]
        public void Test_LoadFromFile_CorrectProject_CorrectLoadedFile()
        {
            // Setup
            var exceptedProject = new Project();
            exceptedProject = CreateExceptedProject();

            // Act
            var actualProject = ProjectManager.LoadFromFile(ExceptedFileName);

            // Assert
            Assert.AreEqual(exceptedProject.Notes.Count, actualProject.Notes.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < exceptedProject.Notes.Count; i++)
                {
                    Assert.AreEqual(exceptedProject.Notes[i], actualProject.Notes[i]);
                }
            });
        }

        [Test(Description = "Попытка загрузки некорректного проекта из файла")]
        public void Test_LoadFromFile_LoadCorruptedProject_ReturnsEmptyProject()
        {
            //Setup
            var wrongFileName = DirectoryInformation + @"\12345.txt";


            // Act
            var actualProject = ProjectManager.LoadFromFile(wrongFileName);

            // Assert
            Assert.AreEqual(actualProject.Notes.Count, 0);
        }

        [Test(Description = "Попытка загрузки проекта из несуществующего файла")]
        public void Test_LoadFromFile_LoadNonExistentProject_ReturnsEmptyProject()
        {
            // Setup
            var nonExistentFileName = DirectoryInformation + @"\123456789.notes";

            // Act
            var actualProject = ProjectManager.LoadFromFile(nonExistentFileName);

            // Assert
            Assert.IsNotNull(actualProject);
            Assert.AreEqual(actualProject.Notes.Count, 0);
        }

        public Project CreateExceptedProject()
        {
            var exceptedProject = new Project();
            exceptedProject.Notes.Add(new Note()
            {
                Name = "note1",
                Text = "text1",
                Category = NoteCategory.Other,
                CreatedTime = new DateTime(2022, 01, 19),
                ModifiedTime = new DateTime(2022, 01, 19)
            });
            ProjectManager.SaveToFile(exceptedProject, ExceptedFileName);
            return exceptedProject;
        }
    }
}
