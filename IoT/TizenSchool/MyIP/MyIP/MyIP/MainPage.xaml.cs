using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

namespace MyIP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Load(object sender, EventArgs e)
        {
            myIP.Text = await new HttpClient().GetStringAsync("https://api6.ipify.org");
        }

        private void Button_Reset(object sender, EventArgs e)
        {
            myIP.Text = "0.0.0.0";
        }
    }
}