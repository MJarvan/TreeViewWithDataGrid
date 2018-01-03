using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeViewWithDataGrid
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow:Window
	{
		public List<string> command;
		public MainWindow()
		{
			InitializeComponent();
			ShowTreeView();
			ShowDataGrid();
		}

		private void Window_Loaded(object sender,RoutedEventArgs e)
		{
			CollapsedDGR();
			string[] s = this.Tag as string[];
			command = s.ToList();
			try
			{
				System.Diagnostics.Process.Start(@"C:\Users\admin\Documents\Visual Studio 2015\Projects\TreeDemo\TreeDemo\bin\Debug\TreeDemo.exe");    //调用该命令，在程序启动时打开Excel程序
			}
			catch
			{
			}
		}

		/// <summary>
		/// 读取树数据
		/// </summary>
		private void ShowTreeView()
		{
			List<PropertyNodeItem> itemList = new List<PropertyNodeItem>();
			PropertyNodeItem node1 = new PropertyNodeItem()
			{
				ID = 1,
				FatherID = 0,
				DisplayName = "Node No.1",
				Name = "This is the discription of Node1. This is a folder.",
				IsExpanded = false
			};

			PropertyNodeItem node1tag1 = new PropertyNodeItem()
			{
				ID = 2,
				FatherID = 1,
				DisplayName = "Tag No.1",
				Name = "This is the discription of Tag 1. This is a tag.",
				IsExpanded = false
			};
			node1.Children.Add(node1tag1);

			PropertyNodeItem node1tag2 = new PropertyNodeItem()
			{
				ID = 3,
				FatherID = 1,
				DisplayName = "Tag No.2",
				Name = "This is the discription of Tag 2. This is a tag.",
				IsExpanded = false
			};
			node1.Children.Add(node1tag2);
			itemList.Add(node1);

			PropertyNodeItem node2 = new PropertyNodeItem()
			{
				ID = 4,
				FatherID = 0,
				DisplayName = "Node No.2",
				Name = "This is the discription of Node 2. This is a folder.",
				IsExpanded = false
			};

			PropertyNodeItem node2tag3 = new PropertyNodeItem()
			{
				ID = 5,
				FatherID = 4,
				DisplayName = "Tag No.3",
				Name = "This is the discription of Tag 3. This is a tag.",
				IsExpanded = false
			};
			node2.Children.Add(node2tag3);

			PropertyNodeItem node2tag4 = new PropertyNodeItem()
			{
				ID = 6,
				FatherID = 4,
				DisplayName = "Tag No.4",
				Name = "This is the discription of Tag 4. This is a tag.",
				IsExpanded = false
			};
			node2.Children.Add(node2tag4);
			itemList.Add(node2);

			PropertyNodeItem node1tag1tag1 = new PropertyNodeItem()
			{
				ID = 7,
				FatherID = 2,
				DisplayName = "TagTag No.1",
				Name = "This is the discription of TagTag 1. This is a tag.",
				IsExpanded = false
			};
			node1tag1.Children.Add(node1tag1tag1);

			PropertyNodeItem node1tag1tag2 = new PropertyNodeItem()
			{
				ID = 8,
				FatherID = 2,
				DisplayName = "TagTag No.2",
				Name = "This is the discription of TagTag 2. This is a tag.",
				IsExpanded = false
			};
			node1tag1.Children.Add(node1tag1tag2);

			PropertyNodeItem node1tag1tag1tag1 = new PropertyNodeItem()
			{
				ID = 9,
				FatherID = 7,
				DisplayName = "TagTagTag No.1",
				Name = "This is the discription of TagTagTag 1. This is a tag.",
				IsExpanded = false
			};
			node1tag1tag1.Children.Add(node1tag1tag1tag1);

			PropertyNodeItem node1tag1tag1tag2 = new PropertyNodeItem()
			{
				ID = 10,
				FatherID = 7,
				DisplayName = "TagTagTag No.2",
				Name = "This is the discription of TagTagTag 2. This is a tag.",
				IsExpanded = false
			};
			node1tag1tag1.Children.Add(node1tag1tag1tag2);

			this.treeview.ItemsSource = itemList;
		}

		/// <summary>
		/// 读取DG数据
		/// </summary>
		private void ShowDataGrid()
		{
			DataTable dt = new DataTable("table");
			foreach(DataGridColumn dgc in datagrid.Columns)
			{
				dt.Columns.Add(dgc.Header.ToString());
			}
			List<PropertyNodeItem> list = this.treeview.ItemsSource as List<PropertyNodeItem>;
			dt = LoadPropertyNodeItem(list,dt);
			datagrid.ItemsSource = dt.DefaultView;
		}

		/// <summary>
		/// 遍历树加载DG
		/// </summary>
		/// <param name="list"></param>
		/// <param name="dt"></param>
		/// <returns></returns>
		private DataTable LoadPropertyNodeItem(List<PropertyNodeItem> list,DataTable dt)
		{
			DataTable returndt = dt;

			foreach(PropertyNodeItem pni in list)
			{
				DataRow dr = returndt.NewRow();
				dr["FatherID"] = pni.FatherID;
				dr["ID"] = pni.ID;
				dr["DisplayName"] = pni.DisplayName;
				dr["Name"] = pni.Name;
				returndt.Rows.Add(dr);
				if(pni.Children.Count != 0)
				{
					returndt = LoadPropertyNodeItem(pni.Children,returndt);
				}
			}

			return returndt;
		}

		/// <summary>
		/// 初始化添加tvi事件
		/// </summary>
		private void CollapsedDGR()
		{
			//节点收缩
			//datagrid.UpdateLayout();
			//for(int i = 0;i < datagrid.ItemContainerGenerator.Items.Count;i++)
			//{
			//	DataGridRow dgv = (DataGridRow)datagrid.ItemContainerGenerator.ContainerFromIndex(i);
			//	if(dgv == null)
			//	{
			//		datagrid.UpdateLayout();
			//		datagrid.ScrollIntoView(datagrid.Items[i]);
			//		dgv = (DataGridRow)datagrid.ItemContainerGenerator.ContainerFromIndex(i);
			//	}
			//	DataRow dr = (dgv.Item as DataRowView).Row;
			//	if(Convert.ToInt32(dr["FatherID"]) != 0)
			//	{
			//		dgv.Visibility = Visibility.Collapsed;
			//	}
			//}

			//节点展开
			treeview.UpdateLayout();
			foreach(var item in treeview.Items)
			{
				TreeViewItem tvi = treeview.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
				tvi.ExpandSubtree();
			}
			SetNodeDPVisible(treeview);
		}

		/// <summary>
		/// 节点展开
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLoaded(object sender,RoutedEventArgs e)
		{
			TreeViewItem tvi = sender as TreeViewItem;
			PropertyNodeItem pni = tvi.DataContext as PropertyNodeItem;
			SelectDataGridRow(pni,null,pni.Visibility);

			e.Handled = true;
		}

		/// <summary>
		/// 遍历树添加事件
		/// </summary>
		/// <param name="control"></param>
		private void SetNodeDPVisible(ItemsControl control)
		{
			if(control != null)
			{
				foreach(object item in control.Items)
				{
					TreeViewItem treeItem = control.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
					if(treeItem == null)
					{
						control.UpdateLayout();
						treeItem = control.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
					}

					treeItem.IsVisibleChanged += TreeItem_IsVisibleChanged;

					if(treeItem != null && treeItem.HasItems)
					{
						SetNodeDPVisible(treeItem as ItemsControl);
					}
				}
			}
		}

		/// <summary>
		/// TVI可视状态改变
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeItem_IsVisibleChanged(object sender,DependencyPropertyChangedEventArgs e)
		{
			TreeViewItem tvi = sender as TreeViewItem;
			PropertyNodeItem pni = tvi.DataContext as PropertyNodeItem;
			if(tvi.IsVisible)
			{
				SelectDataGridRow(pni,null,Visibility.Visible);
			}
			else
			{
				SelectDataGridRow(pni,null,Visibility.Collapsed);
			}
		}

		/// <summary>
		/// 被选择要选中对应的项
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnSelected(object sender,RoutedEventArgs e)
		{
			if(sender.GetType() == typeof(TreeViewItem))
			{
				TreeViewItem tvi = sender as TreeViewItem;
				PropertyNodeItem pni = tvi.DataContext as PropertyNodeItem;
				SelectDataGridRow(pni,tvi.IsSelected,Visibility.Visible);
				e.Handled = true;
			}
			else if(sender.GetType() == typeof(DataGridRow))
			{
				DataGridRow dgv = sender as DataGridRow;
				DataRow dr = (dgv.Item as DataRowView).Row;
				SelectTreeViewItem(treeview,dr);
			}
		}

		/// <summary>
		/// /选中DGR
		/// </summary>
		/// <param name="pni"></param>
		/// <param name="check"></param>
		/// <param name="visibile"></param>
		private void SelectDataGridRow(PropertyNodeItem pni,bool? check,Visibility visibile)
		{
			for(int i = 0;i < datagrid.ItemContainerGenerator.Items.Count;i++)
			{
				DataGridRow dgv = (DataGridRow)datagrid.ItemContainerGenerator.ContainerFromIndex(i);
				if(dgv == null)
				{
					datagrid.UpdateLayout();
					datagrid.ScrollIntoView(datagrid.Items[i]);
					dgv = (DataGridRow)datagrid.ItemContainerGenerator.ContainerFromIndex(i);
				}
				DataRow dr = (dgv.Item as DataRowView).Row;

				if(pni.ID == Convert.ToInt32(dr["ID"]))
				{
					dgv.Visibility = visibile;

					if(visibile == Visibility.Visible)
					{
						if(check == true)
						{
							dgv.IsSelected = true;
						}
						else if(check == false)
						{
							dgv.IsSelected = false;
						}
					}
				}
			}
		}

		/// <summary>
		/// 选中TVI
		/// </summary>
		/// <param name="control"></param>
		/// <param name="dr"></param>
		private void SelectTreeViewItem(ItemsControl control,DataRow dr)
		{
			if(control != null)
			{
				foreach(object item in control.Items)
				{
					TreeViewItem treeItem = control.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
					if(treeItem != null)
					{
						PropertyNodeItem pni = treeItem.DataContext as PropertyNodeItem;
						if(pni.ID == Convert.ToInt32(dr["ID"]))
						{
							treeItem.IsSelected = true;
							return;
						}
						else if(treeItem.HasItems)
						{
							if(treeItem.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
							{
								treeItem.UpdateLayout();
							}

							SelectTreeViewItem(treeItem as ItemsControl,dr);
						}
					}
				}
			}
		}
	}
	public class PropertyNodeItem:INotifyPropertyChanged
	{
		public int ID
		{
			get; set;
		}
		public int FatherID
		{
			get; set;
		}
		public string DisplayName
		{
			get; set;
		}
		public string Name
		{
			get; set;
		}

		private bool isExpanded;
		public bool IsExpanded
		{
			get
			{
				return isExpanded;
			}
			set
			{
				isExpanded = value;
				OnPropertyChanged("IsExpanded");
			}
		}

		private Visibility visibility;
		public Visibility Visibility
		{
			get
			{
				return visibility;
			}
			set
			{
				visibility = value;
				OnPropertyChanged("Visibility");
			}
		}

		public List<PropertyNodeItem> Children
		{
			get; set;
		}
		public PropertyNodeItem()
		{
			Children = new List<PropertyNodeItem>();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected internal virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
		}
	}
}
