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
            this.feelingABitLostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dATABASEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lookupListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.webProgramToolStripMenuItem,
            this.feelingABitLostToolStripMenuItem,
            this.dATABASEToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(14, 17);
            this.menuStrip1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(27, 27, 14, 39);
            this.menuStrip1.Size = new System.Drawing.Size(665, 118);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainMenuToolStripMenuItem
            // 
            this.mainMenuToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.mainMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ordersToolStripMenuItem,
            this.addOrderToolStripMenuItem});
            this.mainMenuToolStripMenuItem.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenuToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.mainMenuToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.mainMenuToolStripMenuItem.Name = "mainMenuToolStripMenuItem";
            this.mainMenuToolStripMenuItem.Padding = new System.Windows.Forms.Padding(25, 7, 25, 7);
            this.mainMenuToolStripMenuItem.Size = new System.Drawing.Size(117, 47);
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
            this.stockToolStripMenuItem1.BackColor = System.Drawing.Color.White;
            this.stockToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewMantainToolStripMenuItem,
            this.addStockToolStripMenuItem});
            this.stockToolStripMenuItem1.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stockToolStripMenuItem1.Name = "stockToolStripMenuItem1";
            this.stockToolStripMenuItem1.Padding = new System.Windows.Forms.Padding(25, 7, 25, 7);
            this.stockToolStripMenuItem1.Size = new System.Drawing.Size(106, 52);
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
            this.webProgramToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.webProgramToolStripMenuItem.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.webProgramToolStripMenuItem.Name = "webProgramToolStripMenuItem";
            this.webProgramToolStripMenuItem.Padding = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.webProgramToolStripMenuItem.Size = new System.Drawing.Size(129, 52);
            this.webProgramToolStripMenuItem.Text = "Web Program";
            this.webProgramToolStripMenuItem.Click += new System.EventHandler(this.webProgramToolStripMenuItem_Click);
            // 
            // feelingABitLostToolStripMenuItem
            // 
            this.feelingABitLostToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.feelingABitLostToolStripMenuItem.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feelingABitLostToolStripMenuItem.Name = "feelingABitLostToolStripMenuItem";
            this.feelingABitLostToolStripMenuItem.Size = new System.Drawing.Size(166, 52);
            this.feelingABitLostToolStripMenuItem.Text = "Feeling A Bit Lost?";
            this.feelingABitLostToolStripMenuItem.Click += new System.EventHandler(this.feelingABitLostToolStripMenuItem_Click);
            // 
            // dATABASEToolStripMenuItem
            // 
            this.dATABASEToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.dATABASEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backUpToolStripMenuItem,
            this.restoreToolStripMenuItem,
            this.lookupListToolStripMenuItem});
            this.dATABASEToolStripMenuItem.Font = new System.Drawing.Font("Lucida Fax", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dATABASEToolStripMenuItem.Name = "dATABASEToolStripMenuItem";
            this.dATABASEToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.dATABASEToolStripMenuItem.Size = new System.Drawing.Size(104, 52);
            this.dATABASEToolStripMenuItem.Text = "Database";
            this.dATABASEToolStripMenuItem.Click += new System.EventHandler(this.dATABASEToolStripMenuItem_Click);
            // 
            // backUpToolStripMenuItem
            // 
            this.backUpToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(179)))), ((int)(((byte)(174)))));
            this.backUpToolStripMenuItem.Name = "backUpToolStripMenuItem";
            this.backUpToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.backUpToolStripMenuItem.Size = new System.Drawing.Size(190, 50);
            this.backUpToolStripMenuItem.Text = "Back Up";
            this.backUpToolStripMenuItem.Click += new System.EventHandler(this.backUpToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(190, 50);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // lookupListToolStripMenuItem
            // 
            this.lookupListToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(179)))), ((int)(((byte)(174)))));
            this.lookupListToolStripMenuItem.Name = "lookupListToolStripMenuItem";
            this.lookupListToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10, 15, 10, 15);
            this.lookupListToolStripMenuItem.Size = new System.Drawing.Size(190, 50);
            this.lookupListToolStripMenuItem.Text = "Lookup List";
            this.lookupListToolStripMenuItem.Click += new System.EventHandler(this.lookupListToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(214)))), ((int)(((byte)(204)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(726, 307);
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
        private System.Windows.Forms.ToolStripMenuItem stockToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewMantainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem webProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem feelingABitLostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dATABASEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lookupListToolStripMenuItem;
    }
}

