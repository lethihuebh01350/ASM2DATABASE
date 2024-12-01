using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ASM1DATAE_BH01350
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=LAPTOP-IG12I1HR\SQLEXPRESS;Initial Catalog=asm22;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection con;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
            //AddAdminAccount();
        }
        private void AddAdminAccount()
        {
            string adminUserName = "Admin"; // Tên tài khoản admin
            string adminPassword = "@Admin123"; // Mật khẩu của admin
            // Tạo Salt ngẫu nhiên
            string salt = GenerateSalt();
            // Băm mật khẩu với Salt
            string hashedPassword = HashPassword(adminPassword, salt);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (UserName, PasswordHash, Salt, Role) VALUES (@UserName, @PasswordHash, @Salt, 'Admin')";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@UserName", adminUserName);
                command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                command.Parameters.AddWithValue("@Salt", salt);
                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Admin account added successfully.");

            }// Truyền tham số
        }

        private void btn_regis_Click(object sender, EventArgs e)
        {
            Form0 form0 = new Form0();
            form0.Show();
        }

        private void btlogin1_Click(object sender, EventArgs e)
        { 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT PasswordHash, Salt, Role, CustomerID FROM Users WHERE UserName = @UserName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("UserName", txtB_name.Text);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string storedHash = reader["PasswordHash"].ToString();
                    string storedSalt = reader["Salt"].ToString();
                    string role = reader["Role"].ToString();
                    int customerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0;

                    // Hash lại mật khẩu nhập vào với salt từ database
                    string hashedInputPassword = HashPassword(txtB_pass.Text, storedSalt);

                    if (hashedInputPassword == storedHash)
                    {
                        MessageBox.Show("Login successful!");

                        if (role == "Admin")
                        {
                            Form2 form2 = new Form2();
                            form2.Show();
                        }
                        else
                        {
                            // Mở Form khách hàng với tên khách hàng
                            Form7 form7 = new Form7(customerID);
                            form7.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong password!");
                    }
                }
                else
                {
                    MessageBox.Show("Account does not exist!");
                }
            }
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
    }
}
