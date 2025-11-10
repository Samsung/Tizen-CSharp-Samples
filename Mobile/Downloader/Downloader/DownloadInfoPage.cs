using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Binding;

namespace Downloader
{
    /// <summary>
    /// A public class to show download information page in NUI.
    /// </summary>
    public class DownloadInfoPage : ContentPage
    {
        private View downloadInfoLabel;
        private CollectionView downloadInfoListView;
        private List<DownloadInfo> downloadInfoList = new List<DownloadInfo>();
        private bool isUpdatedInfoList;
        private Tizen.NUI.Components.Button goToMainPageButton; // Button to navigate to main page
        private IDownload downloadService;

        /// <summary>
        /// Default constructor (creates a new download service instance)
        /// </summary>
        public DownloadInfoPage() : this(new DownloadImplementation())
        {
        }

        /// <summary>
        /// Constructor that accepts a shared download service instance.
        /// </summary>
        /// <param name="sharedDownloadService">The IDownload service instance to use.</param>
        public DownloadInfoPage(IDownload sharedDownloadService)
        {
            downloadService = sharedDownloadService;
            InitializeComponent();
        }

        /// <summary>
        /// Initialize page events.
        /// </summary>
        private void InitializeEvents()
        {
            // Update a download information list when this page is appearing
            this.Appearing += (object sender, PageAppearingEventArgs e) =>
            {
                UpdateDownloadInfoList();
            };
        }

        /// <summary>
        /// Initialize components of page.
        /// </summary>
        private void InitializeComponent()
        {
            AppBar = new AppBar
            {
                Title = "Download Info",
                BackgroundColor = Resources.PrimaryColor
            };

            // Create and add the navigation button to the AppBar using TextLabel instead of Button
            var mainButtonContainer = new View()
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
            
            var mainButtonText = new TextLabel
            {
                Text = "Main",
                TextColor = Color.White,
                PointSize = Resources.TextSizeMedium,
                WidthSpecification = LayoutParamPolicies.WrapContent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            
            mainButtonContainer.Add(mainButtonText);
            mainButtonContainer.TouchEvent += (s, e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Up)
                {
                    OnGoToMainPageButtonClicked(s, null);
                }
                return true;
            };
            
            goToMainPageButton = null; // Not using Button anymore
            AppBar.Actions = new View[] { mainButtonContainer };

            downloadInfoLabel = CreateDownloadInfoLabel();
            downloadInfoListView = CreateCollectionView();

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
                Padding = Resources.PagePadding
            };

            mainLayout.Add(downloadInfoLabel);
            mainLayout.Add(downloadInfoListView);

            Content = mainLayout;

            isUpdatedInfoList = false;
            
            // Initialize events after content is set
            InitializeEvents();
            AddEvent(); // Add button events
        }


        /// <summary>
        /// Register event handlers for buttons.
        /// </summary>
        private void AddEvent()
        {
            // goToMainPageButton.Clicked event is already subscribed in InitializeComponent
        }

        /// <summary>
        /// Event handler for the "Go to Download Main Page" button click.
        /// Navigates back to the DownloadMainPage.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OnGoToMainPageButtonClicked(object sender, ClickedEventArgs e)
        {
            var navigator = this.Navigator;
            if (navigator != null && navigator.PageCount > 1)
            {
                navigator.Pop();
            }
        }

        /// <summary>
        /// Update a download information list.
        /// </summary>
        private void UpdateDownloadInfoList()
        {
            // Update a download information list when download is completed
            if (!isUpdatedInfoList && downloadService.GetDownloadState() == (int)Downloader.State.Completed)
            {
                // Clear existing list
                downloadInfoList.Clear();

                // Add downloaded URL to list
                downloadInfoList.Add(new DownloadInfo("URL", downloadService.GetUrl()));
                // Add downloaded content name to list
                downloadInfoList.Add(new DownloadInfo("Content Name", downloadService.GetContentName()));
                // Add downloaded content size to list
                downloadInfoList.Add(new DownloadInfo("Content Size", downloadService.GetContentSize().ToString()));
                // Add downloaded MIME type to list
                downloadInfoList.Add(new DownloadInfo("MIME Type", downloadService.GetMimeType()));
                // Add downloaded path to list
                downloadInfoList.Add(new DownloadInfo("Download Path", downloadService.GetDownloadedPath()));

                // Update the CollectionView
                downloadInfoListView.ItemsSource = downloadInfoList;

                isUpdatedInfoList = true;
            }
        }

        /// <summary>
        /// Create a new Label container.
        /// </summary>
        /// <returns>View containing styled label</returns>
        private View CreateDownloadInfoLabel()
        {
            var labelContainer = new View
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

            var label = new TextLabel
            {
                Text = "Download Information",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                PointSize = Resources.TextSizeLarge,
                TextColor = Resources.TextColor,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            
            labelContainer.Add(label);
            return labelContainer;
        }

        /// <summary>
        /// Create a new CollectionView
        /// This CollectionView shows download information list.
        /// </summary>
        /// <returns>CollectionView</returns>
        private CollectionView CreateCollectionView()
        {
            var collectionView = new CollectionView
            {
                ItemsSource = downloadInfoList,
                ScrollingDirection = ScrollableBase.Direction.Vertical,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                ItemsLayouter = new LinearLayouter()
            };

            collectionView.ItemTemplate = new DataTemplate(() =>
            {
                var item = new DefaultLinearItem
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = LayoutParamPolicies.WrapContent,
                    BackgroundColor = Resources.SurfaceColor,
                    CornerRadius = Resources.CornerRadiusSmall,
                    Padding = Resources.CardPadding
                };

                // Create a horizontal layout for name and value
                var itemLayout = new View
                {
                    Layout = new LinearLayout
                    {
                        LinearOrientation = LinearLayout.Orientation.Horizontal,
                        CellPadding = new Size2D(16, 0) // Add horizontal spacing between name and value
                    },
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = LayoutParamPolicies.WrapContent
                };

                // Name label
                var nameLabel = new TextLabel
                {
                    WidthSpecification = 200, // Fixed width for name column
                    HeightSpecification = LayoutParamPolicies.WrapContent,
                    PointSize = Resources.TextSizeMedium,
                    TextColor = Resources.TextColor
                };
                nameLabel.SetBinding(TextLabel.TextProperty, "Name");

                // Value label
                var valueLabel = new TextLabel
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    HeightSpecification = LayoutParamPolicies.WrapContent,
                    PointSize = Resources.TextSizeMedium,
                    TextColor = Resources.TextColorSecondary,
                    HorizontalAlignment = HorizontalAlignment.Begin
                };
                valueLabel.SetBinding(TextLabel.TextProperty, "Value");

                itemLayout.Add(nameLabel);
                itemLayout.Add(valueLabel);

                item.Add(itemLayout);

                return item;
            });

            return collectionView;
        }
    }
}
