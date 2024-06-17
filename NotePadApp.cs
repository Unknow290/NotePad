using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePad // notepad
{
    public partial class NotePad : Form
    {
        private bool isSaved;
        public NotePad()
        {
            InitializeComponent();
        }

        private void New_Click(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                var result = MessageBox.Show("You have unsaved changes. Do you want to save them before creating a new note?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    NewNote();
                }
                else if (result == DialogResult.No)
                {
                    NewNote(); 
                }
            }
            else
            {
                NewNote(); 
            }
        }

        private void NewNote()
        {
            NotePad np = new NotePad();
            np.Show();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog(); 
            ofd.ShowDialog();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.ShowDialog();
        }

        private void Save_As_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd2 = new SaveFileDialog();
            sfd2.ShowDialog();
        }

        private void Print_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.ShowDialog();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}