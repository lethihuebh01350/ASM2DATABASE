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

namespace ASM1DATAE_BH01350
{
    public partial class Form9 : Form
    {
        string connectstring = @"Data Source=LAPTOP-IG12I1HR\SQLEXPRESS;Initial Catalog=asm22;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectstring);
            try
            {
                con.Open();
                cmd = cmd = new SqlCommand("select * from Customer", con);
                adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure that the click is on a valid row (not header or empty)
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Retrieve values from the selected row and display them on the form
                textBox1.Text = selectedRow.Cells[0].Value.ToString();         // ID Customer
                textBox2.Text = selectedRow.Cells[1].Value.ToString();       // Name Customer
                if (selectedRow.Cells[2].Value.ToString() == "FEMALE")
                {
                    rad_fema.Checked = true;  // Set Male gender radio button
                }
                else if (selectedRow.Cells[2].Value.ToString() == "MALE")
                {
                    rad_male.Checked = true;  // Set Female gender radio button
                }
                textBox6.Text = selectedRow.Cells[3].Value.ToString();        // Date of Birth
                textBox3.Text = selectedRow.Cells[4].Value.ToString();      // Phone Number
                textBox4.Text = selectedRow.Cells[5].Value.ToString();      // Email
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string CustomerID = textBox1.Text;
                string CustomerName = textBox2.Text;
                string Gender = (rad_fema.Checked) ? "FEMALE" : "MALE";
                DateTime BirthofDate;
                string Phone = textBox3.Text;
                string Gmail = textBox4.Text;

                // Chuyển đổi chuỗi ngày thành kiểu DateTime
                if (!DateTime.TryParse(textBox6.Text, out BirthofDate))
                {
                    MessageBox.Show("Invalid date format. Please use yyyy-MM-dd.");
                    return;
                }

                string updateQuery = "UPDATE Customer SET CustomerName = @CustomerName, Gender = @Gender, Birth = @Birth, Phone = @Phone, Address = @Address WHERE CustomerID = @CustomerID";

                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                    cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    cmd.Parameters.AddWithValue("@Birth", BirthofDate);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@Address", Gmail);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // Làm mới DataGridView
                        LoadCustomerData();
                        MessageBox.Show("Customer updated successfully!");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("SQL Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update");
            }
        }
        private void LoadCustomerData()
        {
            dt.Clear();
            adt.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy giá trị CustomerID của dòng được chọn
                string CustomerID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                // Xác nhận trước khi xóa
                var confirmResult = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Tạo câu lệnh SQL DELETE
                    string deleteQuery = "DELETE FROM Customer WHERE CustomerID = @CustomerID";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            // Làm mới lại DataGridView
                            LoadCustomerData();
                            MessageBox.Show("Customer deleted successfully!");
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("SQL Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            Form2.Show();
        }

        private void rad_fema_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
