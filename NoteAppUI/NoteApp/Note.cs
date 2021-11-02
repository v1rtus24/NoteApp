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
    public class Note : ICloneable
    {
        /// <summary>
        /// "Название заметки"
        /// </summary>
        private string _name;
        /// <summary>
        /// "Текст заметки"
        /// </summary>
        private string _Text;
        /// <summary>
        /// "Категория заметки"
        /// </summary>
        private NoteCategory _Category;
        /// <summary>
        /// "Время создания заметки"
        /// </summary>
        private DateTime _createdTime;
        /// <summary>
        /// "Время последнего редактирования"
        /// </summary>
        private DateTime _modifiedTime;

        /// <summary>
        /// Свойство для поля "Название заметки"
        /// </summary>
        public string Name { 
            get 
            { 
                return _name; 
            }  
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
        public  NoteCategory Category {
            get
            {
                return _Category;
            }

            set
            {
                _Category = value;
            }
        }
        /// <summary>
        /// Свойство для поля "Текст заметки"
        /// </summary>
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }
        /// <summary>
        /// Свойство для поля "Время создания заметки"
        /// </summary>

        public DateTime CreatedTime { 

            get
            {
                return _createdTime;
            }
            set
            {
                _createdTime = value;
            }
        }
        /// <summary>
        /// Свойство для поля "Время последнего редактирования"
        /// </summary>
        public DateTime ModifiedTime
        {
            get
            {
                return _modifiedTime;
            }
            set 
            {
                _modifiedTime = value;
            }

        }
        /// <summary>
        /// Конструктор, для установки значений 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="noteText"></param>
        /// <param name="noteCat"></param>
        public Note(string name, string noteText, NoteCategory noteCat)
        {
            Name = name;
            Text = noteText;
            Category = noteCat;
            _createdTime = DateTime.Now;
            _modifiedTime = DateTime.Now;
            
        }
        public Note() { }

        /// <summary>
        /// Метод, который возвращает копию объекта
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
