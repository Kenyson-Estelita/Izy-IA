using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using AVJARVISB4.Clases;
using AVJARVISB4.Properties;

namespace AVJARVISB4
{
	// Token: 0x02000018 RID: 24
	public class Clima : Window, IComponentConnector
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00034F78 File Offset: 0x00033178
		public Clima()
		{
			this.InitializeComponent();
			SoundEffects.Somefects();
			this.btnClose.Click += this.btnClose_Click;
			this.btnMini.Click += this.btnMini_Click;
			this.btnBuscar.Click += this.btnBuscar_Click;
			this.btnProbar.Click += this.btnProbar_Click;
			this.tbCodigo.TextChanged += this.tbCodigo_TextChanged;
			this.cbCelsius.Checked += this.cbCelsius_Checked;
			this.cbFahrenheit.Checked += this.cbFahrenheit_Checked;
			base.MouseLeftButtonDown += this.Clima_MouseLeftButtonDown;
			this.cbCelsius.IsChecked = new bool?(Settings.Default.celcius);
			this.cbFahrenheit.IsChecked = new bool?(Settings.Default.fahrenheit);
			this.tbCodigo.Text = Settings.Default.CfgCiudad;
			this.lblClima.Content = this.loadClima();
			this.LayoutRoot.Opacity = Settings.Default.opacidad;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000294E File Offset: 0x00000B4E
		private void btnBuscar_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.Whip.Play();
			Process.Start("http://openweathermap.org/");
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000020E3 File Offset: 0x000002E3
		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ClickClose_menu_hide.Play();
			base.Close();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00002938 File Offset: 0x00000B38
		private void btnMini_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.Hover_main_button.Play();
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00002967 File Offset: 0x00000B67
		private void btnProbar_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.Keyboard.Play();
			this.lblClima.Content = this.loadClima();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000350CC File Offset: 0x000332CC
		private void cbCelsius_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.fahrenheit = false;
			Settings.Default.celcius = true;
			Settings.Default.grados = "&units=metric";
			Settings.Default.Save();
			this.cbFahrenheit.IsChecked = new bool?(false);
			this.cbCelsius.IsChecked = new bool?(true);
			bool flag = this.tbCodigo.Text != "";
			if (flag)
			{
				this.lblClima.Content = this.loadClima();
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00035160 File Offset: 0x00033360
		private void cbFahrenheit_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.fahrenheit = true;
			Settings.Default.celcius = false;
			Settings.Default.grados = "&units=imperial";
			Settings.Default.Save();
			this.cbFahrenheit.IsChecked = new bool?(true);
			this.cbCelsius.IsChecked = new bool?(false);
			bool flag = this.tbCodigo.Text != "";
			if (flag)
			{
				this.lblClima.Content = this.loadClima();
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000020F8 File Offset: 0x000002F8
		private void Clima_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000351F4 File Offset: 0x000333F4
		private string loadClima()
		{
			string result = null;
			try
			{
				string uriString = string.Concat(new string[]
				{
					"http://api.openweathermap.org/data/2.5/weather?id=",
					Settings.Default.CfgCiudad,
					"&appid=",
					Settings.Default.ApiW,
					"&mode=xml",
					Settings.Default.grados
				});
				string xml = new WebClient().DownloadString(new Uri(uriString));
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				string value = xmlDocument.DocumentElement.SelectSingleNode("city").Attributes["name"].Value;
				string text = xmlDocument.DocumentElement.SelectSingleNode("temperature").Attributes["value"].Value;
				bool flag = Settings.Default.grados == "&units=metric";
				if (flag)
				{
					text += " C";
				}
				bool flag2 = Settings.Default.grados == "&units=imperial";
				if (flag2)
				{
					text += " F";
				}
				result = "Cidade: " + value + ". Temperatura: " + text;
			}
			catch (Exception)
			{
				result = "Erro !, verifique seu código e seu acesso à Internet";
			}
			this.lblCityCode.Content = "http://openweathermap.org/city/" + this.tbCodigo.Text;
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00002987 File Offset: 0x00000B87
		private void tbCodigo_TextChanged(object sender, TextChangedEventArgs e)
		{
			Settings.Default.CfgCiudad = this.tbCodigo.Text;
			Settings.Default.Save();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnProbar_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnBuscar_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00035378 File Offset: 0x00033578
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/A V®;component/clima.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000353B0 File Offset: 0x000335B0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.Window = (Clima)target;
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
				this.tbCodigo = (TextBox)target;
				break;
			case 7:
				this.cbCelsius = (CheckBox)target;
				break;
			case 8:
				this.cbFahrenheit = (CheckBox)target;
				break;
			case 9:
				this.btnBuscar = (Button)target;
				this.btnBuscar.MouseEnter += this.btnBuscar_MouseEnter;
				break;
			case 10:
				this.lbl2_Copy = (Label)target;
				break;
			case 11:
				this.btnProbar = (Button)target;
				this.btnProbar.MouseEnter += this.btnProbar_MouseEnter;
				break;
			case 12:
				this.lblClima = (Label)target;
				break;
			case 13:
				this.btnClose = (Button)target;
				this.btnClose.Click += this.btnClose_Click;
				break;
			case 14:
				this.btnMini = (Button)target;
				this.btnMini.Click += this.btnMini_Click;
				break;
			case 15:
				this.lblCityCode = (Label)target;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x040001A8 RID: 424
		internal Clima Window;

		// Token: 0x040001A9 RID: 425
		internal Grid LayoutRoot;

		// Token: 0x040001AA RID: 426
		internal Image imgHud;

		// Token: 0x040001AB RID: 427
		internal Label lbl1;

		// Token: 0x040001AC RID: 428
		internal Label lbl2;

		// Token: 0x040001AD RID: 429
		internal TextBox tbCodigo;

		// Token: 0x040001AE RID: 430
		internal CheckBox cbCelsius;

		// Token: 0x040001AF RID: 431
		internal CheckBox cbFahrenheit;

		// Token: 0x040001B0 RID: 432
		internal Button btnBuscar;

		// Token: 0x040001B1 RID: 433
		internal Label lbl2_Copy;

		// Token: 0x040001B2 RID: 434
		internal Button btnProbar;

		// Token: 0x040001B3 RID: 435
		internal Label lblClima;

		// Token: 0x040001B4 RID: 436
		internal Button btnClose;

		// Token: 0x040001B5 RID: 437
		internal Button btnMini;

		// Token: 0x040001B6 RID: 438
		internal Label lblCityCode;

		// Token: 0x040001B7 RID: 439
		private bool _contentLoaded;
	}
}
