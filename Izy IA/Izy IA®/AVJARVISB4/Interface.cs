using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using AVJARVISB4.Clases;
using AVJARVISB4.Properties;
using VirtualKey;

namespace AVJARVISB4
{
	// Token: 0x0200001D RID: 29
	public class Interface : Window, IComponentConnector
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public Interface()
		{
			this.InitializeComponent();
			this.loadVoces();
			SoundEffects.Somefects();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000286C File Offset: 0x00000A6C
		private void animacion_Completed(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00002DDA File Offset: 0x00000FDA
		private void bntVozOk_Click(object sender, RoutedEventArgs e)
		{
			this.saveVoz();
			SoundEffects.Show_Window.Play();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00039F8C File Offset: 0x0003818C
		private void btn1_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.gConfiguracion.Visibility == Visibility.Visible;
			if (flag)
			{
				this.gConfiguracion.Visibility = Visibility.Hidden;
				SoundEffects.ClickClose_menu_hide.Play();
			}
			else
			{
				this.gConfiguracion.Visibility = Visibility.Visible;
				SoundEffects.ClickOpen_menu_show.Play();
			}
			this.gComandos.Visibility = Visibility.Hidden;
			this.gAvisos.Visibility = Visibility.Hidden;
			this.gOtros.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btn1_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btn1_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0003A00C File Offset: 0x0003820C
		private void btn2_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.gComandos.Visibility == Visibility.Visible;
			if (flag)
			{
				this.gComandos.Visibility = Visibility.Hidden;
				SoundEffects.ClickClose_menu_hide.Play();
			}
			else
			{
				this.gComandos.Visibility = Visibility.Visible;
				SoundEffects.ClickOpen_menu_show.Play();
			}
			this.gConfiguracion.Visibility = Visibility.Hidden;
			this.gAvisos.Visibility = Visibility.Hidden;
			this.gOtros.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btn2_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btn2_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0003A08C File Offset: 0x0003828C
		private void btn3_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.gOtros.Visibility == Visibility.Visible;
			if (flag)
			{
				this.gOtros.Visibility = Visibility.Hidden;
				SoundEffects.ClickClose_menu_hide.Play();
			}
			else
			{
				this.gOtros.Visibility = Visibility.Visible;
				SoundEffects.ClickOpen_menu_show.Play();
			}
			this.gConfiguracion.Visibility = Visibility.Hidden;
			this.gComandos.Visibility = Visibility.Hidden;
			this.gAvisos.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btn3_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btn3_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0003A10C File Offset: 0x0003830C
		private void btn4_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.gAvisos.Visibility == Visibility.Visible;
			if (flag)
			{
				this.gAvisos.Visibility = Visibility.Hidden;
				SoundEffects.ClickClose_menu_hide.Play();
			}
			else
			{
				this.gAvisos.Visibility = Visibility.Visible;
				SoundEffects.ClickOpen_menu_show.Play();
			}
			this.gConfiguracion.Visibility = Visibility.Hidden;
			this.gComandos.Visibility = Visibility.Hidden;
			this.gOtros.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btn4_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btn4_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0003A18C File Offset: 0x0003838C
		private void btnAlram_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.alarm == null;
			if (flag)
			{
				this.alarm = new Alarm();
				this.alarm.Closed += delegate(object a, EventArgs b)
				{
					this.alarm = null;
				};
			}
			else
			{
				this.alarm = null;
				this.alarm = new Alarm();
				SoundEffects.ClickOpen_fields.Play();
			}
			this.alarm.WindowState = WindowState.Normal;
			this.alarm.Show();
			this.alarm.Activate();
			this.gAvisos.Visibility = Visibility.Hidden;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnAlram_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnAlram_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0003A220 File Offset: 0x00038420
		private void btnBDatos_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				bool flag = this.cmd == null;
				if (flag)
				{
					this.cmd = new Comandos();
					this.cmd.Closed += delegate(object a, EventArgs b)
					{
						this.cmd = null;
					};
					SoundEffects.ClickOpen_fields.Play();
				}
				this.cmd.WindowState = WindowState.Normal;
				this.cmd.Show();
				this.cmd.Activate();
				this.gComandos.Visibility = Visibility.Hidden;
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				MessageBox.Show(ex2.Message, "AV", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnBDatos_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnBDatos_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0003A2CC File Offset: 0x000384CC
		private void btnClima_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.cli == null;
			if (flag)
			{
				this.cli = new Clima();
				this.cli.Closed += delegate(object a, EventArgs b)
				{
					this.cli = null;
				};
				SoundEffects.ClickOpen_fields.Play();
			}
			this.cli.WindowState = WindowState.Normal;
			this.cli.Show();
			this.cli.Activate();
			this.gOtros.Visibility = Visibility.Hidden;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnClima_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnClima_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0003A34C File Offset: 0x0003854C
		private void btnCuentas_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.cnts == null;
			if (flag)
			{
				this.cnts = new Cuentas();
				this.cnts.Closed += delegate(object a, EventArgs b)
				{
					this.cnts = null;
				};
				SoundEffects.ClickOpen_fields.Play();
			}
			this.cnts.WindowState = WindowState.Normal;
			this.cnts.Show();
			this.cnts.Activate();
			this.gConfiguracion.Visibility = Visibility.Hidden;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnCuentas_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnCuentas_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00002DEF File Offset: 0x00000FEF
		private void btnIniciar_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://www.paypal.com/donate?business=QEEV2VLBMCTTS&item_name=Doe+para+ajudar+no+desenvolvimento+de+novas+fun%C3%A7%C3%B5es+e+melhorias+para+Assistente+Virtual%21&currency_code=BRL");
			SoundEffects.Hover_import_export.Play();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnIniciar_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnIniciar_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0003A3CC File Offset: 0x000385CC
		private void btnJarvis_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.cfg != null;
			if (flag)
			{
				this.cfg.Close();
			}
			bool flag2 = this.cmd != null;
			if (flag2)
			{
				this.cmd.Close();
			}
			bool flag3 = this.alarm != null;
			if (flag3)
			{
				this.alarm.Close();
			}
			bool flag4 = this.cnts != null;
			if (flag4)
			{
				this.cnts.Close();
			}
			bool flag5 = this.vk != null;
			if (flag5)
			{
				this.vk.Close();
			}
			SoundEffects.Click_settings_arrow.Play();
			this.avjarvis.Dispose();
			this.animacion = (Storyboard)base.TryFindResource("salida");
			this.animacion.Completed += this.animacion_Completed;
			this.animacion.Begin();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnJarvis_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnJarvis_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0003A4B4 File Offset: 0x000386B4
		private void btnLRss_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				bool flag = this.rss == null;
				if (flag)
				{
					this.rss = new PuertoSerial();
					this.rss.Closed += delegate(object a, EventArgs b)
					{
						this.rss = null;
					};
					SoundEffects.ClickOpen_fields.Play();
				}
				this.rss.WindowState = WindowState.Normal;
				this.rss.Show();
				this.rss.Activate();
				this.gOtros.Visibility = Visibility.Hidden;
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				MessageBox.Show(ex2.Message, "AV", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnLRss_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnLRss_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0003A560 File Offset: 0x00038760
		private void btnRecor_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				bool flag = this.rec == null;
				if (flag)
				{
					this.rec = new Recordatorio();
					this.rec.Closed += delegate(object a, EventArgs b)
					{
						this.rec = null;
					};
					SoundEffects.ClickOpen_fields.Play();
				}
				this.rec.WindowState = WindowState.Normal;
				this.rec.Show();
				this.rec.Activate();
				this.gAvisos.Visibility = Visibility.Hidden;
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				MessageBox.Show(ex2.Message, "AV", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnRecor_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnRecor_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0003A60C File Offset: 0x0003880C
		private void btnSist_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.cfg == null;
			if (flag)
			{
				this.cfg = new Confg();
				this.cfg.Closed += delegate(object a, EventArgs b)
				{
					this.cfg = null;
				};
				SoundEffects.ClickOpen_fields.Play();
			}
			this.cfg.WindowState = WindowState.Normal;
			this.cfg.Show();
			this.cfg.Activate();
			this.gConfiguracion.Visibility = Visibility.Hidden;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnSist_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnSist_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0003A68C File Offset: 0x0003888C
		private void btnTeclado_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				bool flag = this.vk == null;
				if (flag)
				{
					this.vk = new MainWindow();
					this.vk.Closed += delegate(object a, EventArgs b)
					{
						this.vk = null;
					};
					SoundEffects.ClickOpen_fields.Play();
				}
				this.vk.WindowState = WindowState.Normal;
				this.vk.Show();
				this.vk.Activate();
				this.gComandos.Visibility = Visibility.Hidden;
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				MessageBox.Show(ex2.Message, "AV", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnTeclado_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnTeclado_MouseLeave(object sender, MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0003A738 File Offset: 0x00038938
		private void cbvoces_DropDownClosed(object sender, EventArgs e)
		{
			bool flag = this.cbvoces.Text != "";
			if (flag)
			{
				this.avjarvis.SpeakAsyncCancelAll();
				try
				{
					Settings.Default.vozDefault = this.cbvoces.Text;
					this.avjarvis.SelectVoice(this.cbvoces.Text);
					this.cbvoces.Items.Remove("Microsoft Mike");
					this.cbvoces.Items.Remove("Microsoft Mary");
					this.avjarvis.SpeakAsync("você selecionou a voz de " + this.cbvoces.Text);
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					MessageBox.Show(ex2.Message, "AV Preconfig", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0003A81C File Offset: 0x00038A1C
		private void loadVoces()
		{
			bool flag = Settings.Default.vozDefault == "";
			if (flag)
			{
				this.gridVoz.Visibility = Visibility.Visible;
				try
				{
					foreach (InstalledVoice installedVoice in this.avjarvis.GetInstalledVoices())
					{
						this.cbvoces.Items.Add(installedVoice.VoiceInfo.Name);
					}
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					MessageBox.Show(ex2.Message, "AV Preconfig", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
			}
			else
			{
				this.gridVoz.Visibility = Visibility.Hidden;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0003A8F0 File Offset: 0x00038AF0
		private void saveVoz()
		{
			this.avjarvis.SelectVoice(Settings.Default.vozDefault);
			this.avjarvis.Volume = Settings.Default.volumen;
			Settings.Default.Save();
			this.gridVoz.Visibility = Visibility.Hidden;
			this.avjarvis.Dispose();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000020F8 File Offset: 0x000002F8
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0003A950 File Offset: 0x00038B50
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/A V®;component/interface.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0003A988 File Offset: 0x00038B88
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.Window = (Interface)target;
				this.Window.MouseLeftButtonDown += this.Window_MouseLeftButtonDown;
				break;
			case 2:
				this.visibleCfg_BeginStoryboard = (BeginStoryboard)target;
				break;
			case 3:
				this.visibleComandos_BeginStoryboard = (BeginStoryboard)target;
				break;
			case 4:
				this.visibleOtros_BeginStoryboard = (BeginStoryboard)target;
				break;
			case 5:
				this.VisibleAvisos_BeginStoryboard1 = (BeginStoryboard)target;
				break;
			case 6:
				this.StoryboardDonacion_BeginStoryboard1 = (BeginStoryboard)target;
				break;
			case 7:
				this.LayoutRoot = (Grid)target;
				break;
			case 8:
				this.elipseP = (Ellipse)target;
				break;
			case 9:
				this.ellipse = (Ellipse)target;
				break;
			case 10:
				this.ellipse_Copy = (Ellipse)target;
				break;
			case 11:
				this.ElipseColor = (Ellipse)target;
				break;
			case 12:
				this.btnJarvis = (Button)target;
				this.btnJarvis.Click += this.btnJarvis_Click;
				this.btnJarvis.MouseEnter += this.btnJarvis_MouseEnter;
				this.btnJarvis.MouseLeave += this.btnJarvis_MouseLeave;
				break;
			case 13:
				this.btnIniciar = (Button)target;
				this.btnIniciar.Click += this.btnIniciar_Click;
				this.btnIniciar.MouseEnter += this.btnIniciar_MouseEnter;
				this.btnIniciar.MouseLeave += this.btnIniciar_MouseLeave;
				break;
			case 14:
				this.gridInicio = (Grid)target;
				break;
			case 15:
				this.recAyuda = (Rectangle)target;
				break;
			case 16:
				this.label4 = (Label)target;
				break;
			case 17:
				this.gConfiguracion = (Grid)target;
				break;
			case 18:
				this.rec1 = (Rectangle)target;
				break;
			case 19:
				this.label = (Label)target;
				break;
			case 20:
				this.btnSist = (Button)target;
				this.btnSist.Click += this.btnSist_Click;
				this.btnSist.MouseLeave += this.btnSist_MouseLeave;
				this.btnSist.MouseEnter += this.btnSist_MouseEnter;
				break;
			case 21:
				this.btnCuentas = (Button)target;
				this.btnCuentas.Click += this.btnCuentas_Click;
				this.btnCuentas.MouseEnter += this.btnCuentas_MouseEnter;
				this.btnCuentas.MouseLeave += this.btnCuentas_MouseLeave;
				break;
			case 22:
				this.path = (Path)target;
				break;
			case 23:
				this.gAvisos = (Grid)target;
				break;
			case 24:
				this.rec4 = (Rectangle)target;
				break;
			case 25:
				this.label3 = (Label)target;
				break;
			case 26:
				this.path3 = (Path)target;
				break;
			case 27:
				this.btnRecor = (Button)target;
				this.btnRecor.Click += this.btnRecor_Click;
				this.btnRecor.MouseEnter += this.btnRecor_MouseEnter;
				this.btnRecor.MouseLeave += this.btnRecor_MouseLeave;
				break;
			case 28:
				this.btnAlram = (Button)target;
				this.btnAlram.Click += this.btnAlram_Click;
				this.btnAlram.MouseEnter += this.btnAlram_MouseEnter;
				this.btnAlram.MouseLeave += this.btnAlram_MouseLeave;
				break;
			case 29:
				this.gOtros = (Grid)target;
				break;
			case 30:
				this.rec3 = (Rectangle)target;
				break;
			case 31:
				this.label2 = (Label)target;
				break;
			case 32:
				this.path2 = (Path)target;
				break;
			case 33:
				this.btnLRss = (Button)target;
				this.btnLRss.Click += this.btnLRss_Click;
				this.btnLRss.MouseEnter += this.btnLRss_MouseEnter;
				this.btnLRss.MouseLeave += this.btnLRss_MouseLeave;
				break;
			case 34:
				this.btnClima = (Button)target;
				this.btnClima.Click += this.btnClima_Click;
				this.btnClima.MouseEnter += this.btnClima_MouseEnter;
				this.btnClima.MouseLeave += this.btnClima_MouseLeave;
				break;
			case 35:
				this.gComandos = (Grid)target;
				break;
			case 36:
				this.rec2 = (Rectangle)target;
				break;
			case 37:
				this.lblTeclado = (Label)target;
				break;
			case 38:
				this.path1 = (Path)target;
				break;
			case 39:
				this.btnBDatos = (Button)target;
				this.btnBDatos.Click += this.btnBDatos_Click;
				this.btnBDatos.MouseEnter += this.btnBDatos_MouseEnter;
				this.btnBDatos.MouseLeave += this.btnBDatos_MouseLeave;
				break;
			case 40:
				this.btnTeclado = (Button)target;
				this.btnTeclado.Click += this.btnTeclado_Click;
				this.btnTeclado.MouseEnter += this.btnTeclado_MouseEnter;
				this.btnTeclado.MouseLeave += this.btnTeclado_MouseLeave;
				break;
			case 41:
				this.gridVoz = (Grid)target;
				break;
			case 42:
				this.rectangle = (Rectangle)target;
				break;
			case 43:
				this.ellipse1 = (Ellipse)target;
				break;
			case 44:
				this.cbvoces = (ComboBox)target;
				this.cbvoces.DropDownClosed += this.cbvoces_DropDownClosed;
				break;
			case 45:
				this.bntVozOk = (Button)target;
				this.bntVozOk.Click += this.bntVozOk_Click;
				break;
			case 46:
				this.rec1_Copy = (Rectangle)target;
				break;
			case 47:
				this.label_Copy = (Label)target;
				break;
			case 48:
				this.rec4_Copy = (Rectangle)target;
				break;
			case 49:
				this.label3_Copy = (Label)target;
				break;
			case 50:
				this.rec2_Copy = (Rectangle)target;
				break;
			case 51:
				this.lblTeclado_Copy = (Label)target;
				break;
			case 52:
				this.rec3_Copy = (Rectangle)target;
				break;
			case 53:
				this.label2_Copy = (Label)target;
				break;
			case 54:
				this.recDonar = (Rectangle)target;
				break;
			case 55:
				this.labelDonar = (Label)target;
				break;
			case 56:
				this.btn1 = (Button)target;
				this.btn1.Click += this.btn1_Click;
				this.btn1.MouseEnter += this.btn1_MouseEnter;
				this.btn1.MouseLeave += this.btn1_MouseLeave;
				break;
			case 57:
				this.btn2 = (Button)target;
				this.btn2.Click += this.btn2_Click;
				this.btn2.MouseEnter += this.btn2_MouseEnter;
				this.btn2.MouseLeave += this.btn2_MouseLeave;
				break;
			case 58:
				this.btn3 = (Button)target;
				this.btn3.Click += this.btn3_Click;
				this.btn3.MouseLeave += this.btn3_MouseLeave;
				this.btn3.MouseEnter += this.btn3_MouseEnter;
				break;
			case 59:
				this.btn4 = (Button)target;
				this.btn4.Click += this.btn4_Click;
				this.btn4.MouseEnter += this.btn4_MouseEnter;
				this.btn4.MouseLeave += this.btn4_MouseLeave;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x04000262 RID: 610
		private SpeechSynthesizer avjarvis = new SpeechSynthesizer();

		// Token: 0x04000263 RID: 611
		private Storyboard animacion;

		// Token: 0x04000264 RID: 612
		private Confg cfg;

		// Token: 0x04000265 RID: 613
		private Comandos cmd;

		// Token: 0x04000266 RID: 614
		private Cuentas cnts;

		// Token: 0x04000267 RID: 615
		private Alarm alarm = new Alarm();

		// Token: 0x04000268 RID: 616
		private Recordatorio rec;

		// Token: 0x04000269 RID: 617
		private PuertoSerial rss;

		// Token: 0x0400026A RID: 618
		private MainWindow vk;

		// Token: 0x0400026B RID: 619
		private Clima cli;

		// Token: 0x0400026C RID: 620
		internal Interface Window;

		// Token: 0x0400026D RID: 621
		internal BeginStoryboard visibleCfg_BeginStoryboard;

		// Token: 0x0400026E RID: 622
		internal BeginStoryboard visibleComandos_BeginStoryboard;

		// Token: 0x0400026F RID: 623
		internal BeginStoryboard visibleOtros_BeginStoryboard;

		// Token: 0x04000270 RID: 624
		internal BeginStoryboard VisibleAvisos_BeginStoryboard1;

		// Token: 0x04000271 RID: 625
		internal BeginStoryboard StoryboardDonacion_BeginStoryboard1;

		// Token: 0x04000272 RID: 626
		internal Grid LayoutRoot;

		// Token: 0x04000273 RID: 627
		internal Ellipse elipseP;

		// Token: 0x04000274 RID: 628
		internal Ellipse ellipse;

		// Token: 0x04000275 RID: 629
		internal Ellipse ellipse_Copy;

		// Token: 0x04000276 RID: 630
		internal Ellipse ElipseColor;

		// Token: 0x04000277 RID: 631
		internal Button btnJarvis;

		// Token: 0x04000278 RID: 632
		internal Button btnIniciar;

		// Token: 0x04000279 RID: 633
		internal Grid gridInicio;

		// Token: 0x0400027A RID: 634
		internal Rectangle recAyuda;

		// Token: 0x0400027B RID: 635
		internal Label label4;

		// Token: 0x0400027C RID: 636
		internal Grid gConfiguracion;

		// Token: 0x0400027D RID: 637
		internal Rectangle rec1;

		// Token: 0x0400027E RID: 638
		internal Label label;

		// Token: 0x0400027F RID: 639
		internal Button btnSist;

		// Token: 0x04000280 RID: 640
		internal Button btnCuentas;

		// Token: 0x04000281 RID: 641
		internal Path path;

		// Token: 0x04000282 RID: 642
		internal Grid gAvisos;

		// Token: 0x04000283 RID: 643
		internal Rectangle rec4;

		// Token: 0x04000284 RID: 644
		internal Label label3;

		// Token: 0x04000285 RID: 645
		internal Path path3;

		// Token: 0x04000286 RID: 646
		internal Button btnRecor;

		// Token: 0x04000287 RID: 647
		internal Button btnAlram;

		// Token: 0x04000288 RID: 648
		internal Grid gOtros;

		// Token: 0x04000289 RID: 649
		internal Rectangle rec3;

		// Token: 0x0400028A RID: 650
		internal Label label2;

		// Token: 0x0400028B RID: 651
		internal Path path2;

		// Token: 0x0400028C RID: 652
		internal Button btnLRss;

		// Token: 0x0400028D RID: 653
		internal Button btnClima;

		// Token: 0x0400028E RID: 654
		internal Grid gComandos;

		// Token: 0x0400028F RID: 655
		internal Rectangle rec2;

		// Token: 0x04000290 RID: 656
		internal Label lblTeclado;

		// Token: 0x04000291 RID: 657
		internal Path path1;

		// Token: 0x04000292 RID: 658
		internal Button btnBDatos;

		// Token: 0x04000293 RID: 659
		internal Button btnTeclado;

		// Token: 0x04000294 RID: 660
		internal Grid gridVoz;

		// Token: 0x04000295 RID: 661
		internal Rectangle rectangle;

		// Token: 0x04000296 RID: 662
		internal Ellipse ellipse1;

		// Token: 0x04000297 RID: 663
		internal ComboBox cbvoces;

		// Token: 0x04000298 RID: 664
		internal Button bntVozOk;

		// Token: 0x04000299 RID: 665
		internal Rectangle rec1_Copy;

		// Token: 0x0400029A RID: 666
		internal Label label_Copy;

		// Token: 0x0400029B RID: 667
		internal Rectangle rec4_Copy;

		// Token: 0x0400029C RID: 668
		internal Label label3_Copy;

		// Token: 0x0400029D RID: 669
		internal Rectangle rec2_Copy;

		// Token: 0x0400029E RID: 670
		internal Label lblTeclado_Copy;

		// Token: 0x0400029F RID: 671
		internal Rectangle rec3_Copy;

		// Token: 0x040002A0 RID: 672
		internal Label label2_Copy;

		// Token: 0x040002A1 RID: 673
		internal Rectangle recDonar;

		// Token: 0x040002A2 RID: 674
		internal Label labelDonar;

		// Token: 0x040002A3 RID: 675
		internal Button btn1;

		// Token: 0x040002A4 RID: 676
		internal Button btn2;

		// Token: 0x040002A5 RID: 677
		internal Button btn3;

		// Token: 0x040002A6 RID: 678
		internal Button btn4;

		// Token: 0x040002A7 RID: 679
		private bool _contentLoaded;
	}
}
