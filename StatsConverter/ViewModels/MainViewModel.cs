﻿using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HDT.Plugins.StatsConverter.Utils;

namespace HDT.Plugins.StatsConverter.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private Dictionary<string, ViewModelBase> _viewModels = new Dictionary<string, ViewModelBase> {
			{ Strings.NavSettings, new SettingsViewModel() },
			{ Strings.NavImport, new ImportViewModel() },
			{ Strings.NavExport, new ExportViewModel() }
		};

		private string _defaultViewModel = Strings.NavExport;

		private string _contentTitle;

		public string ContentTitle
		{
			get { return _contentTitle; }
			set { Set(() => ContentTitle, ref _contentTitle, value); }
		}

		private ViewModelBase _contentViewModel;

		public ViewModelBase ContentViewModel
		{
			get { return _contentViewModel; }
			set { Set(() => ContentViewModel, ref _contentViewModel, value); }
		}

		public RelayCommand<string> NavigateCommand { get; private set; }

		public MainViewModel()
		{
			NavigateCommand = new RelayCommand<string>(x => OnNavigation(x));
			OnNavigation(_defaultViewModel);
		}

		private void OnNavigation(string location)
		{
			var key = location.ToLower();
			if (_viewModels.ContainsKey(key))
			{
				// change if different to current
				if (ContentViewModel != _viewModels[key])
				{
					ContentViewModel = _viewModels[key];
					if (key.Length > 2)
						ContentTitle = key.Substring(0, 1).ToUpper() + key.Substring(1);
				}
			}
		}
	}
}