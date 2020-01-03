using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBase;
namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string ReadWriteLine(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (DataBaseGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("No selected row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AddForm addForm = new AddForm("Редагувати", EnumAction.Update, this);
            addForm.Show(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.testDataSet.Users);
        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm("Створити", EnumAction.Add, this);
            addForm.Show(this);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            this.usersTableAdapter.Fill(this.testDataSet.Users);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (DataBaseGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("No selected row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (ModelOfShop db = new ModelOfShop())
            {
                foreach (DataGridViewRow row in DataBaseGrid.SelectedRows)
                {

                    db.Users.Remove(db.Users.Find(int.Parse(DataBaseGrid[0, row.Index].Value.ToString())));

                }
                db.SaveChanges();
            }
            this.usersTableAdapter.Fill(this.testDataSet.Users);
        }
    }
}
