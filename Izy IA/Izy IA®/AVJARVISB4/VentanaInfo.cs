using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using AVJARVISB4.Clases;
using AVJARVISB4.Properties;

namespace AVJARVISB4
{
	// Token: 0x02000017 RID: 23
	public class VentanaInfo : Window, IComponentConnector
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00032DA0 File Offset: 0x00030FA0
		public VentanaInfo()
		{
			this.InitializeComponent();
			this.tvCmd.Loaded += this.TvCmd_Loaded;
			this.tvCmd.SelectedItemChanged += this.tvCmd_SelectedItemChanged;
			this.LoadListaComandos();
			this.LoadComandos();
			this.LoadTeclado();
			this.loadDefect();
			this.LoadItem();
			SoundEffects.Somefects();
			Rect workArea = SystemParameters.WorkArea;
			base.Left = workArea.Right - 290.0;
			base.Top = workArea.Bottom / 2.0 - 244.0;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0003311C File Offset: 0x0003131C
		private void AddComd()
		{
			this.alarmaCmd.Add("configurar alarme as [hora]");
			this.buscadorCmd.Add("pesquisar isto");
			this.buscadorCmd.Add("pesquisar por [...] no [google,youtube,wikipedia,facebook,yahoo]");
			this.buscadorCmd.Add("pesquisar no [...] em [google,youtube,wikipedia,facebook,yahoo]");
			this.buscadorCmd.Add("procurar [no,em,por] [...] em [google,youtube,wikipedia,facebook,yahoo]");
			this.buscadorCmd.Add("buscar em [...] no [google,youtube,wikipedia,facebook,yahoo]");
			this.buscadorCmd.Add("buscar no [...] em [google,youtube,wikipedia,facebook,yahoo]");
			this.calculadoraCmd.Add("[núm] [operador] [núm]");
			this.calculadoraCmd.Add(" quanto é [núm] [operador] [núm]");
			this.recordatorioCmd.Add("me lembre [titulo]");
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000020E3 File Offset: 0x000002E3
		private void BtnClose_NS_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ClickClose_menu_hide.Play();
			base.Close();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000331D4 File Offset: 0x000313D4
		private void BtnMaxim_Click(object sender, RoutedEventArgs e)
		{
			bool flag = base.WindowState != WindowState.Maximized;
			if (flag)
			{
				base.WindowState = WindowState.Maximized;
			}
			else
			{
				base.WindowState = WindowState.Normal;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00002938 File Offset: 0x00000B38
		private void BtnMini_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.Hover_main_button.Play();
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00033208 File Offset: 0x00031408
		private void loadDefect()
		{
			try
			{
				this.conec = new OleDbConnection(Settings.Default.conexion1);
				this.conec.Open();
				using (OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'sistema'", this.conec).ExecuteReader())
				{
					while (oleDbDataReader.Read())
					{
						this.sistemaCmd.Add(oleDbDataReader["Comando"].ToString());
					}
				}
				OleDbDataReader oleDbDataReader2 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'Inform'", this.conec).ExecuteReader();
				while (oleDbDataReader2.Read())
				{
					this.CmdInform.Add(oleDbDataReader2["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader3 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'web' ", this.conec).ExecuteReader();
				while (oleDbDataReader3.Read())
				{
					this.webCmd.Add(oleDbDataReader3["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader4 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'Ditado' ", this.conec).ExecuteReader();
				while (oleDbDataReader4.Read())
				{
					this.cmdDitado.Add(oleDbDataReader4["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader5 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'Assistente' ", this.conec).ExecuteReader();
				while (oleDbDataReader5.Read())
				{
					this.CmdAssistente.Add(oleDbDataReader5["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader6 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'Clima' ", this.conec).ExecuteReader();
				while (oleDbDataReader6.Read())
				{
					this.climaCmd.Add(oleDbDataReader6["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader7 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'E-mail' ", this.conec).ExecuteReader();
				while (oleDbDataReader7.Read())
				{
					this.correoCmd.Add(oleDbDataReader7["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader8 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'Alarme' ", this.conec).ExecuteReader();
				while (oleDbDataReader8.Read())
				{
					this.alarmaCmd.Add(oleDbDataReader8["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader9 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'facebook' ", this.conec).ExecuteReader();
				while (oleDbDataReader9.Read())
				{
					this.facebookCmd.Add(oleDbDataReader9["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader10 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'Lembrete' ", this.conec).ExecuteReader();
				while (oleDbDataReader10.Read())
				{
					this.recordatorioCmd.Add(oleDbDataReader10["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader11 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'Reprodutor' ", this.conec).ExecuteReader();
				while (oleDbDataReader11.Read())
				{
					this.reproductorCmd.Add(oleDbDataReader11["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader12 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'Teclado' ", this.conec).ExecuteReader();
				while (oleDbDataReader12.Read())
				{
					this.tecladoCmd.Add(oleDbDataReader12["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader13 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'Atalho' ", this.conec).ExecuteReader();
				while (oleDbDataReader13.Read())
				{
					this.atajosCmd.Add(oleDbDataReader13["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader14 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'porta' ", this.conec).ExecuteReader();
				while (oleDbDataReader14.Read())
				{
					this.atajosCmd1.Add(oleDbDataReader14["Comando"].ToString());
				}
				OleDbDataReader oleDbDataReader15 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Tipo LIKE 'youtube'", this.conec).ExecuteReader();
				while (oleDbDataReader15.Read())
				{
					this.CmdAdicionais.Add(oleDbDataReader15["Comando"].ToString());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.conec.Close();
			this.item.ItemsSource = this.alarmaCmd;
			this.item1.ItemsSource = this.atajosCmd;
			this.item2.ItemsSource = this.climaCmd;
			this.item3.ItemsSource = this.correoCmd;
			this.item4.ItemsSource = this.facebookCmd;
			this.item5.ItemsSource = this.webCmd;
			this.item6.ItemsSource = this.recordatorioCmd;
			this.item7.ItemsSource = this.reproductorCmd;
			this.item8.ItemsSource = this.sistemaCmd;
			this.item9.ItemsSource = this.tecladoCmd;
			this.item10.ItemsSource = this.cmdDictado;
			this.item11.ItemsSource = this.buscadorCmd;
			this.item15.ItemsSource = this.calculadoraCmd;
			this.item17.ItemsSource = this.CmdAssistente;
			this.item18.ItemsSource = this.CmdInform;
			this.item19.ItemsSource = this.CmdAdicionais;
			this.item101.ItemsSource = this.atajosCmd1;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000337D0 File Offset: 0x000319D0
		private void LoadComandos()
		{
			try
			{
				this.con = new OleDbConnection(Settings.Default.conexion);
				this.con.Open();
				using (OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT *FROM ComandosAplicaciones", this.con).ExecuteReader())
				{
					while (oleDbDataReader.Read())
					{
						this.aplicacionesCmd.Add(oleDbDataReader["Comando"].ToString().Split(new char[]
						{
							'+'
						})[0]);
					}
				}
				OleDbDataReader oleDbDataReader2 = new OleDbCommand("SELECT *FROM ComandosPaginasWebs", this.con).ExecuteReader();
				while (oleDbDataReader2.Read())
				{
					this.paginaswebsCmd.Add(oleDbDataReader2["Comando"].ToString().Split(new char[]
					{
						'+'
					})[0]);
				}
				OleDbDataReader oleDbDataReader3 = new OleDbCommand("SELECT *FROM ComandosCarpetas", this.con).ExecuteReader();
				while (oleDbDataReader3.Read())
				{
					this.carpetasCmd.Add(oleDbDataReader3["Comando"].ToString().Split(new char[]
					{
						'+'
					})[0]);
				}
				OleDbDataReader oleDbDataReader4 = new OleDbCommand("SELECT *FROM ComandosSociales", this.con).ExecuteReader();
				while (oleDbDataReader4.Read())
				{
					this.socialesCmd.Add(oleDbDataReader4["Comando"].ToString().Split(new char[]
					{
						'+'
					})[0]);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.con.Close();
			this.AddComd();
			this.item16.ItemsSource = this.socialesCmd;
			this.item14.ItemsSource = this.carpetasCmd;
			this.item12.ItemsSource = this.aplicacionesCmd;
			this.item13.ItemsSource = this.paginaswebsCmd;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00033A04 File Offset: 0x00031C04
		private void LoadTeclado()
		{
			try
			{
				this.conecTeclado = new OleDbConnection(Settings.Default.conteclado);
				this.conecTeclado.Open();
				using (OleDbDataReader oleDbDataReader = new OleDbCommand("SELECT *FROM Key_default", this.conecTeclado).ExecuteReader())
				{
					while (oleDbDataReader.Read())
					{
						this.tecladoCmd.Add(oleDbDataReader["Comando"].ToString().Split(new char[]
						{
							'+'
						})[0]);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			this.conecTeclado.Close();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00033AD4 File Offset: 0x00031CD4
		private void LoadItem()
		{
			this.tvCmd.Items.Add(this.item);
			this.tvCmd.Items.Add(this.item1);
			this.tvCmd.Items.Add(this.item2);
			this.tvCmd.Items.Add(this.item3);
			this.tvCmd.Items.Add(this.item4);
			this.tvCmd.Items.Add(this.item5);
			this.tvCmd.Items.Add(this.item6);
			this.tvCmd.Items.Add(this.item7);
			this.tvCmd.Items.Add(this.item8);
			this.tvCmd.Items.Add(this.item9);
			this.tvCmd.Items.Add(this.item10);
			this.tvCmd.Items.Add(this.item12);
			this.tvCmd.Items.Add(this.item13);
			this.tvCmd.Items.Add(this.item14);
			this.tvCmd.Items.Add(this.item11);
			this.tvCmd.Items.Add(this.item15);
			this.tvCmd.Items.Add(this.item16);
			this.tvCmd.Items.Add(this.item17);
			this.tvCmd.Items.Add(this.item18);
			this.tvCmd.Items.Add(this.item19);
			this.tvCmd.Items.Add(this.item101);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00033CC8 File Offset: 0x00031EC8
		private void LoadListaComandos()
		{
			this.item.Header = "Alarme";
			this.item1.Header = "Atalho";
			this.item2.Header = "Clima";
			this.item3.Header = "E-mail";
			this.item4.Header = "Facebook";
			this.item5.Header = "web";
			this.item6.Header = "Lembrete";
			this.item7.Header = "Reprodutor";
			this.item8.Header = "Sistema";
			this.item9.Header = "Teclado";
			this.item10.Header = "Ditado";
			this.item11.Header = "Buscador";
			this.item12.Header = "Comandos aplicativo";
			this.item13.Header = "Comandos páginas webs";
			this.item14.Header = "Comandos pastas";
			this.item15.Header = "Calculadora";
			this.item16.Header = "Comandos conversa";
			this.item17.Header = "Assistente";
			this.item18.Header = "Inform";
			this.item19.Header = "youtube";
			this.item101.Header = "porta";
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000028C1 File Offset: 0x00000AC1
		private void TvCmd_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00033E3C File Offset: 0x0003203C
		private void tvCmd_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			TreeView treeView = sender as TreeView;
			bool flag = this.tvCmd.SelectedItem is TreeViewItem;
			if (flag)
			{
				TreeViewItem treeViewItem = this.tvCmd.SelectedItem as TreeViewItem;
				bool flag2 = treeViewItem.Header.ToString() == "Alarme";
				if (flag2)
				{
					this.tbfunçoes.Text = "configura o alarme \n Nova fução\n desligar Reniciar ou sair na hora exata";
					this.tbInfo.Text = "Configuração por voz\n pessa ao assistente para que configure o alarme para você. informe as horas da manhã/tarde ou noite \nque você deseja que o assistente disperte";
				}
				else
				{
					bool flag3 = treeViewItem.Header.ToString() == "Calculadora";
					if (flag3)
					{
						this.tbfunçoes.Text = "Executa operações matemáticas básicas";
						this.tbInfo.Text = "A cauculadora funciona da senguinte forma\npergunte quanto é 1 até 1000 mais/ mutiplicado por/menos/dividido por \nnumeros de um até mil";
					}
					else
					{
						bool flag4 = treeViewItem.Header.ToString() == "Clima";
						if (flag4)
						{
							this.tbfunçoes.Text = "Saiba como está o tempo para hoje e amanhã. \nRequer conexão à Internet";
							this.tbInfo.Text = "configure sua cidade ou estado acesse o site clicando em buscar \nna pagina copie todos os numeros que aparecer no final de cada pesquisa \ndepois é só preencher a placa e colocar para testar";
						}
						else
						{
							bool flag5 = treeViewItem.Header.ToString() == "Inform";
							if (flag5)
							{
								this.tbfunçoes.Text = "Saiba as principais notícias G1,\n Informações das horas data e dia,\n Saudações";
								this.tbInfo.Text = "bom dia\nboa tarde\nboa noite \nnoticias requer conecção com a internet";
							}
							else
							{
								bool flag6 = treeViewItem.Header.ToString() == "E-mail";
								if (flag6)
								{
									this.tbfunçoes.Text = "Receba seus e-mails mais recentes do Gmail, Outlook e Yahoo. \nRequer conexão com a Internet";
									this.tbInfo.Text = "configure a suas contas e marque as caixas se quizer receber e-mails\ndesmarque se não quiser receber avisos de e-mails";
								}
								else
								{
									bool flag7 = treeViewItem.Header.ToString() == "Facebook";
									if (flag7)
									{
										this.tbfunçoes.Text = "Receba sua notificação mais recente no Facebook.\nRequer conexão com a Internet";
										this.tbInfo.Text = "proxima atualização de versões estes comandos já estarão disponiveis";
									}
									else
									{
										bool flag8 = treeViewItem.Header.ToString() == "Leitor RSS";
										if (flag8)
										{
											this.tbfunçoes.Text = "Receba sua notificação mais recente do seu site favorito.\nRequer conexão com a Internet";
											this.tbInfo.Text = "proxima atualização de versões estes comandos já estarão disponiveis";
										}
										else
										{
											bool flag9 = treeViewItem.Header.ToString() == "web";
											if (flag9)
											{
												this.tbfunçoes.Text = "Manipule navegadores da web Chrome e Firefox\n opera mini edje";
												this.tbInfo.Text = "";
											}
											else
											{
												bool flag10 = treeViewItem.Header.ToString() == "Lembrete";
												if (flag10)
												{
													this.tbfunçoes.Text = "Defina seus lembretes.";
													this.tbInfo.Text = "Ex:para fazer com que seu assistente defina por você o seu lembrete basta chamar e logo em seguida pedir para configurar um lembrete. veja mais em comandos de lembrete";
												}
												else
												{
													bool flag11 = treeViewItem.Header.ToString() == "Reprodutor";
													if (flag11)
													{
														this.tbfunçoes.Text = "Controle seu player favorito como media player e tocador do seu Assistente Virtual";
														this.tbInfo.Text = "controle total do Media player próxima atualização";
													}
													else
													{
														bool flag12 = treeViewItem.Header.ToString() == "Sistema";
														if (flag12)
														{
															this.tbfunçoes.Text = "Controlar o sistema operacional Windows";
															this.tbInfo.Text = "versões para cada comando depende muito da versão todos os comandos funcionam fluetemente em windows 10\nvarias em windows 7 /windows 8 /windows 8.1";
														}
														else
														{
															bool flag13 = treeViewItem.Header.ToString() == "Teclado interno";
															if (flag13)
															{
																this.tbfunçoes.Text = "atalhos de teclado mais usados";
																this.tbInfo.Text = "";
															}
															else
															{
																bool flag14 = treeViewItem.Header.ToString() == "Buscador";
																if (flag14)
																{
																	this.tbfunçoes.Text = "Pesquise os sites mais populares. \nRequira conexão à Internet";
																	this.tbInfo.Text = "Comandos de busca simples, você deverar fazer a pesquisa de uma só palavra \nEx: buscar palavra no google \nbuscar Assistente no youtube";
																}
																else
																{
																	bool flag15 = treeViewItem.Header.ToString() == "Comandos aplicativo";
																	if (flag15)
																	{
																		this.tbfunçoes.Text = "Permite abrir seus programas favoritos";
																		this.tbInfo.Text = "adicione comandos atráves da placa e você verá aqui";
																	}
																	else
																	{
																		bool flag16 = treeViewItem.Header.ToString() == "Comandos páginas webs";
																		if (flag16)
																		{
																			this.tbfunçoes.Text = "Permite abrir seus sites favoritos";
																			this.tbInfo.Text = "comandos para paginas são adicionados pela placa com url\nadicione seus comandos para que apareça aqui";
																		}
																		else
																		{
																			bool flag17 = treeViewItem.Header.ToString() == "Comandos pastas";
																			if (flag17)
																			{
																				this.tbfunçoes.Text = "Permite abrir suas pastas favoritas";
																				this.tbInfo.Text = "comandos adicionados atraves da sua placa de comandos\naparecerão aqui";
																			}
																			else
																			{
																				bool flag18 = treeViewItem.Header.ToString() == "Comandos conversa";
																				if (flag18)
																				{
																					this.tbfunçoes.Text = "Permite que o assistente responda a frases anteriormente guardadas";
																					this.tbInfo.Text = "tenha uma conversa fluete com o assistente virtual adicione conversas a sua placa comandos aparecerão aqui após adicionados";
																				}
																				else
																				{
																					bool flag19 = treeViewItem.Header.ToString() == "Teclado";
																					if (flag19)
																					{
																						this.tbfunçoes.Text = "Permite executar comandos de teclado por voz configurados";
																						this.tbInfo.Text = "";
																					}
																					else
																					{
																						bool flag20 = treeViewItem.Header.ToString() == "Ditado";
																						if (flag20)
																						{
																							this.tbfunçoes.Text = "comandos para o trabalho em documentos word";
																							this.tbInfo.Text = "atualizações futuras";
																						}
																						else
																						{
																							bool flag21 = treeViewItem.Header.ToString() == "Atalho";
																							if (flag21)
																							{
																								this.tbfunçoes.Text = "permite a sequecia de comandos de atalhos do teclado";
																								this.tbInfo.Text = "";
																							}
																							else
																							{
																								bool flag22 = treeViewItem.Header.ToString() == "Assistente";
																								if (flag22)
																								{
																									this.tbfunçoes.Text = "permite a sequecia de comandos do seu Assistente Virtual";
																									this.tbInfo.Text = "";
																								}
																								else
																								{
																									bool flag23 = treeViewItem.Header.ToString() == "youtube";
																									if (flag23)
																									{
																										this.tbfunçoes.Text = "Os comandos do youtube  \n são comandos de atalhos para controlar as paginas de reprodução de videos selecionada";
																										this.tbInfo.Text = "Esses comandos podem também funcionar e diversas paginas de filmes que responde aos mesmo atalhos do youtube";
																									}
																									else
																									{
																										bool flag24 = treeViewItem.Header.ToString() == "porta";
																										if (flag24)
																										{
																											this.tbfunçoes.Text = "controle do porto serial COM";
																											this.tbInfo.Text = "Em teste";
																										}
																										else
																										{
																											bool flag25 = treeViewItem.Header.ToString() == "tecladoexterno";
																											if (flag25)
																											{
																												this.tbfunçoes.Text = "permite a sequecia de comandos do teclado adicionados";
																												this.tbInfo.Text = "Função adicionadas as teclas por comando de voz";
																											}
																											else
																											{
																												this.tbfunçoes.Text = string.Empty;
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			bool flag26 = treeView.SelectedItem is string;
			if (flag26)
			{
				bool flag27 = false;
				this.tbInfo.Text = "Sinonimo ou Ação:\n";
				this.tbfunçoes.Text = "Sinonimos:\n";
				this.con = new OleDbConnection(Settings.Default.conexion);
				this.con.Open();
				try
				{
					bool flag28 = !flag27;
					if (flag28)
					{
						OleDbCommand oleDbCommand = new OleDbCommand("SELECT *FROM ComandosSociales WHERE Comando LIKE '" + treeView.SelectedItem.ToString() + "%'", this.con);
						OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader(CommandBehavior.SingleResult);
						while (oleDbDataReader.Read())
						{
							foreach (string str in oleDbDataReader["Comando"].ToString().Split(new char[]
							{
								'+'
							}))
							{
								this.tbfunçoes.AppendText(str + "\n");
							}
							flag27 = true;
						}
					}
					bool flag29 = !flag27;
					if (flag29)
					{
						OleDbCommand oleDbCommand2 = new OleDbCommand("SELECT *FROM ComandosAplicaciones WHERE Comando LIKE '" + treeView.SelectedItem.ToString() + "%'", this.con);
						OleDbCommand oleDbCommand3 = new OleDbCommand("SELECT *FROM ComandosAplicaciones WHERE Comando LIKE '" + treeView.SelectedItem.ToString() + "%'", this.con);
						OleDbDataReader oleDbDataReader2 = oleDbCommand2.ExecuteReader(CommandBehavior.SingleResult);
						OleDbDataReader oleDbDataReader3 = oleDbCommand3.ExecuteReader(CommandBehavior.SingleResult);
						while (oleDbDataReader3.Read())
						{
							foreach (string str2 in oleDbDataReader3["Comando"].ToString().Split(new char[]
							{
								'+'
							}))
							{
								this.tbfunçoes.AppendText(str2 + "\n");
							}
							flag27 = true;
						}
						while (oleDbDataReader2.Read())
						{
							foreach (string str3 in oleDbDataReader2["Ejecutar"].ToString().Split(new char[]
							{
								'+'
							}))
							{
								this.tbInfo.AppendText(str3 + "\n");
							}
							flag27 = true;
						}
					}
					bool flag30 = !flag27;
					if (flag30)
					{
						OleDbCommand oleDbCommand4 = new OleDbCommand("SELECT *FROM ComandosCarpetas WHERE Comando LIKE '" + treeView.SelectedItem.ToString() + "%'", this.con);
						OleDbCommand oleDbCommand5 = new OleDbCommand("SELECT *FROM ComandosCarpetas WHERE Comando LIKE '" + treeView.SelectedItem.ToString() + "%'", this.con);
						OleDbDataReader oleDbDataReader4 = oleDbCommand4.ExecuteReader(CommandBehavior.SingleResult);
						OleDbDataReader oleDbDataReader5 = oleDbCommand5.ExecuteReader(CommandBehavior.SingleResult);
						while (oleDbDataReader4.Read())
						{
							foreach (string str4 in oleDbDataReader4["Comando"].ToString().Split(new char[]
							{
								'+'
							}))
							{
								this.tbfunçoes.AppendText(str4 + "\n");
							}
							flag27 = true;
						}
						while (oleDbDataReader5.Read())
						{
							foreach (string str5 in oleDbDataReader5["Ejecutar"].ToString().Split(new char[]
							{
								'+'
							}))
							{
								this.tbInfo.AppendText(str5 + "\n");
							}
							flag27 = true;
						}
					}
					bool flag31 = !flag27;
					if (flag31)
					{
						OleDbCommand oleDbCommand6 = new OleDbCommand("SELECT *FROM ComandosPaginasWebs WHERE Comando LIKE '" + treeView.SelectedItem.ToString() + "%'", this.con);
						OleDbCommand oleDbCommand7 = new OleDbCommand("SELECT *FROM ComandosPaginasWebs WHERE Comando LIKE '" + treeView.SelectedItem.ToString() + "%'", this.con);
						OleDbDataReader oleDbDataReader6 = oleDbCommand6.ExecuteReader(CommandBehavior.SingleResult);
						OleDbDataReader oleDbDataReader7 = oleDbCommand7.ExecuteReader(CommandBehavior.SingleResult);
						while (oleDbDataReader6.Read())
						{
							foreach (string str6 in oleDbDataReader6["Comando"].ToString().Split(new char[]
							{
								'+'
							}))
							{
								this.tbfunçoes.AppendText(str6 + "\n");
							}
						}
						while (oleDbDataReader7.Read())
						{
							foreach (string str7 in oleDbDataReader7["Ejecutar"].ToString().Split(new char[]
							{
								'+'
							}))
							{
								this.tbInfo.AppendText(str7 + "\n");
							}
						}
					}
					this.con.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			bool flag32 = treeView.SelectedItem is string;
			if (flag32)
			{
				bool flag33 = false;
				this.conec = new OleDbConnection(Settings.Default.conexion1);
				this.conec.Open();
				try
				{
					bool flag34 = !flag33;
					if (flag34)
					{
						OleDbCommand oleDbCommand8 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Comando LIKE '" + treeView.SelectedItem.ToString() + "%'", this.conec);
						OleDbCommand oleDbCommand9 = new OleDbCommand("SELECT *FROM ComandosDefecto WHERE Comando LIKE '" + treeView.SelectedItem.ToString() + "%'", this.conec);
						OleDbDataReader oleDbDataReader8 = oleDbCommand8.ExecuteReader(CommandBehavior.SingleResult);
						OleDbDataReader oleDbDataReader9 = oleDbCommand9.ExecuteReader(CommandBehavior.SingleResult);
						while (oleDbDataReader8.Read())
						{
							foreach (string str8 in oleDbDataReader8["Sinonimos"].ToString().Split(new char[]
							{
								'+'
							}))
							{
								this.tbfunçoes.AppendText(str8 + "\n");
							}
						}
						while (oleDbDataReader9.Read())
						{
							foreach (string str9 in oleDbDataReader9["Accion"].ToString().Split(new char[]
							{
								'+'
							}))
							{
								this.tbInfo.AppendText(str9 + "\n");
							}
						}
					}
					this.conec.Close();
				}
				catch (Exception ex2)
				{
					MessageBox.Show(ex2.Message);
				}
			}
			bool flag35 = treeView.SelectedItem is string;
			if (flag35)
			{
				bool flag36 = false;
				this.conecTeclado = new OleDbConnection(Settings.Default.conteclado);
				this.conecTeclado.Open();
				try
				{
					bool flag37 = !flag36;
					if (flag37)
					{
						OleDbCommand oleDbCommand10 = new OleDbCommand("SELECT *FROM key_default WHERE Comando LIKE '" + treeView.SelectedItem.ToString() + "%'", this.conecTeclado);
						OleDbCommand oleDbCommand11 = new OleDbCommand("SELECT *FROM key_default WHERE Comando LIKE '" + treeView.SelectedItem.ToString() + "%'", this.conecTeclado);
						OleDbDataReader oleDbDataReader10 = oleDbCommand10.ExecuteReader(CommandBehavior.SingleResult);
						OleDbDataReader oleDbDataReader11 = oleDbCommand11.ExecuteReader(CommandBehavior.SingleResult);
						while (oleDbDataReader10.Read())
						{
							foreach (string str10 in oleDbDataReader10["Comando"].ToString().Split(new char[]
							{
								'+'
							}))
							{
								this.tbfunçoes.AppendText(str10 + "\n");
							}
						}
						while (oleDbDataReader11.Read())
						{
							foreach (string str11 in oleDbDataReader11["Tecla"].ToString().Split(new char[]
							{
								'+'
							}))
							{
								this.tbInfo.AppendText(str11 + "\n");
							}
						}
					}
					this.conecTeclado.Close();
				}
				catch (Exception ex3)
				{
					MessageBox.Show(ex3.Message);
				}
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000020F8 File Offset: 0x000002F8
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00034DD8 File Offset: 0x00032FD8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/A V®;component/ventanainfo.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00034E10 File Offset: 0x00033010
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((VentanaInfo)target).MouseLeftButtonDown += this.Window_MouseLeftButtonDown;
				break;
			case 2:
				this.LayoutRoot = (Grid)target;
				break;
			case 3:
				this.imgHud = (Image)target;
				break;
			case 4:
				this.tvCmd = (TreeView)target;
				break;
			case 5:
				this.lbItems = (ListBox)target;
				break;
			case 6:
				this.lblNameS = (Label)target;
				break;
			case 7:
				this.lblName = (Label)target;
				break;
			case 8:
				this.BtnClose_NS = (Button)target;
				this.BtnClose_NS.Click += this.BtnClose_NS_Click;
				break;
			case 9:
				this.BtnMini = (Button)target;
				this.BtnMini.Click += this.BtnMini_Click;
				break;
			case 10:
				this.tbInfo = (TextBox)target;
				break;
			case 11:
				this.BtnMaxim = (Button)target;
				this.BtnMaxim.Click += this.BtnMaxim_Click;
				break;
			case 12:
				this.tbfunçoes = (TextBox)target;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x0400016D RID: 365
		private OleDbConnection con;

		// Token: 0x0400016E RID: 366
		private OleDbConnection conec;

		// Token: 0x0400016F RID: 367
		private OleDbConnection conecTeclado;

		// Token: 0x04000170 RID: 368
		private TreeViewItem item = new TreeViewItem();

		// Token: 0x04000171 RID: 369
		private TreeViewItem item1 = new TreeViewItem();

		// Token: 0x04000172 RID: 370
		private TreeViewItem item2 = new TreeViewItem();

		// Token: 0x04000173 RID: 371
		private TreeViewItem item3 = new TreeViewItem();

		// Token: 0x04000174 RID: 372
		private TreeViewItem item4 = new TreeViewItem();

		// Token: 0x04000175 RID: 373
		private TreeViewItem item5 = new TreeViewItem();

		// Token: 0x04000176 RID: 374
		private TreeViewItem item6 = new TreeViewItem();

		// Token: 0x04000177 RID: 375
		private TreeViewItem item7 = new TreeViewItem();

		// Token: 0x04000178 RID: 376
		private TreeViewItem item8 = new TreeViewItem();

		// Token: 0x04000179 RID: 377
		private TreeViewItem item9 = new TreeViewItem();

		// Token: 0x0400017A RID: 378
		private TreeViewItem item10 = new TreeViewItem();

		// Token: 0x0400017B RID: 379
		private TreeViewItem item101 = new TreeViewItem();

		// Token: 0x0400017C RID: 380
		private TreeViewItem item11 = new TreeViewItem();

		// Token: 0x0400017D RID: 381
		private TreeViewItem item12 = new TreeViewItem();

		// Token: 0x0400017E RID: 382
		private TreeViewItem item13 = new TreeViewItem();

		// Token: 0x0400017F RID: 383
		private TreeViewItem item14 = new TreeViewItem();

		// Token: 0x04000180 RID: 384
		private TreeViewItem item15 = new TreeViewItem();

		// Token: 0x04000181 RID: 385
		private TreeViewItem item16 = new TreeViewItem();

		// Token: 0x04000182 RID: 386
		private TreeViewItem item17 = new TreeViewItem();

		// Token: 0x04000183 RID: 387
		private TreeViewItem item18 = new TreeViewItem();

		// Token: 0x04000184 RID: 388
		private TreeViewItem item19 = new TreeViewItem();

		// Token: 0x04000185 RID: 389
		private List<string> sistemaCmd = new List<string>();

		// Token: 0x04000186 RID: 390
		private List<string> CmdInform = new List<string>();

		// Token: 0x04000187 RID: 391
		private List<string> webCmd = new List<string>();

		// Token: 0x04000188 RID: 392
		private List<string> climaCmd = new List<string>();

		// Token: 0x04000189 RID: 393
		private List<string> correoCmd = new List<string>();

		// Token: 0x0400018A RID: 394
		private List<string> alarmaCmd = new List<string>();

		// Token: 0x0400018B RID: 395
		private List<string> facebookCmd = new List<string>();

		// Token: 0x0400018C RID: 396
		private List<string> recordatorioCmd = new List<string>();

		// Token: 0x0400018D RID: 397
		private List<string> reproductorCmd = new List<string>();

		// Token: 0x0400018E RID: 398
		private List<string> tecladoCmd = new List<string>();

		// Token: 0x0400018F RID: 399
		private List<string> atajosCmd = new List<string>();

		// Token: 0x04000190 RID: 400
		private List<string> atajosCmd1 = new List<string>();

		// Token: 0x04000191 RID: 401
		private List<string> calculadoraCmd = new List<string>();

		// Token: 0x04000192 RID: 402
		private List<string> aplicacionesCmd = new List<string>();

		// Token: 0x04000193 RID: 403
		private List<string> paginaswebsCmd = new List<string>();

		// Token: 0x04000194 RID: 404
		private List<string> carpetasCmd = new List<string>();

		// Token: 0x04000195 RID: 405
		private List<string> socialesCmd = new List<string>();

		// Token: 0x04000196 RID: 406
		private List<string> rssReaderCmd = new List<string>();

		// Token: 0x04000197 RID: 407
		private List<string> buscadorCmd = new List<string>();

		// Token: 0x04000198 RID: 408
		private List<string> cmdDitado = new List<string>();

		// Token: 0x04000199 RID: 409
		private List<string> CmdAssistente = new List<string>();

		// Token: 0x0400019A RID: 410
		private List<string> CmdAdicionais = new List<string>();

		// Token: 0x0400019B RID: 411
		private string[] cmdDictado = new string[]
		{
			"ativar ditado",
			"desativar ditado",
			"arroba",
			"abrir interrogação",
			"fechar interrogação",
			"abrir exclamação",
			"fechar exclamação",
			"abrir citações",
			"fechar citações",
			"um",
			"dois",
			"três",
			"quatro",
			"cinco",
			"seis",
			"sete",
			"oito",
			"nove",
			"zero",
			"abrir parênteses",
			"fechar parênteses",
			"abrir colchetes",
			"fechar colchetes",
			"abrir chaves",
			"fechar chaves",
			"michi"
		};

		// Token: 0x0400019C RID: 412
		internal Grid LayoutRoot;

		// Token: 0x0400019D RID: 413
		internal Image imgHud;

		// Token: 0x0400019E RID: 414
		internal TreeView tvCmd;

		// Token: 0x0400019F RID: 415
		internal ListBox lbItems;

		// Token: 0x040001A0 RID: 416
		internal Label lblNameS;

		// Token: 0x040001A1 RID: 417
		internal Label lblName;

		// Token: 0x040001A2 RID: 418
		internal Button BtnClose_NS;

		// Token: 0x040001A3 RID: 419
		internal Button BtnMini;

		// Token: 0x040001A4 RID: 420
		internal TextBox tbInfo;

		// Token: 0x040001A5 RID: 421
		internal Button BtnMaxim;

		// Token: 0x040001A6 RID: 422
		internal TextBox tbfunçoes;

		// Token: 0x040001A7 RID: 423
		private bool _contentLoaded;
	}
}
