using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NoteApp;

namespace NoteApp.UnitTests
{
    [TestFixture]
    public class ProjectManagerTest
    {
        /// <summary>
        /// Задаёт путь к папке, где лежит файл
        /// </summary>
        private static readonly string FolderPath = ProjectManager.FolderPath;

        /// <summary>
        /// Задаёт путь к файлу
        /// </summary>
        private static readonly string FilePath = ProjectManager.FilePath;

        /// <summary>
        /// Путь к тестовому файлу c корректными данными>.
        /// </summary>
        private readonly string _correctDataFileName = FolderPath +
            @"\TestData\CorrectData.notes";

        /// <summary>
        /// Путь к папке, в которую сохраняется тестовый файл.
        /// </summary>
        private static readonly string SavingDirectoryPath = FolderPath +
            @"\Output";

        /// <summary>
        /// Путь к сохраняемому тестовому файлу проекта>.
        /// </summary>
        private readonly string _savingDataFilename = SavingDirectoryPath +
            @"\SavingData.notes";

        private Project _project;

        [SetUp]
        public void InitProject()
        {
            _project = new Project();
            _project.Notes.Add(new Note("Name1", "Text1", NoteCategory.Documents));
            _project.Notes.Add(new Note("Name2", "Text2", NoteCategory.Documents));
            _project.Notes.Add(new Note("Name3", "Text3", NoteCategory.Documents));
            ProjectManager.SaveToFile(_project, _correctDataFileName);
        }

        [Test(Description = "Тест геттера FolderPath")]
        public void TestFolderPathGet_CorrectValue()
        {
            // Setup
            var expected = FolderPath;

            // Act
            var actual = ProjectManager.FolderPath;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test(Description = "Позитивный тест сеттера FolderPath")]
        public void TestFolderPathSet_CorrectValue()
        {
            // Setup
            var expected = ProjectManager.FolderPath;

            // Act
            ProjectManager.FolderPath = expected;
            var actual = ProjectManager.FolderPath;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test(Description = "Тест геттера FilePath")]
        public void TestFilePathGet_CorrectValue()
        {
            // Setup
            var expected = FilePath;

            // Act
            var actual = ProjectManager.FilePath;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test(Description = "Позитивный тест сеттера FilePath")]
        public void TestFilePathSet_CorrectValue()
        {
            // Setup
            var expected = ProjectManager.FilePath;

            // Act
            ProjectManager.FilePath = expected;
            var actual = ProjectManager.FilePath;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test(Description = "Сохранение проекта в файл")]
        public void TestSaveToFile()
        {
            // Setup
            var savingProject = _project;
            ProjectManager.SaveToFile(savingProject, _correctDataFileName);
            var expected = File.ReadAllText(_correctDataFileName);

            // Act
            ProjectManager.SaveToFile(savingProject, _savingDataFilename);
            var actual = File.ReadAllText(_savingDataFilename);

            // Assert
            Assert.IsTrue(Directory.Exists(SavingDirectoryPath), "Папка для хранения тестового файла не создана");
            Assert.IsTrue(File.Exists(_savingDataFilename), "Файл для сохранения тестового файла не создан");
            Assert.AreEqual(expected, actual);
        }

        [Test(Description = "Сохранение проекта в несуществующий файл")]
        public void TestSaveToFileWithoutDirectory()
        {
            // Setup
            var savingProject = _project;
            ProjectManager.SaveToFile(savingProject, _correctDataFileName);
            var expected = File.ReadAllText(_correctDataFileName);

            if (Directory.Exists(SavingDirectoryPath))
            {
                Directory.Delete(SavingDirectoryPath, true);
            }

            // Act
            ProjectManager.SaveToFile(savingProject, _savingDataFilename);
            var actual = File.ReadAllText(_savingDataFilename);

            // Assert
            Assert.IsTrue(Directory.Exists(SavingDirectoryPath), "Папка для хранения тестового файла не создана");
            Assert.IsTrue(File.Exists(_savingDataFilename), "Файл для сохранения тестового файла не создан");
            Assert.AreEqual(expected, actual);
        }

        [Test(Description = "Загрузка  сохраненного файла проекта")]
        public void TestLoadFromFile()
        {
            // Setup
            var expectedProject = _project;

            // Act
            var actualProject = ProjectManager.LoadFromFile(_correctDataFileName);

            // Assert
            Assert.AreEqual(expectedProject.Notes.Count, actualProject.Notes.Count);

            for (int i = 0; i < expectedProject.Notes.Count; i++)
            {
                Assert.AreEqual(expectedProject.Notes[i].Name, actualProject.Notes[i].Name);
                Assert.AreEqual(expectedProject.Notes[i].Text, actualProject.Notes[i].Text);
                Assert.AreEqual(expectedProject.Notes[i].Category, actualProject.Notes[i].Category);
                Assert.AreEqual(expectedProject.Notes[i].CreatedTime, actualProject.Notes[i].CreatedTime);
                Assert.AreEqual(expectedProject.Notes[i].ModifiedTime, actualProject.Notes[i].ModifiedTime);
            }
        }

        [Test(Description = "Попытка загрузки некорректного проекта из файла")]
        public void LoadFromFile_LoadCorruptedProject_ReturnsEmptyProject()
        {
            //Setup
            var excepted = File.Create(FolderPath + @"\TestData\NotCorrectData.txt");

            // Act
            var actualProject = ProjectManager.LoadFromFile(excepted.Name);

            // Assert
            Assert.AreEqual(actualProject.Notes.Count, 0); 
        }

        [Test(Description = "Попытка загрузки проекта из несуществующего файла")]
        public void LoadFromFile_LoadNonExistentProject_ReturnsEmptyProject()
        {
            // Act
            var actualProject = ProjectManager.LoadFromFile(FolderPath + @"\TestData\123456.json");

            // Assert
            Assert.IsNotNull(actualProject);
            Assert.AreEqual(actualProject.Notes.Count, 0);
        }
    }
}
