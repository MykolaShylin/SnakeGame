namespace SnakeWinFormsApp
{
    partial class RulesForm
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Items.AddRange(new object[] {
            "* В центре поля появляется серый квадрат (голова змеи).",
            "* В случайном месте появляется зеленый квадрат (фрукт).",
            "* Собирайте фрукты, что бы змея росла.",
            "* Для передвижения змеи используйте стрелочки.",
            "* Будьте аккуратны, съев саму себя или выйдя за границы поля - игра закончится.",
            "* Для полной победы - выростите самую большую змею, которая заполнит все поле."});
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(635, 129);
            this.listBox1.TabIndex = 0;
            // 
            // RulesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 129);
            this.Controls.Add(this.listBox1);
            this.Name = "RulesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Праила";
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox listBox1;
    }
}