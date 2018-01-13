﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoughtsAndCrosses
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainLayout : ContentPage
	{
		public MainLayout ()
		{
			InitializeComponent ();
		}

        public void SetLabelHelloWorld(string text)
        {
            labelHelloWorld.Text = text;
        }
	}
}