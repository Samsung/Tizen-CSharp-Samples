/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Clock.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace Clock.Stopwatch
{
    /// <summary>
    /// The stopwatch page, the class is defined in 2 files
    /// One is for UI part, one is for logical process,
    /// This one is for logical process.
    /// </summary>
    public partial class StopwatchPage : ContentPage
    {
        private static System.Diagnostics.Stopwatch stopWatch;
        private SWTimerState tmState = SWTimerState.init;

        public void ShowPage()
        {
            if (mainView == null)
            {
                Content = CreateStopWatchPage();
            }

            if (stopWatch == null)
            {
                stopWatch = new System.Diagnostics.Stopwatch();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopwatchPage"/> class.
        /// </summary>
        public StopwatchPage()
        {
            //this.SetBinding(ContentPage.TitleProperty, "Title");
            Title = "Stopwatch";
            Icon = "maintabbed/clock_tabs_ic_stopwatch.png";
            Content = new EmptyPage();
        }

        /// <summary>
        /// Updates timer information every millisecond after the timer starts
        /// </summary>
        /// <returns> false: stop timer; true: continue the timer </returns>
        private bool PanelTimerFunc()
        {
            if (tmState == SWTimerState.started)
            {
                UpdatePanelText(stopWatch.Elapsed);
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Start the stopwatch timer.
        /// </summary>
        public void CreatePanelTimer()
        {
            stopWatch.Start();
            tmState = SWTimerState.started;
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 1), PanelTimerFunc);
        }

        /// <summary>
        /// Stop the stopwatch timer.
        /// </summary>
        public void StopPanelTimer()
        {
            stopWatch.Stop();
            tmState = SWTimerState.stopped;
        }

        /// <summary>
        /// Reset the stopwatch timer.
        /// </summary>
        public void ResetPanelTimer()
        {
            stopWatch.Reset();
            tmState = SWTimerState.init;
        }

        /// <summary>
        /// The lap time info of list view.
        /// </summary>
        public class WatchListItem
        {
            /// <summary>
            /// The one based index number
            /// </summary>
            public string No { get; set; }
            /// <summary>
            /// The elapsed time since the timer has been started
            /// </summary>
            public string SplitTime { get; set; }
            /// <summary>
            /// The time between splits
            /// </summary>
            public string LapTime { get; set; }
        }

        /// <summary>
        /// The LapListObservableCollection class which represents a data collection that provides notifications when items get added, removed.
        /// </summary>
        public class LapListObservableCollection : List<WatchListItem>, INotifyCollectionChanged
        {
            public LapListObservableCollection()
            {
            }

            /// <summary>
            /// The event that notifies dynamic changes, such as when items get added or removed
            /// </summary>
            public event NotifyCollectionChangedEventHandler CollectionChanged;

            /// <summary>
            /// Adds WatchListItem object to a list
            /// </summary>
            /// <param name="item">WatchListItem object to add</param>
            /// <seealso cref="WatchListItem">
            public new void Add(WatchListItem item)
            {
                base.Add(item);
                CollectionChanged?.Invoke(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }

            /// <summary>
            /// Inserts a collection of WatchListItem in the list at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
            /// <param name="collection">The collection of WatchListItems whose elements should be inserted into the LapListObservableCollection</param>
            public new void InsertRange(int index, IEnumerable<WatchListItem> collection)
            {
                base.InsertRange(index, collection);
                CollectionChanged?.Invoke(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<WatchListItem>(collection), index));
            }

            /// <summary>
            /// Removes the specified WatchListItem object from the list.
            /// </summary>
            /// <param name="item">WatchListItem object to remove from the LapListObservableCollection</param>
            /// <seealso cref="WatchListItem">
            public new void Remove(WatchListItem item)
            {
                base.Remove(item);
                CollectionChanged?.Invoke(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            }

            /// <summary>
            /// Removes all elements from the list.
            /// </summary>
            public new void Clear()
            {
                base.Clear();
                CollectionChanged?.Invoke(this,
                    new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
    }

    internal enum SWTimerState
    {
        init,
        started,
        stopped
    }
}
