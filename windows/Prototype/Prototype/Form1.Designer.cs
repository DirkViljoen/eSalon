﻿namespace Prototype
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMantainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuToolStripMenuItem,
            this.stockToolStripMenuItem1,
            this.webProgramToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(14, 46);
            this.menuStrip1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(27, 27, 14, 39);
            this.menuStrip1.Size = new System.Drawing.Size(395, 107);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainMenuToolStripMenuItem
            // 
            this.mainMenuToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(104)))), ((int)(((byte)(133)))));
            this.mainMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ordersToolStripMenuItem,
            this.addOrderToolStripMenuItem});
            this.mainMenuToolStripMenuItem.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenuToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.mainMenuToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.mainMenuToolStripMenuItem.Name = "mainMenuToolStripMenuItem";
            this.mainMenuToolStripMenuItem.Padding = new System.Windows.Forms.Padding(25, 7, 25, 7);
            this.mainMenuToolStripMenuItem.Size = new System.Drawing.Size(117, 36);
            this.mainMenuToolStripMenuItem.Text = "Orders";
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(179)))), ((int)(((byte)(174)))));
            this.ordersToolStripMenuItem.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(223, 50);
            this.ordersToolStripMenuItem.Text = "View / Maintain";
            this.ordersToolStripMenuItem.Click += new System.EventHandler(this.ordersToolStripMenuItem_Click);
            // 
            // addOrderToolStripMenuItem
            // 
            this.addOrderToolStripMenuItem.Name = "addOrderToolStripMenuItem";
            this.addOrderToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.addOrderToolStripMenuItem.Size = new System.Drawing.Size(223, 50);
            this.addOrderToolStripMenuItem.Text = "Add Order";
            this.addOrderToolStripMenuItem.Click += new System.EventHandler(this.addOrderToolStripMenuItem_Click);
            // 
            // stockToolStripMenuItem1
            // 
            this.stockToolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(104)))), ((int)(((byte)(100)))));
            this.stockToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewMantainToolStripMenuItem,
            this.addStockToolStripMenuItem});
            this.stockToolStripMenuItem1.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stockToolStripMenuItem1.Name = "stockToolStripMenuItem1";
            this.stockToolStripMenuItem1.Padding = new System.Windows.Forms.Padding(25, 7, 25, 7);
            this.stockToolStripMenuItem1.Size = new System.Drawing.Size(106, 41);
            this.stockToolStripMenuItem1.Text = "Stock";
            this.stockToolStripMenuItem1.Click += new System.EventHandler(this.stockToolStripMenuItem1_Click);
            // 
            // viewMantainToolStripMenuItem
            // 
            this.viewMantainToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(179)))), ((int)(((byte)(174)))));
            this.viewMantainToolStripMenuItem.Name = "viewMantainToolStripMenuItem";
            this.viewMantainToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.viewMantainToolStripMenuItem.Size = new System.Drawing.Size(213, 50);
            this.viewMantainToolStripMenuItem.Text = "View/ Mantain";
            this.viewMantainToolStripMenuItem.Click += new System.EventHandler(this.viewMantainToolStripMenuItem_Click);
            // 
            // addStockToolStripMenuItem
            // 
            this.addStockToolStripMenuItem.Name = "addStockToolStripMenuItem";
            this.addStockToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.addStockToolStripMenuItem.Size = new System.Drawing.Size(213, 50);
            this.addStockToolStripMenuItem.Text = "Add Stock";
            this.addStockToolStripMenuItem.Click += new System.EventHandler(this.addStockToolStripMenuItem_Click);
            // 
            // webProgramToolStripMenuItem
            // 
            this.webProgramToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(104)))), ((int)(((byte)(133)))));
            this.webProgramToolStripMenuItem.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.webProgramToolStripMenuItem.Name = "webProgramToolStripMenuItem";
            this.webProgramToolStripMenuItem.Padding = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.webProgramToolStripMenuItem.Size = new System.Drawing.Size(129, 44);
            this.webProgramToolStripMenuItem.Text = "Web Program";
            this.webProgramToolStripMenuItem.Click += new System.EventHandler(this.webProgramToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(202, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Logged In";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(306, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(116, 26);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "Admin";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(214)))), ((int)(((byte)(204)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(434, 164);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(9, 13, 9, 13);
            this.Text = "Main Menu";
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem stockToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewMantainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem webProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addOrderToolStripMenuItem;
    }
}

