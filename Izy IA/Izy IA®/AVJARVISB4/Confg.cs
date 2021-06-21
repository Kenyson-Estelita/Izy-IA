using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using AVJARVISB4.Clases;
using AVJARVISB4.Properties;
using Microsoft.Win32;

namespace AVJARVISB4
{
	// Token: 0x0200001A RID: 26
	public class Confg : Window, IComponentConnector
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00036C0C File Offset: 0x00034E0C
		public Confg()
		{
			this.InitializeComponent();
			this.tbUser.TextChanged += this.tbUser_TextChanged;
			this.tbAi.TextChanged += this.tbAi_TextChanged;
			this.cbRec.DropDownClosed += this.cbRec_DropDownClosed;
			this.btnMini.Click += delegate(object a, RoutedEventArgs b)
			{
				base.WindowState = WindowState.Minimized;
			};
			this.btnCloseNS.Click += delegate(object a, RoutedEventArgs b)
			{
				base.Close();
			};
			this.loadVoces();
			this.loadData();
			SoundEffects.Somefects();
			this.sliderVolumen.ValueChanged += this.sliderVolumen_ValueChanged;
			this.sliderOpacidad.ValueChanged += this.sliderOpacidad_ValueChanged;
			this.sliderConfidencia.ValueChanged += this.sliderConfidencia_ValueChanged;
			this.sliderVolumen.Value = (double)Settings.Default.volumen;
			this.sliderOpacidad.Value = Settings.Default.opacidad;
			this.sliderConfidencia.Value = Settings.Default.Confidence;
			this.ocultarGrids();
			this.gBienvenida.Visibility = Visibility.Visible;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00002A31 File Offset: 0x00000C31
		private void btnAI_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Lino_03.Play();
			this.ocultarGrids();
			this.gAI.Visibility = Visibility.Visible;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00002A53 File Offset: 0x00000C53
		private void btnCloseNS_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ClickClose_menu_hide.Play();
			this.avjarvis.Dispose();
			this.reg.Dispose();
			base.Close();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00002A80 File Offset: 0x00000C80
		private void btnConfidence_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Lino_03.Play();
			this.ocultarGrids();
			this.gConfidencia.Visibility = Visibility.Visible;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00002AA2 File Offset: 0x00000CA2
		private void btnHud_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Lino_03.Play();
			this.ocultarGrids();
			this.gHud.Visibility = Visibility.Visible;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00002AC4 File Offset: 0x00000CC4
		private void btnOpacidad_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Lino_03.Play();
			this.ocultarGrids();
			this.gOpaciodad.Visibility = Visibility.Visible;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00002AE6 File Offset: 0x00000CE6
		private void btnPref_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Lino_03.Play();
			this.ocultarGrids();
			this.gPreferencias.Visibility = Visibility.Visible;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00002B08 File Offset: 0x00000D08
		private void btnTiempo_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Lino_03.Play();
			this.ocultarGrids();
			this.gRec.Visibility = Visibility.Visible;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00002B2A File Offset: 0x00000D2A
		private void btnUsuario_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Lino_03.Play();
			this.ocultarGrids();
			this.gUsuario.Visibility = Visibility.Visible;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00002B4C File Offset: 0x00000D4C
		private void btnVoces_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Lino_03.Play();
			this.ocultarGrids();
			this.gVoces.Visibility = Visibility.Visible;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00002B6E File Offset: 0x00000D6E
		private void btnVolumen_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Lino_03.Play();
			this.ocultarGrids();
			this.gVolumen.Visibility = Visibility.Visible;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00002B90 File Offset: 0x00000D90
		private void btnWindows_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Lino_03.Play();
			this.ocultarGrids();
			this.gWindows.Visibility = Visibility.Visible;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00036D74 File Offset: 0x00034F74
		private void cbHud_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cfgHud = (this.cbHud.IsChecked != null && this.cbHud.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00036D74 File Offset: 0x00034F74
		private void cbHud_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cfgHud = (this.cbHud.IsChecked != null && this.cbHud.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00002BB2 File Offset: 0x00000DB2
		private void cbRec_DropDownClosed(object sender, EventArgs e)
		{
			Settings.Default.cfgRec = Convert.ToInt32(this.cbRec.Text);
			Settings.Default.Save();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00036DC4 File Offset: 0x00034FC4
		private void cbVoz_DropDownClosed(object sender, EventArgs e)
		{
			try
			{
				this.avjarvis.SpeakAsyncCancelAll();
				this.avjarvis.SelectVoice(this.cbVoz.Text);
				this.cbVoz.Items.Remove("Microsoft Mike");
				this.cbVoz.Items.Remove("Microsoft Mary");
				this.avjarvis.Volume = Settings.Default.volumen;
				this.avjarvis.SpeakAsync("Você selecionou a voz de " + this.cbVoz.Text);
				Settings.Default.vozDefault = this.cbVoz.Text;
				Settings.Default.Save();
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00036EA0 File Offset: 0x000350A0
		private void cbWin_Checked(object sender, RoutedEventArgs e)
		{
			this.reg.SetValue("AV®", System.Windows.Forms.Application.ExecutablePath.ToString());
			Settings.Default.cfgWin = (this.cbWin.IsChecked != null && this.cbWin.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00036F0C File Offset: 0x0003510C
		private void cbWin_Unchecked(object sender, RoutedEventArgs e)
		{
			this.reg.DeleteValue("AV®");
			Settings.Default.cfgWin = (this.cbWin.IsChecked != null && this.cbWin.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00036F6C File Offset: 0x0003516C
		private void chbAR_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.Responder = (this.chbAR.IsChecked != null && this.chbAR.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00036F6C File Offset: 0x0003516C
		private void chbAR_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.Responder = (this.chbAR.IsChecked != null && this.chbAR.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00036FBC File Offset: 0x000351BC
		private void chbCA_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.CmdAlar = (this.chbCA.IsChecked != null && this.chbCA.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00036FBC File Offset: 0x000351BC
		private void chbCA_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.CmdAlar = (this.chbCA.IsChecked != null && this.chbCA.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0003700C File Offset: 0x0003520C
		private void chbCB_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.CmdSearch = (this.chbCB.IsChecked != null && this.chbCB.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0003700C File Offset: 0x0003520C
		private void chbCB_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.CmdSearch = (this.chbCB.IsChecked != null && this.chbCB.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0003705C File Offset: 0x0003525C
		private void chbCC_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.CmdCalc = (this.chbCC.IsChecked != null && this.chbCC.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0003705C File Offset: 0x0003525C
		private void chbCC_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.CmdCalc = (this.chbCC.IsChecked != null && this.chbCC.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000370AC File Offset: 0x000352AC
		private void chbcoma_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.Adicionais = (this.chbcoma.IsChecked != null && this.chbcoma.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000370AC File Offset: 0x000352AC
		private void chbcoma_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.Adicionais = (this.chbcoma.IsChecked != null && this.chbcoma.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000370FC File Offset: 0x000352FC
		private void chbCR_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.CmdRec = (this.chbCR.IsChecked != null && this.chbCR.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000370FC File Offset: 0x000352FC
		private void chbCR_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.CmdRec = (this.chbCR.IsChecked != null && this.chbCR.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0003714C File Offset: 0x0003534C
		private void chbReprod_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.Music = (this.chbReprod.IsChecked != null && this.chbReprod.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0003714C File Offset: 0x0003534C
		private void chbReprod_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.Music = (this.chbReprod.IsChecked != null && this.chbReprod.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0003719C File Offset: 0x0003539C
		private void chbTA_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.ActTecl = (this.chbTA.IsChecked != null && this.chbTA.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0003719C File Offset: 0x0003539C
		private void chbTA_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.ActTecl = (this.chbTA.IsChecked != null && this.chbTA.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000371EC File Offset: 0x000353EC
		private void loadData()
		{
			this.cbRec.Items.Add("1");
			this.cbRec.Items.Add("5");
			this.cbRec.Items.Add("10");
			this.cbRec.Items.Add("15");
			this.cbRec.Items.Add("20");
			this.cbRec.Items.Add("25");
			this.cbRec.Items.Add("30");
			this.cbWin.IsChecked = new bool?(Settings.Default.cfgWin);
			try
			{
				bool cfgWin = Settings.Default.cfgWin;
				if (cfgWin)
				{
					this.reg.SetValue("AV®", System.Windows.Forms.Application.ExecutablePath.ToString());
				}
				else
				{
					bool flag = this.reg.ValueCount >= 2;
					if (flag)
					{
						this.reg.DeleteValue("AV®");
					}
				}
			}
			catch (Exception ex)
			{
			}
			this.chbcoma.IsChecked = new bool?(Settings.Default.Adicionais);
			this.chbAR.IsChecked = new bool?(Settings.Default.Responder);
			this.chbCB.IsChecked = new bool?(Settings.Default.CmdSearch);
			this.chbCC.IsChecked = new bool?(Settings.Default.CmdCalc);
			this.chbCR.IsChecked = new bool?(Settings.Default.CmdRec);
			this.chbCA.IsChecked = new bool?(Settings.Default.CmdAlar);
			this.chbTA.IsChecked = new bool?(Settings.Default.ActTecl);
			this.chbReprod.IsChecked = new bool?(Settings.Default.Music);
			this.cbHud.IsChecked = new bool?(Settings.Default.cfgHud);
			this.tbUser.Text = Settings.Default.cfgUser;
			this.tbAi.Text = Settings.Default.cfgAi;
			this.cbVoz.Text = Settings.Default.vozDefault;
			this.cbRec.Text = Settings.Default.cfgRec.ToString();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00037470 File Offset: 0x00035670
		private void loadVoces()
		{
			try
			{
				foreach (InstalledVoice installedVoice in this.avjarvis.GetInstalledVoices())
				{
					this.cbVoz.Items.Add(installedVoice.VoiceInfo.Name);
				}
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00037500 File Offset: 0x00035700
		private void ocultarGrids()
		{
			this.gBienvenida.Visibility = Visibility.Hidden;
			this.gUsuario.Visibility = Visibility.Hidden;
			this.gAI.Visibility = Visibility.Hidden;
			this.gVoces.Visibility = Visibility.Hidden;
			this.gVolumen.Visibility = Visibility.Hidden;
			this.gOpaciodad.Visibility = Visibility.Hidden;
			this.gConfidencia.Visibility = Visibility.Hidden;
			this.gRec.Visibility = Visibility.Hidden;
			this.gWindows.Visibility = Visibility.Hidden;
			this.gHud.Visibility = Visibility.Hidden;
			this.gPreferencias.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000375A0 File Offset: 0x000357A0
		private void sliderConfidencia_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			System.Windows.Controls.Label label = this.labelConfidence;
			label.Content = Math.Round(this.sliderConfidencia.Value, 2).ToString();
			Settings.Default.Confidence = Math.Round(this.sliderConfidencia.Value, 2);
			Settings.Default.Save();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000375FC File Offset: 0x000357FC
		private void sliderOpacidad_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			bool flag = this.sliderOpacidad.Value > 0.5;
			if (flag)
			{
				this.LayoutRoot.Opacity = this.sliderOpacidad.Value;
				Settings.Default.opacidad = this.sliderOpacidad.Value;
				Settings.Default.Save();
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00037660 File Offset: 0x00035860
		private void sliderVolumen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			this.avjarvis.Volume = Convert.ToInt32(this.sliderVolumen.Value);
			Settings.Default.volumen = Convert.ToInt32(this.sliderVolumen.Value);
			Settings.Default.Save();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00002BDB File Offset: 0x00000DDB
		private void tbAi_TextChanged(object sender, TextChangedEventArgs e)
		{
			Settings.Default.cfgAi = this.tbAi.Text;
			Settings.Default.Save();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00002BFF File Offset: 0x00000DFF
		private void tbUser_TextChanged(object sender, TextChangedEventArgs e)
		{
			Settings.Default.cfgUser = this.tbUser.Text;
			Settings.Default.Save();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000020F8 File Offset: 0x000002F8
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnVoces_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnVolumen_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnConfidence_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnOpacidad_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnTiempo_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnWindows_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnHud_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnPref_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnUsuario_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000292A File Offset: 0x00000B2A
		private void btnAI_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.ShotgunShell_Land_Concrete_02.Play();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00002938 File Offset: 0x00000B38
		private void btnMini_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.Hover_main_button.Play();
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000376B0 File Offset: 0x000358B0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/A V®;component/confg.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000376E8 File Offset: 0x000358E8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.Window = (Confg)target;
				this.Window.MouseLeftButtonDown += this.Window_MouseLeftButtonDown;
				break;
			case 2:
				this.LayoutRoot = (Grid)target;
				break;
			case 3:
				this.imgHud = (Image)target;
				break;
			case 4:
				this.btnCloseNS = (System.Windows.Controls.Button)target;
				this.btnCloseNS.Click += this.btnCloseNS_Click;
				break;
			case 5:
				this.btnMini = (System.Windows.Controls.Button)target;
				this.btnMini.Click += this.btnMini_Click;
				break;
			case 6:
				this.rectangle = (Rectangle)target;
				break;
			case 7:
				this.btnConfidence = (System.Windows.Controls.Button)target;
				this.btnConfidence.Click += this.btnConfidence_Click;
				this.btnConfidence.MouseEnter += this.btnConfidence_MouseEnter;
				break;
			case 8:
				this.btnVoces = (System.Windows.Controls.Button)target;
				this.btnVoces.Click += this.btnVoces_Click;
				this.btnVoces.MouseEnter += this.btnVoces_MouseEnter;
				break;
			case 9:
				this.btnVolumen = (System.Windows.Controls.Button)target;
				this.btnVolumen.Click += this.btnVolumen_Click;
				this.btnVolumen.MouseEnter += this.btnVolumen_MouseEnter;
				break;
			case 10:
				this.btnOpacidad = (System.Windows.Controls.Button)target;
				this.btnOpacidad.Click += this.btnOpacidad_Click;
				this.btnOpacidad.MouseEnter += this.btnOpacidad_MouseEnter;
				break;
			case 11:
				this.btnTiempo = (System.Windows.Controls.Button)target;
				this.btnTiempo.Click += this.btnTiempo_Click;
				this.btnTiempo.MouseEnter += this.btnTiempo_MouseEnter;
				break;
			case 12:
				this.btnWindows = (System.Windows.Controls.Button)target;
				this.btnWindows.Click += this.btnWindows_Click;
				this.btnWindows.MouseEnter += this.btnWindows_MouseEnter;
				break;
			case 13:
				this.btnHud = (System.Windows.Controls.Button)target;
				this.btnHud.Click += this.btnHud_Click;
				this.btnHud.MouseEnter += this.btnHud_MouseEnter;
				break;
			case 14:
				this.btnPref = (System.Windows.Controls.Button)target;
				this.btnPref.Click += this.btnPref_Click;
				this.btnPref.MouseEnter += this.btnPref_MouseEnter;
				break;
			case 15:
				this.gPreferencias = (Grid)target;
				break;
			case 16:
				this.chbAR = (System.Windows.Controls.CheckBox)target;
				this.chbAR.Checked += this.chbAR_Checked;
				this.chbAR.Unchecked += this.chbAR_Unchecked;
				break;
			case 17:
				this.chbCB = (System.Windows.Controls.CheckBox)target;
				this.chbCB.Checked += this.chbCB_Checked;
				this.chbCB.Unchecked += this.chbCB_Unchecked;
				break;
			case 18:
				this.chbCC = (System.Windows.Controls.CheckBox)target;
				this.chbCC.Checked += this.chbCC_Checked;
				this.chbCC.Unchecked += this.chbCC_Unchecked;
				break;
			case 19:
				this.chbCA = (System.Windows.Controls.CheckBox)target;
				this.chbCA.Checked += this.chbCA_Checked;
				this.chbCA.Unchecked += this.chbCA_Unchecked;
				break;
			case 20:
				this.chbCR = (System.Windows.Controls.CheckBox)target;
				this.chbCR.Checked += this.chbCR_Checked;
				this.chbCR.Unchecked += this.chbCR_Unchecked;
				break;
			case 21:
				this.chbTA = (System.Windows.Controls.CheckBox)target;
				this.chbTA.Checked += this.chbTA_Checked;
				this.chbTA.Unchecked += this.chbTA_Unchecked;
				break;
			case 22:
				this.textBlock = (TextBlock)target;
				break;
			case 23:
				this.chbReprod = (System.Windows.Controls.CheckBox)target;
				this.chbReprod.Checked += this.chbReprod_Checked;
				this.chbReprod.Unchecked += this.chbReprod_Unchecked;
				break;
			case 24:
				this.chbcoma = (System.Windows.Controls.CheckBox)target;
				this.chbcoma.Checked += this.chbcoma_Checked;
				this.chbcoma.Unchecked += this.chbcoma_Unchecked;
				break;
			case 25:
				this.gUsuario = (Grid)target;
				break;
			case 26:
				this.tbUser = (System.Windows.Controls.TextBox)target;
				break;
			case 27:
				this.tbInfoUser = (TextBlock)target;
				break;
			case 28:
				this.gAI = (Grid)target;
				break;
			case 29:
				this.tbAi = (System.Windows.Controls.TextBox)target;
				break;
			case 30:
				this.tbInfoAI = (TextBlock)target;
				break;
			case 31:
				this.gVolumen = (Grid)target;
				break;
			case 32:
				this.sliderVolumen = (Slider)target;
				break;
			case 33:
				this.tbInfoVol = (TextBlock)target;
				break;
			case 34:
				this.gConfidencia = (Grid)target;
				break;
			case 35:
				this.sliderConfidencia = (Slider)target;
				break;
			case 36:
				this.labelConfidence = (System.Windows.Controls.Label)target;
				break;
			case 37:
				this.tbInfoConf = (TextBlock)target;
				break;
			case 38:
				this.gOpaciodad = (Grid)target;
				break;
			case 39:
				this.sliderOpacidad = (Slider)target;
				break;
			case 40:
				this.tbInfoOpa = (TextBlock)target;
				break;
			case 41:
				this.gRec = (Grid)target;
				break;
			case 42:
				this.cbRec = (System.Windows.Controls.ComboBox)target;
				break;
			case 43:
				this.tbInfoRec = (TextBlock)target;
				break;
			case 44:
				this.gVoces = (Grid)target;
				break;
			case 45:
				this.cbVoz = (System.Windows.Controls.ComboBox)target;
				this.cbVoz.DropDownClosed += this.cbVoz_DropDownClosed;
				break;
			case 46:
				this.tbInfoVoz = (TextBlock)target;
				break;
			case 47:
				this.gHud = (Grid)target;
				break;
			case 48:
				this.cbHud = (System.Windows.Controls.CheckBox)target;
				this.cbHud.Checked += this.cbHud_Checked;
				this.cbHud.Unchecked += this.cbHud_Unchecked;
				break;
			case 49:
				this.tbInfoHud = (TextBlock)target;
				break;
			case 50:
				this.gWindows = (Grid)target;
				break;
			case 51:
				this.cbWin = (System.Windows.Controls.CheckBox)target;
				this.cbWin.Checked += this.cbWin_Checked;
				this.cbWin.Unchecked += this.cbWin_Unchecked;
				break;
			case 52:
				this.tbInfoWin = (TextBlock)target;
				break;
			case 53:
				this.gBienvenida = (Grid)target;
				break;
			case 54:
				this.tbInfoWin_Copy = (TextBlock)target;
				break;
			case 55:
				this.btnUsuario = (System.Windows.Controls.Button)target;
				this.btnUsuario.Click += this.btnUsuario_Click;
				this.btnUsuario.MouseEnter += this.btnUsuario_MouseEnter;
				break;
			case 56:
				this.btnAI = (System.Windows.Controls.Button)target;
				this.btnAI.Click += this.btnAI_Click;
				this.btnAI.MouseEnter += this.btnAI_MouseEnter;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x040001E7 RID: 487
		private SpeechSynthesizer avjarvis = new SpeechSynthesizer();

		// Token: 0x040001E8 RID: 488
		private RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

		// Token: 0x040001E9 RID: 489
		internal Confg Window;

		// Token: 0x040001EA RID: 490
		internal Grid LayoutRoot;

		// Token: 0x040001EB RID: 491
		internal Image imgHud;

		// Token: 0x040001EC RID: 492
		internal System.Windows.Controls.Button btnCloseNS;

		// Token: 0x040001ED RID: 493
		internal System.Windows.Controls.Button btnMini;

		// Token: 0x040001EE RID: 494
		internal Rectangle rectangle;

		// Token: 0x040001EF RID: 495
		internal System.Windows.Controls.Button btnConfidence;

		// Token: 0x040001F0 RID: 496
		internal System.Windows.Controls.Button btnVoces;

		// Token: 0x040001F1 RID: 497
		internal System.Windows.Controls.Button btnVolumen;

		// Token: 0x040001F2 RID: 498
		internal System.Windows.Controls.Button btnOpacidad;

		// Token: 0x040001F3 RID: 499
		internal System.Windows.Controls.Button btnTiempo;

		// Token: 0x040001F4 RID: 500
		internal System.Windows.Controls.Button btnWindows;

		// Token: 0x040001F5 RID: 501
		internal System.Windows.Controls.Button btnHud;

		// Token: 0x040001F6 RID: 502
		internal System.Windows.Controls.Button btnPref;

		// Token: 0x040001F7 RID: 503
		internal Grid gPreferencias;

		// Token: 0x040001F8 RID: 504
		internal System.Windows.Controls.CheckBox chbAR;

		// Token: 0x040001F9 RID: 505
		internal System.Windows.Controls.CheckBox chbCB;

		// Token: 0x040001FA RID: 506
		internal System.Windows.Controls.CheckBox chbCC;

		// Token: 0x040001FB RID: 507
		internal System.Windows.Controls.CheckBox chbCA;

		// Token: 0x040001FC RID: 508
		internal System.Windows.Controls.CheckBox chbCR;

		// Token: 0x040001FD RID: 509
		internal System.Windows.Controls.CheckBox chbTA;

		// Token: 0x040001FE RID: 510
		internal TextBlock textBlock;

		// Token: 0x040001FF RID: 511
		internal System.Windows.Controls.CheckBox chbReprod;

		// Token: 0x04000200 RID: 512
		internal System.Windows.Controls.CheckBox chbcoma;

		// Token: 0x04000201 RID: 513
		internal Grid gUsuario;

		// Token: 0x04000202 RID: 514
		internal System.Windows.Controls.TextBox tbUser;

		// Token: 0x04000203 RID: 515
		internal TextBlock tbInfoUser;

		// Token: 0x04000204 RID: 516
		internal Grid gAI;

		// Token: 0x04000205 RID: 517
		internal System.Windows.Controls.TextBox tbAi;

		// Token: 0x04000206 RID: 518
		internal TextBlock tbInfoAI;

		// Token: 0x04000207 RID: 519
		internal Grid gVolumen;

		// Token: 0x04000208 RID: 520
		internal Slider sliderVolumen;

		// Token: 0x04000209 RID: 521
		internal TextBlock tbInfoVol;

		// Token: 0x0400020A RID: 522
		internal Grid gConfidencia;

		// Token: 0x0400020B RID: 523
		internal Slider sliderConfidencia;

		// Token: 0x0400020C RID: 524
		internal System.Windows.Controls.Label labelConfidence;

		// Token: 0x0400020D RID: 525
		internal TextBlock tbInfoConf;

		// Token: 0x0400020E RID: 526
		internal Grid gOpaciodad;

		// Token: 0x0400020F RID: 527
		internal Slider sliderOpacidad;

		// Token: 0x04000210 RID: 528
		internal TextBlock tbInfoOpa;

		// Token: 0x04000211 RID: 529
		internal Grid gRec;

		// Token: 0x04000212 RID: 530
		internal System.Windows.Controls.ComboBox cbRec;

		// Token: 0x04000213 RID: 531
		internal TextBlock tbInfoRec;

		// Token: 0x04000214 RID: 532
		internal Grid gVoces;

		// Token: 0x04000215 RID: 533
		internal System.Windows.Controls.ComboBox cbVoz;

		// Token: 0x04000216 RID: 534
		internal TextBlock tbInfoVoz;

		// Token: 0x04000217 RID: 535
		internal Grid gHud;

		// Token: 0x04000218 RID: 536
		internal System.Windows.Controls.CheckBox cbHud;

		// Token: 0x04000219 RID: 537
		internal TextBlock tbInfoHud;

		// Token: 0x0400021A RID: 538
		internal Grid gWindows;

		// Token: 0x0400021B RID: 539
		internal System.Windows.Controls.CheckBox cbWin;

		// Token: 0x0400021C RID: 540
		internal TextBlock tbInfoWin;

		// Token: 0x0400021D RID: 541
		internal Grid gBienvenida;

		// Token: 0x0400021E RID: 542
		internal TextBlock tbInfoWin_Copy;

		// Token: 0x0400021F RID: 543
		internal System.Windows.Controls.Button btnUsuario;

		// Token: 0x04000220 RID: 544
		internal System.Windows.Controls.Button btnAI;

		// Token: 0x04000221 RID: 545
		private bool _contentLoaded;
	}
}
