/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
 *
 */


using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

/// <summary>
/// namespace for Tizen.TV.NUI package
/// </summary>
namespace Tizen.NUI.MediaHub
{
    public class Dropdown : View
    {
        /// <summary>
        /// list position relative to the button
        /// </summary>
        public enum ListPosition
        {
            Up = 0,            //!< Use this style only if there's not enough area to show the lists below the button.
            Down,              //!< DropDown list is located below the button.
        }

        /// <summary>
        /// class to define event args of list item Selected
        /// </summary>
        /// <code>
        /// public event EventHandler<ItemSelectedEventArgs> ItemSelectedEvent      
        /// </code>
        public class ItemSelectedEventArgs : EventArgs
        {
            /// <summary>list item selected index. </summary>
            public int SelectedItemIndex;
        }

        /// <summary>
        /// Constructor to create new Dropdown
        /// </summary>
        public Dropdown()
        {
            Initialize();
        }

        /// <summary>
        /// Item Selected event handler, can be added or removed by user.
        /// </summary>
        public event EventHandler<ItemSelectedEventArgs> ItemSelectedEvent
        {
            add
            {
                itemSelectedHander += value;
            }

            remove
            {
                itemSelectedHander -= value;
            }
        }

        /// <summary>
        /// property for get or set DropdownButton text
        /// </summary>
        public string ButtonText
        {
            get
            {
                return customButton.Text;
            }

            set
            {
                if (customButton != null)
                {
                    customButton.Text = value;
                }
            }
        }

        /// <summary>
        /// Set Item height, must be set before AddData 
        /// </summary>
        public int ListItemHeight
        {
            get
            {
                return optionList.ItemHeight;
            }

            set
            {
                if (optionList != null)
                {
                    optionList.ItemHeight = value;
                }
            }
        }

        public Button DropDownButton
        {
            get
            {
                return dropdownButton;
            }
        }

        /// <summary>
        /// property for get or set optionList Selected item index.
        /// </summary>
        public int SelectedListItemIndex
        {
            get
            {
                return optionList.SelectItemIndex;
            }

            set
            {
                if (optionList != null)
                {
                    optionList.SelectItemIndex = value;
                    ButtonText = optionList.TextString(value);
                }
            }
        }

        /// <summary>
        /// property for get or set optionList focus item index.
        /// </summary>
        public int FocusListItemIndex
        {
            get
            {
                return optionList.FocusItemIndex;
            }

            set
            {
                if (optionList != null)
                {
                    optionList.FocusItemIndex = value;
                }
            }
        }

        /// <summary>
        /// property for get optionList item numbers.
        /// </summary>
        public int ListItemNumbers
        {
            get
            {
                return optionList.NumOfItem;
            }
        }

        /// <summary>
        /// Add OptionList item text string, only effect for UI style.
        /// </summary>
        /// <param name="textString"> OptionList item text string </param>
        public void AddListData(string textString)
        {
            if (optionList != null)
            {
                Tizen.Log.Fatal("NUI"," [------" + "enter into Dropdown AddListData" + " ------]" + textString);
                optionList.AddData(textString);
            }
        }


        /// <summary>
        /// Get text string.
        /// </summary>
        /// <param name="index">OptionList item index</param>
        /// <returns>return list item text string.</returns>
        public string ListTextString(int index)
        {
            return optionList.TextString(index);
        }

        /// <summary>
        /// The property to get/set the size of the list.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Throw when ListSize is null.</exception>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     optionList.Size2D = new Size2D(406, 400);
        /// }
        /// catch(InvalidOperationException key)
        /// {
        ///     Tizen.Log.Error(LogTag, "Failed to set list size value : " + key.Message);
        /// }
        /// </code>
        /// </example>
        public Size2D SetListSize
        {
            get
            {
                return optionList.Size2D;
            }

            set
            {
                if (value == null)
                {
                    Tizen.Log.Fatal("NUI", "Set null value to the size of the list!");
                    throw new InvalidOperationException("Wrong size value of the list. It should be a not-null value!");
                }

                if (optionList != null)
                {
                    optionList.Size2D = new Size2D(value.Width, value.Height);
                }
            }
        }      

