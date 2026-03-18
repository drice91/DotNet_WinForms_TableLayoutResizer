namespace TablePanelLayoutResizer
{
  partial class Form1
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      m_tableLayoutPanel = new TableLayoutPanel();
      label16 = new Label();
      label11 = new Label();
      label4 = new Label();
      label12 = new Label();
      label15 = new Label();
      label14 = new Label();
      label13 = new Label();
      label10 = new Label();
      label9 = new Label();
      label8 = new Label();
      label7 = new Label();
      label6 = new Label();
      label5 = new Label();
      label3 = new Label();
      label2 = new Label();
      label1 = new Label();
      m_tableLayoutResizer = new TableLayoutResizer(components);
      m_ctrlAllowColumnResizing = new CheckBox();
      m_ctrlAllowRowResizing = new CheckBox();
      label17 = new Label();
      m_ctrlDoubleBuffered = new CheckBox();
      m_tableLayoutPanel.SuspendLayout();
      SuspendLayout();
      // 
      // m_tableLayoutPanel
      // 
      m_tableLayoutPanel.ColumnCount = 4;
      m_tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
      m_tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
      m_tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 109F));
      m_tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 168F));
      m_tableLayoutPanel.Controls.Add(label16, 3, 3);
      m_tableLayoutPanel.Controls.Add(label11, 3, 2);
      m_tableLayoutPanel.Controls.Add(label4, 2, 2);
      m_tableLayoutPanel.Controls.Add(label12, 3, 0);
      m_tableLayoutPanel.Controls.Add(label15, 2, 3);
      m_tableLayoutPanel.Controls.Add(label14, 1, 3);
      m_tableLayoutPanel.Controls.Add(label13, 0, 3);
      m_tableLayoutPanel.Controls.Add(label10, 1, 2);
      m_tableLayoutPanel.Controls.Add(label9, 0, 2);
      m_tableLayoutPanel.Controls.Add(label8, 3, 1);
      m_tableLayoutPanel.Controls.Add(label7, 2, 1);
      m_tableLayoutPanel.Controls.Add(label6, 1, 1);
      m_tableLayoutPanel.Controls.Add(label5, 0, 1);
      m_tableLayoutPanel.Controls.Add(label3, 2, 0);
      m_tableLayoutPanel.Controls.Add(label2, 1, 0);
      m_tableLayoutPanel.Controls.Add(label1, 0, 0);
      m_tableLayoutPanel.Location = new Point(8, 8);
      m_tableLayoutPanel.Name = "m_tableLayoutPanel";
      m_tableLayoutPanel.RowCount = 4;
      m_tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      m_tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      m_tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 73F));
      m_tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
      m_tableLayoutPanel.Size = new Size(560, 320);
      m_tableLayoutPanel.TabIndex = 0;
      // 
      // label16
      // 
      label16.BackColor = Color.FromArgb(128, 255, 128);
      label16.Dock = DockStyle.Fill;
      label16.Location = new Point(394, 270);
      label16.Margin = new Padding(3);
      label16.Name = "label16";
      label16.Size = new Size(163, 47);
      label16.TabIndex = 19;
      label16.Text = "3,3";
      // 
      // label11
      // 
      label11.BackColor = Color.FromArgb(255, 192, 128);
      label11.Dock = DockStyle.Fill;
      label11.Location = new Point(394, 197);
      label11.Margin = new Padding(3);
      label11.Name = "label11";
      label11.Size = new Size(163, 67);
      label11.TabIndex = 18;
      label11.Text = "3,2";
      // 
      // label4
      // 
      label4.BackColor = Color.FromArgb(128, 255, 128);
      label4.Dock = DockStyle.Fill;
      label4.Location = new Point(285, 197);
      label4.Margin = new Padding(3);
      label4.Name = "label4";
      label4.Size = new Size(103, 67);
      label4.TabIndex = 17;
      label4.Text = "2,2";
      // 
      // label12
      // 
      label12.BackColor = Color.FromArgb(128, 255, 255);
      label12.Dock = DockStyle.Fill;
      label12.Location = new Point(394, 3);
      label12.Margin = new Padding(3);
      label12.Name = "label12";
      label12.Size = new Size(163, 91);
      label12.TabIndex = 16;
      label12.Text = "3,0";
      // 
      // label15
      // 
      label15.BackColor = Color.FromArgb(255, 128, 255);
      label15.Dock = DockStyle.Fill;
      label15.Location = new Point(285, 270);
      label15.Margin = new Padding(3);
      label15.Name = "label15";
      label15.Size = new Size(103, 47);
      label15.TabIndex = 14;
      label15.Text = "2,3";
      // 
      // label14
      // 
      label14.BackColor = Color.FromArgb(128, 128, 255);
      label14.Dock = DockStyle.Fill;
      label14.Location = new Point(144, 270);
      label14.Margin = new Padding(3);
      label14.Name = "label14";
      label14.Size = new Size(135, 47);
      label14.TabIndex = 13;
      label14.Text = "1,3";
      // 
      // label13
      // 
      label13.BackColor = Color.FromArgb(128, 255, 255);
      label13.Dock = DockStyle.Fill;
      label13.Location = new Point(3, 267);
      label13.Name = "label13";
      label13.Size = new Size(135, 53);
      label13.TabIndex = 12;
      label13.Text = "0,3";
      // 
      // label10
      // 
      label10.BackColor = Color.FromArgb(255, 255, 128);
      label10.Dock = DockStyle.Fill;
      label10.Location = new Point(144, 197);
      label10.Margin = new Padding(3);
      label10.Name = "label10";
      label10.Size = new Size(135, 67);
      label10.TabIndex = 9;
      label10.Text = "1,2";
      // 
      // label9
      // 
      label9.BackColor = Color.FromArgb(255, 128, 128);
      label9.Dock = DockStyle.Fill;
      label9.Location = new Point(3, 194);
      label9.Name = "label9";
      label9.Size = new Size(135, 73);
      label9.TabIndex = 8;
      label9.Text = "0,2";
      // 
      // label8
      // 
      label8.BackColor = Color.Silver;
      label8.Dock = DockStyle.Fill;
      label8.Location = new Point(394, 97);
      label8.Name = "label8";
      label8.Size = new Size(163, 97);
      label8.TabIndex = 7;
      label8.Text = "3,1";
      // 
      // label7
      // 
      label7.BackColor = Color.FromArgb(255, 128, 255);
      label7.Dock = DockStyle.Fill;
      label7.Location = new Point(285, 97);
      label7.Name = "label7";
      label7.Size = new Size(103, 97);
      label7.TabIndex = 6;
      label7.Text = "2,1";
      // 
      // label6
      // 
      label6.BackColor = Color.FromArgb(128, 128, 255);
      label6.Dock = DockStyle.Fill;
      label6.Location = new Point(144, 97);
      label6.Name = "label6";
      label6.Size = new Size(135, 97);
      label6.TabIndex = 5;
      label6.Text = "1,1";
      // 
      // label5
      // 
      label5.BackColor = Color.FromArgb(128, 255, 255);
      label5.Dock = DockStyle.Fill;
      label5.Location = new Point(3, 100);
      label5.Margin = new Padding(3);
      label5.Name = "label5";
      label5.Size = new Size(135, 91);
      label5.TabIndex = 4;
      label5.Text = "0,1";
      // 
      // label3
      // 
      label3.BackColor = Color.FromArgb(255, 255, 128);
      label3.Dock = DockStyle.Fill;
      label3.Location = new Point(285, 0);
      label3.Name = "label3";
      label3.Size = new Size(103, 97);
      label3.TabIndex = 2;
      label3.Text = "2,0";
      // 
      // label2
      // 
      label2.BackColor = Color.FromArgb(255, 192, 128);
      label2.Dock = DockStyle.Fill;
      label2.Location = new Point(144, 3);
      label2.Margin = new Padding(3);
      label2.Name = "label2";
      label2.Size = new Size(135, 91);
      label2.TabIndex = 1;
      label2.Text = "1,0";
      // 
      // label1
      // 
      label1.BackColor = Color.FromArgb(255, 128, 128);
      label1.Dock = DockStyle.Fill;
      label1.Location = new Point(3, 3);
      label1.Margin = new Padding(3);
      label1.Name = "label1";
      label1.Size = new Size(135, 91);
      label1.TabIndex = 0;
      label1.Text = "0,0";
      // 
      // m_tableLayoutResizer
      // 
      m_tableLayoutResizer.TableLayout = m_tableLayoutPanel;
      // 
      // m_ctrlAllowColumnResizing
      // 
      m_ctrlAllowColumnResizing.AutoSize = true;
      m_ctrlAllowColumnResizing.Checked = true;
      m_ctrlAllowColumnResizing.CheckState = CheckState.Checked;
      m_ctrlAllowColumnResizing.Location = new Point(640, 16);
      m_ctrlAllowColumnResizing.Name = "m_ctrlAllowColumnResizing";
      m_ctrlAllowColumnResizing.Size = new Size(145, 19);
      m_ctrlAllowColumnResizing.TabIndex = 1;
      m_ctrlAllowColumnResizing.Text = "Allow Column resizing";
      m_ctrlAllowColumnResizing.UseVisualStyleBackColor = true;
      m_ctrlAllowColumnResizing.CheckedChanged += m_ctrlAllowColumnResizing_CheckedChanged;
      // 
      // m_ctrlAllowRowResizing
      // 
      m_ctrlAllowRowResizing.AutoSize = true;
      m_ctrlAllowRowResizing.Checked = true;
      m_ctrlAllowRowResizing.CheckState = CheckState.Checked;
      m_ctrlAllowRowResizing.Location = new Point(640, 40);
      m_ctrlAllowRowResizing.Name = "m_ctrlAllowRowResizing";
      m_ctrlAllowRowResizing.Size = new Size(125, 19);
      m_ctrlAllowRowResizing.TabIndex = 2;
      m_ctrlAllowRowResizing.Text = "Allow Row resizing";
      m_ctrlAllowRowResizing.UseVisualStyleBackColor = true;
      m_ctrlAllowRowResizing.CheckedChanged += m_ctrlAllowRowResizing_CheckedChanged;
      // 
      // label17
      // 
      label17.BackColor = SystemColors.Info;
      label17.Location = new Point(8, 376);
      label17.Name = "label17";
      label17.Size = new Size(752, 56);
      label17.TabIndex = 3;
      label17.Text = resources.GetString("label17.Text");
      // 
      // m_ctrlDoubleBuffered
      // 
      m_ctrlDoubleBuffered.AutoSize = true;
      m_ctrlDoubleBuffered.Checked = true;
      m_ctrlDoubleBuffered.CheckState = CheckState.Checked;
      m_ctrlDoubleBuffered.Location = new Point(640, 80);
      m_ctrlDoubleBuffered.Name = "m_ctrlDoubleBuffered";
      m_ctrlDoubleBuffered.Size = new Size(140, 19);
      m_ctrlDoubleBuffered.TabIndex = 4;
      m_ctrlDoubleBuffered.Text = "Form DoubleBuffered";
      m_ctrlDoubleBuffered.UseVisualStyleBackColor = true;
      m_ctrlDoubleBuffered.CheckedChanged += m_ctrlDoubleBuffered_CheckedChanged;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(m_ctrlDoubleBuffered);
      Controls.Add(label17);
      Controls.Add(m_ctrlAllowRowResizing);
      Controls.Add(m_ctrlAllowColumnResizing);
      Controls.Add(m_tableLayoutPanel);
      DoubleBuffered = true;
      Name = "Form1";
      Text = "Form1";
      m_tableLayoutPanel.ResumeLayout(false);
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private TableLayoutPanel m_tableLayoutPanel;
    private Label label9;
    private Label label8;
    private Label label7;
    private Label label6;
    private Label label5;
    private Label label3;
    private Label label2;
    private Label label1;
    private Label label16;
    private Label label11;
    private Label label4;
    private Label label12;
    private Label label15;
    private Label label14;
    private Label label13;
    private Label label10;
    private TableLayoutResizer m_tableLayoutResizer;
    private CheckBox m_ctrlAllowColumnResizing;
    private CheckBox m_ctrlAllowRowResizing;
    private Label label17;
    private CheckBox m_ctrlDoubleBuffered;
  }
}
