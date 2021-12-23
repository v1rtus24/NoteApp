using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NoteApp.UnitTests
{
    public class ProjectTest
    {
        [Test(Description = "Позитивный тест геттера Notes")]
        public void TestNotesGet_CorrectValue()
        {
            var expected = new List<Note>();
            expected.Add(new Note("Name1", "Text1", NoteCategory.Other));

            var project = new Project();
            project.Notes.Add(new Note("Name1", "Text1", NoteCategory.Other));
            var actual = project.Notes;

            Assert.AreEqual(expected[0].Name, actual[0].Name);
            Assert.AreEqual(expected[0].Text, actual[0].Text);
            Assert.AreEqual(expected[0].Category, actual[0].Category);
        }

        [Test(Description = "Позитивный тест сеттера Notes")]
        public void TestNotesSet_CorrectValue()
        {
            var exceptedProject = new Project();
            exceptedProject.Notes.Add(new Note("Name1", "Text1", NoteCategory.Other));
            var actualProject = new Project();

            Assert.DoesNotThrow(() => { actualProject = exceptedProject; },
               "Не должно возникать исключения");
        }
    }
}