        /// <summary>
        /// show optionList if necessary while the button is focused on and Enter key clicked
        /// </summary>
        public void ShowList()
        {
            Tizen.Log.Fatal("NUI"," [------" + "enter into Dropdown ShowList" + " ------]");
            if (optionList.NumOfItem == 0)
            {
                return;
            }

            optionList.PositionUsesPivotPoint = true;
            optionList.ParentOrigin = Tizen.NUI.ParentOrigin.TopCenter;
            optionList.PivotPoint = Tizen.NUI.PivotPoint.BottomCenter;
            optionList.Position = new Position(0, 84, 0);
            optionList.Show();

            Tizen.Log.Fatal("NUI"," [------" + "SelectItemIndex: " + optionList.SelectItemIndex + " FocusItemIndex: " + optionList.FocusItemIndex + " ListItemNumbers: " + ListItemNumbers);
            if (optionList.SelectItemIndex >= 0 && optionList.SelectItemIndex < ListItemNumbers)
            {
                optionList.FocusItemIndex = optionList.SelectItemIndex;
            }
            else
            {
                optionList.FocusItemIndex = 0;
            }

            // FocusedControl = optionList;
            FocusManager.Instance.SetCurrentFocusView(optionList);

            if (listCloseAni != null && listCloseAni.State == Animation.States.Playing)
            {
                listCloseAni.Stop();
            }

            if (listOpenAni == null)
            {
                Tizen.Log.Fatal("NUI"," [------" + "enter into Dropdown ShowList" + " ------]");
                listOpenAni = new Animation(334);
                listOpenAni.DefaultAlphaFunction = new AlphaFunction(new Vector2(0.3f, 0.0f), new Vector2(0.15f, 1.0f));
                listOpenAni.AnimateTo(optionList, "Opacity", 1.0f);
                listOpenAni.AnimateTo(optionList, "PositionY", -4.0f, 0, 334);
                Tizen.Log.Fatal("NUI", " [------" + "enter into Dropdown ShowList" + " ------]" + optionList.Position.Y);
            }

            listOpenAni.Play();
        }

        /// <summary>
        /// hide optionList if necessary
        /// </summary>
        public void HideList()
        {
            if (listOpenAni != null && listOpenAni.State == Animation.States.Playing)
            {
                Tizen.Log.Fatal("NUI"," [------" + "enter into Dropdown HideList" + " ------]");
                listOpenAni.Stop();
            }

            if (listCloseAni == null)
            {
                listCloseAni = new Animation(334);
                listCloseAni.DefaultAlphaFunction = new AlphaFunction(new Vector2(0.3f, 0.0f), new Vector2(0.15f, 1.0f));
                listCloseAni.AnimateTo(optionList, "Opacity", 0.0f);
                listCloseAni.AnimateTo(optionList, "PositionY", 84.0f, 0, 334);
            }

            listCloseAni.Play();
            Tizen.Log.Fatal("NUI"," [------" + "enter into Dropdown HideList listCloseAni" + " ------]");
        }

        /// <summary>
        /// Dispose Dropdown.
        /// </summary>
        /// <param name="type">dispose types.</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.

                Remove(dropdownButton);
                //dropdownButton.Clicked -= ClickEventHandler;
                dropdownButton.Dispose();
                dropdownButton = null;

                Remove(optionList);
                optionList.OptionListEvent -= OnOptionListEvent;
                optionList.KeyEvent -= OnListKeyPressed;
                optionList.Dispose();
                optionList = null;


                listOpenAni?.Stop();
                listOpenAni?.Clear();
                listOpenAni?.Dispose();
                listOpenAni = null;

                listCloseAni?.Stop();
                listCloseAni?.Clear();
                listCloseAni?.Dispose();
                listCloseAni = null;
            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.
            //Unreference this from if a static instance refer to this. 

            //You must call base.Dispose(type) just before exit.
            base.Dispose(type);
        }

        /// <summary>
        /// Overrides this method if want to update attributes of component.
        /// </summary>       
        private void OnUpdate()
        {
            SetAttribute();
            Tizen.Log.Fatal("NUI","..." + this.GetHashCode() + " Name:" + this.Name);
        }

        /// <summary>
        /// The callback of FocusGained event
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event args</param>
        private void ControlFocusGained(object sender, EventArgs e)
        {
            OnFocusGained();
        }

        /// <summary>
        /// The callback of FocusLost Event
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event args</param>
        private void ControlFocusLost(object sender, EventArgs e)
        {
            OnFocusLost();
        }

        /// <summary>
        /// update view states when focus gained
        /// </summary>
        private void OnFocusGained()
        {
            Tizen.Log.Fatal("NUI"," [------" + "enter into Dropdown ViewFocusGained" + " ------]" + Name);
            if (dropdownButton != null)
            {
                // FocusedControl = dropdownButton;
                FocusManager.Instance.SetCurrentFocusView(dropdownButton);
            }

            if (optionList != null)
            {
                HideList();
            }
        }

