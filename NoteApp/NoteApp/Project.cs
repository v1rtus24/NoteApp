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
        public List<Note> Notes { get; set; } = new List<Note>();

        /// <summary>
		/// Свойство текущей заметки.
		/// </summary>
		public int CurrentNote { get; set; } = -1;


        /// <summary>
        /// Организовать список по дате создания заметок
        /// </summary>
        /// <returns>Список отсортированных заметок</returns>
        public List<Note> SortList()
        {
            return Notes.OrderByDescending(t => t.ModifiedTime).ToList();
        }

        /// <summary>
        /// Организовать список заметок по дате создания и отфильтровать по категории
        /// </summary>
        /// <param name="category">Категория заметки</param>
        /// <returns>Список отфильтрованных заметок</returns>
        public List<Note> SortList(NoteCategory category)
        {
            List<Note> SortedList = new List<Note>();
            SortedList = SortList().FindAll(t => t.Category == category);
            return SortedList;
        }
    }
}
