using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NoteApp;
using System.Reflection;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace NoteApp.UnitTests
{
    [TestFixture]
    public class ProjectManagerTests
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

        private static readonly string ExpectedFileName = DirectoryInformation + @"\expectedProject.json";

        [Test(Description = "Сохранение проекта в файл")]
        public void Test_SaveToFile_CorrectProject_CorrectSavedFile()
        {
            // Setup
            var expectedProject = new Project();
            expectedProject = CreateExpectedProject();
            var expectedFileContent = File.ReadAllText(ExpectedFileName);

            // Act
            var actualFileName = DirectoryInformation + @"\actualProject.json";
            ProjectManager.SaveToFile(expectedProject, actualFileName);
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
            var expectedProject = new Project();
            expectedProject = CreateExpectedProject();
            var expectedFileContent = File.ReadAllText(ExpectedFileName);

            if (Directory.Exists(DirectoryInformation + @"\actualProject.json"))
            {
                Directory.Delete(DirectoryInformation + @"\actualProject.json", true);
            }

            // Act
            var actualFileName = DirectoryInformation + @"\actualProject.json";
            ProjectManager.SaveToFile(expectedProject, actualFileName);
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
            var expectedProject = new Project();
            expectedProject = CreateExpectedProject();

            // Act
            var actualProject = ProjectManager.LoadFromFile(ExpectedFileName);

            // Assert
            Assert.AreEqual(expectedProject.Notes.Count, actualProject.Notes.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expectedProject.Notes.Count; i++)
                {
                    Assert.AreEqual(expectedProject.Notes[i], actualProject.Notes[i]);
                }
            });
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

        public Project CreateExpectedProject()
        {
            var expectedProject = new Project();
            expectedProject.Notes.Add(new Note()
            {
                Name = "note1",
                Text = "text1",
                Category = NoteCategory.Other,
                CreatedTime = new DateTime(2022, 01, 19),
                ModifiedTime = new DateTime(2022, 01, 19)
            });
            ProjectManager.SaveToFile(expectedProject, ExpectedFileName );
            return expectedProject;
        }
    }
}
