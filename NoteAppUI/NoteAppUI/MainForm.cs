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

        Project listNotes = new Project();
        Project listNotes1 = new Project();

        public MainForm()
        {
            InitializeComponent();
   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                listNotes.Notes.Add(new Note(textBox1.Text, textBox2.Text, (noteCategory)Convert.ToInt32(textBox3.Text)));
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            for (int i = 0;i < listNotes.Notes.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = listNotes.Notes[i].Name;
                    dataGridView1[1, i].Value = listNotes.Notes[i].NoteText;
                    dataGridView1[2, i].Value = listNotes.Notes[i].NoteCategory;
                    dataGridView1[3, i].Value = listNotes.Notes[i].TimeOfCreation;
                    dataGridView1[4, i].Value = listNotes.Notes[i].TimeOfLastEdit;            
                }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ProjectManager.SaveToFile(listNotes);
        }

      

        private void button5_Click(object sender, EventArgs e)
        {
            listNotes1 = ProjectManager.LoadFromFile();
            for (int i = 0; i < listNotes1.Notes.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = listNotes1.Notes[i].Name;
                dataGridView1[1, i].Value = listNotes1.Notes[i].NoteText;
                dataGridView1[2, i].Value = listNotes1.Notes[i].NoteCategory;
                dataGridView1[3, i].Value = listNotes1.Notes[i].TimeOfCreation;
                dataGridView1[4, i].Value = listNotes1.Notes[i].TimeOfLastEdit;
            }
        }
    }
}
