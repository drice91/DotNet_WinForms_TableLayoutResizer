using System.ComponentModel;

namespace TablePanelLayoutResizer.Lib;

/// <summary>
/// Represents the result of a hit test on a <see cref="TableLayoutPanel"/>.
/// </summary>
public record TableLayoutResizerHitTest
{
  /// <summary>
  /// Gets or sets the index of the row that was hit, or -1 if no row boundary was hit.
  /// </summary>
  public int RowIndex { get; set; }

  /// <summary>
  /// Gets or sets the index of the column that was hit, or -1 if no column boundary was hit.
  /// </summary>
  public int ColumnIndex { get; set; }

  /// <summary>
  /// Represents an empty hit test result where no row or column boundary was hit.
  /// </summary>
  public static readonly TableLayoutResizerHitTest Empty = new();

  /// <summary>
  /// Initializes a new instance of the <see cref="TableLayoutResizerHitTest"/> class with no hit indices.
  /// </summary>
  public TableLayoutResizerHitTest()
  {
    RowIndex = -1;
    ColumnIndex = -1;
  }

  /// <summary>
  /// Returns a string that represents the current object.
  /// </summary>
  /// <returns>A string that represents the current object.</returns>
  public override string ToString()
  {
    return $"C={ColumnIndex}, R={RowIndex}";
  }
}

public delegate void TableLayoutResizerBeginResizeEventHandler(object sender, TableLayoutResizerBeginResizeEventArgs e);

/// <summary>
/// Event arguments for the <see cref="TableLayoutResizer.BeginLayoutResize"/> event.
/// </summary>
public class TableLayoutResizerBeginResizeEventArgs : CancelEventArgs
{
  /// <summary>
  /// Gets the hit test results indicating which row and/or column boundary was hit.
  /// </summary>
  public TableLayoutResizerHitTest HitTest { get; private set; }

  /// <summary>
  /// Gets or sets whether a <see cref="SizeType"/> conversion is required to perform the resize operation.
  /// </summary>
  public bool RequiresSizeTypeConversion { get; set; }

  /// <summary>
  /// Initializes a new instance of the <see cref="TableLayoutResizerBeginResizeEventArgs"/> class.
  /// </summary>
  /// <param name="hitTest">The hit test results.</param>
  public TableLayoutResizerBeginResizeEventArgs(TableLayoutResizerHitTest hitTest)
  {
    HitTest = hitTest;
  }
}

/// <summary>
/// Represents the method that will handle the <see cref="TableLayoutResizer.LayoutResizing"/> event.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">A <see cref="TableLayoutResizerResizingEventArgs"/> that contains the event data.</param>
public delegate void TableLayoutResizerResizingEventHandler(object sender, TableLayoutResizerResizingEventArgs e);

/// <summary>
/// Event arguments for the <see cref="TableLayoutResizer.LayoutResizing"/> event.
/// </summary>
public class TableLayoutResizerResizingEventArgs : CancelEventArgs
{
  /// <summary>
  /// Gets or sets the index of the row being resized, or -1 if no row is being resized.
  /// </summary>
  public int RowIndex { get; set; }

  /// <summary>
  /// Gets or sets the index of the column being resized, or -1 if no column is being resized.
  /// </summary>
  public int ColumnIndex { get; set; }

  /// <summary>
  /// Gets or sets the original size (height for rows, width for columns) of the cell before resizing.
  /// </summary>
  public int OldSize { get; set; }

  /// <summary>
  /// Gets or sets the new proposed size (height for rows, width for columns) of the cell.
  /// </summary>
  public int NewSize { get; set; }
}

/// <summary>
/// Represents the method that will handle the <see cref="TableLayoutResizer.LayoutResized"/> event.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">A <see cref="TableLayoutResizerResizedEventArgs"/> that contains the event data.</param>
public delegate void TableLayoutResizerResizedEventHandler(object sender, TableLayoutResizerResizedEventArgs e);

/// <summary>
/// Event arguments for the <see cref="TableLayoutResizer.LayoutResized"/> event.
/// </summary>
public class TableLayoutResizerResizedEventArgs : EventArgs
{
  /// <summary>
  /// Gets or sets the index of the row that was resized, or -1.
  /// </summary>
  public int RowIndex { get; set; }

