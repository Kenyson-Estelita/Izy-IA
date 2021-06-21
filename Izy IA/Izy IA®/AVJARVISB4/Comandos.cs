using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using AVJARVISB4.Clases;
using AVJARVISB4.Properties;
using Jarvis.DataBaseManager;

namespace AVJARVISB4
{
	// Token: 0x02000019 RID: 25
	public class Comandos : Window, IComponentConnector
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00035564 File Offset: 0x00033764
		public Comandos()
		{
			this.InitializeComponent();
			this.mostrarTabla(this.NAME_TABLA_APLICACIONES);
			SoundEffects.Somefects();
			this.LayoutRoot.Opacity = Settings.Default.opacidad;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00035600 File Offset: 0x00033800
		private void Actualizartabla()
		{
			bool flag = this.NAME_TABLA_AHORA == this.NAME_TABLA_SOCIALES;
			if (flag)
			{
				this.mostrarTabla(this.NAME_TABLA_SOCIALES);
			}
			else
			{
				bool flag2 = this.NAME_TABLA_AHORA == this.NAME_TABLA_APLICACIONES;
				if (flag2)
				{
					this.mostrarTabla(this.NAME_TABLA_APLICACIONES);
				}
				else
				{
					bool flag3 = this.NAME_TABLA_AHORA == this.NAME_TABLA_CARPETAS;
					if (flag3)
					{
						this.mostrarTabla(this.NAME_TABLA_CARPETAS);
					}
					else
					{
						bool flag4 = this.NAME_TABLA_AHORA == this.NAME_TABLA_WEBS;
						if (flag4)
						{
							this.mostrarTabla(this.NAME_TABLA_WEBS);
						}
					}
				}
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000356A4 File Offset: 0x000338A4
		private void btnProgramas_Click(object sender, RoutedEventArgs e)
		{
			this.lblResp.Content = "RESPOSTA +";
			this.lblCmd.Content = "COMANDO +";
			this.mostrarTabla(this.NAME_TABLA_APLICACIONES);
			this.DataGridP.Columns[0].Visibility = Visibility.Hidden;
			this.lblEjec.Visibility = Visibility.Visible;
			this.btnSearch.Visibility = Visibility.Visible;
			this.tbEje.Visibility = Visibility.Visible;
			bool flag = this.btnInsert.Visibility > Visibility.Visible;
			if (flag)
			{
				this.btnNuevo.IsEnabled = true;
				this.btnNuevo.Visibility = Visibility.Visible;
			}
			SoundEffects.ClickOpen_menu_show.Play();
			this.btnEliminar.IsEnabled = true;
			this.btnEliminar.Visibility = Visibility.Visible;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00035774 File Offset: 0x00033974
		private void btnCarpetas_Click(object sender, RoutedEventArgs e)
		{
			this.lblResp.Content = "RESPOSTA +";
			this.lblCmd.Content = "COMANDO +";
			SoundEffects.ClickOpen_menu_show.Play();
			this.mostrarTabla(this.NAME_TABLA_CARPETAS);
			this.DataGridP.Columns[0].Visibility = Visibility.Hidden;
			this.lblEjec.Visibility = Visibility.Visible;
			this.btnSearch.Visibility = Visibility.Visible;
			this.tbEje.Visibility = Visibility.Visible;
			this.btnNuevo.IsEnabled = true;
			bool flag = this.btnInsert.Visibility > Visibility.Visible;
			if (flag)
			{
				this.btnNuevo.IsEnabled = true;
				this.btnNuevo.Visibility = Visibility.Visible;
			}
			this.btnEliminar.IsEnabled = true;
			this.btnEliminar.Visibility = Visibility.Visible;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00035850 File Offset: 0x00033A50
		private void btnWebs_Click(object sender, RoutedEventArgs e)
		{
			this.lblResp.Content = "RESPOSTA +";
			this.lblCmd.Content = "COMANDO +";
			SoundEffects.ClickOpen_menu_show.Play();
			this.mostrarTabla(this.NAME_TABLA_WEBS);
			this.DataGridP.Columns[0].Visibility = Visibility.Hidden;
			this.lblEjec.Visibility = Visibility.Visible;
			this.btnSearch.Visibility = Visibility.Hidden;
			this.tbEje.Visibility = Visibility.Visible;
			bool flag = this.btnInsert.Visibility > Visibility.Visible;
			if (flag)
			{
				this.btnNuevo.IsEnabled = true;
				this.btnNuevo.Visibility = Visibility.Visible;
			}
			this.btnEliminar.IsEnabled = true;
			this.btnEliminar.Visibility = Visibility.Visible;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00035920 File Offset: 0x00033B20
		private void btnSociales_Click(object sender, RoutedEventArgs e)
		{
			this.lblResp.Content = "RESPOSTA +";
			this.lblCmd.Content = "COMANDO +";
			SoundEffects.ClickOpen_menu_show.Play();
			this.mostrarTabla(this.NAME_TABLA_SOCIALES);
			this.DataGridP.Columns[0].Visibility = Visibility.Hidden;
			this.lblEjec.Visibility = Visibility.Hidden;
			this.btnSearch.Visibility = Visibility.Hidden;
			this.tbEje.Visibility = Visibility.Hidden;
			bool flag = this.btnInsert.Visibility > Visibility.Visible;
			if (flag)
			{
				this.btnNuevo.IsEnabled = true;
				this.btnNuevo.Visibility = Visibility.Visible;
			}
			this.btnEliminar.IsEnabled = true;
			this.btnEliminar.Visibility = Visibility.Visible;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000359F0 File Offset: 0x00033BF0
		private void btnInternos_Click(object sender, RoutedEventArgs e)
		{
			this.lblResp.Content = "SINÓNIMO +";
			this.lblCmd.Content = "COMANDO";
			SoundEffects.Click_settings_arrow.Play();
			this.mostrarTabla(this.NAME_TABLA_DEFAULT);
			this.DataGridP.Columns[0].Visibility = Visibility.Hidden;
			this.lblEjec.Visibility = Visibility.Visible;
			this.btnNuevo.IsEnabled = false;
			this.btnNuevo.Visibility = Visibility.Hidden;
			this.btnInsert.Visibility = Visibility.Hidden;
			this.gridAddCmds.Visibility = Visibility.Hidden;
			this.btnEliminar.IsEnabled = false;
			this.btnEliminar.Visibility = Visibility.Hidden;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnCarpetas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnCarpetas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00035AAC File Offset: 0x00033CAC
		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ClickClose_menu_hide.Play();
			this.dts = new Datos();
			this.dts.GuardarData(System.Windows.Forms.Application.StartupPath + "\\DataBase\\DataAccess.accdb", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\DataAccess.accdb");
			this.dts.GuardarData(System.Windows.Forms.Application.StartupPath + "\\DefectBase\\DataBaseDefect.accdb", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\DataBaseDefect.accdb");
			base.Close();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00035B50 File Offset: 0x00033D50
		private void btnDatos_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.gridDatos.Visibility == Visibility.Visible;
			if (flag)
			{
				this.gridDatos.Visibility = Visibility.Hidden;
			}
			else
			{
				this.gridDatos.Visibility = Visibility.Visible;
			}
			SoundEffects.ClickOpen_fields.Play();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnDatos_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnDatos_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00035B9C File Offset: 0x00033D9C
		private void btnEliminar_Click(object sender, RoutedEventArgs e)
		{
			this.adap = this.mg.adap;
			try
			{
				List<DataRow> list = new List<DataRow>();
				for (int i = 0; i < this.DataGridP.SelectedItems.Count; i++)
				{
					object obj = this.DataGridP.SelectedItems[i];
					bool flag = obj != CollectionView.NewItemPlaceholder;
					if (flag)
					{
						list.Add(((DataRowView)obj).Row);
					}
				}
				foreach (DataRow row in list)
				{
					int index = this.ds.Tables["comandos"].Rows.IndexOf(row);
					this.ds.Tables[0].Rows[index].Delete();
				}
				this.cmdb = new OleDbCommandBuilder(this.adap);
				this.adap.Update(this.ds, "comandos");
				SystemSounds.Asterisk.Play();
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
			this.Actualizartabla();
			this.DataGridP.Columns[0].Visibility = Visibility.Hidden;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnEliminar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnEliminar_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00035D2C File Offset: 0x00033F2C
		private void btnExp_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog
				{
					InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
					FileName = "DataAccess.accdb",
					Title = "Guardar base de dados",
					Filter = "Base de datos de Access(.accdb)|*.accdb",
					RestoreDirectory = true
				};
				bool flag = saveFileDialog.ShowDialog().ToString().Equals("OK");
				if (flag)
				{
					FileInfo fileInfo = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\DataAccess.accdb");
					fileInfo.CopyTo(saveFileDialog.FileName + fileInfo.Extension, true);
					SystemSounds.Asterisk.Play();
				}
				saveFileDialog.Dispose();
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00035E08 File Offset: 0x00034008
		private void btnHelp_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.vinfo == null;
			if (flag)
			{
				this.vinfo = new VentanaInfo();
				this.vinfo.Closed += delegate(object a, EventArgs b)
				{
					this.vinfo = null;
				};
			}
			SoundEffects.ClickOpen_select_number.Play();
			this.vinfo.WindowState = WindowState.Normal;
			this.vinfo.Show();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00035E6C File Offset: 0x0003406C
		private void btnImp_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Filter = "Base de datos de Access(.accdb)|*.accdb",
					Title = "Carregar base de datos",
					InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\DataAccess.accdb"
				};
				bool flag = openFileDialog.ShowDialog().ToString().Equals("OK");
				if (flag)
				{
					FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
					fileInfo.CopyTo(System.Windows.Forms.Application.StartupPath + "\\DataBase\\DataAccess.accdb", true);
					System.Windows.MessageBox.Show("Banco de dados importado com sucesso");
					this.lblResp.Content = "RESPOSTA +";
					this.lblCmd.Content = "COMANDO +";
					this.mostrarTabla(this.NAME_TABLA_APLICACIONES);
					this.DataGridP.Columns[0].Visibility = Visibility.Hidden;
					this.btnNuevo.IsEnabled = true;
					SystemSounds.Asterisk.Play();
				}
				openFileDialog.Dispose();
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00035F94 File Offset: 0x00034194
		private void btnInsert_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				this.btnNuevo.Visibility = Visibility.Visible;
				SoundEffects.ClickOpen_moving_arc.Play();
				bool flag = this.tbCmd.Text != "" || this.tbEje.Text != "" || this.tbRes.Text != "";
				if (flag)
				{
					this.mg.insertarComando(this.NAME_TABLA_AHORA, this.tbCmd.Text.Trim(), this.tbEje.Text.Trim(), this.tbRes.Text.Trim(), Settings.Default.conexion);
					this.Actualizartabla();
					this.DataGridP.Columns[0].Visibility = Visibility.Hidden;
					this.tbCmd.Clear();
					this.tbEje.Clear();
					this.tbRes.Clear();
				}
				else
				{
					SystemSounds.Asterisk.Play();
				}
			}
			catch (Exception)
			{
				System.Windows.MessageBox.Show("você passou do limite de comandos, ou adicionou parametros incorretos verifique a posição do sinal +");
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnInsert_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnInsert_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnInternos_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnInternos_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000029C7 File Offset: 0x00000BC7
		private void btnJarvis_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("http://dowloadjogos.no.comunidades.net/assistente-virtual");
			SoundEffects.Hover_import_export.Play();
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnJarvis_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnJarvis_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00002938 File Offset: 0x00000B38
		private void btnMini_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.Hover_main_button.Play();
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000029E8 File Offset: 0x00000BE8
		private void btnNuevo_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.Stopped_Listening.Play();
			this.btnNuevo.Visibility = Visibility.Hidden;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnNuevo_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnNuevo_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnProgramas_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnProgramas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000360D8 File Offset: 0x000342D8
		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.NAME_TABLA_AHORA == this.NAME_TABLA_APLICACIONES;
			if (flag)
			{
				OpenFileDialog openFileDialog = new OpenFileDialog
				{
					Filter = "Aplicativos(.exe)|*.exe|Lista de reprodução(.wpl)|*.wpl|Todos os arquivos|*.*",
					Title = "Arquivos exe"
				};
				bool flag2 = openFileDialog.ShowDialog().ToString().Equals("OK");
				if (flag2)
				{
					this.tbEje.Text = openFileDialog.FileName;
				}
				openFileDialog.Dispose();
			}
			bool flag3 = this.NAME_TABLA_AHORA == this.NAME_TABLA_CARPETAS;
			if (flag3)
			{
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				bool flag4 = folderBrowserDialog.ShowDialog().ToString().Equals("OK");
				if (flag4)
				{
					this.tbEje.Text = folderBrowserDialog.SelectedPath;
				}
				folderBrowserDialog.Dispose();
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnSociales_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnSociales_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000029AB File Offset: 0x00000BAB
		private void btnWebs_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc.Play();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000029B9 File Offset: 0x00000BB9
		private void btnWebs_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_moving_arc_zone_changed.Play();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00002A03 File Offset: 0x00000C03
		private void DataGridP_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			this.celdaEditada = true;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00002A0D File Offset: 0x00000C0D
		private void DataGridP_Loaded(object sender, RoutedEventArgs e)
		{
			this.DataGridP.Columns[0].Visibility = Visibility.Hidden;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000361BC File Offset: 0x000343BC
		private void DataGridP_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			try
			{
				bool flag = this.celdaEditada;
				if (flag)
				{
					this.adap = this.mg.adap;
					this.cmdb = new OleDbCommandBuilder(this.adap);
					this.adap.Update(this.ds, "comandos");
					SystemSounds.Asterisk.Play();
					this.celdaEditada = false;
				}
			}
			catch (Exception)
			{
				System.Windows.MessageBox.Show("você passou do limite de comandos, ou adicionou parametros incorretos verifique a posição do sinal +");
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00036248 File Offset: 0x00034448
		private void mostrarTabla(string nombreTabla)
		{
			this.btnSociales.Foreground = new SolidColorBrush(Color.FromRgb(0, 210, byte.MaxValue));
			this.btnCarpetas.Foreground = new SolidColorBrush(Color.FromRgb(0, 210, byte.MaxValue));
			this.btnProgramas.Foreground = new SolidColorBrush(Color.FromRgb(0, 210, byte.MaxValue));
			this.btnWebs.Foreground = new SolidColorBrush(Color.FromRgb(0, 210, byte.MaxValue));
			this.btnInternos.Foreground = new SolidColorBrush(Color.FromRgb(0, 210, byte.MaxValue));
			this.btnHelp.Foreground = new SolidColorBrush(Color.FromRgb(0, 210, byte.MaxValue));
			bool flag = nombreTabla == this.NAME_TABLA_SOCIALES;
			if (flag)
			{
				this.btnSociales.Foreground = new SolidColorBrush(Color.FromRgb(127, byte.MaxValue, 212));
			}
			else
			{
				bool flag2 = nombreTabla == this.NAME_TABLA_CARPETAS;
				if (flag2)
				{
					this.btnCarpetas.Foreground = new SolidColorBrush(Colors.Yellow);
				}
				else
				{
					bool flag3 = nombreTabla == this.NAME_TABLA_APLICACIONES;
					if (flag3)
					{
						this.btnProgramas.Foreground = new SolidColorBrush(Colors.Red);
					}
					else
					{
						bool flag4 = nombreTabla == this.NAME_TABLA_WEBS;
						if (flag4)
						{
							this.btnWebs.Foreground = new SolidColorBrush(Colors.YellowGreen);
						}
						else
						{
							bool flag5 = nombreTabla == this.NAME_TABLA_DEFAULT;
							if (flag5)
							{
								this.btnInternos.Foreground = new SolidColorBrush(Colors.White);
							}
						}
					}
				}
			}
			this.NAME_TABLA_AHORA = nombreTabla;
			bool flag6 = nombreTabla != this.NAME_TABLA_DEFAULT;
			if (flag6)
			{
				try
				{
					this.ds = this.mg.verTabla(nombreTabla, "comandos", Settings.Default.conexion);
					this.DataGridP.ItemsSource = this.ds.Tables[0].DefaultView;
				}
				catch (Exception ex)
				{
					System.Windows.MessageBox.Show(ex.Message);
				}
			}
			else
			{
				bool flag7 = nombreTabla == this.NAME_TABLA_DEFAULT;
				if (flag7)
				{
					try
					{
						this.ds = this.mg.verTablaDef(nombreTabla, "comandos", Settings.Default.conexion1);
						this.DataGridP.ItemsSource = this.ds.Tables[0].DefaultView;
					}
					catch (Exception ex2)
					{
						System.Windows.MessageBox.Show(ex2.Message);
					}
					this.btnSearch.IsEnabled = false;
					this.DataGridP.Columns[1].IsReadOnly = true;
					this.DataGridP.Columns[2].IsReadOnly = true;
				}
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000020F8 File Offset: 0x000002F8
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00036544 File Offset: 0x00034744
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/A V®;component/comandos.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0003657C File Offset: 0x0003477C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.window = (Comandos)target;
				this.window.MouseLeftButtonDown += this.Window_MouseLeftButtonDown;
				break;
			case 2:
				this.animacionAddCmds_BeginStoryboard = (BeginStoryboard)target;
				break;
			case 3:
				this.LayoutRoot = (Grid)target;
				break;
			case 4:
				this.label = (System.Windows.Controls.Label)target;
				break;
			case 5:
				this.label1 = (System.Windows.Controls.Label)target;
				break;
			case 6:
				this.lblCmd = (System.Windows.Controls.Label)target;
				break;
			case 7:
				this.DataGridP = (System.Windows.Controls.DataGrid)target;
				this.DataGridP.Loaded += this.DataGridP_Loaded;
				this.DataGridP.CellEditEnding += this.DataGridP_CellEditEnding;
				this.DataGridP.SelectedCellsChanged += this.DataGridP_SelectedCellsChanged;
				break;
			case 8:
				this.btnProgramas = (System.Windows.Controls.Button)target;
				this.btnProgramas.Click += this.btnProgramas_Click;
				this.btnProgramas.MouseEnter += this.btnProgramas_MouseEnter;
				this.btnProgramas.MouseLeave += this.btnProgramas_MouseLeave;
				break;
			case 9:
				this.btnCarpetas = (System.Windows.Controls.Button)target;
				this.btnCarpetas.Click += this.btnCarpetas_Click;
				this.btnCarpetas.MouseEnter += this.btnCarpetas_MouseEnter;
				this.btnCarpetas.MouseLeave += this.btnCarpetas_MouseLeave;
				break;
			case 10:
				this.btnWebs = (System.Windows.Controls.Button)target;
				this.btnWebs.Click += this.btnWebs_Click;
				this.btnWebs.MouseEnter += this.btnWebs_MouseEnter;
				this.btnWebs.MouseLeave += this.btnWebs_MouseLeave;
				break;
			case 11:
				this.btnSociales = (System.Windows.Controls.Button)target;
				this.btnSociales.Click += this.btnSociales_Click;
				this.btnSociales.MouseEnter += this.btnSociales_MouseEnter;
				this.btnSociales.MouseLeave += this.btnSociales_MouseLeave;
				break;
			case 12:
				this.btnInternos = (System.Windows.Controls.Button)target;
				this.btnInternos.Click += this.btnInternos_Click;
				this.btnInternos.MouseEnter += this.btnInternos_MouseEnter;
				this.btnInternos.MouseLeave += this.btnInternos_MouseLeave;
				break;
			case 13:
				this.lblEjec = (System.Windows.Controls.Label)target;
				break;
			case 14:
				this.lblResp = (System.Windows.Controls.Label)target;
				break;
			case 15:
				this.btnDatos = (System.Windows.Controls.Button)target;
				this.btnDatos.Click += this.btnDatos_Click;
				this.btnDatos.MouseEnter += this.btnDatos_MouseEnter;
				this.btnDatos.MouseLeave += this.btnDatos_MouseLeave;
				break;
			case 16:
				this.btnEliminar = (System.Windows.Controls.Button)target;
				this.btnEliminar.Click += this.btnEliminar_Click;
				this.btnEliminar.MouseEnter += this.btnEliminar_MouseEnter;
				this.btnEliminar.MouseLeave += this.btnEliminar_MouseLeave;
				break;
			case 17:
				this.btnInsert = (System.Windows.Controls.Button)target;
				this.btnInsert.Click += this.btnInsert_Click;
				this.btnInsert.MouseEnter += this.btnInsert_MouseEnter;
				this.btnInsert.MouseLeave += this.btnInsert_MouseLeave;
				break;
			case 18:
				this.btnNuevo = (System.Windows.Controls.Button)target;
				this.btnNuevo.Click += this.btnNuevo_Click;
				this.btnNuevo.MouseEnter += this.btnNuevo_MouseEnter;
				this.btnNuevo.MouseLeave += this.btnNuevo_MouseLeave;
				break;
			case 19:
				this.btnJarvis = (System.Windows.Controls.Button)target;
				this.btnJarvis.Click += this.btnJarvis_Click;
				this.btnJarvis.MouseEnter += this.btnJarvis_MouseEnter;
				this.btnJarvis.MouseLeave += this.btnJarvis_MouseLeave;
				break;
			case 20:
				this.gridAddCmds = (Grid)target;
				break;
			case 21:
				this.tbEje = (System.Windows.Controls.TextBox)target;
				break;
			case 22:
				this.tbCmd = (System.Windows.Controls.TextBox)target;
				break;
			case 23:
				this.tbRes = (System.Windows.Controls.TextBox)target;
				break;
			case 24:
				this.btnSearch = (System.Windows.Controls.Button)target;
				this.btnSearch.Click += this.btnSearch_Click;
				break;
			case 25:
				this.btnClose = (System.Windows.Controls.Button)target;
				this.btnClose.Click += this.btnClose_Click;
				break;
			case 26:
				this.btnMini = (System.Windows.Controls.Button)target;
				this.btnMini.Click += this.btnMini_Click;
				break;
			case 27:
				this.gridDatos = (Grid)target;
				break;
			case 28:
				this.label2 = (System.Windows.Controls.Label)target;
				break;
			case 29:
				this.label3 = (System.Windows.Controls.Label)target;
				break;
			case 30:
				this.textBlock = (TextBlock)target;
				break;
			case 31:
				this.btnExp = (System.Windows.Controls.Button)target;
				this.btnExp.Click += this.btnExp_Click;
				break;
			case 32:
				this.btnImp = (System.Windows.Controls.Button)target;
				this.btnImp.Click += this.btnImp_Click;
				break;
			case 33:
				this.btnHelp = (System.Windows.Controls.Button)target;
				this.btnHelp.Click += this.btnHelp_Click;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x040001B8 RID: 440
		private string NAME_TABLA_SOCIALES = "ComandosSociales";

		// Token: 0x040001B9 RID: 441
		private string NAME_TABLA_CARPETAS = "ComandosCarpetas";

		// Token: 0x040001BA RID: 442
		private string NAME_TABLA_APLICACIONES = "ComandosAplicaciones";

		// Token: 0x040001BB RID: 443
		private string NAME_TABLA_WEBS = "ComandosPaginasWebs";

		// Token: 0x040001BC RID: 444
		private string NAME_TABLA_DEFAULT = "ComandosDefecto";

		// Token: 0x040001BD RID: 445
		private string NAME_TABLA_AHORA = "ComandosAplicaciones";

		// Token: 0x040001BE RID: 446
		private OleDbDataAdapter adap;

		// Token: 0x040001BF RID: 447
		private OleDbCommandBuilder cmdb;

		// Token: 0x040001C0 RID: 448
		private DataSet ds;

		// Token: 0x040001C1 RID: 449
		private VentanaInfo vinfo;

		// Token: 0x040001C2 RID: 450
		private Datos dts;

		// Token: 0x040001C3 RID: 451
		private Manager mg = new Manager();

		// Token: 0x040001C4 RID: 452
		private bool celdaEditada = false;

		// Token: 0x040001C5 RID: 453
		internal Comandos window;

		// Token: 0x040001C6 RID: 454
		internal BeginStoryboard animacionAddCmds_BeginStoryboard;

		// Token: 0x040001C7 RID: 455
		internal Grid LayoutRoot;

		// Token: 0x040001C8 RID: 456
		internal System.Windows.Controls.Label label;

		// Token: 0x040001C9 RID: 457
		internal System.Windows.Controls.Label label1;

		// Token: 0x040001CA RID: 458
		internal System.Windows.Controls.Label lblCmd;

		// Token: 0x040001CB RID: 459
		internal System.Windows.Controls.DataGrid DataGridP;

		// Token: 0x040001CC RID: 460
		internal System.Windows.Controls.Button btnProgramas;

		// Token: 0x040001CD RID: 461
		internal System.Windows.Controls.Button btnCarpetas;

		// Token: 0x040001CE RID: 462
		internal System.Windows.Controls.Button btnWebs;

		// Token: 0x040001CF RID: 463
		internal System.Windows.Controls.Button btnSociales;

		// Token: 0x040001D0 RID: 464
		internal System.Windows.Controls.Button btnInternos;

		// Token: 0x040001D1 RID: 465
		internal System.Windows.Controls.Label lblEjec;

		// Token: 0x040001D2 RID: 466
		internal System.Windows.Controls.Label lblResp;

		// Token: 0x040001D3 RID: 467
		internal System.Windows.Controls.Button btnDatos;

		// Token: 0x040001D4 RID: 468
		internal System.Windows.Controls.Button btnEliminar;

		// Token: 0x040001D5 RID: 469
		internal System.Windows.Controls.Button btnInsert;

		// Token: 0x040001D6 RID: 470
		internal System.Windows.Controls.Button btnNuevo;

		// Token: 0x040001D7 RID: 471
		internal System.Windows.Controls.Button btnJarvis;

		// Token: 0x040001D8 RID: 472
		internal Grid gridAddCmds;

		// Token: 0x040001D9 RID: 473
		internal System.Windows.Controls.TextBox tbEje;

		// Token: 0x040001DA RID: 474
		internal System.Windows.Controls.TextBox tbCmd;

		// Token: 0x040001DB RID: 475
		internal System.Windows.Controls.TextBox tbRes;

		// Token: 0x040001DC RID: 476
		internal System.Windows.Controls.Button btnSearch;

		// Token: 0x040001DD RID: 477
		internal System.Windows.Controls.Button btnClose;

		// Token: 0x040001DE RID: 478
		internal System.Windows.Controls.Button btnMini;

		// Token: 0x040001DF RID: 479
		internal Grid gridDatos;

		// Token: 0x040001E0 RID: 480
		internal System.Windows.Controls.Label label2;

		// Token: 0x040001E1 RID: 481
		internal System.Windows.Controls.Label label3;

		// Token: 0x040001E2 RID: 482
		internal TextBlock textBlock;

		// Token: 0x040001E3 RID: 483
		internal System.Windows.Controls.Button btnExp;

		// Token: 0x040001E4 RID: 484
		internal System.Windows.Controls.Button btnImp;

		// Token: 0x040001E5 RID: 485
		internal System.Windows.Controls.Button btnHelp;

		// Token: 0x040001E6 RID: 486
		private bool _contentLoaded;
	}
}
