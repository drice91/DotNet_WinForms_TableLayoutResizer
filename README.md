# TableLayoutResizer

A non-visual component for Windows Forms that enables interactive resizing of rows and columns in a `TableLayoutPanel` by dragging their boundaries.

## Features

- **Interactive Resizing**: Drag row or column boundaries to resize cells at runtime.
- **Support for All SizeTypes**: Automatically converts `Percent` and `AutoSize` rows/columns to `Absolute` during resizing for predictable behavior.
- **Constraints**: Enforce minimum cell sizes via the `CellSizeMin` property.
- **Simultaneous Resizing**: Support for resizing both a row and a column at once if the mouse is at a corner intersection.
- **Layout Persistence**: Save and restore the layout (styles and sizes) using DTOs.
- **Customization**: Extensive events to intercept, cancel, or respond to resize operations.

## Properties

| Property | Category | Default | Description |
| :--- | :--- | :--- | :--- |
| `TableLayout` | Behavior | `null` | The `TableLayoutPanel` instance to enable resizing for. |
| `AllowRowResizing` | Behavior | `true` | Enables or disables the ability to resize rows. |
| `AllowColumnResizing` | Behavior | `true` | Enables or disables the ability to resize columns. |
| `ResizeMargin` | Appearance | `2, 2` | The distance (in pixels) from a boundary where the resize cursor appears. |
| `CellSizeMin` | Behavior | `10, 10` | The minimum width and height a cell can be resized to. |

## Events

- **`BeginLayoutResize`**: Occurs when a user starts dragging a boundary. Can be cancelled.
- **`LayoutResizing`**: Occurs repeatedly during the drag operation. Can be cancelled or used to implement custom constraints.
- **`LayoutResized`**: Occurs when the user releases the mouse button, finishing the resize operation.

## Usage

### In the Designer

1. Open your Form or UserControl designer.
2. Drag the `TableLayoutResizer` component from the Toolbox onto the designer.
3. In the Properties window, set the `TableLayout` property to the `TableLayoutPanel` you want to resize.
4. (Optional) Configure `AllowRowResizing`, `AllowColumnResizing`, and `CellSizeMin`.

### In Code

```csharp
// Enabling resizing at runtime
var resizer = new TableLayoutResizer(this.components);
resizer.TableLayout = myTableLayoutPanel;

// Handling events
resizer.BeginLayoutResize += (s, e) => {
    if (e.HitTest.RowIndex == 0) {
        e.Cancel = true; // Prevent resizing the first row
    }
};

resizer.LayoutResized += (s, e) => {
    Console.WriteLine($"Cell at R{e.RowIndex}C{e.ColumnIndex} resized to {e.FinalSize}");
};

// Persistence
var layoutDto = resizer.SaveLayout();
// ... save layoutDto to disk or database ...
resizer.RestoreLayout(layoutDto);
```

### JSON Serialization (using Newtonsoft.Json)

```csharp
using Newtonsoft.Json;

// Save to JSON string
string json = JsonConvert.SerializeObject(resizer.SaveLayout(), Formatting.Indented);

// Load from JSON string
var dto = JsonConvert.DeserializeObject<TableLayoutResizerLayout>(json);
resizer.RestoreLayout(dto);
```

### Stream Serialization (using System.IO and Newtonsoft.Json)

```csharp
using System.IO;
using Newtonsoft.Json;

// Save to Stream
public void SaveToStream(TableLayoutResizer resizer, Stream stream)
{
    var dto = resizer.SaveLayout();
    using var writer = new StreamWriter(stream, leaveOpen: true);
    using var jsonWriter = new JsonTextWriter(writer);
    var serializer = new JsonSerializer { Formatting = Formatting.Indented };
    serializer.Serialize(jsonWriter, dto);
}

// Load from Stream
public void LoadFromStream(TableLayoutResizer resizer, Stream stream)
{
    using var reader = new StreamReader(stream, leaveOpen: true);
    using var jsonReader = new JsonTextReader(reader);
    var serializer = new JsonSerializer();
    var dto = serializer.Deserialize<TableLayoutResizerLayout>(jsonReader);
    resizer.RestoreLayout(dto);
}
```

## Technical Notes

- When a non-Absolute row/column is resized, it is converted to `Absolute` using its current rendered pixel size. This prevents the layout from jumping unexpectedly.
- Post-resize validation ensures that all rows and columns respect the `CellSizeMin` even after the layout engine has finished its calculations.