  /// <summary>
  /// Gets or sets the index of the column that was resized, or -1.
  /// </summary>
  public int ColumnIndex { get; set; }

  /// <summary>
  /// Gets or sets the final size (width and height) of the resized cell.
  /// </summary>
  public Size FinalSize { get; set; }
}

/// <summary>
/// Data Transfer Object representing the full layout of a <see cref="TableLayoutPanel"/>.
/// </summary>
public class TableLayoutResizerLayout
{
  /// <summary>
  /// Gets or sets the styles of the rows in the layout.
  /// </summary>
  public TableLayoutResizerRowStyle[] RowStyles { get; set; }

  /// <summary>
  /// Gets or sets the styles of the columns in the layout.
  /// </summary>
  public TableLayoutResizerColumnStyle[] ColumnStyles { get; set; }
}

/// <summary>
/// Data Transfer Object representing a row's style.
/// </summary>
public class TableLayoutResizerRowStyle
{
  /// <summary>
  /// Gets or sets the size type of the row.
  /// </summary>
  public SizeType SizeType { get; set; }

  /// <summary>
  /// Gets or sets the height of the row.
  /// </summary>
  public float Height { get; set; }
}

/// <summary>
/// Data Transfer Object representing a column's style.
/// </summary>
public class TableLayoutResizerColumnStyle
{
  /// <summary>
  /// Gets or sets the size type of the column.
  /// </summary>
  public SizeType SizeType { get; set; }

  /// <summary>
  /// Gets or sets the width of the column.
  /// </summary>
  public float Width { get; set; }
}

///=================================================================================================
/// <summary>
/// A component that can be used with a <see cref="TableLayoutPanel"/> to allow resizing of its
/// rows or columns.
/// </summary>
///=================================================================================================
public class TableLayoutResizer : Component, INotifyPropertyChanged, INotifyPropertyChanging
{

  private Point m_mouseDownLocation;
  private bool m_isResizing;
  private int m_resizeRowIndex = -1;
  private int m_resizeColIndex = -1;
  private TableLayoutPanel m_tableLayoutPanel;
  private Cursor m_oldCursor;
  private TableLayoutResizerHitTest m_lastHitTest = TableLayoutResizerHitTest.Empty;
  private bool m_allowRowResizing = true;
  private bool m_allowColumnResizing = true;
  private Size m_resizeMargin = new Size(2, 2);
  private Size cellSizeMin = new Size(10, 10);

  /// <summary>
  /// Occurs when a resizing operation is about to begin.
  /// </summary>
  public event TableLayoutResizerBeginResizeEventHandler BeginLayoutResize;

  /// <summary>
  /// Occurs while a row or column is being resized.
  /// </summary>
  public event TableLayoutResizerResizingEventHandler LayoutResizing;

  /// <summary>
  /// Occurs after a resizing operation has finished.
  /// </summary>
  public event TableLayoutResizerResizedEventHandler LayoutResized;

  /// <summary>
  /// Occurs when a property value changes.
  /// </summary>
  public event PropertyChangedEventHandler PropertyChanged;

  /// <summary>
  /// Occurs when a property value is changing.
  /// </summary>
  public event PropertyChangingEventHandler PropertyChanging;

  ///=================================================================================================
  /// <summary>
  /// Saves the layout of the TableLayoutPanel.
  /// </summary>
  ///
  /// <returns>
  /// A <see cref="TableLayoutResizerLayout"/> containing the layout information.
  /// </returns>
  ///=================================================================================================
  public TableLayoutResizerLayout SaveLayout()
  {
    if (m_tableLayoutPanel == null)
    {
      return null;
    }

    TableLayoutResizerLayout dto = new()
    {
      RowStyles = new TableLayoutResizerRowStyle[m_tableLayoutPanel.RowStyles.Count],
      ColumnStyles = new TableLayoutResizerColumnStyle[m_tableLayoutPanel.ColumnStyles.Count]
    };

    for (int i = 0; i < m_tableLayoutPanel.RowStyles.Count; i++)
    {
      dto.RowStyles[i] = new TableLayoutResizerRowStyle
      {
        SizeType = m_tableLayoutPanel.RowStyles[i].SizeType,
        Height = m_tableLayoutPanel.RowStyles[i].Height
      };
    }

    for (int i = 0; i < m_tableLayoutPanel.ColumnStyles.Count; i++)
    {
      dto.ColumnStyles[i] = new TableLayoutResizerColumnStyle
      {
        SizeType = m_tableLayoutPanel.ColumnStyles[i].SizeType,
        Width = m_tableLayoutPanel.ColumnStyles[i].Width
      };
    }

    return dto;
  }

