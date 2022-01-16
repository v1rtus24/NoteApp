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
    public class NoteTests
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
            // Setup
            var expected = "Name1";
            _note.Name = expected;

            // Act
            var actual = _note.Name;

            // Assert
            Assert.AreEqual(expected, actual, "Геттер Name возвращает неправильное имя");
        }

        [Test(Description = "Нигативный тест сеттера Name")]
        public void TestNameSet_ArgumentException()
        {
            // Setup
            string wrongName = "ErrorErrorErrorErrorErrorErrorErrorErrorError" +
                "ErrorErrorErrorErrorErrorErrorErrorError";

            // Assert
            Assert.Throws<ArgumentException>(() => 
            { 
                // Act
                _note.Name = wrongName; 
            },"Должно возникать исключение, если фамилия длиннее 50 символов");
        }

        [Test(Description = "Позитивный тест сеттера Name")]
        
        public void TestNameSet_CorrectValue()
        {
            //Setup
            string excepted = "Name1";

            //Act
            _note.Name = excepted;
            string actual = _note.Name;

            // Assert
            Assert.AreEqual(excepted, actual, "Сеттер устанавливает неверное значение");
        }

        [Test(Description = "Присвоение пустой строки для Name")]
        public void TestNameSet_EmptyStringd()
        {
            // Setup
            var expected = "Без названия";

            // Act
            _note.Name = "";
            var actual = _note.Name;

            // Assert
            Assert.AreEqual(expected, actual,"Сеттер устанавливает неверное значение");
        }



        [Test(Description = "Тест геттера Category")]
        public void TestCategoryGet_CorrectValue()
        {
            // Setup
            var expected = NoteCategory.Other;
            _note.Category = expected;

            // Act
            var actual = _note.Category;

            // Assert
            Assert.AreEqual(expected, actual, "Геттер Category возвращает неправильную категорию");
        }

        [Test(Description = "Позитивный тест сеттера Category")]
        public void TestCategorySet_CorrectValue()
        {
            //Setup
            var expected = NoteCategory.Other;

            //Act
            _note.Category = expected;
            var actual = _note.Category;

            // Assert
            Assert.AreEqual(expected, actual, "Сеттер устанавливает неверное значение");
        }

        [Test(Description = "Позитивный тест геттера Text")]
        public void TestTextGet_CorrectValue()
        {
            // Setup
            var expected = "TextTextText";
            _note.Text = expected;
            // Act
            var actual = _note.Text;

            // Assert
            Assert.AreEqual(expected, actual, "Геттер Text возвращает неправильную категорию");
        }

        [Test(Description = "Позитивный тест сеттера Text")]
        public void TestTextSet_CorrectValue()
        {
            //Setup
            var expected = "Text1";

            //Act
            _note.Text = expected;
            var actual = _note.Text;

            // Assert
            Assert.AreEqual(expected, actual, "Сеттер устанавливает неверное значение");
        }

        [Test(Description = "Позитивный тест геттера CreatedTime")]
        public void TestCreatedTimeGet_CorrectValue()
        {
            // Setup
            var expected = DateTime.Now;
            _note.CreatedTime = expected;

            // Act
            var actual = _note.CreatedTime;

            // Assert
            Assert.AreEqual(expected, actual, "Геттер DateOfCreation возвращает неправильное время");
        }

        [Test(Description = "Позитивный тест сеттера CreatedTime")]
        public void TestCreatedTimeSet_CorrectValue()
        {
            // Setup
            var expected = DateTime.Now;

            // Act
            _note.CreatedTime = expected;
            var actual = _note.CreatedTime;

            // Assert
            Assert.AreEqual(expected, actual, "Сеттер устанавливает неверное значение");
        }

        [Test(Description = "Позитивный тест геттера ModifiedTime")]
        public void TestModifiedTimeGet_CorrectValue()
        {
            // Setup
            var expected = DateTime.Now;

            // Act
            _note.ModifiedTime = expected;

            // Assert
            var actual = _note.ModifiedTime;
            Assert.AreEqual(expected, actual, "Геттер ModifiedTime возвращает неправильное время");
        }

        [Test(Description = "Позитивный тест сеттера CreatedTime")]
        public void TestModifiedTimeSet_CorrectValue()
        {
            //Setup
            var expected = DateTime.Now;

            //Act
            _note.ModifiedTime = expected;
            var actual = _note.ModifiedTime;

            // Assert
            Assert.AreEqual(expected, actual, "Сеттер устанавливает неверное значение");
        }

        [Test(Description = "Тест метода Сlone()")]
        public void TestNoteClone()
        {
            // Setup
            var expected = new Note("Name1","Text1",NoteCategory.People);

            // Act
            var actual = (Note)expected.Clone();

            // Assert
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
        // Setup
        [TestCase("Name1", "Text1", NoteCategory.Work)]
        public void TestNoteConstructor(string expectedName, string expectedText, 
            NoteCategory expectedCategory)
        {
            // Act
            _note = new Note(expectedName,expectedText,expectedCategory);

            // Setup
            Assert.AreEqual(expectedName, _note.Name);
            Assert.AreEqual(expectedText, _note.Text);
            Assert.AreEqual(expectedCategory, _note.Category);
        }
    }
}
