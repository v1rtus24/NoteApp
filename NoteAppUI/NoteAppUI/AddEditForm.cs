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
            comboBox1.Items.Add(NoteCategory.Work.ToString());
            comboBox1.Items.Add(NoteCategory.Home.ToString());
            comboBox1.Items.Add(NoteCategory.HealthAndSports.ToString());
            comboBox1.Items.Add(NoteCategory.People.ToString());
            comboBox1.Items.Add(NoteCategory.Documents.ToString());
            comboBox1.Items.Add(NoteCategory.Finance.ToString());
            comboBox1.Items.Add(NoteCategory.Other.ToString());
        }

        private void okButton_Click(object sender, EventArgs e)
        {

            if (EditNote == false)
            {
                try
                {
                    if (comboBox1.SelectedIndex != -1)
                    {
                    NoteCategory curentCategory = (NoteCategory)comboBox1.SelectedIndex;
                    CurrentNote = new Note(titleTextBox.Text, noteTextTextBox.Text, curentCategory);
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
            else
            {
                try
                {
                        CurrentNote.Name = titleTextBox.Text;
                        CurrentNote.Text = noteTextTextBox.Text;
                        CurrentNote.Category = (NoteCategory)comboBox1.SelectedIndex;
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
                noteTextTextBox.Text = CurrentNote.Text;
                comboBox1.Text = CurrentNote.Category.ToString();
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
