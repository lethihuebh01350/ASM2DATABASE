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
    public partial class Form5 : Form
    {
        string connectionString = @"Data Source=LAPTOP-IG12I1HR\SQLEXPRESS;Initial Catalog=asm22;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();

        public Form5()
        {
            InitializeComponent();

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionString);
            try 
            {
                con.Open();
                cmd = new SqlCommand("SELECT * FROM Employee", con);
                adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Retrieve values from the selected row and display them on the form
                txtB_id.Text = selectedRow.Cells[0].Value.ToString();         // ID
                txtName.Text = selectedRow.Cells[1].Value.ToString();       // Name
                txtPhone.Text = selectedRow.Cells[2].Value.ToString();           // Phone Number
                txtEmail.Text = selectedRow.Cells[3].Value.ToString();           // Email
                txtPosition.Text = selectedRow.Cells[4].Value.ToString();        // Position

                // Parse hire date into DateTimePicker
                if (DateTime.TryParse(selectedRow.Cells[5].Value.ToString(), out DateTime hireDate))
                {
                    dtp_HireDate.Value = hireDate;
                }
                else
                {
                    dtp_HireDate.Value = DateTime.Now; // Default if parse fails
                }

                txtSalary.Text = selectedRow.Cells[6].Value.ToString();          // Salary
            }
        }

        private void btadd5_Click(object sender, EventArgs e)
        {
            // Validate mandatory fields
            if (string.IsNullOrWhiteSpace(txtB_id.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPosition.Text) ||
                string.IsNullOrWhiteSpace(txtSalary.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Validate ID (must be a number)
            if (!int.TryParse(txtB_id.Text, out int id))
            {
                MessageBox.Show("Employee ID must be a valid number.");
                return;
            }

            // Validate Phone (must be a number)
            if (!long.TryParse(txtPhone.Text, out long phone))
            {
                MessageBox.Show("Phone number must be a valid number.");
                return;
            }

            // Validate Email (must contain '@')
            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email must contain '@'.");
                return;
            }

            // Validate Salary (must be a valid decimal)
            if (!decimal.TryParse(txtSalary.Text, out decimal salary))
            {
                MessageBox.Show("Please enter a valid salary.");
                return;
            }

            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string position = txtPosition.Text.Trim();
            DateTime hireDate = dtp_HireDate.Value; // Use DateTimePicker for hire date

            // Create and execute the SQL INSERT command with SCOPE_IDENTITY()
            string insertQuery = "INSERT INTO Employee (Name, Phone, Email, Position, HireDate, Salary) " +
                                 "VALUES (@Name, @Phone, @Email, @Position, @HireDate, @Salary); " +
                                 "SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Position", position);
                    cmd.Parameters.AddWithValue("@HireDate", hireDate);
                    cmd.Parameters.AddWithValue("@Salary", salary);

                    try
                    {
                        con.Open();
                        // Execute the command and retrieve the new ID
                        object newId = cmd.ExecuteScalar();
                        con.Close();

                        if (newId != null)
                        {
                            MessageBox.Show($"Employee added successfully! New ID: {newId}");
                        }
                        else
                        {
                            MessageBox.Show("Failed to retrieve the new employee ID.");
                        }

                        // Refresh the DataGridView after adding
                        LoadEmployeeData();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Database error: " + ex.Message);
                    }
                }
            }
            //// Validate user inputs
            //if (string.IsNullOrWhiteSpace(txtB_id.Text) ||
            //    string.IsNullOrWhiteSpace(txtName.Text) ||
            //    string.IsNullOrWhiteSpace(txtPhone.Text) ||
            //    string.IsNullOrWhiteSpace(txtEmail.Text) ||
            //    string.IsNullOrWhiteSpace(txtPosition.Text) ||
            //    string.IsNullOrWhiteSpace(txtSalary.Text))
            //{
            //    MessageBox.Show("Please fill in all fields.");
            //    return;
            //}

            //string id = txtB_id.Text;
            //string name = txtName.Text;
            //string phone = txtPhone.Text;
            //string email = txtEmail.Text;
            //string position = txtPosition.Text;
            //DateTime hireDate = dtp_HireDate.Value; // Use DateTimePicker for hire date
            //decimal salary;

            //// Validate and parse the salary input
            //if (!decimal.TryParse(txtSalary.Text, out salary))
            //{
            //    MessageBox.Show("Please enter a valid salary.");
            //    return;
            //}

            //// Create and execute the SQL INSERT command with SCOPE_IDENTITY()
            //string insertQuery = "INSERT INTO Employee ( Name, Phone, Email, Position, HireDate, Salary) " +
            //                     "VALUES ( @Name, @Phone, @Email, @Position, @HireDate, @Salary); " +
            //                     "SELECT SCOPE_IDENTITY();";

            //using (SqlConnection con = new SqlConnection(connectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
            //    {
            //        cmd.Parameters.AddWithValue("@EmployeeID", id);
            //        cmd.Parameters.AddWithValue("@Name", name);
            //        cmd.Parameters.AddWithValue("@Phone", phone);
            //        cmd.Parameters.AddWithValue("@Email", email);
            //        cmd.Parameters.AddWithValue("@Position", position);
            //        cmd.Parameters.AddWithValue("@HireDate", hireDate);
            //        cmd.Parameters.AddWithValue("@Salary", salary);

            //        try
            //        {
            //            con.Open();
            //            // Execute the command and retrieve the new ID
            //            object newId = cmd.ExecuteScalar();
            //            con.Close();

            //            if (newId != null)
            //            {
            //                MessageBox.Show($"Employee added successfully! New ID: {newId}");
            //            }
            //            else
            //            {
            //                MessageBox.Show("Failed to retrieve the new employee ID.");
            //            }

            //            // Refresh the DataGridView after adding
            //            LoadEmployeeData();
            //        }
            //        catch (SqlException ex)
            //        {
            //            MessageBox.Show("Database error: " + ex.Message);
            //        }
            //    }
            //}
        }

        private void LoadEmployeeData()
        {
            dt.Clear();
            adt.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void btedit5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy giá trị từ các trường
                string employeeID = txtB_id.Text;
                string employeeName = txtName.Text;
                string phone = txtPhone.Text;
                string email = txtEmail.Text;
                string position = txtPosition.Text;
                DateTime hireDate = dtp_HireDate.Value; // Use DateTimePicker for hire date
                decimal salary;

                // Validate and parse the salary input
                if (!decimal.TryParse(txtSalary.Text, out salary))
                {
                    MessageBox.Show("Please enter a valid salary.");
                    return;
                }

                // Tạo câu lệnh SQL UPDATE
                string updateQuery = "UPDATE Employee SET Name = @Name, Phone = @Phone, Email = @Email, Position = @Position, HireDate = @HireDate, Salary = @Salary WHERE EmployeeID = @EmployeeID";

                using (SqlConnection con = new SqlConnection(connectionString)) // Ensure the correct connection string
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                        cmd.Parameters.AddWithValue("@Name", employeeName);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Position", position);
                        cmd.Parameters.AddWithValue("@HireDate", hireDate);  // Set hire date from DateTimePicker
                        cmd.Parameters.AddWithValue("@Salary", salary);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            // Refresh the DataGridView
                            LoadEmployeeData(); // Assuming you have this method to refresh data
                            MessageBox.Show("Employee updated successfully!");
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
                MessageBox.Show("Please select a row to update.");
            }
        }

        private void btclear5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy giá trị CustomerID của dòng được chọn
                string EmployeeID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                // Xác nhận trước khi xóa
                var confirmResult = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Tạo câu lệnh SQL DELETE
                    string deleteQuery = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);

                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            // Làm mới lại DataGridView
                            LoadEmployeeData();
                            MessageBox.Show("Employee deleted successfully!");
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

        private void btout5_Click(object sender, EventArgs e)
        {
           Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void LoadEmployeeData(string employeeID = null)
        {
            dt.Clear();
            string query = "SELECT * FROM Employee";

            // If an employee ID is provided, filter by it
            if (!string.IsNullOrEmpty(employeeID))
            {
                query += " WHERE EmployeeID = @EmployeeID";
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter adt = new SqlDataAdapter(query, con);

                // Add parameter only if it's a search
                if (!string.IsNullOrEmpty(employeeID))
                {
                    adt.SelectCommand.Parameters.AddWithValue("@EmployeeID", employeeID);
                }

                try
                {
                    con.Open();
                    adt.Fill(dt);
                    dataGridView1.DataSource = dt;
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
            }
        }


        

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
