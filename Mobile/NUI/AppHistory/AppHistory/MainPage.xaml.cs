using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.Security;
using Tizen.NUI.Binding;
using AppHistory.ViewModel;
using System.Linq;

namespace AppHistory
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CheckPermission();

            BindingContext = new MainPageViewModel();

            ColView.ItemTemplate = new Tizen.NUI.Binding.DataTemplate(() =>
            {
                var item = new RecyclerViewItem()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    Layout = new FlexLayout
                    {
                        Direction = FlexLayout.FlexDirection.Column,
                        Justification = FlexLayout.FlexJustification.SpaceBetween,
                    }
                };

                item.Clicked += OnClicked;

                var firstLabel = new TextLabel()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    TextColor = Color.Black
                };
                firstLabel.SetBinding(TextLabel.TextProperty, "FirstLine");
                item.Add(firstLabel);

                var secondLabel = new TextLabel()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                    TextColor = Color.Gray
                };
                secondLabel.SetBinding(TextLabel.TextProperty, "SecondLine");
                item.Add(secondLabel);
                return item;
            });
        }

        private void OnClicked(object sender, ClickedEventArgs e)
        {
            ListType listType = ListType.Battery;
            switch (((sender as RecyclerViewItem).Children.First() as TextLabel).Text)
            {
                case "Top 10 frequently used applications":
                    listType = ListType.Frequently;
                    break;
                case "Top 5 recently used applications":
                    listType = ListType.Recently;
                    break;
            }
            Navigator?.PushWithTransition(new AppInfoPage(listType));
        }

        private void CheckPermission()
        {
            CheckResult result = PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/apphistory.read");

            switch (result)
            {
                case CheckResult.Allow:
                    break;
                case CheckResult.Deny:
                    break;
                case CheckResult.Ask:
                    PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/apphistory.read");
                    break;
            }
        }

        /// <summary>
        /// User needs to implement this, if required
        /// </summary>
        /// <param name="type">dispose type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            ExitXaml();

            if (type == DisposeTypes.Explicit)
            {
                //Todo: Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.

            }

            //Todo: Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.


            base.Dispose(type);
        }
    }
}
