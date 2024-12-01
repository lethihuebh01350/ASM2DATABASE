using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM1DATAE_BH01350
{
    public partial class Form0 : Form
    {
        
        public Form0()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnaccept_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=LAPTOP-IG12I1HR\SQLEXPRESS;Initial Catalog=asm22;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Lấy giới tính từ Radio Buttons
                string Gender = "";
                if (rad_male.Checked)
                {
                    Gender = "Male";
                }
                else if (rad_fema.Checked)
                {
                    Gender = "Female";
                }
                else
                {
                    MessageBox.Show("Please select gender..!");
                    return; // Dừng lại nếu chưa chọn giới tính
                }

                // Thêm thông tin khách hàng vào bảng Customer
                string insertCustomerQuery = "INSERT INTO Customer (CustomerName, Gender, Birth, Phone, Email) " +
                                             "VALUES (@CustomerName, @Gender, @Birth, @Phone, @Email); " +
                                             "SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(insertCustomerQuery, connection);
                command.Parameters.AddWithValue("@CustomerName", txtB_userName.Text);
                command.Parameters.AddWithValue("@Gender", Gender);
                command.Parameters.AddWithValue("@Birth", dtp_time.Value);
                command.Parameters.AddWithValue("@Phone", textBoxPhone.Text);
                command.Parameters.AddWithValue("@Email", textBoxEmail.Text);

                // Lấy CustomerID mới thêm
                int customerId = Convert.ToInt32(command.ExecuteScalar());

                // Thêm tài khoản đăng nhập vào bảng Users
                string salt = GenerateSalt();
                string hashedPassword = HashPassword(textBoxPassword.Text, salt);
                string insertUserQuery = "INSERT INTO Users (UserName, PasswordHash, Salt, Role, CustomerID) " +
                                         "VALUES (@UserName, @PasswordHash, @Salt, 'User', @CustomerID)";

                SqlCommand userCommand = new SqlCommand(insertUserQuery, connection);
                userCommand.Parameters.AddWithValue("@UserName", textBoxCustomerName.Text);
                userCommand.Parameters.AddWithValue("@PasswordHash", hashedPassword); // Hàm HashPassword cần được cài đặt
                userCommand.Parameters.AddWithValue("@Salt", salt); // Hàm GenerateSalt cần được cài đặt
                userCommand.Parameters.AddWithValue("@CustomerID", customerId);

                userCommand.ExecuteNonQuery();
                MessageBox.Show("Register Successfull!");
            }
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        private static string GenerateSalt()
        {
            // Tạo mảng byte ngẫu nhiên để làm salt
            byte[] saltBytes = new byte[16];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            // Chuyển đổi mảng byte sang chuỗi Base64
            return Convert.ToBase64String(saltBytes);
        }
        private static string HashPassword(string password, string salt)
        {
            // Kết hợp mật khẩu với salt
            string saltedPassword = password + salt;

            // Chuyển đổi chuỗi mật khẩu đã kết hợp thành mảng byte
            byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);

            // Sử dụng SHA256 để băm mật khẩu
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);
                // Chuyển đổi mảng byte thành chuỗi Base64
                return Convert.ToBase64String(hashBytes);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
