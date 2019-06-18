/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
using Tizen;
using Tizen.Uix.InputMethod;
using System.Runtime.CompilerServices;


namespace IMESample.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        /* the shift key state*/
        public enum IMEShiftStates
        {
            ShiftOff,
            ShiftOn,
            ShiftLock,
        };

        /* the layout type */
        public enum IMEKeyboardLayoutType
        {
            LayoutEnglish,
            LayoutSym1,
            LayoutSym2,
        };

        /// <summary>
        /// key lenght for keyboard
        /// </summary>
        const int KeyLen = 26;

        /// <summary>
        /// Define lower case for alphabet
        /// </summary>
        private static String[] LowerCase =
        {
            "q", "w", "e", "r", "t", "y", "u", "i", "o", "p",
            "a", "s", "d", "f", "g", "h", "j", "k", "l",
            "z", "x", "c", "v", "b", "m", "n"
        };

        /// <summary>
        /// Define upper case for alphabet
        /// </summary>
        private static String[] UpperCase =
        {
            "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P",
            "A", "S", "D", "F", "G", "H", "J", "K", "L",
            "Z", "X", "C", "V", "B", "M", "N"
        };

        /// <summary>
        /// Define symbol character for first symbol page
        /// </summary>
        private static String[] Symbol1 =
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
            "-", "@", "*", "^", ":", ";", "(", ")", "~",
            "/", "'", "\"", ".", ",", "?", "!"
        };

        /// <summary>
        /// Define symbol character for second symbol page
        /// </summary>
        private static String[] Symbol2 =
        {
            "#", "&", "%", "+", "=", "_", "\\", "|", "<", ">",
            "{", "}", "[", "]", "$", "£", "¥", "€", "₩",
            "¢", "`", "°", "·", "®", "©", "¿"
        };

        /// <summary>
        /// A instance of MainPageViewModel which is wrapped as lazy<T> type.
        /// </summary>
        private static readonly Lazy<MainPageViewModel> lazy =
                new Lazy<MainPageViewModel>(() => new MainPageViewModel());

        /// <summary>
        /// A property to provide MainPageViewModel instance.
        /// </summary>
        public static MainPageViewModel Instance => lazy.Value;

        /// <summary>
        /// Restore the status of shift key
        /// </summary>
        private IMEShiftStates ShiftStatus;

        /// <summary>
        /// Restore the layout type of keypad
        /// </summary>
        private IMEKeyboardLayoutType LayoutType;

        /// <summary>
        /// Property changing event handler</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public String[] MainLabel { set; get; }

        public String ShiftImage { set; get; }

        public float ShiftImageOpacity { set; get; }

        public String ShiftBgImageColor { set; get; }

        public String LayoutString { set; get; }

        public String SymText { set; get; }

        public ICommand Backspace { protected set; get; }

        public ICommand PressButton { protected set; get; }

        public ICommand ReturnButton { protected set; get; }

        public ICommand ShiftButton { protected set; get; }

        public ICommand LayoutButton { protected set; get; }


        /// <summary>
        /// When property for xaml is changed, update display.
        /// </summary>
        public void UpdateDisplay()
        {
            OnPropertyChanged("MainLabel");
            OnPropertyChanged("LayoutString");
            OnPropertyChanged("ShiftImage");
            OnPropertyChanged("ShiftImageOpacity");
            OnPropertyChanged("SymText");
            OnPropertyChanged("ShiftBgImageColor");
        }

        public MainPageViewModel()
        {
            MainLabel = new string[KeyLen];

            Array.Copy(LowerCase, MainLabel, MainLabel.Length);

            ShiftStatus = IMEShiftStates.ShiftOff;
            LayoutType = IMEKeyboardLayoutType.LayoutEnglish;
            LayoutString = "?123";
            ShiftImage = "sample_ime_shift_off.png";
            SymText = "";
            ShiftImageOpacity = 1;
            ShiftBgImageColor = "Gray";

            this.Backspace = new Command(() =>
            {
                InputMethodEditor.SendKeyEvent(KeyCode.BackSpace, KeyMask.Pressed);
                InputMethodEditor.SendKeyEvent(KeyCode.BackSpace, KeyMask.Released);
            });
            this.PressButton = new Command((value) =>
            {
                string input = value.ToString();
                InputMethodEditor.CommitString(input);
                if (LayoutType == IMEKeyboardLayoutType.LayoutEnglish && ShiftStatus == IMEShiftStates.ShiftOn)
                {
                    Array.Copy(LowerCase, MainLabel, MainLabel.Length);
                    ShiftStatus = IMEShiftStates.ShiftOff;
                    ShiftImage = "sample_ime_shift_off.png";
                    UpdateDisplay();
                }
            });
            this.ReturnButton = new Command(() =>
            {
                InputMethodEditor.SendKeyEvent(KeyCode.Return, KeyMask.Pressed);
                InputMethodEditor.SendKeyEvent(KeyCode.Return, KeyMask.Released);
            });
            this.ShiftButton = new Command(() =>
            {
                if (LayoutType == IMEKeyboardLayoutType.LayoutEnglish)
                {
                    SymText = "";
                    ShiftImageOpacity = 1;
                    if (ShiftStatus == IMEShiftStates.ShiftOff)
                    {
                        Array.Copy(UpperCase, MainLabel, MainLabel.Length);
                        ShiftStatus = IMEShiftStates.ShiftOn;
                        ShiftImage = "sample_ime_shift_on.png";
                        ShiftBgImageColor = "Gray";
                        UpdateDisplay();
                    }
                    else if (ShiftStatus == IMEShiftStates.ShiftOn)
                    {
                        Array.Copy(UpperCase, MainLabel, MainLabel.Length);
                        ShiftStatus = IMEShiftStates.ShiftLock;
                        ShiftImage = "sample_ime_shift_loc.png";
                        ShiftBgImageColor = "DeepSkyBlue";
                        UpdateDisplay();
                    }
                    else if (ShiftStatus == IMEShiftStates.ShiftLock)
                    {
                        Array.Copy(LowerCase, MainLabel, MainLabel.Length);
                        ShiftStatus = IMEShiftStates.ShiftOff;
                        ShiftImage = "sample_ime_shift_off.png";
                        ShiftBgImageColor = "Gray";
                        UpdateDisplay();
                    }
                }
                else if (LayoutType == IMEKeyboardLayoutType.LayoutSym2)
                {
                    LayoutType = IMEKeyboardLayoutType.LayoutSym1;
                    Array.Copy(Symbol1, MainLabel, MainLabel.Length);
                    SymText = "1/2";
                    ShiftImageOpacity = 0;
                    ShiftBgImageColor = "Gray";
                    UpdateDisplay();
                }
                else
                {
                    LayoutType = IMEKeyboardLayoutType.LayoutSym2;
                    Array.Copy(Symbol2, MainLabel, MainLabel.Length);
                    SymText = "2/2";
                    ShiftImageOpacity = 0;
                    ShiftBgImageColor = "Gray";
                    UpdateDisplay();
                }
            });
            this.LayoutButton = new Command(() =>
            {
                if (LayoutType == IMEKeyboardLayoutType.LayoutEnglish)
                {
                    LayoutType = IMEKeyboardLayoutType.LayoutSym1;
                    Array.Copy(Symbol1, MainLabel, MainLabel.Length);
                    LayoutString = "abc";
                    SymText = "1/2";
                    ShiftImageOpacity = 0;
                    ShiftBgImageColor = "Gray";
                    UpdateDisplay();
                }
                else if (LayoutType == IMEKeyboardLayoutType.LayoutSym1 || LayoutType == IMEKeyboardLayoutType.LayoutSym2)
                {
                    LayoutType = IMEKeyboardLayoutType.LayoutEnglish;
                    Array.Copy(LowerCase, MainLabel, MainLabel.Length);
                    LayoutString = "?123";
                    SymText = "";
                    ShiftBgImageColor = "Gray";
                    ShiftImage = "sample_ime_shift_off.png";
                    ShiftImageOpacity = 1;
                    UpdateDisplay();
                }
            });
        }

        /// <summary>
        /// A method notifying a property changing situation. </summary>
        /// <param name="propertyName"> A property name. </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
