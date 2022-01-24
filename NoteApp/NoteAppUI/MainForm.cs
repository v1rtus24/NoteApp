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
        private Project CurrentProject { get; set; }

        /// <summary>
        /// Список отсортированных заметок
        /// </summary>
        private List<Note> NotesSort { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>        
        public MainForm()
        {
            InitializeComponent();
            CurrentProject = ProjectManager.LoadFromFile(ProjectManager.FilePath);
            NotesSort = new List<Note>();
            UpdateListBox();
            var categories = Enum.GetValues(typeof(NoteCategory)).Cast<object>().ToArray();
            CategoryComboBox.Items.Add("All");
            CategoryComboBox.Items.AddRange(categories);
            if (CurrentProject.CurrentNote == -1)
                ClearInfo();
            else
                NotesListBox.SelectedIndex = CurrentProject.CurrentNote;
        }

        /// <summary>
        /// Метод, который обновляет список заметок в листбоксе
        /// </summary>
        private void UpdateListBox()
        {
            NotesListBox.Items.Clear();
            if ((CategoryComboBox.Text == "All"))
            {
                CurrentProject.Notes = CurrentProject.SortList();
                for (int i = 0; i < CurrentProject.Notes.Count; i++)
                {
                    NotesListBox.Items.Add(CurrentProject.Notes[i].Name);
                }
            }
            else
            {
                var category = (NoteCategory)CategoryComboBox.SelectedIndex - 1;
                NotesSort = CurrentProject.SortList(category);
                for (int i = 0; i < NotesSort.Count; i++)
                {
                    NotesListBox.Items.Add(NotesSort[i].Name);
                }
            }
        }

        /// <summary>
        /// Метод, для отображения информации заметки
        /// </summary>
        private void ShowNoteInfo()
        {
            var showProject = CategoryComboBox.Text == "All" ? CurrentProject.Notes : NotesSort;
            
            var note = showProject[NotesListBox.SelectedIndex];
            TitleLabel.Text = note.Name;
            notesTextBox.Text = note.Text;
            CreatedDateTimePicker.Value = note.CreatedTime;
            ModifiedDateTimePicker.Value = note.ModifiedTime;
            CategoryLabel.Text = note.Category.ToString();

        }

        /// <summary>
        /// Событие, для установки значения индекса заметки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (NotesListBox.SelectedIndex == -1)
                {
                    return;
                }
                ShowNoteInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Метод, для добавления заметки
        /// </summary>
        private void AddNote()
        {
            NoteForm form = new NoteForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                CurrentProject.Notes.Add(form.CurrentNote);
                UpdateListBox();
                if (NotesListBox.Items.Count > 0)
                    NotesListBox.SelectedIndex = 0;
                ProjectManager.SaveToFile(CurrentProject, ProjectManager.FilePath);
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
                var showProject = CategoryComboBox.Text == "All" ? CurrentProject.Notes : NotesSort;

                NoteForm form = new NoteForm();
                form.CurrentNote = showProject[NotesListBox.SelectedIndex];

                if (form.ShowDialog() == DialogResult.OK)
                {
                    showProject[NotesListBox.SelectedIndex].Name = form.CurrentNote.Name;
                    showProject[NotesListBox.SelectedIndex].Text = form.CurrentNote.Text;
                    showProject[NotesListBox.SelectedIndex].Category = form.CurrentNote.Category;
                    showProject[NotesListBox.SelectedIndex].ModifiedTime = form.CurrentNote.ModifiedTime;
                    UpdateListBox();
                    if (NotesListBox.Items.Count > 0)
                    {
                        NotesListBox.SelectedIndex = 0;
                    }
                    else
                    {
                        ClearInfo();
                        return;
                    }

                    ShowNoteInfo();
                    ProjectManager.SaveToFile(CurrentProject, ProjectManager.FilePath);
                }
            }
            else
            {
                MessageBox.Show("Note not selected!");
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
                DialogResult result = MessageBox.Show("Do you want to remove this note: " + 
                                                      CurrentProject.Notes[NotesListBox.SelectedIndex].Name + "", 
                    "Remove Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    CurrentProject.Notes.RemoveAt(NotesListBox.SelectedIndex);
                    UpdateListBox();
                    if (NotesListBox.Items.Count > 0)
                    {
                        NotesListBox.SelectedIndex = 0;
                    }
                    else if (NotesListBox.Items.Count == 0)
                    {
                        ClearInfo();
                    }
                }
                if (result == DialogResult.Cancel)
                {
                    DialogResult = DialogResult.Cancel;
                }
                ProjectManager.SaveToFile(CurrentProject, ProjectManager.FilePath);
            }
            else
            {
                MessageBox.Show("Note not selected!");
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
            CurrentProject.CurrentNote = NotesListBox.SelectedIndex;
            ProjectManager.SaveToFile(CurrentProject, ProjectManager.FilePath);
        }

        /// <summary>
        /// Закрытие формы, через кнопку на тулстрипе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentProject.CurrentNote = NotesListBox.SelectedIndex;
            ProjectManager.SaveToFile(CurrentProject, ProjectManager.FilePath);
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

        /// <summary>
        /// Выбор категории(сортировка по категории)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListBox();
            if (NotesListBox.Items.Count > 0)
            {
                NotesListBox.SelectedIndex = 0;
            }
            if (NotesListBox.Items.Count == 0)
            {
                ClearInfo();
            }
        }

        private void ClearInfo()
        {
            TitleLabel.Text = "";
            notesTextBox.Text = "";
            CreatedDateTimePicker.Value = DateTime.Now;
            ModifiedDateTimePicker.Value = DateTime.Now;
            CategoryLabel.Text = "";
        }

        /// <summary>
        /// Удаление по нажатию клавиши "Delete"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotesListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveNote();
            }
        }
    }
}
