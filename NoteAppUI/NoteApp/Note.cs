using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NoteApp
{
   
   /// <summary>
   /// Класс "Заметка"
   /// </summary>
    public class Note 
    {
        /// <summary>
        /// "Название заметки"
        /// </summary>
        private string _name;
        /// <summary>
        /// "Текст заметки"
        /// </summary>
        private string _noteText;
        /// <summary>
        /// "Категория заметки"
        /// </summary>
        private noteCategory _noteCategory;
        /// <summary>
        /// "Время создания заметки"
        /// </summary>
        private DateTime _timeOfCreation;
        /// <summary>
        /// "Время последнего редактирования"
        /// </summary>
        private DateTime _timeOfLastEdit;

        /// <summary>
        /// Свойство для поля "Название заметки"
        /// </summary>
        public string Name { get { return _name; }  
            set
            {
                if (value.Length == 0 )
                {
                    _name = "Без названия";
                    return;
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("Длина названия больше 50 симовлов!");
                }
                else
                    _name = value;
            }
        }
        /// <summary>
        /// Свойство для поля "Категория заметки"
        /// </summary>
        public  noteCategory NoteCategory {
            get
            {
                return _noteCategory;
            }

            set
            {
                _noteCategory = value;
            }
        }
        /// <summary>
        /// Свойство для поля "Текст заметки"
        /// </summary>
        public string NoteText
        {
            get
            {
                return _noteText;
            }
            set
            {
                if (value.Length == 0 || value == null)
                {
                    throw new ArgumentException("Текст заметки пустой!");
                }
                else
                {
                    _noteText = value;
                }
            }
        }
        /// <summary>
        /// Свойство для поля "Время создания заметки"
        /// </summary>

        public DateTime TimeOfCreation { 

            get
            {
                return _timeOfCreation;
            } 
        }
        /// <summary>
        /// Свойство для поля "Время последнего редактирования"
        /// </summary>
        public DateTime TimeOfLastEdit
        {
            get
            {
                return _timeOfLastEdit;
            }
            
        }
        /// <summary>
        /// Конструктор, для установки значений 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="noteText"></param>
        /// <param name="noteCat"></param>
        public Note(string name, string noteText, noteCategory noteCat)
        {
            Name = name;
            NoteText = noteText;
            NoteCategory = noteCat;
            _timeOfCreation = DateTime.Now;
            _timeOfLastEdit = DateTime.Now;
            
        }

    }
}