  ///=================================================================================================
  /// <summary>
  /// Restores the layout of the TableLayoutPanel.
  /// </summary>
  ///
  /// <param name="dto">The layout information to restore.</param>
  ///=================================================================================================
  public void RestoreLayout(TableLayoutResizerLayout dto)
  {
    if (m_tableLayoutPanel == null || dto == null)
    {
      return;
    }

    if (dto.RowStyles != null)
    {
      for (int i = 0; i < Math.Min(m_tableLayoutPanel.RowStyles.Count, dto.RowStyles.Length); i++)
      {
        m_tableLayoutPanel.RowStyles[i].SizeType = dto.RowStyles[i].SizeType;
        m_tableLayoutPanel.RowStyles[i].Height = dto.RowStyles[i].Height;
      }
    }

    if (dto.ColumnStyles != null)
    {
      for (int i = 0; i < Math.Min(m_tableLayoutPanel.ColumnStyles.Count, dto.ColumnStyles.Length); i++)
      {
        m_tableLayoutPanel.ColumnStyles[i].SizeType = dto.ColumnStyles[i].SizeType;
        m_tableLayoutPanel.ColumnStyles[i].Width = dto.ColumnStyles[i].Width;
      }
    }

    m_tableLayoutPanel.PerformLayout();
  }

  ///=================================================================================================
  /// <summary>
  /// Default constructor.
  /// </summary>
  ///=================================================================================================
  public TableLayoutResizer()
  {
  }

  ///=================================================================================================
  /// <summary>
  /// Constructor.
  /// </summary>
  ///
  /// <param name="container">The container.</param>
  ///=================================================================================================
  public TableLayoutResizer(IContainer container) : this()
  {
    container.Add(this);
  }

  /// <summary>
  /// Gets or sets the TableLayoutPanel to resize.
  /// </summary>
  [DefaultValue(null)]
  [Category("Behavior")]
  [Description("Gets or sets the TableLayoutPanel to resize.")]
  public TableLayoutPanel TableLayout
  {
    get => m_tableLayoutPanel;
    set
    {
      if (m_tableLayoutPanel != value)
      {
        OnPropertyChanging(nameof(TableLayout));
        m_tableLayoutPanel = value;
        OnPropertyChanged(nameof(TableLayout));
      }
    }
  }

  /// <summary>
  /// Gets or sets whether row resizing is allowed.
  /// </summary>
  [DefaultValue(true)]
  [Category("Behavior")]
  [Description("Gets or sets whether row resizing is allowed.")]
  public bool AllowRowResizing
  {
    get => m_allowRowResizing;
    set
    {
      if (m_allowRowResizing != value)
      {
        OnPropertyChanging(nameof(AllowRowResizing));
        m_allowRowResizing = value;
        OnPropertyChanged(nameof(AllowRowResizing));
      }
    }
  }

  /// <summary>
  /// Gets or sets whether column resizing is allowed.
  /// </summary>
  [DefaultValue(true)]
  [Category("Behavior")]
  [Description("Gets or sets whether column resizing is allowed.")]
  public bool AllowColumnResizing
  {
    get => m_allowColumnResizing;
    set
    {
      if (m_allowColumnResizing != value)
      {
        OnPropertyChanging(nameof(AllowColumnResizing));
        m_allowColumnResizing = value;
        OnPropertyChanged(nameof(AllowColumnResizing));
      }
    }
  }

  /// <summary>
  /// Gets or sets the margin around cell boundaries where resizing is triggered.
  /// </summary>
  [DefaultValue(typeof(Size), "2, 2")]
  [Category("Behavior")]
  [Description("Gets or sets the margin around cell boundaries where resizing is triggered.")]
  public Size ResizeMargin
  {
    get => m_resizeMargin;
    set
    {
      if (m_resizeMargin != value)
      {
        OnPropertyChanging(nameof(ResizeMargin));
        m_resizeMargin = value;
        OnPropertyChanged(nameof(ResizeMargin));
      }
    }
  }

