using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using SNSUI.Extensions;
using SNSUI.Tizen.Renderers.Cells;

using NImage = Xamarin.Forms.Platform.Tizen.Native.Image;
using TForms = Xamarin.Forms.Forms;

[assembly: ExportCell(typeof(Type1Cell), typeof(Type1CellRenderer))]

namespace SNSUI.Tizen.Renderers.Cells
{
    // type1
    // -------------------------------------------------------------------------------------------
    // |                  | elm.swallow.icon.0 | elm.text | elm.swallow.icon.1 |                 |
    // | elm.swallow.icon |----------------------------------------------------| elm.swallow.end |
    // |                  |         elm.text.sub          | elm.text.sub.end   |                 |
    // -------------------------------------------------------------------------------------------
    // (*) "elm.text.end" can be used instead of "elm.swallow.icon.1".


    public abstract class BaseTypeCellRenderer : CellRenderer
    {
        string _textPart = "elm.text";
        string _textEndPart = "elm.text.end";
        string _textSubPart = "elm.text.sub";
        string _iconPart = "elm.swallow.icon";
        string _endPart = "elm.swallow.end";

        public BaseTypeCellRenderer(string style) : base(style)
        {
        }

        protected override Span OnGetText(Cell cell, string part)
        {
            BaseTypeCell typeCell = (BaseTypeCell)cell;
            if (part == _textPart)
            {
                return new Span()
                {
                    Text = typeCell.Text,
                    FontSize = -1
                };
            }
            else if (part == _textEndPart)
            {
                return new Span()
                {
                    Text = typeCell.TextEnd,
                    FontSize = -1
                };
            }
            else if (part == _textSubPart)
            {
                return new Span()
                {
                    Text = typeCell.Sub,
                    FontSize = -1
                };
            }

            return null;
        }

        protected override EvasObject OnGetContent(Cell cell, string part)
        {
            BaseTypeCell typeCell = (BaseTypeCell)cell;
            if (part == _iconPart && typeCell.Icon != null)
            {
                var image = new NImage(TForms.NativeParent);
                SetUpImage(typeCell, image);
                return image;
            }
            else if (part == _endPart && typeCell.IsCheckVisible)
            {
                var check = new Check(TForms.NativeParent);
                check.SetAlignment(-1, -1);
                check.SetWeight(1, 1);
                check.IsChecked = typeCell.IsChecked;
                check.StateChanged += (sender, e) =>
                {
                    typeCell.IsChecked = e.NewState;
                };

                //It is a temporary way to prevent that the check of the Cell gets focus until the UX about views in the Cell for TV is defined.
                if (Device.Idiom == TargetIdiom.TV)
                {
                    check.AllowFocus(false);
                }

                return check;
            }

            return null;
        }

        protected override bool OnCellPropertyChanged(Cell cell, string property, Dictionary<string, EvasObject> realizedView)
        {
            BaseTypeCell typeCell = (BaseTypeCell)cell;
            if (property == BaseTypeCell.IconProperty.PropertyName)
            {
                if (!realizedView.ContainsKey(_iconPart) || typeCell.Icon == null)
                {
                    return true;
                }

                var image = realizedView[_iconPart] as NImage;
                SetUpImage(typeCell, image);
                return false;
            }
            else if (property == BaseTypeCell.IsCheckedProperty.PropertyName && realizedView.ContainsKey(_endPart))
            {
                var check = realizedView[_endPart] as Check;
                check.IsChecked = typeCell.IsChecked;
                return false;
            }
            else if (property == BaseTypeCell.IsCheckVisibleProperty.PropertyName ||
                property == BaseTypeCell.TextProperty.PropertyName ||
                property == BaseTypeCell.TextEndProperty.PropertyName ||
                property == BaseTypeCell.SubProperty.PropertyName)
            {
                return true;
            }

            return base.OnCellPropertyChanged(cell, property, realizedView);
        }

        void SetUpImage(BaseTypeCell cell, NImage image)
        {
            if (cell.IconHeight > 0 && cell.IconWidth > 0)
            {
                image.MinimumWidth = cell.IconWidth;
                image.MinimumHeight = cell.IconHeight;
            }
            else
            {
                image.LoadingCompleted += (sender, e) =>
                {
                    image.MinimumWidth = image.ObjectSize.Width;
                    image.MinimumHeight = image.ObjectSize.Height;
                };
            }

            image.LoadFromImageSourceAsync(cell.Icon);
        }
    }

    public class Type1CellRenderer : BaseTypeCellRenderer
    {
        public Type1CellRenderer() : base("type1")
        {
        }
    }
}