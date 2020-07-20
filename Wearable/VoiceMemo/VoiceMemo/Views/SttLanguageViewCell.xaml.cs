/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using VoiceMemo.Models;
using VoiceMemo.ViewModels;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace VoiceMemo.Views
{
    /// <summary>
    /// SttLanguageViewCell
    /// It's a custom view cell for CircleListView in LanguageSelectionPage
    /// </summary>
	public partial class SttLanguageViewCell : ViewCell
	{
		public SttLanguageViewCell()
		{
			InitializeComponent();
		}

        // Called when radio button is selected
        public async void OnSelected(object sender, SelectedEventArgs args)
        {
            Console.WriteLine($"Radio.OnSoundSelected!! value:{args.Value}");
            Radio radio = sender as Radio;
            if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
            {
                int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;

                LanguageSelectionPage currPage = Application.Current.MainPage.Navigation.NavigationStack[index] as LanguageSelectionPage;
                //Error CS0023  Operator '!' cannot be applied to operand of type 'string'
                if (args.Value != null)
                {
                    Console.WriteLine("  < UNSELECTED >  XXXXXX sender " + sender + " -  " + radio.Value);
                    currPage.ignoreRadioSelection = false;
                    return;
                }
                else
                {
                    if (currPage.ignoreRadioSelection)
                    {
                        Console.WriteLine("\n\nRadio_Selected  >>>>>>>  SKIP!\n\n");
                        currPage.ignoreRadioSelection = false;
                        return;
                    }

                    Console.WriteLine("  < SELECTED >  XXXXXX sender " + sender + " -  " + radio.Value);
                    SttLanguage item = radio.BindingContext as SttLanguage;
                    ((MainPageModel)currPage.BindingContext).CurrentLanguage = item.Lang;

                    ((MainPageModel)currPage.BindingContext).SelectedItemIndex = item;
                    await currPage.Navigation.PopToRootAsync();
                }
            }
        }
    }
}