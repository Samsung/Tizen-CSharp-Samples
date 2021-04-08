using Xamarin.Forms;

namespace SNSUI.Extensions
{
    /// <summary>
    /// The Type1Cell class is inherited from a BaseTypeCell.
    /// </summary>
    /// <remarks>
    /// Each property is displayed in the specified position.<br>
    /// The specified position is shown below.<br>
    /// <br>
    /// <table border=2 style="text-align:center;border-collapse:collapse;">
    ///        <tr>
    ///            <th height = 100 width=200 rowspan="2">Icon</th>
    ///            <th width = 150 > Text </th>
    ///            <th width=150>TextEnd</th>
    ///            <th width = 200 rowspan="2">CheckBox</th>
    ///        </tr>
    ///        <tr>
    ///            <th colspan = "2" > Sub </th>
    ///        </tr>
    /// </table>
    /// <br>
    /// Some properties do not work as expected on the TV profile.
    /// </remarks>
    /// <example>
    /// <code>
    /// new DataTemplate(() =>
    /// {
    ///        return new Type1Cell
    ///        {
    ///            Text = "Test Text",
    ///            TextEnd = "TestEnd Text",
    ///            Sub = "Test Sub",
    ///            Icon = ImageSource.FromFile("icon.png"),
    ///            IsCheckVisible = true,
    ///            IconWidth = 80,
    ///            IconHeight = 100,
    ///        };
    ///    });
    /// </code>
    /// </example>
    public class Type1Cell : BaseTypeCell
    {
    }
}
