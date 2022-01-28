using System;
using System.Collections.Generic;
using Tizen.Location.Geofence;
using Tizen.NUI;
using Tizen.NUI.Binding;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Geofence.ViewModels;
using static Tizen.NUI.BaseComponents.TextField;

namespace Geofence
{
    public partial class SelectIDPage : ContentPage
    {
        /// <summary>
        /// Place Name
        /// </summary>
        private string placeName = "";

        public SelectIDPage(VirtualPerimeter perimeter, string title)
        {
            InitializeComponent();

            AppBar.Title = title;

            BindingContext = new SelectIDPageViewModel(perimeter);

            ColView.ItemTemplate = new Tizen.NUI.Binding.DataTemplate(() =>
            {
                var item = new DefaultLinearItem()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                };
                item.Label.SetBinding(TextLabel.TextProperty, "Item");

                //var label = new TextLabel()
                //{
                //    WidthSpecification = LayoutParamPolicies.MatchParent,
                //};
                //label.SetBinding(TextLabel.TextProperty, "Item";
                return item;
            });
        }
    }
}
