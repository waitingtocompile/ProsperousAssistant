using ProsperousAssistant.ProductionModel;
using ProsperousAssistant.Util;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProsperousAssistant
{
	public partial class MainForm : Form
	{
		public CachedDataHelper DataHelper { get; }

		public ProfitEstimatorView ProfitEstimatorView { get; private set; }

		private List<ISavesState> savables = new List<ISavesState>();

		private ConcurrentQueue<ISavesState> queuedSaveOps = new ConcurrentQueue<ISavesState>();

		public MainForm(CachedDataHelper dataHelper)
		{
			DataHelper = dataHelper;
			InitializeComponent();
			PopulateTabs();
		}

		private void PopulateTabs()
		{
			ProfitEstimatorView = new ProfitEstimatorView(DataHelper);
			AddTab(ProfitEstimatorView, "Profit Estimator");

		}

		private void AddTab(UserControl control, string text)
		{
			TabPage newTabPage = new TabPage();
			newTabPage.Controls.Add(control);
			newTabPage.Text = text;
			control.Dock = DockStyle.Fill;
			TabbedView.TabPages.Add(newTabPage);
			if(control is ISavesState savable)
			{
				savables.Add(savable);
			}
		}

		public void QueueToSave(ISavesState savable)
		{
			queuedSaveOps.Enqueue(savable);
			if (!SaveStatesWorker.IsBusy)
			{
				SaveStatesWorker.RunWorkerAsync();
			}
		}

		public void QueueToSave(IEnumerable<ISavesState> savables)
		{
			foreach(var savable in savables)
			{
				queuedSaveOps.Enqueue(savable);
			}
			if (!SaveStatesWorker.IsBusy)
			{
				SaveStatesWorker.RunWorkerAsync();
			}
		}

		private void SaveStatesWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			while (!queuedSaveOps.IsEmpty)
			{
				//dequeue the first item and save it
				if(queuedSaveOps.TryDequeue(out ISavesState savable))
				{
					savable.SaveState();
				}
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			QueueToSave(savables);
		}
	}
}
