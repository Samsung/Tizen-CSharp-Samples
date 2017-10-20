/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;

namespace Clock.Common
{
    /// <summary>
    /// The EmptyPage class
    /// It is used for a page which is not shown when a clock application has been launched at first.
    /// It's for reducing the launching time.
    /// </summary>
    public class EmptyPage : StackLayout
    {
        ///<summary>
        ///Initializes a new instance of the <see cref="EmptyPage"/> class
        ///</summary>
        public EmptyPage()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            HorizontalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
