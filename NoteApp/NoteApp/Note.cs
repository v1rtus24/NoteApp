using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NoteApp
{
   
   	/// <summary>
	/// ����� "�������"
	/// </summary>
    public class Note : ICloneable
    {
        /// <summary>
        /// "�������� �������"
        /// </summary>
        private string _name;

        /// <summary>
        /// �������� ��� ���� "�������� �������"
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
                    _name = "��� ��������";
                    return;
                }
                if (value.Length > 50)
                {
                    throw new ArgumentException("����� �������� ������ 50 ��������!");
                }
                else
                    _name = value;
            }
        }

        /// <summary>
        /// �������� ��� ���� "��������� �������"
        /// </summary>
        public NoteCategory Category { get; set; }

        /// <summary>
        /// �������� ��� ���� "����� �������"
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// �������� ��� ���� "����� �������� �������"
        /// </summary>

        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// �������� ��� ���� "����� ���������� ��������������"
        /// </summary>
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        /// �����������, ��� ��������� �������� 
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
        /// ������ �����������
        /// </summary>
        public Note() { }

        /// <summary>
        /// �����, ������� ���������� ����� �������
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// ���������� �������� ���� �������
        /// </summary>
        /// <param name="obj">�������, � ������� ���� ���������</param>
        /// <returns>true, ���� ��� ���� ����� ������� ��������� � ������, ����� - false</returns>
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