  /// <summary>
  /// Gets or sets the minimum size of a cell during resizing.
  /// </summary>
  [DefaultValue(typeof(Size), "10, 10")]
  [Category("Behavior")]
  [Description("Gets or sets the minimum size of a cell during resizing.")]
  public Size CellSizeMin
  {
    get => cellSizeMin;
    set
    {
      if (cellSizeMin != value)
      {
        OnPropertyChanging(nameof(CellSizeMin));
        cellSizeMin = value;
        OnPropertyChanged(nameof(CellSizeMin));
      }
    }
  }


  ///=================================================================================================
  /// <summary>
  /// Attach or detach event-handlers for the TableLayoutPanel
  /// </summary>
  ///
  /// <param name="attach">True to attach.</param>
  ///=================================================================================================
  private void AttachTableLayoutEventHandlers(bool attach)
  {
    if (m_tableLayoutPanel != null)
    {
      if (attach)
      {
        m_tableLayoutPanel.MouseDown += TableLayoutPanel_MouseDown;
        m_tableLayoutPanel.MouseMove += TableLayoutPanel_MouseMove;
        m_tableLayoutPanel.MouseUp += TableLayoutPanelMouseUp;
        m_tableLayoutPanel.MouseLeave += TableLayoutPanel_MouseLeave;
      }
      else
      {
        m_tableLayoutPanel.MouseDown -= TableLayoutPanel_MouseDown;
        m_tableLayoutPanel.MouseMove -= TableLayoutPanel_MouseMove;
        m_tableLayoutPanel.MouseUp -= TableLayoutPanelMouseUp;
        m_tableLayoutPanel.MouseLeave -= TableLayoutPanel_MouseLeave;
      }
    }
  }


  ///=================================================================================================
  /// <summary>
  /// Event handler. Called by TableLayoutPanel for mouse leave events.
  /// </summary>
  ///
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">     Event information.</param>
  ///=================================================================================================
  private void TableLayoutPanel_MouseLeave(object sender, EventArgs e)
  {
    if (m_oldCursor != null && m_tableLayoutPanel != null)
    {
      m_tableLayoutPanel.Cursor = m_oldCursor;
      m_oldCursor = null;
      m_lastHitTest = TableLayoutResizerHitTest.Empty;
    }
  }


