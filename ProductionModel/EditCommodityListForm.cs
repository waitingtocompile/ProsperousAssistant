using FIOSharp.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProsperousAssistant.ProductionModel
{
	public partial class EditCommodityListForm : Form
	{
		private class ListViewComparer : IComparer
		{
			public MaterialSortMode sortMode;
			public IDictionary<ListViewItem, Material> map;
			public int Compare(object x, object y)
			{
				if(x is ListViewItem firstItem && y is ListViewItem secondItem)
				{
					return sortMode.Compare(map[firstItem], map[secondItem]);
				}


				throw new NotImplementedException();
			}
		}

		public ICollection<Material> SelectedMaterials { get; }
		public ICollection<Material> AllMaterials { get; }

		private readonly BiDictionary<Material, ListViewItem> listViewItems = new BiDictionary<Material, ListViewItem>();

		private readonly ImageList imageList = new ImageList() { ImageSize = new Size(40, 40), ColorDepth = ColorDepth.Depth32Bit };

		private ListViewComparer SelectedSorter;
		private ListViewComparer UnselectedSorter;

		public Action<Material> OnItemSelected;
		public Action<Material> OnItemDeselected;
		
		public EditCommodityListForm(ICollection<Material> allMaterials, ICollection<Material> selectedMaterials = null)
		{
			SelectedMaterials = selectedMaterials ?? new List<Material>();
			AllMaterials = allMaterials;
			InitializeComponent();

			SelectedCommodities.SmallImageList = imageList;
			SelectedCommodities.LargeImageList = imageList;
			UnselectedCommodities.SmallImageList = imageList;
			UnselectedCommodities.LargeImageList = imageList;


			SelectedSorter = new ListViewComparer() { map = listViewItems.Reverse };
			UnselectedSorter = new ListViewComparer() { map = listViewItems.Reverse };
			SelectedCommodities.ListViewItemSorter = SelectedSorter;
			UnselectedCommodities.ListViewItemSorter = UnselectedSorter;
			

			SelectedSortModeSelector.DataSource = MaterialSortMode.ALL_MODES.Values.ToList();
			SelectedSortModeSelector.DisplayMember = "Name";
			UnselectedSortModeSelector.DataSource = MaterialSortMode.ALL_MODES.Values.ToList();
			UnselectedSortModeSelector.DisplayMember = "Name";


			RefreshFromList();
			
		}

		public EditCommodityListForm(CachedDataHelper cachedDataHelper, ICollection<Material> selectedMaterials = null)
			:this(cachedDataHelper.Materials, selectedMaterials) { }

		private int GetImageIndex(Material material)
		{
			if (!imageList.Images.ContainsKey(material.Ticker))
			{
				imageList.Images.Add(material.Ticker, ColorGroup.GetOrCreateIcon(material));
			}

			return imageList.Images.IndexOfKey(material.Ticker);
		}

		private ListViewItem GetItem(Material material)
		{
			if (!listViewItems.ContainsKey(material))
			{
				ListViewItem listViewItem = new ListViewItem
				{
					Text = material.ReadableName,
					ImageIndex = GetImageIndex(material)
				};
				listViewItems.Add(material, listViewItem);
			}

			return listViewItems[material];
			
		}

		private void SelectMaterial(Material material)
		{
			ListViewItem foundItem = GetItem(material);

			if (UnselectedCommodities.Items.Contains(foundItem))
			{
				UnselectedCommodities.Items.Remove(foundItem);
			}

			if (!SelectedCommodities.Items.Contains(foundItem))
			{
				SelectedCommodities.BeginUpdate();
				SelectedCommodities.Items.Add(foundItem);
				SelectedCommodities.Sort();
				SelectedCommodities.EndUpdate();
			}

			

			if (!SelectedMaterials.Contains(material))
			{
				SelectedMaterials.Add(material);
			}

			OnItemSelected(material);
		}

		private void UnselectMaterial(Material material)
		{
			ListViewItem foundItem = GetItem(material);

			if (SelectedCommodities.Items.Contains(foundItem))
			{
				SelectedCommodities.Items.Remove(foundItem);
			}

			if (!UnselectedCommodities.Items.Contains(foundItem))
			{
				UnselectedCommodities.BeginUpdate();
				UnselectedCommodities.Items.Add(foundItem);
				UnselectedCommodities.Sort();
				UnselectedCommodities.EndUpdate();
			}

			

			if (SelectedMaterials.Contains(material))
			{
				SelectedMaterials.Remove(material);
			}

			OnItemDeselected(material);
		}

		public void RefreshFromList()
		{
			RefreshSelectedFromList();
			RefreshUnselectedFromList();
			
		}

		public void RefreshSelectedFromList()
		{
			SelectedCommodities.BeginUpdate();
			SelectedCommodities.Clear();
			List<Material> materials = SelectedMaterials.ToList();
			foreach (Material material in materials)
			{
				SelectedCommodities.Items.Add(GetItem(material));
			}
			UnselectedCommodities.Sort();
			SelectedCommodities.EndUpdate();
		}

		public void RefreshUnselectedFromList()
		{
			UnselectedCommodities.BeginUpdate();
			UnselectedCommodities.Clear();
			List<Material> materials = AllMaterials.Where(mat => !SelectedMaterials.Contains(mat)).ToList();
			foreach (Material material in materials)
			{
				UnselectedCommodities.Items.Add(GetItem(material));
			}
			UnselectedCommodities.Sort();
			UnselectedCommodities.EndUpdate();
		}

		private void OnClosing(object sender, FormClosingEventArgs e)
		{
			Hide();
			e.Cancel = true;//so that the form isn't disposed and can be re-used
		}

		private void SelectedSortModeChanged(object sender, EventArgs e)
		{
			SelectedSorter.sortMode = (MaterialSortMode)SelectedSortModeSelector.SelectedItem;

			SelectedCommodities.Sort();
		}

		private void UnselectedSortModeChanged(object sender, EventArgs e)
		{
			UnselectedSorter.sortMode = (MaterialSortMode)UnselectedSortModeSelector.SelectedItem;
			UnselectedCommodities.Sort();
		}

		private void AddSelectedItems(object sender, EventArgs e)
		{
			if (UnselectedCommodities.SelectedItems.Count == 0) return;
			foreach (ListViewItem item in UnselectedCommodities.SelectedItems)
			{
				SelectMaterial(listViewItems.Reverse[item]);
			}
			UnselectedCommodities.SelectedItems.Clear();
		}

		private void RemoveSelectedItems(object sender, EventArgs e)
		{
			if (SelectedCommodities.SelectedItems.Count == 0) return;
			foreach (ListViewItem item in SelectedCommodities.SelectedItems)
			{
				UnselectMaterial(listViewItems.Reverse[item]);
			}
			SelectedCommodities.SelectedItems.Clear();

			
		}

		private void SelectedCommodities_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewHitTestInfo info = SelectedCommodities.HitTest(e.X, e.Y);
			ListViewItem item = info.Item;
			if(item != null)
			{
				UnselectMaterial(listViewItems.Reverse[item]);
			}
		}

		private void UnselectedCommodities_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewHitTestInfo info = UnselectedCommodities.HitTest(e.X, e.Y);
			ListViewItem item = info.Item;
			if (item != null)
			{
				SelectMaterial(listViewItems.Reverse[item]);
			}
		}
	}
}
