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
        public Project project { get; set; }

        /// <summary>
        /// Список отсортированных заметок
        /// </summary>
        public Project projectSort { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>        
        public MainForm()
        {
            InitializeComponent();
            project = ProjectManager.LoadFromFile(ProjectManager.FilePath);
            projectSort = new Project();
            UpdateListBox();
            var categories = Enum.GetValues(typeof(NoteCategory)).Cast<object>().ToArray();
            CategoryComboBox.Items.Add("All");
            CategoryComboBox.Items.AddRange(categories);
            if (project.CurrentNote == -1)
                ClearInfo();
            else
                NotesListBox.SelectedIndex = project.CurrentNote;
        }

        /// <summary>
        /// Метод, который обновляет список заметок в листбоксе
        /// </summary>
        private void UpdateListBox()
        {
            NotesListBox.Items.Clear();
            if ((CategoryComboBox.Text == "All"))
            {
                project.Notes = project.SortList();
                for (int i = 0; i < project.Notes.Count; i++)
                {
                    project.Notes[i].IndexNote = i;
                    NotesListBox.Items.Add(project.Notes[i].Name);
                }
            }
            else
            {
                var category = (NoteCategory)CategoryComboBox.SelectedIndex - 1;
                projectSort.Notes = project.SortList(category);
                for (int i = 0; i < projectSort.Notes.Count; i++)
                {
                    NotesListBox.Items.Add(projectSort.Notes[i].Name);
                }
            }
        }

        /// <summary>
        /// Метод, для отображения информации заметки
        /// </summary>
        private void ShowNoteInfo()
        {
            if (CategoryComboBox.Text == "All")
            {
                var note = project.Notes[NotesListBox.SelectedIndex];
                TitleLabel.Text = note.Name;
                notesTextBox.Text = note.Text;
                CreatedDateTimePicker.Value = note.CreatedTime;
                ModifiedDateTimePicker.Value = note.ModifiedTime;
                CategoryLabel.Text = note.Category.ToString();
            }
            else
            {
                var note = projectSort.Notes[NotesListBox.SelectedIndex];
                TitleLabel.Text = note.Name;
                notesTextBox.Text = note.Text;
                CreatedDateTimePicker.Value = note.CreatedTime;
                ModifiedDateTimePicker.Value = note.ModifiedTime;
                CategoryLabel.Text = note.Category.ToString();
            }
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
            catch
            {
                return;
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
                project.Notes.Add(form.CurrentNote);
                UpdateListBox();
                if (NotesListBox.Items.Count > 0)
                    NotesListBox.SelectedIndex = 0;
                else
                    return;
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
                if (CategoryComboBox.Text == "All")
                {
                    NoteForm form = new NoteForm();
                    form.CurrentNote = project.Notes[NotesListBox.SelectedIndex];

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        project.Notes[NotesListBox.SelectedIndex].Name = form.CurrentNote.Name;
                        project.Notes[NotesListBox.SelectedIndex].Text = form.CurrentNote.Text;
                        project.Notes[NotesListBox.SelectedIndex].Category = form.CurrentNote.Category;
                        project.Notes[NotesListBox.SelectedIndex].ModifiedTime = form.CurrentNote.ModifiedTime;
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
                    }
                }
                else
                {
                    NoteForm form = new NoteForm();
                    form.CurrentNote = projectSort.Notes[NotesListBox.SelectedIndex];
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        projectSort.Notes[NotesListBox.SelectedIndex].Name = form.CurrentNote.Name;
                        projectSort.Notes[NotesListBox.SelectedIndex].Text = form.CurrentNote.Text;
                        projectSort.Notes[NotesListBox.SelectedIndex].Category = form.CurrentNote.Category;
                        projectSort.Notes[NotesListBox.SelectedIndex].ModifiedTime = form.CurrentNote.ModifiedTime;
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
                    }
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
                if (CategoryComboBox.Text == "All")
                {
                    DialogResult result = MessageBox.Show("Do you want to remove this note: " + project.Notes[NotesListBox.SelectedIndex].Name + "", "Remove Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                    {
                        project.Notes.Remove(project.Notes[NotesListBox.SelectedIndex]);

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
                }
                else
                {
                    DialogResult result = MessageBox.Show("Do you want to remove this note: " + projectSort.Notes[NotesListBox.SelectedIndex].Name + "", "Remove Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                    {
                        for (int i = 0; i < project.Notes.Count; i++)
                        {
                            if (project.Notes[i].IndexNote == projectSort.Notes[NotesListBox.SelectedIndex].IndexNote)
                            {
                                project.Notes.RemoveAt(i);
                            }
                        }
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
            project.CurrentNote = NotesListBox.SelectedIndex;
            ProjectManager.SaveToFile(project, ProjectManager.FilePath);
        }

        /// <summary>
        /// Закрытие формы, через кнопку на тулстрипе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            project.CurrentNote = NotesListBox.SelectedIndex;
            ProjectManager.SaveToFile(project, ProjectManager.FilePath);
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
            else
                return;
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
