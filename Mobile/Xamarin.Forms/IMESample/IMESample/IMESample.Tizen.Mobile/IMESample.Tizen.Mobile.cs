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
using Tizen;
using Tizen.Uix.InputMethod;
using Xamarin.Forms;

namespace IMESample.Tizen.Mobile
{
    /// <summary>
    /// Program class contains Main method.
    /// </summary>
    class Program : global::Xamarin.Forms.Platform.Tizen.IMEApplication
    {
        /// <summary>
        /// global app variable
        /// </summary>
        static Program app;

        /// <summary>
        /// A IMESample App instance.</summary>
        private App application;

        /// <summary>
        /// Handle when your app created
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            application = new App(IsLandscape());
            LoadApplication(application);

            // Registration for device orientation changing detection.
            MainWindow.RotationChanged += (s, e) =>
            {
                if (IsLandscape())
                {
                    application.OnOrientationChanged(AppOrientation.Landscape);
                }
                else
                {
                    application.OnOrientationChanged(AppOrientation.Portrait);
                }
            };
        }

        /// <summary>
        /// Checking whether current device orientation is landscape. </summary>
        /// <returns>
        /// true : Landscape orientation, false : Portrait orientation. </returns>
        private bool IsLandscape()
        {
            int value = MainWindow.Rotation;
            if (value == 90 || value == 270)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// When ime created, this function will be called
        /// </summary>
        private static void Create()
        {
            Log.Info("IMESample", "Create callback");
        }

        /// <summary>
        /// When app terminate, this function will be called
        /// </summary>
        private static void Terminate()
        {
            Log.Info("IMESample", "Terminated callback");
            // Unregister the necessary callback functions
            UnregisterCallback();
            app.Exit();
        }

        /// <summary>
        /// When ime show, the show callback function will be called
        /// </summary>
        /// <param name="a"> IME context ID. </param>
        /// <param name="b"> the context for ime. </param>
        private static void Show(InputMethodEditor.ContextId a, InputMethodContext b)
        {
            Log.Info("IMESample", "Show callback");
        }

        /// <summary>
        /// When ime hide, this callback function will be called
        /// </summary>
        /// <param name="a"> IME context ID. </param>
        private static void Hide(InputMethodEditor.ContextId a)
        {
            Log.Info("IMESample", "Hide callback");
        }

        /// <summary>
        /// Handles "FocusedOut" event.
        /// When edit field has focus in, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_FocusedOut(object sender, FocusedOutEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_FocusedOut callback");
        }