  ///=================================================================================================
  /// <summary>
  /// Event handler. Called by TableLayoutPanel for mouse down events.
  /// </summary>
  ///
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">     Mouse event information.</param>
  ///=================================================================================================
  private void TableLayoutPanel_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Button == MouseButtons.Left && m_tableLayoutPanel != null)
    {
      TableLayoutResizerHitTest hitTest = HitTest(e.Location);
      int rowIndex = m_allowRowResizing ? hitTest.RowIndex : -1;
      int colIndex = m_allowColumnResizing ? hitTest.ColumnIndex : -1;

      if (rowIndex >= 0 || colIndex >= 0)
      {

        if (BeginLayoutResize != null)
        {

          TableLayoutResizerBeginResizeEventArgs args = new(hitTest);

          // Determine if conversion is needed
          if (rowIndex >= 0 && rowIndex < m_tableLayoutPanel.RowStyles.Count)
          {
            if (m_tableLayoutPanel.RowStyles[rowIndex].SizeType != SizeType.Absolute)
            {
              args.RequiresSizeTypeConversion = true;
            }
            else if (rowIndex + 1 < m_tableLayoutPanel.RowStyles.Count && m_tableLayoutPanel.RowStyles[rowIndex + 1].SizeType != SizeType.Absolute)
            {
              args.RequiresSizeTypeConversion = true;
            }
          }

          if (!args.RequiresSizeTypeConversion && colIndex >= 0 && colIndex < m_tableLayoutPanel.ColumnStyles.Count)
          {
            if (m_tableLayoutPanel.ColumnStyles[colIndex].SizeType != SizeType.Absolute)
            {
              args.RequiresSizeTypeConversion = true;
            }
            else if (colIndex + 1 < m_tableLayoutPanel.ColumnStyles.Count && m_tableLayoutPanel.ColumnStyles[colIndex + 1].SizeType != SizeType.Absolute)
            {
              args.RequiresSizeTypeConversion = true;
            }
          }

          BeginLayoutResize.Invoke(this, args);
          if (args.Cancel)
          {
            return;
          }
        }

        m_mouseDownLocation = e.Location;
        m_isResizing = true;
        m_resizeRowIndex = rowIndex;
        m_resizeColIndex = colIndex;
        m_tableLayoutPanel.Capture = true;
      }
    }
  }


  ///=================================================================================================
  /// <summary>
  /// Event handler. Called by TableLayoutPanel for mouse move events.
  /// </summary>
  ///
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">     Mouse event information.</param>
  ///=================================================================================================
  private void TableLayoutPanel_MouseMove(object sender, MouseEventArgs e)
  {

    if (m_tableLayoutPanel == null)
    {
      return;
    }

    if (m_isResizing && (m_resizeRowIndex >= 0 || m_resizeColIndex >= 0))
    {
      int diffX = e.X - m_mouseDownLocation.X;
      int diffY = e.Y - m_mouseDownLocation.Y;

      if (diffX == 0 && diffY == 0)
      {
        return;
      }

      int oldRowSize = -1;
      int newRowSize = -1;
      int oldColSize = -1;
      int newColSize = -1;

      if (m_resizeRowIndex >= 0)
      {
        int[] heights = m_tableLayoutPanel.GetRowHeights();
        oldRowSize = heights[m_resizeRowIndex];
        newRowSize = oldRowSize + diffY;

        if (newRowSize < cellSizeMin.Height)
        {
          newRowSize = cellSizeMin.Height;
          diffY = newRowSize - oldRowSize;
        }

        if (m_resizeRowIndex + 1 < heights.Length)
        {
          int nextRowHeight = heights[m_resizeRowIndex + 1];
          if (nextRowHeight - diffY < cellSizeMin.Height)
          {
            diffY = nextRowHeight - cellSizeMin.Height;
            newRowSize = oldRowSize + diffY;
          }
        }
      }

      if (m_resizeColIndex >= 0)
      {
        int[] widths = m_tableLayoutPanel.GetColumnWidths();
        oldColSize = widths[m_resizeColIndex];
        newColSize = oldColSize + diffX;

        if (newColSize < cellSizeMin.Width)
        {
          newColSize = cellSizeMin.Width;
          diffX = newColSize - oldColSize;
        }

        if (m_resizeColIndex + 1 < widths.Length)
        {
          int nextColWidth = widths[m_resizeColIndex + 1];
          if (nextColWidth - diffX < cellSizeMin.Width)
          {
            diffX = nextColWidth - cellSizeMin.Width;
            newColSize = oldColSize + diffX;
          }
        }
      }

      if (diffX == 0 && diffY == 0)
      {
        return;
      }

      if (LayoutResizing != null)
      {
        TableLayoutResizerResizingEventArgs args = new()
        {
          RowIndex = m_resizeRowIndex,
          ColumnIndex = m_resizeColIndex,
          OldSize = m_resizeRowIndex >= 0 ? oldRowSize : oldColSize,
          NewSize = m_resizeRowIndex >= 0 ? newRowSize : newColSize
        };

        LayoutResizing.Invoke(this, args);
        if (args.Cancel)
        {
          return;
        }
      }

      ResizeLayout(m_tableLayoutPanel, diffX, diffY);
      m_mouseDownLocation = e.Location;
    }
    else
    {
      TableLayoutResizerHitTest hit = HitTest(e.Location);

      bool rowHit = m_allowRowResizing && hit.RowIndex >= 0;
      bool colHit = m_allowColumnResizing && hit.ColumnIndex >= 0;

      bool prevRowHit = m_allowRowResizing && m_lastHitTest.RowIndex >= 0;
      bool prevColHit = m_allowColumnResizing && m_lastHitTest.ColumnIndex >= 0;

      if (rowHit != prevRowHit || colHit != prevColHit || (rowHit && hit.RowIndex != m_lastHitTest.RowIndex) || (colHit && hit.ColumnIndex != m_lastHitTest.ColumnIndex))
      {
        if (rowHit || colHit)
        {
          if (!prevRowHit && !prevColHit)
          {
            m_oldCursor = m_tableLayoutPanel.Cursor;
          }

          if (rowHit && colHit)
          {
            m_tableLayoutPanel.Cursor = Cursors.SizeAll;
          }
          else if (rowHit)
          {
            m_tableLayoutPanel.Cursor = Cursors.HSplit;
          }
          else
          {
            m_tableLayoutPanel.Cursor = Cursors.VSplit;
          }
        }
        else
        {
          if (m_oldCursor != null)
          {
            m_tableLayoutPanel.Cursor = m_oldCursor;
            m_oldCursor = null;
          }
        }

        m_lastHitTest = hit;
      }
    }
  }


  ///=================================================================================================
  /// <summary>
  /// Table layout panel mouse up.
  /// </summary>
  ///
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">     Mouse event information.</param>
  ///=================================================================================================
  private void TableLayoutPanelMouseUp(object sender, MouseEventArgs e)
  {
    if (m_isResizing)
    {
      if (m_tableLayoutPanel != null)
      {
        int[] rowHeights = m_tableLayoutPanel.GetRowHeights();
        for (int i = 0; i < rowHeights.Length; i++)
        {
          if (rowHeights[i] < cellSizeMin.Height)
          {
            m_tableLayoutPanel.RowStyles[i].SizeType = SizeType.Absolute;
            m_tableLayoutPanel.RowStyles[i].Height = cellSizeMin.Height;
          }
        }

        int[] colWidths = m_tableLayoutPanel.GetColumnWidths();
        for (int i = 0; i < colWidths.Length; i++)
        {
          if (colWidths[i] < cellSizeMin.Width)
          {
            m_tableLayoutPanel.ColumnStyles[i].SizeType = SizeType.Absolute;
            m_tableLayoutPanel.ColumnStyles[i].Width = cellSizeMin.Width;
          }
        }
        m_tableLayoutPanel.PerformLayout();
      }

      if (LayoutResized != null)
      {
        int finalWidth = -1;
        int finalHeight = -1;

        if (m_resizeColIndex >= 0)
        {
          int[] widths = m_tableLayoutPanel.GetColumnWidths();
          if (m_resizeColIndex < widths.Length)
          {
            finalWidth = widths[m_resizeColIndex];
          }
        }

        if (m_resizeRowIndex >= 0)
        {
          int[] heights = m_tableLayoutPanel.GetRowHeights();
          if (m_resizeRowIndex < heights.Length)
          {
            finalHeight = heights[m_resizeRowIndex];
          }
        }

        LayoutResized.Invoke(this, new TableLayoutResizerResizedEventArgs
        {
          RowIndex = m_resizeRowIndex,
          ColumnIndex = m_resizeColIndex,
          FinalSize = new Size(finalWidth, finalHeight)
        });
      }
    }

    m_isResizing = false;
    if (m_tableLayoutPanel != null)
    {
      m_tableLayoutPanel.Capture = false;
    }
  }


  ///=================================================================================================
  /// <summary>
  /// Executes the 'property changing' action.
  /// </summary>
  ///
  /// <param name="propertyName">Name of the property.</param>
  ///=================================================================================================
  protected virtual void OnPropertyChanging(string propertyName)
  {
    PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

    if (propertyName == nameof(TableLayout))
    {
      AttachTableLayoutEventHandlers(false);
    }
  }


  ///=================================================================================================
  /// <summary>
  /// Executes the 'property changed' action.
  /// </summary>
  ///
  /// <param name="propertyName">Name of the property.</param>
  ///=================================================================================================
  protected virtual void OnPropertyChanged(string propertyName)
  {
    if (propertyName == nameof(TableLayout))
    {
      AttachTableLayoutEventHandlers(true);
    }

    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }


  ///=================================================================================================
  /// <summary>
  /// Performs a hit test to determine if the specified location is near a row or column boundary.
  /// </summary>
  ///
  /// <param name="location">The coordinates relative to the TableLayoutPanel.</param>
  ///
  /// <returns>
  /// A <see cref="TableLayoutResizerHitTest"/> containing the indices of the hit row and column.
  /// </returns>
  ///=================================================================================================
  public TableLayoutResizerHitTest HitTest(Point location)
  {
    if (m_tableLayoutPanel == null)
    {
      return TableLayoutResizerHitTest.Empty;
    }

    int hitRowIndex = -1;
    int hitColIndex = -1;

    // -------------------------------------------------------
    // Check rows
    // -------------------------------------------------------
    if (AllowRowResizing)
    {
      int currentY = 0;
      int[] rowHeights = m_tableLayoutPanel.GetRowHeights();
      for (int rowIndex = 0; rowIndex < rowHeights.Length - 1; rowIndex++)
      {
        currentY += rowHeights[rowIndex];
        if (Math.Abs(location.Y - currentY) <= m_resizeMargin.Height)
        {
          hitRowIndex = rowIndex;
          break;
        }
      }
    }

    // -------------------------------------------------------
    // Check columns
    // -------------------------------------------------------
    if (AllowColumnResizing)
    {
      int currentX = 0;
      int[] colWidths = m_tableLayoutPanel.GetColumnWidths();
      for (int colIndex = 0; colIndex < colWidths.Length - 1; colIndex++)
      {
        currentX += colWidths[colIndex];
        if (Math.Abs(location.X - currentX) <= m_resizeMargin.Width)
        {
          hitColIndex = colIndex;
          break;
        }
      }
    }

    return new TableLayoutResizerHitTest
    {
      ColumnIndex = hitColIndex,
      RowIndex = hitRowIndex
    };
  }


  ///=================================================================================================
  /// <summary>
  /// Resize layout.
  /// </summary>
  ///
  /// <param name="tlp">  The tlp.</param>
  /// <param name="diffX">The difference x coordinate.</param>
  /// <param name="diffY">The difference y coordinate.</param>
  ///=================================================================================================
  private void ResizeLayout(TableLayoutPanel tlp, int diffX, int diffY)
  {

    if (m_resizeRowIndex >= 0 && m_resizeRowIndex < tlp.RowStyles.Count)
    {
      int[] heights = tlp.GetRowHeights();
      RowStyle prevStyle = tlp.RowStyles[m_resizeRowIndex];
      RowStyle nextStyle = m_resizeRowIndex + 1 < tlp.RowStyles.Count ? tlp.RowStyles[m_resizeRowIndex + 1] : null;

      if (prevStyle.SizeType != SizeType.Absolute)
      {
        prevStyle.SizeType = SizeType.Absolute;
        prevStyle.Height = heights[m_resizeRowIndex];
      }
      prevStyle.Height = Math.Max(prevStyle.Height + diffY, 0);

      if (nextStyle != null)
      {
        if (nextStyle.SizeType != SizeType.Absolute)
        {
          nextStyle.SizeType = SizeType.Absolute;
          nextStyle.Height = heights[m_resizeRowIndex + 1];
        }
        nextStyle.Height = Math.Max(nextStyle.Height - diffY, 0);
      }
    }

    if (m_resizeColIndex >= 0 && m_resizeColIndex < tlp.ColumnStyles.Count)
    {
      int[] widths = tlp.GetColumnWidths();
      ColumnStyle prevStyle = tlp.ColumnStyles[m_resizeColIndex];
      ColumnStyle nextStyle = m_resizeColIndex + 1 < tlp.ColumnStyles.Count ? tlp.ColumnStyles[m_resizeColIndex + 1] : null;

      if (prevStyle.SizeType != SizeType.Absolute)
      {
        prevStyle.SizeType = SizeType.Absolute;
        prevStyle.Width = widths[m_resizeColIndex];
      }
      prevStyle.Width = Math.Max(prevStyle.Width + diffX, 0);

      if (nextStyle != null)
      {
        if (nextStyle.SizeType != SizeType.Absolute)
        {
          nextStyle.SizeType = SizeType.Absolute;
          nextStyle.Width = widths[m_resizeColIndex + 1];
        }
        nextStyle.Width = Math.Max(nextStyle.Width - diffX, 0);
      }
    }

    tlp.PerformLayout();
  }


  ///=================================================================================================
  /// <summary>
  /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" />
  /// and optionally releases the managed resources.
  /// </summary>
  ///
  /// <param name="disposing"><see langword="true" /> to release both managed and unmanaged resources;
  ///                         <see langword="false" /> to release only unmanaged resources.</param>
  ///=================================================================================================
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      AttachTableLayoutEventHandlers(false);
    }

    base.Dispose(disposing);
  }
}