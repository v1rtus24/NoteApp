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
    public partial class AddEditForm : Form
    {
        public bool EditNote { get; set; }
        public Note CurrentNote { get; set; }
        public AddEditForm()
        {
            InitializeComponent();
            comboBox1.Items.Add(noteCategory.Work.ToString());
            comboBox1.Items.Add(noteCategory.Home.ToString());
            comboBox1.Items.Add(noteCategory.HealthAndSports.ToString());
            comboBox1.Items.Add(noteCategory.People.ToString());
            comboBox1.Items.Add(noteCategory.Documents.ToString());
            comboBox1.Items.Add(noteCategory.Finance.ToString());
            comboBox1.Items.Add(noteCategory.Other.ToString());


        }

        private void okButton_Click(object sender, EventArgs e)
        {

            if (EditNote == false)
            {
                try
                {
                    var curentCategory = (noteCategory)comboBox1.SelectedIndex;
                    CurrentNote = new Note(titleTextBox.Text, noteTextTextBox.Text, curentCategory);
                    DialogResult = DialogResult.OK;
                }
                catch(ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    CurrentNote.Name = titleTextBox.Text;
                CurrentNote.NoteText = noteTextTextBox.Text;
                CurrentNote.NoteCategory = (noteCategory)comboBox1.SelectedIndex;
                CurrentNote.ModifiedTime = DateTime.Now;
                DialogResult = DialogResult.OK;
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Add_EditForm_Load(object sender, EventArgs e)
        {
            if (EditNote == true)
            {
                titleTextBox.Text = CurrentNote.Name;
                noteTextTextBox.Text = CurrentNote.NoteText;
                comboBox1.Text = CurrentNote.NoteCategory.ToString();
                dateTimePicker1.Value = CurrentNote.CreatedTime;
                dateTimePicker2.Value = CurrentNote.ModifiedTime;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
