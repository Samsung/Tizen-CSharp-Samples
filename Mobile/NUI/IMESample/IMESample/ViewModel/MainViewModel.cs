/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.ComponentModel;
using System.Windows.Input;
using Tizen;
using Tizen.NUI.Binding;
using Tizen.NUI.Components;
using Tizen.Uix.InputMethod;

namespace IMESample
{
    /// <summary>
    /// Base ViewModel class
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The shift key state
        /// </summary>
        public enum IMEShiftStates
        {
            ShiftOff,
            ShiftOn,
            ShiftLock,
        };

        /// <summary>
        /// The layout type
        /// </summary>
        public enum IMEKeyboardLayoutType
        {
            LayoutEnglish,
            LayoutSym1,
            LayoutSym2,
        };

        /// <summary>
        /// key length for keyboard
        /// </summary>
        const int KeyLen = 26;

        /// <summary>
        /// Define lower case for alphabet
        /// </summary>
        private static string[] LowerCase =
        {
            "q", "w", "e", "r", "t", "y", "u", "i", "o", "p",
            "a", "s", "d", "f", "g", "h", "j", "k", "l",
            "z", "x", "c", "v", "b", "n", "m"
        };

        /// <summary>
        /// Define upper case for alphabet
        /// </summary>
        private static string[] UpperCase =
        {
            "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P",
            "A", "S", "D", "F", "G", "H", "J", "K", "L",
            "Z", "X", "C", "V", "B", "N", "M"
        };

