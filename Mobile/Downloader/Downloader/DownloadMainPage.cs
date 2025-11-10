using System;
using System.Collections.Generic; // Added for IEnumerable<>, List<>
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace Downloader
{
    /// <summary>
    /// A class for download main page in NUI.
    /// </summary>
    public class DownloadMainPage : ContentPage
    {
        private TextField urlTextField;
        private Tizen.NUI.Components.Button downloadButton;
        private Tizen.NUI.Components.Button goToInfoPageButton; // Button to navigate to info page
        private Progress progressBar;
        private TextLabel progressLabel;
        private string downloadUrl;
        private IDownload downloadService;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DownloadMainPage()
        {
            // Initialize the download service
            downloadService = new DownloadImplementation();
            InitializeComponent();
        }

        /// <summary>
        /// Initialize main page.
        /// Add components and events.
        /// </summary>
        private void InitializeComponent()
        {
            AppBar = new AppBar
            {
                Title = "Download",
                BackgroundColor = Resources.PrimaryColor
            };

            // Create and add the navigation button to the AppBar using TextLabel instead of Button
            var infoButtonContainer = new View()
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    LinearAlignment = LinearLayout.Alignment.Center,
                    CellPadding = new Size2D(0, 0)
                },
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Padding = new Extents(40, 40, 8, 8),
                BackgroundColor = Color.Transparent
            };
            
            var infoButtonText = new TextLabel
            {
                Text = "Info",
                TextColor = Color.White,
                PointSize = Resources.TextSizeMedium,
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            
            infoButtonContainer.Add(infoButtonText);
            infoButtonContainer.TouchEvent += (s, e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Up)
                {
                    OnGoToInfoPageButtonClicked(s, null);
                }
                return true;
            };
            
            goToInfoPageButton = null; // Not using Button anymore
            AppBar.Actions = new View[] { infoButtonContainer };

            var mainLayout = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = Resources.LayoutSpacingMedium
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = Resources.BackgroundColor,
                Padding = Resources.FormPadding
            };

            // Create URL input section
            var urlSection = CreateUrlSection();
            mainLayout.Add(urlSection);

            // Create download button
            downloadButton = CreateDownloadButton();
            mainLayout.Add(downloadButton);

            // Create progress section
            var progressSection = CreateProgressSection();
            mainLayout.Add(progressSection);

            Content = mainLayout;

            AddEvent();
        }

        /// <summary>
        /// Create URL input section.
        /// </summary>
        /// <returns>View containing URL input</returns>
        private View CreateUrlSection()
        {
            var section = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = Resources.LayoutSpacingSmall
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                BackgroundColor = Resources.SurfaceColor,
                CornerRadius = Resources.CornerRadiusMedium,
                Padding = Resources.CardPadding
            };

            // URL label
            var urlLabel = new TextLabel
            {
                Text = "Download URL",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                PointSize = Resources.TextSizeMedium,
                TextColor = Resources.TextColor
            };
            section.Add(urlLabel);

            // Add spacing
            var spacer1 = new View
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = Resources.SpacingSmall
            };
            section.Add(spacer1);

            // URL text field
            urlTextField = new TextField
            {
                PlaceholderText = "Enter URL to download...",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 56,
                MaxLength = int.MaxValue,
                BackgroundColor = Resources.SurfaceColor,
                TextColor = Resources.TextColor,
                PlaceholderTextColor = Resources.TextColorSecondary,
                PointSize = Resources.TextSizeMedium,
                CornerRadius = Resources.CornerRadiusSmall,
                Padding = new Extents(16, 16, 12, 12)
            };
            section.Add(urlTextField);

            return section;
        }

        /// <summary>
        /// Create a new Button to start download.
        /// </summary>
        /// <returns>Button</returns>
        private Tizen.NUI.Components.Button CreateDownloadButton()
        {
            var button = new Tizen.NUI.Components.Button(Resources.PrimaryButtonStyle)
            {
                Text = "Start Download",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 56,
                BackgroundColor = Resources.PrimaryColor,
                TextColor = Color.White,
                PointSize = Resources.TextSizeMedium,
                CornerRadius = Resources.CornerRadiusMedium
            };

            return button;
        }


        /// <summary>
        /// Create progress section with progress bar and label.
        /// </summary>
        /// <returns>View containing progress elements</returns>
        private View CreateProgressSection()
        {
            var section = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = Resources.LayoutSpacingSmall
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                BackgroundColor = Resources.SurfaceColor,
                CornerRadius = Resources.CornerRadiusMedium,
                Padding = Resources.CardPadding
            };

            // Progress label
            var progressTitleLabel = new TextLabel
            {
                Text = "Download Progress",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                PointSize = Resources.TextSizeMedium,
                TextColor = Resources.TextColor
            };
            section.Add(progressTitleLabel);

            // Add spacing
            var spacer2 = new View
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = Resources.SpacingSmall
            };
            section.Add(spacer2);

            // Progress bar
            progressBar = new Progress
            {
                CurrentValue = 0.0f,
                MaxValue = 100.0f,
                MinValue = 0.0f,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 8,
                TrackColor = new Tizen.NUI.Color(0.9f, 0.9f, 0.9f, 1.0f),
                ProgressColor = Resources.PrimaryColor,
                CornerRadius = Resources.CornerRadiusSmall
            };
            section.Add(progressBar);

            // Add spacing
            var spacer3 = new View
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = Resources.SpacingSmall
            };
            section.Add(spacer3);

            // Progress label
            progressLabel = new TextLabel
            {
                Text = "0 bytes / 0 bytes",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                PointSize = Resources.TextSizeSmall,
                TextColor = Resources.TextColorSecondary
            };
            section.Add(progressLabel);

            return section;
        }

        /// <summary>
        /// Register event handlers for entry, button and state callback.
        /// </summary>
        private void AddEvent()
        {
            downloadButton.Clicked += OnButtonClicked;
            // goToInfoPageButton.Clicked event is already subscribed in InitializeComponent
            downloadService.DownloadStateChanged += OnStateChanged;
            downloadService.DownloadProgress += OnProgressbarChanged;
        }

        /// <summary>
        /// Event handler when download state is changed.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments including download state</param>
        private void OnStateChanged(object sender, DownloadStateChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.stateMsg))
                return;

            downloadService.DownloadLog("State: " + e.stateMsg);
            
            // Update UI on main thread
            NUIApplication.Post(() =>
            {
                if (e.stateMsg == "Failed")
                {
                    downloadButton.Text = e.stateMsg + "! Please start download again.";
                    // If download is failed, dispose a request
                    downloadService.Dispose();
                    // Enable a download button
                    downloadButton.Sensitive = true;
                }
                else if (e.stateMsg != downloadButton.Text)
                {
                    downloadButton.Text = e.stateMsg;
                }
            });
        }

        /// <summary>
        /// Event handler when data is received.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments including received data size</param>
        private void OnProgressbarChanged(object sender, DownloadProgressEventArgs e)
        {
            if (e.ReceivedSize <= 0)
                return;

            ulong contentSize = downloadService.GetContentSize();
            
            // Update UI on main thread
            NUIApplication.Post(() =>
            {
                if (contentSize > 0)
                {
                    progressBar.CurrentValue = (float)((double)e.ReceivedSize / contentSize * 100.0);
                }
                progressLabel.Text = e.ReceivedSize + " bytes / " + contentSize + " bytes";
            });
        }

        /// <summary>
        /// Event handler when button is clicked.
        /// Once a button is clicked and download is started, a button is disabled.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OnButtonClicked(object sender, ClickedEventArgs e)
        {
            downloadUrl = urlTextField.Text;

            downloadService.DownloadLog("Start Download: " + downloadUrl);
            try
            {
                // Start to download content
                downloadService.StartDownload(downloadUrl);
                // Disable a button to avoid duplicated request.
                downloadButton.Sensitive = false;
            }
            catch (Exception ex)
            {
                downloadService.DownloadLog("Request.Start() is failed: " + ex.Message);
                // In case download is failed, enable a button.
                downloadButton.Sensitive = true;
            }
        }

        /// <summary>
        /// Event handler for the "Go to Download Info Page" button click.
        /// Navigates to the DownloadInfoPage.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OnGoToInfoPageButtonClicked(object sender, ClickedEventArgs e)
        {
            this.Navigator?.Push(new DownloadInfoPage(this.downloadService));
        }

        /// <summary>
        /// Clean up resources when page is disposed.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Unsubscribe from events
                if (downloadService != null)
                {
                    downloadService.DownloadStateChanged -= OnStateChanged;
                    downloadService.DownloadProgress -= OnProgressbarChanged;
                }
            }
            base.Dispose(disposing);
        }
    }
}
