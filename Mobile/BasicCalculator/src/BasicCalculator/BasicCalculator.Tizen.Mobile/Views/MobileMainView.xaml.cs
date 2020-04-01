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
using Xamarin.Forms;
using ElmSharp;
using BasicCalculator.ViewModels;

namespace BasicCalculator.Tizen.Mobile.Views
{
    /// <summary>
    /// MainPage Xaml C# partial class code.
    /// </summary>
    public partial class MobileMainView
    {
        EcoreEvent<EcoreKeyEventArgs> _ecoreKeyUp;

        /// <summary>
        /// Class constructor.
        /// Initializes component (Xaml partial).
        /// </summary>
        public MobileMainView()
        {
            InitializeComponent();

            _ecoreKeyUp = new EcoreEvent<EcoreKeyEventArgs>(EcoreEventType.KeyUp, EcoreKeyEventArgs.Create);
            _ecoreKeyUp.On += _ecoreKeyUp_On;
        }

        private void _ecoreKeyUp_On(object sender, EcoreKeyEventArgs e)
        {
            // e.KeyName, e.KeyCode
            DependencyService.Get<IKeyboardService>().KeyEvent(sender, e);
            
        }
    }
}
