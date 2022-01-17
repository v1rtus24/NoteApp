using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NoteApp.UnitTests
{
    
    [TestFixture]
    public class ProjectTest
    {
        private Project _project;
        
        [Test(Description = "Тест геттера Notes")]
        public void TestNotesGet_CorrectValue()
        {
            // Setup
            var expected = new List<Note>();
            expected.Add(new Note("Name1", "Text1", NoteCategory.Other));

            // Act
            _project = new Project();
            _project.Notes.Add(new Note("Name1", "Text1", NoteCategory.Other));
            var actual = _project.Notes;

            // Assert
            Assert.AreEqual(expected[0].Name, actual[0].Name);
            Assert.AreEqual(expected[0].Text, actual[0].Text);
            Assert.AreEqual(expected[0].Category, actual[0].Category);
        }

        [Test(Description = "Позитивный тест сеттера Notes")]
        public void TestNotesSet_CorrectValue()
        {
            // Setup
            var expected = new List<Note>();
            expected.Add(new Note("Name1", "Text1", NoteCategory.Other));

            // Act
            _project = new Project();
            _project.Notes = expected;
            var actual = _project.Notes;

            // Assert
            Assert.AreEqual(expected[0].Name, actual[0].Name);
            Assert.AreEqual(expected[0].Text, actual[0].Text);
            Assert.AreEqual(expected[0].Category, actual[0].Category);
        }
    }
}