        /// <summary>
        /// Define symbol character for first symbol page
        /// </summary>
        private static string[] Symbol1 =
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
            "-", "@", "*", "^", ":", ";", "(", ")", "~",
            "/", "'", "\"", ".", ",", "?", "!"
        };

        /// <summary>
        /// Define symbol character for second symbol page
        /// </summary>
        private static string[] Symbol2 =
        {
            "#", "&", "%", "+", "=", "_", "\\", "|", "<", ">",
            "{", "}", "[", "]", "$", "£", "¥", "€", "₩",
            "¢", "`", "°", "·", "®", "©", "¿"
        };

        /// <summary>
        /// A instance of MainPageViewModel which is wrapped as lazy<T> type
        /// </summary>
        private static readonly Lazy<MainViewModel> lazy =
                new Lazy<MainViewModel>(() => new MainViewModel());

        /// <summary>
        /// A property to provide MainPageViewModel instance
        /// </summary>
        public static MainViewModel Instance => lazy.Value;

        /// <summary>
        /// Restore the status of shift key
        /// </summary>
        private IMEShiftStates ShiftStatus;

        /// <summary>
        /// Restore the layout type of keypad
        /// </summary>
        private IMEKeyboardLayoutType LayoutType;

        /// <summary>
        /// The main label
        /// </summary>
        public string[] MainLabel { set; get; }

        /// <summary>
        /// The shift image file of the current state
        /// </summary>
        public string ShiftImage { set; get; }

        /// <summary>
        /// The backspace image file
        /// </summary>
        public string BackSpaceImage { set; get; }

        /// <summary>
        /// The text of symbol key
        /// </summary>
        public string SymText { set; get; }

        /// <summary>
        /// The space image file
        /// </summary>
        public string SpaceImage { set; get; }

        /// <summary>
        /// The return image file
        /// </summary>
        public string ReturnImage { set; get; }

        /// <summary>
        /// The layout string
        /// </summary>
        public string LayoutString { set; get; }

        /// <summary>
        /// The IME resource path
        /// </summary>
        static string ImageURL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "image/";

        /// <summary>
        /// Property changing event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Command which handles key event
        /// </summary>
        public ICommand PressButton { protected set; get; }

        /// <summary>
        /// Command which handles 'shift' key event
        /// </summary>
        public ICommand ShiftButton { protected set; get; }

        /// <summary>
        /// Command which handles 'backspace' key event
        /// </summary>
        public ICommand BackSpaceButton { protected set; get; }

        /// <summary>
        /// Command which allows to change the keyboard layout
        /// </summary>
        public ICommand LayoutButton { protected set; get; }

        /// <summary>
        /// Command which handles 'return' key event
        /// </summary>
        public ICommand ReturnButton { protected set; get; }

        /// <summary>
        /// When property for xaml is changed, update display
        /// </summary>
        public void UpdateDisplay()
        {
            OnPropertyChanged("MainLabel");
            OnPropertyChanged("ShiftImage");
            OnPropertyChanged("LayoutString");
            OnPropertyChanged("SymText");
        }

        /// <summary>
        /// Default class constructor
        /// </summary>
        public MainViewModel()
        {
            MainLabel = new string[KeyLen];
            Array.Copy(LowerCase, MainLabel, MainLabel.Length);
            ShiftStatus = IMEShiftStates.ShiftOff;
            ShiftImage = ImageURL + "shift_off.png";
            BackSpaceImage = ImageURL + "back_space_normal.png";
            SpaceImage = ImageURL + "space_normal.png";
            ReturnImage = ImageURL + "enter_normal.png";
            LayoutString = "?123";
            SymText = "";

            /// <summary>
            /// Process text button event
            /// </summary>
            this.PressButton = new Command((value) =>
            {
                string text = value.ToString();
                Log.Info("IMESample", "PressButton: Text : " + text);
                InputMethodEditor.CommitString(text);
                if (LayoutType == IMEKeyboardLayoutType.LayoutEnglish && ShiftStatus == IMEShiftStates.ShiftOn)
                {
                    Array.Copy(LowerCase, MainLabel, MainLabel.Length);
                    ShiftStatus = IMEShiftStates.ShiftOff;
                    ShiftImage = ImageURL + "shift_off.png";
                    UpdateDisplay();
                }
            });

            /// <summary>
            /// Process shift button event
            /// </summary>
            this.ShiftButton = new Command(() =>
            {
                Log.Info("IMESample", "ShiftButton Clicked.");
                if (LayoutType == IMEKeyboardLayoutType.LayoutEnglish)
                {
                    SymText = "";
                    switch (ShiftStatus)
                    {
                        case IMEShiftStates.ShiftOff:
                            Array.Copy(UpperCase, MainLabel, MainLabel.Length);
                            ShiftStatus = IMEShiftStates.ShiftOn;
                            ShiftImage = ImageURL + "shift_on.png";
                            break;
                        case IMEShiftStates.ShiftOn:
                            Array.Copy(UpperCase, MainLabel, MainLabel.Length);
                            ShiftStatus = IMEShiftStates.ShiftLock;
                            ShiftImage = ImageURL + "shift_loc.png";
                            break;
                        case IMEShiftStates.ShiftLock:
                            Array.Copy(LowerCase, MainLabel, MainLabel.Length);
                            ShiftStatus = IMEShiftStates.ShiftOff;
                            ShiftImage = ImageURL + "shift_off.png";
                            break;
                    }
                }
                else if (LayoutType == IMEKeyboardLayoutType.LayoutSym1)
                {
                    LayoutType = IMEKeyboardLayoutType.LayoutSym2;
                    Array.Copy(Symbol2, MainLabel, MainLabel.Length);
                    SymText = "2/2";
                }
                else
                {
                    LayoutType = IMEKeyboardLayoutType.LayoutSym1;
                    Array.Copy(Symbol1, MainLabel, MainLabel.Length);
                    SymText = "1/2";
                }

                UpdateDisplay();
            });

            /// <summary>
            /// Process backspace button event
            /// </summary>
            this.BackSpaceButton = new Command(() =>
            {
                Log.Info("IMESample", "BackSpaceButton Clicked.");
                InputMethodEditor.SendKeyEvent(KeyCode.BackSpace, KeyMask.Pressed);
                InputMethodEditor.SendKeyEvent(KeyCode.BackSpace, KeyMask.Released);
            });

            /// <summary>
            /// Process layout button event
            /// </summary>
            this.LayoutButton = new Command(() =>
            {
                Log.Info("IMESample", "LayoutButton Clicked.");
                if (LayoutType == IMEKeyboardLayoutType.LayoutEnglish)
                {
                    LayoutType = IMEKeyboardLayoutType.LayoutSym1;
                    Array.Copy(Symbol1, MainLabel, MainLabel.Length);
                    LayoutString = "abc";
                    SymText = "1/2";
                    ShiftImage = "";
                }
                else
                {
                    LayoutType = IMEKeyboardLayoutType.LayoutEnglish;
                    Array.Copy(LowerCase, MainLabel, MainLabel.Length);
                    LayoutString = "?123";
                    SymText = "";
                    ShiftImage = ImageURL + "shift_off.png";
                }

                UpdateDisplay();
            });

            /// <summary>
            /// Process return button event
            /// </summary>
            this.ReturnButton = new Command(() =>
            {
                Log.Info("IMESample", "ReturnButton Clicked.");
                InputMethodEditor.SendKeyEvent(KeyCode.Return, KeyMask.Pressed);
                InputMethodEditor.SendKeyEvent(KeyCode.Return, KeyMask.Released);
            });
        }

        /// <summary>
        /// A method notifying a property changing situation
        /// </summary>
        /// <param name="propertyName"> A property name. </param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
