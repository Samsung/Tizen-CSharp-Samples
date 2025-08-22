/** Copyright (c) 2017 Samsung Electronics Co., Ltd.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using System;

namespace Tizen.NUI
{
    static internal class Constant
    {
        public const float EPSILON = 0.000001f;
    }

    /// <summary>
    /// The Rect class
    /// </summary>
    internal class Rect
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Rect()
        {
            x = 0;
            y = 0;
            width = 0;
            height = 0;
        }

        /// <summary>
        /// Constructor, initialize with four float type value.
        /// </summary>
        /// <param name="x">x value</param>
        /// <param name="y">y value</param>
        /// <param name="width">width value</param>
        /// <param name="height">heiht value</param>
        public Rect(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// Constructor, initialize with a Rect instance.
        /// </summary>
        /// <param name="rhs">the Rect instance</param>
        public Rect(Rect rhs)
        {
            x = rhs.x;
            y = rhs.y;
            width = rhs.width;
            height = rhs.height;
        }

        /// <summary>
        /// X value.
        /// </summary>
        public float X
        {
            set
            {
                x = (value);
            }

            get
            {
                return x;
            }
        }

        /// <summary>
        /// Y value.
        /// </summary>
        public float Y
        {
            set
            {
                y = (value);
            }

            get
            {
                return y;
            }
        }

        /// <summary>
        /// Width value.
        /// </summary>
        public float Width
        {
            set
            {
                width = (value);
            }

            get
            {
                return width;
            }
        }

        /// <summary>
        /// Height value.
        /// </summary>
        public float Height
        {
            set
            {
                height = (value);
            }

            get
            {
                return height;
            }
        }

        /// <summary>
        /// Set the x, y, width, and height value of the Rect instance.
        /// </summary>
        /// <param name="newX">the new X value.</param>
        /// <param name="newY">the new Y value.</param>
        /// <param name="newWidth">the new Width value.</param>
        /// <param name="newHeight">the new Height value.</param>
        public void Set(float newX, float newY, float newWidth, float newHeight)
        {
            x = newX;
            y = newY;
            width = newWidth;
            height = newHeight;
        }

        /// <summary>
        /// Check the Rect instance is empty or not.
        /// </summary>
        /// <returns>If the rect instance is empty, return true, else return false</returns>
        public bool IsEmpty()
        {
            return Math.Abs(x) < Constant.EPSILON && Math.Abs(y) < Constant.EPSILON && Math.Abs(width) < Constant.EPSILON && Math.Abs(height) < Constant.EPSILON;
        }

        /// <summary>
        /// Get the left value, it equals to the x value.
        /// </summary>
        /// <returns>return the x value</returns>
        public float Left()
        {
            return x;
        }

        /// <summary>
        /// Get the right value.
        /// </summary>
        /// <returns>return the right value, it equals to the x value add width value</returns>
        public float Right()
        {
            return x + width;
        }

        /// <summary>
        /// Get the top value, it equals to the y value.
        /// </summary>
        /// <returns>return the y value</returns>
        public float Top()
        {
            return y;
        }

        /// <summary>
        /// Get the bottom value.
        /// </summary>
        /// <returns>return the bottom value, it equals to the y value add height value</returns>
        public float Bottom()
        {
            return y + height;
        }

        //public bool Intersects(Rect other)
        //{
        //    // TODO
        //    return false;
        //}

        //public bool Contains(Rect other)
        //{
        //    // TODO
        //    return false;
        //}

        /// <summary>
        /// Overrides the "==" operator.
        /// If both are null, or both are same instance, return true.
        /// If one is null, but not both, return false.
        /// if the fields match, Return true:
        /// </summary>
        /// <param name="a">the left rect object</param>
        /// <param name="b">the right rect object</param>
        /// <returns>return the two Rect object matches or not</returns>
        public static bool operator ==(Rect a, Rect b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return Math.Abs(a.X - b.X) < Constant.EPSILON && Math.Abs(a.Y - b.Y) < Constant.EPSILON && Math.Abs(a.Width - b.Width) < Constant.EPSILON && Math.Abs(a.Height - b.Height) < Constant.EPSILON;
        }

        public static bool operator !=(Rect a, Rect b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            Rect l = this;
            Rect r = obj as Rect;
            if (r == null)
            {
                return false;
            }

            return l == r;
        }

        /// <summary>
        /// Get the hashcode of the Rect instance.
        /// </summary>
        /// <returns>return thr hash code</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private float x;
        private float y;
        private float width;
        private float height;

    }

}