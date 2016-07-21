﻿using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace HDT.Plugins.StatsConverter.Controls
{
	public partial class PluginMenu : MenuItem
	{
		public PluginMenu()
		{
			InitializeComponent();
		}

		// TODO: can probably do this better, instead of creating new objects each time

		private void MenuItem_Export_Click(object sender, RoutedEventArgs e)
		{
			Hearthstone_Deck_Tracker.API.Core.MainWindow.ShowMetroDialogAsync(new ExportDialog());
		}

		private void MenuItem_Settings_Click(object sender, RoutedEventArgs e)
		{
		}
	}
}