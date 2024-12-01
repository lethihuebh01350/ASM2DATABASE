namespace ASM1DATAE_BH01350
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv_Product = new System.Windows.Forms.DataGridView();
            this.btn_addProduct = new System.Windows.Forms.Button();
            this.btn_editProduct = new System.Windows.Forms.Button();
            this.btn_clearProduct = new System.Windows.Forms.Button();
            this.btn_outProduct = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtB_sellingpriceProduct = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtB_sizeProduct = new System.Windows.Forms.TextBox();
            this.txtB_inputpriceProduct = new System.Windows.Forms.TextBox();
            this.quantity3 = new System.Windows.Forms.Label();
            this.txtB_inventorypriceProduct = new System.Windows.Forms.TextBox();
            this.txtB_nameProduct = new System.Windows.Forms.TextBox();
            this.txtB_idProduct = new System.Windows.Forms.TextBox();
            this.lcsize3 = new System.Windows.Forms.Label();
            this.lbprice3 = new System.Windows.Forms.Label();
            this.lbname3 = new System.Windows.Forms.Label();
            this.lbid3 = new System.Windows.Forms.Label();
            this.picB_imageProduct = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnsearch = new System.Windows.Forms.Button();
            this.txtB_searchSP = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Product)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_imageProduct)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_Product
            // 
            this.dgv_Product.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgv_Product.BackgroundColor = System.Drawing.Color.MistyRose;
            this.dgv_Product.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Product.Location = new System.Drawing.Point(-2, 99);
            this.dgv_Product.Name = "dgv_Product";
            this.dgv_Product.RowHeadersWidth = 51;
            this.dgv_Product.RowTemplate.Height = 24;
            this.dgv_Product.Size = new System.Drawing.Size(560, 350);
            this.dgv_Product.TabIndex = 0;
            this.dgv_Product.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Product_CellClick);
            // 
            // btn_addProduct
            // 
            this.btn_addProduct.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addProduct.Location = new System.Drawing.Point(84, 368);
            this.btn_addProduct.Name = "btn_addProduct";
            this.btn_addProduct.Size = new System.Drawing.Size(74, 31);
            this.btn_addProduct.TabIndex = 1;
            this.btn_addProduct.Text = "Add";
            this.btn_addProduct.UseVisualStyleBackColor = true;
            this.btn_addProduct.Click += new System.EventHandler(this.btn_addProduct_Click);
            // 
            // btn_editProduct
            // 
            this.btn_editProduct.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_editProduct.Location = new System.Drawing.Point(9, 408);
            this.btn_editProduct.Name = "btn_editProduct";
            this.btn_editProduct.Size = new System.Drawing.Size(68, 32);
            this.btn_editProduct.TabIndex = 2;
            this.btn_editProduct.Text = "Edit";
            this.btn_editProduct.UseVisualStyleBackColor = true;
            this.btn_editProduct.Click += new System.EventHandler(this.btn_editProduct_Click);
            // 
            // btn_clearProduct
            // 
            this.btn_clearProduct.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clearProduct.Location = new System.Drawing.Point(84, 408);
            this.btn_clearProduct.Name = "btn_clearProduct";
            this.btn_clearProduct.Size = new System.Drawing.Size(74, 32);
            this.btn_clearProduct.TabIndex = 3;
            this.btn_clearProduct.Text = "Clear";
            this.btn_clearProduct.UseVisualStyleBackColor = true;
            this.btn_clearProduct.Click += new System.EventHandler(this.btn_clearProduct_Click);
            // 
            // btn_outProduct
            // 
            this.btn_outProduct.Font = new System.Drawing.Font("Modern No. 20", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_outProduct.Location = new System.Drawing.Point(164, 409);
            this.btn_outProduct.Name = "btn_outProduct";
            this.btn_outProduct.Size = new System.Drawing.Size(64, 31);
            this.btn_outProduct.TabIndex = 4;
            this.btn_outProduct.Text = "Out";
            this.btn_outProduct.UseVisualStyleBackColor = true;
            this.btn_outProduct.Click += new System.EventHandler(this.btn_outProduct_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.txtB_sellingpriceProduct);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtB_sizeProduct);
            this.groupBox1.Controls.Add(this.btn_outProduct);
            this.groupBox1.Controls.Add(this.btn_addProduct);
            this.groupBox1.Controls.Add(this.btn_clearProduct);
            this.groupBox1.Controls.Add(this.btn_editProduct);
            this.groupBox1.Controls.Add(this.txtB_inputpriceProduct);
            this.groupBox1.Controls.Add(this.quantity3);
            this.groupBox1.Controls.Add(this.txtB_inventorypriceProduct);
            this.groupBox1.Controls.Add(this.txtB_nameProduct);
            this.groupBox1.Controls.Add(this.txtB_idProduct);
            this.groupBox1.Controls.Add(this.lcsize3);
            this.groupBox1.Controls.Add(this.lbprice3);
            this.groupBox1.Controls.Add(this.lbname3);
            this.groupBox1.Controls.Add(this.lbid3);
            this.groupBox1.Controls.Add(this.picB_imageProduct);
            this.groupBox1.Location = new System.Drawing.Point(560, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 451);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // txtB_sellingpriceProduct
            // 
            this.txtB_sellingpriceProduct.Location = new System.Drawing.Point(114, 331);
            this.txtB_sellingpriceProduct.Name = "txtB_sellingpriceProduct";
            this.txtB_sellingpriceProduct.Size = new System.Drawing.Size(114, 22);
            this.txtB_sellingpriceProduct.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 336);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Selling price";
            // 
            // txtB_sizeProduct
            // 
            this.txtB_sizeProduct.Location = new System.Drawing.Point(97, 215);
            this.txtB_sizeProduct.Name = "txtB_sizeProduct";
            this.txtB_sizeProduct.Size = new System.Drawing.Size(133, 22);
            this.txtB_sizeProduct.TabIndex = 15;
            // 
            // txtB_inputpriceProduct
            // 
            this.txtB_inputpriceProduct.Location = new System.Drawing.Point(114, 256);
            this.txtB_inputpriceProduct.Name = "txtB_inputpriceProduct";
            this.txtB_inputpriceProduct.Size = new System.Drawing.Size(114, 22);
            this.txtB_inputpriceProduct.TabIndex = 14;
            // 
            // quantity3
            // 
            this.quantity3.AutoSize = true;
            this.quantity3.BackColor = System.Drawing.Color.White;
            this.quantity3.Font = new System.Drawing.Font("Modern No. 20", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantity3.Location = new System.Drawing.Point(4, 294);
            this.quantity3.Name = "quantity3";
            this.quantity3.Size = new System.Drawing.Size(76, 17);
            this.quantity3.TabIndex = 13;
            this.quantity3.Text = "Inventory";
            // 
            // txtB_inventorypriceProduct
            // 
            this.txtB_inventorypriceProduct.Location = new System.Drawing.Point(116, 294);
            this.txtB_inventorypriceProduct.Name = "txtB_inventorypriceProduct";
            this.txtB_inventorypriceProduct.Size = new System.Drawing.Size(114, 22);
            this.txtB_inventorypriceProduct.TabIndex = 8;
            // 
            // txtB_nameProduct
            // 
            this.txtB_nameProduct.Location = new System.Drawing.Point(95, 182);
            this.txtB_nameProduct.Name = "txtB_nameProduct";
            this.txtB_nameProduct.Size = new System.Drawing.Size(133, 22);
            this.txtB_nameProduct.TabIndex = 7;
            // 
            // txtB_idProduct
            // 
            this.txtB_idProduct.Location = new System.Drawing.Point(84, 18);
            this.txtB_idProduct.Name = "txtB_idProduct";
            this.txtB_idProduct.Size = new System.Drawing.Size(133, 22);
            this.txtB_idProduct.TabIndex = 6;
            // 
            // lcsize3
            // 
            this.lcsize3.AutoSize = true;
            this.lcsize3.BackColor = System.Drawing.Color.White;
            this.lcsize3.Font = new System.Drawing.Font("Modern No. 20", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcsize3.Location = new System.Drawing.Point(6, 215);
            this.lcsize3.Name = "lcsize3";
            this.lcsize3.Size = new System.Drawing.Size(37, 17);
            this.lcsize3.TabIndex = 4;
            this.lcsize3.Text = "Size";
            // 
            // lbprice3
            // 
            this.lbprice3.AutoSize = true;
            this.lbprice3.BackColor = System.Drawing.Color.White;
            this.lbprice3.Font = new System.Drawing.Font("Modern No. 20", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbprice3.Location = new System.Drawing.Point(6, 261);
            this.lbprice3.Name = "lbprice3";
            this.lbprice3.Size = new System.Drawing.Size(87, 17);
            this.lbprice3.TabIndex = 3;
            this.lbprice3.Text = "Input price";
            // 
            // lbname3
            // 
            this.lbname3.AutoSize = true;
            this.lbname3.BackColor = System.Drawing.Color.White;
            this.lbname3.Font = new System.Drawing.Font("Modern No. 20", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbname3.Location = new System.Drawing.Point(6, 187);
            this.lbname3.Name = "lbname3";
            this.lbname3.Size = new System.Drawing.Size(46, 17);
            this.lbname3.TabIndex = 2;
            this.lbname3.Text = "Name";
            // 
            // lbid3
            // 
            this.lbid3.AutoSize = true;
            this.lbid3.BackColor = System.Drawing.Color.White;
            this.lbid3.Font = new System.Drawing.Font("Modern No. 20", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbid3.Location = new System.Drawing.Point(30, 18);
            this.lbid3.Name = "lbid3";
            this.lbid3.Size = new System.Drawing.Size(27, 17);
            this.lbid3.TabIndex = 1;
            this.lbid3.Text = "ID";
            // 
            // picB_imageProduct
            // 
            this.picB_imageProduct.BackColor = System.Drawing.Color.White;
            this.picB_imageProduct.Location = new System.Drawing.Point(28, 48);
            this.picB_imageProduct.Name = "picB_imageProduct";
            this.picB_imageProduct.Size = new System.Drawing.Size(189, 112);
            this.picB_imageProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picB_imageProduct.TabIndex = 0;
            this.picB_imageProduct.TabStop = false;
            this.picB_imageProduct.Click += new System.EventHandler(this.picB_imageProduct_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-2, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 53);
            this.panel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(203, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product";
            // 
            // btnsearch
            // 
            this.btnsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.Location = new System.Drawing.Point(348, 71);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(75, 23);
            this.btnsearch.TabIndex = 1;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = true;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // txtB_searchSP
            // 
            this.txtB_searchSP.Location = new System.Drawing.Point(58, 71);
            this.txtB_searchSP.Name = "txtB_searchSP";
            this.txtB_searchSP.Size = new System.Drawing.Size(258, 22);
            this.txtB_searchSP.TabIndex = 0;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnsearch);
            this.Controls.Add(this.txtB_searchSP);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_Product);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Product)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picB_imageProduct)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Product;
        private System.Windows.Forms.Button btn_addProduct;
        private System.Windows.Forms.Button btn_editProduct;
        private System.Windows.Forms.Button btn_clearProduct;
        private System.Windows.Forms.Button btn_outProduct;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lcsize3;
        private System.Windows.Forms.Label lbprice3;
        private System.Windows.Forms.Label lbname3;
        private System.Windows.Forms.Label lbid3;
        private System.Windows.Forms.PictureBox picB_imageProduct;
        private System.Windows.Forms.TextBox txtB_inventorypriceProduct;
        private System.Windows.Forms.TextBox txtB_nameProduct;
        private System.Windows.Forms.TextBox txtB_idProduct;
        private System.Windows.Forms.Label quantity3;
        private System.Windows.Forms.TextBox txtB_inputpriceProduct;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.TextBox txtB_searchSP;
        private System.Windows.Forms.TextBox txtB_sizeProduct;
        private System.Windows.Forms.TextBox txtB_sellingpriceProduct;
        private System.Windows.Forms.Label label2;
    }
}