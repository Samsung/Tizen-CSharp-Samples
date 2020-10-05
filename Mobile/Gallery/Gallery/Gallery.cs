using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.UIComponents;

namespace Gallery
{
    class Program : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            /// Create the main flex container
            FlexContainer flexContainer = new FlexContainer();
            flexContainer.ParentOrigin = ParentOrigin.TopLeft;
            flexContainer.PivotPoint = PivotPoint.TopLeft;
            flexContainer.WidthResizePolicy = ResizePolicyType.FillToParent;
            flexContainer.HeightResizePolicy = ResizePolicyType.FillToParent;
            /// Set the background color to white
            flexContainer.BackgroundColor = Color.White;
            /// Add the container to the window
            Window.Instance.Add(flexContainer);

            /// Display toolbar and content vertically
            flexContainer.FlexDirection = FlexContainer.FlexDirectionType.Column;

            /// Create the toolbar
            FlexContainer toolBar = new FlexContainer();
            toolBar.ParentOrigin = ParentOrigin.TopLeft;
            toolBar.PivotPoint = PivotPoint.TopLeft;
            /// Set the toolbar background color
            toolBar.BackgroundColor = Color.Cyan;
            /// Add the toolbar to the main container
            flexContainer.Add(toolBar);

            /// Display toolbar items horizontally
            toolBar.FlexDirection = FlexContainer.FlexDirectionType.Row;
            /// Align toolbar items vertically center
            toolBar.AlignItems = FlexContainer.Alignment.AlignCenter;
            /// Create equal empty spaces between children
            toolBar.JustifyContent = FlexContainer.Justification.JustifySpaceBetween;

            /// Occupy 10 percent of available space on the cross axis
            toolBar.Flex = 0.1f;

            /// Create the content area
            FlexContainer content = new FlexContainer();
            content.ParentOrigin = ParentOrigin.TopLeft;
            content.PivotPoint = PivotPoint.TopLeft;
            /// Place items horizontally
            content.FlexDirection = FlexContainer.FlexDirectionType.Row;
            /// Align items horizontally center
            content.JustifyContent = FlexContainer.Justification.JustifyCenter;
            /// Align items vertically center
            content.AlignItems = FlexContainer.Alignment.AlignCenter;
            /// Occupy 90 percent of available space on the cross axis
            content.Flex = 0.9f;
            /// Add the content area to the main container
            flexContainer.Add(content);

            /// Add a button to the left of the toolbar
            PushButton prevButton = new PushButton();
            prevButton.ParentOrigin = ParentOrigin.TopLeft;
            prevButton.PivotPoint = PivotPoint.TopLeft;
            /// Minimum size the button must keep
            prevButton.MinimumSize = new Vector2(100.0f, 60.0f);
            /// Set a 10-pixel margin around the button
            prevButton.FlexMargin = new Vector4(10.0f, 10.0f, 10.0f, 10.0f);
            toolBar.Add(prevButton);

            /// Set the button text
            PropertyMap labelMap = new PropertyMap();
            labelMap.Add("text", new PropertyValue("Prev"));
            labelMap.Add("textColor", new PropertyValue(Color.Black));
            prevButton.Label = labelMap;

            /// Add a title to the center of the toolbar
            TextLabel title = new TextLabel("Gallery");
            title.ParentOrigin = ParentOrigin.TopLeft;
            title.PivotPoint = PivotPoint.TopLeft;
            title.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            title.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            /// Set a 10-pixel margin around the title
            title.FlexMargin = new Vector4(10.0f, 10.0f, 10.0f, 10.0f);
            toolBar.Add(title);

            /// Add a button to the right of the toolbar
            PushButton nextButton = new PushButton();
            nextButton.ParentOrigin = ParentOrigin.TopLeft;
            nextButton.PivotPoint = PivotPoint.TopLeft;
            /// Minimum size the button must keep
            nextButton.MinimumSize = new Vector2(100.0f, 60.0f);
            /// Set a 10-pixel margin around the button
            nextButton.FlexMargin = new Vector4(10.0f, 10.0f, 10.0f, 10.0f);
            toolBar.Add(nextButton);

            /// Set the button text
            PropertyMap labelMap2 = new PropertyMap();
            labelMap2.Add("text", new PropertyValue("Next"));
            labelMap2.Add("textColor", new PropertyValue(Color.Black));
            nextButton.Label = labelMap2;

            /// Add an image to the center of the content area
            ImageView imageView = new ImageView(DirectoryInfo.Resource + "image.png");
            imageView.ParentOrigin = ParentOrigin.TopLeft;
            imageView.PivotPoint = PivotPoint.TopLeft;
            content.Add(imageView);
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
