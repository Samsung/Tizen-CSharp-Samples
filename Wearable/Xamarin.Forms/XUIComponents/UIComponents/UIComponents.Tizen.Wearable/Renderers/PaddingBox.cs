/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using ElmSharp;

namespace UIComponents.Tizen.Wearable.Renderers
{
    class PaddingBox : EvasObject
    {
        bool _layouting = false;

        Thickness _padding;
        Size _headerSize;
        Size _footerSize;
        int _headerGap;
        int _footerGap;

        ElmSharp.Box _box;
        EvasObject _content;
        EvasObject _header;
        EvasObject _footer;

        /// <summary>
        /// Constructor of PaddingBox class
        /// </summary>
        /// <param name="parent">EvasObject</param>
        public PaddingBox(EvasObject parent) : base(parent)
        {
            _box.SetLayoutCallback(OnLayout);
        }

        /// <summary>
        /// Create EvasObject handler
        /// </summary>
        /// <param name="parent">EvasObject</param>
        /// <returns>Returns box.Handle pointer</returns>
        protected override IntPtr CreateHandle(EvasObject parent)
        {
            _box = new Box(parent);
            return _box.Handle;
        }

        /// <summary>
        /// Setter and Getter for background color
        /// </summary>
        public Color BackgroundColor { get => _box.BackgroundColor; set => _box.BackgroundColor = value; }

        /// <summary>
        /// Setter and Getter for Header
        /// </summary>
        public EvasObject Header
        {
            get => _header;
            set
            {
                if (_header == value)
                {
                    return;
                }

                _header = value;
                if (_header == null)
                {
                    _box.UnPack(_header);
                }
                else
                {
                    _box.PackStart(_header);
                    _header.Show();
                }
            }
        }

        /// <summary>
        /// Setter and Getter for Content
        /// </summary>
        public EvasObject Content
        {
            get => _content;
            set
            {
                if (_content == value)
                {
                    return;
                }

                _content = value;
                if (_content == null)
                {
                    _box.UnPack(_content);
                }
                else
                {
                    _box.PackEnd(_content);
                    _content.Show();
                }

                if (_footer != null)
                {
                    _footer.RaiseTop();
                }

                if (_header != null)
                {
                    _header.RaiseTop();
                }
            }
        }

        /// <summary>
        /// Setter and Getter for Footer
        /// </summary>
        public EvasObject Footer
        {
            get => _footer;
            set
            {
                if (_footer == value)
                {
                    return;
                }

                _footer = value;
                if (_footer == null)
                {
                    _box.UnPack(_footer);
                }
                else
                {
                    _box.PackEnd(_footer);
                    _footer.Show();
                }
            }
        }

        /// <summary>
        /// Setter and Getter for Padding
        /// </summary>
        public Thickness Padding
        {
            get => _padding;
            set
            {
                if (_padding != value)
                {
                    _padding = value;
                    _box.Recalculate();
                }
            }
        }

        /// <summary>
        /// Setter and Getter for Header size
        /// </summary>
        public Size HeaderSize
        {
            get => _headerSize;
            set
            {
                if (_headerSize != value)
                {
                    _headerSize = value;
                    _box.Recalculate();
                }
            }
        }

        /// <summary>
        /// Setter and Getter for Header gap
        /// </summary>
        public int HeaderGap
        {
            get => _headerGap;
            set
            {
                if (_headerGap != value)
                {
                    _headerGap = value;
                    _box.Recalculate();
                }
            }
        }

        /// <summary>
        /// Setter and Getter for Footer size
        /// </summary>
        public Size FooterSize
        {
            get => _footerSize;
            set
            {
                if (_footerSize != value)
                {
                    _footerSize = value;
                    _box.Recalculate();
                }
            }
        }

        /// <summary>
        /// Setter and Getter for Footer gap
        /// </summary>
        public int FooterGap
        {
            get => _footerGap;
            set
            {
                if (_footerGap != value)
                {
                    _footerGap = value;
                    _box.Recalculate();
                }
            }
        }

        public void OnLayout()
        {
            if (_layouting)
            {
                return;
            }

            _layouting = true;

            int w = 0, h = 0, x = 0, y = 0;
            double ax = 0.5, ay = 0.5;
            var myg = Geometry;
            int hw = 0, hh = 0, fw = 0, fh = 0;

            if (_content == null)
            {
                MinimumHeight = 0;
                MinimumWidth = 0;
                _layouting = false;
                return;
            }

            w = myg.Width - _padding.Left - _padding.Right;
            h = myg.Height - _padding.Top - _padding.Bottom;

            if (_header != null)
            {
                var top = _header.Geometry;
                ax = _header.AlignmentX;
                ay = _header.AlignmentY;

                hw = _headerSize.Width >= 0 ? _headerSize.Width : w;
                hh = _headerSize.Height >= 0 ? _headerSize.Height : 0;

                if (ax >= 0)
                {
                    x = (int)((w - hw) * ax);
                }

                _header.Geometry = new Rect(myg.X + x + _padding.Left, myg.Y + y + _padding.Top, hw, hh);

                y += hh + _headerGap;
                x = 0;
                h -= hh + _headerGap;
                if (h < hh)
                {
                    h = hh;
                }
            }

            if (_footer != null)
            {
                x = 0;

                var footer = _footer.Geometry;
                ax = _footer.AlignmentX;
                ay = _footer.AlignmentY;

                fw = _footerSize.Width >= 0 ? _footerSize.Width : w;
                fh = _footerSize.Height >= 0 ? _footerSize.Height : h;

                var fy = h - _footerSize.Height - _footerGap;

                if (ax >= 0)
                {
                    x = (int)((w - fw) * ax);
                }

                _footer.Geometry = new Rect(myg.X + x + _padding.Left, myg.Y + fy + _padding.Top, fw, fh);

                x = 0;
            }

            var content = _content.Geometry;
            ax = _content.AlignmentX;
            ay = _content.AlignmentY;

            if (w < content.Width && ax >= 0)
            {
                x = (int)((w - content.Width) * ax);
                w = content.Width;
            }

            if (h < content.Height && ay >= 0)
            {
                y += (int)((h - content.Height) * ay);
                h = content.Height;
            }

            _content.Geometry = new Rect(myg.X + x + _padding.Left, myg.Y + y + _padding.Top, w, h);

            var ah = hh + _headerGap + h + fh + _footerGap + _padding.Top + _padding.Bottom;
            if (myg.Height < ah)
            {
                MinimumWidth = myg.Width;
                MinimumHeight = ah;
            }

            _layouting = false;
        }
    }

    struct Thickness : IEquatable<Thickness>
    {
        /// <summary>
        /// Setter and getter for thickness of left area
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Setter and getter for thickness of right area
        /// </summary>
        public int Right { get; set; }

        /// <summary>
        /// Setter and getter for thickness of top area
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Setter and getter for thickness of bottom area
        /// </summary>
        public int Bottom { get; set; }

        public bool Equals(Thickness other) =>
            other.Left == Left && other.Right == Right && other.Top == Top && other.Bottom == Bottom;

        public override bool Equals(object obj) => obj.GetType() == typeof(Thickness) && Equals((Thickness)obj);

        public static bool operator ==(Thickness t1, Thickness t2) => t1.Equals(t2);

        public static bool operator !=(Thickness t1, Thickness t2) => !t1.Equals(t2);
        public override int GetHashCode() => Left.GetHashCode() ^ Top.GetHashCode() ^ Right.GetHashCode() ^ Bottom.GetHashCode();
    }
}
