/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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
 */

using Preference.Models;
using System;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace Preference.ViewModels
{
    /// <summary>
    /// Main application ViewModel.
    /// </summary>
    public class EditPreferencesViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field for <see cref="NameValue"/> property.
        /// </summary>
        private string _nameValue;

        /// <summary>
        /// Backing field for <see cref="SurnameValue"/> property.
        /// </summary>
        private string _surnameValue;

        /// <summary>
        /// Backing field for <see cref="AgeValue"/> property.
        /// </summary>
        private string _ageValue;

        /// <summary>
        /// Backing field for <see cref="HeightValue"/> property.
        /// </summary>
        private string _heightValue;

        /// <summary>
        /// Backing field for <see cref="CPlusPlus"/> property.
        /// </summary>
        private bool? _cplusplusValue;

        /// <summary>
        /// Backing field for <see cref="CSharp"/> property.
        /// </summary>
        private bool? _csharpValue;

        /// <summary>
        /// Backing field for <see cref="JavaValue"/> property.
        /// </summary>
        private bool? _javaValue;

        /// <summary>
        /// <see cref="PreferenceModel"/> instance.
        /// </summary>
        private readonly PreferenceModel _model = new PreferenceModel();

        #endregion

        #region properties

        /// <summary>
        /// Property for "Name" value.
        /// </summary>
        public string NameValue
        {
            get => _nameValue;
            set => SetProperty(ref _nameValue, value);
        }

        /// <summary>
        /// Property for "Surname" value.
        /// </summary>
        public string SurnameValue
        {
            get => _surnameValue;
            set => SetProperty(ref _surnameValue, value);
        }

        /// <summary>
        /// Property for "Age" value.
        /// </summary>
        public string AgeValue
        {
            get => _ageValue;
            set => SetProperty(ref _ageValue, value);
        }

        /// <summary>
        /// Property for "Height" value.
        /// </summary>
        public string HeightValue
        {
            get => _heightValue;
            set => SetProperty(ref _heightValue, value);
        }

        /// <summary>
        /// Property for "CPlusPlusValue" value.
        /// </summary>
        public bool? CPlusPlusValue
        {
            get => _cplusplusValue;
            set => SetProperty(ref _cplusplusValue, value);
        }

        /// <summary>
        /// Property for "C#" value.
        /// </summary>
        public bool? CSharpValue
        {
            get => _csharpValue;
            set => SetProperty(ref _csharpValue, value);
        }

        /// <summary>
        /// Property for "Java" value.
        /// </summary>
        public bool? JavaValue
        {
            get => _javaValue;
            set => SetProperty(ref _javaValue, value);
        }

        /// <summary>
        /// Save command.
        /// </summary>
        public ICommand SaveRequestCommand { get; }

        public event EventHandler OnSaveCompleteEvent;

        public event EventHandler OnLoadCompleteEvent;

        public event EventHandler OnInvalidDataEvent;

        #endregion

        #region methods

        /// <summary>
        /// EditPreferencesViewModel class constructor.
        /// Defines "Save" command.
        /// Calls load preferences method.
        /// </summary>
        public EditPreferencesViewModel()
        {
            SaveRequestCommand = new Command(SavePreferences);
            LoadPreferences();
        }

        /// <summary>
        /// Stores data provided by user if data are correct.
        /// </summary>
        private void SavePreferences()
        {
            int tempAgeValue;
            double tempHeightValue;

            if (int.TryParse(AgeValue, out tempAgeValue) &&
                double.TryParse(HeightValue, NumberStyles.Float, CultureInfo.InvariantCulture, out tempHeightValue))
            {
                _model.Set("Name", NameValue);
                _model.Set("Surname", SurnameValue);
                _model.Set("Age", tempAgeValue);
                _model.Set("Height", tempHeightValue);
                _model.Set("CPlusPlus", CPlusPlusValue);
                _model.Set("CSharp", CSharpValue);
                _model.Set("Java", JavaValue);
                OnSaveComplete();
            }
            else
            {
                OnInvalidData();
            }
        }

        /// <summary>
        /// Loads stored data.
        /// </summary>
        public void LoadPreferences()
        {
            NameValue = _model.Get<string>("Name").ToString();
            SurnameValue = _model.Get<string>("Surname").ToString();
            AgeValue = _model.Get<int>("Age").ToString();
            HeightValue = _model.Get<double>("Height").ToString();
            CPlusPlusValue = _model.Get<bool>("CPlusPlus") as bool?;
            CSharpValue = _model.Get<bool>("CSharp") as bool?;
            JavaValue = _model.Get<bool>("Java") as bool?;

            OnLoadComplete();
        }

        /// <summary>
        /// Invokes event handlers bound to OnSaveCompleteEvent.
        /// </summary>
        protected virtual void OnSaveComplete()
        {
            OnSaveCompleteEvent?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Invokes event handlers bound to OnLoadCompleteEvent.
        /// </summary>
        protected virtual void OnLoadComplete()
        {
            OnLoadCompleteEvent?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Invokes event handlers bound to OnInvalidDataEvent.
        /// </summary>
        protected virtual void OnInvalidData()
        {
            OnInvalidDataEvent?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}