using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using System.Threading.Tasks;
using Tizen.Security;
using System.Collections.ObjectModel;

namespace PhotoWatch
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        DateTime _time;
        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }

        bool _isNormalMode = true;
        public bool IsNormalMode
        {
            get => _isNormalMode;
            set
            {
                _isNormalMode = value;
                OnPropertyChanged();
            }
        }

        bool _settingEnabled;
        public bool SettingEnabled
        {
            get => _settingEnabled;
            set
            {
                _settingEnabled = value;
                OnPropertyChanged();
            }
        }

        public Command EnterSettingCommand => new Command(EnterSetting);

        void EnterSetting()
        {
            SettingEnabled = true;
            var setting = new SettingView();
            SettingView = setting;
            RotarayFocusObject = setting.RotaryFocusObject;
            ActionButton = new ActionButtonItem
            {
                Text = "OK",
                Command = ExitSettingCommand
            };
        }

        public Command ExitSettingCommand => new Command(ExitSetting);

        void ExitSetting()
        {
            SettingEnabled = false;
            SettingView = null;
            RotarayFocusObject = null;
            ActionButton.IsVisible = false;
            UpdateBackgroundImage();
            SaveToPreference();
        }

        public Command PickPhotoCommand => new Command(PickPhoto);

        async void PickPhoto()
        {
            try
            {
                var result = await PickAppControl();
                foreach (var path in result)
                {
                    BackgroundItems.Add(MakeBackgroundItem(path));
                }
            }
            catch (Exception err)
            {
                System.Console.WriteLine($"Fail to pick {err.Message}");
            }

        }

        View _settingView;
        public View SettingView
        {
            get => _settingView;
            set
            {
                _settingView = value;
                OnPropertyChanged();
            }
        }

        string _currentBackground;
        public string CurrentBackground
        {
            get => _currentBackground;
            set
            {
                _currentBackground = value;
                OnPropertyChanged();
            }
        }

        IRotaryFocusable _rotarayFocusObject;
        public IRotaryFocusable RotarayFocusObject
        {
            get => _rotarayFocusObject;
            set
            {
                _rotarayFocusObject = value;
                OnPropertyChanged();
            }
        }

        ActionButtonItem _actionButtonItem;
        public ActionButtonItem ActionButton
        {
            get => _actionButtonItem;
            set
            {
                _actionButtonItem = value;
                OnPropertyChanged();
            }
        }
        public IList<BackgroundImageModel> BackgroundItems { get; } = new ObservableCollection<BackgroundImageModel>();

        public Command UpdateBackgroundImageCommand => new Command(UpdateBackgroundImage);

        public event PropertyChangedEventHandler PropertyChanged;

        int _currentBackgroundIndex = 0;
        public void UpdateBackgroundImage()
        {
            if (BackgroundItems.Count < 1)
            {
                CurrentBackground = "default.png";
                return;
            }
            _currentBackgroundIndex = (++_currentBackgroundIndex) % BackgroundItems.Count;
            CurrentBackground = BackgroundItems[_currentBackgroundIndex].Path;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RemoveBackgroundItem(string path)
        {
            var found = BackgroundItems.FirstOrDefault((item) => item.Path == path);
            if (found != null)
            {
                BackgroundItems.Remove(found);
            }
        }

        BackgroundImageModel MakeBackgroundItem(string path)
        {
            var displayName = System.IO.Path.GetFileName(path);
            if (displayName.Length > 10)
            {
                displayName = displayName.Substring(0, 10) + "...";
            }
            return new BackgroundImageModel
            {
                Path = path,
                DisplayName = displayName,
                DeleteCommand = new Command<string>(RemoveBackgroundItem)
            };
        }

        public void LoadFromPreference()
        {
            BackgroundItems.Clear();
            foreach(var key in Tizen.Applications.Preference.Keys)
            {
                var path = Tizen.Applications.Preference.Get<string>(key);
                BackgroundItems.Add(MakeBackgroundItem(path));
            }
        }
        public void SaveToPreference()
        {
            int index = 0;
            Tizen.Applications.Preference.RemoveAll();
            foreach (var bg in BackgroundItems)
            {
                Tizen.Applications.Preference.Set($"bg{index}", bg.Path);
                index++;
            }
        }

        static async Task<IList<string>> PickAppControl()
        {
            if (PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/mediastorage") != CheckResult.Allow)
            {
                await RequestPermission();
            }

            TaskCompletionSource<IList<string>> tcs = new TaskCompletionSource<IList<string>>();
            Tizen.Applications.AppControl appControl = new Tizen.Applications.AppControl
            {
                Operation = Tizen.Applications.AppControlOperations.Pick,
                Mime = "image/*"
            };
            appControl.ExtraData.Add(Tizen.Applications.AppControlData.SectionMode, "multiple");
            try
            {
                Tizen.Applications.AppControl.SendLaunchRequest(appControl, (req, res, result) =>
                {
                    if (result == Tizen.Applications.AppControlReplyResult.Succeeded)
                    {
                        var list = res.ExtraData.Get<IEnumerable<string>>("http://tizen.org/appcontrol/data/selected").ToList();
                        tcs.TrySetResult(list);
                    }
                    else
                    {
                        tcs.TrySetException(new InvalidOperationException("Fail to pick"));
                    }
                });
            }
            catch (Exception err)
            {
                System.Console.WriteLine($"Error : {err.Message}");
                tcs.TrySetException(err);
            }
            return await tcs.Task;
        }

        static Task RequestPermission()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            var response = PrivacyPrivilegeManager.GetResponseContext("http://tizen.org/privilege/mediastorage");
            PrivacyPrivilegeManager.ResponseContext target;
            response.TryGetTarget(out target);
            target.ResponseFetched += (s, e) =>
            {
                tcs.SetResult(true);
            };
            PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/mediastorage");

            return tcs.Task;
        }
    }
}