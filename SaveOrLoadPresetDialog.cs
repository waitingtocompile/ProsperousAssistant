using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProsperousAssistant
{
	public partial class SaveOrLoadPresetDialog : Form
	{
		public bool IsLoad { get; }
		public string TargetDirectory => DirectoryInfo.FullName;
		public FileSystemWatcher DirectoryWatcher { get; }
		public DirectoryInfo DirectoryInfo { get; }

		public FileStream LoadedFile { get; private set; } = null;

		private Func<string, bool> OperateOnFile;

		public SaveOrLoadPresetDialog(string targetDirectory, bool isLoad, Func<string, bool> operateOnFile)
		{
			InitializeComponent();
			IsLoad = isLoad;
			DirectoryInfo = Directory.CreateDirectory(targetDirectory);
			DirectoryWatcher = new FileSystemWatcher(targetDirectory, "*.json");
			DirectoryWatcher.Created += (object sender, FileSystemEventArgs e) => RefreshList();
			DirectoryWatcher.Deleted += (object sender, FileSystemEventArgs e) => RefreshList();
			DirectoryWatcher.Renamed += (object sender, RenamedEventArgs e) => RefreshList();

			SaveLoadButton.Text = IsLoad ? "Load" : "Save";
			SaveLoadButton.Enabled = false;
			Text = (IsLoad ? "Load" : "Save") + " Preset";
			FileNameBox.ReadOnly = IsLoad;

			OperateOnFile = operateOnFile;

			RefreshList();
		}

		private void RefreshList()
		{
			string selection = FileNameBox.Text;
			FileListBox.Items.Clear();

			FileInfo[] files = DirectoryInfo.GetFiles("*.json");
			Array.Sort(files, (file1, file2) => file1.Name.CompareTo(file2.Name));
			foreach(FileInfo file in files)
			{
				FileListBox.Items.Add(Path.GetFileNameWithoutExtension(file.Name));
			}

			if(FileListBox.Items.Cast<string>().Any(str => str.Equals(selection)))
			{
				FileListBox.SelectedItem = selection;
			} 
			else
			{
				FileListBox.ClearSelected();
			}

		}

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			if (FileListBox.SelectedItem == null && IsLoad)
			{
				//we don't have anything selected, prevent loading
				SaveLoadButton.Enabled = false;
				return;
			}
			FileNameBox.Text = FileListBox.SelectedItem.ToString();
			SaveLoadButton.Enabled = FileNameBox.Text.Length > 0;
		}

		private void FileNameChanged(object sender, EventArgs e)
		{
			SaveLoadButton.Enabled = FileNameBox.Text.Length > 0;
			
		}

		private void SaveOrLoadFile(object sender, EventArgs e)
		{
			if (FileNameBox.Text.Length == 0 || Path.GetInvalidFileNameChars().Any(ch => FileNameBox.Text.Contains(ch)))
			{
				//one or more invalid characters, abort
				MessageBox.Show("The preset name contains one or more invalid characters", "Invalid file name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			string fullPath = $"{TargetDirectory}\\{FileNameBox.Text}.json";
			
			try
			{
				if (!IsLoad && File.Exists(fullPath) && MessageBox.Show("A preset with this name already exists. Do you want to overwite it?", "File already exists", MessageBoxButtons.YesNo) != DialogResult.Yes)
				{
					//we don't want to overrite the file
					return;
				}
				if(OperateOnFile(fullPath))
				{
					DialogResult = DialogResult.OK;
				}

			}
			catch(Exception ex)
			{
				bool didWrite = false;
				string filePath = $"{Settings.StoragePath}\\error_{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.txt";
				try
				{
					using StreamWriter file = new StreamWriter(filePath);
					file.WriteLine("Error accessing file " + fullPath);
					file.WriteLine("-----------------------------------------------------------------------------");
					file.WriteLine("Date : " + DateTime.Now.ToString());
					file.WriteLine();

					while (ex != null)
					{
						file.WriteLine(ex.GetType().FullName);
						file.WriteLine("Message : " + ex.Message);
						file.WriteLine("StackTrace : " + ex.StackTrace);

						ex = ex.InnerException;
					}
				}
				finally
				{
					MessageBox.Show("Error while " + (IsLoad ? "reading" : "writing") + " file. " + (didWrite?$"Detailed error written to {filePath}":""), "Error accessing file", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		
	}
}
