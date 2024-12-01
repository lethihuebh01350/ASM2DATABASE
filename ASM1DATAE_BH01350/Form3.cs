using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ASM1DATAE_BH01350
{
    public partial class Form3 : Form
    {
        string connectionstring = @"Data Source=LAPTOP-IG12I1HR\SQLEXPRESS;Initial Catalog=asm22;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt = new DataTable();
        public Form3()
        {
            InitializeComponent();


        }

        private void Form3_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(connectionstring); // Khởi tạo kết nối với cơ sở dữ liệu
            LoadProducts(); // Gọi hàm Load lại dữ liệu từ CSDL khi form load
        }
        private void LoadProducts()
        {
            dt.Clear(); // Xóa dữ liệu cũ trong DataTable
            using (SqlConnection con = new SqlConnection(connectionstring)) // Tạo kết nối mới
            {
                try
                {
                    con.Open(); // Mở kết nối
                    cmd = new SqlCommand("SELECT * FROM Product", con); // Tạo lệnh SQL để lấy tất cả sản phẩm
                    adt = new SqlDataAdapter(cmd); // Tạo SqlDataAdapter từ lệnh
                    adt.Fill(dt); // Điền dữ liệu vào DataTable
                    dgv_Product.DataSource = dt; // Gán DataTable cho DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); // Hiển thị thông báo lỗi nếu có
                }
            }
        }


        private void dgv_Product_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra nếu chỉ số dòng hợp lệ
            {
                DataGridViewRow selectedRow = dgv_Product.Rows[e.RowIndex];

                try
                {
                    // Gán giá trị từ dòng được chọn vào các TextBox tương ứng
                    txtB_idProduct.Text = selectedRow.Cells["ProductID"].Value.ToString();       // ProductID
                    txtB_nameProduct.Text = selectedRow.Cells["Name"].Value?.ToString() ?? string.Empty;   // ProductName

                    txtB_sizeProduct.Text = selectedRow.Cells["Size"].Value?.ToString() ?? string.Empty;   // SizeProduct
                    txtB_inputpriceProduct.Text = selectedRow.Cells["InputPrice"].Value?.ToString() ?? string.Empty;    // InputPrice
                    txtB_inventorypriceProduct.Text = selectedRow.Cells["InventoryPrice"].Value?.ToString() ?? string.Empty; // InventoryPrice
                    txtB_sellingpriceProduct.Text = selectedRow.Cells["SellingPrice"].Value?.ToString() ?? string.Empty; // SellingPrice

                    // Hiển thị hình ảnh trong PictureBox nếu có
                    if (selectedRow.Cells["Image"].Value != DBNull.Value && selectedRow.Cells["Image"].Value is byte[] imageData)
                    {
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            picB_imageProduct.Image = Image.FromStream(ms); // Set image
                        }
                    }
                    else
                    {
                        picB_imageProduct.Image = null; // Nếu không có hình ảnh, xóa PictureBox
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message); // Hiển thị thông báo lỗi nếu có
                }
            }
        }
        private void btn_addProduct_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào của người dùng
            if (
                string.IsNullOrWhiteSpace(txtB_nameProduct.Text) ||
                string.IsNullOrWhiteSpace(txtB_sizeProduct.Text) ||
                string.IsNullOrWhiteSpace(txtB_idProduct.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Kiểm tra ID
            if (!int.TryParse(txtB_idProduct.Text, out int productId))
            {
                MessageBox.Show("Product ID must be a number.");
                return;
            }

            // Kiểm tra giá nhập và giá bán
            if (!decimal.TryParse(txtB_inventorypriceProduct.Text, out decimal importPrice) ||
                !decimal.TryParse(txtB_sellingpriceProduct.Text, out decimal sellingPrice))
            {
                MessageBox.Show("Import price and selling price must be valid decimal numbers.");
                return;
            }

            // Kiểm tra số lượng tồn kho
            if (!int.TryParse(txtB_inputpriceProduct.Text, out int quantity))
            {
                MessageBox.Show("Input quantity must be a valid integer.");
                return;
            }

            decimal profit = sellingPrice - importPrice; // Tính lợi nhuận
            byte[] productImage = PathToByteArray(this.Text); // Giả sử hàm chuyển đổi đã được định nghĩa

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Product (ProductID, Name, Size, Image, InputPrice, InventoryPrice, SellingPrice) VALUES (@ProductID, @Name, @Size, @Image, @InputPrice, @InventoryPrice, @SellingPrice)", con))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@Name", txtB_nameProduct.Text.Trim());
                        cmd.Parameters.AddWithValue("@Size", txtB_sizeProduct.Text.Trim());
                        cmd.Parameters.AddWithValue("@Image", productImage);
                        cmd.Parameters.AddWithValue("@InputPrice", quantity);
                        cmd.Parameters.AddWithValue("@InventoryPrice", importPrice);
                        cmd.Parameters.AddWithValue("@SellingPrice", sellingPrice);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Product added successfully! Profit: {profit:C2}"); // Hiển thị lợi nhuận
                    LoadProducts(); // Tải lại danh sách sản phẩm sau khi thêm mới
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}");
                }
            }
            
        }

        // Chuyển đổi file hình ảnh thành mảng byte
        byte[] PathToByteArray(string path)
        {
            if (!File.Exists(path))
            {
                return null; // Trả về null nếu không có ảnh
            }

            using (MemoryStream m = new MemoryStream())
            {
                using (Image img = Image.FromFile(path))
                {
                    img.Save(m, System.Drawing.Imaging.ImageFormat.Png);
                }
                return m.ToArray();
            }
        }
       
        private void picB_imageProduct_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog(); // Tạo hộp thoại mở file
            if (open.ShowDialog() == DialogResult.OK) // Kiểm tra xem người dùng đã chọn file chưa
            {
                picB_imageProduct.Image = Image.FromFile(open.FileName); // Hiển thị hình ảnh đã chọn
                this.Text = open.FileName; // Hiển thị đường dẫn file cho mục đích gỡ lỗi
            }
        }

        private void btn_editProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtB_nameProduct.Text) ||
    string.IsNullOrWhiteSpace(txtB_sizeProduct.Text) ||
    string.IsNullOrWhiteSpace(txtB_idProduct.Text) ||
    !decimal.TryParse(txtB_inventorypriceProduct.Text, out decimal inventoryPrice) ||
    !decimal.TryParse(txtB_inputpriceProduct.Text, out decimal inputPrice) ||
    !decimal.TryParse(txtB_sellingpriceProduct.Text, out decimal sellingPrice))
{
    MessageBox.Show("Please enter valid values for all fields.");
    return;
}

