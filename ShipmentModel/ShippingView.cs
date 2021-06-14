using System.Windows.Forms;

namespace ProsperousAssistant.ShipmentModel
{
	public partial class ShippingView : UserControl, SavesState
	{
		private ShipmentCalculator Calculator { get; }

		public CachedDataHelper DataHelper { get; }

		public string ShipmentRowsDir => Settings.StoragePath + "\\shipping calculator\\row presets";
		public string ShipmentSettingsDir => Settings.StoragePath + "\\shipping calculator\\settings presets";


		public ShippingView(CachedDataHelper dataHelper)
		{
			InitializeComponent();
			DataHelper = dataHelper;
			Calculator = new ShipmentCalculator(dataHelper);
		}

		public void SaveState()
		{
			throw new System.NotImplementedException();
		}
	}
}
