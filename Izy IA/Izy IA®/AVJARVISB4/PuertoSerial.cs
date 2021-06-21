using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using AVJARVISB4.Properties;
using Jarvis.CapaData;

namespace AVJARVISB4
{
	// Token: 0x02000015 RID: 21
	public class PuertoSerial : Window, IComponentConnector
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00031808 File Offset: 0x0002FA08
		public PuertoSerial()
		{
			this.InitializeComponent();
			this.mostrarTabla();
			this.btnClose.Click += this.BtnClose_Click;
			this.btnMini.Click += this.btnMini_Click;
			this.tbBaudio.Text = Settings.Default.Baudio;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00031894 File Offset: 0x0002FA94
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.gridAddCmds.Visibility == Visibility.Visible;
			if (flag)
			{
				this.gridAddCmds.Visibility = Visibility.Hidden;
				this.btnAdd.Visibility = Visibility.Hidden;
				this.btnNew.Visibility = Visibility.Visible;
			}
			bool flag2 = this.cbComCopy.Text != "" & this.tbCmd.Text != "" & this.tbDataSerial.Text != "";
			if (flag2)
			{
				this.db.add_XmlCmdSerial(this.cbComCopy.Text, this.tbCmd.Text, this.tbDataSerial.Text, this.tbRes.Text);
				this.cbComCopy.Text = "";
				this.tbCmd.Text = "";
				this.tbDataSerial.Text = "";
				this.tbRes.Text = "";
				this.mostrarTabla();
			}
			else
			{
				SystemSounds.Asterisk.Play();
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000286C File Offset: 0x00000A6C
		private void btnCerrar_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000286C File Offset: 0x00000A6C
		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000319BC File Offset: 0x0002FBBC
		private void btnCOM_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				DataRowView dataRowView = (DataRowView)this.dgCmd.SelectedItems[0];
				string comando = dataRowView["Comando"].ToString();
				string text = SerialDB.info_XmlCmdsSerial(comando);
				bool flag = text != "no found";
				if (flag)
				{
					SerialPort serialPort = new SerialPort(text.Split(new char[]
					{
						'+'
					})[0], Convert.ToInt32(Settings.Default.Baudio));
					bool isOpen = serialPort.IsOpen;
					if (isOpen)
					{
						serialPort.Close();
					}
					try
					{
						serialPort.Open();
						serialPort.Write(text.Split(new char[]
						{
							'+'
						})[2]);
						bool isOpen2 = serialPort.IsOpen;
						if (isOpen2)
						{
							serialPort.Close();
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
			}
			catch (Exception ex2)
			{
				Exception ex3 = ex2;
				MessageBox.Show(ex3.Message, "AV", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00031AE0 File Offset: 0x0002FCE0
		private void btnConect_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.cbCom.Text != "" & this.IsNumeric(this.tbBaudio.Text);
			if (flag)
			{
				this.conectar(this.cbCom.Text, Convert.ToInt32(this.tbBaudio.Text));
			}
			else
			{
				MessageBox.Show("COM o Baudio Inválido", "CONTROLE DE PORTA SERIAL");
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00031B54 File Offset: 0x0002FD54
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			DataRowView dataRowView = (DataRowView)this.dgCmd.SelectedItems[0];
			string comando = dataRowView["Comando"].ToString();
			this.db.clear_XmlCmdSerial(comando);
			this.mostrarTabla();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002876 File Offset: 0x00000A76
		private void btnDesconec_Click(object sender, RoutedEventArgs e)
		{
			this.desconectar();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00002880 File Offset: 0x00000A80
		private void btnMini_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00031BA0 File Offset: 0x0002FDA0
		private void btnNew_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.gridAddCmds.Visibility == Visibility.Hidden;
			if (flag)
			{
				this.gridAddCmds.Visibility = Visibility.Visible;
				this.btnAdd.Visibility = Visibility.Visible;
				this.btnNew.Visibility = Visibility.Hidden;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00031BEC File Offset: 0x0002FDEC
		private void btnSendCmd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				bool flag = this.tbData.Text != "";
				if (flag)
				{
					this.puertoCom.Write(this.tbData.Text);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "CONTROLE DE PORTA SERIAL");
				this.stateOffline();
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00031C60 File Offset: 0x0002FE60
		private void buscarPuertos()
		{
			foreach (string newItem in SerialPort.GetPortNames())
			{
				this.cbCom.Items.Add(newItem);
				this.cbComCopy.Items.Add(newItem);
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000288B File Offset: 0x00000A8B
		private void cbCom_DropDownOpened(object sender, EventArgs e)
		{
			this.cbCom.Items.Clear();
			this.buscarPuertos();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000028A6 File Offset: 0x00000AA6
		private void cbComCopy_DropDownOpened(object sender, EventArgs e)
		{
			this.cbComCopy.Items.Clear();
			this.buscarPuertos();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00031CB0 File Offset: 0x0002FEB0
		private void conectar(string COM, int baudio)
		{
			this.puertoCom = new SerialPort(COM, baudio);
			bool isOpen = this.puertoCom.IsOpen;
			if (isOpen)
			{
				this.puertoCom.Close();
			}
			try
			{
				this.puertoCom.Open();
				this.stateOnline();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "CONTROLE DE PORTA SERIAL");
				this.stateOffline();
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00031D30 File Offset: 0x0002FF30
		private void desconectar()
		{
			bool isOpen = this.puertoCom.IsOpen;
			if (isOpen)
			{
				this.puertoCom.Close();
			}
			this.stateOffline();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000028C1 File Offset: 0x00000AC1
		private void dgCmd_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000028C1 File Offset: 0x00000AC1
		private void dgCmd_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000028C1 File Offset: 0x00000AC1
		private void dgCmd_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000028C1 File Offset: 0x00000AC1
		private void dgCmd_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00031D64 File Offset: 0x0002FF64
		public bool IsNumeric(object Expression)
		{
			double num;
			return double.TryParse(Convert.ToString(Expression), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out num);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00031D90 File Offset: 0x0002FF90
		private void mostrarTabla()
		{
			this.ds = new DataSet();
			this.ds.ReadXml(this.urlXml);
			try
			{
				DataView itemsSource = new DataView(this.ds.Tables[0]);
				this.dgCmd.ItemsSource = itemsSource;
			}
			catch
			{
				this.dgCmd.ItemsSource = null;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00031E08 File Offset: 0x00030008
		private void stateOffline()
		{
			this.lblState.Content = "DESLIGADO";
			this.lblState.Foreground = new SolidColorBrush(Color.FromRgb(155, 155, 155));
			this.btnConect.Visibility = Visibility.Visible;
			this.btnDesconec.Visibility = Visibility.Hidden;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00031E68 File Offset: 0x00030068
		private void stateOnline()
		{
			this.lblState.Content = "LIGADO";
			this.lblState.Foreground = new SolidColorBrush(Colors.Green);
			this.btnConect.Visibility = Visibility.Hidden;
			this.btnDesconec.Visibility = Visibility.Visible;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000028C4 File Offset: 0x00000AC4
		private void tbBaudio_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Settings.Default.Baudio = this.tbBaudio.Text;
			Settings.Default.Save();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000020F8 File Offset: 0x000002F8
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00031EB8 File Offset: 0x000300B8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/A V®;component/puertoserial.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00031EF0 File Offset: 0x000300F0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.Window = (PuertoSerial)target;
				this.Window.MouseLeftButtonDown += this.Window_MouseLeftButtonDown;
				break;
			case 2:
				this.LayoutRoot = (Grid)target;
				break;
			case 3:
				this.imgHud = (Image)target;
				break;
			case 4:
				this.lbl1 = (Label)target;
				break;
			case 5:
				this.lbl2 = (Label)target;
				break;
			case 6:
				this.btnClose = (Button)target;
				break;
			case 7:
				this.btnMini = (Button)target;
				break;
			case 8:
				this.lblPuerto = (Label)target;
				break;
			case 9:
				this.lblBaudio = (Label)target;
				break;
			case 10:
				this.cbCom = (ComboBox)target;
				this.cbCom.DropDownOpened += this.cbCom_DropDownOpened;
				break;
			case 11:
				this.tbBaudio = (TextBox)target;
				this.tbBaudio.DataContextChanged += this.tbBaudio_DataContextChanged;
				break;
			case 12:
				this.lblData = (Label)target;
				break;
			case 13:
				this.btnSendCOM = (Button)target;
				this.btnSendCOM.Click += this.btnCOM_Click;
				break;
			case 14:
				this.btnDelete = (Button)target;
				this.btnDelete.Click += this.btnDelete_Click;
				break;
			case 15:
				this.btnNew = (Button)target;
				this.btnNew.Click += this.btnNew_Click;
				break;
			case 16:
				this.btnAdd = (Button)target;
				this.btnAdd.Click += this.btnAdd_Click;
				break;
			case 17:
				this.btnConect = (Button)target;
				this.btnConect.Click += this.btnConect_Click;
				break;
			case 18:
				this.btnDesconec = (Button)target;
				this.btnDesconec.Click += this.btnDesconec_Click;
				break;
			case 19:
				this.tbData = (TextBox)target;
				break;
			case 20:
				this.dgCmd = (DataGrid)target;
				this.dgCmd.SelectedCellsChanged += this.dgCmd_SelectedCellsChanged;
				this.dgCmd.CellEditEnding += this.dgCmd_CellEditEnding;
				this.dgCmd.Loaded += this.dgCmd_Loaded;
				this.dgCmd.BeginningEdit += this.dgCmd_BeginningEdit;
				break;
			case 21:
				this.btnSendCmd = (Button)target;
				this.btnSendCmd.Click += this.btnSendCmd_Click;
				break;
			case 22:
				this.gridAddCmds = (Grid)target;
				break;
			case 23:
				this.tbDataSerial = (TextBox)target;
				break;
			case 24:
				this.tbCmd = (TextBox)target;
				break;
			case 25:
				this.tbRes = (TextBox)target;
				break;
			case 26:
				this.cbComCopy = (ComboBox)target;
				this.cbComCopy.DropDownOpened += this.cbComCopy_DropDownOpened;
				break;
			case 27:
				this.lblState = (Label)target;
				break;
			case 28:
				this.btnCerrar = (Button)target;
				this.btnCerrar.Click += this.btnCerrar_Click;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x0400012F RID: 303
		private SerialPort puertoCom;

		// Token: 0x04000130 RID: 304
		private DataSet ds;

		// Token: 0x04000131 RID: 305
		private SerialDB db = new SerialDB();

		// Token: 0x04000132 RID: 306
		private string urlXml = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\DBSerial.xml";

		// Token: 0x04000133 RID: 307
		internal PuertoSerial Window;

		// Token: 0x04000134 RID: 308
		internal Grid LayoutRoot;

		// Token: 0x04000135 RID: 309
		internal Image imgHud;

		// Token: 0x04000136 RID: 310
		internal Label lbl1;

		// Token: 0x04000137 RID: 311
		internal Label lbl2;

		// Token: 0x04000138 RID: 312
		internal Button btnClose;

		// Token: 0x04000139 RID: 313
		internal Button btnMini;

		// Token: 0x0400013A RID: 314
		internal Label lblPuerto;

		// Token: 0x0400013B RID: 315
		internal Label lblBaudio;

		// Token: 0x0400013C RID: 316
		internal ComboBox cbCom;

		// Token: 0x0400013D RID: 317
		internal TextBox tbBaudio;

		// Token: 0x0400013E RID: 318
		internal Label lblData;

		// Token: 0x0400013F RID: 319
		internal Button btnSendCOM;

		// Token: 0x04000140 RID: 320
		internal Button btnDelete;

		// Token: 0x04000141 RID: 321
		internal Button btnNew;

		// Token: 0x04000142 RID: 322
		internal Button btnAdd;

		// Token: 0x04000143 RID: 323
		internal Button btnConect;

		// Token: 0x04000144 RID: 324
		internal Button btnDesconec;

		// Token: 0x04000145 RID: 325
		internal TextBox tbData;

		// Token: 0x04000146 RID: 326
		internal DataGrid dgCmd;

		// Token: 0x04000147 RID: 327
		internal Button btnSendCmd;

		// Token: 0x04000148 RID: 328
		internal Grid gridAddCmds;

		// Token: 0x04000149 RID: 329
		internal TextBox tbDataSerial;

		// Token: 0x0400014A RID: 330
		internal TextBox tbCmd;

		// Token: 0x0400014B RID: 331
		internal TextBox tbRes;

		// Token: 0x0400014C RID: 332
		internal ComboBox cbComCopy;

		// Token: 0x0400014D RID: 333
		internal Label lblState;

		// Token: 0x0400014E RID: 334
		internal Button btnCerrar;

		// Token: 0x0400014F RID: 335
		private bool _contentLoaded;
	}
}