byte[] productImage = null;
if (picB_imageProduct.Image != null) // Kiểm tra nếu có hình ảnh trong PictureBox
{
    using (MemoryStream ms = new MemoryStream())
    {
        picB_imageProduct.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Lưu ảnh vào MemoryStream
        productImage = ms.ToArray(); // Chuyển đổi hình ảnh thành mảng byte
    }
}

// Mở kết nối và thực hiện câu lệnh SQL để cập nhật thông tin sản phẩm
using (SqlConnection con = new SqlConnection(connectionstring))
{
    try
    {
        con.Open();
        string query = @"UPDATE Product 
                         SET Name = @Name, Size = @Size, Image = @Image, 
                             InputPrice = @InputPrice, SellingPrice = @SellingPrice, 
                             InventoryPrice = @InventoryPrice
                         WHERE ProductID = @ProductID";

        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@ProductID", int.Parse(txtB_idProduct.Text));
            cmd.Parameters.AddWithValue("@Name", txtB_nameProduct.Text);
            cmd.Parameters.AddWithValue("@Size", txtB_sizeProduct.Text);
            cmd.Parameters.AddWithValue("@Image", (object)productImage ?? DBNull.Value); // Thêm kiểm tra null
            cmd.Parameters.AddWithValue("@InputPrice", inputPrice);
            cmd.Parameters.AddWithValue("@SellingPrice", sellingPrice);
            cmd.Parameters.AddWithValue("@InventoryPrice", inventoryPrice);

            cmd.ExecuteNonQuery();
        }

        MessageBox.Show("Product updated successfully!");
        LoadProducts(); // Tải lại danh sách sản phẩm sau khi chỉnh sửa
    }
    catch (SqlException ex)
    {
        MessageBox.Show($"Database error: {ex.Message}");
    }
}
        }

        private void btn_clearProduct_Click(object sender, EventArgs e)
        {
            if (dgv_Product.SelectedRows.Count > 0) // Kiểm tra xem có dòng nào được chọn không
            {
                var selectedRow = dgv_Product.SelectedRows[0];
                int productIdToDelete = Convert.ToInt32(selectedRow.Cells["ProductID"].Value); // Lấy ProductID từ dòng đã chọn

                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    try
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE ProductID = @ProductID", con))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", productIdToDelete);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Product deleted successfully!");
                        LoadProducts(); // Tải lại danh sách sản phẩm sau khi xóa
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Error deleting product: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }

        private void btn_outProduct_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            string productId = txtB_searchSP.Text.Trim(); // Lấy ID sản phẩm từ TextBox

            if (!string.IsNullOrEmpty(productId)) // Kiểm tra ID không rỗng
            {
                using (SqlConnection con = new SqlConnection(connectionstring)) // Tạo kết nối mới
                {
                    try
                    {
                        con.Open(); // Mở kết nối
                        string query = "SELECT ProductID, Name, Size, InventoryPrice, SellingPrice, PImage FROM Product WHERE ProductID = @ProductID";
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
                                MessageBox.Show($"Product information:\nID: {id}\nTên: {name}\nName: {size}\nInventory quantity: {inventoryQuantity}\nSelling price: {sellingPrice:C}");
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