namespace Arauto
{
    partial class ConfigConta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigConta));
            textBox1 = new TextBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            comboBox2 = new ComboBox();
            checkBox1 = new CheckBox();
            label7 = new Label();
            textBox2 = new TextBox();
            label8 = new Label();
            button1 = new Button();
            button2 = new Button();
            textBox3 = new TextBox();
            label9 = new Label();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            webView22 = new Microsoft.Web.WebView2.WinForms.WebView2();
            textBox4 = new TextBox();
            label10 = new Label();
            label11 = new Label();
            textBox5 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webView22).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 229);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.Size = new Size(499, 64);
            textBox1.TabIndex = 0;
            textBox1.Text = "Gere uma imagem sobre a noticia apenas com o titulo e uma chamada, com um fundo atras desfocado que esteja relacionado ao tema";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Português", "Inglês", "Espanhol", "Alemão", "Italiano" });
            comboBox1.Location = new Point(335, 167);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(176, 23);
            comboBox1.TabIndex = 2;
            comboBox1.Text = "Português";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(285, 170);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 3;
            label1.Text = "Idioma";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 39);
            label2.Name = "label2";
            label2.Size = new Size(117, 15);
            label2.TabIndex = 4;
            label2.Text = "Nome conta Google:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(264, 39);
            label3.Name = "label3";
            label3.Size = new Size(112, 15);
            label3.TabIndex = 5;
            label3.Text = "Nome conta TikTok:";
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(125, 38);
            label4.Name = "label4";
            label4.Size = new Size(133, 16);
            label4.TabIndex = 6;
            label4.Text = "Carregando...";
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(372, 38);
            label5.Name = "label5";
            label5.Size = new Size(141, 16);
            label5.TabIndex = 7;
            label5.Text = "Carregando...";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(11, 170);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 9;
            label6.Text = "Nicho";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Notícias", "Futebol", "Famosos", "Saúde", "Signos", "Política", "Economia", "Saúde", "Tecnologia", "Curiosidades", "Celebridades", "Moda", "História", "Esportes", "Humor", "Games", "Música", "Cinema", "Séries", "Educação", "Viagens", "Animais", "Natureza", "Astronomia", "Espiritualidade", "Religião", "Psicologia", "Finanças", "Empreendedorismo", "Carros", "Culinária", "Receitas", "Reviews", "Criptomoedas", "Inteligência", "Mistérios", "Conspirações", "Meditação", "Motivação", "Tendências", "Negócios" });
            comboBox2.Location = new Point(56, 167);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(202, 23);
            comboBox2.TabIndex = 8;
            comboBox2.Text = "Notícias";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(212, 531);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(88, 19);
            checkBox1.TabIndex = 10;
            checkBox1.Text = "Conta Ativa";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 205);
            label7.Name = "label7";
            label7.Size = new Size(407, 15);
            label7.TabIndex = 11;
            label7.Text = "Prompt geração da imagem (considera o contexto do prompt de conteúdo)";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 99);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ScrollBars = ScrollBars.Both;
            textBox2.Size = new Size(499, 55);
            textBox2.TabIndex = 12;
            textBox2.Text = "Qual a noticia mais importante do dia, resuma em um paragrafo com titulo e resumo, use texto sem caracteres estranhos e sem citar fonte [apenas texto, sem imagem ou thumb][números e datas por extenso]";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 77);
            label8.Name = "label8";
            label8.Size = new Size(162, 15);
            label8.TabIndex = 13;
            label8.Text = "Prompt geração de conteúdo";
            // 
            // button1
            // 
            button1.Cursor = Cursors.Hand;
            button1.Enabled = false;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(436, 528);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Salvar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Cursor = Cursors.Hand;
            button2.Enabled = false;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(306, 528);
            button2.Name = "button2";
            button2.Size = new Size(124, 23);
            button2.TabIndex = 15;
            button2.Text = "Gerar Postagem";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 323);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ScrollBars = ScrollBars.Both;
            textBox3.Size = new Size(499, 64);
            textBox3.TabIndex = 16;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 305);
            label9.Name = "label9";
            label9.Size = new Size(256, 15);
            label9.TabIndex = 17;
            label9.Text = "Blacklist (use sempre o mesmo padrão * TEMA)";
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(16, 325);
            webView21.Name = "webView21";
            webView21.Size = new Size(60, 60);
            webView21.TabIndex = 18;
            webView21.ZoomFactor = 1D;
            // 
            // webView22
            // 
            webView22.AllowExternalDrop = true;
            webView22.CreationProperties = null;
            webView22.DefaultBackgroundColor = Color.White;
            webView22.Location = new Point(82, 325);
            webView22.Name = "webView22";
            webView22.Size = new Size(60, 60);
            webView22.TabIndex = 19;
            webView22.ZoomFactor = 1D;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(11, 415);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.ScrollBars = ScrollBars.Both;
            textBox4.Size = new Size(500, 38);
            textBox4.TabIndex = 20;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 397);
            label10.Name = "label10";
            label10.Size = new Size(30, 15);
            label10.TabIndex = 21;
            label10.Text = "Tags";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(12, 465);
            label11.Name = "label11";
            label11.Size = new Size(58, 15);
            label11.TabIndex = 23;
            label11.Text = "Chamada";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(11, 483);
            textBox5.Name = "textBox5";
            textBox5.ScrollBars = ScrollBars.Both;
            textBox5.Size = new Size(500, 23);
            textBox5.TabIndex = 22;
            // 
            // ConfigConta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 567);
            Controls.Add(label11);
            Controls.Add(textBox5);
            Controls.Add(label10);
            Controls.Add(textBox4);
            Controls.Add(label9);
            Controls.Add(textBox3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label8);
            Controls.Add(textBox2);
            Controls.Add(label7);
            Controls.Add(checkBox1);
            Controls.Add(label6);
            Controls.Add(comboBox2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(textBox1);
            Controls.Add(webView21);
            Controls.Add(webView22);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(539, 606);
            MinimumSize = new Size(539, 606);
            Name = "ConfigConta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configurações";
            Load += ConfigConta_Load;
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ((System.ComponentModel.ISupportInitialize)webView22).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ComboBox comboBox2;
        private CheckBox checkBox1;
        private Label label7;
        private TextBox textBox2;
        private Label label8;
        private Button button1;
        private Button button2;
        private TextBox textBox3;
        private Label label9;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView22;
        private TextBox textBox4;
        private Label label10;
        private Label label11;
        private TextBox textBox5;
    }
}