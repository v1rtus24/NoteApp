using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    	/// <summary>
	/// ����� "������", �������� ������ ���� �������
	/// </summary>
    public class Project
    {
        /// <summary>
        /// ������ �������
        /// </summary>
        public List<Note> Notes { get; set; } = new List<Note>();

        /// <summary>
		/// �������� ������� �������.
		/// </summary>
		public int CurrentNote { get; set; } = -1;


        /// <summary>
        /// ������������ ������ �� ���� �������� �������
        /// </summary>
        /// <returns>������ ��������������� �������</returns>
        public List<Note> SortList()
        {
            return Notes.OrderByDescending(t => t.ModifiedTime).ToList();
        }

        /// <summary>
        /// ������������ ������ ������� �� ���� �������� � ������������� �� ���������
        /// </summary>
        /// <param name="category">��������� �������</param>
        /// <returns>������ ��������������� �������</returns>
        public List<Note> SortList(NoteCategory category)
        {
            List<Note> SortedList = new List<Note>();
            SortedList = SortList().FindAll(t => t.Category == category);
            return SortedList;
        }
    }
}
