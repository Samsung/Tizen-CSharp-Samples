//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Utils
{
    /// <summary>
    /// Class that allows binding to task.
    /// After task is completed it notify view about it.
    /// </summary>
    /// <typeparam name="T">Type of the result of the task.</typeparam>
    public class NotificationTask<T> : INotifyPropertyChanged
    {
        #region properties

        /// <summary>
        /// Task that will be executed.
        /// </summary>
        public Task<T> Task { get; }

        /// <summary>
        /// Gets result of the task.
        /// </summary>
        public T Result => Task.Status == TaskStatus.RanToCompletion ? Task.Result : default(T);

        /// <summary>
        /// Gets status of the task.
        /// </summary>
        public TaskStatus Status => Task.Status;

        /// <summary>
        /// Indicates if task is completed.
        /// </summary>
        public bool IsCompleted => Task.IsCompleted;

        /// <summary>
        /// Indicates if task is completed successfully.
        /// </summary>
        public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;

        /// <summary>
        /// Indicates if task is not completed.
        /// </summary>
        public bool IsNotCompleted => !Task.IsCompleted;

        /// <summary>
        /// Indicates if task is cancelled.
        /// </summary>
        public bool IsCanceled => Task.IsCanceled;

        /// <summary>
        /// Indicates if task is faulted.
        /// </summary>
        public bool IsFaulted => Task.IsFaulted;

        /// <summary>
        /// Gets all exceptions from execution of the task.
        /// </summary>
        public AggregateException Exception => Task.Exception;

        /// <summary>
        /// Gets current exception from execution of the task.
        /// </summary>
        public Exception InnerException => Exception?.InnerException;

        /// <summary>
        /// Event that informs view about property change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        /// <param name="task">Task to be executed.</param>
        public NotificationTask(Task<T> task)
        {
            Task = task;

            if (!task.IsCompleted)
            {
                _ = ExecuteTaskAsync(task);
            }
        }

        /// <summary>
        /// Executes task and notifies view about changes of its properties.
        /// </summary>
        /// <param name="task">Task to be executed.</param>
        /// <returns>Task that was executed.</returns>
        private async Task ExecuteTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            var propertyChanged = PropertyChanged;
            if (propertyChanged == null)
            {
                return;
            }

            propertyChanged(this, new PropertyChangedEventArgs(nameof(Status)));
            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCompleted)));
            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsNotCompleted)));

            if (Task.IsCanceled)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCanceled)));
            }
            else if (Task.IsFaulted)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsFaulted)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(Exception)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(InnerException)));
            }
            else
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsSuccessfullyCompleted)));
                propertyChanged(this, new PropertyChangedEventArgs(nameof(Result)));
            }
        }

        /// <summary>
        /// PropertyChanged event invoker.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}