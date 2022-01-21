using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NoteApp.UnitTests
{
    
    [TestFixture]
    public class ProjectTests
    {      
        [Test(Description = "Тест геттера Notes")]
        public void Test_Notes_CorrectValue_ReturnsSameValue()
        {
            // Setup
            var project = new Project();
            var expected = new Note("Name1", "Text1", NoteCategory.Other);

            // Act
            project.Notes.Add(expected);
            var actual = project.Notes;

            // Assert
            Assert.AreEqual(1,actual.Count);
            Assert.AreEqual(expected, actual[0]);
        }

        [Test(Description = "Позитивный тест сеттера Notes")]
        public void Test_Notes_ReplaceDefaultNotesList_SetsCorrectValue()
        {
            // Setup
            var project = new Project();
            var expectedList = new List<Note>();
            expectedList.Add(new Note("Name1", "Text1", NoteCategory.Other));

            // Act
            project.Notes = expectedList;
            var actual = project.Notes;

            // Assert
            Assert.AreEqual(expectedList.Count,actual.Count);
            Assert.AreEqual(expectedList[0], actual[0]);

        }
    }
}
