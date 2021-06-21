using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Windows.Threading;
using AVJARVISB4.Clases;
using AVJARVISB4.Properties;

namespace AVJARVISB4
{
	// Token: 0x02000005 RID: 5
	public class Alarm : Window, IComponentConnector
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00003A70 File Offset: 0x00001C70
		public Alarm()
		{
			this.InitializeComponent();
			this.IniciarTimer();
			this.LoadData();
			SoundEffects.Somefects();
			this.sSetAlarm.Visibility = Visibility.Hidden;
			Rect workArea = SystemParameters.WorkArea;
			base.Left = workArea.Right - base.Width;
			base.Top = workArea.Bottom - base.Height;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000020E3 File Offset: 0x000002E3
		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ClickClose_menu_hide.Play();
			base.Close();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003B1C File Offset: 0x00001D1C
		private void btnFile_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ClickOpen_import_export.Play();
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "Arquivos de musica(.wav, .midi, .aac, .wma, .ogg, .mp3)|*.wav; *.midi; *.aac; *.wma; *.ogg; *.mp3|Aplicativos(.exe)|*.exe|Todos os arquivos|*.*",
				Title = "Arquivos mp3",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)
			};
			bool flag = openFileDialog.ShowDialog().ToString().Equals("OK");
			if (flag)
			{
				this.btnFile.ToolTip = openFileDialog.FileName;
				Settings.Default.RutaAlarma = openFileDialog.FileName;
				Settings.Default.Save();
			}
			openFileDialog.Dispose();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003BBC File Offset: 0x00001DBC
		private void cbDo_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cDom = (this.cbDo.IsChecked != null && this.cbDo.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003BBC File Offset: 0x00001DBC
		private void cbDo_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cDom = (this.cbDo.IsChecked != null && this.cbDo.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003C0C File Offset: 0x00001E0C
		private void cbJu_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cJue = (this.cbJu.IsChecked != null && this.cbJu.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003C0C File Offset: 0x00001E0C
		private void cbJu_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cJue = (this.cbJu.IsChecked != null && this.cbJu.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003C5C File Offset: 0x00001E5C
		private void cbLu_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cLun = (this.cbLu.IsChecked != null && this.cbLu.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003C5C File Offset: 0x00001E5C
		private void cbLu_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cLun = (this.cbLu.IsChecked != null && this.cbLu.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003CAC File Offset: 0x00001EAC
		private void cbMa_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cMar = (this.cbMa.IsChecked != null && this.cbMa.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003CAC File Offset: 0x00001EAC
		private void cbMa_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cMar = (this.cbMa.IsChecked != null && this.cbMa.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003CFC File Offset: 0x00001EFC
		private void cbMi_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cMie = (this.cbMi.IsChecked != null && this.cbMi.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003CFC File Offset: 0x00001EFC
		private void cbMi_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cMie = (this.cbMi.IsChecked != null && this.cbMi.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003D4C File Offset: 0x00001F4C
		private void cbSa_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cSab = (this.cbSa.IsChecked != null && this.cbSa.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003D4C File Offset: 0x00001F4C
		private void cbSa_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cSab = (this.cbSa.IsChecked != null && this.cbSa.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003D9C File Offset: 0x00001F9C
		private void cbVi_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cVie = (this.cbVi.IsChecked != null && this.cbVi.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003D9C File Offset: 0x00001F9C
		private void cbVi_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cVie = (this.cbVi.IsChecked != null && this.cbVi.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003DEC File Offset: 0x00001FEC
		private void cbApagarPC_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.tApagado = (this.cbApagarPC.IsChecked != null && this.cbApagarPC.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003E3C File Offset: 0x0000203C
		private void cbReiniciarPc_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.tReinicar = (this.cbReiniciarPc.IsChecked != null && this.cbReiniciarPc.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003E8C File Offset: 0x0000208C
		private void cbSuspender_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.tSuspender = (this.cbSuspender.IsChecked != null && this.cbSuspender.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003EDC File Offset: 0x000020DC
		private void cbCerrarSesion_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.tSesion = (this.cbCerrarSesion.IsChecked != null && this.cbCerrarSesion.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003DEC File Offset: 0x00001FEC
		private void cbApagarPC_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.tApagado = (this.cbApagarPC.IsChecked != null && this.cbApagarPC.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003E3C File Offset: 0x0000203C
		private void cbReiniciarPc_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.tReinicar = (this.cbReiniciarPc.IsChecked != null && this.cbReiniciarPc.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003E8C File Offset: 0x0000208C
		private void cbSuspender_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.tSuspender = (this.cbSuspender.IsChecked != null && this.cbSuspender.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003EDC File Offset: 0x000020DC
		private void cbCerrarSesion_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.tSesion = (this.cbCerrarSesion.IsChecked != null && this.cbCerrarSesion.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003F2C File Offset: 0x0000212C
		private void TbDisp_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.tMusica = (this.TbDisp.IsChecked != null && this.TbDisp.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003F2C File Offset: 0x0000212C
		private void TbDisp_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.tMusica = (this.TbDisp.IsChecked != null && this.TbDisp.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003F7C File Offset: 0x0000217C
		private void IniciarTimer()
		{
			this.TimerAlarm = new DispatcherTimer
			{
				Interval = new TimeSpan(0, 0, 1)
			};
			this.TimerAlarm.Tick += this.TimerAlarm_Tick;
			this.TimerAlarm.Start();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003FC8 File Offset: 0x000021C8
		private void lblAlarm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			bool flag = this.sSetAlarm.Visibility > Visibility.Visible;
			if (flag)
			{
				this.sSetAlarm.Visibility = Visibility.Visible;
			}
			else
			{
				this.sSetAlarm.Visibility = Visibility.Hidden;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00004008 File Offset: 0x00002208
		public void LoadData()
		{
			this.cbDo.IsChecked = new bool?(Settings.Default.cDom);
			this.cbLu.IsChecked = new bool?(Settings.Default.cLun);
			this.cbMa.IsChecked = new bool?(Settings.Default.cMar);
			this.cbMi.IsChecked = new bool?(Settings.Default.cMie);
			this.cbJu.IsChecked = new bool?(Settings.Default.cJue);
			this.cbVi.IsChecked = new bool?(Settings.Default.cVie);
			this.cbSa.IsChecked = new bool?(Settings.Default.cSab);
			this.TbDisp.IsChecked = new bool?(Settings.Default.tMusica);
			this.cbApagarPC.IsChecked = new bool?(Settings.Default.tApagado);
			this.cbReiniciarPc.IsChecked = new bool?(Settings.Default.tReinicar);
			this.cbSuspender.IsChecked = new bool?(Settings.Default.tSuspender);
			this.cbCerrarSesion.IsChecked = new bool?(Settings.Default.tSesion);
			this.btnFile.ToolTip = Settings.Default.RutaAlarma;
			this.tbHora.Text = this.horaAlarma;
			this.tbMin.Text = this.minutoAlarma;
			this.tbHorario.Text = this.periodoAlarma;
			this.lblAlarm.Content = string.Concat(new string[]
			{
				this.horaAlarma,
				":",
				this.minutoAlarma,
				" ",
				this.periodoAlarma
			});
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000041E8 File Offset: 0x000023E8
		private void tbHora_TextChanged(object sender, TextChangedEventArgs e)
		{
			bool flag = this.tbHora.Text != "";
			if (flag)
			{
				try
				{
					this.horaSet = Convert.ToInt32(this.tbHora.Text);
					bool flag2 = this.horaSet >= 0 && this.horaSet <= 12;
					if (flag2)
					{
						this.horaAlarma = Convert.ToString(this.horaSet);
					}
					else
					{
						System.Windows.MessageBox.Show("apenas números de 1 a 12 são suportados");
						this.tbHora.Text = this.horaAlarma;
					}
				}
				catch
				{
					System.Windows.MessageBox.Show("apenas números são suportados");
					this.tbHora.Text = this.horaAlarma;
				}
				Settings.Default.tHora = this.horaAlarma;
				Settings.Default.Save();
				this.lblAlarm.Content = string.Concat(new string[]
				{
					this.horaAlarma,
					":",
					this.minutoAlarma,
					" ",
					this.periodoAlarma
				});
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004310 File Offset: 0x00002510
		private void tbHorario_TextChanged(object sender, TextChangedEventArgs e)
		{
			bool flag = !(this.tbHorario.Text == "") && this.tbHorario.Text.Length > 1;
			if (flag)
			{
				bool flag2 = !(this.tbHorario.Text.ToLower().Trim() == "am") && this.tbHorario.Text.ToLower().Trim() != "pm";
				if (flag2)
				{
					System.Windows.MessageBox.Show("basta digitar am ou pm");
				}
				else
				{
					this.periodoAlarma = this.tbHorario.Text.ToLower().Trim();
				}
				Settings.Default.Periodo = this.periodoAlarma;
				Settings.Default.Save();
				this.lblAlarm.Content = string.Concat(new string[]
				{
					this.horaAlarma,
					":",
					this.minutoAlarma,
					" ",
					this.periodoAlarma
				});
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004428 File Offset: 0x00002628
		private void tbMin_TextChanged(object sender, TextChangedEventArgs e)
		{
			bool flag = this.tbMin.Text != "";
			if (flag)
			{
				try
				{
					this.minutoSet = Convert.ToInt32(this.tbMin.Text);
					bool flag2 = this.minutoSet >= 0 && this.minutoSet <= 59;
					if (flag2)
					{
						this.minutoAlarma = Convert.ToString(this.minutoSet);
						bool flag3 = this.minutoAlarma.Length == 1;
						if (flag3)
						{
							this.minutoAlarma = "0" + this.minutoAlarma;
						}
					}
					else
					{
						System.Windows.MessageBox.Show("apenas números de 1 a 60");
						this.tbMin.Text = this.minutoAlarma;
					}
				}
				catch
				{
					System.Windows.MessageBox.Show("apenas números são suportados");
					this.tbMin.Text = this.minutoAlarma;
				}
				Settings.Default.tMin = this.minutoAlarma;
				Settings.Default.Save();
				this.lblAlarm.Content = string.Concat(new string[]
				{
					this.horaAlarma,
					":",
					this.minutoAlarma,
					" ",
					this.periodoAlarma
				});
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000457C File Offset: 0x0000277C
		private static void Tdeslingar()
		{
			bool tApagado = Settings.Default.tApagado;
			if (tApagado)
			{
				Process.Start("shutdown.exe", "-s -t 1");
			}
			else
			{
				bool tReinicar = Settings.Default.tReinicar;
				if (tReinicar)
				{
					Process.Start("shutdown.exe", "-r -t 1");
				}
				else
				{
					bool tSuspender = Settings.Default.tSuspender;
					if (tSuspender)
					{
						Process.Start("shutdown.exe", "-h");
					}
					else
					{
						bool tSesion = Settings.Default.tSesion;
						if (tSesion)
						{
							Process.Start("shutdown.exe", "-L");
						}
					}
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004610 File Offset: 0x00002810
		private void TimerAlarm_Tick(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			this.lblHora.Content = now.ToString("h:mm tt", new CultureInfo("en-US"));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004648 File Offset: 0x00002848
		public static void checkAlarma(string hora, string dia)
		{
			string[] array = hora.Split(new char[]
			{
				':'
			});
			bool flag = (!(dia == "segunda-feira") || !Settings.Default.cLun) && (!(dia == "terça-feira") || !Settings.Default.cMar) && (!(dia == "quarta-feira") || !Settings.Default.cMie) && (!(dia == "quinta-feira") || !Settings.Default.cJue) && (!(dia == "sexta-feira") || !Settings.Default.cVie) && (!(dia == "sábado") || !Settings.Default.cSab);
			bool flag2 = !flag || (!(dia != "domingo") && Settings.Default.cDom);
			bool flag3 = flag2;
			if (flag3)
			{
				bool flag4 = Settings.Default.tHora == array[0];
				if (flag4)
				{
					bool flag5 = Settings.Default.tMin == array[1];
					if (flag5)
					{
						string[] array2 = array[2].Split(new char[]
						{
							' '
						});
						bool flag6 = array2[0] == "00";
						if (flag6)
						{
							bool flag7 = Settings.Default.Periodo == array2[1].ToLower();
							if (flag7)
							{
								bool tMusica = Settings.Default.tMusica;
								if (tMusica)
								{
									try
									{
										Process.Start(Settings.Default.RutaAlarma);
										Alarm.Tdeslingar();
									}
									catch (Exception)
									{
										try
										{
											SoundEffects.sonidoAlarm.PlayLooping();
											Notificacion notificacion = new Notificacion();
											notificacion.lblTitulo.Content = string.Concat(new string[]
											{
												"AV"
											});
											notificacion.txtNota.Text = string.Concat(new string[]
											{
												"O Alarme foi programado para Dispertar hoje"
											});
											notificacion.Show();
											notificacion.Closed += delegate(object a, EventArgs b)
											{
												SoundEffects.sonidoAlarm.Stop();
												notificacion = null;
											};
											Alarm.Tdeslingar();
										}
										catch (Exception)
										{
											System.Windows.MessageBox.Show("Não encontro o som do alarme. Por favor selecione uma música");
										}
									}
								}
								else
								{
									bool flag8 = !Settings.Default.tMusica;
									if (flag8)
									{
										try
										{
											SoundEffects.sonidoAlarm.PlayLooping();
											Notificacion notificacion = new Notificacion();
											notificacion.lblTitulo.Content = string.Concat(new string[]
											{
												"AV"
											});
											notificacion.txtNota.Text = string.Concat(new string[]
											{
												"O Alarme foi programado para Dispertar hoje"
											});
											notificacion.Show();
											notificacion.Closed += delegate(object a, EventArgs b)
											{
												SoundEffects.sonidoAlarm.Stop();
												notificacion = null;
											};
											Alarm.Tdeslingar();
										}
										catch (Exception)
										{
											System.Windows.MessageBox.Show("Não encontro o som do alarme. Por favor selecione uma música");
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000020F8 File Offset: 0x000002F8
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004994 File Offset: 0x00002B94
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/A V®;component/alarm.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000049CC File Offset: 0x00002BCC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.Window = (Alarm)target;
				this.Window.MouseLeftButtonDown += this.Window_MouseLeftButtonDown;
				break;
			case 2:
				this.LayoutRoot = (Grid)target;
				break;
			case 3:
				this.skin1 = (Grid)target;
				break;
			case 4:
				this.Sobreado = (Ellipse)target;
				break;
			case 5:
				this.Aro1 = (Ellipse)target;
				break;
			case 6:
				this.Aro2 = (Ellipse)target;
				break;
			case 7:
				this.Hud = (Ellipse)target;
				break;
			case 8:
				this.lblHora = (System.Windows.Controls.Label)target;
				break;
			case 9:
				this.btnFile = (System.Windows.Controls.Button)target;
				this.btnFile.Click += this.btnFile_Click;
				break;
			case 10:
				this.lblAlarm = (System.Windows.Controls.Label)target;
				this.lblAlarm.MouseLeftButtonDown += this.lblAlarm_MouseLeftButtonDown;
				break;
			case 11:
				this.cbDo = (System.Windows.Controls.CheckBox)target;
				this.cbDo.Checked += this.cbDo_Checked;
				this.cbDo.Unchecked += this.cbDo_Unchecked;
				break;
			case 12:
				this.cbLu = (System.Windows.Controls.CheckBox)target;
				this.cbLu.Checked += this.cbLu_Checked;
				this.cbLu.Unchecked += this.cbLu_Unchecked;
				break;
			case 13:
				this.cbMa = (System.Windows.Controls.CheckBox)target;
				this.cbMa.Checked += this.cbMa_Checked;
				this.cbMa.Unchecked += this.cbMa_Unchecked;
				break;
			case 14:
				this.cbMi = (System.Windows.Controls.CheckBox)target;
				this.cbMi.Checked += this.cbMi_Checked;
				this.cbMi.Unchecked += this.cbMi_Unchecked;
				break;
			case 15:
				this.cbJu = (System.Windows.Controls.CheckBox)target;
				this.cbJu.Checked += this.cbJu_Checked;
				this.cbJu.Unchecked += this.cbJu_Unchecked;
				break;
			case 16:
				this.cbVi = (System.Windows.Controls.CheckBox)target;
				this.cbVi.Checked += this.cbVi_Checked;
				this.cbVi.Unchecked += this.cbVi_Unchecked;
				break;
			case 17:
				this.cbSa = (System.Windows.Controls.CheckBox)target;
				this.cbSa.Checked += this.cbSa_Checked;
				this.cbSa.Unchecked += this.cbSa_Unchecked;
				break;
			case 18:
				this.btnClose = (System.Windows.Controls.Button)target;
				this.btnClose.Click += this.btnClose_Click;
				break;
			case 19:
				this.cbApagarPC = (System.Windows.Controls.RadioButton)target;
				this.cbApagarPC.Checked += this.cbApagarPC_Checked;
				this.cbApagarPC.Unchecked += this.cbApagarPC_Unchecked;
				break;
			case 20:
				this.cbSuspender = (System.Windows.Controls.RadioButton)target;
				this.cbSuspender.Checked += this.cbSuspender_Checked;
				this.cbSuspender.Unchecked += this.cbSuspender_Unchecked;
				break;
			case 21:
				this.cbCerrarSesion = (System.Windows.Controls.RadioButton)target;
				this.cbCerrarSesion.Checked += this.cbCerrarSesion_Checked;
				this.cbCerrarSesion.Unchecked += this.cbCerrarSesion_Unchecked;
				break;
			case 22:
				this.cbativarAlarme = (System.Windows.Controls.RadioButton)target;
				break;
			case 23:
				this.cbReiniciarPc = (System.Windows.Controls.RadioButton)target;
				this.cbReiniciarPc.Checked += this.cbReiniciarPc_Checked;
				this.cbReiniciarPc.Unchecked += this.cbReiniciarPc_Unchecked;
				break;
			case 24:
				this.TbDisp = (System.Windows.Controls.CheckBox)target;
				this.TbDisp.Checked += this.TbDisp_Checked;
				this.TbDisp.Unchecked += this.TbDisp_Unchecked;
				break;
			case 25:
				this.sSetAlarm = (Grid)target;
				break;
			case 26:
				this.tbHora = (System.Windows.Controls.TextBox)target;
				this.tbHora.TextChanged += this.tbHora_TextChanged;
				break;
			case 27:
				this.tbMin = (System.Windows.Controls.TextBox)target;
				this.tbMin.TextChanged += this.tbMin_TextChanged;
				break;
			case 28:
				this.tbHorario = (System.Windows.Controls.TextBox)target;
				this.tbHorario.TextChanged += this.tbHorario_TextChanged;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x04000009 RID: 9
		private DispatcherTimer TimerAlarm;

		// Token: 0x0400000A RID: 10
		private string horaAlarma = Settings.Default.tHora;

		// Token: 0x0400000B RID: 11
		private string minutoAlarma = Settings.Default.tMin;

		// Token: 0x0400000C RID: 12
		private string periodoAlarma = Settings.Default.Periodo;

		// Token: 0x0400000D RID: 13
		private int horaSet = 0;

		// Token: 0x0400000E RID: 14
		private int minutoSet = 0;

		// Token: 0x0400000F RID: 15
		internal Alarm Window;

		// Token: 0x04000010 RID: 16
		internal Grid LayoutRoot;

		// Token: 0x04000011 RID: 17
		internal Grid skin1;

		// Token: 0x04000012 RID: 18
		internal Ellipse Sobreado;

		// Token: 0x04000013 RID: 19
		internal Ellipse Aro1;

		// Token: 0x04000014 RID: 20
		internal Ellipse Aro2;

		// Token: 0x04000015 RID: 21
		internal Ellipse Hud;

		// Token: 0x04000016 RID: 22
		internal System.Windows.Controls.Label lblHora;

		// Token: 0x04000017 RID: 23
		internal System.Windows.Controls.Button btnFile;

		// Token: 0x04000018 RID: 24
		internal System.Windows.Controls.Label lblAlarm;

		// Token: 0x04000019 RID: 25
		internal System.Windows.Controls.CheckBox cbDo;

		// Token: 0x0400001A RID: 26
		internal System.Windows.Controls.CheckBox cbLu;

		// Token: 0x0400001B RID: 27
		internal System.Windows.Controls.CheckBox cbMa;

		// Token: 0x0400001C RID: 28
		internal System.Windows.Controls.CheckBox cbMi;

		// Token: 0x0400001D RID: 29
		internal System.Windows.Controls.CheckBox cbJu;

		// Token: 0x0400001E RID: 30
		internal System.Windows.Controls.CheckBox cbVi;

		// Token: 0x0400001F RID: 31
		internal System.Windows.Controls.CheckBox cbSa;

		// Token: 0x04000020 RID: 32
		internal System.Windows.Controls.Button btnClose;

		// Token: 0x04000021 RID: 33
		internal System.Windows.Controls.RadioButton cbApagarPC;

		// Token: 0x04000022 RID: 34
		internal System.Windows.Controls.RadioButton cbSuspender;

		// Token: 0x04000023 RID: 35
		internal System.Windows.Controls.RadioButton cbCerrarSesion;

		// Token: 0x04000024 RID: 36
		internal System.Windows.Controls.RadioButton cbativarAlarme;

		// Token: 0x04000025 RID: 37
		internal System.Windows.Controls.RadioButton cbReiniciarPc;

		// Token: 0x04000026 RID: 38
		internal System.Windows.Controls.CheckBox TbDisp;

		// Token: 0x04000027 RID: 39
		internal Grid sSetAlarm;

		// Token: 0x04000028 RID: 40
		internal System.Windows.Controls.TextBox tbHora;

		// Token: 0x04000029 RID: 41
		internal System.Windows.Controls.TextBox tbMin;

		// Token: 0x0400002A RID: 42
		internal System.Windows.Controls.TextBox tbHorario;

		// Token: 0x0400002B RID: 43
		private bool _contentLoaded;
	}
}
