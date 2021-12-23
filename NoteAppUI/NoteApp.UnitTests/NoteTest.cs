using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NoteApp;

namespace NoteApp.UnitTests
{
    [TestFixture]
    public class NoteTest
    {
        private Note _note;

        [SetUp]
        public void InitNote()
        {
            _note = new Note();
        }

        [Test(Description = "Позитивный тест геттера Name")]
        public void TestNameGet_CorrectValue()
        {
            var expected = "Name1";
            _note.Name = expected;
            var actual = _note.Name;
            Assert.AreEqual(expected, actual, "Геттер Name возвращает неправильное имя");
        }

        [Test(Description = "Нигативный тест сеттера Name")]
        public void TestNameSet_ArgumentException()
        {
            string wrongName = "Влад-Влад-Влад-Влад-Влад-Влад" +
                "-Влад-Влад-Влад-Влад-Влад-Влад-Влад-Влад-Влад-Влад";
            Assert.Throws<ArgumentException>(() => { _note.Name = wrongName; },
                "Должно возникать исключение, если фамилия длиннее 50 символов");
        }

        [Test(Description = "Позитивный тест сеттера Name")]
        [TestCase("","Не должно возникать исключания",TestName ="Присвоение пустой строки")]
        [TestCase("Name1", "Не должно возникать исключания", 
            TestName = "Присвоение корректного значение")]
        public void TestNameSet_NotArgumentException(string wrongName,string message)
        {
            Assert.DoesNotThrow(() => { _note.Name = wrongName; }, message );
        }

        [Test(Description = "Тест геттера Category")]
        public void TestCategoryGet_CorrectValue()
        {
            var expected = NoteCategory.Other;
            _note.Category = expected;
            var actual = _note.Category;
            Assert.AreEqual(expected, actual, "Геттер Category возвращает неправильную категорию");
        }

        [Test(Description = "Позитивный тест сеттера Category")]
        public void TestCategorySet_CorrectValue()
        {
            Assert.DoesNotThrow(() => { _note.Category = NoteCategory.Other; },
                "Не должно возникать исключения");
        }

        [Test(Description = "Позитивный тест геттера Text")]
        public void TestTextGet_CorrectValue()
        {
            var expected = "TextTextText";
            _note.Text = expected;
            var actual = _note.Text;
            Assert.AreEqual(expected, actual, "Геттер Text возвращает неправильную категорию");
        }

        [Test(Description = "Позитивный тест сеттера Text")]
        public void TestTextSet_CorrectValue()
        {
            Assert.DoesNotThrow(() => { _note.Text = "TextTextText"; }, 
                "Не должно возникать исключения");
        }

        [Test(Description = "Позитивный тест геттера CreatedTime")]
        public void TestCreatedTimeGet_CorrectValue()
        {
            var expected = DateTime.Now;
            _note.CreatedTime = expected;
            var actual = _note.CreatedTime;
            Assert.AreEqual(expected, actual, "Геттер DateOfCreation возвращает неправильное время");
        }

        [Test(Description = "Позитивный тест сеттера CreatedTime")]
        public void TestCreatedTimeSet_CorrectValue()
        {
            Assert.DoesNotThrow(() => { _note.CreatedTime = DateTime.Now; },
                "Не должно возникать исключения");
        }

        [Test(Description = "Позитивный тест геттера ModifiedTime")]
        public void TestModifiedTimeGet_CorrectValue()
        {
            var expected = DateTime.Now;
            _note.ModifiedTime = expected;
            var actual = _note.ModifiedTime;
            Assert.AreEqual(expected, actual, "Геттер ModifiedTime возвращает неправильное время");
        }

        [Test(Description = "Позитивный тест сеттера CreatedTime")]
        public void TestModifiedTimeSet_CorrectValue()
        {
            Assert.DoesNotThrow(() => { _note.ModifiedTime = DateTime.Now; }, 
                "Не должно возникать исключения");
        }

        [Test(Description = "Тест метода Сlone()")]
        public void TestNoteClone()
        {
            var expected = new Note("Name1","Text1",NoteCategory.People);
            var actual = (Note)expected.Clone();
            Assert.AreEqual(expected.Name, actual.Name,
                "Возвращает неправильное имя");
            Assert.AreEqual(expected.Text, actual.Text,
                "Возвращает неправильный текст");
            Assert.AreEqual(expected.Category, actual.Category,
                "Возвращает неправильную категорию");
            Assert.AreEqual(expected.CreatedTime, actual.CreatedTime,
                "Возвращает непривильное время создания");
            Assert.AreEqual(expected.ModifiedTime, actual.ModifiedTime,
                "Возвращает неправильное время изменения");
        }

        [Test(Description = "Тест конструктора")]
        [TestCase("Name1", "Text1", NoteCategory.Work)]
        public void TestNoteConstructor(string expectedName, string expectedText, 
            NoteCategory expectedCategory)
        {
            _note = new Note(expectedName,expectedText,expectedCategory);
            Assert.AreEqual(expectedName, _note.Name);
            Assert.AreEqual(expectedText, _note.Text);
            Assert.AreEqual(expectedCategory, _note.Category);
        }
    }
}
