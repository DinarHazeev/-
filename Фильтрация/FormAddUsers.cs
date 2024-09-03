using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Фильтрация.DBContext;

namespace Фильтрация
{
    public partial class FormAddUsers : Form
    {
        public FormAddUsers()
        {
            InitializeComponent();
        }
        Model1 model = new Model1();
        private void FormAddUsers_Load(object sender, EventArgs e)
        {
            //загружаем данные в источник данных

        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //проверка входных данных
            Regex reg = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);
            if (!reg.IsMatch(textBox4.Text))
            {
                MessageBox.Show("Почта не соотвествует требованиям!");
                return;
            }
            if (!textBox2.Text.Equals(textBox3.Text))
            {
                MessageBox.Show("Пароли не равны!");
                return;
            }
            if (String.IsNullOrWhiteSpace(textBox1.Text) ||
            String.IsNullOrWhiteSpace(textBox2.Text) ||
            String.IsNullOrWhiteSpace(textBox3.Text) ||
            String.IsNullOrWhiteSpace(textBox6.Text) ||
            !maskedTextBox1.MaskCompleted)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            //Заполнение данных о новом пользователе
            Users users = new Users();
            users.ID = 0;
            users.Login = textBox1.Text;
            users.Password = textBox2.Text;
            users.Email = textBox4.Text;
            users.Phone = maskedTextBox1.Text;
            users.First_Name = textBox5.Text;
            users.Second_Name = textBox6.Text;
            users.RoleID = (int)comboBox1.SelectedValue;
            users.Gender = radioButton1.Checked ? "Мужской" : "Женский";
            try
            {
                //сохранение данных в БД
                model.Users.Add(users);
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Данные добавленны!");
            Close();
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void FormAddUsers_Load_1(object sender, EventArgs e)
        {
            rolesBindingSource.DataSource = model.Roles.ToList();

        }

    }
}
