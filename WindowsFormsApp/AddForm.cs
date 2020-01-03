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
    public delegate void Action();
    public enum EnumAction {Add, Update};
    public partial class AddForm : Form
    {
        public Action ButtonAction;
        public Form1 MotherForm;
        private AddForm()
        {
            InitializeComponent();
            this.NameField.TextChanged += CheckStats;
            this.LastNameField.TextChanged += CheckStats;
            this.MiddleNameField.TextChanged += CheckStats;
            this.PasswordField.TextChanged += CheckStats;
            this.PasswordField.TextChanged += CheckStats;
            this.NumberField.TextChanged += CheckStats;
        }
        public AddForm(string buttonText, EnumAction buttonAction, Form1 form): this()
        {
            CreateButton.Text = buttonText;
            MotherForm = form;
            switch (buttonAction) {
                case EnumAction.Add:
                    ButtonAction = StandartAction;
                    break;
                case EnumAction.Update:
                    ButtonAction = EditAction;
                    User target;
                    using (ModelOfShop db = new ModelOfShop())
                    {
                        target = db.Users.Find(int.Parse(MotherForm.DataBaseGrid[0, MotherForm.DataBaseGrid.SelectedRows[0].Index].Value.ToString()));
                    }
                    NameField.Text = target.FirsName;
                    LastNameField.Text = target.LastName;
                    MiddleNameField.Text = target.MiddleName;
                    LoginField.Text = target.Login;
                    PasswordField.Text = target.Password;
                    CheckAdmin.Checked = (bool)target.IsAdmin;
                    NumberField.Text = target.Phone.ToString();
                    CountryCodeField.SelectedIndex = (int)target.CountryCode;
                    break;
            }
            CheckStats(this, null);
        }
        public void StandartAction()
        {
            if (ValidateFields())
            {
                CreateButton.Enabled = false;
                if (!Actions.IsIdenticaLogin(LoginField.Text))
                {
                    CreateButton.BackColor = Color.Yellow;
                    return;
                }
                Actions.AddUser(new User() { FirsName = NameField.Text, CountryCode = (short)CountryCodeField.SelectedIndex, IsAdmin = CheckAdmin.Checked, LastName = LastNameField.Text, Login = LoginField.Text, MiddleName = MiddleNameField.Text, Password = PasswordField.Text, Phone = long.Parse(NumberField.Text) });
                CreateButton.Enabled = true;
                MessageBox.Show("Succesfull", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void EditAction()
        {
            if (ValidateFields())
            {
                using (ModelOfShop db = new ModelOfShop()) {
                    User target;
                    target = db.Users.Find(int.Parse(MotherForm.DataBaseGrid[0, MotherForm.DataBaseGrid.SelectedRows[0].Index].Value.ToString()));
                    target.FirsName = NameField.Text;
                    target.LastName = LastNameField.Text;
                    target.MiddleName = MiddleNameField.Text;
                    target.Login = LoginField.Text;
                    target.Password = PasswordField.Text;
                    target.IsAdmin = CheckAdmin.Checked;
                    target.Phone = long.Parse(NumberField.Text);
                    target.CountryCode = (short)CountryCodeField.SelectedIndex;
                    db.SaveChanges();
                    MessageBox.Show("Succesfull", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void CreateButton_Click(object sender, EventArgs e)
        {
            ButtonAction();
        }
        public bool ValidateFields()
        {
            if (CountryCodeField.SelectedItem == null || NameField.Text.Length == 0 || NameField.Text.Length > 50 || LastNameField.Text.Length == 0 || LastNameField.Text.Length > 50 || MiddleNameField.Text.Length == 0 || MiddleNameField.Text.Length > 50 || LoginField.Text.Length == 0 || LoginField.Text.Length > 50 || PasswordField.Text.Length == 0 || PasswordField.Text.Length > 50 || NumberField.Text.Length != 9)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void CheckStats(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                CreateButton.BackColor = Color.Red;
            } else
            {
                CreateButton.BackColor = Color.White;
            }
        }
    }
}
