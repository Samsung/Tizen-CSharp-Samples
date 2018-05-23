using System;
using Xamarin.Forms;

namespace SNSUI.Extensions
{
    /// <summary>
    /// The BaseTypeCell contains Text, TextEnd, Sub, Icon, and Checkbox(IsCheckVisible).
    /// </summary>
    /// <remarks>
    /// The BaseTypeCell is an abstract class inherited from a cell.<br>
    /// The Type1Cell class is used to inherit this class.<br>
    /// Properties are used equally and are only at slightly different positions.<br>
    /// Each property is displayed in the specified position.<br>
    /// The specified position is shown below.<br>
    /// <br>
    /// Type1Cell
    /// <table border=2 style="text-align:center;border-collapse:collapse;">
    ///        <tr>
    ///            <th height = 100 width=200 rowspan="2">Icon</th>
    ///            <th width = 150> Text </th>
    ///            <th width = 150>TextEnd</th>
    ///            <th width = 200 rowspan="2">CheckBox</th>
    ///        </tr>
    ///        <tr>
    ///            <th colspan = "2" > Sub </th>
    ///        </tr>
    /// </table>
    /// <br>
    /// Type2Cell
    /// <table border=2 style="text-align:center;border-collapse:collapse;">
    ///        <tr>
    ///            <th height = 100 width=200 rowspan="2">Icon</th>
    ///            <th colspan = "2" > Sub </th>
    ///            <th width=200 rowspan="2">CheckBox</th>
    ///        </tr>
    ///        <tr>
    ///            <th width = 150> Text </th>
    ///            <th width=150>TextEnd</th>
    ///        </tr>
    /// </table>
    /// </remarks>
    public abstract class BaseTypeCell : Cell
    {
        /// <summary>
        /// BindableProperty. Identifies the Text bindable property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(BaseTypeCell), default(string));

        /// <summary>
        /// BindableProperty. Identifies the TextEnd bindable property.
        /// </summary>
        public static readonly BindableProperty TextEndProperty = BindableProperty.Create("TextEnd", typeof(string), typeof(BaseTypeCell), default(string));

        /// <summary>
        /// BindableProperty. Identifies the Sub bindable property.
        /// </summary>
        public static readonly BindableProperty SubProperty = BindableProperty.Create("Sub", typeof(string), typeof(TextCell), default(string));

        /// <summary>
        /// BindableProperty. Identifies the Icon bindable property.
        /// </summary>
        public static readonly BindableProperty IconProperty = BindableProperty.Create("Icon", typeof(ImageSource), typeof(BaseTypeCell), null,
            propertyChanged: (bindable, oldvalue, newvalue) => ((BaseTypeCell)bindable).OnSourcePropertyChanged((ImageSource)oldvalue, (ImageSource)newvalue));

        /// <summary>
        /// BindableProperty. Identifies the IsChecked bindable property.
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create("IsChecked", typeof(bool), typeof(BaseTypeCell), false,
            propertyChanged: (obj, oldValue, newValue) =>
            {
                var baseTypeCell = (BaseTypeCell)obj;
                baseTypeCell.Toggled?.Invoke(obj, new ToggledEventArgs((bool)newValue));
            }, defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// BindableProperty. Identifies the IsCheckVisible bindable property.
        /// </summary>
        public static readonly BindableProperty IsCheckVisibleProperty = BindableProperty.Create("IsCheckVisible", typeof(bool), typeof(BaseTypeCell), false);

        /// <summary>
        /// BindableProperty. Identifies the IconWidth bindable property.
        /// </summary>
        public static readonly BindableProperty IconWidthProperty = BindableProperty.Create("IconWidth", typeof(int), typeof(BaseTypeCell), 0);

        /// <summary>
        /// BindableProperty. Identifies the IconHeight bindable property.
        /// </summary>
        public static readonly BindableProperty IconHeightProperty = BindableProperty.Create("IconHeight", typeof(int), typeof(BaseTypeCell), 0);

        /// <summary>
        /// The BaseTypeCell's constructor.
        /// </summary>
        public BaseTypeCell()
        {
            Disappearing += (sender, e) =>
            {
                Icon?.Cancel();
            };
        }

        /// <summary>
        /// Gets or sets the Text displayed as the content of the item.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the TextEnd displayed as the content of the item.
        /// </summary>
        public string TextEnd
        {
            get { return (string)GetValue(TextEndProperty); }
            set { SetValue(TextEndProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Sub displayed as the content of the item.
        /// </summary>
        public string Sub
        {
            get { return (string)GetValue(SubProperty); }
            set { SetValue(SubProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Image on the left side of the item.
        /// </summary>
        [TypeConverter(typeof(ImageSourceConverter))]
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// True or False is used to indicate whether the checkbox is displayed on the right side of the item.
        /// </summary>
        public bool IsCheckVisible
        {
            get { return (bool)GetValue(IsCheckVisibleProperty); }
            set { SetValue(IsCheckVisibleProperty, value); }
        }

        /// <summary>
        /// True or False is used to indicate whether the checkbox has been toggled.
        /// </summary>
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Icon's width.
        /// </summary>
        public int IconWidth
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Icon's height.
        /// </summary>
        public int IconHeight
        {
            get { return (int)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        /// <summary>
        /// The event is raised when the checkbox is toggled.
        /// </summary>
        public event EventHandler<ToggledEventArgs> Toggled;

        void OnSourcePropertyChanged(ImageSource oldvalue, ImageSource newvalue)
        {
            if (newvalue != null)
            {
                SetInheritedBindingContext(newvalue, BindingContext);
            }
        }
    }
}
