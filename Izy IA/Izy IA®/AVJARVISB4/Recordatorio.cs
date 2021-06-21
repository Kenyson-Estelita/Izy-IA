using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using AVJARVISB4.Clases;
using AVJARVISB4.Properties;
using Jarvis.CapaData;

namespace AVJARVISB4
{
	// Token: 0x02000016 RID: 22
	public class Recordatorio : Window, IComponentConnector
	{
		// Token: 0x0600011F RID: 287 RVA: 0x000322D0 File Offset: 0x000304D0
		public Recordatorio()
		{
			this.InitializeComponent();
			SoundEffects.Somefects();
			Rect workArea = SystemParameters.WorkArea;
			base.Left = workArea.Right / 2.0 + 310.0 - base.Width;
			base.Top = workArea.Bottom - base.Height;
			this.mostrarRecordatorios();
			this.loadHora();
			this.LayoutRoot.Opacity = Settings.Default.opacidad;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00032384 File Offset: 0x00030584
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			bool flag = !this.allDays;
			if (flag)
			{
				bool flag2 = this.tbTitulo.Text != "" && this.dpFecha.Text != "" && this.cbHor.Text != "" && this.cbMin.Text != "" && this.cbPer.Text != "";
				if (flag2)
				{
					this.sRec = new CDRecordatorio(this.tbTitulo.Text, this.tbNota.Text, this.dpFecha.Text, string.Concat(new string[]
					{
						this.cbHor.Text,
						":",
						this.cbMin.Text,
						":00 ",
						this.cbPer.Text
					}), this.allDays);
					this.tbRecors.Visibility = Visibility.Hidden;
					this.btnAtraz.Visibility = Visibility.Hidden;
					this.tbTitulo.Clear();
					this.tbNota.Clear();
					this.mostrarRecordatorios();
					this.cbAviso.IsChecked = new bool?(false);
					this.allDays = false;
				}
				else
				{
					System.Windows.MessageBox.Show("faltam dados");
				}
			}
			else
			{
				bool flag3 = this.tbTitulo.Text != "" && this.cbHor.Text != "" && this.cbMin.Text != "" && this.cbPer.Text != "";
				if (flag3)
				{
					this.sRec = new CDRecordatorio(this.tbTitulo.Text, this.tbNota.Text, "Todos os dias", string.Concat(new string[]
					{
						this.cbHor.Text,
						":",
						this.cbMin.Text,
						":00 ",
						this.cbPer.Text
					}), this.allDays);
					this.tbRecors.Visibility = Visibility.Hidden;
					this.btnAtraz.Visibility = Visibility.Hidden;
					this.tbTitulo.Clear();
					this.tbNota.Clear();
					this.mostrarRecordatorios();
					this.cbAviso.IsChecked = new bool?(false);
					this.allDays = false;
				}
				else
				{
					System.Windows.MessageBox.Show("faltam dados");
				}
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00032638 File Offset: 0x00030838
		private void btnAtraz_Click(object sender, RoutedEventArgs e)
		{
			this.tbRecors.Visibility = Visibility.Hidden;
			this.listRecor.Visibility = Visibility.Visible;
			this.btnAtraz.Visibility = Visibility.Hidden;
			this.btnBorrar.Visibility = Visibility.Visible;
			this.CalendarRec.Visibility = Visibility.Hidden;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00032688 File Offset: 0x00030888
		private void btnBorrar_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.sRec = new CDRecordatorio();
				bool flag = System.Windows.MessageBox.Show("Deseja realmente excluir o lembrete? ", " Excluir", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
				if (flag)
				{
					this.sRec.borrarInfoRec(this.listRecor.SelectedItem.ToString().Split(new char[]
					{
						'<'
					}));
					this.mostrarRecordatorios();
				}
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000028E8 File Offset: 0x00000AE8
		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			this.saveRecordatorio();
			base.Close();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00002880 File Offset: 0x00000A80
		private void btnMini_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000028F9 File Offset: 0x00000AF9
		private void cbAviso_Checked(object sender, RoutedEventArgs e)
		{
			this.allDays = true;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00002903 File Offset: 0x00000B03
		private void cbAviso_Unchecked(object sender, RoutedEventArgs e)
		{
			this.allDays = false;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00032714 File Offset: 0x00030914
		private void listRecor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			try
			{
				this.tbRecors.Visibility = Visibility.Visible;
				this.listRecor.Visibility = Visibility.Hidden;
				this.btnBorrar.Visibility = Visibility.Hidden;
				this.btnAtraz.Visibility = Visibility.Visible;
				this.CalendarRec.Visibility = Visibility.Visible;
				this.tbRecors.Clear();
				this.sRec = new CDRecordatorio();
				this.detalleRecorString = this.sRec.detalleInfoRecordatorio(this.listRecor.SelectedItem.ToString().Split(new char[]
				{
					'<'
				}));
				foreach (string textData in this.detalleRecorString)
				{
					this.tbRecors.AppendText(textData);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00032810 File Offset: 0x00030A10
		private void loadHora()
		{
			this.cbPer.Items.Add("AM");
			this.cbPer.Items.Add("PM");
			for (int i = 0; i < 13; i++)
			{
				this.cbHor.Items.Add(i.ToString());
			}
			for (int j = 0; j < 10; j++)
			{
				this.cbMin.Items.Add("0" + j.ToString());
			}
			for (int k = 10; k < 60; k++)
			{
				this.cbMin.Items.Add(k.ToString());
			}
			this.cbHor.Text = "12";
			this.cbMin.Text = "12";
			this.cbPer.Text = "AM";
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00032910 File Offset: 0x00030B10
		public void mostrarRecordatorios()
		{
			try
			{
				this.listRecor.Items.Clear();
				this.sRec = new CDRecordatorio();
				this.lisRecorString = this.sRec.mostrarAllRecordatorio();
				foreach (string newItem in this.lisRecorString)
				{
					this.listRecor.Items.Add(newItem);
				}
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000329C4 File Offset: 0x00030BC4
		private void saveRecordatorio()
		{
			try
			{
				this.sRec = new CDRecordatorio();
				this.sRec.guardarDocRecordatorio(System.Windows.Forms.Application.StartupPath + "\\DataBase", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data");
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000290D File Offset: 0x00000B0D
		private void tbRecors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			this.tbRecors.Visibility = Visibility.Hidden;
			this.btnAtraz.Visibility = Visibility.Hidden;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000020F8 File Offset: 0x000002F8
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnAdd_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnBorrar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnAtraz_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00032A30 File Offset: 0x00030C30
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/A V®;component/recordatorio.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00032A68 File Offset: 0x00030C68
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.Window = (Recordatorio)target;
				this.Window.MouseLeftButtonDown += this.Window_MouseLeftButtonDown;
				break;
			case 2:
				this.LayoutRoot = (Grid)target;
				break;
			case 3:
				this.pack___siteoforigin_____imagenes_HuD_png = (Image)target;
				break;
			case 4:
				this.btnClose = (System.Windows.Controls.Button)target;
				this.btnClose.Click += this.btnClose_Click;
				break;
			case 5:
				this.btnMini = (System.Windows.Controls.Button)target;
				this.btnMini.Click += this.btnMini_Click;
				break;
			case 6:
				this.lblCmd = (System.Windows.Controls.Label)target;
				break;
			case 7:
				this.lblCmd_Copy = (System.Windows.Controls.Label)target;
				break;
			case 8:
				this.lblCmd_Copy1 = (System.Windows.Controls.Label)target;
				break;
			case 9:
				this.lblCmd_Copy2 = (System.Windows.Controls.Label)target;
				break;
			case 10:
				this.tbTitulo = (System.Windows.Controls.TextBox)target;
				break;
			case 11:
				this.tbNota = (System.Windows.Controls.TextBox)target;
				break;
			case 12:
				this.dpFecha = (DatePicker)target;
				break;
			case 13:
				this.cbHor = (System.Windows.Controls.ComboBox)target;
				break;
			case 14:
				this.cbPer = (System.Windows.Controls.ComboBox)target;
				break;
			case 15:
				this.cbMin = (System.Windows.Controls.ComboBox)target;
				break;
			case 16:
				this.cbAviso = (System.Windows.Controls.CheckBox)target;
				this.cbAviso.Checked += this.cbAviso_Checked;
				this.cbAviso.Unchecked += this.cbAviso_Unchecked;
				break;
			case 17:
				this.tbRecors = (System.Windows.Controls.TextBox)target;
				this.tbRecors.MouseDoubleClick += this.tbRecors_MouseDoubleClick;
				break;
			case 18:
				this.listRecor = (System.Windows.Controls.ListBox)target;
				this.listRecor.MouseDoubleClick += this.listRecor_MouseDoubleClick;
				break;
			case 19:
				this.btnAdd = (System.Windows.Controls.Button)target;
				this.btnAdd.Click += this.btnAdd_Click;
				this.btnAdd.MouseEnter += this.btnAdd_MouseEnter;
				break;
			case 20:
				this.btnBorrar = (System.Windows.Controls.Button)target;
				this.btnBorrar.Click += this.btnBorrar_Click;
				this.btnBorrar.MouseEnter += this.btnBorrar_MouseEnter;
				break;
			case 21:
				this.btnAtraz = (System.Windows.Controls.Button)target;
				this.btnAtraz.Click += this.btnAtraz_Click;
				this.btnAtraz.MouseEnter += this.btnAtraz_MouseEnter;
				break;
			case 22:
				this.lblCmd_Copy4 = (System.Windows.Controls.Label)target;
				break;
			case 23:
				this.CalendarRec = (Calendar)target;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x04000150 RID: 336
		private readonly string nombreUsuarioSistema = SystemInformation.UserName;

		// Token: 0x04000151 RID: 337
		private List<string> lisRecorString = new List<string>();

		// Token: 0x04000152 RID: 338
		private List<string> detalleRecorString = new List<string>();

		// Token: 0x04000153 RID: 339
		private CDRecordatorio sRec;

		// Token: 0x04000154 RID: 340
		private bool allDays = false;

		// Token: 0x04000155 RID: 341
		internal Recordatorio Window;

		// Token: 0x04000156 RID: 342
		internal Grid LayoutRoot;

		// Token: 0x04000157 RID: 343
		internal Image pack___siteoforigin_____imagenes_HuD_png;

		// Token: 0x04000158 RID: 344
		internal System.Windows.Controls.Button btnClose;

		// Token: 0x04000159 RID: 345
		internal System.Windows.Controls.Button btnMini;

		// Token: 0x0400015A RID: 346
		internal System.Windows.Controls.Label lblCmd;

		// Token: 0x0400015B RID: 347
		internal System.Windows.Controls.Label lblCmd_Copy;

		// Token: 0x0400015C RID: 348
		internal System.Windows.Controls.Label lblCmd_Copy1;

		// Token: 0x0400015D RID: 349
		internal System.Windows.Controls.Label lblCmd_Copy2;

		// Token: 0x0400015E RID: 350
		internal System.Windows.Controls.TextBox tbTitulo;

		// Token: 0x0400015F RID: 351
		internal System.Windows.Controls.TextBox tbNota;

		// Token: 0x04000160 RID: 352
		internal DatePicker dpFecha;

		// Token: 0x04000161 RID: 353
		internal System.Windows.Controls.ComboBox cbHor;

		// Token: 0x04000162 RID: 354
		internal System.Windows.Controls.ComboBox cbPer;

		// Token: 0x04000163 RID: 355
		internal System.Windows.Controls.ComboBox cbMin;

		// Token: 0x04000164 RID: 356
		internal System.Windows.Controls.CheckBox cbAviso;

		// Token: 0x04000165 RID: 357
		internal System.Windows.Controls.TextBox tbRecors;

		// Token: 0x04000166 RID: 358
		internal System.Windows.Controls.ListBox listRecor;

		// Token: 0x04000167 RID: 359
		internal System.Windows.Controls.Button btnAdd;

		// Token: 0x04000168 RID: 360
		internal System.Windows.Controls.Button btnBorrar;

		// Token: 0x04000169 RID: 361
		internal System.Windows.Controls.Button btnAtraz;

		// Token: 0x0400016A RID: 362
		internal System.Windows.Controls.Label lblCmd_Copy4;

		// Token: 0x0400016B RID: 363
		internal Calendar CalendarRec;

		// Token: 0x0400016C RID: 364
		private bool _contentLoaded;
	}
}
