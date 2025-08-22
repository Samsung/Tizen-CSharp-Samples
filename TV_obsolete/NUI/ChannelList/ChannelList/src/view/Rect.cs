/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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

using System;

/// <summary>
/// Namespace for Tizen.NUI package
/// </summary>
namespace ChannelList
{
    /// <summary>
    /// The float constant.
    /// To compare float with 0.
    /// We can't compare float with 0 directly like interger.
    /// </summary>
    static internal class Constants1
    {
        public const float EPSILON = 0.000001f;
    }

    /// <summary>
    /// The Rect class.
    /// </summary>
    /// <code>
    /// Rect rect = new Rect(100, 200, 100, 200);
    /// </code>
    internal class Rect
    {
        /// <summary>
        /// The Constructor.
        /// </summary>
        public Rect()
        {
            x = 0;
            y = 0;
            width = 0;
            height = 0;
        }

        /// <summary>
        /// The Constructor with parameter.
        /// </summary>
        /// <param name="x">x value</param>
        /// <param name="y">y value</param>
        /// <param name="width">width value</param>
        /// <param name="height">height value</param>
        public Rect(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <summary>
        /// The copy constructor.
        /// </summary>
        /// <param name="rhs">the Rect object instance</param>
        public Rect(Rect rhs)
        {
            x = rhs.x;
            y = rhs.y;
            width = rhs.width;
            height = rhs.height;
        }

        /// <summary>
        /// The X coordinate.
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
        /// The Y coordinate.
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
        /// The width.
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
        /// The height.
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
        /// The set function.
        /// </summary>
        /// <param name="newX">new X coordinate</param>
        /// <param name="newY">new Y coordinate</param>
        /// <param name="newWidth">new width</param>
        /// <param name="newHeight">new height</param>
        public void Set(float newX, float newY, float newWidth, float newHeight)
        {
            x = newX;
            y = newY;
            width = newWidth;
            height = newHeight;
        }

        /// <summary>
        /// Check empty.
        /// </summary>
        /// <returns>True if this rect has a non-zero value.</returns>
        public bool IsEmpty()
        {
            return Math.Abs(x) < Constants1.EPSILON && Math.Abs(y) < Constants1.EPSILON && Math.Abs(width) < Constants1.EPSILON && Math.Abs(height) < Constants1.EPSILON;
        }

        /// <summary>
        /// Get left X coordinate of rect.
        /// </summary>
        /// <returns>The left x coordinate of rect.</returns>
        public float Left()
        {
            return x;
        }

        /// <summary>
        /// Get right X coordinate of rect.
        /// </summary>
        /// <returns>The right x coordinate of rect.</returns>
        public float Right()
        {
            return x + width;
        }

        /// <summary>
        /// Get top Y coordinate of rect.
        /// </summary>
        /// <returns>The top Y coordinate of rect.</returns>
        public float Top()
        {
            return y;
        }

        /// <summary>
        /// Get bottom Y coordinate of rect.
        /// </summary>
        /// <returns>The bottom Y coordinate of rect.</returns>
        public float Bottom()
        {
            return y + height;
        }

        /// <summary>
        /// Check if a rect intersects with another rect.
        /// </summary>
        /// <param name="other">the Rect object instance.</param>
        /// <returns>True if two rect have an intersection.</returns>
        public bool Intersects(Rect other)
        {
            // TODO
            return false;
        }

        /// <summary>
        /// Check if a rect contains another rect.
        /// </summary>
        /// <param name="other">the Rect object instance</param>
        /// <returns>True if a rect contains another rect.</returns>
        public bool Contains(Rect other)
        {
            // TODO
            return false;
        }

        /// <summary>
        /// Check if two rects are equal.
        /// </summary>
        /// <param name="a">the first rect object used to compare</param>
        /// <param name="b">the second rect object used to compare</param>
        /// <returns>True if two rects are equal.</returns>
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
            return Math.Abs(a.X - b.X) < Constants1.EPSILON && Math.Abs(a.Y - b.Y) < Constants1.EPSILON && Math.Abs(a.Width - b.Width) < Constants1.EPSILON && Math.Abs(a.Height - b.Height) < Constants1.EPSILON;
        }

        /// <summary>
        /// Check if two rects are not equal.
        /// </summary>
        /// <param name="a">the first rect object used to compare</param>
        /// <param name="b">the second rect object used to compare</param>
        /// <returns>True if two rects are not equal.</returns>
        public static bool operator !=(Rect a, Rect b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Check if a rect is equal with another rect
        /// </summary>
        /// <param name="obj">the object which used to compare</param>
        /// <returns>True if two rects are equal.</returns>
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
        /// Get hash code.
        /// </summary>
        /// <returns>Hash code of rect.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private float x; // x coordinate
        private float y; // y coordinate
        private float width; // width
        private float height; // height
    }
}
