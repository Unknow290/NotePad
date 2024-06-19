using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NotePad // notepad!!
{
    public partial class NotePad : Form
    {
        private bool isSaved;
        private string currentFilePath;
        public NotePad()
        {
            InitializeComponent();
        }

        private void New_Click(object sender, EventArgs e)
        {
            if (!isSaved || richTextBox1.Text.Length >= 1) // checks to see if you want to save your work
            {
                exitPrompt();

                if (DialogResult == DialogResult.Yes)
                {
                    SaveFile(); 
                    richTextBox1.Text = String.Empty;
                }
                else if (DialogResult == DialogResult.No)
                {
                    this.Hide();
                    NewNote();
                }
            }
            else //if work has already been saved
            {
                this.Hide();
                NewNote();
            }
        }
        private void Open_Click(object sender, EventArgs e) // opens an existing file
        {
            if (richTextBox1.Text.Length >= 1) // checks to see if you want to save your work
            {
                exitPrompt();

                if (DialogResult == DialogResult.Yes)
                {
                    SaveFile();
                    richTextBox1.Text = String.Empty;
                }
                else if (DialogResult == DialogResult.No)
                {
                    OpenNote();
                  
                }
            }
            else
            {
                OpenNote();
            }
            
        }
        

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void Save_As_Click(object sender, EventArgs e)
        {
            SaveAsFile();
        }
        private void Print_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.ShowDialog();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length >= 1) // checks to see if you want to save your work
            {
                exitPrompt();

                if (DialogResult == DialogResult.Yes)
                {
                    SaveFile();
                    this.Close();

                }
                else if (DialogResult == DialogResult.No)
                {
                    this.Close(); 
                }
            }
            else 
            { 
                this.Close(); 
            }
        }

        private void OpenNote() //opens note
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = ofd.FileName;
                using (StreamReader sr = new StreamReader(currentFilePath))
                {
                    richTextBox1.Text = sr.ReadToEnd();
                }
                isSaved = true;
            }
        }
        private void NewNote() //creates new note
        {
            NotePad np = new NotePad();
            np.Closed += (s, args) => this.Close(); //closes all hiden notes
            np.Show();
        }
        private void exitPrompt() //exit message
        {
            DialogResult = MessageBox.Show("Do you want to save current file?",
                "Notepad",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }
        private void SaveAsFile() // Saves as / code behind the save file
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = sfd.FileName;
                SaveFile(currentFilePath);
            }
        }
        private void SaveFile(string filePath = null) // saves file
        {
            if (filePath == null)
            {
                SaveAsFile();
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.Write(richTextBox1.Text);
                }
                isSaved = true;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo(); // undo
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = true;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo(); // redo
            undoToolStripMenuItem.Enabled = true;
            redoToolStripMenuItem.Enabled = false;
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText); //cut
            richTextBox1.SelectedText = string.Empty;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText); //copy
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string xx = Clipboard.GetText();  //paste
            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, xx);
        }
    }
}