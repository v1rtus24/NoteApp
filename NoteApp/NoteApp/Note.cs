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
        /// Свойство для поля "Название заметки"
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length == 0)
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
        public NoteCategory Category { get; set; }

        /// <summary>
        /// Свойство для поля "Текст заметки"
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Свойство для поля "Время создания заметки"
        /// </summary>

        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Свойство для поля "Время последнего редактирования"
        /// </summary>
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// Конструктор, для установки значений 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="category"></param>
        public Note(string name, string text, NoteCategory category)
        {
            this.Name = name;
            this.Text = text;
            this.Category = category;
            ModifiedTime = DateTime.Now;
            CreatedTime = DateTime.Now;
            
        }
        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Note() { }

        /// <summary>
        /// Метод, который возвращает копию объекта
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Сравнивает значения двух заметок
        /// </summary>
        /// <param name="obj">Заметка, с которой идет сравнение</param>
        /// <returns>true, если все поля одной заметки совпадают с другой, иначе - false</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Note other))
            {
                return false;
            }

            return Name == other.Name && Text == other.Text && Category == other.Category &&
                CreatedTime == other.CreatedTime && ModifiedTime == other.ModifiedTime;
        }

    }
}
