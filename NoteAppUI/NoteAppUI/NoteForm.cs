using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoteApp;

namespace NoteAppUI
{
    public partial class NoteForm : Form
        {    
        /// <summary>
        /// Поле  текущей заметки
        /// </summary>
        public Note CurrentNote { get; set; }
        public NoteForm()
        {
            InitializeComponent();
            var categories = Enum.GetValues(typeof(NoteCategory)).Cast<object>().ToArray();
            CategoryComboBox.Items.AddRange(categories);
        }

        /// <summary>
        /// Нажатие кнопки "Ок"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {          
            try
            {
                if (CategoryComboBox.SelectedIndex != -1)
                {
                    var curentCategory = (NoteCategory)CategoryComboBox.SelectedIndex;
                    CurrentNote = new Note(nameTextBox.Text, noteTextTextBox.Text, curentCategory);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Не выбрана категория заметки!");
                }
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void UpdateForm()
        {
            nameTextBox.Text = CurrentNote.Name;
            noteTextTextBox.Text = CurrentNote.Text;
            CategoryComboBox.Text = CurrentNote.Category.ToString();
            dateTimePicker1.Value = CurrentNote.CreatedTime;
            dateTimePicker2.Value = CurrentNote.ModifiedTime;
        }

        /// <summary>
        /// Метод загрузки формы. Если EditNote = true, то отображает данные редактируемой заметки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_EditForm_Load(object sender, EventArgs e)
        {
            if (CurrentNote != null)
            {
                UpdateForm();
            }
        }

        /// <summary>
        /// Кнопки "Отмена"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Метод, для изменения цвета titleTextBox, если длина текста больше 50
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            if(nameTextBox.TextLength > 50)
            {
                nameTextBox.BackColor = Color.LightSalmon;
                okButton.Enabled = false;
            }
            else
            {
                nameTextBox.BackColor = Color.White;
                okButton.Enabled = true;
            }
        }
    }
}
