namespace BüchersucheManager
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonSuche = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridBücher = new System.Windows.Forms.DataGridView();
            this.buttonFavoriten = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBücher)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(216, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(217, 39);
            this.textBox1.TabIndex = 0;
            // 
            // buttonSuche
            // 
            this.buttonSuche.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSuche.Location = new System.Drawing.Point(460, 27);
            this.buttonSuche.Name = "buttonSuche";
            this.buttonSuche.Size = new System.Drawing.Size(164, 50);
            this.buttonSuche.TabIndex = 1;
            this.buttonSuche.Text = "Suche";
            this.buttonSuche.UseVisualStyleBackColor = true;
            this.buttonSuche.Click += new System.EventHandler(this.buttonSuche_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Suchbegriff:";
            // 
            // dataGridBücher
            // 
            this.dataGridBücher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridBücher.Location = new System.Drawing.Point(12, 151);
            this.dataGridBücher.Name = "dataGridBücher";
            this.dataGridBücher.RowTemplate.Height = 28;
            this.dataGridBücher.Size = new System.Drawing.Size(1319, 481);
            this.dataGridBücher.TabIndex = 3;
            // 
            // buttonFavoriten
            // 
            this.buttonFavoriten.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFavoriten.Location = new System.Drawing.Point(672, 27);
            this.buttonFavoriten.Name = "buttonFavoriten";
            this.buttonFavoriten.Size = new System.Drawing.Size(266, 50);
            this.buttonFavoriten.TabIndex = 4;
            this.buttonFavoriten.Text = "Zeige Favoriten";
            this.buttonFavoriten.UseVisualStyleBackColor = true;
            this.buttonFavoriten.Click += new System.EventHandler(this.buttonFavoriten_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 673);
            this.Controls.Add(this.buttonFavoriten);
            this.Controls.Add(this.dataGridBücher);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSuche);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBücher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonSuche;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridBücher;
        private System.Windows.Forms.Button buttonFavoriten;
    }
}

