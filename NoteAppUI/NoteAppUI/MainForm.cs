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
    public partial class MainForm : Form
    {
        public Project project1 { get; set; }
        public int idNote { get; set; }
        
        
        public MainForm()
        {
            InitializeComponent();
            

        }
        private void UpdateListBox()
        {
            project1 = ProjectManager.LoadFromFile();
            NotesListBox.Items.Clear();
            for (int i = 0; i < project1.Notes.Count; i++)
            {
                NotesListBox.Items.Add(project1.Notes[i].Name);
            }
        }
        private void ClearInfo()
        {
            TitleLabel.Text = "";
            notesTextBox.Text = "";
            CategoryLabel.Text = "";
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        private void NotesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            idNote = NotesListBox.SelectedIndex;
            if (idNote != -1)
            {
                TitleLabel.Text = project1.Notes[idNote].Name;
                notesTextBox.Text = project1.Notes[idNote].NoteText;
                CreatedDateTimePicker.Value = project1.Notes[idNote].CreatedTime;
                ModifiedDateTimePicker.Value = project1.Notes[idNote].ModifiedTime;
                CategoryLabel.Text = project1.Notes[idNote].NoteCategory.ToString();

            }
        }

        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
                AddEditForm f = new AddEditForm();
                f.EditNote = false;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    project1.Notes.Add(f.CurrentNote);
                    ProjectManager.SaveToFile(project1);
                    UpdateListBox();
                    ClearInfo();
                }
        }

        private void editNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NotesListBox.SelectedIndex != -1)
            {
                AddEditForm f = new AddEditForm();
                f.EditNote = true;
                f.CurrentNote = project1.Notes[idNote];
                
                if (f.ShowDialog() == DialogResult.OK)
                {
                    project1.Notes[idNote].Name = f.CurrentNote.Name;
                    project1.Notes[idNote].NoteText = f.CurrentNote.NoteText;
                    project1.Notes[idNote].NoteCategory = f.CurrentNote.NoteCategory;
                    project1.Notes[idNote].ModifiedTime = f.CurrentNote.ModifiedTime;
                    ProjectManager.SaveToFile(project1);
                    UpdateListBox();
                    TitleLabel.Text = project1.Notes[idNote].Name;
                    notesTextBox.Text = project1.Notes[idNote].NoteText;
                    CreatedDateTimePicker.Value = project1.Notes[idNote].CreatedTime;
                    ModifiedDateTimePicker.Value = project1.Notes[idNote].ModifiedTime;
                    CategoryLabel.Text = project1.Notes[idNote].NoteCategory.ToString();

                }
            }
            else
            {
                MessageBox.Show("Не выбрана заметка!");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm f = new AboutForm();
            f.Show();
        }

        private void removeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NotesListBox.SelectedIndex != -1)
            {
                

                DialogResult result = MessageBox.Show("Do you want to remove this note: " + project1.Notes[idNote].Name + "", "Remove Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    project1.Notes.Remove(project1.Notes[idNote]);
                    ClearInfo();
                    ProjectManager.SaveToFile(project1);
                    UpdateListBox();
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

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProjectManager.SaveToFile(project1);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectManager.SaveToFile(project1);
            Application.Exit();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            addNoteToolStripMenuItem_Click(sender, e);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            editNoteToolStripMenuItem_Click(sender, e);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            removeNoteToolStripMenuItem_Click(sender, e);
        }
    }
}
