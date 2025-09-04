/*
 * Copyright(c) 2025 Samsung Electronics Co., Ltd.
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

// Import the core Tizen.NUI namespace which provides the main application framework and base UI components
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace TizenNUITemplate
{
    /// <summary>
    /// Program class that inherits from NUIApplication - the main application class for Tizen NUI apps
    /// This class serves as the entry point for NUI application and manages the app lifecycle
    /// </summary>
    class Program : NUIApplication
    {
        /// <summary>
        /// This method is called when the application is created and initialized
        /// It's part of the NUI application lifecycle and is where the initial UI is set up
        /// </summary>
        protected override void OnCreate()
        {
            // Always call the base class implementation first to ensure proper initialization
            base.OnCreate();
            // Call the custom initialization method to set up the UI components
            Initialize();
        }

        /// <summary>
        /// This method initializes all the UI components for this application
        /// It creates the text label, page structure, and animation for this sample app
        /// </summary>
        void Initialize()
        {
            // Register a key event handler to listen for hardware key presses (like back button)
            // GetDefaultWindow() returns the main window of the application
            GetDefaultWindow().KeyEvent += OnKeyEvent;

            // Create a TextLabel - a UI component used to display text
            // TextLabel is a lightweight, non-editable text display component
            TextLabel label = new TextLabel()
            {
                // Set the text content to display
                Text = "Hello Tizen NUI World",
                // Set the text color to blue
                TextColor = Color.Blue,
                // Set the width to match the parent container (fill available horizontal space)
                WidthSpecification = LayoutParamPolicies.MatchParent,
                // Set the height to match the parent container (fill available vertical space)
                HeightSpecification = LayoutParamPolicies.MatchParent,
                // Align the text horizontally in the center of the label
                HorizontalAlignment = HorizontalAlignment.Center,
                // Align the text vertically in the center of the label
                VerticalAlignment = VerticalAlignment.Center,
                // Set the font size to 50 pixels
                PixelSize = 50,
            };

            // Create a ContentPage - a full-screen page container with an app bar
            // ContentPage is a pre-built page layout that includes a title bar (AppBar) and content area
            ContentPage page = new ContentPage
            {
                // Create and configure the AppBar (the title bar at the top of the page)
                AppBar = new AppBar()
                {
                    // Set the title text that will appear in the app bar
                    Title = "Tizen NUI Sample App",
                },
                // Set the main content of the page to be the text label
                // This will display the label below the app bar
                Content = label,
            };

            // Push the page to the default navigator for display
            // GetDefaultWindow() gets the main application window
            // GetDefaultNavigator() gets the navigation system that manages page stacking
            // Push() adds the page to the navigation stack and displays it to the user
            GetDefaultWindow().GetDefaultNavigator().Push(page);

            // Create an animation object that will animate UI properties over time
            // The parameter 2000 specifies the default duration in milliseconds (2 seconds)
            Animation animation = new Animation(2000);

            // Add an animation to rotate the label around the X-axis
            // AnimateTo() animates a property from its current value to a target value
            // Parameters:
            //   label - the target object to animate
            //   "Orientation" - the property to animate (rotation of the object)
            //   new Rotation(new Radian(new Degree(180.0f), PositionAxis.X) - target value (180-degree rotation around X-axis)
            //   0 - start time in milliseconds (begin immediately)
            //   500 - end time in milliseconds (complete first half rotation in 0.5 seconds)
            animation.AnimateTo(label, "Orientation", new Rotation(new Radian(new Degree(180.0f)), PositionAxis.X), 0, 500);

            // Add a second animation to rotate the label back to its original position
            // This creates a continuous back-and-forth rotation effect
            animation.AnimateTo(label, "Orientation", new Rotation(new Radian(new Degree(0.0f)), PositionAxis.X), 500, 1000);

            // Set the animation to loop continuously
            // When true, the animation will repeat indefinitely
            animation.Looping = true;

            // Start playing the animation
            // This begins the rotation effect on the label
            animation.Play();
        }

        /// <summary>
        /// Event handler for key press events
        /// This method is called when the user presses hardware keys on the device
        /// </summary>
        /// <param name="sender">The object that raised the event (the window)</param>
        /// <param name="e">Event arguments containing key information</param>
        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            // Check if a key was pressed down (not released)
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                // If the back button or escape key was pressed, exit the application
                // This is the standard way to handle the back button in Tizen applications
                Exit();
            }
        }

        /// <summary>
        /// The main entry point of the application
        /// This is where the application starts execution
        /// </summary>
        /// <param name="args">Command line arguments passed to the application</param>
        static void Main(string[] args)
        {
            // Disable XAML usage for this application
            // XAML is a markup language for defining UI, but we're creating UI in csharp code
            IsUsingXaml = false;

            // Create an instance of the Program class (which is a NUIApplication)
            var app = new Program();

            // Run the application with the provided arguments
            // This starts the main event loop and displays the UI
            app.Run(args);
        }
    }
}
