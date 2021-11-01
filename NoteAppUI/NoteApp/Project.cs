using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Класс "Проект", содержит список всех заметок
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Список заметок
        /// </summary>
        private List<Note> _notes = new List<Note>();
        /// <summary>
        /// Свойство для поля "_notes"
        /// </summary>
        public List<Note> Notes {
            get
            {
                return _notes;
            }
            set
            {
                _notes = value;
            }
        }
        
    }
}