        /// <summary>
        /// update view states when focus lost
        /// </summary>
        private void OnFocusLost()
        {
            //TODO hide list 
            if (optionList != null)
            {
                HideList();
            }
        }       

        private void Initialize()
        {
            Tizen.Log.Fatal("NUI"," [------" + "enter into Dropdown Initiate" + " ------]");
            Relayout += OnRelayout;
            FocusGained += ControlFocusGained;
            FocusLost += ControlFocusLost;
            customButton = new CustomButton();
            dropdownButton = customButton.GetPushButton();
            dropdownButton.Name = "DropdownButton";
            dropdownButton.PositionUsesPivotPoint = false;
            dropdownButton.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            dropdownButton.PivotPoint = Tizen.NUI.PivotPoint.Center;
            dropdownButton.Position = new Position(0, 0, 0);
            Add(dropdownButton);
            dropdownButton.Focusable = true;

            optionList = new OptionList();
            optionList.Name = "OptionList";
            optionList.PositionUsesPivotPoint = true;
            optionList.PivotPoint = Tizen.NUI.PivotPoint.TopCenter;
            optionList.ParentOrigin = Tizen.NUI.ParentOrigin.TopCenter;
            optionList.Position = new Position(0, 0, 0);
            Add(optionList);
            optionList.Hide();
            optionList.Focusable = true;

            dropdownButton.ClickEvent += (obj, e) =>
            {
                if (optionList != null)
                {
                    if (!optionListFlag)
                    {
                        ShowList();
                        optionListFlag = true;
                    }
                    else
                    {
                        optionListFlag = false;
                    }
                }
            };
            optionList.OptionListEvent += OnOptionListEvent;
            optionList.KeyEvent += OnListKeyPressed;
        }

        private void OnRelayout(object source, EventArgs e)
        {
            OnUpdate();
        }     

        private void OnOptionListEvent(object o, ListView.ListEventArgs e)
        {
            if (e.EventType == ListView.ListEventType.FocusMoveOut)
            {
                if ((e.param[0] == (int)MoveDirection.Down))
                {
                    FocusManager.Instance.SetCurrentFocusView(dropdownButton);
                    HideList();
                }
            }
        }

        /// <summary>
        /// The callback of KeyEvent
        /// </summary>
        /// <param name="source">the object</param>
        /// <param name="e">the args of the event</param>
        /// <returns>whether the key has been consumed or not</returns>
        private bool OnListKeyPressed(object source, View.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && optionList != null)
            {
                if (e.Key.KeyPressedName == "Up")
                {
                    //Key-UP
                    optionList.Move(MoveDirection.Up);
                }
                else if (e.Key.KeyPressedName == "Down")
                {
                    //Key-DOWN
                    optionList.Move(MoveDirection.Down);
                }
                else if (e.Key.KeyPressedName == "BackSpace" || e.Key.KeyPressedName == "XF86Back")
                {
                    Tizen.Log.Fatal("NUI","dropdown Back before");
                    FocusManager.Instance.SetCurrentFocusView(dropdownButton);
                    HideList();
                }
                else if (e.Key.KeyPressedName == "Return")
                {
                    FocusManager.Instance.SetCurrentFocusView(dropdownButton);
                    optionList.SelectItemIndex = optionList.FocusItemIndex;
                    customButton.Text = Name + " : " + optionList.TextString(optionList.SelectItemIndex);
                    ItemSelectedEventArgs ev = new ItemSelectedEventArgs();
                    ev.SelectedItemIndex = optionList.SelectItemIndex;
                    ItemSelectedHandler(this, ev);
                    HideList();
                }
            }

            return true;
        }

        /// <summary>
        /// Set the Size of the button and update the optionList.
        /// </summary>
        private void SetAttribute()
        {
            dropdownButton.Size2D = new Size2D((int)this.SizeWidth, 84);
            optionList.Update();
        }

        /// <summary>
        /// Trigger the item selected event
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The event args</param>
        private void ItemSelectedHandler(object sender, ItemSelectedEventArgs e)
        {
            if (itemSelectedHander != null)
            {
                itemSelectedHander(sender, e);
            }
        }

        private Button dropdownButton = null;
        private OptionList optionList = null;
        private bool optionListFlag = false;

        private Animation listOpenAni = null;
        private Animation listCloseAni = null;
        private CustomButton customButton;

        private EventHandler<ItemSelectedEventArgs> itemSelectedHander;

    }
}
