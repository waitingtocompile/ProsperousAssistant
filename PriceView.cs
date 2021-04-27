using FIOSharp;
using FIOSharp.Data;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProsperousAssistant
{
	public partial class PriceView : Form
	{
		
		private static string[] visibleColumns =
		{
			"Ticker",
			"Price",
			"Ask",
			"Bid",
			"Supply",
			"Demand"
		};

		private FnarOracleDataSource oracleClient;
		public ExchangeData ExchangeData { get; private set; }

		private DataGridView dataGridView = new DataGridView();
		private BindingSource bindingSource = new BindingSource();
		//todo: replace this with a more flexible approach
		private List<int> positiveCurrencyColumns = new List<int>();

		public PriceView(FnarOracleDataSource oracleClient, ExchangeData exchangeData)
		{
			this.oracleClient = oracleClient;
			ExchangeData = exchangeData;
			InitializeComponent();
			this.Load += OnLoad;
		}


		private void OnLoad(object sender, System.EventArgs e)
		{
			SetupTable();
			this.AutoSize = true;
		}

		private void SetupTable()
		{
			this.Controls.Add(dataGridView);
			dataGridView.AutoGenerateColumns = false;
			dataGridView.AllowUserToAddRows = false;
			dataGridView.AllowUserToDeleteRows = false;
			dataGridView.ReadOnly = true;
			dataGridView.AllowUserToResizeRows = false;

			//bindingSource.DataSource = ExchangeData.ExchangeEntries.Values;
			
			foreach(ExchangeEntry entry in ExchangeData.ExchangeEntries.Values)
			{
				bindingSource.Add(entry);
			}
			dataGridView.DataSource = bindingSource;
			addColumn("Material", "Ticker");
			addColumn("Price", true);
			addColumn("Ask", true);
			addColumn("Bid", true);
			addColumn("Supply");
			addColumn("Demand");

			dataGridView.Dock = DockStyle.Fill;
			dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridView.CellFormatting += OnCellFormatting;
		}

		private void ReloadDataBinding()
		{
			//oracleClient.GetAllEntriesForExchange(exchange, )
		}

		private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if(positiveCurrencyColumns.Contains(e.ColumnIndex) && e.Value != null)
			{
				decimal number = 0;
				if(decimal.TryParse(e.Value.ToString(), out number))
				{
					if(number > 0)
					{
						//limit to 2 decimals and add our currency code
						e.Value = number.ToString("0.00") + ExchangeData.Currency;
					}
					else
					{
						//we're handling "null" values
						e.Value = "--";
					}

					e.FormattingApplied = true;
				}
			}
		}

		private void addColumn(string name, bool isCurrency = false)
		{
			addColumn(name, name, isCurrency);
		}

		private void addColumn(string title, string propertyName, bool isCurrency = false)
		{
			DataGridViewColumn column = new DataGridViewTextBoxColumn();
			column.DataPropertyName = propertyName;
			column.Name = title;
			if (isCurrency) {
				positiveCurrencyColumns.Add(dataGridView.Columns.Count);
			}
			dataGridView.Columns.Add(column);
		}

		private void PriceView_Load(object sender, System.EventArgs e)
		{

		}
	}
}
