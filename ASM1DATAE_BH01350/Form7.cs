using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ASM1DATAE_BH01350
{

    public partial class Form7 : Form
    {
        private int CustomerID;
        string connectionstring = @"Data Source=LAPTOP-IG12I1HR\SQLEXPRESS;Initial Catalog=asm22;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public Form7(int customerID)
        {
            InitializeComponent();
           CustomerID =customerID;
            //LoadCustomerName();

        }
        private void Form7_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionstring);
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT ProductID, Image, Name, Size, InputPrice, InventoryPrice, SellingPrice FROM Product", con);
                adt = new SqlDataAdapter(cmd);
                adt.Fill(dt);
                dgv_User.DataSource = dt;

                // Ẩn cột InputPrice
                dgv_User.Columns["InputPrice"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void dgv_User_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra nếu chỉ số dòng hợp lệ
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dgv_User.Rows[e.RowIndex];

                // Hiển thị dữ liệu trong TextBox
                try
                {
                    // Gán giá trị từ dòng được chọn vào các TextBox tương ứng
                    txtB_idUser.Text = selectedRow.Cells["ProductID"].Value.ToString();
                    txtB_nameUser.Text = selectedRow.Cells["Name"].Value.ToString();
                    cbB_sizeUser.Text = selectedRow.Cells["Size"].Value.ToString();
                    txtB_inventory.Text = selectedRow.Cells["InventoryPrice"].Value.ToString();
                    txtB_SellingpriceUser.Text = selectedRow.Cells["SellingPrice"].Value.ToString(); // Populate ImportPrice

                    // Hiển thị hình ảnh trong PictureBox
                    if (selectedRow.Cells["Image"].Value != DBNull.Value)
                    {
                        byte[] imageData = (byte[])selectedRow.Cells["Image"].Value; // Lấy dữ liệu hình ảnh
                        if (imageData != null && imageData.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(imageData)) // Chuyển đổi byte array thành hình ảnh
                            {
                                picB_imageUser.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            picB_imageUser.Image = null; // Hoặc hình ảnh mặc định
                        }
                    }
                    else
                    {
                        picB_imageUser.Image = null; // Hoặc hình ảnh mặc định
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message); // Hiển thị thông báo lỗi nếu có
                }
            }
        }

        private void btn_total_Click(object sender, EventArgs e)
        {
            if (dgv_User.SelectedRows.Count > 0) // Kiểm tra xem có dòng nào được chọn không
            {
                // Duyệt qua từng dòng trong DataGridView
                foreach (DataGridViewRow selectedProductRow in dgv_User.SelectedRows)
                {
                    string productId = selectedProductRow.Cells["ProductID"].Value.ToString();
                    string productname = selectedProductRow.Cells["Name"].Value.ToString();
                    decimal sellingPrice = Convert.ToDecimal(selectedProductRow.Cells["SellingPrice"].Value);
                    decimal costPrice = Convert.ToDecimal(selectedProductRow.Cells["InputPrice"].Value);
                    decimal inventoryQuantity = Convert.ToDecimal(selectedProductRow.Cells["InventoryPrice"].Value);

                    if (inventoryQuantity <= 0)
                    {
                        MessageBox.Show("Product is out of stock. Unable to checkout.");
                        return;
                    }

                    if (!int.TryParse(txtB_purchaseUser.Text, out int quantitySold) || quantitySold <= 0)
                    {
                        MessageBox.Show("Please enter a valid sales quantity.");
                        return;
                    }

                    if (quantitySold > inventoryQuantity)
                    {
                        MessageBox.Show("Sales quantity must not exceed inventory quantity.");
                        return;
                    }

                    decimal totalPrice = sellingPrice * quantitySold;
                    decimal totalCost = costPrice * quantitySold; // Tính tổng chi phí
                    DateTime saleDate = DateTime.Now;

                    DialogResult result = MessageBox.Show($"Are you sure you want to pay for the product:\n\nID: {productId}\nName: {productname}\nSelling Price: {sellingPrice:C}\nInput Price: {costPrice:C}\nQuantity: {inventoryQuantity}\nTotal: {totalPrice:C}\n\nPress OK to confirm.", "Payment Confirmation", MessageBoxButtons.OKCancel);

                    if (result == DialogResult.OK)
                    {
                        // Thanh toán thành công
                        MessageBox.Show("Successful payment for product: " + productname);

                        // Cập nhật số lượng tồn kho
                        UpdateInventory(productId, inventoryQuantity - quantitySold);

                        // Lưu thông tin vào bảng Statistic
                        int employeeId = 1; // Giá trị EmployeeID tạm thời, thay bằng giá trị đúng trong hệ thống
                        //int customerId = 1; // Giá trị CustomerID tạm thời, thay bằng giá trị đúng trong hệ thống
                        SaveToStatistic(productId, employeeId, quantitySold, totalPrice, costPrice, saleDate);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product to checkout.");
            }

        }
        private void SaveToStatistic(string productId,  int employeeId, int quantitySold, decimal totalPrice, decimal costPrice, DateTime saleDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                try
                {
                    connection.Open();

                    // Truy vấn chèn dữ liệu vào bảng Statistic và lấy CustomerProductID trong cùng một bước
                    string insertStatisticQuery = @"
                    INSERT INTO Statistic (ProductID, CustomerProductID, EmployeeID, QuantitySold, SaleDate, TotalPrice, InputPrice)
                    SELECT @ProductID, CustomerProductID, @EmployeeID, @QuantitySold, @SaleDate, @TotalPrice, @InputPrice
                    FROM CustomerProduct
                    WHERE CustomerID = @CustomerID AND ProductID = @ProductID";

                    using (SqlCommand command = new SqlCommand(insertStatisticQuery, connection))
                    {
                        // Gắn tham số
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        command.Parameters.AddWithValue("@ProductID", productId);
                        command.Parameters.AddWithValue("@EmployeeID", employeeId);
                        command.Parameters.AddWithValue("@QuantitySold", quantitySold);
                        command.Parameters.AddWithValue("@SaleDate", saleDate);
                        command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        command.Parameters.AddWithValue("@InputPrice", costPrice);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data successfully saved to Statistic.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to save data. Please check your input.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
        private void UpdateInventory(string productId, decimal newQuantity)
        {
            // Kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                try
                {
                    // Mở kết nối với cơ sở dữ liệu
                    connection.Open();

                    // Câu lệnh SQL để cập nhật số lượng tồn kho của sản phẩm
                    string query = "UPDATE Product SET InventoryPrice = @NewQuantity WHERE ProductID = @ProductID";

                    // Tạo đối tượng SqlCommand để thực hiện câu lệnh SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số cho câu lệnh SQL
                        command.Parameters.AddWithValue("@NewQuantity", newQuantity);
                        command.Parameters.AddWithValue("@ProductID", productId);

                        // Thực thi câu lệnh SQL (cập nhật số lượng tồn kho)
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu có ngoại lệ xảy ra
                    MessageBox.Show("Error updating inventory: " + ex.Message);
                }
            }
        }

        private void btn_searchUser_Click(object sender, EventArgs e)
        {
            string productId = txtB_searchUser.Text.Trim(); // Lấy ProductID từ TextBox

            if (!string.IsNullOrEmpty(productId)) // Kiểm tra ID không rỗng
            {
                using (SqlConnection con = new SqlConnection(connectionstring)) // Tạo kết nối mới
                {
                    try
                    {
                        con.Open(); // Mở kết nối
                        string query = "SELECT ProductID, Name, Size, InventoryPrice, SellingPrice, Image FROM Product WHERE ProductID = @ProductID";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", productId); // Thêm tham số ProductID
                            SqlDataReader reader = cmd.ExecuteReader(); // Thực thi truy vấn

                            if (reader.Read()) // Nếu có dữ liệu trả về
                            {
                                string id = reader["ProductID"].ToString();
                                string name = reader["Name"].ToString();
                                string size = reader["Size"].ToString();
                                decimal inventoryQuantity = reader.GetDecimal(reader.GetOrdinal("InventoryPrice"));
                                decimal sellingPrice = reader.GetDecimal(reader.GetOrdinal("SellingPrice"));

                                // Hiển thị thông tin sản phẩm
                                MessageBox.Show($"Product information:\nID: {id}\nName: {name}\nSize: {size}\nInventoryPrice: {inventoryQuantity}\nSellingPrice: {sellingPrice:C}");
                            }
                            else
                            {
                                MessageBox.Show("No product found with ID: " + productId);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Database error: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter product ID to search.");
            }
        }
    }
}
