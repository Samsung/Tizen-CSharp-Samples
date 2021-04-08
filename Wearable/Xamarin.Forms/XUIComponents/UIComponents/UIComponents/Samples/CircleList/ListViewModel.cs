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


using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace UIComponents.Samples.CircleList
{
    /// <summary>
    /// ListViewModel class
    /// This class is used for CircleList sample's binding context
    /// </summary>
    public class ListViewModel : INotifyPropertyChanged
    {
        const string SelectAll = "Select all";
        const string DeselectAll = "Deselect all";
        static List<string> _names = new List<string>
        {
            "Aaliyah", "Aamir", "Aaralyn", "Aaron", "Abagail",
            "Babitha", "Bahuratna", "Bandana", "Bulbul", "Cade", "Caldwell",
            "Chandan", "Caster", "Dagan ", "Daulat", "Dag", "Earl", "Ebenzer",
            "Ellison", "Elizabeth", "Filbert", "Fitzpatrick", "Florian", "Fulton",
            "Frazer", "Gabriel", "Gage", "Galen", "Garland", "Gauhar", "Hadden",
            "Hafiz", "Hakon", "Haleem", "Hank", "Hanuman", "Jabali ", "Jaimini",
            "Jayadev", "Jake", "Jayatsena", "Jonathan", "Kamaal", "Jeirk",
            "Jasper", "Jack", "Mac", "Macy", "Marlon", "Milson"
        };

        static List<string> _longTexts = new List<string>
        {
            "Hey John, how have you been?",
            "Andy, it's been a long time, how are you man?",
            "I finally have some free time. I just finished taking a big examination, and I'm so relieved that I'm done with it",
            "Wow. How long has it been? It seems like more than a year. I'm doing pretty well. How about you?",
            "I'm playing a video game on my computer because I have nothing to do.",
            "I'm pretty busy right now. I'm doing my homework because I have an exam tomorrow.",
            "I'm taking the day off from work today because I have so many errands. I'm going to the post office to send some packages to my friends."
        };

        int _checkedNamesCount;
        string _selectOptionMessage1;
        string _selectOptionMessage2;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> Names => _names;
        public List<string> LongTexts => _longTexts;
        public ObservableCollection<CheckableName> CheckableNames { get; private set; }

        /// <summary>
        /// Getter for CheckedNamesCount
        /// </summary>
        public int CheckedNamesCount
        {
            get => _checkedNamesCount;
            private set
            {
                if (_checkedNamesCount != value)
                {
                    _checkedNamesCount = value;
                    OnPropertyChanged();

                    UpdateSelectOptionMessage();
                }
            }
        }

        /// <summary>
        /// Setter and Getter for SelectOptionMessage1
        /// </summary>
        public string SelectOptionMessage1
        {
            get => _selectOptionMessage1;
            set
            {
                if (_selectOptionMessage1 != value)
                {
                    _selectOptionMessage1 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Setter and Getter for SelectOptionMessage2
        /// </summary>
        public string SelectOptionMessage2
        {
            get => _selectOptionMessage2;
            set
            {
                if (_selectOptionMessage2 != value)
                {
                    _selectOptionMessage2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Command for Accept button of ContextPopupEffectBehavior
        /// </summary>
        public ICommand SelectCommand1 => new Command(SelectOption1Job);
        /// <summary>
        /// Command for Cancel button of ContextPopupEffectBehavior
        /// </summary>
        public ICommand SelectCommand2 => new Command(SelectOption2Job);


        /// <summary>
        /// Constructor
        /// </summary>
        public ListViewModel()
        {
            CheckableNames = new ObservableCollection<CheckableName>();
            foreach (var name in _names)
            {
                var data = new CheckableName(name, false);
                data.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "Checked")
                    {
                        CheckedNamesCount += data.Checked ? 1 : -1;
                    }
                };
                CheckableNames.Add(data);
            }

            UpdateSelectOptionMessage();
        }

        /// <summary>
        /// Called to notify that a change of property happened
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Called when Accept button of ContextPopupEffectBehavior is executed.
        /// </summary>
        void SelectOption1Job()
        {
            bool r = CheckedNamesCount < CheckableNames.Count;
            foreach (var x in CheckableNames)
            {
                x.Checked = r;
            }
        }
        /// <summary>
        /// Called when Cancel button of ContextPopupEffectBehavior is executed.
        /// </summary>
        void SelectOption2Job()
        {
            if (CheckedNamesCount > 0 && CheckedNamesCount != CheckableNames.Count)
            {
                foreach (var x in CheckableNames)
                {
                    x.Checked = false;
                }
            }
        }

        /// <summary>
        /// Set SelectOptionMessage
        /// </summary>
        void UpdateSelectOptionMessage()
        {
            SelectOptionMessage1 = _checkedNamesCount < CheckableNames.Count ? SelectAll : DeselectAll;
            SelectOptionMessage2 = _checkedNamesCount != 0 && _checkedNamesCount != CheckableNames.Count ? DeselectAll : "";
        }
    }

    /// <summary>
    /// CheckableName class
    /// This class is element of MyGroup class
    /// </summary>
    public class CheckableName : INotifyPropertyChanged
    {
        string _name;
        bool _checked;

        /// <summary>
        /// Constructor of CheckableName class
        /// </summary>
        /// <param name="name">string</param>
        /// <param name="isChecked">bool</param>
        public CheckableName(string name, bool isChecked)
        {
            _name = name;
            _checked = isChecked;
        }

        /// <summary>
        /// Handle the PropertyChanged event raised when a property is changed on a component
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Setter and Getter for Name
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Setter and Getter for Checked
        /// </summary>
        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked != value)
                {
                    _checked = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Called to notify that a change of property happened
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// MyGroup class
    /// This class is element of GroupList class
    /// </summary>
    public class MyGroup : List<CheckableName>
    {
        /// <summary>
        /// Setter and Getter for group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">string</param>
        public MyGroup(string name)
        {
            GroupName = name;
        }
    }

    /// <summary>
    /// ListViewGroupModel class
    /// </summary>
    public class ListViewGroupModel
    {
        /// <summary>
        /// Getter for group list
        /// </summary>
        public List<MyGroup> GroupList { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ListViewGroupModel()
        {

            GroupList = new List<MyGroup>
            {
                new MyGroup("group1")
                {
                    new CheckableName("Aaliyah", false),
                    new CheckableName("Aamir", false),
                    new CheckableName("Aaralyn", false),
                    new CheckableName("Aaron", false),
                    new CheckableName("Abagail", false),
                    new CheckableName("Babitha", false),
                    new CheckableName("Bahuratna", false),
                    new CheckableName("Bandana", false),
                    new CheckableName("Bulbul", false),
                    new CheckableName("Cade", false),
                    new CheckableName("Caldwell", false)
                },
                new MyGroup("group2")
                { 
                    new CheckableName("Chandan", false),
                    new CheckableName("Caster", false),
                    new CheckableName("Dagan", false),
                    new CheckableName("Daulat", false),
                    new CheckableName("Dag", false),
                    new CheckableName("Earl", false),
                    new CheckableName("Ebenzer", false),
                    new CheckableName("Ellison", false),
                    new CheckableName("Elizabeth", false),
                    new CheckableName("Filbert", false),
                    new CheckableName("Fitzpatrick", false),
                    new CheckableName("Florian", false),
                    new CheckableName("Fulton", false)
                },
                new MyGroup("group3")
                { 
                    new CheckableName("Frazer", false),
                    new CheckableName("Gabriel", false),
                    new CheckableName("Gage", false),
                    new CheckableName("Galen", false),
                    new CheckableName("Garland", false),
                    new CheckableName("Gauhar", false),
                    new CheckableName("Hadden", false),
                    new CheckableName("Hafiz", false),
                    new CheckableName("Hakon", false),
                    new CheckableName("Haleem", false),
                    new CheckableName("Hank", false),
                    new CheckableName("Hanuman", false)
                },
                new MyGroup("group4")
                { 
                    new CheckableName("Jabali", false),
                    new CheckableName("Jaimini", false),
                    new CheckableName("Jayadev", false),
                    new CheckableName("Jake", false),
                    new CheckableName("Jayatsena", false),
                    new CheckableName("Jonathan", false),
                    new CheckableName("Jeirk", false),
                    new CheckableName("Jasper", false),
                    new CheckableName("Jack", false),
                    new CheckableName("Kamaal", false),
                    new CheckableName("Mac", false),
                    new CheckableName("Macy", false),
                    new CheckableName("Marlon", false),
                    new CheckableName("Milson", false)
                },
            };
        }
    }
}