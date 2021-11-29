using System;
using System.Collections;
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
    /// <summary>
    /// Класс основной формы
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Поле, содержащее список заметок
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// Поле, которое содержит индекс заметки
        /// </summary>
        public int IndexCurrentNote { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>        
        public MainForm()
        {
            InitializeComponent();
            Project = ProjectManager.LoadFromFile();
        }

        /// <summary>
        /// Метод, который обновляет список заметок в листбоксе
        /// </summary>
        private void UpdateListBox()
        {
            NotesListBox.Items.Clear();
            for (int i = 0; i < Project.Notes.Count; i++)
            {
                NotesListBox.Items.Add(Project.Notes[i].Name);
            }
        }

        /// <summary>
        /// Метод при загрузке формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        /// <summary>
        /// Метод, для отображения информации заметки
        /// </summary>
        private void ShowNoteInfo()
        {
            TitleLabel.Text = Project.Notes[IndexCurrentNote].Name;
            notesTextBox.Text = Project.Notes[IndexCurrentNote].Text;
            CreatedDateTimePicker.Value = Project.Notes[IndexCurrentNote].CreatedTime;
            ModifiedDateTimePicker.Value = Project.Notes[IndexCurrentNote].ModifiedTime;
            CategoryLabel.Text = Project.Notes[IndexCurrentNote].Category.ToString();
        }

        /// <summary>
        /// Событие, для установки значения индекса заметки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IndexCurrentNote = NotesListBox.SelectedIndex;
            if (IndexCurrentNote == -1)
            {
                return;
            }
            ShowNoteInfo();
        }

        /// <summary>
        /// Метод, для добавления заметки
        /// </summary>
        private void AddNote()
        {
            NoteForm form = new NoteForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Project.Notes.Add(form.CurrentNote);
                ProjectManager.SaveToFile(Project);
                UpdateListBox();
                NotesListBox.SelectedIndex = NotesListBox.Items.Count - 1;
            }
        }
        /// <summary>
        /// Кнопка добавить в верхней панели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNote();
        }

        /// <summary>
        /// Метод, для редактирования заметки
        /// </summary>
        private void EditNote()
        {
            if (NotesListBox.SelectedIndex != -1)
            {
                NoteForm form = new NoteForm();
                form.CurrentNote = Project.Notes[IndexCurrentNote];

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Project.Notes.RemoveAt(IndexCurrentNote);
                    Project.Notes.Insert(IndexCurrentNote, form.CurrentNote);
                    ProjectManager.SaveToFile(Project);
                    UpdateListBox();
                    ShowNoteInfo();
                    NotesListBox.SelectedIndex = IndexCurrentNote;
                }
            }
            else
            {
                MessageBox.Show("Не выбрана заметка!");
            }
        }

        /// <summary>
        /// Кнопка изменить в верхней панели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditNote();
        }

        /// <summary>
        /// Открытие формы About
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.Show();
        }

        /// <summary>
        /// Метод для удаления заметки
        /// </summary>
        private void RemoveNote()
        {
            if (NotesListBox.SelectedIndex != -1)
            {

                DialogResult result = MessageBox.Show("Do you want to remove this note: " + Project.Notes[IndexCurrentNote].Name + "", "Remove Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    Project.Notes.Remove(Project.Notes[IndexCurrentNote]);
                    ProjectManager.SaveToFile(Project);
                    UpdateListBox();
                    NotesListBox.SelectedIndex = 0;
                }

                if (result == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                MessageBox.Show("Не выбрана заметка!");
            }
        }
        /// <summary>
        /// Кнопка изменить в верхней панели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveNote();
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProjectManager.SaveToFile(Project);
        }

        /// <summary>
        /// Закрытие формы, через кнопку на тулстрипе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectManager.SaveToFile(Project);
            Application.Exit();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddNote();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            EditNote();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            RemoveNote();
        }
    }
}
