using FIOSharp;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace ProsperousAssistant.AuthForms
{
	public partial class ManageAPIKeysForm : Form
	{
		struct ApiKey
		{
			public string Name { get; }
			public string Key { get; }
			public ApiKey(string name, string key)
			{
				Name = name;
				Key = key;
			}
		}

		private FnarOracleDataSource oracle;
		private string password;
		private DataGridView dataGridView = new DataGridView();
		private BindingSource bindingSource;
		private BindingList<ApiKey> bindingList;

		public ManageAPIKeysForm(FnarOracleDataSource oracle, string password = null)
		{
			InitializeComponent();
			this.oracle = oracle;
			this.password = password;
			bindingList = new BindingList<ApiKey>();
			bindingSource = new BindingSource(bindingList, null);
		}

		private void OnLoad(object sender, EventArgs e)
		{
			if(password == null)
			{
				ConfirmPasswordForm passwordForm = new ConfirmPasswordForm(oracle);
				if(passwordForm.ShowDialog() == DialogResult.OK)
				{
					password = passwordForm.PasswordField.Text;
				}
				else
				{
					if (Modal)
					{
						DialogResult = DialogResult.Cancel;
					}
					else
					{
						Close();
					}
				}
				
			}

			SetupTable();
			
		}

		private void SetupTable()
		{
			Controls.Add(dataGridView);
			dataGridView.AutoGenerateColumns = false;
			dataGridView.AllowUserToAddRows = false;
			dataGridView.AllowUserToDeleteRows = false;
			dataGridView.ReadOnly = false;
			dataGridView.AllowUserToResizeRows = false;
			dataGridView.RowHeadersVisible = false;
			dataGridView.ShowCellToolTips = false;
			dataGridView.Size = new Size(506, 328);
			dataGridView.Location = new Point(12, 12);
			dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridView.MultiSelect = false;
			dataGridView.CellContentClick += CellClicked;
			dataGridView.DataSource = bindingSource;
			dataGridView.SelectionChanged += SelectionChanged;

			DataGridViewCheckBoxColumn selectedColumn = new DataGridViewCheckBoxColumn();
			selectedColumn.Name = "Login";
			selectedColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			selectedColumn.ReadOnly = false;
			selectedColumn.TrueValue = true;
			selectedColumn.FalseValue = false;
			dataGridView.Columns.Add(selectedColumn);

			

			DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
			nameColumn.DataPropertyName = "Name";
			nameColumn.Name = "Name";
			nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			dataGridView.Columns.Add(nameColumn);
			
			DataGridViewTextBoxColumn keyColumn = new DataGridViewTextBoxColumn();
			keyColumn.DataPropertyName = "Key";
			keyColumn.Name = "API Key";
			keyColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView.Columns.Add(keyColumn);





			/*add our test data
			for(int i = 0; i < 20; i++)
			{
				bindingList.Add(new ApiKey(i.ToString(), i.ToString() + "Key junk"));
			}*/

			ReloadKeys();
		}

		private void SelectionChanged(object sender, EventArgs e)
		{
			CopyButton.Enabled = dataGridView.SelectedRows.Count == 1;
			DeleteButton.Enabled = dataGridView.SelectedRows.Count > 0;
		}

		private void CellClicked(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex != 0) return;

			foreach(DataGridViewRow row in dataGridView.Rows)
			{
				if(row.Index != e.RowIndex)
				{
					row.Cells[0].Value = false;
				}
				else
				{
					Settings.APIKey = row.Cells[0].Value == null||(bool)row.Cells[0].Value ? bindingList[e.RowIndex].Key : "";
				}
			}
			Console.WriteLine(Settings.APIKey);
		}

		private void CreateKey(object sender, EventArgs e)
		{
			KeynameForm keynameForm = new KeynameForm();
			if(keynameForm.ShowDialog() == DialogResult.OK)
			{
				oracle.CreateAPIKey(keynameForm.KeyField.Text, password);
				ReloadKeys();
			}
		}

		private void DeleteKey(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 0) return;
			string key = bindingList[dataGridView.SelectedRows[0].Index].Key;
			oracle.DeleteApiKey(key, password);
			Guid guid;
			if (Guid.TryParse(Settings.APIKey, out guid) && Guid.Parse(key).Equals(guid))
			{
				Settings.APIKey = "";
			}
			ReloadKeys();
		}

		private void CopyKey(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 0) return;
			Clipboard.SetText(bindingList[dataGridView.SelectedRows[0].Index].Key);
		}

		private void ReloadKeys()
		{
			//actually load keys from the api
			dataGridView.ClearSelection();
			ApiKey[] keys = oracle.GetAPIKeys(password).Select(pair => new ApiKey(pair.name, pair.key)).ToArray();
			Guid normalizedKey = Guid.Empty;
			Guid.TryParse(Settings.APIKey, out normalizedKey);
			bindingList.Clear();
			for(int i = 0; i < keys.Count(); i++)
			{
				//we need to count our way to so we can set the checkbox on the appropriate row
				bindingList.Add(keys[i]);
				if (normalizedKey != Guid.Empty && normalizedKey.Equals(Guid.Parse(keys[i].Key)))
				{
					dataGridView.Rows[i].Cells[0].Value = true;
				}
			}
		}
	}
}
