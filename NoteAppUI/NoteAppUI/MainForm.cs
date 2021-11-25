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
        public Project Project1 { get; set; }
        public int IdNote { get; set; }
        
        
        public MainForm()
        {
            InitializeComponent();
            

        }
        private void UpdateListBox()
        {
            Project1 = ProjectManager.LoadFromFile();
            NotesListBox.Items.Clear();
            for (int i = 0; i < Project1.Notes.Count; i++)
            {
                NotesListBox.Items.Add(Project1.Notes[i].Name);
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
            ClearInfo();
            UpdateListBox();
        }

        private void NotesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdNote = NotesListBox.SelectedIndex;
            if (IdNote != -1)
            {
                TitleLabel.Text = Project1.Notes[IdNote].Name;
                notesTextBox.Text = Project1.Notes[IdNote].Text;
                CreatedDateTimePicker.Value = Project1.Notes[IdNote].CreatedTime;
                ModifiedDateTimePicker.Value = Project1.Notes[IdNote].ModifiedTime;
                CategoryLabel.Text = Project1.Notes[IdNote].Category.ToString();

            }
        }

        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
                AddEditForm f = new AddEditForm();
                f.EditNote = false;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Project1.Notes.Add(f.CurrentNote);
                    ProjectManager.SaveToFile(Project1);
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
                f.CurrentNote = Project1.Notes[IdNote];
                
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Project1.Notes[IdNote].Name = f.CurrentNote.Name;
                    Project1.Notes[IdNote].Text = f.CurrentNote.Text;
                    Project1.Notes[IdNote].Category = f.CurrentNote.Category;
                    Project1.Notes[IdNote].ModifiedTime = f.CurrentNote.ModifiedTime;
                    ProjectManager.SaveToFile(Project1);
                    UpdateListBox();
                    TitleLabel.Text = Project1.Notes[IdNote].Name;
                    notesTextBox.Text = Project1.Notes[IdNote].Text;
                    CreatedDateTimePicker.Value = Project1.Notes[IdNote].CreatedTime;
                    ModifiedDateTimePicker.Value = Project1.Notes[IdNote].ModifiedTime;
                    CategoryLabel.Text = Project1.Notes[IdNote].Category.ToString();
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

                DialogResult result = MessageBox.Show("Do you want to remove this note: " + Project1.Notes[IdNote].Name + "", "Remove Note", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    Project1.Notes.Remove(Project1.Notes[IdNote]);
                    ClearInfo();
                    ProjectManager.SaveToFile(Project1);
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
            ProjectManager.SaveToFile(Project1);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectManager.SaveToFile(Project1);
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
