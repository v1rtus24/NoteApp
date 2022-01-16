﻿using System;
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

        public Project ProjectSort { get; set; }

        /// <summary>
        /// Поле, которое содержит индекс заметки
        /// </summary>
        public int CurrentNoteIndex { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>        
        public MainForm()
        {
            InitializeComponent();
            Project = ProjectManager.LoadFromFile(ProjectManager.FilePath);
            ProjectSort = new Project();
            UpdateListBox();
            var categories = Enum.GetValues(typeof(NoteCategory)).Cast<object>().ToArray();
            CategoryComboBox.Items.Add("All");
            CategoryComboBox.Items.AddRange(categories);
        }

        /// <summary>
        /// Метод, который обновляет список заметок в листбоксе
        /// </summary>
        private void UpdateListBox()
        {
            Project.Notes = Project.SortList();
            NotesListBox.Items.Clear();
            if (CategoryComboBox.Text == "All")
            {
                for (int i = 0; i < Project.Notes.Count; i++)
                {
                    NotesListBox.Items.Add(Project.Notes[i].Name);
                }
            }
            else
            {
                ProjectSort.Notes = Project.SortList((NoteCategory)CategoryComboBox.SelectedItem);
                for (int i = 0; i < ProjectSort.Notes.Count; i++)
                {
                    NotesListBox.Items.Add(ProjectSort.Notes[i].Name);
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
                var note = Project.Notes[CurrentNoteIndex];
                TitleLabel.Text = note.Name;
                notesTextBox.Text = note.Text;
                CreatedDateTimePicker.Value = note.CreatedTime;
                ModifiedDateTimePicker.Value = note.ModifiedTime;
                CategoryLabel.Text = note.Category.ToString();
            }
            else
            {
                var note = ProjectSort.Notes[CurrentNoteIndex];
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
                CurrentNoteIndex = NotesListBox.SelectedIndex;
                Project.CurrentNote = Project.Notes[CurrentNoteIndex];
                if (CurrentNoteIndex == -1)
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
                Project.Notes.Add(form.CurrentNote);
                UpdateListBox();
                NotesListBox.SelectedIndex = 0;
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
                form.CurrentNote = Project.Notes[CurrentNoteIndex];

                if (form.ShowDialog() == DialogResult.OK)
                {
                    Project.Notes[CurrentNoteIndex].Name = form.CurrentNote.Name;
                    Project.Notes[CurrentNoteIndex].Text = form.CurrentNote.Text;
                    Project.Notes[CurrentNoteIndex].Category = form.CurrentNote.Category;
                    Project.Notes[CurrentNoteIndex].ModifiedTime = form.CurrentNote.ModifiedTime;
                    UpdateListBox();
                    ShowNoteInfo();
                    NotesListBox.SelectedIndex = 0;
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

                DialogResult result = MessageBox.Show("Do you want to remove this note: " + Project.Notes[CurrentNoteIndex].Name + "", "Remove Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    Project.Notes.Remove(Project.Notes[CurrentNoteIndex]);
                    UpdateListBox();
                    if (NotesListBox.Items.Count > 0)
                    {
                        NotesListBox.SelectedIndex = 0;
                    }
                    else
                    {
                        TitleLabel.Text = "";
                        notesTextBox.Text = "";
                        CreatedDateTimePicker.Value = DateTime.Now;
                        ModifiedDateTimePicker.Value = DateTime.Now;
                        CategoryLabel.Text = "";
                    }
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
            ProjectManager.SaveToFile(Project,ProjectManager.FilePath);
        }

        /// <summary>
        /// Закрытие формы, через кнопку на тулстрипе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectManager.SaveToFile(Project,ProjectManager.FilePath);
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
            if (CategoryComboBox.Text != "All")
            {
                AddButton.Enabled = false;
                EditButton.Enabled = false;
                DeleteButton.Enabled = false;
                UpdateListBox();
            }
            else
            {
                AddButton.Enabled = true;
                EditButton.Enabled = true;
                DeleteButton.Enabled = true;
                UpdateListBox();
            }
        }
    }
}
