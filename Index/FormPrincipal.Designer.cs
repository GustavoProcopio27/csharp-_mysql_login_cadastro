namespace Index
{
    partial class FormPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            menuStrip1 = new MenuStrip();
            cadastroToolStripMenuItem = new ToolStripMenuItem();
            funcionarioToolStripMenuItem = new ToolStripMenuItem();
            produtoVendaToolStripMenuItem = new ToolStripMenuItem();
            sairToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.MediumPurple;
            menuStrip1.Items.AddRange(new ToolStripItem[] { cadastroToolStripMenuItem, sairToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(824, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // cadastroToolStripMenuItem
            // 
            cadastroToolStripMenuItem.BackColor = Color.MediumPurple;
            cadastroToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { funcionarioToolStripMenuItem, produtoVendaToolStripMenuItem });
            cadastroToolStripMenuItem.Font = new Font("Poor Richard", 12.75F, FontStyle.Bold);
            cadastroToolStripMenuItem.ForeColor = Color.Black;
            cadastroToolStripMenuItem.Image = (Image)resources.GetObject("cadastroToolStripMenuItem.Image");
            cadastroToolStripMenuItem.Name = "cadastroToolStripMenuItem";
            cadastroToolStripMenuItem.Size = new Size(102, 24);
            cadastroToolStripMenuItem.Text = "Cadastro";
            // 
            // funcionarioToolStripMenuItem
            // 
            funcionarioToolStripMenuItem.Image = (Image)resources.GetObject("funcionarioToolStripMenuItem.Image");
            funcionarioToolStripMenuItem.Name = "funcionarioToolStripMenuItem";
            funcionarioToolStripMenuItem.Size = new Size(191, 24);
            funcionarioToolStripMenuItem.Text = "Funcionario";
            funcionarioToolStripMenuItem.Click += funcionarioToolStripMenuItem_Click;
            // 
            // produtoVendaToolStripMenuItem
            // 
            produtoVendaToolStripMenuItem.Image = (Image)resources.GetObject("produtoVendaToolStripMenuItem.Image");
            produtoVendaToolStripMenuItem.Name = "produtoVendaToolStripMenuItem";
            produtoVendaToolStripMenuItem.Size = new Size(191, 24);
            produtoVendaToolStripMenuItem.Text = "Produto/Venda";
            produtoVendaToolStripMenuItem.Click += produtoVendaToolStripMenuItem_Click;
            // 
            // sairToolStripMenuItem
            // 
            sairToolStripMenuItem.BackColor = Color.MediumPurple;
            sairToolStripMenuItem.Font = new Font("Poor Richard", 12.75F, FontStyle.Bold);
            sairToolStripMenuItem.ForeColor = Color.Black;
            sairToolStripMenuItem.Image = (Image)resources.GetObject("sairToolStripMenuItem.Image");
            sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            sairToolStripMenuItem.Size = new Size(66, 24);
            sairToolStripMenuItem.Text = "Sair";
            sairToolStripMenuItem.Click += sairToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Sylfaen", 11F, FontStyle.Bold | FontStyle.Italic);
            label1.ForeColor = Color.White;
            label1.Location = new Point(611, 440);
            label1.Name = "label1";
            label1.Size = new Size(213, 19);
            label1.TabIndex = 1;
            label1.Text = "Gustavo Luiz, Davi Nogueira";
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(212, 190, 238);
            ClientSize = new Size(824, 468);
            Controls.Add(menuStrip1);
            Controls.Add(label1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "FormPrincipal";
            Text = " Menu";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem cadastroToolStripMenuItem;
        private ToolStripMenuItem funcionarioToolStripMenuItem;
        private ToolStripMenuItem sairToolStripMenuItem;
        private Label label1;
        private ToolStripMenuItem produtoVendaToolStripMenuItem;
    }
}