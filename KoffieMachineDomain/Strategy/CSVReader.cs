using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace KoffieMachineDomain.Strategy
{
	public class CSVReader : IConfigurationReader
	{
		private Dictionary<string, Configurable> Configurables { get; set; }

		public Dictionary<string, Configurable> ReadConfigurations()
		{
			Configurables = new Dictionary<string, Configurable>();

			using (OpenFileDialog fileDialog = new OpenFileDialog())
			{
				fileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
				fileDialog.FilterIndex = 1;
				fileDialog.RestoreDirectory = true;

				if (fileDialog.ShowDialog() == DialogResult.OK)
				{
					InterpretConfiguration(fileDialog.OpenFile());
				}
			}

			return Configurables;
		}

		private void InterpretConfiguration(Stream fileStream)
		{
			if (fileStream != null)
			{
				using (TextFieldParser parser = new TextFieldParser(fileStream))
				{
					parser.TextFieldType = FieldType.Delimited;
					parser.SetDelimiters(",");
					while (!parser.EndOfData)
					{
						string[] fields = parser.ReadFields();
						Configurables[fields[0]] = new Configurable(Convert.ToDouble(fields[1]), (Strength) Enum.Parse(typeof(Strength), fields[2]), (Amount) Enum.Parse(typeof(Amount), fields[3]), (Amount) Enum.Parse(typeof(Amount), fields[4]));
					}
				}
			}
		}
	}
}