        /// <summary>
        /// Handles "FocusedIn" event.
        /// When edit field has focus out, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_FocusedIn(object sender, FocusedInEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_FocusedIn callback");
        }

        /// <summary>
        /// Handles "DisplayLanguageChanged" event.
        /// When the display language of device is changed, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_DisplayLanguageChanged(object sender, DisplayLanguageChangedEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_DisplayLanguageChanged callback");
        }

        /// <summary>
        /// Handles "ReturnKeyStateSet" event.
        /// When the Return key state for IME is set, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_ReturnKeyStateSet(object sender, ReturnKeyStateSetEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_ReturnKeyStateSet callback");
        }

        /// <summary>
        /// Handles "ReturnKeySet" event.
        /// When the Return key for IME is set, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_ReturnKeySet(object sender, ReturnKeySetEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_ReturnKeySet callback");
        }

        /// <summary>
        /// Handles "LayoutSet" event.
        /// When the layout for IME is set, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_LayoutSet(object sender, LayoutSetEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_LayoutSet callback");
        }

        /// <summary>
        /// Handles "DataSet" event.
        /// When the data for IME is set, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_DataSet(object sender, SetDataEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_DataSet callback");
        }

        /// <summary>
        /// Handles "LanguageSet" event.
        /// When the language for IME is set, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_LanguageSet(object sender, LanguageSetEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_LanguageSet callback");
        }

        /// <summary>
        /// Handles "CursorPositionUpdated" event.
        /// When the cursor position of edit field is updated, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_CursorPositionUpdated(object sender, CursorPositionUpdatedEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_CursorPositionUpdated callback");
        }

        /// <summary>
        /// Handles "InputContextReset" event.
        /// When InputContext is reset, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_InputContextReset(object sender, EventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_InputContextReset callback");
        }

        /// <summary>
        /// Handles "SurroundingTextUpdated" event.
        /// When the surroundingText for edit field is updated, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_SurroundingTextUpdated(object sender, SurroundingTextUpdatedEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_SurroundingTextUpdated callback");
        }

        /// <summary>
        /// Handles "AccessibilityStateChanged" event.
        /// When the accessibility status is changed, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_AccessibilityStateChanged(object sender, AccessibilityStateChangedEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_AccessibilityStateChanged callback");
        }

        /// <summary>
        /// Handles "RotationChanged" event.
        /// When the rotation for device is changed, this callback function will be called
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private static void InputMethodEditor_RotationChanged(object sender, RotationChangedEventArgs e)
        {
            Log.Info("IMESample", "InputMethodEditor_RotationChanged callback");
        }

        /// <summary>
        /// Register callback functions
        /// </summary>
        private static void RegisterCallback()
        {
            // Set the necessary callback functions
            InputMethodEditor.FocusedIn += InputMethodEditor_FocusedIn;
            InputMethodEditor.FocusedOut += InputMethodEditor_FocusedOut;
            InputMethodEditor.RotationChanged += InputMethodEditor_RotationChanged;
            InputMethodEditor.AccessibilityStateChanged += InputMethodEditor_AccessibilityStateChanged;
            InputMethodEditor.SurroundingTextUpdated += InputMethodEditor_SurroundingTextUpdated;
            InputMethodEditor.InputContextReset += InputMethodEditor_InputContextReset;
            InputMethodEditor.CursorPositionUpdated += InputMethodEditor_CursorPositionUpdated;
            InputMethodEditor.LanguageSet += InputMethodEditor_LanguageSet;
            InputMethodEditor.DataSet += InputMethodEditor_DataSet;
            InputMethodEditor.LayoutSet += InputMethodEditor_LayoutSet;
            InputMethodEditor.ReturnKeySet += InputMethodEditor_ReturnKeySet;
            InputMethodEditor.ReturnKeyStateSet += InputMethodEditor_ReturnKeyStateSet;
            InputMethodEditor.DisplayLanguageChanged += InputMethodEditor_DisplayLanguageChanged;
        }

        /// <summary>
        /// Unregister callback functions
        /// </summary>
        private static void UnregisterCallback()
        {
            InputMethodEditor.FocusedIn -= InputMethodEditor_FocusedIn;
            InputMethodEditor.FocusedOut -= InputMethodEditor_FocusedOut;
            InputMethodEditor.RotationChanged -= InputMethodEditor_RotationChanged;
            InputMethodEditor.AccessibilityStateChanged -= InputMethodEditor_AccessibilityStateChanged;
            InputMethodEditor.SurroundingTextUpdated -= InputMethodEditor_SurroundingTextUpdated;
            InputMethodEditor.InputContextReset -= InputMethodEditor_InputContextReset;
            InputMethodEditor.CursorPositionUpdated -= InputMethodEditor_CursorPositionUpdated;
            InputMethodEditor.LanguageSet -= InputMethodEditor_LanguageSet;
            InputMethodEditor.DataSet -= InputMethodEditor_DataSet;
            InputMethodEditor.LayoutSet -= InputMethodEditor_LayoutSet;
            InputMethodEditor.ReturnKeySet -= InputMethodEditor_ReturnKeySet;
            InputMethodEditor.ReturnKeyStateSet -= InputMethodEditor_ReturnKeyStateSet;
            InputMethodEditor.DisplayLanguageChanged -= InputMethodEditor_DisplayLanguageChanged;
        }

        /// <summary>
        /// The main entrance of the application.
        /// </summary>
        /// <param name="args"> The string arguments. </param>
        static void Main(string[] args)
        {
            try
            {
                // Set the necessary callback functions
                RegisterCallback();
                // start inputmethod
                InputMethodEditor.Run(Create, Terminate, Show, Hide);
                app = new Program();
                Forms.Init(app);
                app.Run(args);
            }
            catch (Exception e)
            {
                Log.Info("IMESample", "Inputmethod Run error : Caught Exception " + e.ToString());
            }
        }
    }
}
