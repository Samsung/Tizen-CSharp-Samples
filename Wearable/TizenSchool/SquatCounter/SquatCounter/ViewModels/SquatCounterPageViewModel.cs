using SquatCounter.Services;
using System;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace SquatCounter.ViewModels
{
	public class SquatCounterPageViewModel : ViewModelBase
    {
        private SquatCounterService _squatService;
		private int _squatsCount;

		private Timer _timer;
		private int _seconds;
		private string _time;

		private bool _isCounting;

		public int SquatsCount
		{
			get => _squatsCount;
			set => SetProperty(ref _squatsCount, value);
		}

		public string Time
		{
			get => _time;
			set => SetProperty(ref _time, value);
		}

		public bool IsCounting
		{
			get => _isCounting;
			set => SetProperty(ref _isCounting, value);
		}

		public ICommand ChangeServiceStateCommand { get; }
		public ICommand ResetCommand { get; }

		public SquatCounterPageViewModel()
		{
			_seconds = 0;
			_time = "00:00";

			_squatService = new SquatCounterService();
			_squatService.SquatsUpdated += ExecuteSquatsUpdatedCallBack;

			_timer = new Timer(1000);
			_timer.Elapsed += TimerTick;
			_timer.Start();

			ChangeServiceStateCommand = new Command(ExecuteChnageServiceStateCommand);
			ResetCommand = new Command(ExecuteResetCommand);

			if(Application.Current.MainPage is NavigationPage navigationPage)
			{
				navigationPage.Popped += OnPagePopped;
			}
		}

		private void OnPagePopped(object sender, NavigationEventArgs e)
		{
			_timer.Elapsed -= TimerTick;
			_timer.Close();

			_squatService.SquatsUpdated -= ExecuteSquatsUpdatedCallBack;
			_squatService.Dispose();

			if (Application.Current.MainPage is NavigationPage navigationPage)
			{
				navigationPage.Popped -= OnPagePopped;
			}
		}

		private void ExecuteResetCommand()
		{
			_squatService.Reset();
			Time = "00:00";
			_seconds = 0;
		}

		private void ExecuteChnageServiceStateCommand()
		{
			if(IsCounting)
			{
				_timer.Stop();
				_squatService.Stop();
			}
			else
			{
				_timer.Start();
				_squatService.Start();
			}

			IsCounting = !IsCounting;
		}

		private void TimerTick(object sender, EventArgs e)
		{
			_seconds++;
			Time = TimeSpan.FromSeconds(_seconds).ToString("mm\\:ss");
		}

		private void ExecuteSquatsUpdatedCallBack(object sender, int squatsCount)
		{
			SquatsCount = squatsCount;
		}
	}
}
