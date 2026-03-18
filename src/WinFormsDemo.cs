namespace TablePanelLayoutResizer
{
  public partial class WinFormsDemo : Form
  {
    public WinFormsDemo()
    {
      InitializeComponent();
    }

    private void m_ctrlAllowColumnResizing_CheckedChanged(object sender, EventArgs e)
    {
      m_tableLayoutResizer.AllowColumnResizing = m_ctrlAllowColumnResizing.Checked;
    }

    private void m_ctrlAllowRowResizing_CheckedChanged(object sender, EventArgs e)
    {
      m_tableLayoutResizer.AllowRowResizing = m_ctrlAllowRowResizing.Checked;

    }

    private void m_ctrlDoubleBuffered_CheckedChanged(object sender, EventArgs e)
    {
      this.DoubleBuffered = m_ctrlDoubleBuffered.Checked;
    }



  }
}
