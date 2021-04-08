/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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
 */

namespace TextReader.ViewModels
{
    /// <summary>
    /// Text paragraph view model class.
    /// </summary>
    public class ParagraphViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Paragraph text value.
        /// </summary>
        private string _text;

        /// <summary>
        /// Flag indicating if paragraph is active one.
        /// </summary>
        private bool _active;

        #endregion

        #region properties

        /// <summary>
        /// Paragraph text value.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        /// <summary>
        /// Flag indicating if paragraph is active one.
        /// </summary>
        public bool Active
        {
            get { return _active; }
            set { SetProperty(ref _active, value); }
        }

        #endregion

        #region methods

        /// <summary>
        /// ParagraphViewModel class constructor.
        /// </summary>
        /// <param name="text">Paragraph text value.</param>
        /// <param name="active">Flag indicating if paragraph is active one.</param>
        public ParagraphViewModel(string text, bool active)
        {
            _text = text;
            _active = active;
        }

        #endregion
    }
}
