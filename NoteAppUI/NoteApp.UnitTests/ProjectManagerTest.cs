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
    public class ProjectManagerTest
    {
        private static readonly string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        + "\\NoteApp\\";

        private static readonly string FileName = "NoteApp.notes";
        private Project _project;

        [SetUp]
        public void InitProject()
        {
            _project = new Project();
            _project.Notes.Add(new Note("Name1", "Text1", NoteCategory.Documents));
        }

        [Test(Description = "Tест сериализации проекта в файл")]
        public void TestProjectManagerSaveToFile()
        {
            ProjectManager.SaveToFile(_project);
            Assert.True(File.Exists(FolderPath+FileName));
        }

        [Test(Description = "Tест сериализации проекта без файла")]
        public void TestProjectManagerSaveToFileWithoutFile()
        {
            if (File.Exists(FolderPath + FileName))
            {
                File.Delete(FolderPath + FileName);
            }
            ProjectManager.SaveToFile(_project);
            Assert.True(File.Exists(FolderPath + FileName));
        }

        [Test(Description = "Tест сериализации проекта без существующей дериктории")]
        public void TestProjectManagerSaveToFileWithoutDirectory()
        {
            if (File.Exists(FolderPath + FileName))
            {
                File.Delete(FolderPath + FileName);
                Directory.Delete(FolderPath);
            }
            ProjectManager.SaveToFile(_project);
            Assert.True(File.Exists(FolderPath + FileName));
        }

        [Test(Description = "Тест десериализации проекта из файла")]
        public void TestProjectManagerLoadFromFile()
        {
            var expectedNoteName = "Name1";
            var expectedNoteText = "Text1";
            NoteCategory expectedNoteCategory = NoteCategory.People;

            Project project = new Project();
            project.Notes.Add(new Note("Name1", "Text1", NoteCategory.People));
            ProjectManager.SaveToFile(project);

            var actual = ProjectManager.LoadFromFile();
            Assert.AreEqual(expectedNoteName, actual.Notes[0].Name);
            Assert.AreEqual(expectedNoteText, actual.Notes[0].Text);
            Assert.AreEqual(expectedNoteCategory, actual.Notes[0].Category);
        }

        [Test(Description = "Тест десериализации проекта без файла сохранения ")]
        public void TestProjectManagerLoadFromFileWithoutFile()
        {
            if (File.Exists(FolderPath + FileName))
            {
                File.Delete(FolderPath + FileName);
            }
            var project = ProjectManager.LoadFromFile();
            var expected = 0;
            var actual = project.Notes.Count;
            Assert.AreEqual(expected, actual);
        }
        [Test(Description = "Тест десериализации проекта существующей дериктории")]
        public void TestProjectManagerLoadFromFileWithoutDerictory()
        {
            if (File.Exists(FolderPath + FileName))
            {
                File.Delete(FolderPath + FileName);
                Directory.Delete(FolderPath);
            }
            var project = ProjectManager.LoadFromFile();
            var expected = 0;
            var actual = project.Notes.Count;
            Assert.AreEqual(expected, actual);
        }
    }
}
