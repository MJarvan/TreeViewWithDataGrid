using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TreeViewWithDataGrid
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App:Application
	{
		private void OnAppStartup(object sender,StartupEventArgs e)
		{
			Window window = new MainWindow();
			window.Tag = e.Args;
			window.Show();
		}
	}
}
