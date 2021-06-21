using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;
using AutoUpdaterDotNET;
using AVJARVISB4.Clases;
using AVJARVISB4.Properties;
using AVJARVISB4.Resources;
using comabusc;
using Jarvis.CapaData;
using Jarvis.DataBaseManager;
using MailKit;
using MailKit.Net.Imap;
using Microsoft.Speech.Recognition;
using MoviePlayList;
using VirtualKey;
using WindowsInput;

namespace AVJARVISB4
{
	// Token: 0x02000008 RID: 8
	public class AVJARVIS : Window, IComponentConnector
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002135 File Offset: 0x00000335
		private void endSearchCmd()
		{
			this.comandoEjecutado = true;
			this.TimerRecOFF = Settings.Default.cfgRec;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004F1C File Offset: 0x0000311C
		public void frases()
		{
			this.SpeakOut(new string[]
			{
				"sim ",
				"agora mesmo ",
				"pronto ",
				"concluindo! ",
				"como quiser ",
				"muito bem ",
				"sim " + Settings.Default.cfgUser,
				"Agora mesmo " + Settings.Default.cfgUser,
				"pronto " + Settings.Default.cfgUser,
				"concluindo! " + Settings.Default.cfgUser,
				"como quiser " + Settings.Default.cfgUser,
				"muito bem " + Settings.Default.cfgUser,
				"Agora mesmo " + Settings.Default.cfgUser,
				"pronto " + Settings.Default.cfgUser
			});
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00005028 File Offset: 0x00003228
		public void frasesresp()
		{
			this.SpeakOut(new string[]
			{
				"não encontro o navegador",
				"não estou encontrando navegador selecionado",
				"selecione um navegador",
				"não encontro o navegador" + Settings.Default.cfgUser,
				"não estou encontrando navegador selecionado" + Settings.Default.cfgUser,
				"selecione um navegador" + Settings.Default.cfgUser
			});
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000050A0 File Offset: 0x000032A0
		private void Comandos_defecto(string _speech)
		{
			DateTime now = DateTime.Now;
			GrammarBuilder grammarBuilder = new GrammarBuilder();
			GrammarBuilder grammarBuilder2 = new GrammarBuilder();
			try
			{
				bool flag = _speech == "ativar ditado";
				if (flag)
				{
					this._reco.UnloadAllGrammars();
					this._reco.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(this.cmdDictado))));
					this.EnableDictado = true;
					this.SpeakOut(new string[]
					{
						"ditado ativado "
					});
				}
				else
				{
					bool flag2 = _speech == "Bom Dia" || _speech == "Boa tarde" || _speech == "Boa noite";
					if (flag2)
					{
						DateTime now2 = DateTime.Now;
						bool flag3 = now2.Hour <= 11 && now2.Hour < 24;
						if (flag3)
						{
							this.Hora = now2.ToString("hh:mm tt");
							string[] array = new string[3];
							string[] array2 = new string[5];
							array2[0] = "bom dia , .. agora são ";
							string[] array3 = array2;
							array3[1] = now2.Hour.ToString();
							array3[2] = " horas da manhã e ";
							array3[3] = now2.Minute.ToString();
							array3[4] = " minutos";
							array[0] = string.Concat(array3);
							array[1] = "Bom Dia " + Settings.Default.cfgUser;
							array[2] = "Bom Dia ";
							this.SpeakOut(array);
						}
						else
						{
							bool flag4 = now2.Hour > 11 && now2.Hour < 18;
							if (flag4)
							{
								string[] array4 = new string[3];
								string[] array5 = new string[5];
								array5[0] = "boa tarde ..agora são ";
								string[] array6 = array5;
								array6[1] = now2.Hour.ToString();
								array6[2] = " horas da tarde e ";
								array6[3] = now2.Minute.ToString();
								array6[4] = " minutos";
								array4[0] = string.Concat(array6);
								array4[1] = "Boa tarde " + Settings.Default.cfgUser;
								array4[2] = "Boa tarde ";
								this.SpeakOut(array4);
							}
							else
							{
								bool flag5 = now2.Hour <= 18 || now2.Hour < 24;
								if (flag5)
								{
									string[] array7 = new string[3];
									string[] array8 = new string[5];
									array8[0] = "boa Noite ..agora são ";
									string[] array9 = array8;
									array9[1] = now2.Hour.ToString();
									array9[2] = " horas da noite e ";
									array9[3] = now2.Minute.ToString();
									array9[4] = " minutos";
									array7[0] = string.Concat(array9);
									array7[1] = "Boa noite " + Settings.Default.cfgUser;
									array7[2] = "Boa noite ";
									this.SpeakOut(array7);
								}
								else
								{
									string[] array10 = new string[3];
									string[] array11 = new string[5];
									array11[0] = " já são ";
									string[] array12 = array11;
									array12[1] = now2.Hour.ToString();
									array12[2] = " horas e ";
									array12[3] = now2.Minute.ToString();
									array12[4] = "minutos posso te dizer boa noite";
									array10[0] = string.Concat(array12);
									array10[1] = "Boa noite " + Settings.Default.cfgUser;
									array10[2] = "Boa noite ";
									this.SpeakOut(array10);
								}
							}
						}
						this.endSearchCmd();
					}
					else
					{
						bool flag6 = _speech == "que horas são" || _speech == "quantas horas" || _speech == "quantas horas são";
						if (flag6)
						{
							DateTime now3 = DateTime.Now;
							bool flag7 = now3.Hour <= 11 && now3.Hour < 24;
							if (flag7)
							{
								string[] array13 = new string[5];
								array13[0] = "são";
								string[] array14 = array13;
								array14[1] = now3.Hour.ToString();
								array14[2] = " horas da manhã e ";
								array14[3] = now3.Minute.ToString();
								array14[4] = " minutos";
								this.SpeakOut(new string[]
								{
									string.Concat(array14)
								});
							}
							else
							{
								bool flag8 = now3.Hour > 11 && now3.Hour < 18;
								if (flag8)
								{
									string[] array15 = new string[5];
									array15[0] = "são";
									string[] array16 = array15;
									array16[1] = now3.Hour.ToString();
									array16[2] = " horas da tarde e ";
									array16[3] = now3.Minute.ToString();
									array16[4] = " minutos";
									this.SpeakOut(new string[]
									{
										string.Concat(array16)
									});
								}
								else
								{
									bool flag9 = now3.Hour <= 18 || now3.Hour < 24;
									if (flag9)
									{
										string[] array17 = new string[5];
										array17[0] = "são";
										string[] array18 = array17;
										array18[1] = now3.Hour.ToString();
										array18[2] = " horas da noite e ";
										array18[3] = now3.Minute.ToString();
										array18[4] = "minutos";
										this.SpeakOut(new string[]
										{
											string.Concat(array18)
										});
									}
									else
									{
										string[] array19 = new string[5];
										array19[0] = "são ";
										string[] array20 = array19;
										array20[1] = now3.Hour.ToString();
										array20[2] = " horas e ";
										array20[3] = now3.Minute.ToString();
										array20[4] = "minutos";
										this.SpeakOut(new string[]
										{
											string.Concat(array20)
										});
									}
								}
							}
							this.endSearchCmd();
						}
						else
						{
							bool flag10 = _speech == "Que dia é hoje" || _speech == "Hoje é que dia" || _speech == "Em que dia estamos";
							if (flag10)
							{
								string text = DateTime.Today.ToString("dddd");
								this.SpeakOut(new string[]
								{
									text
								});
								this.endSearchCmd();
							}
							else
							{
								bool flag11 = _speech == "em que ano estamos" || _speech == "estamos em que ano";
								if (flag11)
								{
									this.SpeakOut(new string[]
									{
										DateTime.Today.ToString("yyyy")
									});
								}
								else
								{
									bool flag12 = _speech == "em que mês estamos" || _speech == "estamos em que mês";
									if (flag12)
									{
										string str = "";
										switch (now.Month)
										{
										case 1:
											str = "janeiro";
											break;
										case 2:
											str = "fevereiro";
											break;
										case 3:
											str = "março";
											break;
										case 4:
											str = "abril";
											break;
										case 5:
											str = "maio";
											break;
										case 6:
											str = "junho";
											break;
										case 7:
											str = "julho";
											break;
										case 8:
											str = "agosto";
											break;
										case 9:
											str = "setembro";
											break;
										case 10:
											str = "outubro";
											break;
										case 11:
											str = "novembro";
											break;
										case 12:
											str = "dezembro";
											break;
										}
										this.SpeakOut(new string[]
										{
											"estamos no mês de " + str
										});
									}
									else
									{
										bool flag13 = _speech == "Data de hoje" || _speech == "Me informe a data" || _speech == "me diga a data" || _speech == "qual é a data de hoje" || _speech == "em que data estamos";
										if (flag13)
										{
											string text2 = now.ToString("dddd");
											string text3 = now.ToString("dd");
											string text4 = now.ToString("MMMM");
											string text5 = now.ToString("yyyy");
											this.SpeakOut(new string[]
											{
												string.Concat(new string[]
												{
													text2,
													" ",
													text3,
													" de ",
													text4,
													" de ",
													text5
												})
											});
											this.endSearchCmd();
										}
										else
										{
											bool flag14 = _speech == "detalhes dos processos" || _speech == "Me fale os detalhes dos processos" || _speech == "Qual são os detalhes dos processos" || _speech == "em que data estamos";
											if (flag14)
											{
												try
												{
													Process[] processes = Process.GetProcesses();
													this.SpeakOut(new string[]
													{
														"obtendo lista de processos"
													});
													Process[] array21 = processes;
													int num;
													for (int i = 0; i < array21.Length; i = num + 1)
													{
														Process process = array21[i];
														bool flag15 = process.MainWindowTitle != "";
														if (flag15)
														{
															this.SpeakOut(new string[]
															{
																process.MainWindowTitle + "está usando " + Convert.ToString(process.PagedMemorySize64 / 1024L / 1024L) + " mega baites"
															});
														}
														num = i;
													}
													this.lecturaEnable = true;
												}
												catch (Exception ex)
												{
													Exception ex2 = ex;
													this.SpeakOut(new string[]
													{
														"ocorreu um erro " + ex2.Message
													});
												}
											}
											else
											{
												bool flag16 = _speech == "quanta memória ram está sendo usada?" || _speech == "quanta memória está em uso";
												if (flag16)
												{
													this.SpeakOut(new string[]
													{
														"estão sendo usados " + ((float)PCStats.GetTotalMemory() - PCStats.GetFreeMemory()).ToString() + " megas baites de memória ram"
													});
												}
												else
												{
													bool flag17 = _speech == "quanta memória ram ainda há livre?" || _speech == "quanta memória há livre?";
													if (flag17)
													{
														this.SpeakOut(new string[]
														{
															"há " + ((int)PCStats.GetFreeMemory()).ToString() + " megas baites de memória ram livres "
														});
													}
													else
													{
														bool flag18 = _speech == "quanta memória ram há no total?" || _speech == "quanta memória há no total?";
														if (flag18)
														{
															this.SpeakOut(new string[]
															{
																"há " + ((int)PCStats.GetTotalMemory()).ToString() + " megas baites de memória ram no total "
															});
														}
														else
														{
															bool flag19 = _speech == "uso do processador" || _speech == "como está o uso do processador";
															if (flag19)
															{
																this.SpeakOut(new string[]
																{
																	"o uso do processador estar em " + Math.Round(PCStats.GetCPUUsage(), 2).ToString() + "por cento "
																});
															}
															else
															{
																bool flag20 = _speech == "Quais são as notícias" || _speech == "próxima notícia" || _speech == "notícia de hoje" || _speech == "notícia seguinte";
																if (flag20)
																{
																	this.lecturaEnable = true;
																	this.newsFromG1 = G1FeedNews.GetNews();
																	this.SpeakOut(new string[]
																	{
																		"Título da notícia... " + this.newsFromG1[this.newsIndex].Title + " ... " + this.newsFromG1[this.newsIndex].Text
																	});
																	int num = this.newsIndex;
																	this.newsIndex = num + 1;
																	this.endSearchCmd();
																}
																else
																{
																	bool flag21 = _speech == "abrir lembrete" || _speech == "acessar o lembrete";
																	if (flag21)
																	{
																		this.SpeakOutStop(new string[]
																		{
																			"As anotações, está sendo aberta",
																			"pronto " + Settings.Default.cfgUser
																		});
																		this.recd = new Recordatorio();
																		this.recd.Show();
																		this.TimerRecOFF = 0;
																		this.ventanaOpen = true;
																		this.lblSpeech2.Content = "";
																		this.lblSpeech2.Content = "Reconhecimento Desligado Lembre Detectado";
																		this.recd.Closed += delegate(object a, EventArgs b)
																		{
																			this.recd = null;
																			this.loadDataRec();
																			this.SpeakOut(new string[]
																			{
																				"em linha "
																			});
																			this.ventanaOpen = false;
																		};
																		this.endSearchCmd();
																	}
																	else
																	{
																		bool flag22 = _speech == "configure um lembrete" || _speech == "adicionar um lembrete" || _speech == "me lembre";
																		if (flag22)
																		{
																			this.alarmaSet = true;
																			this.SpeakOut(new string[]
																			{
																				"a que hora " + Settings.Default.cfgUser
																			});
																			this.lblSpeech2.Content = " a que hora ";
																			this.endSearchCmd();
																		}
																		else
																		{
																			bool flag23 = _speech == "Adicionar novo comando" || _speech == "ingresar comandos" || _speech == "editor de comandos" || _speech == "Adicionar comando";
																			if (flag23)
																			{
																				this.SpeakOutStop(new string[]
																				{
																					"Mostrando editor de comandos para o assistente ",
																					"Mostrando editor do assistente "
																				});
																				this.cmds = new Comandos();
																				this.cmds.Show();
																				this.cmds.Activate();
																				this.TimerRecOFF = 0;
																				this.ventanaOpen = true;
																				this.lblSpeech2.Content = "";
																				this.lblSpeech2.Content = "Reconhecimento Desligado Placa Detectada";
																				this.cmds.Closed += delegate(object a, EventArgs b)
																				{
																					this.cmds = null;
																					this.posiçãodeiniciovoz();
																					this.UnloadGramarTabla();
																					this.LoadGramarTabla();
																					this.ventanaOpen = false;
																				};
																				this.endSearchCmd();
																			}
																			else
																			{
																				bool flag24 = _speech == "Dispensado" || _speech == "fechar o assistente" || _speech == "Dispensada" || _speech == "fechar a assistente";
																				if (flag24)
																				{
																					this.SpeakOutStop(new string[]
																					{
																						"Até logo " + Settings.Default.cfgUser,
																						"desativando assistente ",
																						"até a próxima " + Settings.Default.cfgUser
																					});
																					System.Windows.Application.Current.Shutdown();
																					this.endSearchCmd();
																				}
																				else
																				{
																					bool flag25 = _speech == "minimizar o assistente" || _speech == "Sai da frente" || _speech == "minimizar a assistente";
																					if (flag25)
																					{
																						bool flag26 = base.WindowState != WindowState.Normal && base.WindowState != WindowState.Maximized;
																						if (flag26)
																						{
																							this.SpeakOut(new string[]
																							{
																								"já está desativada " + Settings.Default.cfgUser,
																								"não tem como fazer isso outra vez ",
																								"já fiz isso " + Settings.Default.cfgUser
																							});
																						}
																						else
																						{
																							base.WindowState = WindowState.Minimized;
																							this.notifi();
																							this.SpeakOut(new string[]
																							{
																								"barra em estado desativado ",
																								"ocultando assistente "
																							});
																						}
																						this.endSearchCmd();
																					}
																					else
																					{
																						bool flag27 = _speech == "mostrar assistente" || _speech == "ver assistente" || _speech == "mostre o assistente" || _speech == "mostre a assistente";
																						if (flag27)
																						{
																							bool flag28 = base.WindowState != WindowState.Minimized;
																							if (flag28)
																							{
																								this.SpeakOut(new string[]
																								{
																									"já foi ativada " + Settings.Default.cfgUser,
																									"não tem como fazer isso outra vez ",
																									"já fiz isso " + Settings.Default.cfgUser
																								});
																							}
																							else
																							{
																								base.Show();
																								this.SpeakOut(new string[]
																								{
																									"barra em estado ativado ",
																									"Mostrando assistente "
																								});
																								base.WindowState = WindowState.Normal;
																							}
																							this.endSearchCmd();
																						}
																						else
																						{
																							bool flag29 = _speech == "Menu de Configuração" || _speech == "mostrar Configuração" || _speech == "configurar o Assistente" || _speech == "configurar a Assistente" || _speech == "Ver Configuração";
																							if (flag29)
																							{
																								this.mostrarConfiguracion();
																								this.endSearchCmd();
																							}
																							else
																							{
																								bool flag30 = _speech == "ver placa de comandos" || _speech == "placa de comandos" || _speech == "ver comandos";
																								if (flag30)
																								{
																									this.SpeakOut(new string[]
																									{
																										"Mostrando comandos para assistencia ",
																										"Abrindo Placa de comando para assistencia "
																									});
																									bool flag31 = this.vinfo == null;
																									if (flag31)
																									{
																										this.vinfo = new VentanaInfo();
																										this.vinfo.Closed += delegate(object a, EventArgs b)
																										{
																											this.vinfo = null;
																										};
																									}
																									this.vinfo.WindowState = WindowState.Normal;
																									this.vinfo.Show();
																									this.vinfo.Activate();
																									this.vinfo.lbItems.Items.Clear();
																									this.endSearchCmd();
																								}
																								else
																								{
																									bool flag32 = _speech == "acessar o teclado por voz" || _speech == "mostrar o teclado por voz" || _speech == "Abri o teclado de voz";
																									if (flag32)
																									{
																										this.SpeakOutStop(new string[]
																										{
																											"Mostrando configurações das teclas de fala "
																										});
																										this.vk = new MainWindow();
																										this.vk.Show();
																										this.TimerRecOFF = 0;
																										this.ventanaOpen = true;
																										this.lblSpeech2.Content = "";
																										this.lblSpeech2.Content = "Reconhecimento Desligado Teclado Detectado";
																										this.vk.Closed += delegate(object a, EventArgs b)
																										{
																											this.vk = null;
																											this.UnloadGrammarTablaKey();
																											this.LoadGramarTablaKey();
																											this.posiçãodeiniciovoz();
																											this.ventanaOpen = false;
																										};
																										this.endSearchCmd();
																									}
																									else
																									{
																										bool flag33 = _speech == "ativar teclado de voz" || _speech == "ativar teclado" || _speech == "habilitar o uso de teclados já configurados";
																										if (flag33)
																										{
																											this.LoadtecladoOn = true;
																											this.rectSkin2.Stroke = new SolidColorBrush(Colors.Orange);
																											this.SpeakOut(new string[]
																											{
																												"teclas e modo normal ",
																												"O teclado agora está ligado "
																											});
																											this.endSearchCmd();
																										}
																										else
																										{
																											bool flag34 = _speech == "desativar teclado de voz" || _speech == "desligar o teclado de voz" || _speech == "desativar teclado";
																											if (flag34)
																											{
																												this.LoadtecladoOn = false;
																												this.rectSkin2.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(36, 79, 126));
																												this.SpeakOut(new string[]
																												{
																													"teclas e modo offi ",
																													"teclas desligadas "
																												});
																												this.endSearchCmd();
																											}
																											else
																											{
																												bool flag35 = _speech == "abrir bandeja" || _speech == "abrir bandeja de CD" || _speech == "abrir bandeja de DVD";
																												if (flag35)
																												{
																													Winm.abrirBandeja();
																													this.SpeakOut(new string[]
																													{
																														"bandeja aberta ",
																														"pronto "
																													});
																													this.endSearchCmd();
																												}
																												else
																												{
																													bool flag36 = _speech == "fechar bandeja" || _speech == "fechar bandeja de DVD" || _speech == "fechar bandeja de CD";
																													if (flag36)
																													{
																														this.SpeakOut(new string[]
																														{
																															"Pronto dispositivo de disco unidade de CD e DVD está sendo recolhido. "
																														});
																														Winm.cerrarBandeja();
																														this.endSearchCmd();
																													}
																													else
																													{
																														bool flag37 = _speech == "apagar monitor" || _speech == "desligar a tela" || _speech == "desligar o Monitor";
																														if (flag37)
																														{
																															User32.SetMonitorOFF();
																															this.endSearchCmd();
																														}
																														else
																														{
																															bool flag38 = _speech == "Acessar meus documentos" || _speech == "Abri documentos";
																															if (flag38)
																															{
																																Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
																																this.SpeakOut(new string[]
																																{
																																	"muito bem ",
																																	"documentos localizados "
																																});
																																this.endSearchCmd();
																															}
																															else
																															{
																																bool flag39 = _speech == "Acessar minhas imagens" || _speech == "Abri pasta de imagens";
																																if (flag39)
																																{
																																	Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
																																	this.SpeakOut(new string[]
																																	{
																																		"muito bem ",
																																		"imagens localizadas "
																																	});
																																	this.endSearchCmd();
																																}
																																else
																																{
																																	bool flag40 = _speech == "Acessar meus vídeos" || _speech == "Abri meus vídeos";
																																	if (flag40)
																																	{
																																		Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
																																		this.SpeakOut(new string[]
																																		{
																																			"muito bem ",
																																			"vídeos localizados "
																																		});
																																		this.endSearchCmd();
																																	}
																																	else
																																	{
																																		bool flag41 = _speech == "Acessar minhas músicas" || _speech == "Abri pasta de músicas";
																																		if (flag41)
																																		{
																																			Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
																																			this.SpeakOut(new string[]
																																			{
																																				"muito bem ",
																																				"músicas localizadas "
																																			});
																																			this.endSearchCmd();
																																		}
																																		else
																																		{
																																			bool flag42 = _speech == "almentar volume do windows" || _speech == "almentar o volume do windows" || _speech == "almentar mais o volume do windows";
																																			if (flag42)
																																			{
																																				this.volumewinup();
																																				this.SpeakOut(new string[]
																																				{
																																					"calibrando mais dois porcento do volume ",
																																					"pronto "
																																				});
																																				this.endSearchCmd();
																																			}
																																			else
																																			{
																																				bool flag43 = _speech == "almentar volume em seis por cento" || _speech == "almentar o volume em seis por cento" || _speech == "almentar seis por cento" || _speech == "almentar mais seis por cento";
																																				if (flag43)
																																				{
																																					int num;
																																					for (int j = 0; j < 3; j = num + 1)
																																					{
																																						this.volumewinup();
																																						Thread.Sleep(50);
																																						num = j;
																																					}
																																					this.SpeakOut(new string[]
																																					{
																																						"calibrando mais seis porcento do volume ",
																																						"pronto "
																																					});
																																					this.endSearchCmd();
																																				}
																																				else
																																				{
																																					bool flag44 = _speech == "almentar volume em dez por cento" || _speech == "almentar o volume em dez por cento" || _speech == "almentar dez por cento" || _speech == "almentar mais dez por cento";
																																					if (flag44)
																																					{
																																						int num;
																																						for (int k = 0; k < 5; k = num + 1)
																																						{
																																							this.volumewinup();
																																							Thread.Sleep(50);
																																							num = k;
																																						}
																																						this.SpeakOut(new string[]
																																						{
																																							"calibrando mais dez porcento do volume ",
																																							"pronto "
																																						});
																																						this.endSearchCmd();
																																					}
																																					else
																																					{
																																						bool flag45 = _speech == "almentar volume em trinta por cento" || _speech == "almentar o volume em trinta por cento" || _speech == "almentar trinta por cento" || _speech == "almentar mais trinta por cento";
																																						if (flag45)
																																						{
																																							int num;
																																							for (int l = 0; l < 15; l = num + 1)
																																							{
																																								this.volumewinup();
																																								Thread.Sleep(50);
																																								num = l;
																																							}
																																							this.SpeakOut(new string[]
																																							{
																																								"calibrando mais trinta porcento do volume ",
																																								"pronto "
																																							});
																																							this.endSearchCmd();
																																						}
																																						else
																																						{
																																							bool flag46 = _speech == "almentar volume em cinquenta por cento" || _speech == "almentar o volume em cinquenta por cento" || _speech == "almentar cinquenta por cento" || _speech == "almentar mais cinquenta por cento";
																																							if (flag46)
																																							{
																																								int num;
																																								for (int m = 0; m < 25; m = num + 1)
																																								{
																																									this.volumewinup();
																																									Thread.Sleep(50);
																																									num = m;
																																								}
																																								this.SpeakOut(new string[]
																																								{
																																									"calibrando mais cinquenta porcento do volume ",
																																									"pronto "
																																								});
																																								this.endSearchCmd();
																																							}
																																							else
																																							{
																																								bool flag47 = _speech == "almentar volume ao máximo" || _speech == "almentar ao máximo o volume do windows" || _speech == "almentar ao máximo" || _speech == "almentar volume ao máximo";
																																								if (flag47)
																																								{
																																									int num;
																																									for (int n = 0; n < 50; n = num + 1)
																																									{
																																										this.volumewinup();
																																										Thread.Sleep(50);
																																										num = n;
																																									}
																																									this.SpeakOut(new string[]
																																									{
																																										"Almentando ao maximo o volume do windows ",
																																										"pronto "
																																									});
																																									this.endSearchCmd();
																																								}
																																								else
																																								{
																																									bool flag48 = _speech == "diminuir volume do windows" || _speech == "diminuir o volume do windows" || _speech == "diminuir o volume" || _speech == "diminuir mais o volume do windows";
																																									if (flag48)
																																									{
																																										this.volumewindow();
																																										this.SpeakOut(new string[]
																																										{
																																											"suavizando em menos dois porcento do volume "
																																										});
																																										this.endSearchCmd();
																																									}
																																									else
																																									{
																																										bool flag49 = _speech == "diminuir volume em seis por cento" || _speech == "diminuir o volume em seis por cento" || _speech == "diminuir mais seis por cento" || _speech == "diminuir seis por cento";
																																										if (flag49)
																																										{
																																											int num;
																																											for (int num2 = 0; num2 < 3; num2 = num + 1)
																																											{
																																												this.volumewindow();
																																												Thread.Sleep(50);
																																												num = num2;
																																											}
																																											this.SpeakOut(new string[]
																																											{
																																												"suavizando em menos seis porcento do volume ",
																																												"pronto "
																																											});
																																											this.endSearchCmd();
																																										}
																																										else
																																										{
																																											bool flag50 = _speech == "diminuir volume em dez por cento" || _speech == "diminuir o volume em dez por cento" || _speech == "diminuir mais dez por cento" || _speech == "diminuir dez por cento";
																																											if (flag50)
																																											{
																																												int num;
																																												for (int num3 = 0; num3 < 5; num3 = num + 1)
																																												{
																																													this.volumewindow();
																																													Thread.Sleep(50);
																																													num = num3;
																																												}
																																												this.SpeakOut(new string[]
																																												{
																																													"suavizando em menos dez porcento do volume ",
																																													"pronto "
																																												});
																																												this.endSearchCmd();
																																											}
																																											else
																																											{
																																												bool flag51 = _speech == "diminuir volume em trinta por cento" || _speech == "diminuir o volume em trinta por cento" || _speech == "diminuir trinta por cento" || _speech == "diminuir mais trinta por cento";
																																												if (flag51)
																																												{
																																													int num;
																																													for (int num4 = 0; num4 < 15; num4 = num + 1)
																																													{
																																														this.volumewindow();
																																														Thread.Sleep(50);
																																														num = num4;
																																													}
																																													this.SpeakOut(new string[]
																																													{
																																														"suavizando em menos trinta porcento do volume ",
																																														"pronto "
																																													});
																																													this.endSearchCmd();
																																												}
																																												else
																																												{
																																													bool flag52 = _speech == "diminuir volume em cinquenta por cento" || _speech == "diminuir o volume em cinquenta por cento" || _speech == "diminuir cinquenta por cento" || _speech == "diminuir mais cinquenta por cento";
																																													if (flag52)
																																													{
																																														int num;
																																														for (int num5 = 0; num5 < 25; num5 = num + 1)
																																														{
																																															this.volumewindow();
																																															Thread.Sleep(50);
																																															num = num5;
																																														}
																																														this.SpeakOut(new string[]
																																														{
																																															"suavizando em menos cinquenta porcento do volume ",
																																															"pronto "
																																														});
																																														this.endSearchCmd();
																																													}
																																													else
																																													{
																																														bool flag53 = _speech == "deixar o windows mudo" || _speech == "Silenciar o windows" || _speech == "windows mundo";
																																														if (flag53)
																																														{
																																															this.mutewin();
																																															this.SpeakOut(new string[]
																																															{
																																																"suavizando o volume total "
																																															});
																																															this.endSearchCmd();
																																														}
																																														else
																																														{
																																															bool flag54 = _speech == "Abrir menu iniciar" || _speech == "menu iniciar" || _speech == "abrir menu do windows";
																																															if (flag54)
																																															{
																																																InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.ESCAPE);
																																																this.SpeakOut(new string[]
																																																{
																																																	"sim ",
																																																	"menu pronto"
																																																});
																																																this.endSearchCmd();
																																															}
																																															else
																																															{
																																																bool flag55 = _speech == "fechar menu do windows" || _speech == "fechar menu iniciar" || _speech == "sair do menu iniciar";
																																																if (flag55)
																																																{
																																																	InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.ESCAPE);
																																																	this.frases();
																																																	this.endSearchCmd();
																																																}
																																																else
																																																{
																																																	bool flag56 = _speech == "buscar texto selecionado" || _speech == "buscar isto" || _speech == "busque isto" || _speech == "Pesquisar isto" || _speech == "Pesquisar texto selecionado";
																																																	if (flag56)
																																																	{
																																																		InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
																																																		Thread.Sleep(100);
																																																		Process.Start("https://www.google.com/search?q=" + System.Windows.Clipboard.GetText());
																																																		this.SpeakOut(new string[]
																																																		{
																																																			"Buscando " + System.Windows.Clipboard.GetText()
																																																		});
																																																		this.endSearchCmd();
																																																	}
																																																	else
																																																	{
																																																		bool flag57 = _speech == "ler isto" || _speech == "leia isto";
																																																		if (flag57)
																																																		{
																																																			try
																																																			{
																																																				InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
																																																				Thread.Sleep(300);
																																																				this.lecturaEnable = true;
																																																				this.SpeakOut(new string[]
																																																				{
																																																					System.Windows.Clipboard.GetText()
																																																				});
																																																				this.btnFiltro.Background = new SolidColorBrush(Colors.White);
																																																				Settings.Default.soundOn = false;
																																																				Settings.Default.Save();
																																																			}
																																																			catch
																																																			{
																																																				this.SpeakOut(new string[]
																																																				{
																																																					"você deve selecionar um texto para ler "
																																																				});
																																																			}
																																																			this.endSearchCmd();
																																																		}
																																																		else
																																																		{
																																																			bool flag58 = _speech == "ler todo texto" || _speech == "leia tudo isso" || _speech == "ler tudo isso" || _speech == "leia todo o texto";
																																																			if (flag58)
																																																			{
																																																				try
																																																				{
																																																					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);
																																																					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
																																																					Thread.Sleep(300);
																																																					this.lecturaEnable = true;
																																																					this.SpeakOut(new string[]
																																																					{
																																																						System.Windows.Clipboard.GetText()
																																																					});
																																																					this.btnFiltro.Background = new SolidColorBrush(Colors.White);
																																																					Settings.Default.soundOn = false;
																																																					Settings.Default.Save();
																																																				}
																																																				catch
																																																				{
																																																					this.SpeakOut(new string[]
																																																					{
																																																						"o texto não foi encontrado "
																																																					});
																																																				}
																																																				this.endSearchCmd();
																																																			}
																																																			else
																																																			{
																																																				bool flag59 = _speech == "capturar tela do computador" || _speech == "capturar tela" || _speech == "tira foto da tela" || _speech == "tira uma foto da tela";
																																																				if (flag59)
																																																				{
																																																					this.TakeScreenShot(this.Capture);
																																																					Process.Start(this.Capture);
																																																					this.SpeakOut(new string[]
																																																					{
																																																						"print concluido ",
																																																						"tirando uma foto da tela "
																																																					});
																																																					this.endSearchCmd();
																																																				}
																																																				else
																																																				{
																																																					bool flag60 = _speech == "Desligar Computador" || _speech == "Desligue o computador";
																																																					if (flag60)
																																																					{
																																																						bool flag61 = !this.ShutdownTimer;
																																																						if (flag61)
																																																						{
																																																							this.QEvent = "apagar";
																																																							this.SpeakOutStop(new string[]
																																																							{
																																																								"Iniciando desativação total do sistema "
																																																							});
																																																							this.Timer.Start();
																																																							this.ShutdownTimer = true;
																																																							this.EnableShutdown = true;
																																																							this.btnFiltro.Background = new SolidColorBrush(Colors.White);
																																																							this.lblSpeech2.Content = "Diga>CANCELAR ou ABORTAR em dez minutos";
																																																						}
																																																						this.endSearchCmd();
																																																					}
																																																					else
																																																					{
																																																						bool flag62 = _speech == "reiniciar Computador" || _speech == "reiniciar o computador" || _speech == "reiniciar o sistema";
																																																						if (flag62)
																																																						{
																																																							bool flag63 = !this.ShutdownTimer;
																																																							if (flag63)
																																																							{
																																																								this.QEvent = "reiniciar";
																																																								this.SpeakOutStop(new string[]
																																																								{
																																																									"resetando o sistema ",
																																																									"iniciando a contagem "
																																																								});
																																																								this.Timer.Start();
																																																								this.ShutdownTimer = true;
																																																								this.EnableShutdown = true;
																																																								this.btnFiltro.Background = new SolidColorBrush(Colors.White);
																																																								this.lblSpeech2.Content = "Diga>CANCELAR ou ABORTAR em dez minutos";
																																																							}
																																																							this.endSearchCmd();
																																																						}
																																																						else
																																																						{
																																																							bool flag64 = _speech == "suspender o sistema" || _speech == "encerrar a seção";
																																																							if (flag64)
																																																							{
																																																								bool flag65 = !this.ShutdownTimer;
																																																								if (flag65)
																																																								{
																																																									this.QEvent = "sesionoff";
																																																									this.SpeakOutStop(new string[]
																																																									{
																																																										"encerrando ",
																																																										"saindo "
																																																									});
																																																									this.ShutdownTimer = true;
																																																									this.Timer.Start();
																																																									this.EnableShutdown = true;
																																																									this.btnFiltro.Background = new SolidColorBrush(Colors.White);
																																																									this.lblSpeech2.Content = "Diga>CANCELAR ou ABORTAR em dez minutos";
																																																								}
																																																								this.endSearchCmd();
																																																							}
																																																							else
																																																							{
																																																								bool flag66 = _speech == "limpar lixeira" || _speech == "esvaziar lixeira" || _speech == "limpe a lixeira";
																																																								if (flag66)
																																																								{
																																																									Shell32.limpiarPapelera();
																																																									this.SpeakOut(new string[]
																																																									{
																																																										"executando limpeza total ",
																																																										"pronto "
																																																									});
																																																									this.endSearchCmd();
																																																								}
																																																								else
																																																								{
																																																									bool flag67 = _speech == "mostrar área de trabalho" || _speech == "mostrar a área de trabalho" || _speech == "minimizar todas as janelas";
																																																									if (flag67)
																																																									{
																																																										Type typeFromProgID = Type.GetTypeFromProgID("Shell.Application");
																																																										object target = Activator.CreateInstance(typeFromProgID);
																																																										typeFromProgID.InvokeMember("MinimizeAll", BindingFlags.InvokeMethod, null, target, null);
																																																										this.SpeakOut(new string[]
																																																										{
																																																											"indo até o Desktop ",
																																																											"olcutando as janelas "
																																																										});
																																																										this.endSearchCmd();
																																																									}
																																																									else
																																																									{
																																																										bool flag68 = _speech == "minimizar janela" || _speech == "minimizar" || _speech == "minimize essa janela";
																																																										if (flag68)
																																																										{
																																																											this.HandleID = User32.minimizarExplorer();
																																																											this.SpeakOut(new string[]
																																																											{
																																																												"olcuto "
																																																											});
																																																											this.endSearchCmd();
																																																										}
																																																										else
																																																										{
																																																											bool flag69 = _speech == "maximizar janela" || _speech == "maximizar" || _speech == "maximize essa janela";
																																																											if (flag69)
																																																											{
																																																												User32.maximizaExplorer();
																																																												this.SpeakOut(new string[]
																																																												{
																																																													"pronto "
																																																												});
																																																												this.endSearchCmd();
																																																											}
																																																											else
																																																											{
																																																												bool flag70 = _speech == "restaurar janela" || _speech == "restaurar a janela" || _speech == "restaure essa janela";
																																																												if (flag70)
																																																												{
																																																													User32.restaurarVentana(this.HandleID);
																																																													this.frases();
																																																													this.endSearchCmd();
																																																												}
																																																												else
																																																												{
																																																													bool flag71 = _speech == "trocar de janela" || _speech == "mudar de janela" || _speech == "trocar janela";
																																																													if (flag71)
																																																													{
																																																														InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.ESCAPE);
																																																														this.SpeakOut(new string[]
																																																														{
																																																															"pronto ",
																																																															"Alterando janela "
																																																														});
																																																														this.endSearchCmd();
																																																													}
																																																													else
																																																													{
																																																														bool flag72 = _speech == "percorrer janelas" || _speech == "ativar tela de intens";
																																																														if (flag72)
																																																														{
																																																															InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.TAB);
																																																															this.TimerFlip.Start();
																																																															this.frases();
																																																															this.endSearchCmd();
																																																															this.flip = "reset";
																																																														}
																																																														else
																																																														{
																																																															bool flag73 = _speech == "sim";
																																																															if (flag73)
																																																															{
																																																																InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.VK_S);
																																																																this.frases();
																																																																this.endSearchCmd();
																																																															}
																																																															else
																																																															{
																																																																bool flag74 = _speech == "não";
																																																																if (flag74)
																																																																{
																																																																	InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.VK_N);
																																																																	this.frases();
																																																																	this.endSearchCmd();
																																																																}
																																																																else
																																																																{
																																																																	bool flag75 = _speech == "seguinte";
																																																																	if (flag75)
																																																																	{
																																																																		InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.VK_T);
																																																																		this.frases();
																																																																		this.endSearchCmd();
																																																																	}
																																																																	else
																																																																	{
																																																																		bool flag76 = _speech == "ative o alarme" || _speech == "ativar o alarme";
																																																																		if (flag76)
																																																																		{
																																																																			this.setAlarma();
																																																																			this.SpeakOut(new string[]
																																																																			{
																																																																				"ligando o despertador ",
																																																																				"pronto o alarme foi ligado "
																																																																			});
																																																																			this.endSearchCmd();
																																																																		}
																																																																		else
																																																																		{
																																																																			bool flag77 = _speech == "configurar o alarme" || _speech == "configure o alarme";
																																																																			if (flag77)
																																																																			{
																																																																				this.alarmaSet = true;
																																																																				this.TimerAviso.Start();
																																																																				this.SpeakOut(new string[]
																																																																				{
																																																																					"a que hora " + Settings.Default.cfgUser
																																																																				});
																																																																				this.lblSpeech2.Content = "a que hora";
																																																																				this.endSearchCmd();
																																																																			}
																																																																			else
																																																																			{
																																																																				bool flag78 = _speech == "desativar o alarme" || _speech == "desligar o alarme";
																																																																				if (flag78)
																																																																				{
																																																																					this.clearAlarma();
																																																																					this.setdefaut();
																																																																					this.SpeakOut(new string[]
																																																																					{
																																																																						"despertador desligado ",
																																																																						"agora o alarme foi desligado "
																																																																					});
																																																																					this.endSearchCmd();
																																																																				}
																																																																				else
																																																																				{
																																																																					bool flag79 = _speech == "Mostre-me o alarme" || _speech == "abrir alarme" || _speech == "ver o alarme" || _speech == "abrir o alarme";
																																																																					if (flag79)
																																																																					{
																																																																						this.SpeakOutStop(new string[]
																																																																						{
																																																																							"Abrindo Menu de configurações do Alarme "
																																																																						});
																																																																						this.TimerRecOFF = 0;
																																																																						this.ventanaOpen = true;
																																																																						this.lblSpeech2.Content = "";
																																																																						this.lblSpeech2.Content = "Reconhecimento Desligado Alarme Detectado";
																																																																						bool flag80 = this.alarm == null;
																																																																						if (flag80)
																																																																						{
																																																																							this.alarm = new Alarm();
																																																																							this.alarm.Closed += delegate(object a, EventArgs b)
																																																																							{
																																																																								this.alarm = null;
																																																																								this.SpeakOut(new string[]
																																																																								{
																																																																									"em linha "
																																																																								});
																																																																								this.ventanaOpen = false;
																																																																							};
																																																																						}
																																																																						else
																																																																						{
																																																																							this.alarm = null;
																																																																							this.alarm = new Alarm();
																																																																						}
																																																																						this.alarm.WindowState = WindowState.Normal;
																																																																						this.alarm.Show();
																																																																						this.alarm.Activate();
																																																																						this.endSearchCmd();
																																																																					}
																																																																					else
																																																																					{
																																																																						bool flag81 = _speech == "estado do alarme" || _speech == "como esta o alarme" || _speech == "qual é o estado do alarme";
																																																																						if (flag81)
																																																																						{
																																																																							bool flag82 = Settings.Default.cLun || Settings.Default.cMar || Settings.Default.cMie || Settings.Default.cJue || Settings.Default.cVie || Settings.Default.cSab || Settings.Default.cDom;
																																																																							if (flag82)
																																																																							{
																																																																								this.SpeakOut(new string[]
																																																																								{
																																																																									"O Alarme está Ativado " + Settings.Default.cfgUser
																																																																								});
																																																																							}
																																																																							else
																																																																							{
																																																																								this.SpeakOut(new string[]
																																																																								{
																																																																									"O Alarme se encontra desativado ",
																																																																									"o alarme está desativado"
																																																																								});
																																																																							}
																																																																							this.endSearchCmd();
																																																																						}
																																																																						else
																																																																						{
																																																																							bool flag83 = _speech == "copiar" || _speech == "copiar isto";
																																																																							if (flag83)
																																																																							{
																																																																								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
																																																																								this.endSearchCmd();
																																																																								this.SpeakOut(new string[]
																																																																								{
																																																																									"copiado ",
																																																																									"pronto "
																																																																								});
																																																																							}
																																																																							else
																																																																							{
																																																																								bool flag84 = _speech == "rastro" || _speech == "clone" || _speech == "Cole o texto" || _speech == "Cola" || _speech == "Cola todo texto";
																																																																								if (flag84)
																																																																								{
																																																																									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
																																																																									this.endSearchCmd();
																																																																									this.SpeakOut(new string[]
																																																																									{
																																																																										"colando ",
																																																																										"todo o texto copiado, agora foi colado "
																																																																									});
																																																																								}
																																																																								else
																																																																								{
																																																																									bool flag85 = _speech == "cortar" || _speech == "recortar" || _speech == "cortar isto";
																																																																									if (flag85)
																																																																									{
																																																																										InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_X);
																																																																										this.endSearchCmd();
																																																																										this.SpeakOut(new string[]
																																																																										{
																																																																											"recortado ",
																																																																											"sim",
																																																																											"agora mesmo"
																																																																										});
																																																																									}
																																																																									else
																																																																									{
																																																																										bool flag86 = _speech == "cancelar essa ação" || _speech == "abortar essa ação" || _speech == "cancele a ação";
																																																																										if (flag86)
																																																																										{
																																																																											InputSimulator.SimulateKeyDown(VirtualKeyCode.ESCAPE);
																																																																											this.endSearchCmd();
																																																																											this.SpeakOut(new string[]
																																																																											{
																																																																												"A ação foi cancelada ",
																																																																												"cancelando",
																																																																												"agora mesmo a ação foi cancelada"
																																																																											});
																																																																										}
																																																																										else
																																																																										{
																																																																											bool flag87 = _speech == "cancelar" || _speech == "abortar";
																																																																											if (flag87)
																																																																											{
																																																																												InputSimulator.SimulateKeyDown(VirtualKeyCode.ESCAPE);
																																																																												this.comandoEjecutado = true;
																																																																												this.SpeakOut(new string[]
																																																																												{
																																																																													"A ação foi cancelada ",
																																																																													"cancelando",
																																																																													"agora mesmo a ação foi cancelada"
																																																																												});
																																																																											}
																																																																											else
																																																																											{
																																																																												bool flag88 = _speech == "desfazer" || _speech == "desfazer essa ação" || _speech == "desfaça essa ação";
																																																																												if (flag88)
																																																																												{
																																																																													InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_Z);
																																																																													this.endSearchCmd();
																																																																													this.frases();
																																																																												}
																																																																												else
																																																																												{
																																																																													bool flag89 = _speech == "refazer essa ação" || _speech == "refaça essa ação" || _speech == "refazer ação";
																																																																													if (flag89)
																																																																													{
																																																																														InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_Y);
																																																																														this.endSearchCmd();
																																																																														this.frases();
																																																																													}
																																																																													else
																																																																													{
																																																																														bool flag90 = _speech == "Entrar" || _speech == "aceitar" || _speech == "okey";
																																																																														if (flag90)
																																																																														{
																																																																															InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
																																																																															this.endSearchCmd();
																																																																															this.frases();
																																																																														}
																																																																														else
																																																																														{
																																																																															bool flag91 = _speech == "fechar isto" || _speech == "fechar janela" || _speech == "fechar aplicativo" || _speech == "fechar o aplicativo" || _speech == "fechar esse aplicativo" || _speech == "feche esse aplicativo" || _speech == "sair do programa";
																																																																															if (flag91)
																																																																															{
																																																																																InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.F4);
																																																																																this.endSearchCmd();
																																																																																this.frases();
																																																																															}
																																																																															else
																																																																															{
																																																																																bool flag92 = _speech == "guardar" || _speech == "Salvar" || _speech == "Salvar isto";
																																																																																if (flag92)
																																																																																{
																																																																																	InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_G);
																																																																																	InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_S);
																																																																																	this.endSearchCmd();
																																																																																	this.frases();
																																																																																}
																																																																																else
																																																																																{
																																																																																	bool flag93 = _speech == "Guardar Bloco de notas como" || _speech == "Salvar Bloco de notas como";
																																																																																	if (flag93)
																																																																																	{
																																																																																		InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, new VirtualKeyCode[]
																																																																																		{
																																																																																			VirtualKeyCode.SHIFT,
																																																																																			VirtualKeyCode.VK_S
																																																																																		});
																																																																																		this.endSearchCmd();
																																																																																		this.frases();
																																																																																	}
																																																																																	else
																																																																																	{
																																																																																		bool flag94 = _speech == "Guardar Documento" || _speech == "Salvar Documento";
																																																																																		if (flag94)
																																																																																		{
																																																																																			InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_G);
																																																																																			this.endSearchCmd();
																																																																																			this.frases();
																																																																																		}
																																																																																		else
																																																																																		{
																																																																																			bool flag95 = _speech == "guardar isto";
																																																																																			if (flag95)
																																																																																			{
																																																																																				InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_G);
																																																																																				InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_S);
																																																																																				this.endSearchCmd();
																																																																																				this.frases();
																																																																																			}
																																																																																			else
																																																																																			{
																																																																																				bool flag96 = _speech == "não guardar" || _speech == "não Salvar";
																																																																																				if (flag96)
																																																																																				{
																																																																																					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.VK_N);
																																																																																					this.endSearchCmd();
																																																																																					this.frases();
																																																																																				}
																																																																																				else
																																																																																				{
																																																																																					bool flag97 = _speech == "salvar como" || _speech == "escolher como salvar";
																																																																																					if (flag97)
																																																																																					{
																																																																																						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_S);
																																																																																						this.frases();
																																																																																						this.endSearchCmd();
																																																																																					}
																																																																																					else
																																																																																					{
																																																																																						bool flag98 = _speech == "presionar enter" || _speech == "enter";
																																																																																						if (flag98)
																																																																																						{
																																																																																							bool flag99 = !this.LoadtecladoOn;
																																																																																							if (flag99)
																																																																																							{
																																																																																								InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
																																																																																								this.frases();
																																																																																								this.endSearchCmd();
																																																																																							}
																																																																																						}
																																																																																						else
																																																																																						{
																																																																																							bool flag100 = _speech == "Selecionar todo texto" || _speech == "Selecionar tudo";
																																																																																							if (flag100)
																																																																																							{
																																																																																								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);
																																																																																								this.frases();
																																																																																								this.endSearchCmd();
																																																																																							}
																																																																																							else
																																																																																							{
																																																																																								bool flag101 = _speech == "Excluir" || _speech == "Exclua esse arquivo";
																																																																																								if (flag101)
																																																																																								{
																																																																																									InputSimulator.SimulateKeyPress(VirtualKeyCode.DELETE);
																																																																																									this.SpeakOut(new string[]
																																																																																									{
																																																																																										"O arquivo selecionado foi escluido ",
																																																																																										"arquivo deletado "
																																																																																									});
																																																																																									this.endSearchCmd();
																																																																																								}
																																																																																								else
																																																																																								{
																																																																																									bool flag102 = _speech == "como está o tempo" || _speech == "qual é o tempo" || _speech == "como está o clima" || _speech == "qual é o clima" || _speech == "qual é o tempo para hoje" || _speech == "qual é o clima para hoje" || _speech == "me informe o tempo";
																																																																																									if (flag102)
																																																																																									{
																																																																																										try
																																																																																										{
																																																																																											Task.Run(delegate()
																																																																																											{
																																																																																												this.SpeakOut(new string[]
																																																																																												{
																																																																																													this.GetWeather("today")
																																																																																												});
																																																																																											});
																																																																																											this.endSearchCmd();
																																																																																										}
																																																																																										catch (Exception)
																																																																																										{
																																																																																											this.SpeakOut(new string[]
																																																																																											{
																																																																																												"por favor faça o teste com sua conexão antes de executar este comando"
																																																																																											});
																																																																																										}
																																																																																									}
																																																																																									else
																																																																																									{
																																																																																										bool flag103 = _speech == "qual é a temperatura" || _speech == "qual é a temperatura para hoje" || _speech == "a quê temperatura estamos";
																																																																																										if (flag103)
																																																																																										{
																																																																																											try
																																																																																											{
																																																																																												Task.Run(delegate()
																																																																																												{
																																																																																													this.SpeakOut(new string[]
																																																																																													{
																																																																																														this.GetWeather("temp")
																																																																																													});
																																																																																												});
																																																																																												this.endSearchCmd();
																																																																																											}
																																																																																											catch (Exception)
																																																																																											{
																																																																																												this.SpeakOut(new string[]
																																																																																												{
																																																																																													"por favor faça o teste com sua conexão antes de executar este comando"
																																																																																												});
																																																																																											}
																																																																																										}
																																																																																										else
																																																																																										{
																																																																																											bool flag104 = _speech == "qual é a temperatura para amanhã" || _speech == "que tempo vai fazer amanhã" || _speech == "qual é o clima para amanhã" || _speech == "me informe o tempo de amanhã";
																																																																																											if (flag104)
																																																																																											{
																																																																																												try
																																																																																												{
																																																																																													Task.Run(delegate()
																																																																																													{
																																																																																														this.SpeakOut(new string[]
																																																																																														{
																																																																																															this.GetWatherNextDays(7)
																																																																																														});
																																																																																													});
																																																																																													this.endSearchCmd();
																																																																																												}
																																																																																												catch (Exception)
																																																																																												{
																																																																																													this.SpeakOut(new string[]
																																																																																													{
																																																																																														"por favor faça o teste com sua conexão antes de executar este comando"
																																																																																													});
																																																																																												}
																																																																																											}
																																																																																											else
																																																																																											{
																																																																																												bool flag105 = _speech == "verificar e-mail" || _speech == "veja se tem e-mail";
																																																																																												if (flag105)
																																																																																												{
																																																																																													try
																																																																																													{
																																																																																														this.numeroCuentas();
																																																																																														this.QEvent = "Checkfornewemails";
																																																																																														bool flag106 = this.cuentas >= 2;
																																																																																														if (flag106)
																																																																																														{
																																																																																															this.SpeakOut(new string[]
																																																																																															{
																																																																																																"revisando seus " + this.cuentas + "e-mails "
																																																																																															});
																																																																																														}
																																																																																														else
																																																																																														{
																																																																																															this.SpeakOut(new string[]
																																																																																															{
																																																																																																"revisando os e-mails "
																																																																																															});
																																																																																														}
																																																																																														bool flag107 = Settings.Default.checkGmail;
																																																																																														if (flag107)
																																																																																														{
																																																																																															bool cntGmailAtomOK = Settings.Default.cntGmailAtomOK;
																																																																																															if (cntGmailAtomOK)
																																																																																															{
																																																																																																this.Timer3.Start();
																																																																																																this.EmailNum = 0;
																																																																																																this.CheckForEmails();
																																																																																															}
																																																																																															else
																																																																																															{
																																																																																																bool cntGmailOK = Settings.Default.cntGmailOK;
																																																																																																if (cntGmailOK)
																																																																																																{
																																																																																																	bool flag108 = this.checkGmail;
																																																																																																	if (flag108)
																																																																																																	{
																																																																																																		this.speechGmailTrue = true;
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		this.TimerImap.Stop();
																																																																																																		this.conexionGmail();
																																																																																																		this.speechGmailTrue = true;
																																																																																																	}
																																																																																																}
																																																																																															}
																																																																																														}
																																																																																														bool flag109 = Settings.Default.checkOutlook;
																																																																																														if (flag109)
																																																																																														{
																																																																																															bool flag110 = this.checkOutlook;
																																																																																															if (flag110)
																																																																																															{
																																																																																																this.speechOutloockTrue = true;
																																																																																															}
																																																																																															else
																																																																																															{
																																																																																																this.TimerImap.Stop();
																																																																																																this.conexionOutloock();
																																																																																																this.speechOutloockTrue = true;
																																																																																															}
																																																																																														}
																																																																																														bool flag111 = Settings.Default.checkYahoo;
																																																																																														if (flag111)
																																																																																														{
																																																																																															bool flag112 = this.checkYahoo;
																																																																																															if (flag112)
																																																																																															{
																																																																																																this.speechYahooTrue = true;
																																																																																															}
																																																																																															else
																																																																																															{
																																																																																																this.TimerImap.Stop();
																																																																																																this.conexionYahoo();
																																																																																																this.speechYahooTrue = true;
																																																																																															}
																																																																																														}
																																																																																														bool flag113 = Settings.Default.checkEmail;
																																																																																														if (flag113)
																																																																																														{
																																																																																															bool flag114 = this.checkEmail;
																																																																																															if (flag114)
																																																																																															{
																																																																																																this.speechEmailTrue = true;
																																																																																															}
																																																																																															else
																																																																																															{
																																																																																																this.TimerImap.Stop();
																																																																																																this.conexionEmail();
																																																																																																this.speechEmailTrue = true;
																																																																																															}
																																																																																														}
																																																																																														bool flag115 = !Settings.Default.checkEmail && !Settings.Default.checkGmail && !Settings.Default.checkOutlook && !Settings.Default.checkYahoo;
																																																																																														if (flag115)
																																																																																														{
																																																																																															this.SpeakOut(new string[]
																																																																																															{
																																																																																																Settings.Default.cfgUser + "nenhum email foi configurado "
																																																																																															});
																																																																																														}
																																																																																														this.endSearchCmd();
																																																																																													}
																																																																																													catch (Exception)
																																																																																													{
																																																																																														this.SpeakOut(new string[]
																																																																																														{
																																																																																															"por favor faça o teste com sua conexão antes de executar este comando "
																																																																																														});
																																																																																													}
																																																																																												}
																																																																																												else
																																																																																												{
																																																																																													bool flag116 = _speech == "ler e-mail" || _speech == "leia o e-mail";
																																																																																													if (flag116)
																																																																																													{
																																																																																														this.numeroCuentas();
																																																																																														try
																																																																																														{
																																																																																															bool flag117 = this.cuentas >= 2;
																																																																																															if (flag117)
																																																																																															{
																																																																																																this.SpeakOutStop(new string[]
																																																																																																{
																																																																																																	"Carregando " + this.cuentas + "e-mails "
																																																																																																});
																																																																																															}
																																																																																															else
																																																																																															{
																																																																																																this.SpeakOutStop(new string[]
																																																																																																{
																																																																																																	"só um segundo"
																																																																																																});
																																																																																															}
																																																																																															bool flag118 = Settings.Default.checkGmail;
																																																																																															if (flag118)
																																																																																															{
																																																																																																bool cntGmailAtomOK2 = Settings.Default.cntGmailAtomOK;
																																																																																																if (cntGmailAtomOK2)
																																																																																																{
																																																																																																	bool flag119 = !this.CargaDecorreo;
																																																																																																	if (flag119)
																																																																																																	{
																																																																																																		this.CheckForEmails();
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																	bool flag120 = this.cuentas <= 1;
																																																																																																	if (flag120)
																																																																																																	{
																																																																																																		this.SpeakOut(new string[]
																																																																																																		{
																																																																																																			this.MsgList[this.EmailNum]
																																																																																																		});
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		this.SpeakOut(new string[]
																																																																																																		{
																																																																																																			"mensagem do gmail. " + this.MsgList[this.EmailNum]
																																																																																																		});
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	bool cntGmailOK2 = Settings.Default.cntGmailOK;
																																																																																																	if (cntGmailOK2)
																																																																																																	{
																																																																																																		bool flag121 = this.MsgSpeechGmail.Count == 0;
																																																																																																		if (flag121)
																																																																																																		{
																																																																																																			this.TimerImap.Stop();
																																																																																																			this.conexionGmail();
																																																																																																			this.fromLecturaGmail = true;
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			bool flag122 = this.cuentas <= 1;
																																																																																																			if (flag122)
																																																																																																			{
																																																																																																				this.SpeakOut(new string[]
																																																																																																				{
																																																																																																					this.MsgSpeechGmail[this.gmailNum]
																																																																																																				});
																																																																																																				this.lecturaEnable = true;
																																																																																																			}
																																																																																																			else
																																																																																																			{
																																																																																																				this.SpeakOut(new string[]
																																																																																																				{
																																																																																																					"mensagem do gmail. " + this.MsgSpeechGmail[this.gmailNum]
																																																																																																				});
																																																																																																				this.lecturaEnable = true;
																																																																																																			}
																																																																																																		}
																																																																																																	}
																																																																																																}
																																																																																															}
																																																																																															bool flag123 = Settings.Default.checkOutlook;
																																																																																															if (flag123)
																																																																																															{
																																																																																																bool flag124 = this.MsgSpeechOutlook.Count == 0;
																																																																																																if (flag124)
																																																																																																{
																																																																																																	this.TimerImap.Stop();
																																																																																																	this.conexionOutloock();
																																																																																																	this.fromLecturaOutlook = true;
																																																																																																	this.lecturaEnable = true;
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	bool flag125 = this.cuentas <= 1;
																																																																																																	if (flag125)
																																																																																																	{
																																																																																																		this.SpeakOut(new string[]
																																																																																																		{
																																																																																																			this.MsgSpeechOutlook[this.outlookNum]
																																																																																																		});
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		this.SpeakOut(new string[]
																																																																																																		{
																																																																																																			"mensagem do outlook. " + this.MsgSpeechOutlook[this.outlookNum]
																																																																																																		});
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																}
																																																																																															}
																																																																																															bool flag126 = Settings.Default.checkYahoo;
																																																																																															if (flag126)
																																																																																															{
																																																																																																bool flag127 = this.MsgSpeechYahoo.Count == 0;
																																																																																																if (flag127)
																																																																																																{
																																																																																																	this.TimerImap.Stop();
																																																																																																	this.conexionYahoo();
																																																																																																	this.fromLecturaYahoo = true;
																																																																																																	this.lecturaEnable = true;
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	bool flag128 = this.cuentas <= 1;
																																																																																																	if (flag128)
																																																																																																	{
																																																																																																		this.SpeakOut(new string[]
																																																																																																		{
																																																																																																			this.MsgSpeechYahoo[this.yahooNum]
																																																																																																		});
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		this.SpeakOut(new string[]
																																																																																																		{
																																																																																																			"mensagem do yahoo. " + this.MsgSpeechYahoo[this.yahooNum]
																																																																																																		});
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																}
																																																																																															}
																																																																																															bool flag129 = Settings.Default.checkEmail;
																																																																																															if (flag129)
																																																																																															{
																																																																																																bool flag130 = this.MsgSpeechEmail.Count == 0;
																																																																																																if (flag130)
																																																																																																{
																																																																																																	this.TimerImap.Stop();
																																																																																																	this.conexionEmail();
																																																																																																	this.fromLecturaEmail = true;
																																																																																																	this.lecturaEnable = true;
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	bool flag131 = this.cuentas <= 1;
																																																																																																	if (flag131)
																																																																																																	{
																																																																																																		this.SpeakOut(new string[]
																																																																																																		{
																																																																																																			this.MsgSpeechEmail[this.emailNum]
																																																																																																		});
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		this.SpeakOut(new string[]
																																																																																																		{
																																																																																																			"mensagem pessoal. " + this.MsgSpeechEmail[this.emailNum]
																																																																																																		});
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																}
																																																																																															}
																																																																																															bool flag132 = !Settings.Default.checkEmail && !Settings.Default.checkGmail && !Settings.Default.checkOutlook && !Settings.Default.checkYahoo;
																																																																																															if (flag132)
																																																																																															{
																																																																																																this.SpeakOut(new string[]
																																																																																																{
																																																																																																	Settings.Default.cfgUser + "nenhum email foi configurado "
																																																																																																});
																																																																																															}
																																																																																														}
																																																																																														catch
																																																																																														{
																																																																																															this.SpeakOut(new string[]
																																																																																															{
																																																																																																"não há e-mails para ler "
																																																																																															});
																																																																																														}
																																																																																														this.lblSpeech2.Content = "";
																																																																																														this.lblSpeech2.Content = "Espere até que todos os e-mails são lidos!";
																																																																																														this.endSearchCmd();
																																																																																													}
																																																																																													else
																																																																																													{
																																																																																														bool flag133 = _speech == "ler o próximo e-mail" || _speech == "leia o próximo e-mail" || _speech == "próximo e-mail";
																																																																																														if (flag133)
																																																																																														{
																																																																																															try
																																																																																															{
																																																																																																bool flag134 = Settings.Default.checkGmail;
																																																																																																if (flag134)
																																																																																																{
																																																																																																	bool cntGmailAtomOK3 = Settings.Default.cntGmailAtomOK;
																																																																																																	if (cntGmailAtomOK3)
																																																																																																	{
																																																																																																		int num = this.EmailNum;
																																																																																																		this.EmailNum = num + 1;
																																																																																																		bool flag135 = !this.CargaDecorreo;
																																																																																																		if (flag135)
																																																																																																		{
																																																																																																			this.CheckForEmails();
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																		bool flag136 = this.cuentas <= 1;
																																																																																																		if (flag136)
																																																																																																		{
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				this.MsgList[this.EmailNum]
																																																																																																			});
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				"mensagem do gmail. " + this.MsgList[this.EmailNum]
																																																																																																			});
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		bool cntGmailOK3 = Settings.Default.cntGmailOK;
																																																																																																		if (cntGmailOK3)
																																																																																																		{
																																																																																																			int num = this.gmailNum;
																																																																																																			this.gmailNum = num + 1;
																																																																																																			bool flag137 = this.MsgSpeechGmail.Count == 0;
																																																																																																			if (flag137)
																																																																																																			{
																																																																																																				this.TimerImap.Stop();
																																																																																																				this.conexionGmail();
																																																																																																				this.fromLecturaGmail = true;
																																																																																																				this.lecturaEnable = true;
																																																																																																			}
																																																																																																			else
																																																																																																			{
																																																																																																				bool flag138 = this.cuentas <= 1;
																																																																																																				if (flag138)
																																																																																																				{
																																																																																																					this.SpeakOut(new string[]
																																																																																																					{
																																																																																																						this.MsgSpeechGmail[this.gmailNum]
																																																																																																					});
																																																																																																					this.lecturaEnable = true;
																																																																																																				}
																																																																																																				else
																																																																																																				{
																																																																																																					this.SpeakOut(new string[]
																																																																																																					{
																																																																																																						"mensagem do gmail. " + this.MsgSpeechGmail[this.gmailNum]
																																																																																																					});
																																																																																																					this.lecturaEnable = true;
																																																																																																				}
																																																																																																			}
																																																																																																		}
																																																																																																	}
																																																																																																}
																																																																																															}
																																																																																															catch
																																																																																															{
																																																																																																this.EmailNum = 0;
																																																																																																this.gmailNum = 0;
																																																																																																bool flag139 = this.cuentas <= 1;
																																																																																																if (flag139)
																																																																																																{
																																																																																																	this.SpeakOut(new string[]
																																																																																																	{
																																																																																																		"não há mais novos e-mails "
																																																																																																	});
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	this.SpeakOut(new string[]
																																																																																																	{
																																																																																																		"não há mais novos e-mails no Gmail "
																																																																																																	});
																																																																																																}
																																																																																															}
																																																																																															try
																																																																																															{
																																																																																																bool flag140 = Settings.Default.checkOutlook;
																																																																																																if (flag140)
																																																																																																{
																																																																																																	int num = this.outlookNum;
																																																																																																	this.outlookNum = num + 1;
																																																																																																	bool flag141 = this.MsgSpeechOutlook.Count == 0;
																																																																																																	if (flag141)
																																																																																																	{
																																																																																																		this.TimerImap.Stop();
																																																																																																		this.conexionOutloock();
																																																																																																		this.fromLecturaOutlook = true;
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		bool flag142 = this.cuentas <= 1;
																																																																																																		if (flag142)
																																																																																																		{
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				this.MsgSpeechOutlook[this.outlookNum]
																																																																																																			});
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				"mensagem do outlook. " + this.MsgSpeechOutlook[this.outlookNum]
																																																																																																			});
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																	}
																																																																																																}
																																																																																															}
																																																																																															catch
																																																																																															{
																																																																																																this.outlookNum = 0;
																																																																																																bool flag143 = this.cuentas <= 1;
																																																																																																if (flag143)
																																																																																																{
																																																																																																	this.SpeakOut(new string[]
																																																																																																	{
																																																																																																		"não há mais novos e-mails "
																																																																																																	});
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	this.SpeakOut(new string[]
																																																																																																	{
																																																																																																		"não há mais novos e-mails no Outlook"
																																																																																																	});
																																																																																																}
																																																																																															}
																																																																																															try
																																																																																															{
																																																																																																bool flag144 = Settings.Default.checkYahoo;
																																																																																																if (flag144)
																																																																																																{
																																																																																																	int num = this.yahooNum;
																																																																																																	this.yahooNum = num + 1;
																																																																																																	bool flag145 = this.MsgSpeechYahoo.Count == 0;
																																																																																																	if (flag145)
																																																																																																	{
																																																																																																		this.TimerImap.Stop();
																																																																																																		this.conexionYahoo();
																																																																																																		this.fromLecturaYahoo = true;
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		bool flag146 = this.cuentas <= 1;
																																																																																																		if (flag146)
																																																																																																		{
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				this.MsgSpeechYahoo[this.yahooNum]
																																																																																																			});
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				"mensagem do yahoo. " + this.MsgSpeechYahoo[this.yahooNum]
																																																																																																			});
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																	}
																																																																																																}
																																																																																															}
																																																																																															catch
																																																																																															{
																																																																																																this.yahooNum = 0;
																																																																																																bool flag147 = this.cuentas <= 1;
																																																																																																if (flag147)
																																																																																																{
																																																																																																	this.SpeakOut(new string[]
																																																																																																	{
																																																																																																		"não há mais novos e-mails "
																																																																																																	});
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	this.SpeakOut(new string[]
																																																																																																	{
																																																																																																		"não há mais novos e-mails no Yahoo "
																																																																																																	});
																																																																																																}
																																																																																															}
																																																																																															try
																																																																																															{
																																																																																																bool flag148 = Settings.Default.checkEmail;
																																																																																																if (flag148)
																																																																																																{
																																																																																																	int num = this.emailNum;
																																																																																																	this.emailNum = num + 1;
																																																																																																	bool flag149 = this.MsgSpeechEmail.Count == 0;
																																																																																																	if (flag149)
																																																																																																	{
																																																																																																		this.TimerImap.Stop();
																																																																																																		this.conexionEmail();
																																																																																																		this.fromLecturaEmail = true;
																																																																																																		this.lecturaEnable = true;
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		bool flag150 = this.cuentas <= 1;
																																																																																																		if (flag150)
																																																																																																		{
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				this.MsgSpeechEmail[this.emailNum]
																																																																																																			});
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				"mensagem pessoal. " + this.MsgSpeechEmail[this.emailNum]
																																																																																																			});
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																	}
																																																																																																}
																																																																																															}
																																																																																															catch
																																																																																															{
																																																																																																this.emailNum = 0;
																																																																																																bool flag151 = this.cuentas <= 1;
																																																																																																if (flag151)
																																																																																																{
																																																																																																	this.SpeakOut(new string[]
																																																																																																	{
																																																																																																		"não há mais novos e-mails "
																																																																																																	});
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	this.SpeakOut(new string[]
																																																																																																	{
																																																																																																		"não há mais novos e-mails pessoal "
																																																																																																	});
																																																																																																}
																																																																																															}
																																																																																															bool flag152 = !Settings.Default.checkEmail && !Settings.Default.checkGmail && !Settings.Default.checkOutlook && !Settings.Default.checkYahoo;
																																																																																															if (flag152)
																																																																																															{
																																																																																																this.SpeakOut(new string[]
																																																																																																{
																																																																																																	Settings.Default.cfgUser + "nenhum email foi configurado "
																																																																																																});
																																																																																															}
																																																																																															this.endSearchCmd();
																																																																																														}
																																																																																														else
																																																																																														{
																																																																																															bool flag153 = _speech == "leia o e-mail anterior" || _speech == "ler o e-mail anterior" || _speech == "e-mail anterior" || _speech == "ler e-mail anterior";
																																																																																															if (flag153)
																																																																																															{
																																																																																																try
																																																																																																{
																																																																																																	bool flag154 = Settings.Default.checkGmail;
																																																																																																	if (flag154)
																																																																																																	{
																																																																																																		bool cntGmailAtomOK4 = Settings.Default.cntGmailAtomOK;
																																																																																																		if (cntGmailAtomOK4)
																																																																																																		{
																																																																																																			int num = this.EmailNum;
																																																																																																			this.EmailNum = num - 1;
																																																																																																			bool flag155 = !this.CargaDecorreo;
																																																																																																			if (flag155)
																																																																																																			{
																																																																																																				this.CheckForEmails();
																																																																																																			}
																																																																																																			this.SpeakOutStop(new string[]
																																																																																																			{
																																																																																																				this.MsgList[this.EmailNum]
																																																																																																			});
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			bool cntGmailOK4 = Settings.Default.cntGmailOK;
																																																																																																			if (cntGmailOK4)
																																																																																																			{
																																																																																																				int num = this.gmailNum;
																																																																																																				this.gmailNum = num - 1;
																																																																																																				bool flag156 = this.MsgSpeechGmail.Count == 0;
																																																																																																				if (flag156)
																																																																																																				{
																																																																																																					this.TimerImap.Stop();
																																																																																																					this.conexionGmail();
																																																																																																					this.fromLecturaGmail = true;
																																																																																																					this.lecturaEnable = true;
																																																																																																				}
																																																																																																				else
																																																																																																				{
																																																																																																					bool flag157 = this.cuentas <= 1;
																																																																																																					if (flag157)
																																																																																																					{
																																																																																																						this.SpeakOut(new string[]
																																																																																																						{
																																																																																																							this.MsgSpeechGmail[this.gmailNum]
																																																																																																						});
																																																																																																						this.lecturaEnable = true;
																																																																																																					}
																																																																																																					else
																																																																																																					{
																																																																																																						this.SpeakOut(new string[]
																																																																																																						{
																																																																																																							"mensagem do gmail. " + this.MsgSpeechGmail[this.gmailNum]
																																																																																																						});
																																																																																																						this.lecturaEnable = true;
																																																																																																					}
																																																																																																				}
																																																																																																			}
																																																																																																		}
																																																																																																	}
																																																																																																	bool flag158 = Settings.Default.checkOutlook;
																																																																																																	if (flag158)
																																																																																																	{
																																																																																																		int num = this.outlookNum;
																																																																																																		this.outlookNum = num - 1;
																																																																																																		bool flag159 = this.MsgSpeechOutlook.Count == 0;
																																																																																																		if (flag159)
																																																																																																		{
																																																																																																			this.TimerImap.Stop();
																																																																																																			this.conexionOutloock();
																																																																																																			this.fromLecturaOutlook = true;
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			bool flag160 = this.cuentas <= 1;
																																																																																																			if (flag160)
																																																																																																			{
																																																																																																				this.SpeakOut(new string[]
																																																																																																				{
																																																																																																					this.MsgSpeechOutlook[this.outlookNum]
																																																																																																				});
																																																																																																				this.lecturaEnable = true;
																																																																																																			}
																																																																																																			else
																																																																																																			{
																																																																																																				this.SpeakOut(new string[]
																																																																																																				{
																																																																																																					"mensagem do outlook. " + this.MsgSpeechOutlook[this.outlookNum]
																																																																																																				});
																																																																																																				this.lecturaEnable = true;
																																																																																																			}
																																																																																																		}
																																																																																																	}
																																																																																																	bool flag161 = Settings.Default.checkYahoo;
																																																																																																	if (flag161)
																																																																																																	{
																																																																																																		int num = this.yahooNum;
																																																																																																		this.yahooNum = num - 1;
																																																																																																		bool flag162 = this.MsgSpeechYahoo.Count == 0;
																																																																																																		if (flag162)
																																																																																																		{
																																																																																																			this.TimerImap.Stop();
																																																																																																			this.conexionYahoo();
																																																																																																			this.fromLecturaYahoo = true;
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			bool flag163 = this.cuentas <= 1;
																																																																																																			if (flag163)
																																																																																																			{
																																																																																																				this.SpeakOut(new string[]
																																																																																																				{
																																																																																																					this.MsgSpeechYahoo[this.yahooNum]
																																																																																																				});
																																																																																																				this.lecturaEnable = true;
																																																																																																			}
																																																																																																			else
																																																																																																			{
																																																																																																				this.SpeakOut(new string[]
																																																																																																				{
																																																																																																					"mensagem do yahoo. " + this.MsgSpeechYahoo[this.yahooNum]
																																																																																																				});
																																																																																																				this.lecturaEnable = true;
																																																																																																			}
																																																																																																		}
																																																																																																	}
																																																																																																	bool flag164 = Settings.Default.checkEmail;
																																																																																																	if (flag164)
																																																																																																	{
																																																																																																		int num = this.emailNum;
																																																																																																		this.emailNum = num - 1;
																																																																																																		bool flag165 = this.MsgSpeechEmail.Count == 0;
																																																																																																		if (flag165)
																																																																																																		{
																																																																																																			this.TimerImap.Stop();
																																																																																																			this.conexionEmail();
																																																																																																			this.fromLecturaEmail = true;
																																																																																																			this.lecturaEnable = true;
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			bool flag166 = this.cuentas <= 1;
																																																																																																			if (flag166)
																																																																																																			{
																																																																																																				this.SpeakOut(new string[]
																																																																																																				{
																																																																																																					this.MsgSpeechEmail[this.emailNum]
																																																																																																				});
																																																																																																				this.lecturaEnable = true;
																																																																																																			}
																																																																																																			else
																																																																																																			{
																																																																																																				this.SpeakOut(new string[]
																																																																																																				{
																																																																																																					"mensagem pessoal. " + this.MsgSpeechEmail[this.emailNum]
																																																																																																				});
																																																																																																				this.lecturaEnable = true;
																																																																																																			}
																																																																																																		}
																																																																																																	}
																																																																																																	bool flag167 = !Settings.Default.checkEmail && !Settings.Default.checkGmail && !Settings.Default.checkOutlook && !Settings.Default.checkYahoo;
																																																																																																	if (flag167)
																																																																																																	{
																																																																																																		this.SpeakOut(new string[]
																																																																																																		{
																																																																																																			Settings.Default.cfgUser + "nenhum email foi configurado "
																																																																																																		});
																																																																																																	}
																																																																																																}
																																																																																																catch
																																																																																																{
																																																																																																	this.EmailNum = 0;
																																																																																																	this.gmailNum = 0;
																																																																																																	this.outlookNum = 0;
																																																																																																	this.yahooNum = 0;
																																																																																																	this.emailNum = 0;
																																																																																																	this.SpeakOut(new string[]
																																																																																																	{
																																																																																																		"você não tem e-mails anteriores "
																																																																																																	});
																																																																																																}
																																																																																																this.endSearchCmd();
																																																																																															}
																																																																																															else
																																																																																															{
																																																																																																bool flag168 = _speech == "acessar contas de e-mail" || _speech == "abrir e-mail" || _speech == "ver e-mail";
																																																																																																if (flag168)
																																																																																																{
																																																																																																	this.numeroCuentas();
																																																																																																	try
																																																																																																	{
																																																																																																		bool flag169 = this.cuentas >= 2;
																																																																																																		if (flag169)
																																																																																																		{
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				"abrindo " + this.cuentas + " e-mails "
																																																																																																			});
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			this.frases();
																																																																																																		}
																																																																																																		bool flag170 = Settings.Default.checkGmail;
																																																																																																		if (flag170)
																																																																																																		{
																																																																																																			bool cntGmailAtomOK5 = Settings.Default.cntGmailAtomOK;
																																																																																																			if (cntGmailAtomOK5)
																																																																																																			{
																																																																																																				Process.Start(this.MsgLink[this.EmailNum]);
																																																																																																			}
																																																																																																			else
																																																																																																			{
																																																																																																				bool cntGmailOK5 = Settings.Default.cntGmailOK;
																																																																																																				if (cntGmailOK5)
																																																																																																				{
																																																																																																					Process.Start("https://mail.google.com/");
																																																																																																				}
																																																																																																			}
																																																																																																		}
																																																																																																		bool flag171 = Settings.Default.checkOutlook;
																																																																																																		if (flag171)
																																																																																																		{
																																																																																																			Process.Start("https://hotmail.com");
																																																																																																		}
																																																																																																		bool flag172 = Settings.Default.checkYahoo;
																																																																																																		if (flag172)
																																																																																																		{
																																																																																																			Process.Start("https://mail.yahoo.com/?.intl=e1");
																																																																																																		}
																																																																																																		bool flag173 = Settings.Default.checkEmail;
																																																																																																		if (flag173)
																																																																																																		{
																																																																																																			Process.Start("https://" + Settings.Default.servidorEmail.Split(new char[]
																																																																																																			{
																																																																																																				'@'
																																																																																																			})[1] + "/webmail");
																																																																																																		}
																																																																																																		bool flag174 = !Settings.Default.checkEmail && !Settings.Default.checkGmail && !Settings.Default.checkOutlook && !Settings.Default.checkYahoo;
																																																																																																		if (flag174)
																																																																																																		{
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				Settings.Default.cfgUser + " nenhum email foi configurado "
																																																																																																			});
																																																																																																		}
																																																																																																	}
																																																																																																	catch
																																																																																																	{
																																																																																																		this.SpeakOut(new string[]
																																																																																																		{
																																																																																																			"você não tem e-mails para abrir "
																																																																																																		});
																																																																																																	}
																																																																																																	this.endSearchCmd();
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	bool flag175 = _speech == "ler mensagem" || _speech == "leia as mensagens";
																																																																																																	if (flag175)
																																																																																																	{
																																																																																																		try
																																																																																																		{
																																																																																																			this.FaceMsgNum = this.MsgFace.Count<string>();
																																																																																																			int num = this.FaceMsgNum;
																																																																																																			this.FaceMsgNum = num - 1;
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				this.MsgFace[this.FaceMsgNum]
																																																																																																			});
																																																																																																		}
																																																																																																		catch
																																																																																																		{
																																																																																																			this.SpeakOut(new string[]
																																																																																																			{
																																																																																																				"não tem novas mensagens "
																																																																																																			});
																																																																																																		}
																																																																																																		this.endSearchCmd();
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		bool flag176 = _speech == "leia as mensagens anterior" || _speech == "ler mensagem anterior";
																																																																																																		if (flag176)
																																																																																																		{
																																																																																																			try
																																																																																																			{
																																																																																																				int num = this.FaceMsgNum;
																																																																																																				this.FaceMsgNum = num - 1;
																																																																																																				this.SpeakOut(new string[]
																																																																																																				{
																																																																																																					this.MsgFace[this.FaceMsgNum]
																																																																																																				});
																																																																																																			}
																																																																																																			catch
																																																																																																			{
																																																																																																				int num = this.FaceMsgNum;
																																																																																																				this.FaceMsgNum = num + 1;
																																																																																																				this.SpeakOut(new string[]
																																																																																																				{
																																																																																																					"não tem mensagens "
																																																																																																				});
																																																																																																			}
																																																																																																			this.endSearchCmd();
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			bool flag177 = _speech == "leia a próxima mensagem" || _speech == "ler a próxima mensagem" || _speech == "leia a próxima mensagem do facebook";
																																																																																																			if (flag177)
																																																																																																			{
																																																																																																				try
																																																																																																				{
																																																																																																					int num = this.FaceMsgNum;
																																																																																																					this.FaceMsgNum = num + 1;
																																																																																																					this.SpeakOut(new string[]
																																																																																																					{
																																																																																																						this.MsgFace[this.FaceMsgNum]
																																																																																																					});
																																																																																																				}
																																																																																																				catch
																																																																																																				{
																																																																																																					int num = this.FaceMsgNum;
																																																																																																					this.FaceMsgNum = num - 1;
																																																																																																					this.SpeakOut(new string[]
																																																																																																					{
																																																																																																						"não tem novas mensagens "
																																																																																																					});
																																																																																																				}
																																																																																																				this.endSearchCmd();
																																																																																																			}
																																																																																																			else
																																																																																																			{
																																																																																																				bool flag178 = _speech == "abrir uma nova janela" || _speech == "abrir outra janela" || _speech == "nova janela";
																																																																																																				if (flag178)
																																																																																																				{
																																																																																																					bool flag179 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																					if (flag179)
																																																																																																					{
																																																																																																						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_N);
																																																																																																						this.SpeakOut(new string[]
																																																																																																						{
																																																																																																							"pagína do navegador pronta "
																																																																																																						});
																																																																																																					}
																																																																																																					else
																																																																																																					{
																																																																																																						this.frasesresp();
																																																																																																					}
																																																																																																					this.endSearchCmd();
																																																																																																				}
																																																																																																				else
																																																																																																				{
																																																																																																					bool flag180 = _speech == "abrir uma nova janela" || _speech == "abrir outra janela" || _speech == "nova janela";
																																																																																																					if (flag180)
																																																																																																					{
																																																																																																						bool flag181 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																						if (flag181)
																																																																																																						{
																																																																																																							InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_N);
																																																																																																							this.SpeakOut(new string[]
																																																																																																							{
																																																																																																								"pagína do navegador pronta "
																																																																																																							});
																																																																																																						}
																																																																																																						else
																																																																																																						{
																																																																																																							this.frasesresp();
																																																																																																						}
																																																																																																						this.endSearchCmd();
																																																																																																					}
																																																																																																					else
																																																																																																					{
																																																																																																						bool flag182 = _speech == "abrir uma nova guia" || _speech == "abrir nova guia" || _speech == "nova guia";
																																																																																																						if (flag182)
																																																																																																						{
																																																																																																							bool flag183 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																							if (flag183)
																																																																																																							{
																																																																																																								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
																																																																																																								this.SpeakOut(new string[]
																																																																																																								{
																																																																																																									"nova instância aberta "
																																																																																																								});
																																																																																																							}
																																																																																																							else
																																																																																																							{
																																																																																																								this.frasesresp();
																																																																																																							}
																																																																																																							this.endSearchCmd();
																																																																																																						}
																																																																																																						else
																																																																																																						{
																																																																																																							bool flag184 = _speech == "abrir guia anterior" || _speech == "abrir guia fechada" || _speech == "guia anterior";
																																																																																																							if (flag184)
																																																																																																							{
																																																																																																								bool flag185 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																								if (flag185)
																																																																																																								{
																																																																																																									InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
																																																																																																									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LSHIFT, VirtualKeyCode.VK_T);
																																																																																																									InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
																																																																																																									this.SpeakOut(new string[]
																																																																																																									{
																																																																																																										"reabrindo ultima instancia encerrada "
																																																																																																									});
																																																																																																								}
																																																																																																								else
																																																																																																								{
																																																																																																									this.frasesresp();
																																																																																																								}
																																																																																																								this.endSearchCmd();
																																																																																																							}
																																																																																																							else
																																																																																																							{
																																																																																																								bool flag186 = _speech == "atualizar" || _speech == "Atualize essa página" || _speech == "Atualizar a página";
																																																																																																								if (flag186)
																																																																																																								{
																																																																																																									InputSimulator.SimulateKeyDown(VirtualKeyCode.F5);
																																																																																																									this.frases();
																																																																																																									this.endSearchCmd();
																																																																																																								}
																																																																																																								else
																																																																																																								{
																																																																																																									bool flag187 = _speech == "Percorrer elemento" || _speech == "Percorrer elemento da tela";
																																																																																																									if (flag187)
																																																																																																									{
																																																																																																										bool flag188 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																										if (flag188)
																																																																																																										{
																																																																																																											InputSimulator.SimulateKeyDown(VirtualKeyCode.F6);
																																																																																																											this.frases();
																																																																																																										}
																																																																																																										else
																																																																																																										{
																																																																																																											this.frasesresp();
																																																																																																										}
																																																																																																										this.endSearchCmd();
																																																																																																									}
																																																																																																									else
																																																																																																									{
																																																																																																										bool flag189 = _speech == "percorrer a guia" || _speech == "percorrer guia";
																																																																																																										if (flag189)
																																																																																																										{
																																																																																																											bool flag190 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																											if (flag190)
																																																																																																											{
																																																																																																												InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
																																																																																																												this.SpeakOut(new string[]
																																																																																																												{
																																																																																																													"selecionando um item "
																																																																																																												});
																																																																																																											}
																																																																																																											else
																																																																																																											{
																																																																																																												this.frasesresp();
																																																																																																											}
																																																																																																											this.endSearchCmd();
																																																																																																										}
																																																																																																										else
																																																																																																										{
																																																																																																											bool flag191 = _speech == "retroceder a seleção" || _speech == "retroceder selecionado";
																																																																																																											if (flag191)
																																																																																																											{
																																																																																																												InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.TAB);
																																																																																																												this.SpeakOut(new string[]
																																																																																																												{
																																																																																																													"Retornando ao anterior. "
																																																																																																												});
																																																																																																												this.endSearchCmd();
																																																																																																											}
																																																																																																											else
																																																																																																											{
																																																																																																												bool flag192 = _speech == "Abrir página inicial" || _speech == "página inicial";
																																																																																																												if (flag192)
																																																																																																												{
																																																																																																													bool flag193 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																													if (flag193)
																																																																																																													{
																																																																																																														InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.HOME);
																																																																																																														this.SpeakOut(new string[]
																																																																																																														{
																																																																																																															"Sim ",
																																																																																																															"retornando a pagina inicial ",
																																																																																																															"Agora mesmo "
																																																																																																														});
																																																																																																													}
																																																																																																													else
																																																																																																													{
																																																																																																														this.frasesresp();
																																																																																																													}
																																																																																																													this.endSearchCmd();
																																																																																																												}
																																																																																																												else
																																																																																																												{
																																																																																																													bool flag194 = _speech == "Abrir arquivo no Navegador" || _speech == "Selecionar arquivo para o navegador" || _speech == "arquivo no navegador";
																																																																																																													if (flag194)
																																																																																																													{
																																																																																																														bool flag195 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																														if (flag195)
																																																																																																														{
																																																																																																															InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_O);
																																																																																																															this.SpeakOut(new string[]
																																																																																																															{
																																																																																																																"Sim ",
																																																																																																																"selecione o arquivo para abrir no navegador ",
																																																																																																																"Selecione o arquivo " + Settings.Default.cfgUser
																																																																																																															});
																																																																																																														}
																																																																																																														else
																																																																																																														{
																																																																																																															this.frasesresp();
																																																																																																														}
																																																																																																														this.endSearchCmd();
																																																																																																													}
																																																																																																													else
																																																																																																													{
																																																																																																														bool flag196 = _speech == "abrir guia anterior da página atual" || _speech == "guia anterior da página";
																																																																																																														if (flag196)
																																																																																																														{
																																																																																																															bool flag197 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																															if (flag197)
																																																																																																															{
																																																																																																																InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.LEFT);
																																																																																																																this.SpeakOut(new string[]
																																																																																																																{
																																																																																																																	"Sim ",
																																																																																																																	"página anterior do histórico de navegação aberto ",
																																																																																																																	"página anterior " + Settings.Default.cfgUser
																																																																																																																});
																																																																																																															}
																																																																																																															else
																																																																																																															{
																																																																																																																this.frasesresp();
																																																																																																															}
																																																																																																															this.endSearchCmd();
																																																																																																														}
																																																																																																														else
																																																																																																														{
																																																																																																															bool flag198 = _speech == "abrir próxima guia da página atual" || _speech == "próxima guia da página";
																																																																																																															if (flag198)
																																																																																																															{
																																																																																																																bool flag199 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																if (flag199)
																																																																																																																{
																																																																																																																	InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.RIGHT);
																																																																																																																	this.SpeakOut(new string[]
																																																																																																																	{
																																																																																																																		"Sim ",
																																																																																																																		"próxima página do histórico de navegação aberto ",
																																																																																																																		"próxima página " + Settings.Default.cfgUser
																																																																																																																	});
																																																																																																																}
																																																																																																																else
																																																																																																																{
																																																																																																																	this.frasesresp();
																																																																																																																}
																																																																																																																this.endSearchCmd();
																																																																																																															}
																																																																																																															else
																																																																																																															{
																																																																																																																bool flag200 = _speech == "Abrir Menu do Navegador" || _speech == "menu do Navegador";
																																																																																																																if (flag200)
																																																																																																																{
																																																																																																																	bool flag201 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																	if (flag201)
																																																																																																																	{
																																																																																																																		InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.VK_F);
																																																																																																																		this.SpeakOut(new string[]
																																																																																																																		{
																																																																																																																			"Sim ",
																																																																																																																			"Selecione sua preferência ",
																																																																																																																			"agora mesmo " + Settings.Default.cfgUser
																																																																																																																		});
																																																																																																																	}
																																																																																																																	else
																																																																																																																	{
																																																																																																																		this.frasesresp();
																																																																																																																	}
																																																																																																																	this.endSearchCmd();
																																																																																																																}
																																																																																																																else
																																																																																																																{
																																																																																																																	bool flag202 = _speech == "dessa a página" || _speech == "desser a página";
																																																																																																																	if (flag202)
																																																																																																																	{
																																																																																																																		bool flag203 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																		if (flag203)
																																																																																																																		{
																																																																																																																			int num;
																																																																																																																			for (int num6 = 0; num6 < 11; num6 = num + 1)
																																																																																																																			{
																																																																																																																				InputSimulator.SimulateKeyPress(VirtualKeyCode.DOWN);
																																																																																																																				Thread.Sleep(50);
																																																																																																																				num = num6;
																																																																																																																			}
																																																																																																																			this.frases();
																																																																																																																		}
																																																																																																																		else
																																																																																																																		{
																																																																																																																			this.frasesresp();
																																																																																																																		}
																																																																																																																		this.endSearchCmd();
																																																																																																																	}
																																																																																																																	else
																																																																																																																	{
																																																																																																																		bool flag204 = _speech == "suba a página" || _speech == "subir essa página";
																																																																																																																		if (flag204)
																																																																																																																		{
																																																																																																																			bool flag205 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																			if (flag205)
																																																																																																																			{
																																																																																																																				int num;
																																																																																																																				for (int num7 = 0; num7 < 11; num7 = num + 1)
																																																																																																																				{
																																																																																																																					InputSimulator.SimulateKeyPress(VirtualKeyCode.UP);
																																																																																																																					Thread.Sleep(50);
																																																																																																																					num = num7;
																																																																																																																				}
																																																																																																																				this.frases();
																																																																																																																			}
																																																																																																																			else
																																																																																																																			{
																																																																																																																				this.frasesresp();
																																																																																																																			}
																																																																																																																			this.endSearchCmd();
																																																																																																																		}
																																																																																																																		else
																																																																																																																		{
																																																																																																																			bool flag206 = _speech == "fechar essa guia" || _speech == "feche essa guia" || _speech == "fechar a guia";
																																																																																																																			if (flag206)
																																																																																																																			{
																																																																																																																				bool flag207 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																				if (flag207)
																																																																																																																				{
																																																																																																																					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_W);
																																																																																																																					this.frases();
																																																																																																																				}
																																																																																																																				else
																																																																																																																				{
																																																																																																																					this.frasesresp();
																																																																																																																				}
																																																																																																																				this.endSearchCmd();
																																																																																																																			}
																																																																																																																			else
																																																																																																																			{
																																																																																																																				bool flag208 = _speech == "guia anterior" || _speech == "Abri guia anterior";
																																																																																																																				if (flag208)
																																																																																																																				{
																																																																																																																					bool flag209 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																					if (flag209)
																																																																																																																					{
																																																																																																																						InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
																																																																																																																						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LSHIFT, VirtualKeyCode.TAB);
																																																																																																																						InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
																																																																																																																						this.SpeakOut(new string[]
																																																																																																																						{
																																																																																																																							"acessando instancia anterior "
																																																																																																																						});
																																																																																																																					}
																																																																																																																					else
																																																																																																																					{
																																																																																																																						this.frasesresp();
																																																																																																																					}
																																																																																																																					this.endSearchCmd();
																																																																																																																				}
																																																																																																																				else
																																																																																																																				{
																																																																																																																					bool flag210 = _speech == "próxima guia" || _speech == "trocar guia" || _speech == "trocar de guia";
																																																																																																																					if (flag210)
																																																																																																																					{
																																																																																																																						bool flag211 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																						if (flag211)
																																																																																																																						{
																																																																																																																							InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.TAB);
																																																																																																																							this.SpeakOut(new string[]
																																																																																																																							{
																																																																																																																								"instancia seguinte ",
																																																																																																																								"sim ",
																																																																																																																								"trocando de guia "
																																																																																																																							});
																																																																																																																						}
																																																																																																																						else
																																																																																																																						{
																																																																																																																							this.frasesresp();
																																																																																																																						}
																																																																																																																						this.endSearchCmd();
																																																																																																																					}
																																																																																																																					else
																																																																																																																					{
																																																																																																																						bool flag212 = _speech == "página de Download" || _speech == "abrir página de Download";
																																																																																																																						if (flag212)
																																																																																																																						{
																																																																																																																							bool flag213 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																							if (flag213)
																																																																																																																							{
																																																																																																																								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_J);
																																																																																																																								this.SpeakOut(new string[]
																																																																																																																								{
																																																																																																																									"Abrindo conteúdos baixados "
																																																																																																																								});
																																																																																																																							}
																																																																																																																							else
																																																																																																																							{
																																																																																																																								this.frasesresp();
																																																																																																																							}
																																																																																																																							this.endSearchCmd();
																																																																																																																						}
																																																																																																																						else
																																																																																																																						{
																																																																																																																							bool flag214 = _speech == "imprimir página" || _speech == "imprimir a página";
																																																																																																																							if (flag214)
																																																																																																																							{
																																																																																																																								bool flag215 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																								if (flag215)
																																																																																																																								{
																																																																																																																									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_P);
																																																																																																																									this.SpeakOut(new string[]
																																																																																																																									{
																																																																																																																										"configurando sua impresora "
																																																																																																																									});
																																																																																																																								}
																																																																																																																								else
																																																																																																																								{
																																																																																																																									this.frasesresp();
																																																																																																																								}
																																																																																																																								this.endSearchCmd();
																																																																																																																							}
																																																																																																																							else
																																																																																																																							{
																																																																																																																								bool flag216 = _speech == "modo anônimo" || _speech == "abrir modo incognito" || _speech == "abrir página anônima";
																																																																																																																								if (flag216)
																																																																																																																								{
																																																																																																																									bool flag217 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																									if (flag217)
																																																																																																																									{
																																																																																																																										InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
																																																																																																																										InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LSHIFT, VirtualKeyCode.VK_N);
																																																																																																																										InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
																																																																																																																										this.SpeakOut(new string[]
																																																																																																																										{
																																																																																																																											"nova janela do modo de navegação anônima iniciada ",
																																																																																																																											"iniciando modo anonimo"
																																																																																																																										});
																																																																																																																									}
																																																																																																																									else
																																																																																																																									{
																																																																																																																										this.frasesresp();
																																																																																																																									}
																																																																																																																									this.endSearchCmd();
																																																																																																																								}
																																																																																																																								else
																																																																																																																								{
																																																																																																																									bool flag218 = _speech == "abrir tocador" || _speech == "tocador de midia";
																																																																																																																									if (flag218)
																																																																																																																									{
																																																																																																																										bool music = Settings.Default.Music;
																																																																																																																										if (music)
																																																																																																																										{
																																																																																																																											this.SpeakOut(new string[]
																																																																																																																											{
																																																																																																																												"Preparando reprodutor de midias"
																																																																																																																											});
																																																																																																																											bool flag219 = this.mpl == null;
																																																																																																																											if (flag219)
																																																																																																																											{
																																																																																																																												this.mpl = new MediaPlay();
																																																																																																																												this.mpl.Closed += delegate(object a, EventArgs b)
																																																																																																																												{
																																																																																																																													this.mpl = null;
																																																																																																																												};
																																																																																																																											}
																																																																																																																											this.mpl.WindowState = WindowState.Normal;
																																																																																																																											this.mpl.Show();
																																																																																																																											this.mpl.Activate();
																																																																																																																											this.endSearchCmd();
																																																																																																																										}
																																																																																																																									}
																																																																																																																									else
																																																																																																																									{
																																																																																																																										bool flag220 = _speech == "reproduza algo" || _speech == "reproduzir alguma midia" || _speech == "reproduza alguma midia";
																																																																																																																										if (flag220)
																																																																																																																										{
																																																																																																																											bool music2 = Settings.Default.Music;
																																																																																																																											if (music2)
																																																																																																																											{
																																																																																																																												bool flag221 = this.mpl == null;
																																																																																																																												if (flag221)
																																																																																																																												{
																																																																																																																													this.mpl = new MediaPlay();
																																																																																																																													this.mpl.Closed += delegate(object a, EventArgs b)
																																																																																																																													{
																																																																																																																														this.mpl = null;
																																																																																																																													};
																																																																																																																													this.mpl.comandoVoz("reproducir la música");
																																																																																																																												}
																																																																																																																												this.SpeakOut(new string[]
																																																																																																																												{
																																																																																																																													"vou reproduzir algo",
																																																																																																																													"espero que sua lista já esteja carregada, vou reproduzir algo"
																																																																																																																												});
																																																																																																																												this.mpl.WindowState = WindowState.Normal;
																																																																																																																												this.mpl.Show();
																																																																																																																												this.mpl.Activate();
																																																																																																																												this.endSearchCmd();
																																																																																																																											}
																																																																																																																										}
																																																																																																																										else
																																																																																																																										{
																																																																																																																											bool flag222 = _speech == "reproduzir música" || _speech == "reproduza uma música" || _speech == "reproduzir a faixa" || _speech == "reproduzir mídia";
																																																																																																																											if (flag222)
																																																																																																																											{
																																																																																																																												bool flag223 = this.mpl == null;
																																																																																																																												if (flag223)
																																																																																																																												{
																																																																																																																													this.processesR = Process.GetProcessesByName("winamp");
																																																																																																																													bool flag224 = this.processesR.Count<Process>() != 0;
																																																																																																																													if (flag224)
																																																																																																																													{
																																																																																																																														this.iHandleR = Win32.FindWindow("Winamp v1.x", null);
																																																																																																																														Win32.SendMessage(this.iHandleR, 273, 40045, 0);
																																																																																																																														this.SpeakOut(new string[]
																																																																																																																														{
																																																																																																																															"reproduzindo "
																																																																																																																														});
																																																																																																																														this.endSearchCmd();
																																																																																																																													}
																																																																																																																													this.processesR = Process.GetProcessesByName("wmplayer");
																																																																																																																													bool flag225 = this.processesR.Count<Process>() != 0;
																																																																																																																													if (flag225)
																																																																																																																													{
																																																																																																																														this.iHandleR = Win32.FindWindow("WMPlayerApp", "Reproductor de Windows Media");
																																																																																																																														Win32.SendMessage(this.iHandleR, 273, 18808, 0);
																																																																																																																														this.SpeakOut(new string[]
																																																																																																																														{
																																																																																																																															"o tocador não está aberto "
																																																																																																																														});
																																																																																																																														this.endSearchCmd();
																																																																																																																													}
																																																																																																																												}
																																																																																																																												else
																																																																																																																												{
																																																																																																																													this.mpl.comandoVoz("reproducir la música");
																																																																																																																													this.endSearchCmd();
																																																																																																																												}
																																																																																																																											}
																																																																																																																											else
																																																																																																																											{
																																																																																																																												bool flag226 = _speech == "música anterior" || _speech == "faixa anterior" || _speech == "canção anterior" || _speech == "midia anterior";
																																																																																																																												if (flag226)
																																																																																																																												{
																																																																																																																													bool flag227 = this.mpl == null;
																																																																																																																													if (flag227)
																																																																																																																													{
																																																																																																																														this.processesR = Process.GetProcessesByName("winamp");
																																																																																																																														bool flag228 = this.processesR.Count<Process>() != 0;
																																																																																																																														if (flag228)
																																																																																																																														{
																																																																																																																															this.iHandleR = Win32.FindWindow("Winamp v1.x", null);
																																																																																																																															Win32.SendMessage(this.iHandleR, 273, 40044, 0);
																																																																																																																															this.SpeakOut(new string[]
																																																																																																																															{
																																																																																																																																"retrocendendo a midia "
																																																																																																																															});
																																																																																																																															this.endSearchCmd();
																																																																																																																														}
																																																																																																																														this.processesR = Process.GetProcessesByName("wmplayer");
																																																																																																																														bool flag229 = this.processesR.Count<Process>() != 0;
																																																																																																																														if (flag229)
																																																																																																																														{
																																																																																																																															this.iHandleR = Win32.FindWindow("WMPlayerApp", "Reproductor de Windows Media");
																																																																																																																															Win32.SendMessage(this.iHandleR, 273, 18810, 0);
																																																																																																																															this.SpeakOut(new string[]
																																																																																																																															{
																																																																																																																																"o tocador não está aberto "
																																																																																																																															});
																																																																																																																															this.endSearchCmd();
																																																																																																																														}
																																																																																																																													}
																																																																																																																													else
																																																																																																																													{
																																																																																																																														this.mpl.comandoVoz("anterior música");
																																																																																																																														this.endSearchCmd();
																																																																																																																													}
																																																																																																																												}
																																																																																																																												else
																																																																																																																												{
																																																																																																																													bool flag230 = _speech == "próxima música" || _speech == "próxima faixa" || _speech == "mídia seguinte";
																																																																																																																													if (flag230)
																																																																																																																													{
																																																																																																																														bool flag231 = this.mpl == null;
																																																																																																																														if (flag231)
																																																																																																																														{
																																																																																																																															this.processesR = Process.GetProcessesByName("winamp");
																																																																																																																															bool flag232 = this.processesR.Count<Process>() != 0;
																																																																																																																															if (flag232)
																																																																																																																															{
																																																																																																																																this.iHandleR = Win32.FindWindow("Winamp v1.x", null);
																																																																																																																																Win32.SendMessage(this.iHandleR, 273, 40048, 0);
																																																																																																																																this.SpeakOut(new string[]
																																																																																																																																{
																																																																																																																																	"acessando midia senguinte "
																																																																																																																																});
																																																																																																																																this.endSearchCmd();
																																																																																																																															}
																																																																																																																															this.processesR = Process.GetProcessesByName("wmplayer");
																																																																																																																															bool flag233 = this.processesR.Count<Process>() != 0;
																																																																																																																															if (flag233)
																																																																																																																															{
																																																																																																																																this.iHandleR = Win32.FindWindow("WMPlayerApp", "Reproductor de Windows Media");
																																																																																																																																Win32.SendMessage(this.iHandleR, 273, 18811, 0);
																																																																																																																																this.SpeakOut(new string[]
																																																																																																																																{
																																																																																																																																	"O tocador não está aberto "
																																																																																																																																});
																																																																																																																																this.endSearchCmd();
																																																																																																																															}
																																																																																																																														}
																																																																																																																														else
																																																																																																																														{
																																																																																																																															this.mpl.comandoVoz("siguiente música");
																																																																																																																															this.endSearchCmd();
																																																																																																																														}
																																																																																																																													}
																																																																																																																													else
																																																																																																																													{
																																																																																																																														bool flag234 = _speech == "pausar a música" || _speech == "pausar a midia" || _speech == "pausar a faixa" || _speech == "pausar mídia";
																																																																																																																														if (flag234)
																																																																																																																														{
																																																																																																																															bool flag235 = this.mpl == null;
																																																																																																																															if (flag235)
																																																																																																																															{
																																																																																																																																this.processesR = Process.GetProcessesByName("winamp");
																																																																																																																																bool flag236 = this.processesR.Count<Process>() != 0;
																																																																																																																																if (flag236)
																																																																																																																																{
																																																																																																																																	this.iHandleR = Win32.FindWindow("Winamp v1.x", null);
																																																																																																																																	Win32.SendMessage(this.iHandleR, 273, 40046, 0);
																																																																																																																																	this.SpeakOut(new string[]
																																																																																																																																	{
																																																																																																																																		"congelando a exibição "
																																																																																																																																	});
																																																																																																																																	this.endSearchCmd();
																																																																																																																																}
																																																																																																																																this.processesR = Process.GetProcessesByName("wmplayer");
																																																																																																																																bool flag237 = this.processesR.Count<Process>() != 0;
																																																																																																																																if (flag237)
																																																																																																																																{
																																																																																																																																	this.iHandleR = Win32.FindWindow("WMPlayerApp", "Reproductor de Windows Media");
																																																																																																																																	Win32.SendMessage(this.iHandleR, 273, 18808, 0);
																																																																																																																																	this.SpeakOut(new string[]
																																																																																																																																	{
																																																																																																																																		"o tocador não está aberto "
																																																																																																																																	});
																																																																																																																																	this.endSearchCmd();
																																																																																																																																}
																																																																																																																															}
																																																																																																																															else
																																																																																																																															{
																																																																																																																																this.mpl.comandoVoz("pausar la música");
																																																																																																																																this.endSearchCmd();
																																																																																																																															}
																																																																																																																														}
																																																																																																																														else
																																																																																																																														{
																																																																																																																															bool flag238 = _speech == "aumentar o tocador" || _speech == "suspendam o volume";
																																																																																																																															if (flag238)
																																																																																																																															{
																																																																																																																																bool flag239 = this.mpl == null;
																																																																																																																																if (flag239)
																																																																																																																																{
																																																																																																																																	this.processesR = Process.GetProcessesByName("winamp");
																																																																																																																																	bool flag240 = this.processesR.Count<Process>() != 0;
																																																																																																																																	if (flag240)
																																																																																																																																	{
																																																																																																																																		this.iHandleR = Win32.FindWindow("Winamp v1.x", null);
																																																																																																																																		Win32.SendMessage(this.iHandleR, 273, 40058, 0);
																																																																																																																																		this.SpeakOut(new string[]
																																																																																																																																		{
																																																																																																																																			"mais? "
																																																																																																																																		});
																																																																																																																																		this.endSearchCmd();
																																																																																																																																	}
																																																																																																																																	this.processesR = Process.GetProcessesByName("wmplayer");
																																																																																																																																	bool flag241 = this.processesR.Count<Process>() != 0;
																																																																																																																																	if (flag241)
																																																																																																																																	{
																																																																																																																																		this.iHandleR = Win32.FindWindow("WMPlayerApp", "Reproductor de Windows Media");
																																																																																																																																		Win32.SendMessage(this.iHandleR, 273, 18815, 0);
																																																																																																																																		this.SpeakOut(new string[]
																																																																																																																																		{
																																																																																																																																			"não está aberto "
																																																																																																																																		});
																																																																																																																																		this.endSearchCmd();
																																																																																																																																	}
																																																																																																																																}
																																																																																																																																else
																																																																																																																																{
																																																																																																																																	this.mpl.comandoVoz("subir el volumen");
																																																																																																																																	this.endSearchCmd();
																																																																																																																																}
																																																																																																																															}
																																																																																																																															else
																																																																																																																															{
																																																																																																																																bool flag242 = _speech == "Abaixar o tocador" || _speech == "diminua o volume";
																																																																																																																																if (flag242)
																																																																																																																																{
																																																																																																																																	bool flag243 = this.mpl == null;
																																																																																																																																	if (flag243)
																																																																																																																																	{
																																																																																																																																		this.processesR = Process.GetProcessesByName("winamp");
																																																																																																																																		bool flag244 = this.processesR.Count<Process>() != 0;
																																																																																																																																		if (flag244)
																																																																																																																																		{
																																																																																																																																			this.iHandleR = Win32.FindWindow("Winamp v1.x", null);
																																																																																																																																			Win32.SendMessage(this.iHandleR, 273, 40059, 0);
																																																																																																																																			this.SpeakOut(new string[]
																																																																																																																																			{
																																																																																																																																				"menos?"
																																																																																																																																			});
																																																																																																																																			this.endSearchCmd();
																																																																																																																																		}
																																																																																																																																		this.processesR = Process.GetProcessesByName("wmplayer");
																																																																																																																																		bool flag245 = this.processesR.Count<Process>() != 0;
																																																																																																																																		if (flag245)
																																																																																																																																		{
																																																																																																																																			this.iHandleR = Win32.FindWindow("WMPlayerApp", "Reproductor de Windows Media");
																																																																																																																																			Win32.SendMessage(this.iHandleR, 273, 18816, 0);
																																																																																																																																			this.SpeakOut(new string[]
																																																																																																																																			{
																																																																																																																																				"O tocador não está aberto "
																																																																																																																																			});
																																																																																																																																			this.endSearchCmd();
																																																																																																																																		}
																																																																																																																																	}
																																																																																																																																	else
																																																																																																																																	{
																																																																																																																																		this.mpl.comandoVoz("bajar el volumen");
																																																																																																																																		this.endSearchCmd();
																																																																																																																																	}
																																																																																																																																}
																																																																																																																																else
																																																																																																																																{
																																																																																																																																	bool flag246 = _speech == "silenciar música" || _speech == "silenciar a música" || _speech == "deixar a mídia muda" || _speech == "silenciar a mídia";
																																																																																																																																	if (flag246)
																																																																																																																																	{
																																																																																																																																		bool flag247 = this.mpl == null;
																																																																																																																																		if (flag247)
																																																																																																																																		{
																																																																																																																																			this.iHandleR = Win32.FindWindow("WMPlayerApp", "Reproductor de Windows Media");
																																																																																																																																			Process[] processesByName = Process.GetProcessesByName("wmplayer");
																																																																																																																																			int num;
																																																																																																																																			for (int num8 = 0; num8 < processesByName.Length; num8 = num + 1)
																																																																																																																																			{
																																																																																																																																				IntPtr mainWindowHandle = processesByName[num8].MainWindowHandle;
																																																																																																																																				Win32.SendMessage(this.iHandleR, 273, 18817, 0);
																																																																																																																																				this.SpeakOut(new string[]
																																																																																																																																				{
																																																																																																																																					"controle de volume mundo "
																																																																																																																																				});
																																																																																																																																				this.endSearchCmd();
																																																																																																																																				num = num8;
																																																																																																																																			}
																																																																																																																																		}
																																																																																																																																		else
																																																																																																																																		{
																																																																																																																																			this.mpl.comandoVoz("silenciar la música");
																																																																																																																																			this.endSearchCmd();
																																																																																																																																		}
																																																																																																																																	}
																																																																																																																																	else
																																																																																																																																	{
																																																																																																																																		bool flag248 = _speech == "ativar aleatório" || _speech == "desativar aleatório";
																																																																																																																																		if (flag248)
																																																																																																																																		{
																																																																																																																																			bool flag249 = this.mpl == null;
																																																																																																																																			if (flag249)
																																																																																																																																			{
																																																																																																																																				this.processesR = Process.GetProcessesByName("winamp");
																																																																																																																																				bool flag250 = this.processesR.Count<Process>() != 0;
																																																																																																																																				if (flag250)
																																																																																																																																				{
																																																																																																																																					this.iHandleR = Win32.FindWindow("Winamp v1.x", null);
																																																																																																																																					Win32.SendMessage(this.iHandleR, 273, 40023, 0);
																																																																																																																																					this.SpeakOut(new string[]
																																																																																																																																					{
																																																																																																																																						"misturador ativado "
																																																																																																																																					});
																																																																																																																																					this.endSearchCmd();
																																																																																																																																				}
																																																																																																																																				this.processesR = Process.GetProcessesByName("wmplayer");
																																																																																																																																				bool flag251 = this.processesR.Count<Process>() != 0;
																																																																																																																																				if (flag251)
																																																																																																																																				{
																																																																																																																																					this.iHandleR = Win32.FindWindow("WMPlayerApp", "Reproductor de Windows Media");
																																																																																																																																					Win32.SendMessage(this.iHandleR, 273, 18842, 0);
																																																																																																																																					this.SpeakOut(new string[]
																																																																																																																																					{
																																																																																																																																						"o tocador não está aberto "
																																																																																																																																					});
																																																																																																																																					this.endSearchCmd();
																																																																																																																																				}
																																																																																																																																			}
																																																																																																																																			else
																																																																																																																																			{
																																																																																																																																				this.mpl.comandoVoz("activar aleatorio");
																																																																																																																																				this.endSearchCmd();
																																																																																																																																			}
																																																																																																																																		}
																																																																																																																																		else
																																																																																																																																		{
																																																																																																																																			bool flag252 = _speech == "abrir com reprodutor" || _speech == "abrir midia com o reprodutor";
																																																																																																																																			if (flag252)
																																																																																																																																			{
																																																																																																																																				bool flag253 = this.mpl == null;
																																																																																																																																				if (flag253)
																																																																																																																																				{
																																																																																																																																					this.SpeakOut(new string[]
																																																																																																																																					{
																																																																																																																																						"o tocador não esta aberto"
																																																																																																																																					});
																																																																																																																																				}
																																																																																																																																				else
																																																																																																																																				{
																																																																																																																																					this.mpl.comandoVoz("abrir con reproductor");
																																																																																																																																					this.frases();
																																																																																																																																				}
																																																																																																																																				this.comandoEjecutado = true;
																																																																																																																																			}
																																																																																																																																			else
																																																																																																																																			{
																																																																																																																																				bool flag254 = _speech == "fechar o tocador" || _speech == "sair do tocador";
																																																																																																																																				if (flag254)
																																																																																																																																				{
																																																																																																																																					bool flag255 = this.mpl == null;
																																																																																																																																					if (flag255)
																																																																																																																																					{
																																																																																																																																						this.processesR = Process.GetProcessesByName("winamp");
																																																																																																																																						bool flag256 = this.processesR.Count<Process>() != 0;
																																																																																																																																						if (flag256)
																																																																																																																																						{
																																																																																																																																							this.processesR[0].CloseMainWindow();
																																																																																																																																							this.frases();
																																																																																																																																							this.endSearchCmd();
																																																																																																																																						}
																																																																																																																																						this.processesR = Process.GetProcessesByName("wmplayer");
																																																																																																																																						bool flag257 = this.processesR.Count<Process>() != 0;
																																																																																																																																						if (flag257)
																																																																																																																																						{
																																																																																																																																							this.processesR[0].CloseMainWindow();
																																																																																																																																							this.SpeakOut(new string[]
																																																																																																																																							{
																																																																																																																																								"o tocador não esta aberto "
																																																																																																																																							});
																																																																																																																																							this.endSearchCmd();
																																																																																																																																						}
																																																																																																																																					}
																																																																																																																																					else
																																																																																																																																					{
																																																																																																																																						this.mpl.comandoVoz("cerrar el reproductor");
																																																																																																																																						this.endSearchCmd();
																																																																																																																																					}
																																																																																																																																				}
																																																																																																																																				else
																																																																																																																																				{
																																																																																																																																					bool flag258 = _speech == "pare a música" || _speech == "parar de tocar";
																																																																																																																																					if (flag258)
																																																																																																																																					{
																																																																																																																																						bool flag259 = this.mpl == null;
																																																																																																																																						if (flag259)
																																																																																																																																						{
																																																																																																																																							this.processesR = Process.GetProcessesByName("winamp");
																																																																																																																																							bool flag260 = this.processesR.Count<Process>() != 0;
																																																																																																																																							if (flag260)
																																																																																																																																							{
																																																																																																																																								this.iHandleR = Win32.FindWindow("Winamp v1.x", null);
																																																																																																																																								Win32.SendMessage(this.iHandleR, 273, 40047, 0);
																																																																																																																																								this.frases();
																																																																																																																																								this.endSearchCmd();
																																																																																																																																							}
																																																																																																																																							this.processesR = Process.GetProcessesByName("wmplayer");
																																																																																																																																							bool flag261 = this.processesR.Count<Process>() != 0;
																																																																																																																																							if (flag261)
																																																																																																																																							{
																																																																																																																																								this.iHandleR = Win32.FindWindow("WMPlayerApp", "Reproductor de Windows Media");
																																																																																																																																								Win32.SendMessage(this.iHandleR, 273, 18809, 0);
																																																																																																																																								this.SpeakOut(new string[]
																																																																																																																																								{
																																																																																																																																									"o tocador não está aberto "
																																																																																																																																								});
																																																																																																																																								this.endSearchCmd();
																																																																																																																																							}
																																																																																																																																						}
																																																																																																																																						else
																																																																																																																																						{
																																																																																																																																							this.mpl.comandoVoz("stop");
																																																																																																																																							this.endSearchCmd();
																																																																																																																																						}
																																																																																																																																					}
																																																																																																																																					else
																																																																																																																																					{
																																																																																																																																						bool flag262 = _speech == "maximizar tocador" || _speech == "tocador em tela normal" || _speech == "maximizar o tocador";
																																																																																																																																						if (flag262)
																																																																																																																																						{
																																																																																																																																							bool flag263 = this.mpl == null;
																																																																																																																																							if (flag263)
																																																																																																																																							{
																																																																																																																																								this.processesR = Process.GetProcessesByName("winamp");
																																																																																																																																								bool flag264 = this.processesR.Count<Process>() != 0;
																																																																																																																																								if (flag264)
																																																																																																																																								{
																																																																																																																																									this.iHandleR = Win32.FindWindow("Winamp v1.x", null);
																																																																																																																																									Win32.SendMessage(this.iHandleR, 273, 40045, 0);
																																																																																																																																									this.SpeakOut(new string[]
																																																																																																																																									{
																																																																																																																																										"reproduzindo "
																																																																																																																																									});
																																																																																																																																									this.endSearchCmd();
																																																																																																																																								}
																																																																																																																																								this.processesR = Process.GetProcessesByName("wmplayer");
																																																																																																																																								bool flag265 = this.processesR.Count<Process>() != 0;
																																																																																																																																								if (flag265)
																																																																																																																																								{
																																																																																																																																									this.iHandleR = Win32.FindWindow("WMPlayerApp", "Reproductor de Windows Media");
																																																																																																																																									Win32.SendMessage(this.iHandleR, 273, 18808, 0);
																																																																																																																																									this.SpeakOut(new string[]
																																																																																																																																									{
																																																																																																																																										"o tocador não está aberto "
																																																																																																																																									});
																																																																																																																																									this.endSearchCmd();
																																																																																																																																								}
																																																																																																																																							}
																																																																																																																																							else
																																																																																																																																							{
																																																																																																																																								this.mpl.comandoVoz("encher a tela");
																																																																																																																																								this.endSearchCmd();
																																																																																																																																							}
																																																																																																																																						}
																																																																																																																																						else
																																																																																																																																						{
																																																																																																																																							bool flag266 = _speech == "minimizar o tocador" || _speech == "minimizar tocador";
																																																																																																																																							if (flag266)
																																																																																																																																							{
																																																																																																																																								bool flag267 = this.mpl == null;
																																																																																																																																								if (flag267)
																																																																																																																																								{
																																																																																																																																									this.processesR = Process.GetProcessesByName("winamp");
																																																																																																																																									bool flag268 = this.processesR.Count<Process>() != 0;
																																																																																																																																									if (flag268)
																																																																																																																																									{
																																																																																																																																										this.iHandleR = Win32.FindWindow("Winamp v1.x", null);
																																																																																																																																										Win32.SendMessage(this.iHandleR, 273, 40023, 0);
																																																																																																																																										this.SpeakOut(new string[]
																																																																																																																																										{
																																																																																																																																											"misturador ativado "
																																																																																																																																										});
																																																																																																																																										this.endSearchCmd();
																																																																																																																																									}
																																																																																																																																									this.processesR = Process.GetProcessesByName("wmplayer");
																																																																																																																																									bool flag269 = this.processesR.Count<Process>() != 0;
																																																																																																																																									if (flag269)
																																																																																																																																									{
																																																																																																																																										this.iHandleR = Win32.FindWindow("WMPlayerApp", "Reproductor de Windows Media");
																																																																																																																																										Win32.SendMessage(this.iHandleR, 273, 18842, 0);
																																																																																																																																										this.SpeakOut(new string[]
																																																																																																																																										{
																																																																																																																																											"o tocador não está aberto "
																																																																																																																																										});
																																																																																																																																										this.endSearchCmd();
																																																																																																																																									}
																																																																																																																																								}
																																																																																																																																								else
																																																																																																																																								{
																																																																																																																																									this.mpl.comandoVoz("minimizar el reproductor");
																																																																																																																																									this.endSearchCmd();
																																																																																																																																								}
																																																																																																																																							}
																																																																																																																																							else
																																																																																																																																							{
																																																																																																																																								bool flag270 = this.mpl != null;
																																																																																																																																								if (flag270)
																																																																																																																																								{
																																																																																																																																									this.mpl.comandoVoz(_speech);
																																																																																																																																									this.endSearchCmd();
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
											}
										}
									}
								}
							}
						}
					}
				}
				bool adicionais = Settings.Default.Adicionais;
				if (adicionais)
				{
					bool flag271 = _speech == "Apagar";
					if (flag271)
					{
						InputSimulator.SimulateKeyPress(VirtualKeyCode.BACK);
						this.frases();
						this.endSearchCmd();
					}
					else
					{
						bool flag272 = _speech == "Apagar Tres vezes" || _speech == "deletar Tres vezes";
						if (flag272)
						{
							int num;
							for (int num9 = 0; num9 < 3; num9 = num + 1)
							{
								InputSimulator.SimulateKeyPress(VirtualKeyCode.BACK);
								Thread.Sleep(50);
								num = num9;
							}
							this.frases();
							this.endSearchCmd();
						}
						else
						{
							bool flag273 = _speech == "Apagar cinco vezes" || _speech == "deletar cinco vezes";
							if (flag273)
							{
								int num;
								for (int num10 = 0; num10 < 5; num10 = num + 1)
								{
									InputSimulator.SimulateKeyPress(VirtualKeyCode.BACK);
									Thread.Sleep(50);
									num = num10;
								}
								this.frases();
								this.endSearchCmd();
							}
							else
							{
								bool flag274 = _speech == "Apagar dez vezes" || _speech == "deletar dez vezes";
								if (flag274)
								{
									try
									{
										int num;
										for (int num11 = 0; num11 < 10; num11 = num + 1)
										{
											InputSimulator.SimulateKeyPress(VirtualKeyCode.BACK);
											Thread.Sleep(50);
											num = num11;
										}
										this.frases();
										this.endSearchCmd();
									}
									catch (Exception)
									{
										this.SpeakOut(new string[]
										{
											"ative os comandos avançados",
											"os comandos avaçandos não foram ativados"
										});
									}
								}
								else
								{
									bool flag275 = _speech == "Apagar ciquenta vezes" || _speech == "deletar ciquenta vezes";
									if (flag275)
									{
										int num;
										for (int num12 = 0; num12 < 50; num12 = num + 1)
										{
											InputSimulator.SimulateKeyPress(VirtualKeyCode.BACK);
											Thread.Sleep(50);
											num = num12;
										}
										this.frases();
										this.endSearchCmd();
									}
									else
									{
										bool flag276 = _speech == "Dessa até o fim da pagina" || _speech == "ir para o fim da página";
										if (flag276)
										{
											bool flag277 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
											if (flag277)
											{
												InputSimulator.SimulateKeyPress(VirtualKeyCode.END);
												this.frases();
											}
											else
											{
												this.frasesresp();
											}
											this.endSearchCmd();
										}
										else
										{
											bool flag278 = _speech == "Exibi o código fonte" || _speech == "Exiba o código fonte da página";
											if (flag278)
											{
												bool flag279 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
												if (flag279)
												{
													InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_U);
													this.frases();
												}
												else
												{
													this.frasesresp();
												}
												this.endSearchCmd();
											}
											else
											{
												bool flag280 = _speech == "excluir a palavra" || _speech == "Excluir o texto";
												if (flag280)
												{
													InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.BACK);
													this.frases();
													this.endSearchCmd();
												}
												else
												{
													bool flag281 = _speech == "Exibir Favoritos" || _speech == "Olcutar Favoritos";
													if (flag281)
													{
														bool flag282 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
														if (flag282)
														{
															InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, new VirtualKeyCode[]
															{
																VirtualKeyCode.SHIFT,
																VirtualKeyCode.VK_B
															});
															this.frases();
														}
														else
														{
															this.frasesresp();
														}
														this.endSearchCmd();
													}
													else
													{
														bool flag283 = _speech == "Entrar Tres vezes" || _speech == "nova linha Tres vezes";
														if (flag283)
														{
															int num;
															for (int num13 = 0; num13 < 3; num13 = num + 1)
															{
																InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
																Thread.Sleep(50);
																num = num13;
															}
															this.frases();
															this.endSearchCmd();
														}
														else
														{
															bool flag284 = _speech == "Entrar cinco vezes" || _speech == "nova linha cinco vezes";
															if (flag284)
															{
																int num;
																for (int num14 = 0; num14 < 5; num14 = num + 1)
																{
																	InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
																	Thread.Sleep(50);
																	num = num14;
																}
																this.frases();
																this.endSearchCmd();
															}
															else
															{
																bool flag285 = _speech == "Entrar dez vezes" || _speech == "nova linha dez vezes";
																if (flag285)
																{
																	int num;
																	for (int num15 = 0; num15 < 10; num15 = num + 1)
																	{
																		InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
																		Thread.Sleep(50);
																		num = num15;
																	}
																	this.frases();
																	this.endSearchCmd();
																}
																else
																{
																	bool flag286 = _speech == "Fechar Janela Da Página" || _speech == "Sair da Página";
																	if (flag286)
																	{
																		bool flag287 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																		if (flag287)
																		{
																			InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, new VirtualKeyCode[]
																			{
																				VirtualKeyCode.SHIFT,
																				VirtualKeyCode.VK_W
																			});
																			this.SpeakOut(new string[]
																			{
																				"Fechando a janela atual "
																			});
																		}
																		else
																		{
																			this.frasesresp();
																		}
																		this.endSearchCmd();
																	}
																	else
																	{
																		bool flag288 = _speech == "Foco do navegador" || _speech == "Definir o foco";
																		if (flag288)
																		{
																			bool flag289 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																			if (flag289)
																			{
																				InputSimulator.SimulateKeyDown(VirtualKeyCode.F6);
																				this.SpeakOut(new string[]
																				{
																					"Definindo o foco no primeiro item na barra de ferramentas do Navegador "
																				});
																			}
																			else
																			{
																				this.frasesresp();
																			}
																			this.endSearchCmd();
																		}
																		else
																		{
																			bool flag290 = _speech == "guia um" || _speech == "ir para guia um";
																			if (flag290)
																			{
																				bool flag291 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																				if (flag291)
																				{
																					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.NUMPAD1);
																					this.SpeakOut(new string[]
																					{
																						"selecionando a primeira guia "
																					});
																				}
																				else
																				{
																					this.SpeakOut(new string[]
																					{
																						"não estou encontrando navegador selecionado"
																					});
																				}
																				this.endSearchCmd();
																			}
																			else
																			{
																				bool flag292 = _speech == "guia dois" || _speech == "ir para guia dois";
																				if (flag292)
																				{
																					bool flag293 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																					if (flag293)
																					{
																						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.NUMPAD2);
																						this.SpeakOut(new string[]
																						{
																							"selecionando a segunda guia "
																						});
																					}
																					else
																					{
																						this.SpeakOut(new string[]
																						{
																							"a segunda guia não está aberta ou seu navegador não está selecionado "
																						});
																					}
																					this.endSearchCmd();
																				}
																				else
																				{
																					bool flag294 = _speech == "guia três" || _speech == "ir para guia três";
																					if (flag294)
																					{
																						bool flag295 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																						if (flag295)
																						{
																							InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.NUMPAD3);
																							this.SpeakOut(new string[]
																							{
																								"selecionando a terceira guia "
																							});
																						}
																						else
																						{
																							this.SpeakOut(new string[]
																							{
																								"a segunda guia não está aberta ou seu navegador não está selecionado "
																							});
																						}
																						this.endSearchCmd();
																					}
																					else
																					{
																						bool flag296 = _speech == "guia quatro" || _speech == "ir para guia quatro";
																						if (flag296)
																						{
																							bool flag297 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																							if (flag297)
																							{
																								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.NUMPAD4);
																								this.SpeakOut(new string[]
																								{
																									"selecionando a quarta guia "
																								});
																							}
																							else
																							{
																								this.SpeakOut(new string[]
																								{
																									"a quarta guia não está aberta ou seu navegador não está selecionado "
																								});
																							}
																							this.endSearchCmd();
																						}
																						else
																						{
																							bool flag298 = _speech == "guia cinco" || _speech == "ir para guia cinco";
																							if (flag298)
																							{
																								bool flag299 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																								if (flag299)
																								{
																									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.NUMPAD5);
																									this.SpeakOut(new string[]
																									{
																										"selecionando a quinta guia "
																									});
																								}
																								else
																								{
																									this.SpeakOut(new string[]
																									{
																										"a quinta guia não está aberta ou seu navegador não está selecionado "
																									});
																								}
																								this.endSearchCmd();
																							}
																							else
																							{
																								bool flag300 = _speech == "guia seis" || _speech == "ir para guia seis";
																								if (flag300)
																								{
																									bool flag301 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																									if (flag301)
																									{
																										InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.NUMPAD6);
																										this.SpeakOut(new string[]
																										{
																											"selecionando a sexta guia "
																										});
																									}
																									else
																									{
																										this.SpeakOut(new string[]
																										{
																											"a sexta guia não está aberta ou seu navegador não está selecionado "
																										});
																									}
																									this.endSearchCmd();
																								}
																								else
																								{
																									bool flag302 = _speech == "guia sete" || _speech == "ir para guia sete";
																									if (flag302)
																									{
																										bool flag303 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																										if (flag303)
																										{
																											InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.NUMPAD7);
																											this.SpeakOut(new string[]
																											{
																												"selecionando a sétima guia "
																											});
																										}
																										else
																										{
																											this.SpeakOut(new string[]
																											{
																												"a sétima guia não está aberta ou seu navegador não está selecionado "
																											});
																										}
																										this.endSearchCmd();
																									}
																									else
																									{
																										bool flag304 = _speech == "guia oito" || _speech == "ir para guia oito";
																										if (flag304)
																										{
																											bool flag305 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																											if (flag305)
																											{
																												InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.NUMPAD8);
																												this.SpeakOut(new string[]
																												{
																													"selecionando a oitava guia "
																												});
																											}
																											else
																											{
																												this.SpeakOut(new string[]
																												{
																													"a oitava guia não está aberta ou seu navegador não está selecionado "
																												});
																											}
																											this.endSearchCmd();
																										}
																										else
																										{
																											bool flag306 = _speech == "guia nove" || _speech == "ir para guia nove";
																											if (flag306)
																											{
																												bool flag307 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																												if (flag307)
																												{
																													InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.NUMPAD9);
																													this.SpeakOut(new string[]
																													{
																														"selecionando a nona guia "
																													});
																												}
																												else
																												{
																													this.SpeakOut(new string[]
																													{
																														"a nona guia não está aberta ou seu navegador não está selecionado "
																													});
																												}
																												this.endSearchCmd();
																											}
																											else
																											{
																												bool flag308 = _speech == "guia inicial da pagina" || _speech == "voltar para guia inicial";
																												if (flag308)
																												{
																													bool flag309 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																													if (flag309)
																													{
																														InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.HOME);
																														this.SpeakOut(new string[]
																														{
																															"voltando para guia inicial "
																														});
																													}
																													else
																													{
																														this.frasesresp();
																													}
																													this.endSearchCmd();
																												}
																												else
																												{
																													bool flag310 = _speech == "Gereciador do Navegador" || _speech == "Abrir Gereciador do Navegador";
																													if (flag310)
																													{
																														bool flag311 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																														if (flag311)
																														{
																															InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.ESCAPE);
																															this.SpeakOut(new string[]
																															{
																																"Abrindo o Gerenciador de tarefas do Navegador "
																															});
																														}
																														else
																														{
																															this.frasesresp();
																														}
																														this.endSearchCmd();
																													}
																													else
																													{
																														bool flag312 = _speech == "Histórico de navegação" || _speech == "Abrir histórico";
																														if (flag312)
																														{
																															bool flag313 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																															if (flag313)
																															{
																																InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_H);
																																this.SpeakOut(new string[]
																																{
																																	"Abrindo a página do histórico em uma nova guia "
																																});
																															}
																															else
																															{
																																this.frasesresp();
																															}
																															this.endSearchCmd();
																														}
																														else
																														{
																															bool flag314 = _speech == "Interromper carregamento da página" || _speech == "Cancelar Carregamento";
																															if (flag314)
																															{
																																bool flag315 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																if (flag315)
																																{
																																	InputSimulator.SimulateKeyPress(VirtualKeyCode.ESCAPE);
																																	this.SpeakOut(new string[]
																																	{
																																		"Cancelando atualizações "
																																	});
																																}
																																else
																																{
																																	this.frasesresp();
																																}
																																this.endSearchCmd();
																															}
																															else
																															{
																																bool flag316 = _speech == "Localizar" || _speech == "Abrir a barra de localização";
																																if (flag316)
																																{
																																	bool flag317 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																	if (flag317)
																																	{
																																		InputSimulator.SimulateKeyPress(VirtualKeyCode.F3);
																																		this.SpeakOut(new string[]
																																		{
																																			"digite sua pesquisa "
																																		});
																																	}
																																	else
																																	{
																																		this.frasesresp();
																																	}
																																	this.endSearchCmd();
																																}
																																else
																																{
																																	bool flag318 = _speech == "limpar dados de navegação" || _speech == "Excluir dados de navegação";
																																	if (flag318)
																																	{
																																		bool flag319 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																		if (flag319)
																																		{
																																			SendKeys.SendWait("^+{DEL}");
																																			this.SpeakOut(new string[]
																																			{
																																				"clique em continuar "
																																			});
																																		}
																																		else
																																		{
																																			this.frasesresp();
																																		}
																																		this.endSearchCmd();
																																	}
																																	else
																																	{
																																		bool flag320 = _speech == "Navegar para frente" || _speech == "Navegar";
																																		if (flag320)
																																		{
																																			InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
																																			this.SpeakOut(new string[]
																																			{
																																				"Selecionado "
																																			});
																																			this.endSearchCmd();
																																		}
																																		else
																																		{
																																			bool flag321 = _speech == "Navegar tres vezes" || _speech == "percorre tres vezes";
																																			if (flag321)
																																			{
																																				int num;
																																				for (int num16 = 0; num16 < 3; num16 = num + 1)
																																				{
																																					InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
																																					Thread.Sleep(50);
																																					num = num16;
																																				}
																																				this.frases();
																																				this.endSearchCmd();
																																			}
																																			else
																																			{
																																				bool flag322 = _speech == "Navegar cinco vezes" || _speech == "percorre cinco vezes";
																																				if (flag322)
																																				{
																																					int num;
																																					for (int num17 = 0; num17 < 5; num17 = num + 1)
																																					{
																																						InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
																																						Thread.Sleep(50);
																																						num = num17;
																																					}
																																					this.frases();
																																					this.endSearchCmd();
																																				}
																																				else
																																				{
																																					bool flag323 = _speech == "Navegar dez vezes" || _speech == "percorre dez vezes";
																																					if (flag323)
																																					{
																																						int num;
																																						for (int num18 = 0; num18 < 10; num18 = num + 1)
																																						{
																																							InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
																																							Thread.Sleep(50);
																																							num = num18;
																																						}
																																						this.frases();
																																						this.endSearchCmd();
																																					}
																																					else
																																					{
																																						bool flag324 = _speech == "Navegar cinquenta vezes" || _speech == "percorre cinquenta vezes";
																																						if (flag324)
																																						{
																																							int num;
																																							for (int num19 = 0; num19 < 50; num19 = num + 1)
																																							{
																																								InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
																																								Thread.Sleep(50);
																																								num = num19;
																																							}
																																							this.frases();
																																							this.endSearchCmd();
																																						}
																																						else
																																						{
																																							bool flag325 = _speech == "Navegar para tras" || _speech == "retornar a navegação para trás" || _speech == "retornar a navegação";
																																							if (flag325)
																																							{
																																								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.TAB);
																																								this.frases();
																																								this.endSearchCmd();
																																							}
																																							else
																																							{
																																								bool flag326 = _speech == "Navegar para trás três vezes" || _speech == "retroceder mais três vezes" || _speech == "retroceder mais tres";
																																								if (flag326)
																																								{
																																									int num;
																																									for (int num20 = 0; num20 < 3; num20 = num + 1)
																																									{
																																										InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.TAB);
																																										Thread.Sleep(50);
																																										num = num20;
																																									}
																																									this.frases();
																																									this.endSearchCmd();
																																								}
																																								else
																																								{
																																									bool flag327 = _speech == "Navegar para trás cinco vezes" || _speech == "retroceder mais cinco vezes" || _speech == "retroceder mais cinco";
																																									if (flag327)
																																									{
																																										int num;
																																										for (int num21 = 0; num21 < 5; num21 = num + 1)
																																										{
																																											InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.TAB);
																																											Thread.Sleep(50);
																																											num = num21;
																																										}
																																										this.frases();
																																										this.endSearchCmd();
																																									}
																																									else
																																									{
																																										bool flag328 = _speech == "Navegar para trás dez vezes" || _speech == "retroceder mais dez vezes" || _speech == "retroceder mais dez";
																																										if (flag328)
																																										{
																																											int num;
																																											for (int num22 = 0; num22 < 10; num22 = num + 1)
																																											{
																																												InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.TAB);
																																												Thread.Sleep(50);
																																												num = num22;
																																											}
																																											this.frases();
																																											this.endSearchCmd();
																																										}
																																										else
																																										{
																																											bool flag329 = _speech == "Navegar para trás ciquenta vezes" || _speech == "retroceder mais cinquenta vezes" || _speech == "retroceder mais cinquenta";
																																											if (flag329)
																																											{
																																												int num;
																																												for (int num23 = 0; num23 < 50; num23 = num + 1)
																																												{
																																													InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.TAB);
																																													Thread.Sleep(50);
																																													num = num23;
																																												}
																																												this.frases();
																																												this.endSearchCmd();
																																											}
																																											else
																																											{
																																												bool flag330 = _speech == "nova linha" || _speech == "próxima linha";
																																												if (flag330)
																																												{
																																													InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
																																													this.frases();
																																													this.endSearchCmd();
																																												}
																																												else
																																												{
																																													bool flag331 = _speech == "Nova guia de acesso" || _speech == "abrir uma nova guia de acesso";
																																													if (flag331)
																																													{
																																														bool flag332 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																														if (flag332)
																																														{
																																															InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
																																															this.frases();
																																														}
																																														else
																																														{
																																															this.frasesresp();
																																														}
																																														this.endSearchCmd();
																																													}
																																													else
																																													{
																																														bool flag333 = _speech == "Selecionar Pesquisa" || _speech == "Selecionar pesquisa da pagina";
																																														if (flag333)
																																														{
																																															bool flag334 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																															if (flag334)
																																															{
																																																InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_K);
																																																this.SpeakOut(new string[]
																																																{
																																																	"digite sua pesquisa "
																																																});
																																															}
																																															else
																																															{
																																																this.frasesresp();
																																															}
																																															this.endSearchCmd();
																																														}
																																														else
																																														{
																																															bool flag335 = _speech == "page para baixo" || _speech == "precionar page para cima";
																																															if (flag335)
																																															{
																																																InputSimulator.SimulateKeyPress(VirtualKeyCode.NEXT);
																																																this.frases();
																																																this.endSearchCmd();
																																															}
																																															else
																																															{
																																																bool flag336 = _speech == "page para cima" || _speech == "precionar page para baixo";
																																																if (flag336)
																																																{
																																																	InputSimulator.SimulateKeyPress(VirtualKeyCode.PRIOR);
																																																	this.frases();
																																																	this.endSearchCmd();
																																																}
																																																else
																																																{
																																																	bool flag337 = _speech == "Renomear" || _speech == "Renomear o arquivo";
																																																	if (flag337)
																																																	{
																																																		try
																																																		{
																																																			InputSimulator.SimulateKeyPress(VirtualKeyCode.F2);
																																																			this.SpeakOut(new string[]
																																																			{
																																																				"Escreva um nome para o arquivo "
																																																			});
																																																			this.endSearchCmd();
																																																		}
																																																		catch (Exception)
																																																		{
																																																			this.SpeakOut(new string[]
																																																			{
																																																				"selecione uma pasta ou documento que deseja renomear não encontro"
																																																			});
																																																		}
																																																	}
																																																	else
																																																	{
																																																		bool flag338 = _speech == "Rola até o inicio" || _speech == "ir para o inicio da página";
																																																		if (flag338)
																																																		{
																																																			bool flag339 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																			if (flag339)
																																																			{
																																																				InputSimulator.SimulateKeyPress(VirtualKeyCode.HOME);
																																																				this.frases();
																																																			}
																																																			else
																																																			{
																																																				this.frasesresp();
																																																			}
																																																			this.endSearchCmd();
																																																		}
																																																		else
																																																		{
																																																			bool flag340 = _speech == "Salvar como favorito" || _speech == "Salvar a pagína atual como favorito";
																																																			if (flag340)
																																																			{
																																																				bool flag341 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																				if (flag341)
																																																				{
																																																					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_D);
																																																					this.frases();
																																																				}
																																																				else
																																																				{
																																																					this.frasesresp();
																																																				}
																																																				this.endSearchCmd();
																																																			}
																																																			else
																																																			{
																																																				bool flag342 = _speech == "Salvar todas as guias" || _speech == "Salvar guias em uma nova pasta";
																																																				if (flag342)
																																																				{
																																																					bool flag343 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																					if (flag343)
																																																					{
																																																						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, new VirtualKeyCode[]
																																																						{
																																																							VirtualKeyCode.SHIFT,
																																																							VirtualKeyCode.VK_D
																																																						});
																																																						this.SpeakOut(new string[]
																																																						{
																																																							"Salvando, escolha onde quer salvar. . . . . . . ."
																																																						});
																																																					}
																																																					else
																																																					{
																																																						this.frasesresp();
																																																					}
																																																					this.endSearchCmd();
																																																				}
																																																				else
																																																				{
																																																					bool flag344 = _speech == "Sair da guia atual" || _speech == "Fechar guia atual";
																																																					if (flag344)
																																																					{
																																																						bool flag345 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																						if (flag345)
																																																						{
																																																							InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_W);
																																																							this.frases();
																																																						}
																																																						else
																																																						{
																																																							this.frasesresp();
																																																						}
																																																						this.endSearchCmd();
																																																					}
																																																					else
																																																					{
																																																						bool flag346 = _speech == "seta para cima" || _speech == "para cima";
																																																						if (flag346)
																																																						{
																																																							InputSimulator.SimulateKeyPress(VirtualKeyCode.UP);
																																																							this.frases();
																																																							this.endSearchCmd();
																																																						}
																																																						else
																																																						{
																																																							bool flag347 = _speech == "seta para cima tres vezes" || _speech == "para cima tres vezes";
																																																							if (flag347)
																																																							{
																																																								int num;
																																																								for (int num24 = 0; num24 < 3; num24 = num + 1)
																																																								{
																																																									InputSimulator.SimulateKeyPress(VirtualKeyCode.UP);
																																																									Thread.Sleep(50);
																																																									num = num24;
																																																								}
																																																								this.frases();
																																																								this.endSearchCmd();
																																																							}
																																																							else
																																																							{
																																																								bool flag348 = _speech == "seta para cima cinco vezes" || _speech == "para cima cinco vezes";
																																																								if (flag348)
																																																								{
																																																									int num;
																																																									for (int num25 = 0; num25 < 5; num25 = num + 1)
																																																									{
																																																										InputSimulator.SimulateKeyPress(VirtualKeyCode.UP);
																																																										Thread.Sleep(50);
																																																										num = num25;
																																																									}
																																																									this.frases();
																																																									this.endSearchCmd();
																																																								}
																																																								else
																																																								{
																																																									bool flag349 = _speech == "seta para cima dez vezes" || _speech == "para cima dez vezes";
																																																									if (flag349)
																																																									{
																																																										int num;
																																																										for (int num26 = 0; num26 < 10; num26 = num + 1)
																																																										{
																																																											InputSimulator.SimulateKeyPress(VirtualKeyCode.UP);
																																																											Thread.Sleep(50);
																																																											num = num26;
																																																										}
																																																										this.frases();
																																																										this.endSearchCmd();
																																																									}
																																																									else
																																																									{
																																																										bool flag350 = _speech == "seta para baixo" || _speech == "para baixo";
																																																										if (flag350)
																																																										{
																																																											InputSimulator.SimulateKeyPress(VirtualKeyCode.DOWN);
																																																											this.frases();
																																																											this.endSearchCmd();
																																																										}
																																																										else
																																																										{
																																																											bool flag351 = _speech == "seta para baixo tres vezes" || _speech == "para baixo tres vezes";
																																																											if (flag351)
																																																											{
																																																												int num;
																																																												for (int num27 = 0; num27 < 3; num27 = num + 1)
																																																												{
																																																													InputSimulator.SimulateKeyPress(VirtualKeyCode.DOWN);
																																																													Thread.Sleep(50);
																																																													num = num27;
																																																												}
																																																												this.frases();
																																																												this.endSearchCmd();
																																																											}
																																																											else
																																																											{
																																																												bool flag352 = _speech == "seta para baixo cinco vezes" || _speech == "para baixo tres vezes";
																																																												if (flag352)
																																																												{
																																																													int num;
																																																													for (int num28 = 0; num28 < 5; num28 = num + 1)
																																																													{
																																																														InputSimulator.SimulateKeyPress(VirtualKeyCode.DOWN);
																																																														Thread.Sleep(50);
																																																														num = num28;
																																																													}
																																																													this.frases();
																																																													this.endSearchCmd();
																																																												}
																																																												else
																																																												{
																																																													bool flag353 = _speech == "seta para baixo dez vezes" || _speech == "para baixo dez vezes";
																																																													if (flag353)
																																																													{
																																																														int num;
																																																														for (int num29 = 0; num29 < 10; num29 = num + 1)
																																																														{
																																																															InputSimulator.SimulateKeyPress(VirtualKeyCode.DOWN);
																																																															Thread.Sleep(50);
																																																															num = num29;
																																																														}
																																																														this.frases();
																																																														this.endSearchCmd();
																																																													}
																																																													else
																																																													{
																																																														bool flag354 = _speech == "seta para Esquerda" || _speech == "para Esquerda";
																																																														if (flag354)
																																																														{
																																																															InputSimulator.SimulateKeyPress(VirtualKeyCode.LEFT);
																																																															this.frases();
																																																															this.endSearchCmd();
																																																														}
																																																														else
																																																														{
																																																															bool flag355 = _speech == "seta para Esquerda tres vezes" || _speech == "para Esquerda tres vezes";
																																																															if (flag355)
																																																															{
																																																																int num;
																																																																for (int num30 = 0; num30 < 3; num30 = num + 1)
																																																																{
																																																																	InputSimulator.SimulateKeyPress(VirtualKeyCode.LEFT);
																																																																	Thread.Sleep(50);
																																																																	num = num30;
																																																																}
																																																																this.frases();
																																																																this.endSearchCmd();
																																																															}
																																																															else
																																																															{
																																																																bool flag356 = _speech == "seta para Esquerda cinco vezes" || _speech == "para Esquerda cinco vezes";
																																																																if (flag356)
																																																																{
																																																																	int num;
																																																																	for (int num31 = 0; num31 < 5; num31 = num + 1)
																																																																	{
																																																																		InputSimulator.SimulateKeyPress(VirtualKeyCode.LEFT);
																																																																		Thread.Sleep(50);
																																																																		num = num31;
																																																																	}
																																																																	this.frases();
																																																																	this.endSearchCmd();
																																																																}
																																																																else
																																																																{
																																																																	bool flag357 = _speech == "seta para Esquerda dez vezes" || _speech == "para Esquerda dez vezes";
																																																																	if (flag357)
																																																																	{
																																																																		int num;
																																																																		for (int num32 = 0; num32 < 10; num32 = num + 1)
																																																																		{
																																																																			InputSimulator.SimulateKeyPress(VirtualKeyCode.LEFT);
																																																																			Thread.Sleep(50);
																																																																			num = num32;
																																																																		}
																																																																		this.frases();
																																																																		this.endSearchCmd();
																																																																	}
																																																																	else
																																																																	{
																																																																		bool flag358 = _speech == "seta para direita" || _speech == "para direita";
																																																																		if (flag358)
																																																																		{
																																																																			InputSimulator.SimulateKeyPress(VirtualKeyCode.RIGHT);
																																																																			this.frases();
																																																																			this.endSearchCmd();
																																																																		}
																																																																		else
																																																																		{
																																																																			bool flag359 = _speech == "seta para direita tres vezes" || _speech == "para direita tres vezes";
																																																																			if (flag359)
																																																																			{
																																																																				int num;
																																																																				for (int num33 = 0; num33 < 3; num33 = num + 1)
																																																																				{
																																																																					InputSimulator.SimulateKeyPress(VirtualKeyCode.RIGHT);
																																																																					Thread.Sleep(50);
																																																																					num = num33;
																																																																				}
																																																																				this.frases();
																																																																				this.endSearchCmd();
																																																																			}
																																																																			else
																																																																			{
																																																																				bool flag360 = _speech == "seta para direita cinco vezes" || _speech == "para direita cinco vezes";
																																																																				if (flag360)
																																																																				{
																																																																					int num;
																																																																					for (int num34 = 0; num34 < 5; num34 = num + 1)
																																																																					{
																																																																						InputSimulator.SimulateKeyPress(VirtualKeyCode.RIGHT);
																																																																						Thread.Sleep(50);
																																																																						num = num34;
																																																																					}
																																																																					this.frases();
																																																																					this.endSearchCmd();
																																																																				}
																																																																				else
																																																																				{
																																																																					bool flag361 = _speech == "seta para direita dez vezes" || _speech == "para direita dez vezes";
																																																																					if (flag361)
																																																																					{
																																																																						int num;
																																																																						for (int num35 = 0; num35 < 10; num35 = num + 1)
																																																																						{
																																																																							InputSimulator.SimulateKeyPress(VirtualKeyCode.RIGHT);
																																																																							Thread.Sleep(50);
																																																																							num = num35;
																																																																						}
																																																																						this.frases();
																																																																						this.endSearchCmd();
																																																																					}
																																																																					else
																																																																					{
																																																																						bool flag362 = _speech == "Navegador em tela cheia" || _speech == "Tela cheia do navegador" || _speech == "Navegador em tela normal" || _speech == "tela normal do navegador";
																																																																						if (flag362)
																																																																						{
																																																																							bool flag363 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																							if (flag363)
																																																																							{
																																																																								InputSimulator.SimulateKeyPress(VirtualKeyCode.F11);
																																																																								this.frases();
																																																																							}
																																																																							else
																																																																							{
																																																																								this.frasesresp();
																																																																							}
																																																																							this.endSearchCmd();
																																																																						}
																																																																						else
																																																																						{
																																																																							bool flag364 = _speech == "avançar para o final do video" || _speech == "final do video";
																																																																							if (flag364)
																																																																							{
																																																																								bool flag365 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																								if (flag365)
																																																																								{
																																																																									InputSimulator.SimulateKeyPress(VirtualKeyCode.END);
																																																																									this.frases();
																																																																								}
																																																																								else
																																																																								{
																																																																									this.frasesresp();
																																																																								}
																																																																								this.endSearchCmd();
																																																																							}
																																																																							else
																																																																							{
																																																																								bool flag366 = _speech == "avaçar para dez por cento" || _speech == "colocar video em dez por cento";
																																																																								if (flag366)
																																																																								{
																																																																									bool flag367 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																									if (flag367)
																																																																									{
																																																																										InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_1);
																																																																										this.frases();
																																																																									}
																																																																									else
																																																																									{
																																																																										this.frasesresp();
																																																																									}
																																																																									this.endSearchCmd();
																																																																								}
																																																																								else
																																																																								{
																																																																									bool flag368 = _speech == "avaçar para vinte por cento" || _speech == "colocar video em vinte por cento";
																																																																									if (flag368)
																																																																									{
																																																																										bool flag369 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																										if (flag369)
																																																																										{
																																																																											InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_2);
																																																																											this.frases();
																																																																										}
																																																																										else
																																																																										{
																																																																											this.frasesresp();
																																																																										}
																																																																										this.endSearchCmd();
																																																																									}
																																																																									else
																																																																									{
																																																																										bool flag370 = _speech == "avaçar para trinta por cento" || _speech == "colocar video em trinta por cento";
																																																																										if (flag370)
																																																																										{
																																																																											bool flag371 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																											if (flag371)
																																																																											{
																																																																												InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_3);
																																																																												this.frases();
																																																																											}
																																																																											else
																																																																											{
																																																																												this.frasesresp();
																																																																											}
																																																																											this.endSearchCmd();
																																																																										}
																																																																										else
																																																																										{
																																																																											bool flag372 = _speech == "avaçar para quarenta por cento" || _speech == "colocar video em quarenta por cento";
																																																																											if (flag372)
																																																																											{
																																																																												bool flag373 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																												if (flag373)
																																																																												{
																																																																													InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_4);
																																																																													this.frases();
																																																																												}
																																																																												else
																																																																												{
																																																																													this.frasesresp();
																																																																												}
																																																																												this.endSearchCmd();
																																																																											}
																																																																											else
																																																																											{
																																																																												bool flag374 = _speech == "avaçar para ciquenta por cento" || _speech == "colocar video em ciquenta por cento";
																																																																												if (flag374)
																																																																												{
																																																																													bool flag375 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																													if (flag375)
																																																																													{
																																																																														InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_5);
																																																																														this.frases();
																																																																													}
																																																																													else
																																																																													{
																																																																														this.frasesresp();
																																																																													}
																																																																													this.endSearchCmd();
																																																																												}
																																																																												else
																																																																												{
																																																																													bool flag376 = _speech == "avaçar para sessenta por cento" || _speech == "colocar video em sessenta por cento";
																																																																													if (flag376)
																																																																													{
																																																																														bool flag377 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																														if (flag377)
																																																																														{
																																																																															InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_6);
																																																																															this.frases();
																																																																														}
																																																																														else
																																																																														{
																																																																															this.frasesresp();
																																																																														}
																																																																														this.endSearchCmd();
																																																																													}
																																																																													else
																																																																													{
																																																																														bool flag378 = _speech == "avaçar para setenta por cento" || _speech == "colocar video em setenta por cento";
																																																																														if (flag378)
																																																																														{
																																																																															bool flag379 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																															if (flag379)
																																																																															{
																																																																																InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_7);
																																																																																this.frases();
																																																																															}
																																																																															else
																																																																															{
																																																																																this.frasesresp();
																																																																															}
																																																																															this.endSearchCmd();
																																																																														}
																																																																														else
																																																																														{
																																																																															bool flag380 = _speech == "avaçar para oitenta por cento" || _speech == "colocar video em oitenta por cento";
																																																																															if (flag380)
																																																																															{
																																																																																bool flag381 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																if (flag381)
																																																																																{
																																																																																	InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_8);
																																																																																	this.frases();
																																																																																}
																																																																																else
																																																																																{
																																																																																	this.frasesresp();
																																																																																}
																																																																																this.endSearchCmd();
																																																																															}
																																																																															else
																																																																															{
																																																																																bool flag382 = _speech == "avaçar para noventa por cento" || _speech == "colocar video em noventa por cento";
																																																																																if (flag382)
																																																																																{
																																																																																	bool flag383 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																	if (flag383)
																																																																																	{
																																																																																		InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_9);
																																																																																		this.frases();
																																																																																	}
																																																																																	else
																																																																																	{
																																																																																		this.frasesresp();
																																																																																	}
																																																																																	this.endSearchCmd();
																																																																																}
																																																																																else
																																																																																{
																																																																																	bool flag384 = _speech == "acelerar velocidade de reproduçao" || _speech == "almentar velocindade do video";
																																																																																	if (flag384)
																																																																																	{
																																																																																		bool flag385 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																		if (flag385)
																																																																																		{
																																																																																			SendKeys.SendWait("+{>}");
																																																																																			this.frases();
																																																																																		}
																																																																																		else
																																																																																		{
																																																																																			this.frasesresp();
																																																																																		}
																																																																																		this.endSearchCmd();
																																																																																	}
																																																																																	else
																																																																																	{
																																																																																		bool flag386 = _speech == "avança dez segundos" || _speech == "avança video dez segundos";
																																																																																		if (flag386)
																																																																																		{
																																																																																			bool flag387 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																			if (flag387)
																																																																																			{
																																																																																				InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_L);
																																																																																				this.frases();
																																																																																			}
																																																																																			else
																																																																																			{
																																																																																				this.frasesresp();
																																																																																			}
																																																																																			this.endSearchCmd();
																																																																																		}
																																																																																		else
																																																																																		{
																																																																																			bool flag388 = _speech == "coloca o vídeo em modo tela cheia" || _speech == "video em tela cheia" || _speech == "video em tela normal";
																																																																																			if (flag388)
																																																																																			{
																																																																																				bool flag389 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																				if (flag389)
																																																																																				{
																																																																																					InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_F);
																																																																																					this.frases();
																																																																																				}
																																																																																				else
																																																																																				{
																																																																																					this.frasesresp();
																																																																																				}
																																																																																				this.endSearchCmd();
																																																																																			}
																																																																																			else
																																																																																			{
																																																																																				bool flag390 = _speech == "desativar audio do vídeo" || _speech == "restaurar volume do video" || _speech == "reativar audio do video";
																																																																																				if (flag390)
																																																																																				{
																																																																																					bool flag391 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																					if (flag391)
																																																																																					{
																																																																																						InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_M);
																																																																																						this.frases();
																																																																																					}
																																																																																					else
																																																																																					{
																																																																																						this.frasesresp();
																																																																																					}
																																																																																					this.endSearchCmd();
																																																																																				}
																																																																																				else
																																																																																				{
																																																																																					bool flag392 = _speech == "dar play no video" || _speech == "pausar o video" || _speech == "dar o play no video" || _speech == "pausar video";
																																																																																					if (flag392)
																																																																																					{
																																																																																						bool flag393 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																						if (flag393)
																																																																																						{
																																																																																							InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_K);
																																																																																							this.frases();
																																																																																						}
																																																																																						else
																																																																																						{
																																																																																							this.frasesresp();
																																																																																						}
																																																																																						this.endSearchCmd();
																																																																																					}
																																																																																					else
																																																																																					{
																																																																																						bool flag394 = _speech == "ligar legendas" || _speech == "ligar legendas do video" || _speech == "ativar legendas";
																																																																																						if (flag394)
																																																																																						{
																																																																																							bool flag395 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																							if (flag395)
																																																																																							{
																																																																																								InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_C);
																																																																																								this.frases();
																																																																																							}
																																																																																							else
																																																																																							{
																																																																																								this.frasesresp();
																																																																																							}
																																																																																							this.endSearchCmd();
																																																																																						}
																																																																																						else
																																																																																						{
																																																																																							bool flag396 = _speech == "sair do modo tela cheia" || _speech == "tirar o video da tela cheia";
																																																																																							if (flag396)
																																																																																							{
																																																																																								bool flag397 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																								if (flag397)
																																																																																								{
																																																																																									InputSimulator.SimulateKeyPress(VirtualKeyCode.ESCAPE);
																																																																																									this.frases();
																																																																																								}
																																																																																								else
																																																																																								{
																																																																																									this.frasesresp();
																																																																																								}
																																																																																								this.endSearchCmd();
																																																																																							}
																																																																																							else
																																																																																							{
																																																																																								bool flag398 = _speech == "Retrocede dez segundos" || _speech == "Retrocede o video em dez segundos";
																																																																																								if (flag398)
																																																																																								{
																																																																																									bool flag399 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																									if (flag399)
																																																																																									{
																																																																																										InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_J);
																																																																																										this.frases();
																																																																																									}
																																																																																									else
																																																																																									{
																																																																																										this.frasesresp();
																																																																																									}
																																																																																									this.endSearchCmd();
																																																																																								}
																																																																																								else
																																																																																								{
																																																																																									bool flag400 = _speech == "reduzi velocidade de reproduçao" || _speech == "diminuir velocidade do video";
																																																																																									if (flag400)
																																																																																									{
																																																																																										bool flag401 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																										if (flag401)
																																																																																										{
																																																																																											SendKeys.SendWait("+{<}");
																																																																																											this.frases();
																																																																																										}
																																																																																										else
																																																																																										{
																																																																																											this.frasesresp();
																																																																																										}
																																																																																										this.endSearchCmd();
																																																																																									}
																																																																																									else
																																																																																									{
																																																																																										bool flag402 = _speech == "ir para o início do video" || _speech == "colocar video no início";
																																																																																										if (flag402)
																																																																																										{
																																																																																											bool flag403 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																											if (flag403)
																																																																																											{
																																																																																												InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_0);
																																																																																												this.frases();
																																																																																											}
																																																																																											else
																																																																																											{
																																																																																												this.frasesresp();
																																																																																											}
																																																																																											this.endSearchCmd();
																																																																																										}
																																																																																										else
																																																																																										{
																																																																																											bool flag404 = _speech == "trocar de usuario";
																																																																																											if (flag404)
																																																																																											{
																																																																																												bool flag405 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																												if (flag405)
																																																																																												{
																																																																																													SendKeys.SendWait("+^{M}");
																																																																																													this.SpeakOut(new string[]
																																																																																													{
																																																																																														"FAÇA longui na sua conta ou navegue como visitante "
																																																																																													});
																																																																																												}
																																																																																												else
																																																																																												{
																																																																																													this.frasesresp();
																																																																																												}
																																																																																												this.endSearchCmd();
																																																																																											}
																																																																																											else
																																																																																											{
																																																																																												bool flag406 = _speech == "criar uma nova pasta" || _speech == "nova pasta" || _speech == "crie uma pasta" || _speech == "criar uma pasta";
																																																																																												if (flag406)
																																																																																												{
																																																																																													SendKeys.SendWait("^+{N}");
																																																																																													this.frases();
																																																																																													this.endSearchCmd();
																																																																																												}
																																																																																												else
																																																																																												{
																																																																																													bool flag407 = _speech == "Arrasta janela para esquerda" || _speech == "janela para esquerda" || _speech == "Arrasta mais para esquerda" || _speech == "Arrasta para esquerda";
																																																																																													if (flag407)
																																																																																													{
																																																																																														InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.LEFT);
																																																																																														Thread.Sleep(100);
																																																																																														this.frases();
																																																																																														InputSimulator.SimulateKeyPress(VirtualKeyCode.ESCAPE);
																																																																																														this.endSearchCmd();
																																																																																													}
																																																																																													else
																																																																																													{
																																																																																														bool flag408 = _speech == "Arrasta janela para Direita" || _speech == "janela para Direita" || _speech == "Arrasta mais para Direita" || _speech == "Arrasta para Direita";
																																																																																														if (flag408)
																																																																																														{
																																																																																															InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.RIGHT);
																																																																																															Thread.Sleep(100);
																																																																																															this.frases();
																																																																																															InputSimulator.SimulateKeyPress(VirtualKeyCode.ESCAPE);
																																																																																															this.endSearchCmd();
																																																																																														}
																																																																																														else
																																																																																														{
																																																																																															bool flag409 = _speech == "Arrasta janela para cima" || _speech == "janela para cima" || _speech == "Arrasta mais para cima" || _speech == "Arrasta para cima";
																																																																																															if (flag409)
																																																																																															{
																																																																																																InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.UP);
																																																																																																this.frases();
																																																																																																this.endSearchCmd();
																																																																																															}
																																																																																															else
																																																																																															{
																																																																																																bool flag410 = _speech == "Arrasta janela para baixo" || _speech == "janela para baixo" || _speech == "Arrasta mais para baixo" || _speech == "Arrasta para baixo";
																																																																																																if (flag410)
																																																																																																{
																																																																																																	InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.DOWN);
																																																																																																	Thread.Sleep(100);
																																																																																																	this.frases();
																																																																																																	InputSimulator.SimulateKeyPress(VirtualKeyCode.ESCAPE);
																																																																																																	this.endSearchCmd();
																																																																																																}
																																																																																																else
																																																																																																{
																																																																																																	bool flag411 = _speech == "Percorrer Aplicativo" || _speech == "Percorre o Aplicativo" || _speech == "Percorrer barra de tarefas" || _speech == "Percorre a barra de tarefas";
																																																																																																	if (flag411)
																																																																																																	{
																																																																																																		InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
																																																																																																		this.frases();
																																																																																																		this.endSearchCmd();
																																																																																																	}
																																																																																																	else
																																																																																																	{
																																																																																																		bool flag412 = _speech == "Mundar idioma" || _speech == "Trocar de idioma" || _speech == "mundar o idioma do teclado" || _speech == "trocar o idioma do teclado";
																																																																																																		if (flag412)
																																																																																																		{
																																																																																																			InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.SPACE);
																																																																																																			this.frases();
																																																																																																			this.endSearchCmd();
																																																																																																		}
																																																																																																		else
																																																																																																		{
																																																																																																			bool flag413 = _speech == "emojis" || _speech == "selecionar um emoji" || _speech == "Abrir emoji" || _speech == "fechar emoji" || _speech == "abrir emojis";
																																																																																																			if (flag413)
																																																																																																			{
																																																																																																				InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.OEM_PERIOD);
																																																																																																				this.frases();
																																																																																																				this.endSearchCmd();
																																																																																																			}
																																																																																																			else
																																																																																																			{
																																																																																																				bool flag414 = _speech == "acessar link rápido" || _speech == "link rápido" || _speech == "Abrir o menu de link rápido" || _speech == "menu de link rápido" || _speech == "menu link rápido";
																																																																																																				if (flag414)
																																																																																																				{
																																																																																																					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_X);
																																																																																																					this.frases();
																																																																																																					this.endSearchCmd();
																																																																																																				}
																																																																																																				else
																																																																																																				{
																																																																																																					bool flag415 = _speech == "Abrir Central de Facilidade" || _speech == "Central de Facilidade de Acesso" || _speech == "Central de Facilidade" || _speech == "Abrir Central de Facilidade de Acesso" || _speech == "Facilidade de Acesso";
																																																																																																					if (flag415)
																																																																																																					{
																																																																																																						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_U);
																																																																																																						this.frases();
																																																																																																						this.endSearchCmd();
																																																																																																					}
																																																																																																					else
																																																																																																					{
																																																																																																						bool flag416 = _speech == "Abrir pesquisa" || _speech == "Acessar pesquisa" || _speech == "Abrir pesquisa do windows" || _speech == "acessar pesquisa do windows" || _speech == "abrir caixa de pesquisa do windows";
																																																																																																						if (flag416)
																																																																																																						{
																																																																																																							InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_S);
																																																																																																							this.frases();
																																																																																																							this.endSearchCmd();
																																																																																																						}
																																																																																																						else
																																																																																																						{
																																																																																																							bool flag417 = _speech == "Abrir caixa de diálogo" || _speech == "caixa de diálogo" || _speech == "Abrir Executar" || _speech == "Executar" || _speech == "Abrir a caixa de diálogo Executar";
																																																																																																							if (flag417)
																																																																																																							{
																																																																																																								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_R);
																																																																																																								this.frases();
																																																																																																								this.endSearchCmd();
																																																																																																							}
																																																																																																							else
																																																																																																							{
																																																																																																								bool flag418 = _speech == "Abrir propriedades" || _speech == "Abrir propriedades do sistema" || _speech == "propriedades do sistema";
																																																																																																								if (flag418)
																																																																																																								{
																																																																																																									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.PAUSE);
																																																																																																									this.frases();
																																																																																																									this.endSearchCmd();
																																																																																																								}
																																																																																																								else
																																																																																																								{
																																																																																																									bool flag419 = _speech == "Abrir as Configurações do windows" || _speech == "configuração do windows" || _speech == "janela de configuração" || _speech == "abrir janela de configuração" || _speech == "configuração";
																																																																																																									if (flag419)
																																																																																																									{
																																																																																																										InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_I);
																																																																																																										this.frases();
																																																																																																										this.endSearchCmd();
																																																																																																									}
																																																																																																									else
																																																																																																									{
																																																																																																										bool flag420 = _speech == "Abrir o Explorador de Arquivos" || _speech == "Explorador de Arquivos" || _speech == "Executar o Explorador de Arquivos" || _speech == "Acessar o Explorador de Arquivos" || _speech == "Explorar Arquivos";
																																																																																																										if (flag420)
																																																																																																										{
																																																																																																											InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_E);
																																																																																																											this.frases();
																																																																																																											this.endSearchCmd();
																																																																																																										}
																																																																																																										else
																																																																																																										{
																																																																																																											bool flag421 = _speech == "área de notificação" || _speech == "foco na área de notificação" || _speech == "Definir o foco na área de notificação" || _speech == "Selecionar área de notificação" || _speech == "Selecionar noficações";
																																																																																																											if (flag421)
																																																																																																											{
																																																																																																												InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_B);
																																																																																																												this.frases();
																																																																																																												this.endSearchCmd();
																																																																																																											}
																																																																																																											else
																																																																																																											{
																																																																																																												bool flag422 = _speech == "Abrir Central de ações" || _speech == "Abrir a Central de ações" || _speech == "Central de ações" || _speech == "Acessar Central de ações" || _speech == "ver a Central de ações";
																																																																																																												if (flag422)
																																																																																																												{
																																																																																																													InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_A);
																																																																																																													this.frases();
																																																																																																													this.endSearchCmd();
																																																																																																												}
																																																																																																												else
																																																																																																												{
																																																																																																													bool flag423 = _speech == "deletar" || _speech == "deletar a pasta" || _speech == "deletar o arquivo";
																																																																																																													if (flag423)
																																																																																																													{
																																																																																																														InputSimulator.SimulateKeyPress(VirtualKeyCode.DELETE);
																																																																																																														this.frases();
																																																																																																														this.endSearchCmd();
																																																																																																													}
																																																																																																													else
																																																																																																													{
																																																																																																														bool flag424 = _speech == "deletar permanentemente" || _speech == "excluir permanentemente" || _speech == "deletar pasta permanentemente";
																																																																																																														if (flag424)
																																																																																																														{
																																																																																																															SendKeys.SendWait("+{DEL}");
																																																																																																															this.frases();
																																																																																																															this.endSearchCmd();
																																																																																																														}
																																																																																																														else
																																																																																																														{
																																																																																																															bool flag425 = _speech == "correr um aplicativo" || _speech == "selecionar proximo aplicativo";
																																																																																																															if (flag425)
																																																																																																															{
																																																																																																																int num;
																																																																																																																for (int num36 = 0; num36 < 1; num36 = num + 1)
																																																																																																																{
																																																																																																																	InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
																																																																																																																	Thread.Sleep(50);
																																																																																																																	num = num36;
																																																																																																																}
																																																																																																																this.SpeakOut(new string[]
																																																																																																																{
																																																																																																																	"percorrendo um aplicativo",
																																																																																																																	"selecionado mais um aplicativo",
																																																																																																																	"percorrendo mais um aplicativo da barra de tarefa"
																																																																																																																});
																																																																																																																this.endSearchCmd();
																																																																																																															}
																																																																																																															else
																																																																																																															{
																																																																																																																bool flag426 = _speech == "correr dois aplicativos" || _speech == "selecionar mais dois aplicativos";
																																																																																																																if (flag426)
																																																																																																																{
																																																																																																																	int num;
																																																																																																																	for (int num37 = 0; num37 < 2; num37 = num + 1)
																																																																																																																	{
																																																																																																																		InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
																																																																																																																		Thread.Sleep(50);
																																																																																																																		num = num37;
																																																																																																																	}
																																																																																																																	this.SpeakOut(new string[]
																																																																																																																	{
																																																																																																																		"percorrendo dois aplicativos",
																																																																																																																		"selecionado mais dois aplicativos",
																																																																																																																		"percorrendo mais dois aplicativos da barra de tarefa"
																																																																																																																	});
																																																																																																																	this.endSearchCmd();
																																																																																																																}
																																																																																																																else
																																																																																																																{
																																																																																																																	bool flag427 = _speech == "correr três aplicativos" || _speech == "pula mais três aplicativos";
																																																																																																																	if (flag427)
																																																																																																																	{
																																																																																																																		int num;
																																																																																																																		for (int num38 = 0; num38 < 3; num38 = num + 1)
																																																																																																																		{
																																																																																																																			InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
																																																																																																																			Thread.Sleep(50);
																																																																																																																			num = num38;
																																																																																																																		}
																																																																																																																		this.SpeakOut(new string[]
																																																																																																																		{
																																																																																																																			"percorrendo três aplicativos",
																																																																																																																			"selecionado mais três aplicativos",
																																																																																																																			"percorrendo mais três aplicativos da barra de tarefas"
																																																																																																																		});
																																																																																																																		this.endSearchCmd();
																																																																																																																	}
																																																																																																																	else
																																																																																																																	{
																																																																																																																		bool flag428 = _speech == "correr quatro aplicativos" || _speech == "pula mais quatro aplicativos";
																																																																																																																		if (flag428)
																																																																																																																		{
																																																																																																																			int num;
																																																																																																																			for (int num39 = 0; num39 < 4; num39 = num + 1)
																																																																																																																			{
																																																																																																																				InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
																																																																																																																				Thread.Sleep(50);
																																																																																																																				num = num39;
																																																																																																																			}
																																																																																																																			this.SpeakOut(new string[]
																																																																																																																			{
																																																																																																																				"percorrendo quatro aplicativos",
																																																																																																																				"selecionado mais quatro aplicativos",
																																																																																																																				"percorrendo mais quatro aplicativos da barra de tarefas"
																																																																																																																			});
																																																																																																																			this.endSearchCmd();
																																																																																																																		}
																																																																																																																		else
																																																																																																																		{
																																																																																																																			bool flag429 = _speech == "correr cinco aplicativos" || _speech == "pula mais cinco aplicativos";
																																																																																																																			if (flag429)
																																																																																																																			{
																																																																																																																				int num;
																																																																																																																				for (int num40 = 0; num40 < 5; num40 = num + 1)
																																																																																																																				{
																																																																																																																					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_T);
																																																																																																																					Thread.Sleep(50);
																																																																																																																					num = num40;
																																																																																																																				}
																																																																																																																				this.SpeakOut(new string[]
																																																																																																																				{
																																																																																																																					"percorrendo cinco aplicativos",
																																																																																																																					"selecionado mais cinco aplicativos",
																																																																																																																					"percorrendo mais cinco aplicativos da barra de tarefas"
																																																																																																																				});
																																																																																																																				this.endSearchCmd();
																																																																																																																			}
																																																																																																																			else
																																																																																																																			{
																																																																																																																				bool flag430 = _speech == "abrir aplicativo um" || _speech == "selecionar aplicativo um" || _speech == "selecionar o primeiro aplicativo" || _speech == "selecione o aplicativo um";
																																																																																																																				if (flag430)
																																																																																																																				{
																																																																																																																					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_1);
																																																																																																																					this.SpeakOut(new string[]
																																																																																																																					{
																																																																																																																						"aplicativo um foi selecionado ",
																																																																																																																						"o primeiro aplicativo foi selecionado "
																																																																																																																					});
																																																																																																																					this.endSearchCmd();
																																																																																																																				}
																																																																																																																				else
																																																																																																																				{
																																																																																																																					bool flag431 = _speech == "abrir aplicativo dois" || _speech == "selecionar aplicativo dois" || _speech == "selecionar o segundo aplicativo" || _speech == "selecione o aplicativo dois";
																																																																																																																					if (flag431)
																																																																																																																					{
																																																																																																																						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_2);
																																																																																																																						this.SpeakOut(new string[]
																																																																																																																						{
																																																																																																																							"Aplicativo dois foi selecionado ",
																																																																																																																							"o segundo aplicativo foi selecionado "
																																																																																																																						});
																																																																																																																						this.endSearchCmd();
																																																																																																																					}
																																																																																																																					else
																																																																																																																					{
																																																																																																																						bool flag432 = _speech == "abrir aplicativo três" || _speech == "selecionar aplicativo três" || _speech == "selecionar o terceiro aplicativo" || _speech == "selecione o aplicativo três";
																																																																																																																						if (flag432)
																																																																																																																						{
																																																																																																																							InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_3);
																																																																																																																							this.SpeakOut(new string[]
																																																																																																																							{
																																																																																																																								"Aplicativo três foi selecionado ",
																																																																																																																								"o terceiro aplicativo foi selecionado "
																																																																																																																							});
																																																																																																																							this.endSearchCmd();
																																																																																																																						}
																																																																																																																						else
																																																																																																																						{
																																																																																																																							bool flag433 = _speech == "abrir aplicativo quatro" || _speech == "selecionar aplicativo quatro" || _speech == "selecionar o quarto aplicativo" || _speech == "selecione o aplicativo quatro";
																																																																																																																							if (flag433)
																																																																																																																							{
																																																																																																																								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_4);
																																																																																																																								this.SpeakOut(new string[]
																																																																																																																								{
																																																																																																																									"Aplicativo quatro foi selecionado ",
																																																																																																																									"o quarto aplicativo foi selecionado "
																																																																																																																								});
																																																																																																																								this.endSearchCmd();
																																																																																																																							}
																																																																																																																							else
																																																																																																																							{
																																																																																																																								bool flag434 = _speech == "abrir aplicativo cinco" || _speech == "selecionar aplicativo cinco" || _speech == "selecionar o quinto aplicativo" || _speech == "selecione o aplicativo cinco";
																																																																																																																								if (flag434)
																																																																																																																								{
																																																																																																																									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_5);
																																																																																																																									this.SpeakOut(new string[]
																																																																																																																									{
																																																																																																																										"Aplicativo cinco foi selecionado ",
																																																																																																																										"o quinto aplicativo foi selecionado "
																																																																																																																									});
																																																																																																																									this.endSearchCmd();
																																																																																																																								}
																																																																																																																								else
																																																																																																																								{
																																																																																																																									bool flag435 = _speech == "abrir aplicativo seis" || _speech == "selecionar aplicativo seis" || _speech == "selecionar o sesto aplicativo" || _speech == "selecione o aplicativo seis";
																																																																																																																									if (flag435)
																																																																																																																									{
																																																																																																																										InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_6);
																																																																																																																										this.SpeakOut(new string[]
																																																																																																																										{
																																																																																																																											"Aplicativo seis foi selecionado ",
																																																																																																																											"o sesto aplicativo foi selecionado "
																																																																																																																										});
																																																																																																																										this.endSearchCmd();
																																																																																																																									}
																																																																																																																									else
																																																																																																																									{
																																																																																																																										bool flag436 = _speech == "abrir aplicativo sete" || _speech == "selecionar aplicativo sete" || _speech == "selecionar o setimo aplicativo" || _speech == "selecione o aplicativo sete";
																																																																																																																										if (flag436)
																																																																																																																										{
																																																																																																																											InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_7);
																																																																																																																											this.SpeakOut(new string[]
																																																																																																																											{
																																																																																																																												"Aplicativo sete foi selecionado ",
																																																																																																																												"o setimo aplicativo foi selecionado "
																																																																																																																											});
																																																																																																																											this.endSearchCmd();
																																																																																																																										}
																																																																																																																										else
																																																																																																																										{
																																																																																																																											bool flag437 = _speech == "abrir aplicativo oito" || _speech == "selecionar aplicativo oito" || _speech == "selecionar o oitavo aplicativo" || _speech == "selecione o aplicativo oito";
																																																																																																																											if (flag437)
																																																																																																																											{
																																																																																																																												InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_8);
																																																																																																																												this.SpeakOut(new string[]
																																																																																																																												{
																																																																																																																													"Aplicativo oito foi selecionado ",
																																																																																																																													"o oitavo aplicativo foi selecionado"
																																																																																																																												});
																																																																																																																												this.endSearchCmd();
																																																																																																																											}
																																																																																																																											else
																																																																																																																											{
																																																																																																																												bool flag438 = _speech == "abrir aplicativo nove" || _speech == "selecionar aplicativo nove" || _speech == "selecionar o nono aplicativo" || _speech == "selecione o aplicativo nove";
																																																																																																																												if (flag438)
																																																																																																																												{
																																																																																																																													InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_9);
																																																																																																																													this.SpeakOut(new string[]
																																																																																																																													{
																																																																																																																														"Aplicativo nove foi selecionado ",
																																																																																																																														"o nono aplicativo foi selecionado "
																																																																																																																													});
																																																																																																																													this.endSearchCmd();
																																																																																																																												}
																																																																																																																												else
																																																																																																																												{
																																																																																																																													bool flag439 = _speech == "Não me avise sobre e-mails" || _speech == "desativar avisos" || _speech == "desativar aviso do e-mail";
																																																																																																																													if (flag439)
																																																																																																																													{
																																																																																																																														bool flag440 = Settings.Default.checkGmail || Settings.Default.checkOutlook || Settings.Default.checkYahoo || Settings.Default.checkEmail;
																																																																																																																														if (flag440)
																																																																																																																														{
																																																																																																																															this.clearcuetas();
																																																																																																																															this.SpeakOut(new string[]
																																																																																																																															{
																																																																																																																																"aviso de contas desabilitado ",
																																																																																																																																"não te avisarei sobre e-mails "
																																																																																																																															});
																																																																																																																														}
																																																																																																																														else
																																																																																																																														{
																																																																																																																															this.SpeakOut(new string[]
																																																																																																																															{
																																																																																																																																"o aviso já foi desabilitado "
																																																																																																																															});
																																																																																																																														}
																																																																																																																														this.endSearchCmd();
																																																																																																																													}
																																																																																																																													else
																																																																																																																													{
																																																																																																																														bool flag441 = _speech == "me avise se chegar e-mail" || _speech == "ativar avisos de Email" || _speech == "ativar aviso do e-mail";
																																																																																																																														if (flag441)
																																																																																																																														{
																																																																																																																															bool flag442 = !Settings.Default.checkEmail;
																																																																																																																															if (flag442)
																																																																																																																															{
																																																																																																																																Settings.Default.checkEmail = (this.cont.cbEmail.IsChecked != null && this.cont.cbEmail.IsChecked.Value);
																																																																																																																																bool flag443 = !Settings.Default.checkEmail && !Settings.Default.cntEmailOK;
																																																																																																																																if (flag443)
																																																																																																																																{
																																																																																																																																	this.cont.cbEmail.IsChecked = new bool?(false);
																																																																																																																																	Settings.Default.checkEmail = false;
																																																																																																																																	this.SpeakOut(new string[]
																																																																																																																																	{
																																																																																																																																		"Certifique que a conta foi configurada ",
																																																																																																																																		"a conta não está configurada ",
																																																																																																																																		Settings.Default.cfgUser + "não tem nem uma conta configurada "
																																																																																																																																	});
																																																																																																																																}
																																																																																																																																else
																																																																																																																																{
																																																																																																																																	this.setcuetasEmail();
																																																																																																																																	this.SpeakOut(new string[]
																																																																																																																																	{
																																																																																																																																		"aviso das contas de correios eletrónicos foi habilitado ",
																																																																																																																																		"Te avisarei se chegar correios eletrónicos "
																																																																																																																																	});
																																																																																																																																}
																																																																																																																															}
																																																																																																																															else
																																																																																																																															{
																																																																																																																																this.SpeakOut(new string[]
																																																																																																																																{
																																																																																																																																	"o aviso já está abilitado ",
																																																																																																																																	"o aviso já foi abilitado ",
																																																																																																																																	"o aviso já foi abilitado " + Settings.Default.cfgUser,
																																																																																																																																	"o aviso já está abilitado " + Settings.Default.cfgUser
																																																																																																																																});
																																																																																																																															}
																																																																																																																															this.endSearchCmd();
																																																																																																																														}
																																																																																																																														else
																																																																																																																														{
																																																																																																																															bool flag444 = _speech == "me avise se chegar e-mail do yahoo" || _speech == "ativar avisos de Email do yahoo" || _speech == "ativar aviso do yahoo";
																																																																																																																															if (flag444)
																																																																																																																															{
																																																																																																																																bool flag445 = !Settings.Default.checkYahoo;
																																																																																																																																if (flag445)
																																																																																																																																{
																																																																																																																																	Settings.Default.checkYahoo = (this.cont.cbYahoo.IsChecked != null && this.cont.cbYahoo.IsChecked.Value);
																																																																																																																																	bool flag446 = !Settings.Default.checkYahoo && !Settings.Default.cntYahooOK;
																																																																																																																																	if (flag446)
																																																																																																																																	{
																																																																																																																																		this.cont.cbYahoo.IsChecked = new bool?(false);
																																																																																																																																		Settings.Default.checkYahoo = false;
																																																																																																																																		this.SpeakOut(new string[]
																																																																																																																																		{
																																																																																																																																			"Certifique que a conta foi configurada ",
																																																																																																																																			"a conta não está configurada ",
																																																																																																																																			Settings.Default.cfgUser + "não tem nem uma conta configurada "
																																																																																																																																		});
																																																																																																																																	}
																																																																																																																																	else
																																																																																																																																	{
																																																																																																																																		this.setcuetasYahoo();
																																																																																																																																		this.SpeakOut(new string[]
																																																																																																																																		{
																																																																																																																																			"aviso das contas de correios eletrónicos do yahoo foi habilitado ",
																																																																																																																																			"Te avisarei se chegar correios eletrónicos do yahoo "
																																																																																																																																		});
																																																																																																																																	}
																																																																																																																																}
																																																																																																																																else
																																																																																																																																{
																																																																																																																																	this.SpeakOut(new string[]
																																																																																																																																	{
																																																																																																																																		"o aviso já está abilitado ",
																																																																																																																																		"o aviso já foi abilitado ",
																																																																																																																																		"o aviso já foi abilitado " + Settings.Default.cfgUser,
																																																																																																																																		"o aviso já está abilitado " + Settings.Default.cfgUser
																																																																																																																																	});
																																																																																																																																}
																																																																																																																																this.endSearchCmd();
																																																																																																																															}
																																																																																																																															else
																																																																																																																															{
																																																																																																																																bool flag447 = _speech == "me avise se chegar e-mail do Outlook" || _speech == "ativar avisos de Email do Outlook" || _speech == "ativar aviso do Outlook";
																																																																																																																																if (flag447)
																																																																																																																																{
																																																																																																																																	bool flag448 = !Settings.Default.checkOutlook;
																																																																																																																																	if (flag448)
																																																																																																																																	{
																																																																																																																																		Settings.Default.checkOutlook = (this.cont.cbOutlook.IsChecked != null && this.cont.cbOutlook.IsChecked.Value);
																																																																																																																																		bool flag449 = !Settings.Default.checkOutlook && !Settings.Default.cntOulookOK;
																																																																																																																																		if (flag449)
																																																																																																																																		{
																																																																																																																																			this.cont.cbOutlook.IsChecked = new bool?(false);
																																																																																																																																			Settings.Default.checkOutlook = false;
																																																																																																																																			this.SpeakOut(new string[]
																																																																																																																																			{
																																																																																																																																				"Certifique que a conta foi configurada ",
																																																																																																																																				"a conta não está configurada ",
																																																																																																																																				Settings.Default.cfgUser + "não tem nem uma conta configurada "
																																																																																																																																			});
																																																																																																																																		}
																																																																																																																																		else
																																																																																																																																		{
																																																																																																																																			this.setcuetasOutlook();
																																																																																																																																			this.SpeakOut(new string[]
																																																																																																																																			{
																																																																																																																																				"aviso das contas de correios eletrónicos do outlook foi habilitado ",
																																																																																																																																				"Te avisarei se chegar correios eletrónicos do outlook "
																																																																																																																																			});
																																																																																																																																		}
																																																																																																																																	}
																																																																																																																																	else
																																																																																																																																	{
																																																																																																																																		this.SpeakOut(new string[]
																																																																																																																																		{
																																																																																																																																			"o aviso já está abilitado ",
																																																																																																																																			"o aviso já foi abilitado ",
																																																																																																																																			"o aviso já foi abilitado " + Settings.Default.cfgUser,
																																																																																																																																			"o aviso já está abilitado " + Settings.Default.cfgUser
																																																																																																																																		});
																																																																																																																																	}
																																																																																																																																	this.endSearchCmd();
																																																																																																																																}
																																																																																																																																else
																																																																																																																																{
																																																																																																																																	bool flag450 = _speech == "me avise se chegar e-mail do gmail" || _speech == "ativar avisos de Email do gmail" || _speech == "ativar aviso do gmail";
																																																																																																																																	if (flag450)
																																																																																																																																	{
																																																																																																																																		bool flag451 = !Settings.Default.checkGmail;
																																																																																																																																		if (flag451)
																																																																																																																																		{
																																																																																																																																			Settings.Default.checkGmail = (this.cont.cbGmail.IsChecked != null && this.cont.cbGmail.IsChecked.Value);
																																																																																																																																			bool flag452 = !Settings.Default.cntGmailOK && !Settings.Default.cntGmailAtomOK;
																																																																																																																																			if (flag452)
																																																																																																																																			{
																																																																																																																																				this.cont.cbGmail.IsChecked = new bool?(false);
																																																																																																																																				Settings.Default.checkGmail = false;
																																																																																																																																				this.SpeakOut(new string[]
																																																																																																																																				{
																																																																																																																																					"Certifique que a conta foi configurada ",
																																																																																																																																					"a conta não está configurada ",
																																																																																																																																					Settings.Default.cfgUser + "não tem nem uma conta configurada "
																																																																																																																																				});
																																																																																																																																			}
																																																																																																																																			else
																																																																																																																																			{
																																																																																																																																				this.setcuetasgermail();
																																																																																																																																				this.SpeakOut(new string[]
																																																																																																																																				{
																																																																																																																																					"aviso das contas de correios eletrónicos do gmail foi habilitado ",
																																																																																																																																					"Te avisarei se chegar correios eletrónicos do gmail "
																																																																																																																																				});
																																																																																																																																			}
																																																																																																																																		}
																																																																																																																																		else
																																																																																																																																		{
																																																																																																																																			this.SpeakOut(new string[]
																																																																																																																																			{
																																																																																																																																				"o aviso já está abilitado ",
																																																																																																																																				"o aviso já foi abilitado ",
																																																																																																																																				"o aviso já foi abilitado " + Settings.Default.cfgUser,
																																																																																																																																				"o aviso já está abilitado " + Settings.Default.cfgUser
																																																																																																																																			});
																																																																																																																																		}
																																																																																																																																		this.endSearchCmd();
																																																																																																																																	}
																																																																																																																																	else
																																																																																																																																	{
																																																																																																																																		bool flag453 = _speech == "fechar a guia um" || _speech == "Fechar guia um";
																																																																																																																																		if (flag453)
																																																																																																																																		{
																																																																																																																																			bool flag454 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																																			if (flag454)
																																																																																																																																			{
																																																																																																																																				InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_1);
																																																																																																																																				Thread.Sleep(100);
																																																																																																																																				InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_W);
																																																																																																																																				this.frases();
																																																																																																																																			}
																																																																																																																																			else
																																																																																																																																			{
																																																																																																																																				this.frasesresp();
																																																																																																																																			}
																																																																																																																																			this.endSearchCmd();
																																																																																																																																		}
																																																																																																																																		else
																																																																																																																																		{
																																																																																																																																			bool flag455 = _speech == "fechar a guia dois" || _speech == "Fechar guia dois";
																																																																																																																																			if (flag455)
																																																																																																																																			{
																																																																																																																																				bool flag456 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																																				if (flag456)
																																																																																																																																				{
																																																																																																																																					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_2);
																																																																																																																																					Thread.Sleep(100);
																																																																																																																																					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_W);
																																																																																																																																					this.frases();
																																																																																																																																				}
																																																																																																																																				else
																																																																																																																																				{
																																																																																																																																					this.frasesresp();
																																																																																																																																				}
																																																																																																																																				this.endSearchCmd();
																																																																																																																																			}
																																																																																																																																			else
																																																																																																																																			{
																																																																																																																																				bool flag457 = _speech == "fechar a guia três" || _speech == "Fechar guia três";
																																																																																																																																				if (flag457)
																																																																																																																																				{
																																																																																																																																					bool flag458 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																																					if (flag458)
																																																																																																																																					{
																																																																																																																																						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_3);
																																																																																																																																						Thread.Sleep(100);
																																																																																																																																						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_W);
																																																																																																																																						this.frases();
																																																																																																																																					}
																																																																																																																																					else
																																																																																																																																					{
																																																																																																																																						this.frasesresp();
																																																																																																																																					}
																																																																																																																																					this.endSearchCmd();
																																																																																																																																				}
																																																																																																																																				else
																																																																																																																																				{
																																																																																																																																					bool flag459 = _speech == "fechar a guia quatro" || _speech == "Fechar guia quatro";
																																																																																																																																					if (flag459)
																																																																																																																																					{
																																																																																																																																						bool flag460 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																																						if (flag460)
																																																																																																																																						{
																																																																																																																																							InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_4);
																																																																																																																																							Thread.Sleep(100);
																																																																																																																																							InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_W);
																																																																																																																																							this.frases();
																																																																																																																																						}
																																																																																																																																						else
																																																																																																																																						{
																																																																																																																																							this.frasesresp();
																																																																																																																																						}
																																																																																																																																						this.endSearchCmd();
																																																																																																																																					}
																																																																																																																																					else
																																																																																																																																					{
																																																																																																																																						bool flag461 = _speech == "fechar a guia cinco" || _speech == "Fechar guia cinco";
																																																																																																																																						if (flag461)
																																																																																																																																						{
																																																																																																																																							bool flag462 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																																							if (flag462)
																																																																																																																																							{
																																																																																																																																								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_5);
																																																																																																																																								Thread.Sleep(100);
																																																																																																																																								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_W);
																																																																																																																																								this.frases();
																																																																																																																																							}
																																																																																																																																							else
																																																																																																																																							{
																																																																																																																																								this.frasesresp();
																																																																																																																																							}
																																																																																																																																							this.endSearchCmd();
																																																																																																																																						}
																																																																																																																																						else
																																																																																																																																						{
																																																																																																																																							bool flag463 = _speech == "fechar a guia seis" || _speech == "Fechar guia seis";
																																																																																																																																							if (flag463)
																																																																																																																																							{
																																																																																																																																								bool flag464 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																																								if (flag464)
																																																																																																																																								{
																																																																																																																																									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_6);
																																																																																																																																									Thread.Sleep(100);
																																																																																																																																									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_W);
																																																																																																																																									this.frases();
																																																																																																																																								}
																																																																																																																																								else
																																																																																																																																								{
																																																																																																																																									this.frasesresp();
																																																																																																																																								}
																																																																																																																																								this.endSearchCmd();
																																																																																																																																							}
																																																																																																																																							else
																																																																																																																																							{
																																																																																																																																								bool flag465 = _speech == "fechar a guia sete" || _speech == "Fechar guia sete";
																																																																																																																																								if (flag465)
																																																																																																																																								{
																																																																																																																																									bool flag466 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																																									if (flag466)
																																																																																																																																									{
																																																																																																																																										InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_7);
																																																																																																																																										Thread.Sleep(100);
																																																																																																																																										InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_W);
																																																																																																																																										this.frases();
																																																																																																																																									}
																																																																																																																																									else
																																																																																																																																									{
																																																																																																																																										this.frasesresp();
																																																																																																																																									}
																																																																																																																																									this.endSearchCmd();
																																																																																																																																								}
																																																																																																																																								else
																																																																																																																																								{
																																																																																																																																									bool flag467 = _speech == "fechar a guia oito" || _speech == "Fechar guia oito";
																																																																																																																																									if (flag467)
																																																																																																																																									{
																																																																																																																																										bool flag468 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																																										if (flag468)
																																																																																																																																										{
																																																																																																																																											InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_8);
																																																																																																																																											Thread.Sleep(100);
																																																																																																																																											InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_W);
																																																																																																																																											this.frases();
																																																																																																																																										}
																																																																																																																																										else
																																																																																																																																										{
																																																																																																																																											this.frasesresp();
																																																																																																																																										}
																																																																																																																																										this.endSearchCmd();
																																																																																																																																									}
																																																																																																																																									else
																																																																																																																																									{
																																																																																																																																										bool flag469 = _speech == "fechar a guia nove" || _speech == "Fechar guia nove";
																																																																																																																																										if (flag469)
																																																																																																																																										{
																																																																																																																																											bool flag470 = User32.nombreProcesoNoExe() == "chrome" || User32.nombreProcesoNoExe() == "firefox" || User32.nombreProcesoNoExe() == "iexplore" || User32.nombreProcesoNoExe() == "opera" || User32.nombreProcesoNoExe() == "msedge";
																																																																																																																																											if (flag470)
																																																																																																																																											{
																																																																																																																																												InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_9);
																																																																																																																																												Thread.Sleep(100);
																																																																																																																																												InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_W);
																																																																																																																																												this.frases();
																																																																																																																																											}
																																																																																																																																											else
																																																																																																																																											{
																																																																																																																																												this.frasesresp();
																																																																																																																																											}
																																																																																																																																											this.endSearchCmd();
																																																																																																																																										}
																																																																																																																																										else
																																																																																																																																										{
																																																																																																																																											bool flag471 = _speech == "Habilitar Desligamento" || _speech == "ativar função de Desligamento" || _speech == "ativar Desligamento" || _speech == "ativar o Desligamento" || _speech == "configura o Desligamento";
																																																																																																																																											if (flag471)
																																																																																																																																											{
																																																																																																																																												this.alarmaSet = true;
																																																																																																																																												this.setDeslig();
																																																																																																																																												this.SpeakOut(new string[]
																																																																																																																																												{
																																																																																																																																													"a que hora quer desligar " + Settings.Default.cfgUser
																																																																																																																																												});
																																																																																																																																												this.lblSpeech2.Content = "Desligar a que Horas ";
																																																																																																																																												this.endSearchCmd();
																																																																																																																																											}
																																																																																																																																											else
																																																																																																																																											{
																																																																																																																																												bool flag472 = _speech == "Habilitar reinicialização" || _speech == "ativar função de reinicializar" || _speech == "ativar reinicio" || _speech == "ativar o reinicio" || _speech == "configura a Reinicialização";
																																																																																																																																												if (flag472)
																																																																																																																																												{
																																																																																																																																													this.alarmaSet = true;
																																																																																																																																													this.setReinicar();
																																																																																																																																													this.SpeakOut(new string[]
																																																																																																																																													{
																																																																																																																																														"a que hora você quer que reinicie? " + Settings.Default.cfgUser
																																																																																																																																													});
																																																																																																																																													this.lblSpeech2.Content = "Reiniciar a que Horas ";
																																																																																																																																													this.endSearchCmd();
																																																																																																																																												}
																																																																																																																																												else
																																																																																																																																												{
																																																																																																																																													bool flag473 = _speech == "Habilitar Suspenção" || _speech == "ativar função de Suspenção" || _speech == "ativar Suspenção" || _speech == "ativar a Suspenção do sistema" || _speech == "configurar Suspenção do sistema";
																																																																																																																																													if (flag473)
																																																																																																																																													{
																																																																																																																																														this.alarmaSet = true;
																																																																																																																																														this.setSuspender();
																																																																																																																																														this.SpeakOut(new string[]
																																																																																																																																														{
																																																																																																																																															"a que hora " + Settings.Default.cfgUser
																																																																																																																																														});
																																																																																																																																														this.lblSpeech2.Content = "Suspender a que Horas ";
																																																																																																																																														this.endSearchCmd();
																																																																																																																																													}
																																																																																																																																													else
																																																																																																																																													{
																																																																																																																																														bool flag474 = _speech == "Habilitar hibernação" || _speech == "ativar função de hibernação" || _speech == "ativar hibernação" || _speech == "ativar a hibernação do sistema" || _speech == "configurar hibernação do sistema";
																																																																																																																																														if (flag474)
																																																																																																																																														{
																																																																																																																																															this.alarmaSet = true;
																																																																																																																																															this.setSesion();
																																																																																																																																															this.SpeakOut(new string[]
																																																																																																																																															{
																																																																																																																																																"a que hora! você deseja sair " + Settings.Default.cfgUser
																																																																																																																																															});
																																																																																																																																															this.lblSpeech2.Content = "hibernar a que Horas ";
																																																																																																																																															this.endSearchCmd();
																																																																																																																																														}
																																																																																																																																														else
																																																																																																																																														{
																																																																																																																																															bool flag475 = _speech == "Desativar Desligamento" || _speech == "Desabilitar Desligamento" || _speech == "Desabilitar reinicialização" || _speech == "Desabilitar hibernação" || _speech == "Desabilitar suspenção";
																																																																																																																																															if (flag475)
																																																																																																																																															{
																																																																																																																																																this.setdefaut();
																																																																																																																																																this.SpeakOut(new string[]
																																																																																																																																																{
																																																																																																																																																	"As funçõe de desativação do sistema foram desativadas no alarme "
																																																																																																																																																});
																																																																																																																																																this.endSearchCmd();
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
			catch (Exception)
			{
				this.SpeakOut(new string[]
				{
					"Não entendi" + Settings.Default.cfgUser + "repita por favor! ",
					"Acho que não estamos conectados a internet " + Settings.Default.cfgUser,
					"repita o comando, por que parece que não estamos conectados ",
					"veja se estamos conectados "
				});
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002150 File Offset: 0x00000350
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002158 File Offset: 0x00000358
		public bool comandoEjecutado { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002161 File Offset: 0x00000361
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002169 File Offset: 0x00000369
		public bool EnableComandos { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002172 File Offset: 0x00000372
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000217A File Offset: 0x0000037A
		public string Hora { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002183 File Offset: 0x00000383
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000218B File Offset: 0x0000038B
		public bool lecturaInicio { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002194 File Offset: 0x00000394
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000219C File Offset: 0x0000039C
		public string[] nombreAI { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000021A5 File Offset: 0x000003A5
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000021AD File Offset: 0x000003AD
		public string QEvent { get; set; }

		// Token: 0x0600004D RID: 77 RVA: 0x00011478 File Offset: 0x0000F678
		public AVJARVIS()
		{
			this.InitializeComponent();
			this.loadInterface();
			this.ActualizacionData();
			this.Fala.SelectVoice(Settings.Default.vozDefault);
			this.Fala.Volume = Settings.Default.volumen;
			base.Closing += this.AVJARVIS_Closing;
			base.Closed += this.AVJARVIS_Closed;
			this.btnCmd.Click += this.btnCmd_Click;
			base.Deactivated += this.AVJARVIS_Deactivated;
			this.btnJarvis.Click += this.btnJarvis_Click;
			this.btnJarvis2.Click += this.btnJarvis_Click;
			this.sliderVolumen.ValueChanged += this.sliderVolumen_ValueChanged;
			this.sliderVolumen.Value = (double)Settings.Default.volumen;
			base.ShowInTaskbar = false;
			this.icono();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000243AC File Offset: 0x000225AC
		private void _jarvis_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
		{
			this.btnPrueba.Background = new SolidColorBrush(Colors.Green);
			this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 160, 0));
			bool flag = this.lecturaEnable;
			if (flag)
			{
				this.TimerRecOFF = Settings.Default.cfgRec;
				this.EnableComandos = true;
				bool soundOn = Settings.Default.soundOn;
				if (soundOn)
				{
					Settings.Default.soundOn = true;
				}
				Settings.Default.Save();
			}
			this.lblSpeech2.Content = "";
			this.lblSpeech2.Content = "Estou te ouvindo";
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0002445C File Offset: 0x0002265C
		private void _jarvis_SpeakStarted(object sender, SpeakStartedEventArgs e)
		{
			this.btnPrueba.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 210, byte.MaxValue));
			this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 210, byte.MaxValue));
			bool flag = this.lecturaEnable;
			if (flag)
			{
				this.lblSpeech2.Content = "cancelar, pare de ler, silêncio, para, cancelar leitura, para leitura, até ai, não leia mais...";
				this.TimerRecOFF = Settings.Default.cfgRec;
				this.EnableComandos = true;
				Settings.Default.Save();
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000021B6 File Offset: 0x000003B6
		private void _jarvis_SpeakProgress(object sender, SpeakProgressEventArgs e)
		{
			this.lblSpeech.Content = (e.Text ?? "");
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000244EC File Offset: 0x000226EC
		private void _reco_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
		{
			this.pbMicro.Value = (double)e.AudioLevel;
			this.pbMicro2.Value = (double)e.AudioLevel;
			bool flag = Settings.Default.Skin == 1;
			if (flag)
			{
				this.SliderBar(e.AudioLevel);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00024540 File Offset: 0x00022740
		private void _reco_AudioStateChanged(object sender, AudioStateChangedEventArgs e)
		{
			this.EstadoRec = e.AudioState.ToString();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00024568 File Offset: 0x00022768
		private void _reco_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
		{
			try
			{
				bool flag = (double)e.Result.Confidence >= Settings.Default.Confidence;
				if (flag)
				{
					string text = e.Result.Text;
					Trace.WriteLine(text + " " + Math.Round((double)e.Result.Confidence, 5).ToString());
					System.Windows.Controls.Label label = this.lblPrecision;
					label.Content = Math.Round((double)e.Result.Confidence, 5).ToString();
					this.lblSpeech.Content = e.Result.Text;
					this.SpeechResult = e.Result;
					this.speech = e.Result.Text;
					bool flag2 = this.setHoraR;
					if (flag2)
					{
						this.setHoraRecord(this.speech);
					}
					bool flag3 = this.confirRecord;
					if (flag3)
					{
						this.confirmarRecodatorio(this.speech);
					}
					bool flag4 = this.EnableComandos && !this.lecturaEnable;
					if (flag4)
					{
						this.lblSpeech2.Content = e.Result.Text;
					}
					bool flag5 = this.alarmaSet;
					if (flag5)
					{
						this.setHoraAlarma(this.speech);
					}
					bool enableDictado = this.EnableDictado;
					if (enableDictado)
					{
						this.writeDictado(this.speech);
					}
					bool flag6 = this.completarBusqueda && this.LoadCmdSearch;
					if (flag6)
					{
						this.preBuscarWeb(this.speech);
					}
					bool enableShutdown = this.EnableShutdown;
					if (enableShutdown)
					{
						this.desactivar(this.speech);
					}
					bool flag7 = this.lecturaEnable;
					if (flag7)
					{
						this.offLectura(this.speech);
					}
					bool flag8 = !this.ventanaOpen && !this.EnableDictado && !this.completarBusqueda && !this.EnableShutdown && !this.lecturaEnable && !this.comandoEjecutado && !this.alarmaSet && !this.confirRecord && !this.setHoraR;
					if (flag8)
					{
						this.rect.Fill = new SolidColorBrush(Colors.Green);
						this.speech = this.filtroNombreAsistente(this.speech);
						this.EnableReconocimeinto(this.speech);
						bool flag9 = Settings.Default.soundOn && this.EnableComandos;
						if (flag9)
						{
							try
							{
								bool soundOn = Settings.Default.soundOn;
								if (soundOn)
								{
									SoundEffects.sonido.Play();
								}
							}
							catch (Exception ex)
							{
								Exception ex2 = ex;
								System.Windows.MessageBox.Show(ex2.Message, "AV", MessageBoxButton.OK, MessageBoxImage.Hand);
							}
						}
						bool flag10 = this.EnableComandos && !this.comandoEjecutado;
						if (flag10)
						{
							this.controlDeBusqueda(this.speech);
						}
					}
					else
					{
						this.rect.Fill = new SolidColorBrush(Colors.Red);
					}
					this.comandoEjecutado = false;
				}
				else
				{
					bool flag11 = (double)e.Result.Confidence >= 0.5;
					if (flag11)
					{
						this.lblSpeech2.Content = "fale mais claramente";
						bool flag12 = this.EnableDictado && !this.comandoEjecutado;
						if (flag12)
						{
							this.writeDictado(e.Result.Text);
						}
					}
				}
			}
			catch (Exception ex3)
			{
				Exception ex4 = ex3;
				System.Windows.MessageBox.Show(ex4.Message, "AV", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00024918 File Offset: 0x00022B18
		private void ActualizacionData()
		{
			this.dts = new Datos();
			Datos.check_dataBase(System.Windows.Forms.Application.StartupPath + "\\DataBase\\DataAccess.accdb", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\DataAccess.accdb");
			Datos.check_dataBase(System.Windows.Forms.Application.StartupPath + "\\DataKey\\DataBaseKey.accdb", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\DataBaseKey.accdb");
			Datos.check_dataBase(System.Windows.Forms.Application.StartupPath + "\\DefectBase\\DataBaseDefect.accdb", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\AV Data\\DataBaseDefect.accdb");
			this.dts = null;
			this.md = new MovieData();
			this.md.loadMuzicFolder();
			this.md = null;
			SerialDB.check_XmlCmdSerial();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000249FC File Offset: 0x00022BFC
		public void configuracionTimer()
		{
			this.Timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
			this.Timer.Tick += this.Timer_Tick;
			this.Timer2.Interval = new TimeSpan(0, 0, 0, 1, 0);
			this.Timer2.Tick += this.Timer2_Tick;
			this.Timer3.Interval = new TimeSpan(0, 0, 0, 1, 0);
			this.Timer3.Tick += this.Timer3_Tick;
			this.Timer2.Start();
			this.TimerFlip.Interval = new TimeSpan(0, 0, 0, 1);
			this.TimerFlip.Tick += this.TimerFlip_Tick;
			this.TimerImap.Interval = new TimeSpan(0, 1, 0);
			this.TimerImap.Tick += this.TimerImap_Tick;
			this.TimerImap.Start();
			this.TimerAviso.Interval = new TimeSpan(0, 0, 1);
			this.TimerAviso.Tick += this.TimerAviso_Tick;
			this.TimerSetAlarm.Interval = new TimeSpan(0, 0, 10);
			this.TimerSetAlarm.Tick += this.TimerSetAlarm_Tick;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00024B60 File Offset: 0x00022D60
		private void TimerSetAlarm_Tick(object sender, EventArgs e)
		{
			this.SpeakOut(new string[]
			{
				"cancelado"
			});
			this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(11, 166, 96));
			this.TimerRecOFF = Settings.Default.cfgRec;
			this.comandoEjecutado = true;
			this.alarmaSet = false;
			this.TimerSetAlarm.Stop();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00024BD0 File Offset: 0x00022DD0
		private void Cfgalarma(string horaa)
		{
			string[] array = horaa.Split(new char[]
			{
				':'
			});
			Settings.Default.tHora = array[0];
			string[] array2 = array[1].Split(new char[]
			{
				' '
			});
			Settings.Default.tMin = array2[0];
			Settings.Default.Periodo = array2[1].ToLower();
			this.setAlarma();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00024C3C File Offset: 0x00022E3C
		private void alarma()
		{
			string text = string.Empty;
			bool flag = this.SpeechResult.Semantics.ContainsKey("Alarm") && this.SpeechResult.Semantics.ContainsKey("AlarmResult");
			if (flag)
			{
				bool flag2 = !(this.SpeechResult.Semantics["AlarmResult"].Value.ToString() == "") && this.SpeechResult.Semantics["Alarm"].Value.ToString() == "setAlarm";
				if (flag2)
				{
					try
					{
						text = this.SpeechResult.Semantics["AlarmResult"].Value.ToString();
						this.Cfgalarma(text);
						this.Fala.SpeakAsync("Pronto " + Settings.Default.cfgUser + ", o alarme será ativado as" + text);
						this.TimerRecOFF = Settings.Default.cfgRec;
						this.comandoEjecutado = true;
					}
					catch (Exception ex)
					{
						Exception ex2 = ex;
						System.Windows.MessageBox.Show(ex2.Message, "AV - Alarme", MessageBoxButton.OK, MessageBoxImage.Hand);
					}
				}
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00024D80 File Offset: 0x00022F80
		private void actualizaCuentas(string imail)
		{
			try
			{
				bool flag = this.fromLecturaGmail;
				if (flag)
				{
					bool flag2 = this.cuentas > 1;
					if (flag2)
					{
						this.Fala.SpeakAsync("Mensagem do Gmail. " + this.MsgSpeechGmail[this.gmailNum]);
					}
					else
					{
						this.Fala.SpeakAsync(this.MsgSpeechGmail[this.gmailNum]);
					}
					this.fromLecturaGmail = false;
				}
				bool flag3 = this.fromLecturaOutlook;
				if (flag3)
				{
					bool flag4 = this.cuentas > 1;
					if (flag4)
					{
						this.Fala.SpeakAsync("Mensagem do Outlook. " + this.MsgSpeechOutlook[this.outlookNum]);
					}
					else
					{
						this.Fala.SpeakAsync(this.MsgSpeechOutlook[this.outlookNum]);
					}
					this.fromLecturaOutlook = false;
				}
				bool flag5 = this.fromLecturaYahoo;
				if (flag5)
				{
					bool flag6 = this.cuentas > 1;
					if (flag6)
					{
						this.Fala.SpeakAsync("Mensagem do Yahoo. " + this.MsgSpeechYahoo[this.yahooNum]);
					}
					else
					{
						this.Fala.SpeakAsync(this.MsgSpeechYahoo[this.yahooNum]);
					}
					this.fromLecturaYahoo = false;
				}
				bool flag7 = this.fromLecturaEmail;
				if (flag7)
				{
					bool flag8 = this.cuentas > 1;
					if (flag8)
					{
						this.Fala.SpeakAsync("Mensagem pessoal. " + this.MsgSpeechEmail[this.emailNum]);
					}
					else
					{
						this.Fala.SpeakAsync(this.MsgSpeechEmail[this.emailNum]);
					}
					this.fromLecturaEmail = false;
				}
			}
			catch (Exception)
			{
				this.Fala.SpeakAsync(imail + "nenhum novo e-mail para ler ");
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000021D4 File Offset: 0x000003D4
		private void anillo_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.popupJarvis.IsOpen = true;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000021E4 File Offset: 0x000003E4
		private void AVJARVIS_Click(object sender, EventArgs e)
		{
			this.mostrarConfiguracion();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000021EE File Offset: 0x000003EE
		private void AVJARVIS_Closed(object sender, EventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
			this.nicon.Dispose();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002208 File Offset: 0x00000408
		private void AVJARVIS_Closing(object sender, CancelEventArgs e)
		{
			Settings.Default.Top = base.Top;
			Settings.Default.Left = base.Left;
			Settings.Default.inicioWPosition = true;
			Settings.Default.Save();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002244 File Offset: 0x00000444
		private void AVJARVIS_Deactivated(object sender, EventArgs e)
		{
			this.popupJarvis.IsOpen = false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002244 File Offset: 0x00000444
		private void btnClosepp_Click(object sender, RoutedEventArgs e)
		{
			this.popupJarvis.IsOpen = false;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00024F7C File Offset: 0x0002317C
		private void btnCmd_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.vinfo == null;
			if (flag)
			{
				this.vinfo = new VentanaInfo();
				this.vinfo.Closed += delegate(object a, EventArgs b)
				{
					this.vinfo = null;
				};
				SoundEffects.Hover_comandos.Play();
			}
			this.vinfo.WindowState = WindowState.Normal;
			this.vinfo.Show();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002254 File Offset: 0x00000454
		private void btnCmd_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_skipe1.Play();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002244 File Offset: 0x00000444
		private void btnCmd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.popupJarvis.IsOpen = false;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002262 File Offset: 0x00000462
		private void btnDonar_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://www.paypal.com/donate?business=QEEV2VLBMCTTS&item_name=Doe+para+ajudar+no+desenvolvimento+de+novas+fun%C3%A7%C3%B5es+e+melhorias+para+Assistente+Virtual%21&currency_code=BRL");
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002254 File Offset: 0x00000454
		private void btnDonar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_skipe1.Play();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002270 File Offset: 0x00000470
		private void Btnfechar_Click(object sender, EventArgs e)
		{
			Process.GetCurrentProcess().MaxWorkingSet = Process.GetCurrentProcess().MinWorkingSet;
			System.Windows.Application.Current.Shutdown();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00024FE0 File Offset: 0x000231E0
		private void btnFiltro_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.gridJarvis.Visibility == Visibility.Visible;
			if (flag)
			{
				this.gridJarvis.Visibility = Visibility.Hidden;
			}
			else
			{
				this.gridJarvis.Visibility = Visibility.Visible;
				SoundEffects.Hover_import_export.Play();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002254 File Offset: 0x00000454
		private void btnFiltro_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_skipe1.Play();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000021E4 File Offset: 0x000003E4
		private void btnJarvis_Click(object sender, RoutedEventArgs e)
		{
			this.mostrarConfiguracion();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002254 File Offset: 0x00000454
		private void btnJarvis_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_skipe1.Play();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002254 File Offset: 0x00000454
		private void btnJarvis2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_skipe1.Play();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002244 File Offset: 0x00000444
		private void btnJarvisCfg2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.popupJarvis.IsOpen = false;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002293 File Offset: 0x00000493
		private void btnPrueba_Click(object sender, RoutedEventArgs e)
		{
			this.Fala.SpeakAsync("É importante que o botão fique verde, exatamente quando você para de falar e não antes. Altere para um número maior para obter melhor precisão. Execute este teste para cada nova voz");
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0002502C File Offset: 0x0002322C
		private void btnTraining_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				bool flag = System.Windows.MessageBox.Show("O AV fechará para iniciar o treinamento", "AV", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk) == MessageBoxResult.OK;
				if (flag)
				{
					new Process
					{
						StartInfo = 
						{
							FileName = System.IO.Path.Combine(Environment.SystemDirectory, "speech\\speechux\\SpeechUXWiz.exe"),
							Arguments = "UserTraining"
						}
					}.Start();
					System.Windows.Application.Current.Shutdown();
				}
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000250C0 File Offset: 0x000232C0
		private void calculadora()
		{
			bool flag = this.SpeechResult.Semantics.ContainsKey("number1") && this.SpeechResult.Semantics.ContainsKey("number2") && this.SpeechResult.Semantics.ContainsKey("operator");
			if (flag)
			{
				try
				{
					bool flag2 = this.SpeechResult.Semantics["number1"].Value.ToString() != "" && this.SpeechResult.Semantics["operator"].Value.ToString() != "" && this.SpeechResult.Semantics["number2"].Value.ToString() != "";
					if (flag2)
					{
						int op = Convert.ToInt32(this.SpeechResult.Semantics["number1"].Value.ToString());
						char operation = Convert.ToChar(this.SpeechResult.Semantics["operator"].Value.ToString());
						int op2 = Convert.ToInt32(this.SpeechResult.Semantics["number2"].Value.ToString());
						this.calcular(op, operation, op2);
						this.TimerRecOFF = Settings.Default.cfgRec;
						this.comandoEjecutado = true;
					}
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					System.Windows.MessageBox.Show(ex2.Message + "error calculadora");
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0002527C File Offset: 0x0002347C
		private void calcular(int op1, char operation, int op2)
		{
			float num = 0f;
			string text = "";
			bool flag = operation == '/' && op2 == 0;
			if (flag)
			{
				string textToSpeak = op1.ToString() + "entre zero não está definido.";
				this.Fala.SpeakAsync(textToSpeak);
			}
			else
			{
				switch (operation)
				{
				case '*':
					num = (float)(op1 * op2);
					text = " por ";
					break;
				case '+':
					num = (float)(op1 + op2);
					text = " mais ";
					break;
				case '-':
					num = (float)(op1 - op2);
					text = " menos ";
					break;
				case '/':
					num = (float)(op1 / op2);
					text = " entre ";
					break;
				}
				string[] values = new string[]
				{
					op1.ToString(),
					text,
					op2.ToString(),
					" é igual a ",
					num.ToString()
				};
				string textToSpeak = string.Concat(values);
				this.Fala.SpeakAsync(textToSpeak);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00025384 File Offset: 0x00023584
		public GrammarBuilder CreateGrammarCalc()
		{
			GrammarBuilder[] array = new GrammarBuilder[]
			{
				new GrammarBuilder(new Choices(new string[]
				{
					"salir"
				})),
				new GrammarBuilder()
			};
			array[1].Append("quanto é", 0, 1999);
			Choices choices = new Choices();
			for (int i = 0; i < this.numberString.Length; i++)
			{
				choices.Add(new GrammarBuilder[]
				{
					new SemanticResultValue(this.numberString[i], i)
				});
			}
			array[1].Append(new SemanticResultKey("number1", new GrammarBuilder[]
			{
				choices
			}));
			string[] array2 = new string[]
			{
				"mais",
				"menos",
				"por",
				"entre",
				"vezes",
				"multiplicado por",
				"dividido entre"
			};
			Choices choices2 = new Choices();
			choices2.Add(new GrammarBuilder[]
			{
				new SemanticResultValue("mais", "+")
			});
			choices2.Add(new GrammarBuilder[]
			{
				new SemanticResultValue("menos", "-")
			});
			choices2.Add(new GrammarBuilder[]
			{
				new SemanticResultValue("por", "*")
			});
			choices2.Add(new GrammarBuilder[]
			{
				new SemanticResultValue("multiplicado por", "*")
			});
			choices2.Add(new GrammarBuilder[]
			{
				new SemanticResultValue("vezes", "*")
			});
			choices2.Add(new GrammarBuilder[]
			{
				new SemanticResultValue("entre", "/")
			});
			choices2.Add(new GrammarBuilder[]
			{
				new SemanticResultValue("dividido entre", "/")
			});
			array[1].Append(new SemanticResultKey("operator", new GrammarBuilder[]
			{
				choices2
			}));
			array[1].Append(new SemanticResultKey("number2", new GrammarBuilder[]
			{
				choices
			}));
			return new GrammarBuilder(new Choices(array));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000022A7 File Offset: 0x000004A7
		private void cargaGramaticas()
		{
			this.loadGramars();
			this.LoadGramarTabla();
			this.LoadGramarTablaKey();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000022BF File Offset: 0x000004BF
		private void cbOpacidad_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cbOpacity = true;
			Settings.Default.Save();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000255D8 File Offset: 0x000237D8
		private void cbOpacidad_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.cbOpacity = false;
			Settings.Default.Save();
			this.skin2.Opacity = 1.0;
			this.skin1.Opacity = 1.0;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00025628 File Offset: 0x00023828
		private void cbskin_DropDownClosed(object sender, EventArgs e)
		{
			bool flag = this.cbskin.Text == "Skin 1";
			if (flag)
			{
				this.skin1.Visibility = Visibility.Visible;
				this.skin2.Visibility = Visibility.Hidden;
			}
			bool flag2 = this.cbskin.Text == "Skin 2";
			if (flag2)
			{
				this.skin1.Visibility = Visibility.Hidden;
				this.skin2.Visibility = Visibility.Visible;
			}
			Settings.Default.Skin = Convert.ToByte(this.cbskin.Text.Split(new char[]
			{
				' '
			})[1]);
			Settings.Default.Save();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000022D9 File Offset: 0x000004D9
		private void cbSound_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.soundOn = true;
			Settings.Default.Save();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000022F3 File Offset: 0x000004F3
		private void cbSound_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.soundOn = false;
			Settings.Default.Save();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000230D File Offset: 0x0000050D
		private void cbTopMost_Checked(object sender, RoutedEventArgs e)
		{
			base.Topmost = true;
			Settings.Default.topMost = true;
			Settings.Default.Save();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000232F File Offset: 0x0000052F
		private void cbTopMost_Unchecked(object sender, RoutedEventArgs e)
		{
			base.Topmost = false;
			Settings.Default.topMost = false;
			Settings.Default.Save();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000256D8 File Offset: 0x000238D8
		private void clearAlarma()
		{
			Settings.Default.tActivo = "DESACTIVADO";
			Settings.Default.cLun = false;
			Settings.Default.cMar = false;
			Settings.Default.cMie = false;
			Settings.Default.cJue = false;
			Settings.Default.cVie = false;
			Settings.Default.cSab = false;
			Settings.Default.cDom = false;
			Settings.Default.Save();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00025758 File Offset: 0x00023958
		private void ComputerTermination()
		{
			string qevent = this.QEvent;
			bool flag = qevent == "apagar";
			if (flag)
			{
				Process.Start("shutdown", "-s");
			}
			else
			{
				bool flag2 = qevent == "sesionoff";
				if (flag2)
				{
					Process.Start("shutdown", "-l");
				}
				else
				{
					bool flag3 = qevent == "reiniciar";
					if (flag3)
					{
						Process.Start("shutdown", "-r");
					}
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000257D4 File Offset: 0x000239D4
		private void CheckForEmails()
		{
			string url = "https://mail.google.com/mail/feed/atom";
			XmlUrlResolver xmlResolver = new XmlUrlResolver
			{
				Credentials = new NetworkCredential(Settings.Default.userGmail, Settings.Default.passGmail)
			};
			XmlTextReader reader = new XmlTextReader(url)
			{
				XmlResolver = xmlResolver
			};
			try
			{
				XNamespace xNamespace = XNamespace.Get("http://purl.org/atom/ns#");
				XDocument xdocument = XDocument.Load(reader);
				var enumerable = from item in xdocument.Descendants(xNamespace + "entry")
				select new
				{
					Author = item.Element(xNamespace + "author").Element(xNamespace + "name").Value,
					Title = item.Element(xNamespace + "title").Value,
					Link = item.Element(xNamespace + "link").Attribute("href").Value,
					Summary = item.Element(xNamespace + "summary").Value
				};
				this.MsgList.Clear();
				this.MsgLink.Clear();
				this.MsgLinkInfo.Clear();
				foreach (var <>f__AnonymousType in enumerable)
				{
					bool flag = <>f__AnonymousType.Title != string.Empty;
					if (flag)
					{
						this.MsgList.Add(string.Concat(new string[]
						{
							"Mensagem do gmail, de",
							<>f__AnonymousType.Author,
							",cujo o assunto é",
							<>f__AnonymousType.Title,
							"e diz. ",
							<>f__AnonymousType.Summary
						}));
						this.MsgLinkInfo.Add(<>f__AnonymousType.Author);
						this.MsgLink.Add(<>f__AnonymousType.Link);
					}
					else
					{
						this.MsgList.Add("Mensagem do gmail, de" + <>f__AnonymousType.Author + ", Não tem assunto e, em resumo, diz " + <>f__AnonymousType.Summary);
						this.MsgLinkInfo.Add(<>f__AnonymousType.Author);
						this.MsgLink.Add(<>f__AnonymousType.Link);
					}
					this.CargaDecorreo = true;
				}
				bool flag2 = !this.bollCantidadAnterioresCorreos;
				if (flag2)
				{
					this.cantidadAnteriorCorreos = enumerable.Count();
					this.bollCantidadAnterioresCorreos = true;
				}
				this.cantidadCorreos = enumerable.Count();
				bool flag3 = !this.lecturaCorreo;
				if (flag3)
				{
					bool flag4 = enumerable.Count() > 0;
					if (flag4)
					{
						bool flag5 = enumerable.Count() != 1;
						if (flag5)
						{
							this.SpeakOut(new string[]
							{
								string.Concat(new object[]
								{
									Settings.Default.cfgUser,
									"tem ",
									enumerable.Count(),
									"novos e-mails que você não leu "
								})
							});
						}
						else
						{
							this.SpeakOut(new string[]
							{
								"você tem um novo email para ler "
							});
						}
					}
					else
					{
						bool flag6 = !(this.QEvent != "Checkfornewemails") && enumerable.Count() == 0;
						if (flag6)
						{
							this.SpeakOut(new string[]
							{
								"Você não tem novos e-mails para ler "
							});
							this.QEvent = string.Empty;
						}
					}
				}
			}
			catch
			{
				bool flag7 = !this.lecturaCorreo;
				if (flag7)
				{
					this.SpeakOut(new string[]
					{
						"Erro, acesso inválido ao email, verifique sua conexão "
					});
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00025B20 File Offset: 0x00023D20
		private Task conexionEmail()
		{
			return Task.Run(delegate()
			{
				try
				{
					this.clientImapE = new ImapClient();
					ImapClient imapClient = this.clientImapE;
					string servidorEmail = Settings.Default.servidorEmail;
					int port = Convert.ToInt32(Settings.Default.puertoEmail);
					bool cbSll = Settings.Default.cbSll;
					imapClient.Connect(servidorEmail, port, cbSll, default(CancellationToken));
					this.clientImapE.AuthenticationMechanisms.Remove("XOAUTH");
					ImapClient imapClient2 = this.clientImapE;
					string userEmail = Settings.Default.userEmail;
					string passEmail = Settings.Default.passEmail;
					imapClient2.Authenticate(userEmail, passEmail, default(CancellationToken));
					IMailFolder inbox = this.clientImapE.Inbox;
					inbox.Open(FolderAccess.ReadOnly, default(CancellationToken));
					this.totalMensages = inbox.Count;
					this.MsgsUnreadEmail = 0;
					this.msgsRecibidos = this.totalMensages - Settings.Default.msgsEmail;
					Settings.Default.msgsEmail = this.totalMensages;
					Settings.Default.Save();
					int num = (this.totalMensages >= 20) ? 20 : this.totalMensages;
					this.MsgSpeechEmail.Clear();
					CancellationToken cancellationToken = default(CancellationToken);
					foreach (IMessageSummary messageSummary in inbox.Fetch(inbox.Count - num, -1, MessageSummaryItems.Body | MessageSummaryItems.Envelope | MessageSummaryItems.Flags | MessageSummaryItems.InternalDate | MessageSummaryItems.MessageSize | MessageSummaryItems.UniqueId, cancellationToken))
					{
						bool flag = !new MessageInfo(messageSummary).Flags.HasFlag(MessageFlags.Seen);
						if (flag)
						{
							this.MsgsUnreadEmail++;
							int index = messageSummary.Index;
							inbox.GetMessage(index, default(CancellationToken));
							string str;
							try
							{
								str = messageSummary.Envelope.From.ToString().Split(new char[]
								{
									'<'
								})[0].Split(new char[]
								{
									'"'
								})[1];
								string text = messageSummary.Envelope.From.ToString().Split(new char[]
								{
									'<'
								})[0];
							}
							catch
							{
								str = messageSummary.Envelope.From.ToString();
							}
							this.MsgSpeechEmail.Add("De " + str + ", cujo o assunto é, " + messageSummary.Envelope.Subject);
						}
					}
					this.MsgSpeechEmail.Reverse();
					bool flag2 = this.msgsRecibidos == 0;
					if (flag2)
					{
						bool flag3 = this.speechEmailTrue;
						if (flag3)
						{
							bool flag4 = this.cuentas > 1;
							if (flag4)
							{
								this.Fala.SpeakAsync("mensagem pessoal ");
							}
							bool flag5 = this.MsgsUnreadEmail == 1;
							if (flag5)
							{
								this.Fala.SpeakAsync(Settings.Default.cfgUser + "tem um novo e-mail que você não leu ");
							}
							else
							{
								bool flag6 = this.MsgsUnreadEmail != 0;
								if (flag6)
								{
									this.Fala.SpeakAsync(string.Concat(new object[]
									{
										Settings.Default.cfgUser,
										"tem ",
										this.MsgsUnreadEmail,
										"novos e-mails que você não leu "
									}));
								}
								else
								{
									this.Fala.SpeakAsync(Settings.Default.cfgUser + "você não tem nenhum novo e-mail para ler ");
								}
							}
						}
					}
					else
					{
						bool flag7 = this.msgsRecibidos >= 2;
						if (flag7)
						{
							bool flag8 = this.cuentas > 1;
							if (flag8)
							{
								this.Fala.SpeakAsync("mensagem pessoal ");
							}
							this.Fala.SpeakAsync(string.Concat(new object[]
							{
								Settings.Default.cfgUser,
								"você acabou de receber ",
								this.msgsRecibidos,
								"novos e-mails do Email "
							}));
							this.emailNum = 0;
						}
						else
						{
							bool flag9 = this.cuentas > 1;
							if (flag9)
							{
								this.Fala.SpeakAsync("mensagem pessoal ");
							}
							this.Fala.SpeakAsync(Settings.Default.cfgUser + "você acabou de receber uma nova mensagem de " + this.MsgSpeechEmail[0]);
							this.emailNum = 0;
						}
					}
					ImapClient imapClient3 = this.clientImapE;
					imapClient3.Disconnect(true, default(CancellationToken));
					this.clientImapE.Dispose();
					this.TimerImap.Start();
					this.checkEmail = false;
					this.speechEmailTrue = false;
					this.actualizaCuentas("mensagem pessoal ");
				}
				catch (Exception)
				{
					ImapClient imapClient4 = this.clientImapE;
					imapClient4.Disconnect(true, default(CancellationToken));
					this.clientImapE.Dispose();
					this.TimerImap.Start();
					this.checkEmail = false;
					this.speechEmailTrue = false;
				}
			});
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00025B48 File Offset: 0x00023D48
		private Task conexionGmail()
		{
			return Task.Run(delegate()
			{
				try
				{
					this.clientImapG = new ImapClient();
					ImapClient imapClient = this.clientImapG;
					imapClient.Connect("imap.gmail.com", 993, true, default(CancellationToken));
					this.clientImapG.AuthenticationMechanisms.Remove("XOAUTH2");
					ImapClient imapClient2 = this.clientImapG;
					string userGmail = Settings.Default.userGmail;
					string passGmail = Settings.Default.passGmail;
					imapClient2.Authenticate(userGmail, passGmail, default(CancellationToken));
					IMailFolder inbox = this.clientImapG.Inbox;
					inbox.Open(FolderAccess.ReadOnly, default(CancellationToken));
					this.totalMensages = inbox.Count;
					this.MsgsUnreadGmail = 0;
					this.msgsRecibidos = this.totalMensages - Settings.Default.msgsGmail;
					Settings.Default.msgsGmail = this.totalMensages;
					Settings.Default.Save();
					this.MsgSpeechGmail.Clear();
					CancellationToken cancellationToken = default(CancellationToken);
					foreach (IMessageSummary messageSummary in inbox.Fetch(inbox.Count - 20, -1, MessageSummaryItems.Body | MessageSummaryItems.Envelope | MessageSummaryItems.Flags | MessageSummaryItems.InternalDate | MessageSummaryItems.MessageSize | MessageSummaryItems.UniqueId, cancellationToken))
					{
						bool flag = !new MessageInfo(messageSummary).Flags.HasFlag(MessageFlags.Seen);
						if (flag)
						{
							this.MsgsUnreadGmail++;
							int index = messageSummary.Index;
							inbox.GetMessage(index, default(CancellationToken));
							string str;
							try
							{
								str = messageSummary.Envelope.From.ToString().Split(new char[]
								{
									'<'
								})[0].Split(new char[]
								{
									'"'
								})[1];
								string text = messageSummary.Envelope.From.ToString().Split(new char[]
								{
									'<'
								})[0];
							}
							catch
							{
								str = messageSummary.Envelope.From.ToString();
							}
							this.MsgSpeechGmail.Add("De " + str + ", cujo o assunto é, " + messageSummary.Envelope.Subject);
						}
					}
					this.MsgSpeechGmail.Reverse();
					bool flag2 = this.msgsRecibidos == 0;
					if (flag2)
					{
						bool flag3 = this.speechGmailTrue;
						if (flag3)
						{
							bool flag4 = this.cuentas > 1;
							if (flag4)
							{
								this.Fala.SpeakAsync("mensagem do gmail. ");
							}
							bool flag5 = this.MsgsUnreadGmail == 1;
							if (flag5)
							{
								this.Fala.SpeakAsync(Settings.Default.cfgUser + "tem um novo e-mail que você não leu ");
							}
							else
							{
								bool flag6 = this.MsgsUnreadGmail != 0;
								if (flag6)
								{
									this.Fala.SpeakAsync(string.Concat(new object[]
									{
										Settings.Default.cfgUser,
										"tem ",
										this.MsgsUnreadGmail,
										"novos e-mails que você não leu "
									}));
								}
								else
								{
									this.Fala.SpeakAsync(Settings.Default.cfgUser + "você não tem nenhum novo e-mail para ler ");
								}
							}
						}
					}
					else
					{
						bool flag7 = this.msgsRecibidos >= 2;
						if (flag7)
						{
							bool flag8 = this.cuentas > 1;
							if (flag8)
							{
								this.Fala.SpeakAsync("mensagem do gmail. ");
							}
							this.Fala.SpeakAsync(string.Concat(new object[]
							{
								Settings.Default.cfgUser,
								"você acabou de receber ",
								this.msgsRecibidos,
								"novos e-mails "
							}));
						}
						else
						{
							bool flag9 = this.cuentas > 1;
							if (flag9)
							{
								this.Fala.SpeakAsync("mensagem do gmail. ");
							}
							this.Fala.SpeakAsync(Settings.Default.cfgUser + "você acabou de receber uma nova mensagem de " + this.MsgSpeechGmail[0]);
							this.gmailNum = 0;
						}
					}
					ImapClient imapClient3 = this.clientImapG;
					imapClient3.Disconnect(true, default(CancellationToken));
					this.clientImapG.Dispose();
					this.speechGmailTrue = false;
					this.TimerImap.Start();
					this.checkGmail = false;
					this.actualizaCuentas("mensagem do gmail. ");
				}
				catch (Exception)
				{
					ImapClient imapClient4 = this.clientImapG;
					imapClient4.Disconnect(true, default(CancellationToken));
					this.clientImapG.Dispose();
					this.speechGmailTrue = false;
					this.TimerImap.Start();
					this.checkGmail = false;
				}
			});
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00025B70 File Offset: 0x00023D70
		private Task conexionOutloock()
		{
			return Task.Run(delegate()
			{
				try
				{
					this.clientImapO = new ImapClient();
					ImapClient imapClient = this.clientImapO;
					imapClient.Connect("imap-mail.outlook.com", 993, true, default(CancellationToken));
					this.clientImapO.AuthenticationMechanisms.Remove("XOAUTH");
					ImapClient imapClient2 = this.clientImapO;
					string userOutlook = Settings.Default.userOutlook;
					string passOutlook = Settings.Default.passOutlook;
					imapClient2.Authenticate(userOutlook, passOutlook, default(CancellationToken));
					IMailFolder inbox = this.clientImapO.Inbox;
					inbox.Open(FolderAccess.ReadOnly, default(CancellationToken));
					this.totalMensages = inbox.Count;
					this.MsgsUnreadOutlook = 0;
					this.msgsRecibidos = this.totalMensages - Settings.Default.msgsOutlook;
					Settings.Default.msgsOutlook = this.totalMensages;
					Settings.Default.Save();
					this.MsgSpeechOutlook.Clear();
					CancellationToken cancellationToken = default(CancellationToken);
					foreach (IMessageSummary messageSummary in inbox.Fetch(inbox.Count - 20, -1, MessageSummaryItems.Body | MessageSummaryItems.Envelope | MessageSummaryItems.Flags | MessageSummaryItems.InternalDate | MessageSummaryItems.MessageSize | MessageSummaryItems.UniqueId, cancellationToken))
					{
						bool flag = !new MessageInfo(messageSummary).Flags.HasFlag(MessageFlags.Seen);
						if (flag)
						{
							this.MsgsUnreadOutlook++;
							int index = messageSummary.Index;
							inbox.GetMessage(index, default(CancellationToken));
							string str;
							try
							{
								str = messageSummary.Envelope.From.ToString().Split(new char[]
								{
									'<'
								})[0].Split(new char[]
								{
									'"'
								})[1];
								string text = messageSummary.Envelope.From.ToString().Split(new char[]
								{
									'<'
								})[0];
							}
							catch
							{
								str = messageSummary.Envelope.From.ToString();
							}
							this.MsgSpeechOutlook.Add("De " + str + ", cujo o assunto é, " + messageSummary.Envelope.Subject);
						}
					}
					this.MsgSpeechOutlook.Reverse();
					bool flag2 = this.msgsRecibidos == 0;
					if (flag2)
					{
						bool flag3 = this.speechOutloockTrue;
						if (flag3)
						{
							bool flag4 = this.cuentas > 1;
							if (flag4)
							{
								this.Fala.SpeakAsync("mensagem de Outlook");
							}
							bool flag5 = this.MsgsUnreadOutlook == 1;
							if (flag5)
							{
								this.Fala.SpeakAsync(Settings.Default.cfgUser + "tem um novo e-mail que você não leu ");
							}
							else
							{
								bool flag6 = this.MsgsUnreadOutlook != 0;
								if (flag6)
								{
									this.Fala.SpeakAsync(string.Concat(new object[]
									{
										Settings.Default.cfgUser,
										"tem ",
										this.MsgsUnreadOutlook,
										"novos e-mails que você não leu "
									}));
								}
								else
								{
									this.Fala.SpeakAsync(Settings.Default.cfgUser + "você não tem nenhum novo e-mail para ler ");
								}
							}
						}
					}
					else
					{
						bool flag7 = this.msgsRecibidos >= 2;
						if (flag7)
						{
							bool flag8 = this.cuentas > 1;
							if (flag8)
							{
								this.Fala.SpeakAsync("mensagem de Outlook ");
							}
							this.Fala.SpeakAsync(string.Concat(new object[]
							{
								Settings.Default.cfgUser,
								"você acabou de receber ",
								this.msgsRecibidos,
								"novos e-mails "
							}));
							this.outlookNum = 0;
						}
						else
						{
							bool flag9 = this.cuentas > 1;
							if (flag9)
							{
								this.Fala.SpeakAsync("mensagem de Outlook ");
							}
							this.Fala.SpeakAsync(Settings.Default.cfgUser + "você acabou de receber uma nova mensagem de " + this.MsgSpeechOutlook[0]);
							this.outlookNum = 0;
						}
					}
					ImapClient imapClient3 = this.clientImapO;
					imapClient3.Disconnect(true, default(CancellationToken));
					this.clientImapO.Dispose();
					this.speechOutloockTrue = false;
					this.TimerImap.Start();
					this.checkOutlook = false;
					this.actualizaCuentas(" mensagem de Outlook");
				}
				catch (Exception)
				{
					ImapClient imapClient4 = this.clientImapO;
					imapClient4.Disconnect(true, default(CancellationToken));
					this.clientImapO.Dispose();
					this.speechOutloockTrue = false;
					this.TimerImap.Start();
					this.checkOutlook = false;
				}
			});
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00025B98 File Offset: 0x00023D98
		private Task conexionYahoo()
		{
			return Task.Run(delegate()
			{
				try
				{
					this.clientImapY = new ImapClient();
					ImapClient imapClient = this.clientImapY;
					imapClient.Connect("imap.mail.yahoo.com", 993, true, default(CancellationToken));
					this.clientImapY.AuthenticationMechanisms.Remove("XOAUTH");
					ImapClient imapClient2 = this.clientImapY;
					string userYahoo = Settings.Default.userYahoo;
					string passYahoo = Settings.Default.passYahoo;
					imapClient2.Authenticate(userYahoo, passYahoo, default(CancellationToken));
					IMailFolder inbox = this.clientImapY.Inbox;
					inbox.Open(FolderAccess.ReadOnly, default(CancellationToken));
					this.totalMensages = inbox.Count;
					this.MsgsUnreadYahoo = 0;
					this.msgsRecibidos = this.totalMensages - Settings.Default.msgsYahoo;
					Settings.Default.msgsYahoo = this.totalMensages;
					Settings.Default.Save();
					int num = (this.totalMensages >= 20) ? 20 : this.totalMensages;
					this.MsgSpeechYahoo.Clear();
					CancellationToken cancellationToken = default(CancellationToken);
					foreach (IMessageSummary messageSummary in inbox.Fetch(inbox.Count - num, -1, MessageSummaryItems.Body | MessageSummaryItems.Envelope | MessageSummaryItems.Flags | MessageSummaryItems.InternalDate | MessageSummaryItems.MessageSize | MessageSummaryItems.UniqueId, cancellationToken))
					{
						bool flag = !new MessageInfo(messageSummary).Flags.HasFlag(MessageFlags.Seen);
						if (flag)
						{
							this.MsgsUnreadYahoo++;
							int index = messageSummary.Index;
							inbox.GetMessage(index, default(CancellationToken));
							string str;
							try
							{
								str = messageSummary.Envelope.From.ToString().Split(new char[]
								{
									'<'
								})[0].Split(new char[]
								{
									'"'
								})[1];
								string text = messageSummary.Envelope.From.ToString().Split(new char[]
								{
									'<'
								})[0];
							}
							catch
							{
								str = messageSummary.Envelope.From.ToString();
							}
							this.MsgSpeechYahoo.Add("De " + str + ", cujo o assunto é, " + messageSummary.Envelope.Subject);
						}
					}
					this.MsgSpeechYahoo.Reverse();
					bool flag2 = this.msgsRecibidos == 0;
					if (flag2)
					{
						bool flag3 = this.speechYahooTrue;
						if (flag3)
						{
							bool flag4 = this.cuentas > 1;
							if (flag4)
							{
								this.Fala.SpeakAsync("mensagens de Yahoo. ");
							}
							bool flag5 = this.MsgsUnreadYahoo == 1;
							if (flag5)
							{
								this.Fala.SpeakAsync(Settings.Default.cfgUser + "tem um novo e-mail que você não leu ");
							}
							else
							{
								bool flag6 = this.MsgsUnreadYahoo != 0;
								if (flag6)
								{
									this.Fala.SpeakAsync(string.Concat(new object[]
									{
										Settings.Default.cfgUser,
										"tem ",
										this.MsgsUnreadYahoo,
										"novos e-mails que você não leu "
									}));
								}
								else
								{
									this.Fala.SpeakAsync(Settings.Default.cfgUser + "você não tem nenhum novo e-mail para ler ");
								}
							}
						}
					}
					else
					{
						bool flag7 = this.msgsRecibidos >= 2;
						if (flag7)
						{
							bool flag8 = this.cuentas > 1;
							if (flag8)
							{
								this.Fala.SpeakAsync("mensagens de Yahoo. ");
							}
							this.Fala.SpeakAsync(string.Concat(new object[]
							{
								Settings.Default.cfgUser,
								"você acabou de receber ",
								this.msgsRecibidos,
								"novos e-mails "
							}));
							this.yahooNum = 0;
						}
						else
						{
							bool flag9 = this.cuentas > 1;
							if (flag9)
							{
								this.Fala.SpeakAsync("mensagens de Yahoo. ");
							}
							this.Fala.SpeakAsync(Settings.Default.cfgUser + "você acabou de receber uma nova mensagem de " + this.MsgSpeechYahoo[0]);
							this.yahooNum = 0;
						}
					}
					ImapClient imapClient3 = this.clientImapY;
					imapClient3.Disconnect(true, default(CancellationToken));
					this.clientImapY.Dispose();
					this.TimerImap.Start();
					this.checkYahoo = false;
					this.speechYahooTrue = false;
					this.actualizaCuentas("mensagens de Yahoo. ");
				}
				catch (Exception)
				{
					ImapClient imapClient4 = this.clientImapY;
					imapClient4.Disconnect(true, default(CancellationToken));
					this.clientImapY.Dispose();
					this.TimerImap.Start();
					this.checkYahoo = false;
					this.speechYahooTrue = false;
				}
			});
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00025BC0 File Offset: 0x00023DC0
		private void confirmarRecodatorio(string _speech)
		{
			bool flag = _speech == "confirmar" || _speech == "aceitar";
			if (flag)
			{
				this.sRec = new CDRecordatorio(this.textoR, this.textoR, this.diaR, this.horaR, false);
				this.loadDataRec();
				this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 160, 0));
				this.Fala.SpeakAsync("pronto " + Settings.Default.cfgUser);
				this.TimerRecOFF = Settings.Default.cfgRec;
				this.comandoEjecutado = true;
				this.confirRecord = false;
			}
			else
			{
				bool flag2 = _speech == "cancelar";
				if (flag2)
				{
					this.Fala.SpeakAsync("cancelado");
					this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 160, 0));
					this.TimerRecOFF = Settings.Default.cfgRec;
					this.comandoEjecutado = true;
					this.confirRecord = false;
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00025CDC File Offset: 0x00023EDC
		private void controlDeBusqueda(string _speech)
		{
			bool flag = this.EnableComandos && !this.comandoEjecutado;
			if (flag)
			{
				bool loadCmdSearch = this.LoadCmdSearch;
				if (loadCmdSearch)
				{
					this.preBuscarWeb(_speech);
				}
				bool flag2 = !this.comandoEjecutado;
				if (flag2)
				{
					this.cmds = new Comandos();
					_speech = this.sinonimoCmd(_speech);
					this.cmds = null;
					this.Comandos_defecto(_speech);
					bool flag3 = !this.comandoEjecutado;
					if (flag3)
					{
						this.cmdConfirmado = false;
						try
						{
							this.con = new OleDbConnection(Settings.Default.conexion);
							this.con.Open();
							OleDbCommand oleDbCommand = new OleDbCommand("SELECT *FROM ComandosSociales WHERE Comando LIKE '%" + _speech + "%'", this.con);
							OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader(CommandBehavior.SingleResult);
							bool flag4 = oleDbDataReader.Read();
							if (flag4)
							{
								this.confirmarCmd = oleDbDataReader["Comando"].ToString().Split(new char[]
								{
									'+'
								});
								string[] array = this.confirmarCmd;
								for (int i = 0; i < array.Length; i++)
								{
									bool flag5 = _speech == array[i];
									if (flag5)
									{
										this.cmdConfirmado = true;
									}
								}
								bool flag6 = this.cmdConfirmado;
								if (flag6)
								{
									this.Ejecutar = oleDbDataReader["Ejecutar"].ToString();
									this.Respuesta = oleDbDataReader["Respuesta"].ToString();
									this.multiRespuesta = this.Respuesta.Split(new char[]
									{
										'+'
									});
									bool flag7 = this.Ejecutar != "";
									if (flag7)
									{
										Process.Start(this.Ejecutar);
									}
									this.ranNum = this.rnd.Next(0, this.multiRespuesta.Count<string>());
									this.Fala.SpeakAsync(this.multiRespuesta[this.ranNum]);
									this.comandoEjecutado = true;
									this.TimerRecOFF = Settings.Default.cfgRec;
									return;
								}
							}
							this.con.Close();
						}
						catch (Exception ex)
						{
							System.Windows.MessageBox.Show(ex.Message);
						}
					}
					bool flag8 = !this.comandoEjecutado;
					if (flag8)
					{
						this.cmdConfirmado = false;
						try
						{
							this.con = new OleDbConnection(Settings.Default.conexion);
							this.con.Open();
							OleDbCommand oleDbCommand2 = new OleDbCommand("SELECT *FROM ComandosCarpetas WHERE Comando LIKE '%" + _speech + "%'", this.con);
							OleDbDataReader oleDbDataReader2 = oleDbCommand2.ExecuteReader(CommandBehavior.SingleResult);
							bool flag9 = oleDbDataReader2.Read();
							if (flag9)
							{
								this.confirmarCmd = oleDbDataReader2["Comando"].ToString().Split(new char[]
								{
									'+'
								});
								string[] array2 = this.confirmarCmd;
								for (int j = 0; j < array2.Length; j++)
								{
									bool flag10 = _speech == array2[j];
									if (flag10)
									{
										this.cmdConfirmado = true;
									}
								}
								bool flag11 = this.cmdConfirmado;
								if (flag11)
								{
									this.Ejecutar = oleDbDataReader2["Ejecutar"].ToString();
									this.Respuesta = oleDbDataReader2["Respuesta"].ToString();
									this.multiRespuesta = this.Respuesta.Split(new char[]
									{
										'+'
									});
									Process.Start(this.Ejecutar);
									this.ranNum = this.rnd.Next(0, this.multiRespuesta.Count<string>());
									this.Fala.SpeakAsync(this.multiRespuesta[this.ranNum]);
									this.comandoEjecutado = true;
									this.TimerRecOFF = Settings.Default.cfgRec;
									return;
								}
							}
							this.con.Close();
						}
						catch
						{
							this.Fala.SpeakAsync(Settings.Default.cfgUser + "A pasta não pode ser localizada. ");
						}
					}
					bool flag12 = !this.comandoEjecutado;
					if (flag12)
					{
						this.cmdConfirmado = false;
						try
						{
							this.con = new OleDbConnection(Settings.Default.conexion);
							this.con.Open();
							OleDbCommand oleDbCommand3 = new OleDbCommand("SELECT *FROM ComandosAplicaciones WHERE Comando LIKE '%" + _speech + "%'", this.con);
							OleDbDataReader oleDbDataReader3 = oleDbCommand3.ExecuteReader(CommandBehavior.SingleResult);
							bool flag13 = oleDbDataReader3.Read();
							if (flag13)
							{
								this.confirmarCmd = oleDbDataReader3["Comando"].ToString().Split(new char[]
								{
									'+'
								});
								string[] array3 = this.confirmarCmd;
								for (int k = 0; k < array3.Length; k++)
								{
									bool flag14 = _speech == array3[k];
									if (flag14)
									{
										this.cmdConfirmado = true;
									}
								}
								bool flag15 = this.cmdConfirmado;
								if (flag15)
								{
									this.Ejecutar = oleDbDataReader3["Ejecutar"].ToString();
									this.Respuesta = oleDbDataReader3["Respuesta"].ToString();
									this.multiRespuesta = this.Respuesta.Split(new char[]
									{
										'+'
									});
									Process.Start(this.Ejecutar);
									this.ranNum = this.rnd.Next(0, this.multiRespuesta.Count<string>());
									this.Fala.SpeakAsync(this.multiRespuesta[this.ranNum]);
									this.comandoEjecutado = true;
									this.TimerRecOFF = Settings.Default.cfgRec;
									return;
								}
							}
							this.con.Close();
						}
						catch
						{
							this.Fala.SpeakAsync(Settings.Default.cfgUser + "O aplicativo não pode ser encontrado. ");
						}
					}
					bool flag16 = !this.comandoEjecutado;
					if (flag16)
					{
						this.cmdConfirmado = false;
						try
						{
							this.con = new OleDbConnection(Settings.Default.conexion);
							this.con.Open();
							OleDbCommand oleDbCommand4 = new OleDbCommand("SELECT *FROM ComandosPaginasWebs WHERE Comando LIKE '%" + _speech + "%'", this.con);
							OleDbDataReader oleDbDataReader4 = oleDbCommand4.ExecuteReader(CommandBehavior.SingleResult);
							bool flag17 = oleDbDataReader4.Read();
							if (flag17)
							{
								this.confirmarCmd = oleDbDataReader4["Comando"].ToString().Split(new char[]
								{
									'+'
								});
								string[] array4 = this.confirmarCmd;
								for (int l = 0; l < array4.Length; l++)
								{
									bool flag18 = _speech == array4[l];
									if (flag18)
									{
										this.cmdConfirmado = true;
									}
								}
								bool flag19 = this.cmdConfirmado;
								if (flag19)
								{
									this.Ejecutar = oleDbDataReader4["Ejecutar"].ToString();
									this.Respuesta = oleDbDataReader4["Respuesta"].ToString();
									this.multiRespuesta = this.Respuesta.Split(new char[]
									{
										'+'
									});
									Process.Start(this.Ejecutar);
									this.ranNum = this.rnd.Next(0, this.multiRespuesta.Count<string>());
									this.Fala.SpeakAsync(this.multiRespuesta[this.ranNum]);
									this.comandoEjecutado = true;
									this.TimerRecOFF = Settings.Default.cfgRec;
									return;
								}
							}
							this.con.Close();
						}
						catch (Exception)
						{
							this.Fala.SpeakAsync(Settings.Default.cfgUser + "A pagina não pode ser encontrada ou nem um navegador padrão foi configurado. ");
						}
					}
					bool flag20 = this.activeSerialPort;
					if (flag20)
					{
						string text = SerialDB.info_XmlCmdsSerial(_speech);
						bool flag21 = text != "no found";
						if (flag21)
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
								this.Fala.SpeakAsync(text.Split(new char[]
								{
									'+'
								})[3]);
								bool isOpen2 = serialPort.IsOpen;
								if (isOpen2)
								{
									serialPort.Close();
								}
							}
							catch
							{
								this.SpeakOut(new string[]
								{
									"porta não pode ser aberta" + text.Split(new char[]
									{
										'+'
									})[0] + ", verifique seu dispositivo"
								});
							}
							this.comandoEjecutado = true;
						}
					}
					bool flag22 = !this.comandoEjecutado && this.LoadtecladoOn;
					if (flag22)
					{
						this.vk = new MainWindow();
						string[] array5 = this.vk.PresionarTecla(_speech).Split(new char[]
						{
							'+'
						});
						bool flag23 = array5[0] == "true";
						if (flag23)
						{
							this.comandoEjecutado = true;
							this.TimerRecOFF = Settings.Default.cfgRec;
							bool flag24 = array5[1] != "";
							if (flag24)
							{
								this.Fala.SpeakAsync(array5[1]);
								this.vk = null;
								return;
							}
						}
						else
						{
							bool flag25 = array5[0] == "error";
							if (flag25)
							{
								this.Fala.SpeakAsync(array5[1]);
							}
						}
					}
					bool flag26 = !this.comandoEjecutado;
					if (flag26)
					{
						this.cmdConfirmado = false;
					}
					bool flag27 = !this.comandoEjecutado && this.LoadCmdAlar;
					if (flag27)
					{
						this.alarma();
					}
					bool flag28 = !this.comandoEjecutado && this.LoadCmdCal;
					if (flag28)
					{
						this.recordatorio(_speech);
					}
					bool flag29 = !this.comandoEjecutado && this.LoadCmdCal;
					if (flag29)
					{
						this.calculadora();
					}
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00026744 File Offset: 0x00024944
		private void desactivar(string _speeh)
		{
			bool flag = _speeh == "cancelar" || _speeh == "abortar";
			if (flag)
			{
				this.QEvent = "abortar";
				this.EnableShutdown = false;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000021D4 File Offset: 0x000003D4
		private void ElipseColorHud2_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.popupJarvis.IsOpen = true;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00026788 File Offset: 0x00024988
		private void EnableReconocimeinto(string _speech)
		{
			try
			{
				bool flag = _speech == this.nombreAI[0];
				if (flag)
				{
					this.lblSpeech2.Content = "Estou te ouvindo";
					this.ranNum = this.rnd.Next(1, 11);
					bool loadReponder = this.LoadReponder;
					if (loadReponder)
					{
						bool flag2 = this.ranNum == 1;
						if (flag2)
						{
							this.Fala.SpeakAsync(string.Concat(new string[]
							{
								"sim " + Settings.Default.cfgUser
							}));
						}
						else
						{
							bool flag3 = this.ranNum == 2;
							if (flag3)
							{
								this.Fala.SpeakAsync(string.Concat(new string[]
								{
									"A suas ordens " + Settings.Default.cfgUser
								}));
							}
							else
							{
								bool flag4 = this.ranNum == 3;
								if (flag4)
								{
									this.Fala.SpeakAsync(string.Concat(new string[]
									{
										"Diga " + Settings.Default.cfgUser
									}));
								}
								else
								{
									bool flag5 = this.ranNum == 4;
									if (flag5)
									{
										this.Fala.SpeakAsync(string.Concat(new string[]
										{
											"Aqui estou " + Settings.Default.cfgUser
										}));
									}
									else
									{
										bool flag6 = this.ranNum == 5;
										if (flag6)
										{
											this.Fala.SpeakAsync(string.Concat(new string[]
											{
												"Estou te ouvindo " + Settings.Default.cfgUser
											}));
										}
										else
										{
											bool flag7 = this.ranNum == 6;
											if (flag7)
											{
												this.Fala.SpeakAsync(string.Concat(new string[]
												{
													string.Concat(new string[]
													{
														"Diga "
													})
												}));
											}
											else
											{
												bool flag8 = this.ranNum == 7;
												if (flag8)
												{
													this.Fala.SpeakAsync(string.Concat(new string[]
													{
														string.Concat(new string[]
														{
															"Aqui estou "
														})
													}));
												}
												else
												{
													bool flag9 = this.ranNum == 8;
													if (flag9)
													{
														this.Fala.SpeakAsync(string.Concat(new string[]
														{
															string.Concat(new string[]
															{
																"A suas ordens "
															})
														}));
													}
													else
													{
														bool flag10 = this.ranNum == 9;
														if (flag10)
														{
															this.Fala.SpeakAsync(string.Concat(new string[]
															{
																string.Concat(new string[]
																{
																	"Sim "
																})
															}));
														}
														else
														{
															bool flag11 = this.ranNum == 10;
															if (flag11)
															{
																this.Fala.SpeakAsync(string.Concat(new string[]
																{
																	string.Concat(new string[]
																	{
																		"Estou te ouvindo "
																	})
																}));
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
					this.TimerRecOFF = this.LoadTimerRecOff;
					this.EnableComandos = true;
					this.comandoEjecutado = true;
					this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 160, 0));
					bool loadCbOpacidad = this.LoadCbOpacidad;
					if (loadCbOpacidad)
					{
						this.skin2.Opacity = 1.0;
						this.skin1.Opacity = 1.0;
					}
				}
				else
				{
					bool flag12 = !(_speech != this.nombreAI[1]) && this.EnableComandos;
					if (flag12)
					{
						this.TimerRecOFF = 0;
						this.comandoEjecutado = true;
					}
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				System.Windows.MessageBox.Show(ex2.Message, "AV", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00026B5C File Offset: 0x00024D5C
		private void FacebookGetFeed()
		{
			WebClient webClient = new WebClient();
			webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
			try
			{
				string text = webClient.DownloadString(Settings.Default.rutaRss);
				XDocument xdocument = XDocument.Parse(text);
				var enumerable = from story in xdocument.Descendants("item")
				select new
				{
					Title = (string)story.Element("title"),
					Link = (string)story.Element("link"),
					Description = (string)story.Element("description"),
					PubDate = (string)story.Element("pubDate")
				};
				this.MsgListF.Clear();
				this.MsgLinkF.Clear();
				this.pubDateF.Clear();
				foreach (var <>f__AnonymousType in enumerable)
				{
					bool flag = <>f__AnonymousType.Title != string.Empty;
					if (flag)
					{
						this.MsgListF.Add(<>f__AnonymousType.Title);
						this.MsgLinkF.Add(<>f__AnonymousType.Link);
						this.pubDateF.Add(<>f__AnonymousType.PubDate);
					}
					else
					{
						this.MsgListF.Add(<>f__AnonymousType.Title);
						this.MsgLinkF.Add(<>f__AnonymousType.Link);
						this.pubDateF.Add(<>f__AnonymousType.PubDate);
					}
					this.CargaDeFacebook = true;
				}
				string[] array = this.pubDateF[0].Split(new char[]
				{
					' '
				});
				string text2 = array[2];
				string text3 = text2;
				string text4 = text3;
				if (text4 != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text4);
					if (num <= 1153511100U)
					{
						if (num <= 328018950U)
						{
							if (num != 229007615U)
							{
								if (num == 328018950U)
								{
									if (text4 == "Fev")
									{
										array[2] = "02";
										goto IL_3D5;
									}
								}
							}
							else if (text4 == "Out")
							{
								array[2] = "10";
								goto IL_3D5;
							}
						}
						else if (num != 682729123U)
						{
							if (num != 1118301483U)
							{
								if (num == 1153511100U)
								{
									if (text4 == "Jul")
									{
										array[2] = "07";
										goto IL_3D5;
									}
								}
							}
							else if (text4 == "Mar")
							{
								array[2] = "03";
								goto IL_3D5;
							}
						}
						else if (text4 == "Set")
						{
							array[2] = "09";
							goto IL_3D5;
						}
					}
					else if (num <= 1269300054U)
					{
						if (num != 1187066338U)
						{
							if (num != 1190317742U)
							{
								if (num == 1269300054U)
								{
									if (text4 == "Mai")
									{
										array[2] = "05";
										goto IL_3D5;
									}
								}
							}
							else if (text4 == "Jan")
							{
								array[2] = "01";
								goto IL_3D5;
							}
						}
						else if (text4 == "Jun")
						{
							array[2] = "06";
							goto IL_3D5;
						}
					}
					else if (num != 2319303684U)
					{
						if (num != 2345834568U)
						{
							if (num == 2563502330U)
							{
								if (text4 == "Ago")
								{
									array[2] = "08";
									goto IL_3D5;
								}
							}
						}
						else if (text4 == "Abr")
						{
							array[2] = "04";
							goto IL_3D5;
						}
					}
					else if (text4 == "Nov")
					{
						array[2] = "11";
						goto IL_3D5;
					}
				}
				array[2] = "12";
				IL_3D5:
				string[] array2 = array[4].Split(new char[]
				{
					':'
				});
				DateTime dateTime = new DateTime(Convert.ToInt32(array[3]), Convert.ToInt32(array[2]), Convert.ToInt32(array[1]), Convert.ToInt32(array2[0]), Convert.ToInt32(array2[1]), Convert.ToInt32(array2[2]));
				bool flag2 = !(dateTime < Settings.Default.dateTime) && dateTime != Settings.Default.dateTime;
				if (flag2)
				{
					int i = 0;
					while (i < enumerable.Count())
					{
						string[] array3 = this.pubDateF[i].Split(new char[]
						{
							' '
						});
						string text5 = array3[2];
						string text6 = text5;
						string text7 = text6;
						if (text7 == null)
						{
							goto IL_6D3;
						}
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text7);
						if (num <= 1153511100U)
						{
							if (num <= 328018950U)
							{
								if (num != 229007615U)
								{
									if (num != 328018950U)
									{
										goto IL_6D3;
									}
									if (!(text7 == "Fev"))
									{
										goto IL_6D3;
									}
									array3[2] = "02";
								}
								else
								{
									if (!(text7 == "Out"))
									{
										goto IL_6D3;
									}
									array3[2] = "10";
								}
							}
							else if (num != 682729123U)
							{
								if (num != 1118301483U)
								{
									if (num != 1153511100U)
									{
										goto IL_6D3;
									}
									if (!(text7 == "Jul"))
									{
										goto IL_6D3;
									}
									array3[2] = "07";
								}
								else
								{
									if (!(text7 == "Mar"))
									{
										goto IL_6D3;
									}
									array3[2] = "03";
								}
							}
							else
							{
								if (!(text7 == "Set"))
								{
									goto IL_6D3;
								}
								array3[2] = "09";
							}
						}
						else if (num <= 1269300054U)
						{
							if (num != 1187066338U)
							{
								if (num != 1190317742U)
								{
									if (num != 1269300054U)
									{
										goto IL_6D3;
									}
									if (!(text7 == "Mai"))
									{
										goto IL_6D3;
									}
									array3[2] = "05";
								}
								else
								{
									if (!(text7 == "Jan"))
									{
										goto IL_6D3;
									}
									array3[2] = "01";
								}
							}
							else
							{
								if (!(text7 == "Jun"))
								{
									goto IL_6D3;
								}
								array3[2] = "06";
							}
						}
						else if (num != 2319303684U)
						{
							if (num != 2345834568U)
							{
								if (num != 2563502330U)
								{
									goto IL_6D3;
								}
								if (!(text7 == "Ago"))
								{
									goto IL_6D3;
								}
								array3[2] = "08";
							}
							else
							{
								if (!(text7 == "Abr"))
								{
									goto IL_6D3;
								}
								array3[2] = "04";
							}
						}
						else
						{
							if (!(text7 == "Nov"))
							{
								goto IL_6D3;
							}
							array3[2] = "11";
						}
						IL_6DF:
						string[] array4 = array3[4].Split(new char[]
						{
							':'
						});
						DateTime t = new DateTime(Convert.ToInt32(array3[3]), Convert.ToInt32(array3[2]), Convert.ToInt32(array3[1]), Convert.ToInt32(array4[0]), Convert.ToInt32(array4[1]), Convert.ToInt32(array4[2]));
						bool flag3 = t > Settings.Default.dateTime;
						if (flag3)
						{
							this.contadorNotificaciones++;
						}
						i++;
						continue;
						IL_6D3:
						array3[2] = "12";
						goto IL_6DF;
					}
					bool flag4 = this.contadorNotificaciones == 1;
					if (flag4)
					{
						bool flag5 = !this.lecturaInicio;
						if (flag5)
						{
							this.Fala.SpeakAsync(Settings.Default.cfgUser + " Você recebeu uma nova notificação." + this.MsgListF[0]);
						}
						else
						{
							this.Fala.Speak(Settings.Default.cfgUser + " Você recebeu uma nova notificação." + this.MsgListF[0]);
						}
					}
					else
					{
						bool flag6 = this.contadorNotificaciones > 1;
						if (flag6)
						{
							bool flag7 = !this.lecturaInicio;
							if (flag7)
							{
								this.Fala.SpeakAsync(string.Concat(new object[]
								{
									Settings.Default.cfgUser,
									"você recebeu ",
									this.contadorNotificaciones,
									" novas notificações"
								}));
							}
							else
							{
								this.Fala.Speak(string.Concat(new object[]
								{
									Settings.Default.cfgUser,
									"você recebeu ",
									this.contadorNotificaciones,
									" novas notificações"
								}));
							}
						}
					}
					Settings.Default.dateTime = dateTime;
					this.contadorNotificaciones = 0;
					Settings.Default.Save();
				}
				else
				{
					bool flag8 = !this.lecturaFacebook;
					if (flag8)
					{
						bool flag9 = !this.lecturaInicio;
						if (flag9)
						{
							this.Fala.SpeakAsync(Settings.Default.cfgUser + " você não recebeu novas notificações");
						}
						else
						{
							this.Fala.Speak(Settings.Default.cfgUser + "você não recebeu novas notificações");
						}
					}
				}
			}
			catch (Exception)
			{
				bool flag10 = !this.lecturaFacebook;
				if (flag10)
				{
					bool flag11 = !this.lecturaInicio;
					if (flag11)
					{
						this.Fala.SpeakAsync("Gerou um Erro, acesso inválido ao facebook, verifique sua conexão ou seu endereço RSS ");
					}
					else
					{
						this.Fala.Speak("Gerou um Erro, acesso inválido ao facebook, verifique sua conexão ou seu endereço RSS ");
					}
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00027518 File Offset: 0x00025718
		private string filtroNombreAsistente(string _speech)
		{
			this.nuevaFraseU = string.Empty;
			bool flag = _speech == "até " + Settings.Default.cfgAi || _speech == "até logo " + Settings.Default.cfgAi;
			if (flag)
			{
				this.nuevaFraseU = _speech;
			}
			else
			{
				string[] array = _speech.Split(new char[]
				{
					' '
				});
				for (int i = 0; i < array.Count<string>(); i++)
				{
					bool flag2 = !(array[i] != Settings.Default.cfgAi) && array.Count<string>() > 1;
					if (flag2)
					{
						this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 160, 0));
						bool cbOpacity = Settings.Default.cbOpacity;
						if (cbOpacity)
						{
							this.skin2.Opacity = 1.0;
							this.skin1.Opacity = 1.0;
						}
						this.TimerRecOFF = Settings.Default.cfgRec;
						this.EnableComandos = true;
						array[i] = string.Empty;
					}
					else
					{
						this.nuevaFraseU = this.nuevaFraseU + array[i] + " ";
					}
				}
			}
			return this.nuevaFraseU.Trim();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0002767C File Offset: 0x0002587C
		private string GetWatherNextDays(int day)
		{
			List<string> list = new List<string>();
			string result = string.Empty;
			string empty = string.Empty;
			string text = string.Empty;
			bool celcius = Settings.Default.celcius;
			if (celcius)
			{
				text = "graus celcius";
			}
			bool fahrenheit = Settings.Default.fahrenheit;
			if (fahrenheit)
			{
				text = "graus fahrenheit";
			}
			string uriString = string.Concat(new string[]
			{
				"http://api.openweathermap.org/data/2.5/forecast/daily?id=",
				Settings.Default.CfgCiudad,
				"&appid=",
				Settings.Default.ApiW,
				"&mode=xml",
				Settings.Default.grados,
				"&lang=pt&cnt=",
				day.ToString()
			});
			string xml = new WebClient().DownloadString(new Uri(uriString));
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			string innerText = xmlDocument.DocumentElement.SelectSingleNode("location").SelectSingleNode("name").InnerText;
			foreach (object obj in xmlDocument.SelectNodes("weatherdata/forecast/time"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				list.Add(string.Concat(new string[]
				{
					xmlNode.Attributes["day"].Value,
					"/",
					xmlNode.SelectSingleNode("temperature").Attributes["max"].Value,
					"/",
					xmlNode.SelectSingleNode("temperature").Attributes["min"].Value,
					"/",
					xmlNode.SelectSingleNode("humidity").Attributes["value"].Value,
					"/",
					xmlNode.SelectSingleNode("windSpeed").Attributes["mps"].Value
				}));
			}
			string[] array = new string[10];
			array[0] = "para amanhã";
			string[] array2 = array;
			array2[1] = list[1].Split(new char[]
			{
				'/'
			})[0].Split(new char[]
			{
				'-'
			})[2];
			array2[2] = ", a temperatura máxima será ";
			array2[3] = list[1].Split(new char[]
			{
				'/'
			})[1];
			array2[4] = " ";
			array2[5] = text;
			array2[6] = " e mínimo de ";
			array2[7] = list[1].Split(new char[]
			{
				'/'
			})[2];
			array2[8] = " ";
			array2[9] = text;
			result = string.Concat(array2);
			list.Clear();
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00027978 File Offset: 0x00025B78
		private string GetWeather(string temp)
		{
			string uriString = string.Empty;
			string result = string.Empty;
			string empty = string.Empty;
			string text = string.Empty;
			bool celcius = Settings.Default.celcius;
			if (celcius)
			{
				text = "graus celcius";
			}
			bool fahrenheit = Settings.Default.fahrenheit;
			if (fahrenheit)
			{
				text = "graus fahrenheit";
			}
			try
			{
				uriString = string.Concat(new string[]
				{
					"http://api.openweathermap.org/data/2.5/weather?id=",
					Settings.Default.CfgCiudad,
					"&appid=",
					Settings.Default.ApiW,
					"&mode=xml",
					Settings.Default.grados,
					"&lang=pt"
				});
				string xml = new WebClient().DownloadString(new Uri(uriString));
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				string value = xmlDocument.DocumentElement.SelectSingleNode("city").Attributes["name"].Value;
				string value2 = xmlDocument.DocumentElement.SelectSingleNode("weather").Attributes["value"].Value;
				string value3 = xmlDocument.DocumentElement.SelectSingleNode("temperature").Attributes["value"].Value;
				string value4 = xmlDocument.DocumentElement.SelectSingleNode("weather").Attributes["value"].Value;
				string text2 = value4;
				string value5 = xmlDocument.DocumentElement.SelectSingleNode("humidity").Attributes["value"].Value;
				string value6 = xmlDocument.DocumentElement.SelectSingleNode("wind").SelectSingleNode("speed").Attributes["value"].Value;
				string value7 = xmlDocument.DocumentElement.SelectSingleNode("city").Attributes["name"].Value;
				string value8 = xmlDocument.DocumentElement.SelectSingleNode("temperature").Attributes["max"].Value;
				string value9 = xmlDocument.DocumentElement.SelectSingleNode("temperature").Attributes["min"].Value;
				bool flag = temp == "today";
				if (flag)
				{
					result = string.Concat(new string[]
					{
						"Hoje, o tempo em ",
						value7,
						" é ",
						text2,
						", com uma temperatura de ",
						value3,
						" ",
						text,
						" com uma umidade de ",
						value5,
						" % e com ventos de ",
						value6,
						" metros por segundo"
					});
				}
				bool flag2 = temp == "temp";
				if (flag2)
				{
					result = "estamos com uma temperatura de " + value3 + " " + text;
				}
			}
			catch
			{
				result = "Não consigo encontrar as configurações da sua cidade, estou Sem acesso ao sistema, verifique seu código ou seu acesso à Internet";
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00027C94 File Offset: 0x00025E94
		private void Icon_DoubleClick(object sender, EventArgs e)
		{
			bool flag = base.WindowState == WindowState.Minimized;
			if (flag)
			{
				base.Show();
				base.WindowState = WindowState.Normal;
			}
			else
			{
				bool flag2 = base.WindowState == WindowState.Normal || base.WindowState == WindowState.Maximized;
				if (flag2)
				{
					base.WindowState = WindowState.Minimized;
					this.notifi();
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002351 File Offset: 0x00000551
		private void iconesdoassistente(object sender, EventArgs e)
		{
			base.Show();
			base.WindowState = WindowState.Normal;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00027CEC File Offset: 0x00025EEC
		public void notifi()
		{
			this.nicon.BalloonTipText = "estou aqui em sua barra de tarefas";
			this.nicon.BalloonTipTitle = "notificação Assistente Virtual®";
			this.nicon.BalloonTipIcon = ToolTipIcon.Info;
			this.nicon.Visible = true;
			this.nicon.ShowBalloonTip(5000);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00027D48 File Offset: 0x00025F48
		private void icono()
		{
			this.nicon.Visible = true;
			this.nicon.Icon = Resource1.x;
			this.nicon.ContextMenu = new System.Windows.Forms.ContextMenu();
			this.nicon.ContextMenu.MenuItems.Add("Configuração");
			this.nicon.ContextMenu.MenuItems.Add("Abrir aplicação", new EventHandler(this.iconesdoassistente));
			this.nicon.ContextMenu.MenuItems.Add(new System.Windows.Forms.MenuItem("Fechar", new EventHandler(this.Btnfechar_Click)));
			this.nicon.ContextMenu.MenuItems[0].Click += this.AVJARVIS_Click;
			this.nicon.Text = "ASSISTENTE VIRTUAL";
			this.nicon.ContextMenu.MenuItems[0].Click += this.Icon_DoubleClick;
			this.nicon.DoubleClick += this.Icon_DoubleClick;
		}

		// Token: 0x0600008D RID: 141
		[DllImport("user32.dll")]
		private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

		// Token: 0x0600008E RID: 142 RVA: 0x000021D4 File Offset: 0x000003D4
		private void lblAI_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.popupJarvis.IsOpen = true;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002254 File Offset: 0x00000454
		private void lblSpeech2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_skipe1.Play();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00027E6C File Offset: 0x0002606C
		private void lblSpeech2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.popupJarvis.IsOpen = false;
			bool flag = this.gridJarvis.Visibility == Visibility.Hidden;
			if (flag)
			{
				this.gridJarvis.Visibility = Visibility.Visible;
			}
			else
			{
				this.gridJarvis.Visibility = Visibility.Hidden;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000021D4 File Offset: 0x000003D4
		private void lblSpeech2_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.popupJarvis.IsOpen = true;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00027EBC File Offset: 0x000260BC
		private void loadDataRec()
		{
			try
			{
				this.sRec = new CDRecordatorio();
				this.listAllRec = this.sRec.cargarAllRecordatorios(System.Windows.Forms.Application.StartupPath + "\\DataBase");
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				System.Windows.MessageBox.Show(ex2.Message, "AV - Lembrete", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00027F24 File Offset: 0x00026124
		public void loadGramaNameAI()
		{
			this.nombreAI = new string[]
			{
				Settings.Default.cfgAi,
				"até " + Settings.Default.cfgAi
			};
			this._reco.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(this.nombreAI))));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00027F84 File Offset: 0x00026184
		public void loadGramars()
		{
			this.loadGramaNameAI();
			comabusque comabusque = new comabusque();
			bool cmdSearch = Settings.Default.CmdSearch;
			if (cmdSearch)
			{
				Choices choices = new Choices(new string[]
				{
					"google",
					"youtube",
					"wikipedia",
					"facebook",
					"yahoo",
					"twitter",
					"em google",
					"em youtube",
					"em wikipedia",
					"em facebook",
					"em yahoo",
					"em twitter",
					"no google",
					"no youtube",
					"no wikipedia",
					"no facebook",
					"no yahoo",
					"no twitter"
				});
				this._reco.LoadGrammar(new Grammar(choices));
				GrammarBuilder grammarBuilder = new GrammarBuilder(Settings.Default.cfgAi, 0, 1);
				grammarBuilder.Append(new Choices(new string[]
				{
					"buscar",
					"busca",
					"buscar por",
					"busca por",
					"Pesquisar por",
					"Pesquisa por",
					"Pesquisar",
					"Pesquisa",
					"Procurar",
					"Procura",
					"Procurar por",
					"Procura por"
				}));
				grammarBuilder.Append(new Choices(comabusque.prebuscador));
				grammarBuilder.Append(new Choices(new string[]
				{
					"em",
					"no"
				}), 0, 1);
				grammarBuilder.Append(new SemanticResultKey("searchs", new GrammarBuilder[]
				{
					choices
				}));
				GrammarBuilder grammarBuilder2 = new GrammarBuilder(Settings.Default.cfgAi, 0, 1);
				grammarBuilder2.Append(new Choices(new string[]
				{
					"buscar",
					"busca"
				}));
				grammarBuilder2.Append(new Choices(comabusque.prebuscador));
				grammarBuilder2.Append(new Choices(new string[]
				{
					"em",
					"no"
				}), 0, 1);
				grammarBuilder2.Append(new SemanticResultKey("searchs", new GrammarBuilder[]
				{
					choices
				}));
				GrammarBuilder grammarBuilder3 = new GrammarBuilder(Settings.Default.cfgAi, 0, 1);
				grammarBuilder3.Append(new SemanticResultKey("search", new GrammarBuilder[]
				{
					new Choices(new string[]
					{
						"busca",
						"buscar"
					})
				}));
				grammarBuilder3.Append(new Choices(comabusque.prebuscador));
				this._reco.LoadGrammarAsync(new Grammar(grammarBuilder));
				this._reco.LoadGrammarAsync(new Grammar(grammarBuilder2));
				this._reco.LoadGrammarAsync(new Grammar(grammarBuilder3));
			}
			Choices alternateChoices = new Choices(new string[]
			{
				"música",
				"canção",
				"faixa",
				"artista",
				"album",
				"melodía",
				"grupo"
			});
			GrammarBuilder grammarBuilder4 = new GrammarBuilder(new Choices(new string[]
			{
				"buscar",
				"busca",
				"reproduzir",
				"escutar"
			}));
			grammarBuilder4.Append(new Choices(new string[]
			{
				"em",
				"a"
			}), 0, 1);
			grammarBuilder4.Append(alternateChoices);
			grammarBuilder4.Append("de", 0, 1);
			grammarBuilder4.Append(new Choices(comabusque.prebuscador));
			Choices choices2 = new Choices();
			string[] array = this.fraseAlarma;
			for (int i = 0; i < array.Length; i++)
			{
				choices2.Add(new GrammarBuilder[]
				{
					new SemanticResultValue(array[i], "setAlarm")
				});
			}
			Choices choices3 = new Choices();
			int num = 0;
			string[] alarmFrase = this.AlarmFrase;
			for (int j = 0; j < alarmFrase.Length; j++)
			{
				choices3.Add(new GrammarBuilder[]
				{
					new SemanticResultValue(alarmFrase[j], this.AlarmValue[num])
				});
				num++;
			}
			num = 0;
			Choices choices4 = new Choices();
			string[] alarmValueP = this.AlarmValueP;
			for (int k = 0; k < alarmValueP.Length; k++)
			{
				choices4.Add(new GrammarBuilder[]
				{
					new SemanticResultValue(alarmValueP[k], this.AlarmValue[num])
				});
				num++;
			}
			bool cmdAlar = Settings.Default.CmdAlar;
			if (cmdAlar)
			{
				GrammarBuilder grammarBuilder5 = new GrammarBuilder();
				grammarBuilder5.Append(new SemanticResultKey("Alarm", new GrammarBuilder[]
				{
					choices2
				}));
				grammarBuilder5.Append(new SemanticResultKey("AlarmResult", new GrammarBuilder[]
				{
					choices3
				}));
				this._reco.LoadGrammarAsync(new Grammar(grammarBuilder5));
				GrammarBuilder grammarBuilder6 = new GrammarBuilder();
				grammarBuilder6.Append(new SemanticResultKey("Alarm", new GrammarBuilder[]
				{
					choices2
				}));
				grammarBuilder6.Append(new SemanticResultKey("AlarmResult", new GrammarBuilder[]
				{
					choices4
				}));
				this._reco.LoadGrammarAsync(new Grammar(grammarBuilder6));
				GrammarBuilder grammarBuilder7 = new GrammarBuilder();
				grammarBuilder7.Append(new Choices(new string[]
				{
					"as",
					"para as"
				}), 0, 1);
				grammarBuilder7.Append(new SemanticResultKey("horaExacta", new GrammarBuilder[]
				{
					choices3
				}));
				this._reco.LoadGrammarAsync(new Grammar(grammarBuilder7));
			}
			bool cmdRec = Settings.Default.CmdRec;
			if (cmdRec)
			{
				Choices choices5 = new Choices();
				for (int l = 0; l < this.numberString.Length; l++)
				{
					choices5.Add(new GrammarBuilder[]
					{
						new SemanticResultValue(this.numberString[l], l)
					});
				}
				Choices choices6 = new Choices(new GrammarBuilder[]
				{
					new SemanticResultValue("recuerdame", "record")
				});
				Choices choices7 = new Choices(this.recoDia);
				GrammarBuilder grammarBuilder8 = new GrammarBuilder();
				grammarBuilder8.Append(new SemanticResultKey("recdt", new GrammarBuilder[]
				{
					choices6
				}));
				grammarBuilder8.Append(new Choices(new string[]
				{
					"em",
					"para a",
					"para",
					"para este"
				}), 0, 1);
				grammarBuilder8.Append(new SemanticResultKey("dia", new GrammarBuilder[]
				{
					choices7
				}), 0, 1);
				grammarBuilder8.AppendDictation();
				grammarBuilder8.Append(new Choices(new string[]
				{
					"as"
				}), 0, 1);
				grammarBuilder8.Append(new SemanticResultKey("horaRecor", new GrammarBuilder[]
				{
					choices3
				}), 0, 1);
				this._reco.LoadGrammarAsync(new Grammar(grammarBuilder8));
				GrammarBuilder grammarBuilder9 = new GrammarBuilder();
				grammarBuilder9.Append(new SemanticResultKey("recdt", new GrammarBuilder[]
				{
					choices6
				}));
				grammarBuilder9.AppendDictation();
				grammarBuilder9.Append(new Choices(new string[]
				{
					"dentro de"
				}), 0, 1);
				grammarBuilder9.Append(new SemanticResultKey("horaMinuto", new GrammarBuilder[]
				{
					choices5
				}), 0, 1);
				grammarBuilder9.Append(new SemanticResultKey("time", new GrammarBuilder[]
				{
					new Choices(new string[]
					{
						"minutos",
						"hora",
						"horas"
					})
				}), 0, 1);
				this._reco.LoadGrammarAsync(new Grammar(grammarBuilder9));
				GrammarBuilder grammarBuilder10 = new GrammarBuilder();
				grammarBuilder10.Append(new SemanticResultKey("recdt", new GrammarBuilder[]
				{
					choices6
				}));
				grammarBuilder10.AppendDictation();
				grammarBuilder10.Append(new Choices(new string[]
				{
					"em",
					"para a",
					"para",
					"para este",
					"em a"
				}), 0, 1);
				grammarBuilder10.Append(new SemanticResultKey("dia", new GrammarBuilder[]
				{
					choices7
				}), 0, 1);
				grammarBuilder10.Append(new Choices(new string[]
				{
					"as"
				}), 0, 1);
				grammarBuilder10.Append(new SemanticResultKey("horaRecor", new GrammarBuilder[]
				{
					choices3
				}), 0, 1);
				this._reco.LoadGrammarAsync(new Grammar(grammarBuilder10));
				GrammarBuilder grammarBuilder11 = new GrammarBuilder();
				grammarBuilder11.Append(new SemanticResultKey("recdt", new GrammarBuilder[]
				{
					choices6
				}));
				grammarBuilder11.Append(new Choices(new string[]
				{
					"em",
					"para a",
					"para",
					"para este"
				}), 0, 1);
				grammarBuilder11.Append(new SemanticResultKey("dia", new GrammarBuilder[]
				{
					choices7
				}), 0, 1);
				grammarBuilder11.Append(new Choices(new string[]
				{
					"as"
				}), 0, 1);
				grammarBuilder11.Append(new SemanticResultKey("horaRecor", new GrammarBuilder[]
				{
					choices3
				}), 0, 1);
				grammarBuilder11.AppendDictation();
				this._reco.LoadGrammarAsync(new Grammar(grammarBuilder11));
			}
			bool cmdCalc = Settings.Default.CmdCalc;
			if (cmdCalc)
			{
				this._reco.LoadGrammarAsync(new Grammar(this.CreateGrammarCalc()));
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00028A20 File Offset: 0x00026C20
		private void preBuscarWeb(string _speech)
		{
			try
			{
				bool flag = this.completarBusqueda;
				if (flag)
				{
					bool flag2 = Settings.Default.Confidence > 0.5;
					if (flag2)
					{
						bool soundOn = Settings.Default.soundOn;
						if (soundOn)
						{
							SoundEffects.sonido.Play();
						}
						if (_speech != null)
						{
							uint num = <PrivateImplementationDetails>.ComputeStringHash(_speech);
							if (num <= 1698489344U)
							{
								if (num <= 1550741809U)
								{
									if (num != 484769588U)
									{
										if (num != 1124958004U)
										{
											if (num != 1550741809U)
											{
												goto IL_447;
											}
											if (!(_speech == "facebook"))
											{
												goto IL_447;
											}
										}
										else
										{
											if (!(_speech == "em twitter"))
											{
												goto IL_447;
											}
											goto IL_3D5;
										}
									}
									else
									{
										if (!(_speech == "cancelar"))
										{
											goto IL_447;
										}
										this.Fala.SpeakAsync("cancelado ");
										this.palabraTemporalBusqueda = "";
										this.completarBusqueda = false;
										goto IL_47C;
									}
								}
								else if (num != 1565098291U)
								{
									if (num != 1592966113U)
									{
										if (num != 1698489344U)
										{
											goto IL_447;
										}
										if (!(_speech == "em wikipedia"))
										{
											goto IL_447;
										}
										goto IL_337;
									}
									else if (!(_speech == "em facebook"))
									{
										goto IL_447;
									}
								}
								else
								{
									if (!(_speech == "em yahoo"))
									{
										goto IL_447;
									}
									goto IL_386;
								}
								Process.Start("https://www.facebook.com/search/results.php?q=" + this.palabraTemporalBusqueda);
								this.Fala.SpeakAsync("buscando por " + this.palabraTemporalBusqueda + "no faisbuc ");
								this.palabraTemporalBusqueda = "";
								this.completarBusqueda = false;
								goto IL_47C;
							}
							if (num > 2644829574U)
							{
								if (num <= 3275275844U)
								{
									if (num != 3083192990U)
									{
										if (num != 3275275844U)
										{
											goto IL_447;
										}
										if (!(_speech == "twitter"))
										{
											goto IL_447;
										}
										goto IL_3D5;
									}
									else if (!(_speech == "youtube"))
									{
										goto IL_447;
									}
								}
								else if (num != 3875681718U)
								{
									if (num != 4149437102U)
									{
										goto IL_447;
									}
									if (!(_speech == "em youtube"))
									{
										goto IL_447;
									}
								}
								else
								{
									if (!(_speech == "em google"))
									{
										goto IL_447;
									}
									goto IL_24A;
								}
								Process.Start("http://www.youtube.com/results?search_query=" + this.palabraTemporalBusqueda);
								this.Fala.SpeakAsync("buscando por " + this.palabraTemporalBusqueda + "no youtube ");
								this.palabraTemporalBusqueda = "";
								this.completarBusqueda = false;
								goto IL_47C;
							}
							if (num != 2103871344U)
							{
								if (num != 2255260227U)
								{
									if (num != 2644829574U)
									{
										goto IL_447;
									}
									if (!(_speech == "google"))
									{
										goto IL_447;
									}
								}
								else
								{
									if (!(_speech == "yahoo"))
									{
										goto IL_447;
									}
									goto IL_386;
								}
							}
							else
							{
								if (!(_speech == "wikipedia"))
								{
									goto IL_447;
								}
								goto IL_337;
							}
							IL_24A:
							Process.Start("https://www.google.com.br/search?q=" + this.palabraTemporalBusqueda);
							this.Fala.SpeakAsync("buscando por " + this.palabraTemporalBusqueda + "no google ");
							this.palabraTemporalBusqueda = "";
							this.completarBusqueda = false;
							goto IL_47C;
							IL_337:
							Process.Start("http://pt.wikipedia.org/wiki/" + this.palabraTemporalBusqueda);
							this.Fala.SpeakAsync("buscando por " + this.palabraTemporalBusqueda + "no wikipedia ");
							this.palabraTemporalBusqueda = "";
							this.completarBusqueda = false;
							goto IL_47C;
							IL_386:
							Process.Start("http://br.search.yahoo.com/search;_ylt=A86.Jo7py_JSL0QAb0tUcbt_;_ylc=X1MDMjE0Mjk5MDY3NgRfcgMyBGZyA3lmcC10LTcyNgRuX2dwcwMxMARvcmlnaW4DZXNwYW5vbC55YWhvby5jb20EcXVlcnkDbXVqZXJlcwRzYW8DMQ--?p=" + this.palabraTemporalBusqueda);
							this.Fala.SpeakAsync("buscando por " + this.palabraTemporalBusqueda + "no yahoo ");
							this.palabraTemporalBusqueda = "";
							this.completarBusqueda = false;
							goto IL_47C;
							IL_3D5:
							Process.Start("https://twitter.com/search?q=" + this.palabraTemporalBusqueda);
							this.Fala.SpeakAsync("buscando por " + this.palabraTemporalBusqueda + "no twitter ");
							this.palabraTemporalBusqueda = "";
							this.completarBusqueda = false;
							goto IL_47C;
						}
						IL_447:
						this.Fala.SpeakAsync(Settings.Default.cfgUser + " Não é um mecanismo de pesquisa disponível. cancelado ");
						this.palabraTemporalBusqueda = "";
						this.completarBusqueda = false;
						IL_47C:
						this.comandoEjecutado = true;
					}
				}
				bool flag3 = this.SpeechResult.Semantics.ContainsKey("searchs");
				if (flag3)
				{
					string[] array = _speech.Split(new char[]
					{
						' '
					});
					int num2 = array.Length;
					int num3 = 0;
					foreach (string a in array)
					{
						bool flag4 = a == "buscar" || a == "busca" || a == "Pesquisar" || a == "Pesquisa" || a == "Procurar" || a == "Procura";
						if (flag4)
						{
							array[num3] = "";
							this.condicionB = true;
						}
						else
						{
							bool flag5 = a == "em" || a == "no" || a == "por";
							if (flag5)
							{
								array[num3] = "";
							}
							else
							{
								bool flag6 = a == "google";
								if (flag6)
								{
									array[num3] = "";
									this.google = true;
								}
								else
								{
									bool flag7 = a == "youtube";
									if (flag7)
									{
										array[num3] = "";
										this.youtube = true;
									}
									else
									{
										bool flag8 = a == "facebook";
										if (flag8)
										{
											array[num3] = "";
											this.facebook = true;
										}
										else
										{
											bool flag9 = a == "wikipedia";
											if (flag9)
											{
												array[num3] = "";
												this.wikipedia = true;
											}
											else
											{
												bool flag10 = a == "yahoo";
												if (flag10)
												{
													array[num3] = "";
													this.yahoo = true;
												}
												else
												{
													bool flag11 = a == "twitter";
													if (flag11)
													{
														array[num3] = "";
														this.twitter = true;
													}
													else
													{
														bool flag12 = num3 == num2 - 1 && !this.twitter && !this.yahoo && !this.wikipedia && !this.facebook && !this.youtube && !this.google;
														if (flag12)
														{
															this.condicionB = false;
															return;
														}
													}
												}
											}
										}
									}
								}
							}
						}
						num3++;
					}
					bool flag13 = this.condicionB && this.google;
					if (flag13)
					{
						foreach (string text in array)
						{
							bool flag14 = text != "";
							if (flag14)
							{
								this.nuevaFraseB = this.nuevaFraseB + " " + text;
							}
						}
						Process.Start("https://www.google.com.br/search?q=" + this.nuevaFraseB);
						this.Fala.SpeakAsync("buscando por " + this.nuevaFraseB + "no google ");
					}
					else
					{
						bool flag15 = this.condicionB && this.youtube;
						if (flag15)
						{
							foreach (string text2 in array)
							{
								bool flag16 = text2 != "";
								if (flag16)
								{
									this.nuevaFraseB = this.nuevaFraseB + " " + text2;
								}
							}
							Process.Start("http://www.youtube.com/results?search_query=" + this.nuevaFraseB);
							this.Fala.SpeakAsync("buscando por " + this.nuevaFraseB + "no youtube ");
						}
						else
						{
							bool flag17 = this.condicionB && this.wikipedia;
							if (flag17)
							{
								foreach (string text3 in array)
								{
									bool flag18 = text3 != "";
									if (flag18)
									{
										this.nuevaFraseB = this.nuevaFraseB + " " + text3;
									}
								}
								Process.Start("http://pt.wikipedia.org/wiki/" + this.nuevaFraseB);
								this.Fala.SpeakAsync("buscando por " + this.nuevaFraseB + "no wikipedia ");
							}
							else
							{
								bool flag19 = this.condicionB && this.yahoo;
								if (flag19)
								{
									foreach (string text4 in array)
									{
										bool flag20 = text4 != "";
										if (flag20)
										{
											this.nuevaFraseB = this.nuevaFraseB + " " + text4;
										}
									}
									Process.Start("http://br.search.yahoo.com/search;_ylt=A86.Jo7py_JSL0QAb0tUcbt_;_ylc=X1MDMjE0Mjk5MDY3NgRfcgMyBGZyA3lmcC10LTcyNgRuX2dwcwMxMARvcmlnaW4DZXNwYW5vbC55YWhvby5jb20EcXVlcnkDbXVqZXJlcwRzYW8DMQ--?p=" + this.nuevaFraseB);
									this.Fala.SpeakAsync("buscando por " + this.nuevaFraseB + "no yahoo ");
								}
								else
								{
									bool flag21 = this.condicionB && this.facebook;
									if (flag21)
									{
										foreach (string text5 in array)
										{
											bool flag22 = text5 != "";
											if (flag22)
											{
												this.nuevaFraseB = this.nuevaFraseB + " " + text5;
											}
										}
										Process.Start("https://www.facebook.com/search/results.php?q=" + this.nuevaFraseB);
										this.Fala.SpeakAsync("buscando por " + this.nuevaFraseB + "no faisbuc ");
									}
									else
									{
										bool flag23 = !this.condicionB || !this.twitter;
										if (flag23)
										{
											return;
										}
										foreach (string text6 in array)
										{
											bool flag24 = text6 != "";
											if (flag24)
											{
												this.nuevaFraseB = this.nuevaFraseB + " " + text6;
											}
										}
										Process.Start("https://twitter.com/search?q=" + this.nuevaFraseB);
										this.Fala.SpeakAsync("buscando por " + this.nuevaFraseB + "no twitter ");
									}
								}
							}
						}
					}
					this.condicionB = false;
					this.google = false;
					this.youtube = false;
					this.yahoo = false;
					this.wikipedia = false;
					this.facebook = false;
					this.comandoEjecutado = true;
					this.TimerRecOFF = Settings.Default.cfgRec;
					this.nuevaFraseB = "";
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				System.Windows.MessageBox.Show(ex2.Message, "AV", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000295A8 File Offset: 0x000277A8
		public void LoadGramarTabla()
		{
			this._reco.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(this.frasesExtras))));
			GrammarBuilder grammarBuilder = new GrammarBuilder();
			grammarBuilder.Append(Settings.Default.cfgAi, 0, 1);
			grammarBuilder.Append(new Choices(this.frasesExtras));
			grammarBuilder.Append(Settings.Default.cfgAi, 0, 1);
			this.gfrasesEx = new Grammar(grammarBuilder);
			this._reco.LoadGrammarAsync(this.gfrasesEx);
			this._reco.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(this.Saludos))));
			GrammarBuilder grammarBuilder2 = new GrammarBuilder();
			grammarBuilder2.Append(Settings.Default.cfgAi, 0, 1);
			grammarBuilder2.Append(new Choices(this.Saludos));
			grammarBuilder2.Append(Settings.Default.cfgAi, 0, 1);
			this.gsaludos = new Grammar(grammarBuilder2);
			this._reco.LoadGrammarAsync(this.gsaludos);
			try
			{
				this.comandosArray = this.gr.loadGrammar(this.NAME_TABLA_APLICACIONES, "Comando", Settings.Default.conexion);
				bool flag = this.comandosArray.Count != 0;
				if (flag)
				{
					GrammarBuilder grammarBuilder3 = new GrammarBuilder();
					grammarBuilder3.Append(Settings.Default.cfgAi, 0, 1);
					grammarBuilder3.Append(new Choices(this.comandosArray.ToArray(typeof(string)) as string[]));
					grammarBuilder3.Append(Settings.Default.cfgAi, 0, 1);
					this.gListaComandoA = new Grammar(grammarBuilder3);
					this._reco.LoadGrammar(this.gListaComandoA);
					this.comandosArray.Clear();
				}
				this.comandosArray = this.gr.loadGrammar(this.NAME_TABLA_CARPETAS, "Comando", Settings.Default.conexion);
				bool flag2 = this.comandosArray.Count != 0;
				if (flag2)
				{
					GrammarBuilder grammarBuilder4 = new GrammarBuilder();
					grammarBuilder4.Append(Settings.Default.cfgAi, 0, 1);
					grammarBuilder4.Append(new Choices(this.comandosArray.ToArray(typeof(string)) as string[]));
					grammarBuilder4.Append(Settings.Default.cfgAi, 0, 1);
					this.gListaComandoC = new Grammar(grammarBuilder4);
					this._reco.LoadGrammar(this.gListaComandoC);
					this.comandosArray.Clear();
				}
				this.comandosArray = this.gr.loadGrammar(this.NAME_TABLA_SOCIALES, "Comando", Settings.Default.conexion);
				bool flag3 = this.comandosArray.Count != 0;
				if (flag3)
				{
					GrammarBuilder grammarBuilder5 = new GrammarBuilder();
					grammarBuilder5.Append(Settings.Default.cfgAi, 0, 1);
					grammarBuilder5.Append(new Choices(this.comandosArray.ToArray(typeof(string)) as string[]));
					grammarBuilder5.Append(Settings.Default.cfgAi, 0, 1);
					this.gListaComandoS = new Grammar(grammarBuilder5);
					this._reco.LoadGrammar(this.gListaComandoS);
					this.comandosArray.Clear();
				}
				this.comandosArray = this.gr.loadGrammar(this.NAME_TABLA_WEBS, "Comando", Settings.Default.conexion);
				bool flag4 = this.comandosArray.Count != 0;
				if (flag4)
				{
					GrammarBuilder grammarBuilder6 = new GrammarBuilder();
					grammarBuilder6.Append(Settings.Default.cfgAi, 0, 1);
					grammarBuilder6.Append(new Choices(this.comandosArray.ToArray(typeof(string)) as string[]));
					grammarBuilder6.Append(Settings.Default.cfgAi, 0, 1);
					this.gListaComandoP = new Grammar(grammarBuilder6);
					this._reco.LoadGrammar(this.gListaComandoP);
					this.comandosArray.Clear();
				}
				this.comandosArray = this.gr.loadGrammarDef(this.NAME_TABLA_DEFAULT, "Comando", Settings.Default.conexion1);
				GrammarBuilder grammarBuilder7 = new GrammarBuilder(Settings.Default.cfgAi, 0, 1);
				grammarBuilder7.Append(new Choices(this.comandosArray.ToArray(typeof(string)) as string[]));
				this.gALLComandos = new Grammar(grammarBuilder7);
				this._reco.LoadGrammar(this.gALLComandos);
				this.comandosArray.Clear();
				this.comandosArray = this.gr.loadGrammarDef(this.NAME_TABLA_DEFAULT, "Sinonimos", Settings.Default.conexion1);
				GrammarBuilder grammarBuilder8 = new GrammarBuilder(Settings.Default.cfgAi, 0, 1);
				grammarBuilder8.Append(new Choices(this.comandosArray.ToArray(typeof(string)) as string[]));
				this.gALLComandosS = new Grammar(grammarBuilder8);
				this._reco.LoadGrammar(this.gALLComandosS);
				this.comandosArray.Clear();
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00029B08 File Offset: 0x00027D08
		public void LoadGramarTablaKey()
		{
			MainWindow mainWindow = new MainWindow();
			this.comandosArray = this.gr.loadGrammar(Settings.Default.TablakeyDefault, "Comando", mainWindow.RUTA_DATA_BASE_KEY);
			Choices alternateChoices = new Choices(this.comandosArray.ToArray(typeof(string)) as string[]);
			bool flag = this.comandosArray.Count != 0;
			if (flag)
			{
				GrammarBuilder grammarBuilder = new GrammarBuilder();
				grammarBuilder.Append(Settings.Default.cfgAi, 0, 1);
				grammarBuilder.Append(alternateChoices);
				this.cmdKey = new Grammar(grammarBuilder);
				this._reco.LoadGrammar(this.cmdKey);
				this.comandosArray.Clear();
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00029BC8 File Offset: 0x00027DC8
		private void loadInterface()
		{
			bool cfgHud = Settings.Default.cfgHud;
			if (cfgHud)
			{
				this.intf = new Interface();
				this.intf.Closed += delegate(object a, EventArgs b)
				{
					this.intf = null;
					this.LoadTimerRecOff = Settings.Default.cfgRec;
					this.LoadReponder = Settings.Default.Responder;
					this.LoadCbOpacidad = Settings.Default.cbOpacity;
					this.cfgUser = Settings.Default.cfgUser;
					this.LoadCmdSearch = Settings.Default.CmdSearch;
					this.LoadCmdCal = Settings.Default.CmdCalc;
					this.LoadCmdAlar = Settings.Default.CmdAlar;
					this.LoadtecladoOn = Settings.Default.ActTecl;
				};
				this.intf.ShowDialog();
			}
		}

		// Token: 0x06000099 RID: 153
		[DllImport("user32.dll")]
		private static extern byte MapVirtualKey(uint uCode, uint uMapType);

		// Token: 0x0600009A RID: 154 RVA: 0x00029C18 File Offset: 0x00027E18
		private void mostrarConfiguracion()
		{
			this.TimerRecOFF = 0;
			this.ventanaOpen = true;
			bool flag = this.intf == null;
			if (flag)
			{
				this.intf = new Interface();
				this.TimerRecOFF = 0;
				this.intf.Closed += delegate(object a, EventArgs b)
				{
					this.Fala.SelectVoice(Settings.Default.vozDefault);
					this.Fala.SpeakAsync("atualizando os comandos ");
					this.UnloadGramarTabla();
					this.UnloadGrammarTablaKey();
					this.LoadGramarTabla();
					this.LoadGramarTablaKey();
					this.Fala.SpeakAsync("em linha ");
					this.loadDataRec();
					this.intf = null;
					base.Visibility = Visibility.Visible;
					this.ventanaOpen = false;
					this.btnJarvis.Content = Settings.Default.cfgAi.ToUpper();
					this.btnJarvis2.Content = Settings.Default.cfgAi.ToUpper();
					this.cfgUser = Settings.Default.cfgUser;
					this.LoadTimerRecOff = Settings.Default.cfgRec;
					this.LoadReponder = Settings.Default.Responder;
					this.LoadCbOpacidad = Settings.Default.cbOpacity;
					this.LoadCmdSearch = Settings.Default.CmdSearch;
					this.LoadCmdCal = Settings.Default.CmdCalc;
					this.LoadCmdAlar = Settings.Default.CmdAlar;
					this.LoadtecladoOn = Settings.Default.ActTecl;
				};
				this.intf.WindowState = WindowState.Normal;
				this.intf.Show();
				base.Visibility = Visibility.Hidden;
			}
			else
			{
				this.intf.WindowState = WindowState.Normal;
				this.intf.Show();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002363 File Offset: 0x00000563
		public void mutewin()
		{
			AVJARVIS.keybd_event(173, AVJARVIS.MapVirtualKey(173U, 0U), 1U, 0U);
			AVJARVIS.keybd_event(173, AVJARVIS.MapVirtualKey(173U, 0U), 3U, 0U);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00029CAC File Offset: 0x00027EAC
		private void numeroCuentas()
		{
			int num = Settings.Default.checkGmail ? 1 : 0;
			int num2 = Settings.Default.checkOutlook ? 1 : 0;
			this.cuentas = num + num2 + (Settings.Default.checkYahoo ? 1 : 0) + (Settings.Default.checkEmail ? 1 : 0);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00029D08 File Offset: 0x00027F08
		private void offLectura(string speech)
		{
			bool flag = speech == "cancelar" || speech == "pare de ler" || speech == "silêncio" || speech == "para" || speech == "cancelar leitura" || speech == "para leitura" || speech == "até ai" || speech == "não leia mais";
			if (flag)
			{
				this.StopSpeak();
				this.TimerRecOFF = Settings.Default.cfgRec;
				this.EnableComandos = true;
				this.comandoEjecutado = true;
				bool soundOn = Settings.Default.soundOn;
				if (soundOn)
				{
					Settings.Default.soundOn = true;
				}
				Settings.Default.Save();
				this.lblSpeech2.Content = "em linha";
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002396 File Offset: 0x00000596
		private void pbMicro2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.clicSlider = true;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00029DE8 File Offset: 0x00027FE8
		private void pbMicro2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			bool flag = this.clicSlider;
			if (flag)
			{
				bool flag2 = this.gridJarvis.Visibility == Visibility.Hidden;
				if (flag2)
				{
					this.gridJarvis.Visibility = Visibility.Visible;
				}
				else
				{
					this.gridJarvis.Visibility = Visibility.Hidden;
				}
				this.clicSlider = false;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000023A0 File Offset: 0x000005A0
		private void posiçãodeiniciovoz()
		{
			this.Fala.SpeakAsync("atualizando os novos comandos");
			this.Fala.SpeakAsync("em linha");
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00029E3C File Offset: 0x0002803C
		private void inicialização()
		{
			DateTime now = DateTime.Now;
			DateTime now2 = DateTime.Now;
			bool flag = now.Hour <= 11 && now.Hour < 24;
			if (flag)
			{
				this.Hora = now.ToString("hh:mm tt");
				string[] array = new string[1];
				string[] array2 = new string[5];
				array2[0] = "bom dia " + Settings.Default.cfgUser + "são ";
				string[] array3 = array2;
				array3[1] = now.Hour.ToString();
				array3[2] = " horas da manhã e ";
				array3[3] = now.Minute.ToString();
				array3[4] = " minutos";
				array[0] = string.Concat(array3);
				this.SpeakOut(array);
			}
			else
			{
				bool flag2 = now.Hour > 11 && now.Hour < 18;
				if (flag2)
				{
					this.Hora = now.ToString("hh:mm tt");
					string[] array4 = new string[1];
					string[] array5 = new string[5];
					array5[0] = "boa tarde " + Settings.Default.cfgUser + "são ";
					string[] array6 = array5;
					array6[1] = now.Hour.ToString();
					array6[2] = "horas da tarde e ";
					array6[3] = now.Minute.ToString();
					array6[4] = "minutos ";
					array4[0] = string.Concat(array6);
					this.SpeakOut(array4);
				}
				else
				{
					bool flag3 = now.Hour <= 18 || now.Hour < 24;
					if (flag3)
					{
						this.Hora = now.ToString("hh:mm tt");
						string[] array7 = new string[1];
						string[] array8 = new string[5];
						array8[0] = "boa noite " + Settings.Default.cfgUser + "são ";
						string[] array9 = array8;
						array9[1] = now.Hour.ToString();
						array9[2] = "horas da noite e ";
						array9[3] = now.Minute.ToString();
						array9[4] = "minutos ";
						array7[0] = string.Concat(array9);
						this.SpeakOut(array7);
					}
					else
					{
						string[] array10 = new string[1];
						string[] array11 = new string[5];
						array11[0] = "Olá! " + Settings.Default.cfgUser + "agora são";
						string[] array12 = array11;
						array12[1] = now.Hour.ToString();
						array12[2] = " horas e ";
						array12[3] = now.Minute.ToString();
						array12[4] = " minutos";
						array10[0] = string.Concat(array12);
						this.SpeakOut(array10);
					}
				}
			}
			bool flag4 = Settings.Default.cLun || Settings.Default.cMar || Settings.Default.cMie || Settings.Default.cJue || Settings.Default.cVie || Settings.Default.cSab || Settings.Default.cDom;
			if (flag4)
			{
				this.SpeakOut(new string[]
				{
					string.Concat(new string[]
					{
						"O Alarme está Ativado "
					})
				});
			}
			else
			{
				this.SpeakOut(new string[]
				{
					"O Alarme está desligado "
				});
			}
			this.SpeakOut(new string[]
			{
				string.Concat(new string[]
				{
					"Hoje é... ",
					now2.ToString("dddd"),
					" ",
					now2.ToString("dd"),
					" de ",
					now2.ToString("MMMM"),
					" de ",
					now2.ToString("yyyy")
				})
			});
			bool celcius = Settings.Default.celcius;
			if (celcius)
			{
				Task.Run(delegate()
				{
					this.SpeakOut(new string[]
					{
						this.GetWeather("temp")
					});
				});
			}
			else
			{
				bool fahrenheit = Settings.Default.fahrenheit;
				if (fahrenheit)
				{
					Task.Run(delegate()
					{
						this.SpeakOut(new string[]
						{
							this.GetWeather("today")
						});
					});
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0002A244 File Offset: 0x00028444
		private void reconocimiento()
		{
			try
			{
				Thread thread = new Thread(new ThreadStart(this.cargaGramaticas));
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
				this.inicialização();
				this.SpeakOutStop(new string[]
				{
					"As configurações estão sendo carregadas. Só mais um segundo... "
				});
				thread.Join();
				this._reco.SetInputToDefaultAudioDevice();
				this._reco.RequestRecognizerUpdate();
				this._reco.SpeechRecognized += this._reco_SpeechRecognized;
				this._reco.AudioLevelUpdated += this._reco_AudioLevelUpdated;
				this._reco.AudioStateChanged += this._reco_AudioStateChanged;
				this._reco.MaxAlternates = 10;
				this.Fala.SpeakStarted += this._jarvis_SpeakStarted;
				this.Fala.SpeakCompleted += this._jarvis_SpeakCompleted;
				this.Fala.SpeakProgress += this._jarvis_SpeakProgress;
				this.configuracionTimer();
				this.Timer3.Start();
				this.Timer2.Start();
				this.loadDataRec();
				this.btnJarvis.Content = Settings.Default.cfgAi.ToUpper();
				this.btnJarvis2.Content = Settings.Default.cfgAi.ToUpper();
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				System.Windows.MessageBox.Show(ex2.Message, "AV", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.Fala.Speak("em linha ");
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0002A3F4 File Offset: 0x000285F4
		private void recordatorio(string _speech)
		{
			bool flag = this.SpeechResult.Semantics.ContainsKey("recdt");
			if (flag)
			{
				this.horaVer = false;
				this.diaDef = false;
				DateTime now = DateTime.Now;
				string text = now.ToString("D").Split(new char[]
				{
					','
				})[0];
				int num = 0;
				this.horaR = now.ToString("t").Split(new char[]
				{
					' '
				})[0] + ":00 " + now.ToString("t", new CultureInfo("en-US")).Split(new char[]
				{
					' '
				})[1];
				string text2 = text;
				bool flag2 = text2 != null;
				if (flag2)
				{
					string text3 = text2;
					string text4 = text3;
					if (text4 != null)
					{
						uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text4);
						if (num2 <= 1730344118U)
						{
							if (num2 != 576910943U)
							{
								if (num2 != 1569335225U)
								{
									if (num2 == 1730344118U)
									{
										if (text4 == "domingo")
										{
											num = 7;
										}
									}
								}
								else if (text4 == "sábado")
								{
									num = 6;
								}
							}
							else if (text4 == "quinta")
							{
								num = 4;
							}
						}
						else if (num2 <= 2034440470U)
						{
							if (num2 != 2019488408U)
							{
								if (num2 == 2034440470U)
								{
									if (text4 == "segunda")
									{
										num = 1;
									}
								}
							}
							else if (text4 == "sexta")
							{
								num = 5;
							}
						}
						else if (num2 != 2325360483U)
						{
							if (num2 == 3718793676U)
							{
								if (text4 == "terça")
								{
									num = 2;
								}
							}
						}
						else if (text4 == "quarta")
						{
							num = 3;
						}
					}
				}
				string text5 = "hoje";
				try
				{
					this.diaR = now.ToString("d").Split(new char[]
					{
						' '
					})[0];
					text5 = this.SpeechResult.Semantics["dia"].Value.ToString();
					string text6 = text5;
					bool flag3 = text6 != null;
					if (flag3)
					{
						string text7 = text6;
						string text8 = text7;
						if (text8 != null)
						{
							uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text8);
							if (num2 <= 2095462137U)
							{
								if (num2 <= 932406682U)
								{
									if (num2 <= 546725944U)
									{
										if (num2 <= 229721485U)
										{
											if (num2 != 189786771U)
											{
												if (num2 != 229721485U)
												{
													goto IL_153B;
												}
												if (!(text8 == "Sexta à noite"))
												{
													goto IL_153B;
												}
												bool flag4 = 5 - num > 0;
												if (flag4)
												{
													this.diaR = now.AddDays((double)(5 - num)).ToString("d").Split(new char[]
													{
														' '
													})[0];
												}
												else
												{
													num = 7 - num + 5;
													this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
													{
														' '
													})[0];
												}
												this.horaR = "7:00:00 PM";
												this.diaDef = true;
												goto IL_153B;
											}
											else
											{
												if (!(text8 == "Quarta à tarde"))
												{
													goto IL_153B;
												}
												bool flag5 = 3 - num > 0;
												if (flag5)
												{
													this.diaR = now.AddDays((double)(3 - num)).ToString("d").Split(new char[]
													{
														' '
													})[0];
												}
												else
												{
													num = 7 - num + 3;
													this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
													{
														' '
													})[0];
												}
												this.horaR = "2:00:00 PM";
												this.diaDef = true;
												goto IL_153B;
											}
										}
										else if (num2 != 436394582U)
										{
											if (num2 != 546725944U)
											{
												goto IL_153B;
											}
											if (!(text8 == "Sexta"))
											{
												goto IL_153B;
											}
											goto IL_1013;
										}
										else
										{
											if (!(text8 == "Domingo"))
											{
												goto IL_153B;
											}
											goto IL_1385;
										}
									}
									else if (num2 <= 832193241U)
									{
										if (num2 != 679308887U)
										{
											if (num2 != 832193241U)
											{
												goto IL_153B;
											}
											if (!(text8 == "Sábado"))
											{
												goto IL_153B;
											}
											goto IL_11CC;
										}
										else
										{
											if (!(text8 == "tarde"))
											{
												goto IL_153B;
											}
											this.diaR = now.ToString("d").Split(new char[]
											{
												' '
											})[0];
											this.horaR = "2:00:00 PM";
											this.diaDef = true;
											goto IL_153B;
										}
									}
									else if (num2 != 864828525U)
									{
										if (num2 != 932406682U)
										{
											goto IL_153B;
										}
										if (!(text8 == "terça de manhã"))
										{
											goto IL_153B;
										}
										goto IL_AE8;
									}
									else
									{
										if (!(text8 == "amanhã à tarde"))
										{
											goto IL_153B;
										}
										this.diaR = now.AddDays(1.0).ToString("d").Split(new char[]
										{
											' '
										})[0];
										this.horaR = "2:00:00 PM";
										this.diaDef = true;
										goto IL_153B;
									}
								}
								else if (num2 <= 1530268020U)
								{
									if (num2 <= 1439473363U)
									{
										if (num2 != 1319416718U)
										{
											if (num2 != 1439473363U)
											{
												goto IL_153B;
											}
											if (!(text8 == "Domingo à noite"))
											{
												goto IL_153B;
											}
											bool flag6 = 7 - num > 0;
											if (flag6)
											{
												this.diaR = now.AddDays((double)(7 - num)).ToString("d").Split(new char[]
												{
													' '
												})[0];
											}
											else
											{
												num = 7 - num + 7;
												this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
												{
													' '
												})[0];
											}
											this.horaR = "7:00:00 PM";
											this.diaDef = true;
											goto IL_153B;
										}
										else
										{
											if (!(text8 == "segunda à tarde"))
											{
												goto IL_153B;
											}
											bool flag7 = 1 - num > 0;
											if (flag7)
											{
												this.diaR = now.AddDays((double)(1 - num)).ToString("d").Split(new char[]
												{
													' '
												})[0];
											}
											else
											{
												num = 7 - num + 1;
												this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
												{
													' '
												})[0];
											}
											this.horaR = "2:00:00 PM";
											this.diaDef = true;
											goto IL_153B;
										}
									}
									else if (num2 != 1517141376U)
									{
										if (num2 != 1530268020U)
										{
											goto IL_153B;
										}
										if (!(text8 == "Sexta à tarde"))
										{
											goto IL_153B;
										}
										bool flag8 = 5 - num > 0;
										if (flag8)
										{
											this.diaR = now.AddDays((double)(5 - num)).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										else
										{
											num = 7 - num + 5;
											this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										this.horaR = "2:00:00 PM";
										this.diaDef = true;
										goto IL_153B;
									}
									else
									{
										if (!(text8 == "Terça à tarde"))
										{
											goto IL_153B;
										}
										bool flag9 = 2 - num > 0;
										if (flag9)
										{
											this.diaR = now.AddDays((double)(2 - num)).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										else
										{
											num = 7 - num + 2;
											this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										this.horaR = "2:00:00 PM";
										this.diaDef = true;
										goto IL_153B;
									}
								}
								else if (num2 <= 2034440470U)
								{
									if (num2 != 1794272383U)
									{
										if (num2 != 2034440470U)
										{
											goto IL_153B;
										}
										if (!(text8 == "segunda"))
										{
											goto IL_153B;
										}
									}
									else
									{
										if (!(text8 == "Quinta"))
										{
											goto IL_153B;
										}
										goto IL_E5A;
									}
								}
								else if (num2 != 2089494995U)
								{
									if (num2 != 2095462137U)
									{
										goto IL_153B;
									}
									if (!(text8 == "amanhã"))
									{
										goto IL_153B;
									}
									this.diaR = now.AddDays(1.0).ToString("d").Split(new char[]
									{
										' '
									})[0];
									this.horaR = "9:00:00 AM";
									this.diaDef = true;
									goto IL_153B;
								}
								else
								{
									if (!(text8 == "Quarta de manhã"))
									{
										goto IL_153B;
									}
									goto IL_CA1;
								}
							}
							else if (num2 <= 3404519118U)
							{
								if (num2 <= 2334187226U)
								{
									if (num2 <= 2234784190U)
									{
										if (num2 != 2152483942U)
										{
											if (num2 != 2234784190U)
											{
												goto IL_153B;
											}
											if (!(text8 == "Sexta de manhã"))
											{
												goto IL_153B;
											}
											goto IL_1013;
										}
										else
										{
											if (!(text8 == "noite"))
											{
												goto IL_153B;
											}
											this.diaR = now.ToString("d").Split(new char[]
											{
												' '
											})[0];
											this.horaR = "7:00:00 PM";
											this.diaDef = true;
											goto IL_153B;
										}
									}
									else if (num2 != 2318063745U)
									{
										if (num2 != 2334187226U)
										{
											goto IL_153B;
										}
										if (!(text8 == "Quarta à noite"))
										{
											goto IL_153B;
										}
										bool flag10 = 3 - num > 0;
										if (flag10)
										{
											this.diaR = now.AddDays((double)(3 - num)).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										else
										{
											num = 7 - num + 3;
											this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										this.horaR = "7:00:00 PM";
										this.diaDef = true;
										goto IL_153B;
									}
									else
									{
										if (!(text8 == "Terça à noite"))
										{
											goto IL_153B;
										}
										bool flag11 = 2 - num > 0;
										if (flag11)
										{
											this.diaR = now.AddDays((double)(2 - num)).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										else
										{
											num = 7 - num + 2;
											this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										this.horaR = "7:00:00 PM";
										this.diaDef = true;
										goto IL_153B;
									}
								}
								else if (num2 <= 2759630151U)
								{
									if (num2 != 2432737328U)
									{
										if (num2 != 2759630151U)
										{
											goto IL_153B;
										}
										if (!(text8 == "Quinta de manhã"))
										{
											goto IL_153B;
										}
										goto IL_E5A;
									}
									else
									{
										if (!(text8 == "Sábado à noite"))
										{
											goto IL_153B;
										}
										bool flag12 = 6 - num > 0;
										if (flag12)
										{
											this.diaR = now.AddDays((double)(6 - num)).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										else
										{
											num = 7 - num + 6;
											this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										this.horaR = "7:00:00 PM";
										this.diaDef = true;
										goto IL_153B;
									}
								}
								else if (num2 != 3085039815U)
								{
									if (num2 != 3404519118U)
									{
										goto IL_153B;
									}
									if (!(text8 == "Domingo à tarde"))
									{
										goto IL_153B;
									}
									bool flag13 = 7 - num > 0;
									if (flag13)
									{
										this.diaR = now.AddDays((double)(7 - num)).ToString("d").Split(new char[]
										{
											' '
										})[0];
									}
									else
									{
										num = 7 - num + 7;
										this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
										{
											' '
										})[0];
									}
									this.horaR = "2:00:00 PM";
									this.diaDef = true;
									goto IL_153B;
								}
								else
								{
									if (!(text8 == "Quinta à tarde"))
									{
										goto IL_153B;
									}
									bool flag14 = 4 - num > 0;
									if (flag14)
									{
										this.diaR = now.AddDays((double)(4 - num)).ToString("d").Split(new char[]
										{
											' '
										})[0];
									}
									else
									{
										num = 7 - num + 4;
										this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
										{
											' '
										})[0];
									}
									this.horaR = "2:00:00 PM";
									this.diaDef = true;
									goto IL_153B;
								}
							}
							else if (num2 <= 3754868048U)
							{
								if (num2 <= 3501042051U)
								{
									if (num2 != 3483545590U)
									{
										if (num2 != 3501042051U)
										{
											goto IL_153B;
										}
										if (!(text8 == "Quarta"))
										{
											goto IL_153B;
										}
										goto IL_CA1;
									}
									else
									{
										if (!(text8 == "Quinta à noite"))
										{
											goto IL_153B;
										}
										bool flag15 = 4 - num > 0;
										if (flag15)
										{
											this.diaR = now.AddDays((double)(4 - num)).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										else
										{
											num = 7 - num + 4;
											this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
											{
												' '
											})[0];
										}
										this.horaR = "7:00:00 PM";
										this.diaDef = true;
										goto IL_153B;
									}
								}
								else if (num2 != 3718793676U)
								{
									if (num2 != 3754868048U)
									{
										goto IL_153B;
									}
									if (!(text8 == "amanhã à noite"))
									{
										goto IL_153B;
									}
									this.diaR = now.AddDays(1.0).ToString("d").Split(new char[]
									{
										' '
									})[0];
									this.horaR = "7:00:00 PM";
									this.diaDef = true;
									goto IL_153B;
								}
								else
								{
									if (!(text8 == "terça"))
									{
										goto IL_153B;
									}
									goto IL_AE8;
								}
							}
							else if (num2 <= 4032115859U)
							{
								if (num2 != 3980852365U)
								{
									if (num2 != 4032115859U)
									{
										goto IL_153B;
									}
									if (!(text8 == "segunda à noite"))
									{
										goto IL_153B;
									}
									bool flag16 = 1 - num > 0;
									if (flag16)
									{
										this.diaR = now.AddDays((double)(1 - num)).ToString("d").Split(new char[]
										{
											' '
										})[0];
									}
									else
									{
										num = 7 - num + 1;
										this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
										{
											' '
										})[0];
									}
									this.horaR = "7:00:00 PM";
									this.diaDef = true;
									goto IL_153B;
								}
								else
								{
									if (!(text8 == "Sábado de manhã"))
									{
										goto IL_153B;
									}
									goto IL_11CC;
								}
							}
							else if (num2 != 4114935272U)
							{
								if (num2 != 4262122573U)
								{
									if (num2 != 4283441064U)
									{
										goto IL_153B;
									}
									if (!(text8 == "segunda de manhã"))
									{
										goto IL_153B;
									}
								}
								else
								{
									if (!(text8 == "Sábado à tarde"))
									{
										goto IL_153B;
									}
									bool flag17 = 6 - num > 0;
									if (flag17)
									{
										this.diaR = now.AddDays((double)(6 - num)).ToString("d").Split(new char[]
										{
											' '
										})[0];
									}
									else
									{
										num = 7 - num + 6;
										this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
										{
											' '
										})[0];
									}
									this.horaR = "2:00:00 PM";
									this.diaDef = true;
									goto IL_153B;
								}
							}
							else
							{
								if (!(text8 == "Domingo de manhã"))
								{
									goto IL_153B;
								}
								goto IL_1385;
							}
							bool flag18 = 1 - num > 0;
							if (flag18)
							{
								this.diaR = now.AddDays((double)(1 - num)).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							else
							{
								num = 7 - num + 1;
								this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							this.horaR = "9:00:00 AM";
							this.diaDef = true;
							goto IL_153B;
							IL_AE8:
							bool flag19 = 2 - num > 0;
							if (flag19)
							{
								this.diaR = now.AddDays((double)(2 - num)).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							else
							{
								num = 7 - num + 2;
								this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							this.horaR = "9:00:00 AM";
							this.diaDef = true;
							goto IL_153B;
							IL_CA1:
							bool flag20 = 3 - num > 0;
							if (flag20)
							{
								this.diaR = now.AddDays((double)(3 - num)).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							else
							{
								num = 7 - num + 3;
								this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							this.horaR = "9:00:00 AM";
							this.diaDef = true;
							goto IL_153B;
							IL_E5A:
							bool flag21 = 4 - num > 0;
							if (flag21)
							{
								this.diaR = now.AddDays((double)(4 - num)).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							else
							{
								num = 7 - num + 4;
								this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							this.horaR = "9:00:00 AM";
							this.diaDef = true;
							goto IL_153B;
							IL_1013:
							bool flag22 = 5 - num > 0;
							if (flag22)
							{
								this.diaR = now.AddDays((double)(5 - num)).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							else
							{
								num = 7 - num + 5;
								this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							this.horaR = "9:00:00 AM";
							this.diaDef = true;
							goto IL_153B;
							IL_11CC:
							bool flag23 = 6 - num > 0;
							if (flag23)
							{
								this.diaR = now.AddDays((double)(6 - num)).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							else
							{
								num = 7 - num + 6;
								this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							this.horaR = "9:00:00 AM";
							this.diaDef = true;
							goto IL_153B;
							IL_1385:
							bool flag24 = 7 - num > 0;
							if (flag24)
							{
								this.diaR = now.AddDays((double)(7 - num)).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							else
							{
								num = 7 - num + 7;
								this.diaR = now.AddDays((double)num).ToString("d").Split(new char[]
								{
									' '
								})[0];
							}
							this.horaR = "9:00:00 AM";
							this.diaDef = true;
						}
						IL_153B:;
					}
				}
				catch
				{
					this.diaDef = false;
				}
				try
				{
					this.horaR = this.SpeechResult.Semantics["horaRecor"].Value.ToString();
					this.horaVer = true;
				}
				catch
				{
					this.horaVer = false;
				}
				try
				{
					bool flag25 = !this.diaDef && !this.horaVer;
					if (flag25)
					{
						string value = this.SpeechResult.Semantics["horaMinuto"].Value.ToString();
						bool flag26 = this.SpeechResult.Semantics["time"].Value.ToString() == "minutos";
						if (flag26)
						{
							int num3 = 0;
							bool flag27 = Convert.ToInt32(now.ToString("t").Split(new char[]
							{
								' '
							})[0].Split(new char[]
							{
								':'
							})[1]) + Convert.ToInt32(value) > 59;
							string text9;
							if (flag27)
							{
								num3 = 1;
								int num4 = Convert.ToInt32(now.ToString("t").Split(new char[]
								{
									' '
								})[0].Split(new char[]
								{
									':'
								})[1]) + Convert.ToInt32(value) - 59;
								text9 = ((num4 >= 10) ? num4.ToString() : ("0" + num4.ToString()));
								string text10 = (now.ToString("t", new CultureInfo("en-US")).Split(new char[]
								{
									' '
								})[1] != "AM") ? "AM" : "PM";
							}
							else
							{
								text9 = (Convert.ToInt32(now.ToString("t").Split(new char[]
								{
									' '
								})[0].Split(new char[]
								{
									':'
								})[1]) + Convert.ToInt32(value)).ToString();
							}
							bool flag28 = num3 == 1 && Convert.ToInt32(now.ToString("t").Split(new char[]
							{
								' '
							})[0].Split(new char[]
							{
								':'
							})[0]) + num3 > 12;
							string text11;
							string text12;
							if (flag28)
							{
								text11 = (Convert.ToInt32(now.ToString("t").Split(new char[]
								{
									' '
								})[0].Split(new char[]
								{
									':'
								})[0]) + Convert.ToInt32(num3) - 12).ToString();
								text12 = ((now.ToString("t", new CultureInfo("en-US")).Split(new char[]
								{
									' '
								})[1] != "AM") ? "AM" : "PM");
							}
							else
							{
								bool flag29 = num3 == 1 && Convert.ToInt32(now.ToString("t").Split(new char[]
								{
									' '
								})[0].Split(new char[]
								{
									':'
								})[0]) + num3 == 12;
								if (flag29)
								{
									text11 = (Convert.ToInt32(now.ToString("t").Split(new char[]
									{
										' '
									})[0].Split(new char[]
									{
										':'
									})[0]) + Convert.ToInt32(num3)).ToString();
									text12 = ((now.ToString("t", new CultureInfo("en-US")).Split(new char[]
									{
										' '
									})[1] != "AM") ? "AM" : "PM");
								}
								else
								{
									text11 = (Convert.ToInt32(now.ToString("t").Split(new char[]
									{
										' '
									})[0].Split(new char[]
									{
										':'
									})[0]) + Convert.ToInt32(num3)).ToString();
									text12 = ((now.ToString("t", new CultureInfo("en-US")).Split(new char[]
									{
										' '
									})[1] != "AM") ? "PM" : "AM");
								}
							}
							this.horaR = string.Concat(new string[]
							{
								text11,
								":",
								text9,
								":00 ",
								text12
							});
							this.horaVer = true;
						}
						else
						{
							int num5 = Convert.ToInt32(now.ToString("t").Split(new char[]
							{
								' '
							})[0].Split(new char[]
							{
								':'
							})[0]);
							int num6 = Convert.ToInt32(value);
							bool flag30 = num5 + num6 > 12;
							string text11;
							string text12;
							if (flag30)
							{
								text11 = (Convert.ToInt32(now.ToString("t").Split(new char[]
								{
									' '
								})[0].Split(new char[]
								{
									':'
								})[0]) + Convert.ToInt32(value) - 12).ToString();
								text12 = ((now.ToString("t", new CultureInfo("en-US")).Split(new char[]
								{
									' '
								})[1] != "AM") ? "AM" : "PM");
							}
							else
							{
								bool flag31 = num5 + num6 == 12;
								if (flag31)
								{
									text11 = (Convert.ToInt32(now.ToString("t").Split(new char[]
									{
										' '
									})[0].Split(new char[]
									{
										':'
									})[0]) + Convert.ToInt32(value)).ToString();
									text12 = ((now.ToString("t", new CultureInfo("en-US")).Split(new char[]
									{
										' '
									})[1] != "AM") ? "AM" : "PM");
								}
								else
								{
									text11 = (Convert.ToInt32(now.ToString("t").Split(new char[]
									{
										' '
									})[0].Split(new char[]
									{
										':'
									})[0]) + Convert.ToInt32(value)).ToString();
									text12 = ((now.ToString("t", new CultureInfo("en-US")).Split(new char[]
									{
										' '
									})[1] != "AM") ? "PM" : "AM");
								}
							}
							string[] array = new string[5];
							array[0] = text11;
							array[1] = ":";
							string[] array2 = array;
							array2[2] = now.ToString("t").Split(new char[]
							{
								' '
							})[0].Split(new char[]
							{
								':'
							})[1];
							array2[3] = ":00 ";
							array2[4] = text12;
							this.horaR = string.Concat(array2);
							this.horaVer = true;
						}
					}
				}
				catch (Exception)
				{
					this.horaVer = false;
				}
				string[] array3 = new string[]
				{
					"lembra-me",
					text,
					text5,
					"em a",
					"em",
					"as",
					"para",
					"para a",
					"para este",
					"hoje",
					"minutos",
					"horas",
					"hora",
					"dentro de",
					"um",
					"dois",
					"tres",
					"quantro",
					"dia",
					"amanhã",
					"amanhã à tarde",
					"amanhã à noite",
					"tarde",
					"noite",
					"segunda",
					"segunda de manhã",
					"segunda à tarde",
					"segunda à noite",
					"terça",
					"terça de manhã",
					"Terça à tarde",
					"Terça à noite",
					"Quarta",
					"Quarta de manhã",
					"Quarta à tarde",
					"Quarta à noite",
					"Quinta",
					"Quinta de manhã",
					"Quinta à tarde",
					"Quinta à noite",
					"Sexta",
					"Sexta de manhã",
					"Sexta à tarde",
					"Sexta à noite",
					"Sábado",
					"Sábado de manhã",
					"Sábado à tarde",
					"Sábado à noite",
					"Domingo",
					"Domingo de manhã",
					"Domingo à tarde",
					"Domingo à noite",
					"cinco",
					"seis",
					"sete",
					"oito",
					"nove",
					"dez",
					"onze",
					"doze",
					"treze",
					"quatorze",
					"quinze",
					"dezesseis",
					"dezessete",
					"dezoito",
					"dezenove",
					"vinte",
					"vinte e um",
					"vinte e dois",
					"vinte e três",
					"vinte e quatro",
					"vinte e cinco",
					"vinte e seis",
					"vinte e sete",
					"vinte e oito",
					"vinte e nove",
					"trinta",
					"trinta e um",
					"trinta e dois",
					"trinta e três",
					"trinta e quatro",
					"trinta e cinco",
					"trinta e seis",
					"trinta e sete",
					"trinta e oito",
					"trinta e nove",
					"quarenta",
					"quarenta e um",
					"quarenta e dois",
					"quarenta e três",
					"quarenta e quatro",
					"quarenta e cinco",
					"quarenta e seis",
					"quarenta e sete",
					"quarenta e oito",
					"quarenta e nove",
					"cinquenta",
					"cinquenta e um",
					"cinquenta e dois",
					"cinquenta e três",
					"cinquenta e quatro",
					"cinquenta e cinco",
					"cinquenta e seis",
					"cinquenta e sete",
					"cinquenta e oito",
					"cinquenta e nove",
					"sessenta"
				};
				this.textoR = this.SpeechResult.Text;
				foreach (string oldValue in this.AlarmFrase)
				{
					this.textoR = this.textoR.Replace(oldValue, "");
				}
				foreach (string oldValue2 in array3)
				{
					this.textoR = this.textoR.Replace(oldValue2, "");
				}
				this.textoR = this.textoR.Trim();
				bool flag32 = this.diaDef || this.horaVer;
				if (flag32)
				{
					this.Fala.SpeakAsync(string.Concat(new string[]
					{
						"novo lembrete ",
						this.textoR,
						" para o dia ",
						this.diaR,
						" Na hora ",
						this.horaR,
						". ",
						Settings.Default.cfgUser,
						" Deseja confirmar ou cancelar este lembrete? "
					}));
					this.btnFiltro.Background = new SolidColorBrush(Colors.White);
					this.TimerRecOFF = Settings.Default.cfgRec;
					this.comandoEjecutado = true;
					this.confirRecord = true;
				}
				bool flag33 = !this.diaDef && !this.horaVer && _speech.Count<char>() > 1;
				if (flag33)
				{
					this.Fala.SpeakAsync("a que hora  " + Settings.Default.cfgUser);
					this.setHoraR = true;
					this.btnFiltro.Background = new SolidColorBrush(Colors.White);
					this.TimerRecOFF = Settings.Default.cfgRec;
					this.comandoEjecutado = true;
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000021D4 File Offset: 0x000003D4
		private void Rectangle_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.popupJarvis.IsOpen = true;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0002C63C File Offset: 0x0002A83C
		private void setAlarma()
		{
			Settings.Default.tActivo = "ACTIVADO";
			Settings.Default.cLun = true;
			Settings.Default.cMar = true;
			Settings.Default.cMie = true;
			Settings.Default.cJue = true;
			Settings.Default.cVie = true;
			Settings.Default.cSab = true;
			Settings.Default.cDom = true;
			Settings.Default.Save();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000023C5 File Offset: 0x000005C5
		private void setDeslig()
		{
			Settings.Default.tApagado = true;
			Settings.Default.tReinicar = false;
			Settings.Default.tSuspender = false;
			Settings.Default.tSesion = false;
			Settings.Default.Save();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002403 File Offset: 0x00000603
		private void setReinicar()
		{
			Settings.Default.tReinicar = true;
			Settings.Default.tSuspender = false;
			Settings.Default.tApagado = false;
			Settings.Default.tSesion = false;
			Settings.Default.Save();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002441 File Offset: 0x00000641
		private void setSuspender()
		{
			Settings.Default.tSuspender = true;
			Settings.Default.tApagado = false;
			Settings.Default.tReinicar = false;
			Settings.Default.tSesion = false;
			Settings.Default.Save();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000247F File Offset: 0x0000067F
		private void setSesion()
		{
			Settings.Default.tSesion = true;
			Settings.Default.tApagado = false;
			Settings.Default.tReinicar = false;
			Settings.Default.tSuspender = false;
			Settings.Default.Save();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000024BD File Offset: 0x000006BD
		private void setdefaut()
		{
			Settings.Default.tApagado = false;
			Settings.Default.tReinicar = false;
			Settings.Default.tSuspender = false;
			Settings.Default.tSesion = false;
			Settings.Default.Save();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000024FB File Offset: 0x000006FB
		private void setcuetasgermail()
		{
			Settings.Default.checkGmail = true;
			Settings.Default.Save();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002515 File Offset: 0x00000715
		private void setcuetasOutlook()
		{
			Settings.Default.checkOutlook = true;
			Settings.Default.Save();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000252F File Offset: 0x0000072F
		private void setcuetasYahoo()
		{
			Settings.Default.checkYahoo = true;
			Settings.Default.Save();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002549 File Offset: 0x00000749
		private void setcuetasEmail()
		{
			Settings.Default.checkEmail = true;
			Settings.Default.Save();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002563 File Offset: 0x00000763
		private void clearcuetas()
		{
			Settings.Default.checkGmail = false;
			Settings.Default.checkOutlook = false;
			Settings.Default.checkYahoo = false;
			Settings.Default.checkEmail = false;
			Settings.Default.Save();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0002C6BC File Offset: 0x0002A8BC
		private void setHoraAlarma(string _speech)
		{
			string horaa = string.Empty;
			bool flag = _speech == "cancelar";
			if (flag)
			{
				this.Fala.SpeakAsync("cancelado ");
				this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 160, 0));
				this.TimerRecOFF = Settings.Default.cfgRec;
				this.comandoEjecutado = true;
				this.alarmaSet = false;
				this.TimerSetAlarm.Stop();
			}
			bool flag2 = this.SpeechResult.Semantics.ContainsKey("horaExacta");
			if (flag2)
			{
				bool flag3 = this.SpeechResult.Semantics["horaExacta"].Value.ToString() != "";
				if (flag3)
				{
					try
					{
						horaa = this.SpeechResult.Semantics["horaExacta"].Value.ToString();
						this.Cfgalarma(horaa);
						this.Fala.SpeakAsync("Pronto " + Settings.Default.cfgUser + ", o alarme será ativado as" + _speech);
						this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 160, 0));
						this.TimerRecOFF = Settings.Default.cfgRec;
						this.comandoEjecutado = true;
						this.alarmaSet = false;
						this.TimerSetAlarm.Stop();
					}
					catch (Exception ex)
					{
						Exception ex2 = ex;
						System.Windows.MessageBox.Show(ex2.Message + ": Erro de alarme");
					}
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0002C858 File Offset: 0x0002AA58
		private void setHoraRecord(string _speech)
		{
			string text = string.Empty;
			bool flag = _speech == "cancelar";
			if (flag)
			{
				this.Fala.SpeakAsync("cancelado ");
				this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 160, 0));
				this.TimerRecOFF = Settings.Default.cfgRec;
				this.comandoEjecutado = true;
				this.setHoraR = false;
			}
			bool flag2 = this.SpeechResult.Semantics.ContainsKey("horaExacta");
			if (flag2)
			{
				bool flag3 = this.SpeechResult.Semantics["horaExacta"].Value.ToString() != "";
				if (flag3)
				{
					try
					{
						DateTime now = DateTime.Now;
						text = this.SpeechResult.Semantics["horaExacta"].Value.ToString();
						text = string.Concat(new string[]
						{
							text.Split(new char[]
							{
								':'
							})[0],
							":",
							text.Split(new char[]
							{
								':'
							})[1].Split(new char[]
							{
								' '
							})[0],
							":00 ",
							text.Split(new char[]
							{
								':'
							})[1].Split(new char[]
							{
								' '
							})[1]
						});
						this.diaR = now.ToString("d").Split(new char[]
						{
							' '
						})[0];
						this.sRec = new CDRecordatorio(this.textoR, this.textoR, this.diaR, text, false);
						this.loadDataRec();
						this.Fala.SpeakAsync("Novo Lembrete " + this.textoR + " para hoje as horas " + text);
						this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 160, 0));
						this.TimerRecOFF = Settings.Default.cfgRec;
						this.comandoEjecutado = true;
						this.setHoraR = false;
					}
					catch (Exception ex)
					{
						Exception ex2 = ex;
						System.Windows.MessageBox.Show(ex2.Message + ": Erro no Lembrete");
					}
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0002CAAC File Offset: 0x0002ACAC
		private string sinonimoCmd(string _speech)
		{
			this.cmdConfirmado = false;
			string[] source = _speech.Split(new char[]
			{
				' '
			});
			bool flag = _speech.Length >= 5 || source.Count<string>() >= 2;
			if (flag)
			{
				try
				{
					this.cone = new OleDbConnection(Settings.Default.conexion1);
					this.cone.Open();
					OleDbCommand oleDbCommand = new OleDbCommand("SELECT * FROM ComandosDefecto WHERE Sinonimos LIKE '%" + _speech + "%'", this.cone);
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
					bool flag2 = oleDbDataReader.Read();
					if (flag2)
					{
						string[] array = oleDbDataReader["Sinonimos"].ToString().Split(new char[]
						{
							'+'
						});
						for (int i = 0; i < array.Length; i++)
						{
							bool flag3 = _speech == array[i];
							if (flag3)
							{
								this.cmdConfirmado = true;
							}
						}
						bool flag4 = this.cmdConfirmado;
						if (flag4)
						{
							_speech = oleDbDataReader["Comando"].ToString();
						}
					}
					this.cone.Close();
				}
				catch (Exception ex)
				{
					System.Windows.MessageBox.Show(ex.Message);
				}
			}
			return _speech;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		private void SliderBar(int value)
		{
			bool flag = value < 5;
			if (flag)
			{
				this.bar1.Visibility = Visibility.Hidden;
				this.bar2.Visibility = Visibility.Hidden;
				this.bar3.Visibility = Visibility.Hidden;
				this.bar4.Visibility = Visibility.Hidden;
				this.bar5.Visibility = Visibility.Hidden;
				this.bar6.Visibility = Visibility.Hidden;
				this.bar7.Visibility = Visibility.Hidden;
				this.bar8.Visibility = Visibility.Hidden;
			}
			bool flag2 = value > 5;
			if (flag2)
			{
				this.bar1.Visibility = Visibility.Visible;
				this.bar2.Visibility = Visibility.Hidden;
				this.bar3.Visibility = Visibility.Hidden;
				this.bar4.Visibility = Visibility.Hidden;
				this.bar5.Visibility = Visibility.Hidden;
				this.bar6.Visibility = Visibility.Hidden;
				this.bar7.Visibility = Visibility.Hidden;
				this.bar8.Visibility = Visibility.Hidden;
			}
			bool flag3 = value > 10;
			if (flag3)
			{
				this.bar1.Visibility = Visibility.Visible;
				this.bar2.Visibility = Visibility.Visible;
				this.bar3.Visibility = Visibility.Hidden;
				this.bar4.Visibility = Visibility.Hidden;
				this.bar5.Visibility = Visibility.Hidden;
				this.bar6.Visibility = Visibility.Hidden;
				this.bar7.Visibility = Visibility.Hidden;
				this.bar8.Visibility = Visibility.Hidden;
			}
			bool flag4 = value > 15;
			if (flag4)
			{
				this.bar1.Visibility = Visibility.Visible;
				this.bar2.Visibility = Visibility.Visible;
				this.bar3.Visibility = Visibility.Visible;
				this.bar4.Visibility = Visibility.Hidden;
				this.bar5.Visibility = Visibility.Hidden;
				this.bar6.Visibility = Visibility.Hidden;
				this.bar7.Visibility = Visibility.Hidden;
				this.bar8.Visibility = Visibility.Hidden;
			}
			bool flag5 = value > 20;
			if (flag5)
			{
				this.bar1.Visibility = Visibility.Visible;
				this.bar2.Visibility = Visibility.Visible;
				this.bar3.Visibility = Visibility.Visible;
				this.bar4.Visibility = Visibility.Visible;
				this.bar5.Visibility = Visibility.Hidden;
				this.bar6.Visibility = Visibility.Hidden;
				this.bar7.Visibility = Visibility.Hidden;
				this.bar8.Visibility = Visibility.Hidden;
			}
			bool flag6 = value > 25;
			if (flag6)
			{
				this.bar1.Visibility = Visibility.Visible;
				this.bar2.Visibility = Visibility.Visible;
				this.bar3.Visibility = Visibility.Visible;
				this.bar4.Visibility = Visibility.Visible;
				this.bar5.Visibility = Visibility.Visible;
				this.bar6.Visibility = Visibility.Hidden;
				this.bar7.Visibility = Visibility.Hidden;
				this.bar8.Visibility = Visibility.Hidden;
			}
			bool flag7 = value > 30;
			if (flag7)
			{
				this.bar1.Visibility = Visibility.Visible;
				this.bar2.Visibility = Visibility.Visible;
				this.bar3.Visibility = Visibility.Visible;
				this.bar4.Visibility = Visibility.Visible;
				this.bar5.Visibility = Visibility.Visible;
				this.bar6.Visibility = Visibility.Visible;
				this.bar7.Visibility = Visibility.Hidden;
				this.bar8.Visibility = Visibility.Hidden;
			}
			bool flag8 = value > 50;
			if (flag8)
			{
				this.bar1.Visibility = Visibility.Visible;
				this.bar2.Visibility = Visibility.Visible;
				this.bar3.Visibility = Visibility.Visible;
				this.bar4.Visibility = Visibility.Visible;
				this.bar5.Visibility = Visibility.Visible;
				this.bar6.Visibility = Visibility.Visible;
				this.bar7.Visibility = Visibility.Visible;
				this.bar8.Visibility = Visibility.Hidden;
			}
			bool flag9 = value > 70;
			if (flag9)
			{
				this.bar1.Visibility = Visibility.Visible;
				this.bar2.Visibility = Visibility.Visible;
				this.bar3.Visibility = Visibility.Visible;
				this.bar4.Visibility = Visibility.Visible;
				this.bar5.Visibility = Visibility.Visible;
				this.bar6.Visibility = Visibility.Visible;
				this.bar7.Visibility = Visibility.Visible;
				this.bar8.Visibility = Visibility.Visible;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0002D01C File Offset: 0x0002B21C
		private void sliderRec_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			Settings.Default.Confidence = Math.Round(this.sliderRec.Value, 2);
			this.lblSpeech.Content = Math.Round(this.sliderRec.Value, 2);
			Settings.Default.Save();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0002D074 File Offset: 0x0002B274
		private void sliderVolumen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			this.Fala.Volume = Convert.ToInt32(this.sliderVolumen.Value);
			Settings.Default.volumen = Convert.ToInt32(this.sliderVolumen.Value);
			Settings.Default.Save();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000025A1 File Offset: 0x000007A1
		public void SpeakOut(params string[] texto)
		{
			this.Fala.SpeakAsync(texto[this.rnd.Next(0, texto.Length)]);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000025C1 File Offset: 0x000007C1
		public void SpeakOutStop(params string[] texto)
		{
			this.Fala.Speak(texto[this.rnd.Next(0, texto.Length)]);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000025E1 File Offset: 0x000007E1
		public void StopSpeak()
		{
			this.Fala.SpeakAsyncCancelAll();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002254 File Offset: 0x00000454
		private void TabItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_skipe1.Play();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00002254 File Offset: 0x00000454
		private void TabItem_MouseEnter_1(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_skipe1.Play();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002254 File Offset: 0x00000454
		private void TabItem_MouseEnter_2(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SoundEffects.Hover_skipe1.Play();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0002D0C4 File Offset: 0x0002B2C4
		public void TakeScreenShot(string FilePath)
		{
			int width = Screen.PrimaryScreen.Bounds.Width;
			Bitmap bitmap = new Bitmap(width, Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bitmap);
			int x = Screen.PrimaryScreen.Bounds.X;
			int y = Screen.PrimaryScreen.Bounds.Y;
			graphics.CopyFromScreen(x, y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
			bitmap.Save(FilePath, ImageFormat.Png);
			bitmap.Dispose();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0002D16C File Offset: 0x0002B36C
		private void Timer_Tick(object sender, EventArgs e)
		{
			bool flag = this.contadorTimer == 0;
			if (flag)
			{
				this.ComputerTermination();
				this.Timer.Stop();
				this.ShutdownTimer = false;
			}
			else
			{
				bool flag2 = this.QEvent == "abortar";
				if (flag2)
				{
					this.Timer.Stop();
					this.contadorTimer = 10;
					this.btnJarvis.Content = Settings.Default.cfgAi;
					this.btnJarvis2.Content = Settings.Default.cfgAi;
					this.ShutdownTimer = false;
				}
				else
				{
					this.contadorTimer--;
					this.btnJarvis.Content = this.contadorTimer.ToString().ToUpper();
					this.btnJarvis2.Content = this.contadorTimer.ToString().ToUpper();
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0002D250 File Offset: 0x0002B450
		private void Timer2_Tick(object sender, EventArgs e)
		{
			bool flag = this.TimerRecOFF == 0 && !this.lecturaEnable && !this.completarBusqueda && !this.alarmaSet && !this.EnableShutdown && !this.EnableDictado && !this.confirRecord && !this.setHoraR;
			if (flag)
			{
				this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 210, byte.MaxValue));
				this.btnJarvis2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 210, byte.MaxValue));
				this.EnableComandos = false;
				this.lblSpeech2.Content = "para ativar diga: " + Settings.Default.cfgAi;
				bool cbOpacity = Settings.Default.cbOpacity;
				if (cbOpacity)
				{
					this.skin2.Opacity = 0.5;
					this.skin1.Opacity = 0.5;
				}
			}
			bool flag2 = this.TimerRecOFF > 0;
			if (flag2)
			{
				this.TimerRecOFF--;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0002D36C File Offset: 0x0002B56C
		private void Timer3_Tick(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			List<string> list = new List<string>();
			this.fechaExacta = DateTime.Today.ToString("d") + " " + now.ToString("h:mm:ss tt", new CultureInfo("en-US"));
			this.horaExacta = now.ToString("h:mm:ss tt", new CultureInfo("en-US"));
			foreach (string text in this.listAllRec)
			{
				bool flag = text.Split(new char[]
				{
					','
				})[4] == "false";
				if (flag)
				{
					bool flag2 = this.fechaExacta == text.Split(new char[]
					{
						','
					})[2] + " " + text.Split(new char[]
					{
						','
					})[3];
					if (flag2)
					{
						try
						{
							this.Fala.SpeakAsync(Settings.Default.cfgUser + " tem um Lembrete programado nesta hora " + text.Split(new char[]
							{
								','
							})[0].Split(new char[]
							{
								'('
							})[0]);
							SoundEffects.sonidoRec.PlayLooping();
							Notificacion notificacion = new Notificacion();
							notificacion.lblTitulo.Content = text.Split(new char[]
							{
								','
							})[0];
							notificacion.txtNota.Text = text.Split(new char[]
							{
								','
							})[1];
							notificacion.Show();
							notificacion.Closed += delegate(object a, EventArgs b)
							{
								SoundEffects.sonidoRec.Stop();
								notificacion = null;
							};
						}
						catch (Exception ex)
						{
							Exception ex2 = ex;
							System.Windows.MessageBox.Show(ex2.Message, "AV - Lembrete", MessageBoxButton.OK, MessageBoxImage.Hand);
						}
					}
				}
				else
				{
					bool flag3 = this.horaExacta == text.Split(new char[]
					{
						','
					})[3];
					if (flag3)
					{
						try
						{
							this.Fala.SpeakAsync(Settings.Default.cfgUser + " tem um Lembrete programado nesta hora " + text.Split(new char[]
							{
								','
							})[0].Split(new char[]
							{
								'('
							})[0]);
							SoundEffects.sonidoRec.PlayLooping();
							Notificacion notificacion1 = new Notificacion();
							notificacion1.lblTitulo.Content = text.Split(new char[]
							{
								','
							})[0];
							notificacion1.txtNota.Text = text.Split(new char[]
							{
								','
							})[1];
							notificacion1.Show();
							notificacion1.Closed += delegate(object a, EventArgs b)
							{
								SoundEffects.sonidoRec.Stop();
								notificacion1 = null;
							};
						}
						catch (Exception ex3)
						{
							Exception ex4 = ex3;
							System.Windows.MessageBox.Show(ex4.Message, "AV - Lembrete", MessageBoxButton.OK, MessageBoxImage.Hand);
						}
					}
				}
			}
			bool flag4 = this.contadorTimer3 == 0;
			if (flag4)
			{
				this.lecturaCorreo = true;
				this.lecturaFacebook = true;
				bool checkRss = Settings.Default.checkRss;
				if (checkRss)
				{
					this.FacebookGetFeed();
				}
				bool flag5 = Settings.Default.checkGmail && Settings.Default.servidorGmail == "Atom";
				if (flag5)
				{
					this.CheckForEmails();
					bool flag6 = this.cantidadCorreos > this.cantidadAnteriorCorreos;
					if (flag6)
					{
						int num = this.cantidadCorreos - this.cantidadAnteriorCorreos;
						bool flag7 = num == 1;
						if (flag7)
						{
							this.Fala.SpeakAsync(Settings.Default.cfgUser + " Você acabou de receber uma nova mensagem de " + this.MsgLinkInfo[0]);
						}
						else
						{
							this.Fala.SpeakAsync(string.Concat(new object[]
							{
								Settings.Default.cfgUser,
								" você acabou de receber ",
								num,
								" novos e-mails "
							}));
							for (int i = 0; i < num; i++)
							{
								this.Fala.SpeakAsync(this.MsgLinkInfo[i]);
							}
						}
						this.cantidadAnteriorCorreos = this.cantidadCorreos;
					}
					bool flag8 = this.cantidadCorreos < this.cantidadAnteriorCorreos;
					if (flag8)
					{
						this.cantidadAnteriorCorreos = this.cantidadCorreos;
					}
				}
				this.lecturaCorreo = false;
				this.lecturaFacebook = false;
				this.contadorTimer3 = 110;
			}
			else
			{
				this.contadorTimer3--;
			}
			string hora = now.ToString("h:mm:ss tt", new CultureInfo("en-US"));
			Alarm.checkAlarma(hora, DateTime.Today.ToString("dddd"));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0002D8C0 File Offset: 0x0002BAC0
		private void TimerAviso_Tick(object sender, EventArgs e)
		{
			bool flag = this.tiempoAviso == 0;
			if (flag)
			{
				Storyboard storyboard = (Storyboard)base.FindResource("StoryboardAvisoOff");
				storyboard.Completed += delegate(object a, EventArgs b)
				{
					storyboard.Remove();
				};
				storyboard.Begin();
				this.TimerAviso.Stop();
			}
			this.tiempoAviso--;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0002D938 File Offset: 0x0002BB38
		private void TimerFlip_Tick(object sender, EventArgs e)
		{
			bool flag = this.flip == "reset";
			if (flag)
			{
				this.contadorflip = 5;
				this.flip = "start";
			}
			bool flag2 = this.contadorflip == 0;
			if (flag2)
			{
				InputSimulator.SimulateKeyUp(VirtualKeyCode.LWIN);
				this.TimerFlip.Stop();
			}
			this.contadorflip--;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0002D9A0 File Offset: 0x0002BBA0
		private void TimerImap_Tick(object sender, EventArgs e)
		{
			bool flag = Settings.Default.checkGmail && this.contadorImap == 0 && !Settings.Default.cntGmailAtomOK && Settings.Default.cntGmailOK && !this.checkGmail;
			if (flag)
			{
				this.checkGmail = true;
				this.conexionGmail();
			}
			bool flag2 = Settings.Default.checkOutlook && this.contadorImap == 1 && !this.checkOutlook;
			if (flag2)
			{
				this.checkOutlook = true;
				this.conexionOutloock();
			}
			bool flag3 = Settings.Default.checkYahoo && this.contadorImap == 2 && !this.checkYahoo;
			if (flag3)
			{
				this.checkYahoo = true;
				this.conexionYahoo();
			}
			bool flag4 = Settings.Default.checkEmail && this.contadorImap == 3 && !this.checkEmail;
			if (flag4)
			{
				this.checkEmail = true;
				this.conexionEmail();
			}
			this.contadorImap++;
			bool flag5 = this.contadorImap == 4;
			if (flag5)
			{
				this.contadorImap = 0;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0002DAC0 File Offset: 0x0002BCC0
		private void TimerRec_Tick(object sender, EventArgs e)
		{
			bool flag = this.estado != this.Fala.State.ToString();
			if (flag)
			{
				this.estado = this.Fala.State.ToString();
				this.lblSpeech.Content = this.estado;
				bool flag2 = this.estado == "Speaking";
				if (flag2)
				{
					bool flag3 = !this.lecturaEnable;
					if (flag3)
					{
						this._reco.RecognizeAsyncStop();
					}
					this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 210, byte.MaxValue));
					this.btnJarvis2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 210, byte.MaxValue));
				}
				else
				{
					bool flag4 = this.estado == "Ready";
					if (flag4)
					{
						try
						{
							bool flag5 = !this.lecturaEnable;
							if (flag5)
							{
								this._reco.RecognizeAsync(RecognizeMode.Multiple);
							}
							else
							{
								this.lecturaEnable = false;
							}
						}
						catch
						{
							this.lblSpeech.Content = "Erro no reconhecimento";
							this._reco.RecognizeAsyncCancel();
							this._reco.RecognizeAsyncStop();
							this._reco.RecognizeAsync(RecognizeMode.Multiple);
						}
						this.btnFiltro.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 160, 0));
						this.btnJarvis2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 160, 0));
					}
				}
				Trace.WriteLine(this.estado);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0002DC80 File Offset: 0x0002BE80
		private void UnloadGramarTabla()
		{
			this._reco.UnloadGrammar(this.gListaComandoA);
			this._reco.UnloadGrammar(this.gListaComandoC);
			this._reco.UnloadGrammar(this.gListaComandoS);
			this._reco.UnloadGrammar(this.gListaComandoP);
			this._reco.UnloadGrammar(this.gALLComandos);
			this._reco.UnloadGrammar(this.gALLComandosS);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000025F0 File Offset: 0x000007F0
		private void UnloadGrammarTablaKey()
		{
			this._reco.UnloadGrammar(this.cmdKey);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00002605 File Offset: 0x00000805
		public void volumewindow()
		{
			AVJARVIS.keybd_event(174, AVJARVIS.MapVirtualKey(174U, 0U), 1U, 0U);
			AVJARVIS.keybd_event(174, AVJARVIS.MapVirtualKey(174U, 0U), 3U, 0U);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00002638 File Offset: 0x00000838
		public void volumewinup()
		{
			AVJARVIS.keybd_event(175, AVJARVIS.MapVirtualKey(175U, 0U), 1U, 0U);
			AVJARVIS.keybd_event(175, AVJARVIS.MapVirtualKey(175U, 0U), 3U, 0U);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0002DCFC File Offset: 0x0002BEFC
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (!Settings.Default.inicioWPosition)
			{
				Rect workArea = SystemParameters.WorkArea;
				base.Left = 0.0;
				base.Top = workArea.Bottom - base.Height;
			}
			else
			{
				base.Top = Settings.Default.Top;
				base.Top = Settings.Default.Top;
				base.Left = Settings.Default.Left;
			}
			this.reconocimiento();
			this.LoadTimerRecOff = Settings.Default.cfgRec;
			this.LoadReponder = Settings.Default.Responder;
			this.LoadCbOpacidad = Settings.Default.cbOpacity;
			this.cfgUser = Settings.Default.cfgUser;
			this.LoadCmdSearch = Settings.Default.CmdSearch;
			this.LoadCmdCal = Settings.Default.CmdCalc;
			this.LoadCmdAlar = Settings.Default.CmdAlar;
			this.LoadtecladoOn = Settings.Default.ActTecl;
			this.sliderRec.Value = Settings.Default.Confidence;
			this.cbOpacidad.IsChecked = new bool?(Settings.Default.cbOpacity);
			this.EnableComandos = false;
			this.comandoEjecutado = false;
			for (int i = 1; i < 6; i++)
			{
				this.cbNum.Items.Add(i);
			}
			this.cbTipo.Items.Add("Tipo A");
			this.cbTipo.Items.Add("Tipo B");
			this.cbskin.Items.Add("Skin 1");
			this.cbskin.Items.Add("Skin 2");
			this.cbskin.Text = "Skin " + Settings.Default.Skin.ToString();
			if (this.cbskin.Text == "Skin 1")
			{
				this.skin1.Visibility = Visibility.Visible;
				this.skin2.Visibility = Visibility.Hidden;
			}
			if (this.cbskin.Text == "Skin 2")
			{
				this.skin1.Visibility = Visibility.Hidden;
				this.skin2.Visibility = Visibility.Visible;
			}
			this.cbNum.Text = Settings.Default.tSyn.ToString();
			this.cbTipo.Text = Settings.Default.cSyn;
			string str = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			this.tbVersion.Text = "AV " + str + "\nEditado por Kênyson Estelita";
			AutoUpdater.Start("https://raw.githubusercontent.com/nexthrill/update/master/UpdateAV", null);
			SoundEffects.Somefects();
			this.cbSound.IsChecked = new bool?(Settings.Default.soundOn);
			this.cbTopMost.IsChecked = new bool?(Settings.Default.topMost);
			if (Settings.Default.topMost)
			{
				base.Topmost = true;
			}
			else
			{
				base.Topmost = false;
			}
			this.numeroCuentas();
			this.TimerRec.Interval = new TimeSpan(0, 0, 0, 0, 1);
			this.TimerRec.Tick += this.TimerRec_Tick;
			this.TimerRec.Start();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000020F8 File Offset: 0x000002F8
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0002E038 File Offset: 0x0002C238
		private void writeDictado(string _speech)
		{
			bool flag = false;
			bool flag2 = _speech != null;
			if (flag2)
			{
				if (_speech != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(_speech);
					if (num <= 2047840158U)
					{
						if (num <= 1142232475U)
						{
							if (num <= 610350747U)
							{
								if (num <= 417507213U)
								{
									if (num <= 252472541U)
									{
										if (num != 110023262U)
										{
											if (num != 211627806U)
											{
												if (num != 252472541U)
												{
													goto IL_15EA;
												}
												if (!(_speech == "\n"))
												{
													goto IL_15EA;
												}
												goto IL_150F;
											}
											else
											{
												if (!(_speech == "texto para itálico"))
												{
													goto IL_15EA;
												}
												goto IL_132B;
											}
										}
										else
										{
											if (!(_speech == "selecionar documento inteiro"))
											{
												goto IL_15EA;
											}
											goto IL_129F;
										}
									}
									else if (num != 259704125U)
									{
										if (num != 264251466U)
										{
											if (num != 417507213U)
											{
												goto IL_15EA;
											}
											if (!(_speech == "fechar ponto de interrogação"))
											{
												goto IL_15EA;
											}
											InputSimulator.SimulateTextEntry("?");
											flag = true;
											goto IL_15EA;
										}
										else
										{
											if (!(_speech == "criar novo documento"))
											{
												goto IL_15EA;
											}
											goto IL_12D5;
										}
									}
									else
									{
										if (!(_speech == "baixar a linha"))
										{
											goto IL_15EA;
										}
										InputSimulator.SimulateKeyPress(VirtualKeyCode.DOWN);
										flag = true;
										goto IL_15EA;
									}
								}
								else if (num <= 484181905U)
								{
									if (num != 447823721U)
									{
										if (num != 470958590U)
										{
											if (num != 484181905U)
											{
												goto IL_15EA;
											}
											if (!(_speech == "converter em negrito"))
											{
												goto IL_15EA;
											}
											goto IL_1319;
										}
										else
										{
											if (!(_speech == "sublinhar o texto"))
											{
												goto IL_15EA;
											}
											goto IL_133D;
										}
									}
									else
									{
										if (!(_speech == "converter a itálico"))
										{
											goto IL_15EA;
										}
										goto IL_132B;
									}
								}
								else if (num <= 516104836U)
								{
									if (num != 512054071U)
									{
										if (num != 516104836U)
										{
											goto IL_15EA;
										}
										if (!(_speech == "selecione a próxima palavra"))
										{
											goto IL_15EA;
										}
										InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, new VirtualKeyCode[]
										{
											VirtualKeyCode.SHIFT,
											VirtualKeyCode.RIGHT
										});
										flag = true;
										goto IL_15EA;
									}
									else
									{
										if (!(_speech == "desfazer isso"))
										{
											goto IL_15EA;
										}
										goto IL_127B;
									}
								}
								else if (num != 523813748U)
								{
									if (num != 610350747U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "apagar isso"))
									{
										goto IL_15EA;
									}
									goto IL_1390;
								}
								else if (!(_speech == "fechar comillas"))
								{
									goto IL_15EA;
								}
							}
							else if (num <= 1006160629U)
							{
								if (num <= 742856251U)
								{
									if (num != 726361231U)
									{
										if (num != 730411353U)
										{
											if (num != 742856251U)
											{
												goto IL_15EA;
											}
											if (!(_speech == "excluir caractere à direita"))
											{
												goto IL_15EA;
											}
											InputSimulator.SimulateKeyPress(VirtualKeyCode.DELETE);
											flag = true;
											goto IL_15EA;
										}
										else
										{
											if (!(_speech == "numero zero"))
											{
												goto IL_15EA;
											}
											InputSimulator.SimulateTextEntry("0");
											flag = true;
											goto IL_15EA;
										}
									}
									else
									{
										if (!(_speech == "vista preliminar"))
										{
											goto IL_15EA;
										}
										goto IL_1361;
									}
								}
								else if (num <= 920083201U)
								{
									if (num != 816286010U)
									{
										if (num != 920083201U)
										{
											goto IL_15EA;
										}
										if (!(_speech == "abrir comillas"))
										{
											goto IL_15EA;
										}
									}
									else
									{
										if (!(_speech == "selecione a palavra anterior"))
										{
											goto IL_15EA;
										}
										goto IL_141D;
									}
								}
								else if (num != 926468142U)
								{
									if (num != 1006160629U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "ativar itálico"))
									{
										goto IL_15EA;
									}
									goto IL_132B;
								}
								else
								{
									if (!(_speech == "selecionar tudo"))
									{
										goto IL_15EA;
									}
									goto IL_129F;
								}
							}
							else if (num <= 1033860826U)
							{
								if (num != 1017394514U)
								{
									if (num != 1023183355U)
									{
										if (num != 1033860826U)
										{
											goto IL_15EA;
										}
										if (!(_speech == "vá para o fim da linha"))
										{
											goto IL_15EA;
										}
										goto IL_146D;
									}
									else
									{
										if (!(_speech == "novo documento"))
										{
											goto IL_15EA;
										}
										goto IL_12D5;
									}
								}
								else
								{
									if (!(_speech == "fechar documento"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_R);
									flag = true;
									goto IL_15EA;
								}
							}
							else if (num <= 1083934097U)
							{
								if (num != 1038249346U)
								{
									if (num != 1083934097U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "excluir caractere à esquerda"))
									{
										goto IL_15EA;
									}
									goto IL_1381;
								}
								else
								{
									if (!(_speech == "texto em itálico"))
									{
										goto IL_15EA;
									}
									goto IL_132B;
								}
							}
							else if (num != 1107262984U)
							{
								if (num != 1142232475U)
								{
									goto IL_15EA;
								}
								if (!(_speech == "desativar itálico"))
								{
									goto IL_15EA;
								}
								goto IL_132B;
							}
							else
							{
								if (!(_speech == "desativar negrito"))
								{
									goto IL_15EA;
								}
								goto IL_1319;
							}
							InputSimulator.SimulateTextEntry("''");
							flag = true;
							goto IL_15EA;
							IL_129F:
							InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_E);
							flag = true;
							goto IL_15EA;
						}
						if (num > 1477068431U)
						{
							if (num <= 1818164727U)
							{
								if (num <= 1711496150U)
								{
									if (num != 1479354650U)
									{
										if (num != 1685198114U)
										{
											if (num != 1711496150U)
											{
												goto IL_15EA;
											}
											if (!(_speech == "numero dois"))
											{
												goto IL_15EA;
											}
											InputSimulator.SimulateTextEntry("2");
											flag = true;
											goto IL_15EA;
										}
										else
										{
											if (!(_speech == "excluir palavra"))
											{
												goto IL_15EA;
											}
											goto IL_1390;
										}
									}
									else
									{
										if (!(_speech == "vá para o começo da linha"))
										{
											goto IL_15EA;
										}
										goto IL_145D;
									}
								}
								else if (num <= 1745841823U)
								{
									if (num != 1715786627U)
									{
										if (num != 1745841823U)
										{
											goto IL_15EA;
										}
										if (!(_speech == "sublinhar texto"))
										{
											goto IL_15EA;
										}
										goto IL_133D;
									}
									else if (!(_speech == "novo campo"))
									{
										goto IL_15EA;
									}
								}
								else if (num != 1759668173U)
								{
									if (num != 1818164727U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "abrir um novo documento"))
									{
										goto IL_15EA;
									}
									goto IL_12D5;
								}
								else
								{
									if (!(_speech == "salve o documento"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateKeyPress(VirtualKeyCode.F12);
									flag = true;
									goto IL_15EA;
								}
							}
							else if (num <= 1878410769U)
							{
								if (num != 1870294401U)
								{
									if (num != 1872538253U)
									{
										if (num != 1878410769U)
										{
											goto IL_15EA;
										}
										if (!(_speech == "inserir campo"))
										{
											goto IL_15EA;
										}
									}
									else
									{
										if (!(_speech == "converter para negrito"))
										{
											goto IL_15EA;
										}
										goto IL_1319;
									}
								}
								else
								{
									if (!(_speech == "numero nove"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateTextEntry("9");
									flag = true;
									goto IL_15EA;
								}
							}
							else if (num <= 2023832224U)
							{
								if (num != 1970357868U)
								{
									if (num != 2023832224U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "excluir palavra à esquerda"))
									{
										goto IL_15EA;
									}
									goto IL_1390;
								}
								else
								{
									if (!(_speech == "palavra em negrito"))
									{
										goto IL_15EA;
									}
									goto IL_1319;
								}
							}
							else if (num != 2034776535U)
							{
								if (num != 2047840158U)
								{
									goto IL_15EA;
								}
								if (!(_speech == "converter em itálico"))
								{
									goto IL_15EA;
								}
								goto IL_132B;
							}
							else
							{
								if (!(_speech == "buscar documento"))
								{
									goto IL_15EA;
								}
								goto IL_12B1;
							}
							InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.F9);
							flag = true;
							goto IL_15EA;
						}
						if (num <= 1278775786U)
						{
							if (num <= 1204671273U)
							{
								if (num != 1143541407U)
								{
									if (num != 1192508065U)
									{
										if (num != 1204671273U)
										{
											goto IL_15EA;
										}
										if (!(_speech == "vá para o início da linha"))
										{
											goto IL_15EA;
										}
										goto IL_145D;
									}
									else
									{
										if (!(_speech == "desativar sublinhado"))
										{
											goto IL_15EA;
										}
										goto IL_133D;
									}
								}
								else
								{
									if (!(_speech == "desativar ditado"))
									{
										goto IL_15EA;
									}
									this.Fala.SpeakAsync("parando.");
									this.lblSpeech2.Content = "parando o ditado. Espera ...";
									this.EnableDictado = false;
									this.TimerRecOFF = Settings.Default.cfgRec;
									this.EnableComandos = true;
									this.comandoEjecutado = true;
									this.rectSkin2.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 97, 167));
									this.Fala.SpeakAsync("ditado desativado");
									goto IL_15EA;
								}
							}
							else if (num != 1225464730U)
							{
								if (num != 1251663421U)
								{
									if (num != 1278775786U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "abrir ponto de interrogação"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateTextEntry("¿");
									flag = true;
									goto IL_15EA;
								}
								else if (!(_speech == "buscar"))
								{
									goto IL_15EA;
								}
							}
							else
							{
								if (!(_speech == "ativar negrito"))
								{
									goto IL_15EA;
								}
								goto IL_1319;
							}
						}
						else if (num <= 1402556204U)
						{
							if (num != 1311424915U)
							{
								if (num != 1365048804U)
								{
									if (num != 1402556204U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "palavra para negrito"))
									{
										goto IL_15EA;
									}
									goto IL_1319;
								}
								else
								{
									if (!(_speech == "vá para a próxima palavra"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RIGHT);
									flag = true;
									goto IL_15EA;
								}
							}
							else
							{
								if (!(_speech == "ver pré-visualização"))
								{
									goto IL_15EA;
								}
								goto IL_1361;
							}
						}
						else if (num <= 1415925912U)
						{
							if (num != 1403864348U)
							{
								if (num != 1415925912U)
								{
									goto IL_15EA;
								}
								if (!(_speech == "inserir comentário"))
								{
									goto IL_15EA;
								}
								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, new VirtualKeyCode[]
								{
									VirtualKeyCode.MENU,
									VirtualKeyCode.VK_A
								});
								flag = true;
								goto IL_15EA;
							}
							else
							{
								if (!(_speech == "arroba"))
								{
									goto IL_15EA;
								}
								InputSimulator.SimulateTextEntry("@");
								flag = true;
								goto IL_15EA;
							}
						}
						else if (num != 1473036160U)
						{
							if (num != 1477068431U)
							{
								goto IL_15EA;
							}
							if (!(_speech == "abrir exclamação"))
							{
								goto IL_15EA;
							}
							InputSimulator.SimulateTextEntry("¡");
							flag = true;
							goto IL_15EA;
						}
						else
						{
							if (!(_speech == "converter para minúsculas"))
							{
								goto IL_15EA;
							}
							goto IL_13E7;
						}
						IL_12B1:
						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_B);
						flag = true;
						goto IL_15EA;
						IL_145D:
						InputSimulator.SimulateKeyPress(VirtualKeyCode.HOME);
						flag = true;
						goto IL_15EA;
						IL_12D5:
						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_U);
						flag = true;
						goto IL_15EA;
						IL_1361:
						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, new VirtualKeyCode[]
						{
							VirtualKeyCode.MENU,
							VirtualKeyCode.VK_I
						});
						flag = true;
						goto IL_15EA;
					}
					if (num <= 3196673774U)
					{
						if (num <= 2486230603U)
						{
							if (num <= 2327740222U)
							{
								if (num <= 2250354097U)
								{
									if (num != 2144466751U)
									{
										if (num != 2248821307U)
										{
											if (num != 2250354097U)
											{
												goto IL_15EA;
											}
											if (!(_speech == "vá para o início do documento"))
											{
												goto IL_15EA;
											}
											InputSimulator.SimulateKeyPress(VirtualKeyCode.PRIOR);
											flag = true;
											goto IL_15EA;
										}
										else
										{
											if (!(_speech == "nova linha"))
											{
												goto IL_15EA;
											}
											goto IL_150F;
										}
									}
									else
									{
										if (!(_speech == "inserir nota de rodapé"))
										{
											goto IL_15EA;
										}
										InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, new VirtualKeyCode[]
										{
											VirtualKeyCode.MENU,
											VirtualKeyCode.VK_O
										});
										flag = true;
										goto IL_15EA;
									}
								}
								else if (num != 2262250998U)
								{
									if (num != 2321003388U)
									{
										if (num != 2327740222U)
										{
											goto IL_15EA;
										}
										if (!(_speech == "numero oito"))
										{
											goto IL_15EA;
										}
										InputSimulator.SimulateTextEntry("8");
										flag = true;
										goto IL_15EA;
									}
									else if (!(_speech == "alterar fonte para documento"))
									{
										goto IL_15EA;
									}
								}
								else
								{
									if (!(_speech == "numero três"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateTextEntry("3");
									flag = true;
									goto IL_15EA;
								}
							}
							else if (num <= 2439170913U)
							{
								if (num != 2340623021U)
								{
									if (num != 2391861608U)
									{
										if (num != 2439170913U)
										{
											goto IL_15EA;
										}
										if (!(_speech == "excluir caractere"))
										{
											goto IL_15EA;
										}
										goto IL_1381;
									}
									else
									{
										if (!(_speech == "mude para maiúsculas"))
										{
											goto IL_15EA;
										}
										goto IL_13E7;
									}
								}
								else
								{
									if (!(_speech == "numero cinco"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateTextEntry("5");
									flag = true;
									goto IL_15EA;
								}
							}
							else if (num <= 2478364938U)
							{
								if (num != 2465473507U)
								{
									if (num != 2478364938U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "voltar um espaço"))
									{
										goto IL_15EA;
									}
									goto IL_1381;
								}
								else
								{
									if (!(_speech == "copiar texto"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
									flag = true;
									goto IL_15EA;
								}
							}
							else if (num != 2481199432U)
							{
								if (num != 2486230603U)
								{
									goto IL_15EA;
								}
								if (!(_speech == "mude para minúsculas"))
								{
									goto IL_15EA;
								}
								goto IL_13E7;
							}
							else
							{
								if (!(_speech == "cerrar exclamação"))
								{
									goto IL_15EA;
								}
								InputSimulator.SimulateTextEntry("''");
								flag = true;
								goto IL_15EA;
							}
						}
						else if (num <= 2955398281U)
						{
							if (num <= 2678373943U)
							{
								if (num != 2617531285U)
								{
									if (num != 2619850059U)
									{
										if (num != 2678373943U)
										{
											goto IL_15EA;
										}
										if (!(_speech == "selecionar a linha"))
										{
											goto IL_15EA;
										}
										goto IL_149D;
									}
									else
									{
										if (!(_speech == "pressione enter"))
										{
											goto IL_15EA;
										}
										InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
										flag = true;
										goto IL_15EA;
									}
								}
								else
								{
									if (!(_speech == "texto em negrito"))
									{
										goto IL_15EA;
									}
									goto IL_1319;
								}
							}
							else if (num <= 2802919488U)
							{
								if (num != 2766825698U)
								{
									if (num != 2802919488U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "sublinhar palavra"))
									{
										goto IL_15EA;
									}
									goto IL_133D;
								}
								else
								{
									if (!(_speech == "colar texto"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
									flag = true;
									goto IL_15EA;
								}
							}
							else if (num != 2947745649U)
							{
								if (num != 2955398281U)
								{
									goto IL_15EA;
								}
								if (!(_speech == "pressione tabulação"))
								{
									goto IL_15EA;
								}
								InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
								flag = true;
								goto IL_15EA;
							}
							else
							{
								if (!(_speech == "numero um"))
								{
									goto IL_15EA;
								}
								InputSimulator.SimulateTextEntry("1");
								flag = true;
								goto IL_15EA;
							}
						}
						else if (num <= 3105222355U)
						{
							if (num != 3092516874U)
							{
								if (num != 3096798968U)
								{
									if (num != 3105222355U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "numero quatro"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateTextEntry("4");
									flag = true;
									goto IL_15EA;
								}
								else
								{
									if (!(_speech == "numero sete"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateTextEntry("7");
									flag = true;
									goto IL_15EA;
								}
							}
							else if (!(_speech == "mudar fonte"))
							{
								goto IL_15EA;
							}
						}
						else if (num <= 3192786255U)
						{
							if (num != 3156789834U)
							{
								if (num != 3192786255U)
								{
									goto IL_15EA;
								}
								if (!(_speech == "numero seis"))
								{
									goto IL_15EA;
								}
								InputSimulator.SimulateTextEntry("6");
								flag = true;
								goto IL_15EA;
							}
							else
							{
								if (!(_speech == "pressione espaço"))
								{
									goto IL_15EA;
								}
								InputSimulator.SimulateKeyPress(VirtualKeyCode.SPACE);
								flag = true;
								goto IL_15EA;
							}
						}
						else if (num != 3192794189U)
						{
							if (num != 3196673774U)
							{
								goto IL_15EA;
							}
							if (!(_speech == "verifique a ortografia para documentar"))
							{
								goto IL_15EA;
							}
							goto IL_1309;
						}
						else
						{
							if (!(_speech == "inserir hiperlink"))
							{
								goto IL_15EA;
							}
							InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, new VirtualKeyCode[]
							{
								VirtualKeyCode.MENU,
								VirtualKeyCode.VK_K
							});
							flag = true;
							goto IL_15EA;
						}
						InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_M);
						flag = true;
						goto IL_15EA;
					}
					if (num <= 3651226058U)
					{
						if (num <= 3360071823U)
						{
							if (num <= 3296770723U)
							{
								if (num != 3271289606U)
								{
									if (num != 3287941945U)
									{
										if (num != 3296770723U)
										{
											goto IL_15EA;
										}
										if (!(_speech == "converter para maiúsculas"))
										{
											goto IL_15EA;
										}
										goto IL_13E7;
									}
									else
									{
										if (!(_speech == "voltar um caractere"))
										{
											goto IL_15EA;
										}
										goto IL_1381;
									}
								}
								else
								{
									if (!(_speech == "verificar ortografia"))
									{
										goto IL_15EA;
									}
									goto IL_1309;
								}
							}
							else if (num != 3324775936U)
							{
								if (num != 3349803991U)
								{
									if (num != 3360071823U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "palavra para itálico"))
									{
										goto IL_15EA;
									}
									goto IL_132B;
								}
								else
								{
									if (!(_speech == "apagar caractere"))
									{
										goto IL_15EA;
									}
									goto IL_1381;
								}
							}
							else
							{
								if (!(_speech == "ativar o sublinhado"))
								{
									goto IL_15EA;
								}
								goto IL_133D;
							}
						}
						else if (num <= 3491809458U)
						{
							if (num != 3430543718U)
							{
								if (num != 3477976990U)
								{
									if (num != 3491809458U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "refazer"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_Y);
									flag = true;
									goto IL_15EA;
								}
								else
								{
									if (!(_speech == "vá para o final do documento"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateKeyPress(VirtualKeyCode.NEXT);
									flag = true;
									goto IL_15EA;
								}
							}
							else
							{
								if (!(_speech == "selecione a palavra"))
								{
									goto IL_15EA;
								}
								goto IL_141D;
							}
						}
						else if (num <= 3563832068U)
						{
							if (num != 3505174814U)
							{
								if (num != 3563832068U)
								{
									goto IL_15EA;
								}
								if (!(_speech == "pular a página"))
								{
									goto IL_15EA;
								}
							}
							else
							{
								if (!(_speech == "desativar o sublinhado"))
								{
									goto IL_15EA;
								}
								goto IL_133D;
							}
						}
						else if (num != 3606192335U)
						{
							if (num != 3651226058U)
							{
								goto IL_15EA;
							}
							if (!(_speech == "texto a negrito"))
							{
								goto IL_15EA;
							}
							goto IL_1319;
						}
						else
						{
							if (!(_speech == "palavra em itálico"))
							{
								goto IL_15EA;
							}
							goto IL_132B;
						}
					}
					else if (num <= 3950543336U)
					{
						if (num <= 3771353916U)
						{
							if (num != 3683864431U)
							{
								if (num != 3689829919U)
								{
									if (num != 3771353916U)
									{
										goto IL_15EA;
									}
									if (!(_speech == "novo parágrafo"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
									flag = true;
									goto IL_15EA;
								}
								else
								{
									if (!(_speech == "imprimir o documento"))
									{
										goto IL_15EA;
									}
									InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_P);
									flag = true;
									goto IL_15EA;
								}
							}
							else
							{
								if (!(_speech == "sublinhar a palavra"))
								{
									goto IL_15EA;
								}
								goto IL_133D;
							}
						}
						else if (num <= 3913569084U)
						{
							if (num != 3791609970U)
							{
								if (num != 3913569084U)
								{
									goto IL_15EA;
								}
								if (!(_speech == "excluir palavra à direita"))
								{
									goto IL_15EA;
								}
								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.DELETE);
								flag = true;
								goto IL_15EA;
							}
							else
							{
								if (!(_speech == "cortar texto"))
								{
									goto IL_15EA;
								}
								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
								flag = true;
								goto IL_15EA;
							}
						}
						else if (num != 3944760364U)
						{
							if (num != 3950543336U)
							{
								goto IL_15EA;
							}
							if (!(_speech == "apagar palavra"))
							{
								goto IL_15EA;
							}
							goto IL_1390;
						}
						else
						{
							if (!(_speech == "ir para o fim da linha"))
							{
								goto IL_15EA;
							}
							goto IL_146D;
						}
					}
					else if (num <= 4110630357U)
					{
						if (num != 3962197743U)
						{
							if (num != 4007168538U)
							{
								if (num != 4110630357U)
								{
									goto IL_15EA;
								}
								if (!(_speech == "subir a linha"))
								{
									goto IL_15EA;
								}
								InputSimulator.SimulateKeyPress(VirtualKeyCode.UP);
								flag = true;
								goto IL_15EA;
							}
							else
							{
								if (!(_speech == "vá para a palavra anterior"))
								{
									goto IL_15EA;
								}
								InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.LEFT);
								flag = true;
								goto IL_15EA;
							}
						}
						else
						{
							if (!(_speech == "ativar sublinhado"))
							{
								goto IL_15EA;
							}
							goto IL_133D;
						}
					}
					else if (num <= 4179054064U)
					{
						if (num != 4128750231U)
						{
							if (num != 4179054064U)
							{
								goto IL_15EA;
							}
							if (!(_speech == "selecionar linha"))
							{
								goto IL_15EA;
							}
							goto IL_149D;
						}
						else if (!(_speech == "inserir página"))
						{
							goto IL_15EA;
						}
					}
					else if (num != 4217422903U)
					{
						if (num != 4225848079U)
						{
							goto IL_15EA;
						}
						if (!(_speech == "nova página"))
						{
							goto IL_15EA;
						}
					}
					else
					{
						if (!(_speech == "desfazer"))
						{
							goto IL_15EA;
						}
						goto IL_127B;
					}
					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.RETURN);
					flag = true;
					goto IL_15EA;
					IL_1309:
					InputSimulator.SimulateKeyPress(VirtualKeyCode.F7);
					flag = true;
					goto IL_15EA;
					IL_149D:
					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.HOME);
					flag = true;
					goto IL_15EA;
					IL_127B:
					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_Z);
					flag = true;
					goto IL_15EA;
					IL_1319:
					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_N);
					flag = true;
					goto IL_15EA;
					IL_132B:
					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_K);
					flag = true;
					goto IL_15EA;
					IL_133D:
					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_S);
					flag = true;
					goto IL_15EA;
					IL_1381:
					InputSimulator.SimulateKeyPress(VirtualKeyCode.BACK);
					flag = true;
					goto IL_15EA;
					IL_1390:
					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.BACK);
					flag = true;
					goto IL_15EA;
					IL_13E7:
					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.F3);
					flag = true;
					goto IL_15EA;
					IL_141D:
					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, new VirtualKeyCode[]
					{
						VirtualKeyCode.SHIFT,
						VirtualKeyCode.LEFT
					});
					flag = true;
					goto IL_15EA;
					IL_146D:
					InputSimulator.SimulateKeyPress(VirtualKeyCode.END);
					flag = true;
					goto IL_15EA;
					IL_150F:
					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.SHIFT, VirtualKeyCode.RETURN);
					flag = true;
				}
				IL_15EA:;
			}
			bool flag3 = _speech == "." || _speech == "," || _speech == ";" || _speech == ":";
			if (flag3)
			{
				System.Windows.Clipboard.SetText(_speech);
				InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
			}
			else
			{
				bool flag4 = !flag;
				if (flag4)
				{
					System.Windows.Clipboard.SetText(" " + _speech);
					InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
				}
			}
			this.TimerRecOFF = this.LoadTimerRecOff;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000266B File Offset: 0x0000086B
		private void cbTipo_DropDownClosed(object sender, EventArgs e)
		{
			Settings.Default.cSyn = this.cbTipo.Text;
			Settings.Default.Save();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0002F6B8 File Offset: 0x0002D8B8
		private void cbNum_DropDownClosed(object sender, EventArgs e)
		{
			Settings.Default.tSyn = Convert.ToInt32(this.cbNum.Text);
			switch (Settings.Default.tSyn)
			{
			case 1:
				Settings.Default.tEspera = ".";
				break;
			case 2:
				Settings.Default.tEspera = ". .";
				break;
			case 3:
				Settings.Default.tEspera = ". . .";
				break;
			case 4:
				Settings.Default.tEspera = ". . . .";
				break;
			case 5:
				Settings.Default.tEspera = ". . . . .";
				break;
			}
			Settings.Default.Save();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000268F File Offset: 0x0000088F
		private void btnAtualiza_Click(object sender, RoutedEventArgs e)
		{
			AutoUpdater.Start("https://raw.githubusercontent.com/jarbas1986/update/master/UpdateAV", null);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0002F778 File Offset: 0x0002D978
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/A V®;component/avjarvis.xaml", UriKind.Relative);
				System.Windows.Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0002F7B0 File Offset: 0x0002D9B0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.Window = (AVJARVIS)target;
				this.Window.MouseLeftButtonDown += this.Window_MouseLeftButtonDown;
				this.Window.Loaded += this.Window_Loaded;
				break;
			case 2:
				this.LayoutRoot = (Grid)target;
				break;
			case 3:
				this.gridJarvis = (Grid)target;
				break;
			case 4:
				this.tabControle = (System.Windows.Controls.TabControl)target;
				break;
			case 5:
				((TabItem)target).MouseEnter += this.TabItem_MouseEnter;
				break;
			case 6:
				this.pbMicro = (System.Windows.Controls.ProgressBar)target;
				break;
			case 7:
				this.lblSpeech = (System.Windows.Controls.Label)target;
				break;
			case 8:
				this.sliderRec = (Slider)target;
				this.sliderRec.ValueChanged += this.sliderRec_ValueChanged;
				break;
			case 9:
				this.rect = (System.Windows.Shapes.Rectangle)target;
				break;
			case 10:
				this.lblPrecision = (System.Windows.Controls.Label)target;
				break;
			case 11:
				this.cbNum = (System.Windows.Controls.ComboBox)target;
				this.cbNum.DropDownClosed += this.cbNum_DropDownClosed;
				break;
			case 12:
				this.cbTipo = (System.Windows.Controls.ComboBox)target;
				this.cbTipo.DropDownClosed += this.cbTipo_DropDownClosed;
				break;
			case 13:
				this.lblMicro = (System.Windows.Controls.Label)target;
				break;
			case 14:
				this.btnPrueba = (System.Windows.Controls.Button)target;
				this.btnPrueba.Click += this.btnPrueba_Click;
				break;
			case 15:
				((TabItem)target).MouseEnter += this.TabItem_MouseEnter_1;
				break;
			case 16:
				this.tbVersion = (TextBlock)target;
				break;
			case 17:
				this.btnDonar = (System.Windows.Controls.Button)target;
				this.btnDonar.Click += this.btnDonar_Click;
				this.btnDonar.MouseEnter += this.btnDonar_MouseEnter;
				break;
			case 18:
				this.btnAtualiza = (System.Windows.Controls.Button)target;
				this.btnAtualiza.Click += this.btnAtualiza_Click;
				this.btnAtualiza.MouseEnter += this.btnDonar_MouseEnter;
				break;
			case 19:
				this.skin1 = (Grid)target;
				break;
			case 20:
				this.ElipseColor1 = (Ellipse)target;
				break;
			case 21:
				this.ElipseColor2 = (Ellipse)target;
				break;
			case 22:
				this.ElipseColorHud = (Ellipse)target;
				break;
			case 23:
				this.ElipseColorHud2 = (Ellipse)target;
				this.ElipseColorHud2.MouseRightButtonDown += this.ElipseColorHud2_MouseRightButtonDown;
				break;
			case 24:
				this.btnJarvis = (System.Windows.Controls.Button)target;
				this.btnJarvis.MouseEnter += this.btnJarvis_MouseEnter;
				break;
			case 25:
				this.btnFiltro = (System.Windows.Controls.Button)target;
				this.btnFiltro.Click += this.btnFiltro_Click;
				this.btnFiltro.MouseEnter += this.btnFiltro_MouseEnter;
				break;
			case 26:
				this.gridSlider = (Grid)target;
				break;
			case 27:
				this.anillo = (System.Windows.Shapes.Path)target;
				this.anillo.MouseRightButtonDown += this.anillo_MouseRightButtonDown;
				break;
			case 28:
				this.bar1 = (System.Windows.Shapes.Path)target;
				break;
			case 29:
				this.bar2 = (System.Windows.Shapes.Path)target;
				break;
			case 30:
				this.bar3 = (System.Windows.Shapes.Path)target;
				break;
			case 31:
				this.bar4 = (System.Windows.Shapes.Path)target;
				break;
			case 32:
				this.bar5 = (System.Windows.Shapes.Path)target;
				break;
			case 33:
				this.bar6 = (System.Windows.Shapes.Path)target;
				break;
			case 34:
				this.bar7 = (System.Windows.Shapes.Path)target;
				break;
			case 35:
				this.bar8 = (System.Windows.Shapes.Path)target;
				break;
			case 36:
				this.bar9 = (System.Windows.Shapes.Path)target;
				break;
			case 37:
				this.skin2 = (Grid)target;
				break;
			case 38:
				this.rectSkin2 = (System.Windows.Shapes.Rectangle)target;
				this.rectSkin2.MouseRightButtonDown += this.Rectangle_MouseRightButtonDown;
				break;
			case 39:
				this.pbMicro2 = (System.Windows.Controls.ProgressBar)target;
				break;
			case 40:
				this.lblSpeech2 = (System.Windows.Controls.Label)target;
				this.lblSpeech2.MouseRightButtonDown += this.lblSpeech2_MouseRightButtonDown;
				this.lblSpeech2.MouseLeftButtonDown += this.lblSpeech2_MouseLeftButtonDown;
				this.lblSpeech2.MouseEnter += this.lblSpeech2_MouseEnter;
				break;
			case 41:
				this.btnJarvis2 = (System.Windows.Controls.Button)target;
				this.btnJarvis2.MouseLeftButtonDown += this.btnJarvisCfg2_MouseLeftButtonDown;
				this.btnJarvis2.MouseEnter += this.btnJarvis2_MouseEnter;
				break;
			case 42:
				this.btnCmd = (System.Windows.Controls.Button)target;
				this.btnCmd.MouseLeftButtonDown += this.btnCmd_MouseLeftButtonDown;
				this.btnCmd.Click += this.btnCmd_Click;
				this.btnCmd.MouseEnter += this.btnCmd_MouseEnter;
				break;
			case 43:
				this.popupJarvis = (Popup)target;
				break;
			case 44:
				this.cbSound = (System.Windows.Controls.CheckBox)target;
				this.cbSound.Checked += this.cbSound_Checked;
				this.cbSound.Unchecked += this.cbSound_Unchecked;
				break;
			case 45:
				this.cbTopMost = (System.Windows.Controls.CheckBox)target;
				this.cbTopMost.Checked += this.cbTopMost_Checked;
				this.cbTopMost.Unchecked += this.cbTopMost_Unchecked;
				break;
			case 46:
				this.btnClosepp = (System.Windows.Controls.Button)target;
				this.btnClosepp.Click += this.btnClosepp_Click;
				break;
			case 47:
				this.cbskin = (System.Windows.Controls.ComboBox)target;
				this.cbskin.DropDownClosed += this.cbskin_DropDownClosed;
				break;
			case 48:
				this.btnTraining = (System.Windows.Controls.Button)target;
				this.btnTraining.Click += this.btnTraining_Click;
				break;
			case 49:
				this.sliderVolumen = (Slider)target;
				break;
			case 50:
				this.label_Copy = (System.Windows.Controls.Label)target;
				break;
			case 51:
				this.cbOpacidad = (System.Windows.Controls.CheckBox)target;
				this.cbOpacidad.Checked += this.cbOpacidad_Checked;
				this.cbOpacidad.Unchecked += this.cbOpacidad_Unchecked;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x0400002E RID: 46
		public string[] AlarmFrase = new string[]
		{
			"1 da manhã",
			"1 e 1 da manhã",
			"1 e 2 da manhã",
			"1 e 3 da manhã",
			"1 e 4 da manhã",
			"1 e 5 da manhã",
			"1 h 6 da manhã",
			"1 e 7 da manhã",
			"1 e 8 da manhã",
			"1 e 9 da manhã",
			"1 e 10 da manhã",
			"1 e 11 da manhã",
			"1 h 12 da manhã",
			"1 e 13 da manhã",
			"1 e 14 da manhã",
			"1 e 15 da manhã",
			"1 e 16 da manhã",
			"1 e 17 da manhã",
			"1 h 18 da manhã",
			"1 e 19 da manhã",
			"1 e 20 da manhã",
			"1 e 21 da manhã",
			"1 e 22 da manhã",
			"1 e 23 da manhã",
			"1 h 24 da manhã",
			"1 e 25 da manhã",
			"1 e 26 da manhã",
			"1 e 27 da manhã",
			"1 e 28 da manhã",
			"1 e 29 da manhã",
			"1 h 30 da manhã",
			"1 e 31 da manhã",
			"1 e 32 da manhã",
			"1 e 33 da manhã",
			"1 e 34 da manhã",
			"1 e 35 da manhã",
			"1 h 36 da manhã",
			"1 e 37 da manhã",
			"1 e 38 da manhã",
			"1 e 39 da manhã",
			"1 e 40 da manhã",
			"1 e 41 da manhã",
			"1 h 42 da manhã",
			"1 e 43 da manhã",
			"1 e 44 da manhã",
			"1 e 45 da manhã",
			"1 e 46 da manhã",
			"1 e 47 da manhã",
			"1 h 48 da manhã",
			"1 e 49 da manhã",
			"1 e 50 da manhã",
			"1 e 51 da manhã",
			"1 e 52 da manhã",
			"1 e 53 da manhã",
			"1 h 54 da manhã",
			"1 e 55 da manhã",
			"1 e 56 da manhã",
			"1 e 57 da manhã",
			"1 e 58 da manhã",
			"1 e 59 da manhã",
			"2 da manhã",
			"2 e 1 da manhã",
			"2 e 2 da manhã",
			"2 e 3 da manhã",
			"2 e 4 da manhã",
			"2 e 5 da manhã",
			"2 e 6 da manhã",
			"2 e 7 da manhã",
			"2 e 8 da manhã",
			"2 e 9 da manhã",
			"2 e 10 da manhã",
			"2 e 11 da manhã",
			"2 e 12 da manhã",
			"2 h 13 da manhã",
			"2 e 14 da manhã",
			"2 e 15 da manhã",
			"2 e 16 da manhã",
			"2 e 17 da manhã",
			"2 e 18 da manhã",
			"2 h 19 da manhã",
			"2 e 20 da manhã",
			"2 e 21 da manhã",
			"2 e 22 da manhã",
			"2 e 23 da manhã",
			"2 e 24 da manhã",
			"2 h 25 da manhã",
			"2 e 26 da manhã",
			"2 e 27 da manhã",
			"2 e 28 da manhã",
			"2 e 29 da manhã",
			"2 e 30 da manhã",
			"2 h 31 da manhã",
			"2 e 32 da manhã",
			"2 e 33 da manhã",
			"2 e 34 da manhã",
			"2 e 35 da manhã",
			"2 e 36 da manhã",
			"2 h 37 da manhã",
			"2 e 38 da manhã",
			"2 e 39 da manhã",
			"2 e 40 da manhã",
			"2 e 41 da manhã",
			"2 e 42 da manhã",
			"2 h 43 da manhã",
			"2 e 44 da manhã",
			"2 e 45 da manhã",
			"2 e 46 da manhã",
			"2 e 47 da manhã",
			"2 e 48 da manhã",
			"2 h 49 da manhã",
			"2 e 50 da manhã",
			"2 e 51 da manhã",
			"2 e 52 da manhã",
			"2 e 53 da manhã",
			"2 e 54 da manhã",
			"2 h 55 da manhã",
			"2 e 56 da manhã",
			"2 e 57 da manhã",
			"2 e 58 da manhã",
			"2 e 59 da manhã",
			"3 da manhã",
			"3 e 1 da manhã",
			"3 e 2 da manhã",
			"3 e 3 da manhã",
			"3 e 4 da manhã",
			"3 e 5 da manhã",
			"3 h 6 da manhã",
			"3 e 7 da manhã",
			"3 e 8 da manhã",
			"3 e 9 da manhã",
			"3 e 10 da manhã",
			"3 e 11 da manhã",
			"3 h 12 da manhã",
			"3 e 13 da manhã",
			"3 e 14 da manhã",
			"3 e 15 da manhã",
			"3 e 16 da manhã",
			"3 e 17 da manhã",
			"3 h 18 da manhã",
			"3 e 19 da manhã",
			"3 e 20 da manhã",
			"3 e 21 da manhã",
			"3 e 22 da manhã",
			"3 e 23 da manhã",
			"3 h 24 da manhã",
			"3 e 25 da manhã",
			"3 e 26 da manhã",
			"3 e 27 da manhã",
			"3 e 28 da manhã",
			"3 e 29 da manhã",
			"3 h 30 da manhã",
			"3 e 31 da manhã",
			"3 e 32 da manhã",
			"3 e 33 da manhã",
			"3 e 34 da manhã",
			"3 e 35 da manhã",
			"3 h 36 da manhã",
			"3 e 37 da manhã",
			"3 e 38 da manhã",
			"3 e 39 da manhã",
			"3 e 40 da manhã",
			"3 e 41 da manhã",
			"3 h 42 da manhã",
			"3 e 43 da manhã",
			"3 e 44 da manhã",
			"3 e 45 da manhã",
			"3 e 46 da manhã",
			"3 e 47 da manhã",
			"3 h 48 da manhã",
			"3 e 49 da manhã",
			"3 e 50 da manhã",
			"3 e 51 da manhã",
			"3 e 52 da manhã",
			"3 e 53 da manhã",
			"3 h 54 da manhã",
			"3 e 55 da manhã",
			"3 e 56 da manhã",
			"3 e 57 da manhã",
			"3 e 58 da manhã",
			"3 e 59 da manhã",
			"4 da manhã",
			"4 e 1 da manhã",
			"4 e 2 da manhã",
			"4 e 3 da manhã",
			"4 e 4 da manhã",
			"4 e 5 da manhã",
			"4 e 6 da manhã",
			"4 h 7 da manhã",
			"4 e 8 da manhã",
			"4 e 9 da manhã",
			"4 e 10 da manhã",
			"4 e 11 da manhã",
			"4 e 12 da manhã",
			"4 h 13 da manhã",
			"4 e 14 da manhã",
			"4 e 15 da manhã",
			"4 e 16 da manhã",
			"4 e 17 da manhã",
			"4 e 18 da manhã",
			"4 h 19 da manhã",
			"4 e 20 da manhã",
			"4 e 21 da manhã",
			"4 e 22 da manhã",
			"4 e 23 da manhã",
			"4 e 24 da manhã",
			"4 h 25 da manhã",
			"4 e 26 da manhã",
			"4 e 27 da manhã",
			"4 e 28 da manhã",
			"4 e 29 da manhã",
			"4 e 30 da manhã",
			"4 h 31 da manhã",
			"4 e 32 da manhã",
			"4 e 33 da manhã",
			"4 e 34 da manhã",
			"4 e 35 da manhã",
			"4 e 36 da manhã",
			"4 h 37 da manhã",
			"4 e 38 da manhã",
			"4 e 39 da manhã",
			"4 e 40 da manhã",
			"4 e 41 da manhã",
			"4 e 42 da manhã",
			"4 h 43 da manhã",
			"4 e 44 da manhã",
			"4 e 45 da manhã",
			"4 e 46 da manhã",
			"4 e 47 da manhã",
			"4 e 48 da manhã",
			"4 h 49 da manhã",
			"4 e 50 da manhã",
			"4 e 51 da manhã",
			"4 e 52 da manhã",
			"4 e 53 da manhã",
			"4 e 54 da manhã",
			"4 h 55 da manhã",
			"4 e 56 da manhã",
			"4 e 57 da manhã",
			"4 e 58 da manhã",
			"4 e 59 da manhã",
			"5 da manhã",
			"5 e 1 da manhã",
			"5 e 2 da manhã",
			"5 e 3 da manhã",
			"5 e 4 da manhã",
			"5 e 5 da manhã",
			"5 e 6 da manhã",
			"5 e 7 da manhã",
			"5 e 8 da manhã",
			"5 e 9 da manhã",
			"5 e 10 da manhã",
			"5 e 11 da manhã",
			"5 e 12 da manhã",
			"5 e 13 da manhã",
			"5 e 14 da manhã",
			"5 e 15 da manhã",
			"5 e 16 da manhã",
			"5 e 17 da manhã",
			"5 e 18 da manhã",
			"5 e 19 da manhã",
			"5 e 20 da manhã",
			"5 e 21 da manhã",
			"5 e 22 da manhã",
			"5 e 23 da manhã",
			"5 e 24 da manhã",
			"5 e 25 da manhã",
			"5 e 26 da manhã",
			"5 e 27 da manhã",
			"5 e 28 da manhã",
			"5 e 29 da manhã",
			"5 e 30 da manhã",
			"5 e 31 da manhã",
			"5 e 32 da manhã",
			"5 e 33 da manhã",
			"5 e 34 da manhã",
			"5 e 35 da manhã",
			"5 e 36 da manhã",
			"5 e 37 da manhã",
			"5 e 38 da manhã",
			"5 e 39 da manhã",
			"5 e 40 da manhã",
			"5 e 41 da manhã",
			"5 e 42 da manhã",
			"5 e 43 da manhã",
			"5 e 44 da manhã",
			"5 e 45 da manhã",
			"5 e 46 da manhã",
			"5 e 47 da manhã",
			"5 e 48 da manhã",
			"5 e 49 da manhã",
			"5 e 50 da manhã",
			"5 e 51 da manhã",
			"5 e 52 da manhã",
			"5 e 53 da manhã",
			"5 e 54 da manhã",
			"5 e 55 da manhã",
			"5 e 56 da manhã",
			"5 e 57 da manhã",
			"5 e 58 da manhã",
			"5 e 59 da manhã",
			"6 da manhã",
			"6 e 1 da manhã",
			"6 e 2 da manhã",
			"6 e 3 da manhã",
			"6 e 4 da manhã",
			"6 e 5 da manhã",
			"6 e 6 da manhã",
			"6 e 7 da manhã",
			"6 e 8 da manhã",
			"6 e 9 da manhã",
			"6 e 10 da manhã",
			"6 e 11 da manhã",
			"6 e 12 da manhã",
			"6 e 13 da manhã",
			"6 e 14 da manhã",
			"6 e 15 da manhã",
			"6 e 16 da manhã",
			"6 e 17 da manhã",
			"6 e 18 da manhã",
			"6 e 19 da manhã",
			"6 e 20 da manhã",
			"6 e 21 da manhã",
			"6 e 22 da manhã",
			"6 e 23 da manhã",
			"6 e 24 da manhã",
			"6 e 25 da manhã",
			"6 e 26 da manhã",
			"6 e 27 da manhã",
			"6 e 28 da manhã",
			"6 e 29 da manhã",
			"6 e 30 da manhã",
			"6 e 31 da manhã",
			"6 e 32 da manhã",
			"6 e 33 da manhã",
			"6 e 34 da manhã",
			"6 e 35 da manhã",
			"6 e 36 da manhã",
			"6 e 37 da manhã",
			"6 e 38 da manhã",
			"6 e 39 da manhã",
			"6 e 40 da manhã",
			"6 e 41 da manhã",
			"6 e 42 da manhã",
			"6 e 43 da manhã",
			"6 e 44 da manhã",
			"6 e 45 da manhã",
			"6 e 46 da manhã",
			"6 e 47 da manhã",
			"6 e 48 da manhã",
			"6 e 49 da manhã",
			"6 e 50 da manhã",
			"6 e 51 da manhã",
			"6 e 52 da manhã",
			"6 e 53 da manhã",
			"6 e 54 da manhã",
			"6 e 55 da manhã",
			"6 e 56 da manhã",
			"6 e 57 da manhã",
			"6 e 58 da manhã",
			"6 e 59 da manhã",
			"7 da manhã",
			"7 e 1 da manhã",
			"7 e 2 da manhã",
			"7 e 3 da manhã",
			"7 e 4 da manhã",
			"7 e 5 da manhã",
			"7 e 6 da manhã",
			"7 e 7 da manhã",
			"7 e 8 da manhã",
			"7 e 9 da manhã",
			"7 e 10 da manhã",
			"7 e 11 da manhã",
			"7 e 12 da manhã",
			"7 e 13 da manhã",
			"7 e 14 da manhã",
			"7 e 15 da manhã",
			"7 e 16 da manhã",
			"7 e 17 da manhã",
			"7 e 18 da manhã",
			"7 e 19 da manhã",
			"7 e 20 da manhã",
			"7 e 21 da manhã",
			"7 e 22 da manhã",
			"7 e 23 da manhã",
			"7 e 24 da manhã",
			"7 e 25 da manhã",
			"7 e 26 da manhã",
			"7 e 27 da manhã",
			"7 e 28 da manhã",
			"7 e 29 da manhã",
			"7 e 30 da manhã",
			"7 e 31 da manhã",
			"7 e 32 da manhã",
			"7 e 33 da manhã",
			"7 e 34 da manhã",
			"7 e 35 da manhã",
			"3 e 36 da manhã",
			"7 e 37 da manhã",
			"7 e 38 da manhã",
			"7 e 39 da manhã",
			"7 e 40 da manhã",
			"7 e 41 da manhã",
			"7 e 42 da manhã",
			"7 e 43 da manhã",
			"7 e 44 da manhã",
			"7 e 45 da manhã",
			"7 e 46 da manhã",
			"7 e 47 da manhã",
			"7 e 48 da manhã",
			"7 e 49 da manhã",
			"7 e 50 da manhã",
			"7 e 51 da manhã",
			"7 e 52 da manhã",
			"7 e 53 da manhã",
			"7 e 54 da manhã",
			"7 e 55 da manhã",
			"7 e 56 da manhã",
			"7 e 57 da manhã",
			"7 e 58 da manhã",
			"7 e 59 da manhã",
			"8 da manhã",
			"8 e 1 da manhã",
			"8 e 2 da manhã",
			"8 e 3 da manhã",
			"8 e 4 da manhã",
			"8 e 5 da manhã",
			"8 e 6 da manhã",
			"8 e 7 da manhã",
			"8 e 8 da manhã",
			"8 e 9 da manhã",
			"8 e 10 da manhã",
			"8 e 11 da manhã",
			"8 e 12 da manhã",
			"8 e 13 da manhã",
			"8 e 14 da manhã",
			"8 e 15 da manhã",
			"8 e 16 da manhã",
			"8 e 17 da manhã",
			"8 e 18 da manhã",
			"8 e 19 da manhã",
			"8 e 20 da manhã",
			"8 e 21 da manhã",
			"8 e 22 da manhã",
			"8 e 23 da manhã",
			"8 e 24 da manhã",
			"8 e 25 da manhã",
			"8 e 26 da manhã",
			"8 e 27 da manhã",
			"8 e 28 da manhã",
			"8 e 29 da manhã",
			"8 e 30 da manhã",
			"8 e 31 da manhã",
			"8 e 32 da manhã",
			"8 e 33 da manhã",
			"8 e 34 da manhã",
			"8 e 35 da manhã",
			"8 e 36 da manhã",
			"8 e 37 da manhã",
			"8 e 38 da manhã",
			"8 e 39 da manhã",
			"8 e 40 da manhã",
			"8 e 41 da manhã",
			"8 e 42 da manhã",
			"8 e 43 da manhã",
			"8 e 44 da manhã",
			"8 e 45 da manhã",
			"8 e 46 da manhã",
			"8 e 47 da manhã",
			"8 e 48 da manhã",
			"8 e 49 da manhã",
			"8 e 50 da manhã",
			"8 e 51 da manhã",
			"8 e 52 da manhã",
			"8 e 53 da manhã",
			"8 e 54 da manhã",
			"8 e 55 da manhã",
			"8 e 56 da manhã",
			"8 e 57 da manhã",
			"8 e 58 da manhã",
			"8 e 59 da manhã",
			"9 da manhã",
			"9 e 1 da manhã",
			"9 e 2 da manhã",
			"9 e 3 da manhã",
			"9 e 4 da manhã",
			"9 e 5 da manhã",
			"9 e 6 da manhã",
			"9 e 7 da manhã",
			"9 e 8 da manhã",
			"9 e 9 da manhã",
			"9 e 10 da manhã",
			"9 e 11 da manhã",
			"9 e 12 da manhã",
			"9 e 13 da manhã",
			"9 e 14 da manhã",
			"9 e 15 da manhã",
			"9 e 16 da manhã",
			"9 e 17 da manhã",
			"9 e 18 da manhã",
			"9 e 19 da manhã",
			"9 e 20 da manhã",
			"9 e 21 da manhã",
			"9 e 22 da manhã",
			"9 e 23 da manhã",
			"9 e 24 da manhã",
			"9 e 25 da manhã",
			"9 e 26 da manhã",
			"9 e 27 da manhã",
			"9 e 28 da manhã",
			"9 e 29 da manhã",
			"9 e 30 da manhã",
			"9 e 31 da manhã",
			"9 e 32 da manhã",
			"9 e 33 da manhã",
			"9 e 34 da manhã",
			"9 e 35 da manhã",
			"9 e 36 da manhã",
			"9 e 37 da manhã",
			"9 e 38 da manhã",
			"9 e 39 da manhã",
			"9 e 40 da manhã",
			"9 e 41 da manhã",
			"9 e 42 da manhã",
			"9 e 43 da manhã",
			"9 e 44 da manhã",
			"9 e 45 da manhã",
			"9 e 46 da manhã",
			"9 e 47 da manhã",
			"9 e 48 da manhã",
			"9 e 49 da manhã",
			"9 e 50 da manhã",
			"9 e 51 da manhã",
			"9 e 52 da manhã",
			"9 e 53 da manhã",
			"9 e 54 da manhã",
			"9 e 55 da manhã",
			"9 e 56 da manhã",
			"9 e 57 da manhã",
			"9 e 58 da manhã",
			"9 e 59 da manhã",
			"10 da manhã",
			"10 e 1 da manhã",
			"10 e 2 da manhã",
			"10 e 3 da manhã",
			"10 e 4 da manhã",
			"10 e 5 da manhã",
			"10 e 6 da manhã",
			"10 e 7 da manhã",
			"10 e 8 da manhã",
			"10 e 9 da manhã",
			"10 e 10 da manhã",
			"10 e 11 da manhã",
			"10 e 12 da manhã",
			"10 e 13 da manhã",
			"10 e 14 da manhã",
			"10 e 15 da manhã",
			"10 e 16 da manhã",
			"10 e 17 da manhã",
			"10 e 18 da manhã",
			"10 e 19 da manhã",
			"10 e 20 da manhã",
			"10 e 21 da manhã",
			"10 e 22 da manhã",
			"10 e 23 da manhã",
			"10 e 24 da manhã",
			"10 e 25 da manhã",
			"10 e 26 da manhã",
			"10 e 27 da manhã",
			"10 e 28 da manhã",
			"10 e 29 da manhã",
			"10 e 30 da manhã",
			"10 e 31 da manhã",
			"10 e 32 da manhã",
			"10 e 33 da manhã",
			"10 e 34 da manhã",
			"10 e 35 da manhã",
			"10 e 36 da manhã",
			"10 e 37 da manhã",
			"10 e 38 da manhã",
			"10 e 39 da manhã",
			"10 e 40 da manhã",
			"10 e 41 da manhã",
			"10 e 42 da manhã",
			"10 e 43 da manhã",
			"10 e 44 da manhã",
			"10 e 45 da manhã",
			"10 e 46 da manhã",
			"10 e 47 da manhã",
			"10 e 48 da manhã",
			"10 e 49 da manhã",
			"10 e 50 da manhã",
			"10 e 51 da manhã",
			"10 e 52 da manhã",
			"10 e 53 da manhã",
			"10 e 54 da manhã",
			"10 e 55 da manhã",
			"10 e 56 da manhã",
			"10 e 57 da manhã",
			"10 e 58 da manhã",
			"10 e 59 da manhã",
			"11 da manhã",
			"11 e 1 da manhã",
			"11 e 2 da manhã",
			"11 e 3 da manhã",
			"11 e 4 da manhã",
			"11 e 5 da manhã",
			"11 e 6 da manhã",
			"11 e 7 da manhã",
			"11 e 8 da manhã",
			"11 e 9 da manhã",
			"11 e 10 da manhã",
			"11 e 11 da manhã",
			"11 e 12 da manhã",
			"11 e 13 da manhã",
			"11 e 14 da manhã",
			"11 e 15 da manhã",
			"11 e 16 da manhã",
			"11 e 17 da manhã",
			"11 e 18 da manhã",
			"11 e 19 da manhã",
			"11 e 20 da manhã",
			"11 e 21 da manhã",
			"11 e 22 da manhã",
			"11 e 23 da manhã",
			"11 e 24 da manhã",
			"11 e 25 da manhã",
			"11 e 26 da manhã",
			"11 e 27 da manhã",
			"11 e 28 da manhã",
			"11 e 29 da manhã",
			"11 e 30 da manhã",
			"11 e 31 da manhã",
			"11 e 32 da manhã",
			"11 e 33 da manhã",
			"11 e 34 da manhã",
			"11 e 35 da manhã",
			"11 e 36 da manhã",
			"11 e 37 da manhã",
			"11 e 38 da manhã",
			"11 e 39 da manhã",
			"11 e 40 da manhã",
			"11 e 41 da manhã",
			"11 e 42 da manhã",
			"11 e 43 da manhã",
			"11 e 44 da manhã",
			"11 e 45 da manhã",
			"11 e 46 da manhã",
			"11 e 47 da manhã",
			"11 e 48 da manhã",
			"11 e 49 da manhã",
			"11 e 50 da manhã",
			"11 e 51 da manhã",
			"11 e 52 da manhã",
			"11 e 53 da manhã",
			"11 e 54 da manhã",
			"11 e 55 da manhã",
			"11 e 56 da manhã",
			"11 e 57 da manhã",
			"11 e 58 da manhã",
			"11 e 59 da manhã",
			"0 da manhã",
			"0 e 1 da manhã",
			"0 e 2 da manhã",
			"0 e 3 da manhã",
			"0 e 4 da manhã",
			"0 e 5 da manhã",
			"0 e 6 da manhã",
			"0 e 7 da manhã",
			"0 e 8 da manhã",
			"0 e 9 da manhã",
			"0 e 10 da manhã",
			"0 e 11 da manhã",
			"0 e 12 da manhã",
			"0 e 13 da manhã",
			"0 e 14 da manhã",
			"0 e 15 da manhã",
			"0 e 16 da manhã",
			"0 e 17 da manhã",
			"0 e 18 da manhã",
			"0 e 19 da manhã",
			"0 e 20 da manhã",
			"0 e 21 da manhã",
			"0 e 22 da manhã",
			"0 e 23 da manhã",
			"0 e 24 da manhã",
			"0 e 25 da manhã",
			"0 e 26 da manhã",
			"0 e 27 da manhã",
			"0 e 28 da manhã",
			"0 e 29 da manhã",
			"0 e 30 da manhã",
			"0 e 31 da manhã",
			"0 e 32 da manhã",
			"0 e 33 da manhã",
			"0 e 34 da manhã",
			"0 e 35 da manhã",
			"0 e 36 da manhã",
			"0 e 37 da manhã",
			"0 e 38 da manhã",
			"0 e 39 da manhã",
			"0 e 40 da manhã",
			"0 e 41 da manhã",
			"0 e 42 da manhã",
			"0 e 43 da manhã",
			"0 e 44 da manhã",
			"0 e 45 da manhã",
			"0 e 46 da manhã",
			"0 e 47 da manhã",
			"0 e 48 da manhã",
			"0 e 49 da manhã",
			"0 e 50 da manhã",
			"0 e 51 da manhã",
			"0 e 52 da manhã",
			"0 e 53 da manhã",
			"0 e 54 da manhã",
			"0 e 55 da manhã",
			"0 e 56 da manhã",
			"0 e 57 da manhã",
			"0 e 58 da manhã",
			"0 e 59 da manhã",
			"1 da tarde",
			"1 e 1 da tarde",
			"1 e 2 da tarde",
			"1 e 3 da tarde",
			"1 e 4 da tarde",
			"1 e 5 da tarde",
			"1 e 6 da tarde",
			"1 e 7 da tarde",
			"1 e 8 da tarde",
			"1 e 9 da tarde",
			"1 e 10 da tarde",
			"1 e 11 da tarde",
			"1 e 12 da tarde",
			"1 e 13 da tarde",
			"1 e 14 da tarde",
			"1 e 15 da tarde",
			"1 e 16 da tarde",
			"1 e 17 da tarde",
			"1 e 18 da tarde",
			"1 e 19 da tarde",
			"1 e 20 da tarde",
			"1 e 21 da tarde",
			"1 e 22 da tarde",
			"1 e 23 da tarde",
			"1 e 24 da tarde",
			"1 e 25 da tarde",
			"1 e 26 da tarde",
			"1 e 27 da tarde",
			"1 e 28 da tarde",
			"1 e 29 da tarde",
			"1 e 30 da tarde",
			"1 e 31 da tarde",
			"1 e 32 da tarde",
			"1 e 33 da tarde",
			"1 e 34 da tarde",
			"1 e 35 da tarde",
			"1 e 36 da tarde",
			"1 e 37 da tarde",
			"1 e 38 da tarde",
			"1 e 39 da tarde",
			"1 e 40 da tarde",
			"1 e 41 da tarde",
			"1 e 42 da tarde",
			"1 e 43 da tarde",
			"1 e 44 da tarde",
			"1 e 45 da tarde",
			"1 e 46 da tarde",
			"1 e 47 da tarde",
			"1 e 48 da tarde",
			"1 e 49 da tarde",
			"1 e 50 da tarde",
			"1 e 51 da tarde",
			"1 e 52 da tarde",
			"1 e 53 da tarde",
			"1 e 54 da tarde",
			"1 e 55 da tarde",
			"1 e 56 da tarde",
			"1 e 57 da tarde",
			"1 e 58 da tarde",
			"1 e 59 da tarde",
			"2 da tarde",
			"2 e 1 da tarde",
			"2 e 2 da tarde",
			"2 e 3 da tarde",
			"2 e 4 da tarde",
			"2 e 5 da tarde",
			"2 e 6 da tarde",
			"2 e 7 da tarde",
			"2 e 8 da tarde",
			"2 e 9 da tarde",
			"2 e 10 da tarde",
			"2 e 11 da tarde",
			"2 e 12 da tarde",
			"2 e 13 da tarde",
			"2 e 14 da tarde",
			"2 e 15 da tarde",
			"2 e 16 da tarde",
			"2 e 17 da tarde",
			"2 e 18 da tarde",
			"2 e 19 da tarde",
			"2 e 20 da tarde",
			"2 e 21 da tarde",
			"2 e 22 da tarde",
			"2 e 23 da tarde",
			"2 e 24 da tarde",
			"2 e 25 da tarde",
			"2 e 26 da tarde",
			"2 e 27 da tarde",
			"2 e 28 da tarde",
			"2 e 29 da tarde",
			"2 e 30 da tarde",
			"2 e 31 da tarde",
			"2 e 32 da tarde",
			"2 e 33 da tarde",
			"2 e 34 da tarde",
			"2 e 35 da tarde",
			"2 e 36 da tarde",
			"2 e 37 da tarde",
			"2 e 38 da tarde",
			"2 e 39 da tarde",
			"2 e 40 da tarde",
			"2 e 41 da tarde",
			"2 e 42 da tarde",
			"2 e 43 da tarde",
			"2 e 44 da tarde",
			"2 e 45 da tarde",
			"2 e 46 da tarde",
			"2 e 47 da tarde",
			"2 e 48 da tarde",
			"2 e 49 da tarde",
			"2 e 50 da tarde",
			"2 e 51 da tarde",
			"2 e 52 da tarde",
			"2 e 53 da tarde",
			"2 e 54 da tarde",
			"2 e 55 da tarde",
			"2 e 56 da tarde",
			"2 e 57 da tarde",
			"2 e 58 da tarde",
			"2 e 59 da tarde",
			"3 da tarde",
			"3 e 1 da tarde",
			"3 e 2 da tarde",
			"3 e 3 da tarde",
			"3 e 4 da tarde",
			"3 e 5 da tarde",
			"3 e 6 da tarde",
			"3 e 7 da tarde",
			"3 e 8 da tarde",
			"3 e 9 da tarde",
			"3 e 10 da tarde",
			"3 e 11 da tarde",
			"3 e 12 da tarde",
			"3 e 13 da tarde",
			"3 e 14 da tarde",
			"3 e 15 da tarde",
			"3 e 16 da tarde",
			"3 e 17 da tarde",
			"3 e 18 da tarde",
			"3 e 19 da tarde",
			"3 e 20 da tarde",
			"3 e 21 da tarde",
			"3 e 22 da tarde",
			"3 e 23 da tarde",
			"3 e 24 da tarde",
			"3 e 25 da tarde",
			"3 e 26 da tarde",
			"3 e 27 da tarde",
			"3 e 28 da tarde",
			"3 e 29 da tarde",
			"3 e 30 da tarde",
			"3 e 31 da tarde",
			"3 e 32 da tarde",
			"3 e 33 da tarde",
			"3 e 34 da tarde",
			"3 e 35 da tarde",
			"3 e 36 da tarde",
			"3 e 37 da tarde",
			"3 e 38 da tarde",
			"3 e 39 da tarde",
			"3 e 40 da tarde",
			"3 e 41 da tarde",
			"3 e 42 da tarde",
			"3 e 43 da tarde",
			"3 e 44 da tarde",
			"3 e 45 da tarde",
			"3 e 46 da tarde",
			"3 e 47 da tarde",
			"3 e 48 da tarde",
			"3 e 49 da tarde",
			"3 e 50 da tarde",
			"3 e 51 da tarde",
			"3 e 52 da tarde",
			"3 e 53 da tarde",
			"3 e 54 da tarde",
			"3 e 55 da tarde",
			"3 e 56 da tarde",
			"3 e 57 da tarde",
			"3 e 58 da tarde",
			"3 e 59 da tarde",
			"4 da tarde",
			"4 e 1 da tarde",
			"4 e 2 da tarde",
			"4 e 3 da tarde",
			"4 e 4 da tarde",
			"4 e 5 da tarde",
			"4 e 6 da tarde",
			"4 e 7 da tarde",
			"4 e 8 da tarde",
			"4 e 9 da tarde",
			"4 e 10 da tarde",
			"4 e 11 da tarde",
			"4 e 12 da tarde",
			"4 e 13 da tarde",
			"4 e 14 da tarde",
			"4 e 15 da tarde",
			"4 e 16 da tarde",
			"4 e 17 da tarde",
			"4 e 18 da tarde",
			"4 e 19 da tarde",
			"4 e 20 da tarde",
			"4 e 21 da tarde",
			"4 e 22 da tarde",
			"4 e 23 da tarde",
			"4 e 24 da tarde",
			"4 e 25 da tarde",
			"4 e 26 da tarde",
			"4 e 27 da tarde",
			"4 e 28 da tarde",
			"4 e 29 da tarde",
			"4 e 30 da tarde",
			"4 e 31 da tarde",
			"4 e 32 da tarde",
			"4 e 33 da tarde",
			"4 e 34 da tarde",
			"4 e 35 da tarde",
			"4 e 36 da tarde",
			"4 e 37 da tarde",
			"4 e 38 da tarde",
			"4 e 39 da tarde",
			"4 e 40 da tarde",
			"4 e 41 da tarde",
			"4 e 42 da tarde",
			"4 e 43 da tarde",
			"4 e 44 da tarde",
			"4 e 45 da tarde",
			"4 e 46 da tarde",
			"4 e 47 da tarde",
			"4 e 48 da tarde",
			"4 e 49 da tarde",
			"4 e 50 da tarde",
			"4 e 51 da tarde",
			"4 e 52 da tarde",
			"4 e 53 da tarde",
			"4 e 54 da tarde",
			"4 e 55 da tarde",
			"4 e 56 da tarde",
			"4 e 57 da tarde",
			"4 e 58 da tarde",
			"4 e 59 da tarde",
			"5 da tarde",
			"5 e 1 da tarde",
			"5 e 2 da tarde",
			"5 e 3 da tarde",
			"5 e 4 da tarde",
			"5 e 5 da tarde",
			"5 e 6 da tarde",
			"5 e 7 da tarde",
			"5 e 8 da tarde",
			"5 e 9 da tarde",
			"5 e 10 da tarde",
			"5 e 11 da tarde",
			"5 e 12 da tarde",
			"5 e 13 da tarde",
			"5 e 14 da tarde",
			"5 e 15 da tarde",
			"5 e 16 da tarde",
			"5 e 17 da tarde",
			"5 e 18 da tarde",
			"5 e 19 da tarde",
			"5 e 20 da tarde",
			"5 e 21 da tarde",
			"5 e 22 da tarde",
			"5 e 23 da tarde",
			"5 e 24 da tarde",
			"5 e 25 da tarde",
			"5 e 26 da tarde",
			"5 e 27 da tarde",
			"5 e 28 da tarde",
			"5 e 29 da tarde",
			"5 e 30 da tarde",
			"5 e 31 da tarde",
			"5 e 32 da tarde",
			"5 e 33 da tarde",
			"5 e 34 da tarde",
			"5 e 35 da tarde",
			"5 e 36 da tarde",
			"5 e 37 da tarde",
			"5 e 38 da tarde",
			"5 e 39 da tarde",
			"5 e 40 da tarde",
			"5 e 41 da tarde",
			"5 e 42 da tarde",
			"5 e 43 da tarde",
			"5 e 44 da tarde",
			"5 e 45 da tarde",
			"5 e 46 da tarde",
			"5 e 47 da tarde",
			"5 e 48 da tarde",
			"5 e 49 da tarde",
			"5 e 50 da tarde",
			"5 e 51 da tarde",
			"5 e 52 da tarde",
			"5 e 53 da tarde",
			"5 e 54 da tarde",
			"5 e 55 da tarde",
			"5 e 56 da tarde",
			"5 e 57 da tarde",
			"5 e 58 da tarde",
			"5 e 59 da tarde",
			"6 da noite",
			"6 e 1 da noite",
			"6 e 2 da noite",
			"6 e 3 da noite",
			"6 e 4 da noite",
			"6 e 5 da noite",
			"6 e 6 da noite",
			"6 e 7 da noite",
			"6 e 8 da noite",
			"6 e 9 da noite",
			"6 e 10 da noite",
			"6 e 11 da noite",
			"6 e 12 da noite",
			"6 e 13 da noite",
			"6 e 14 da noite",
			"6 e 15 da noite",
			"6 e 16 da noite",
			"6 e 17 da noite",
			"6 e 18 da noite",
			"6 e 19 da noite",
			"6 e 20 da noite",
			"6 e 21 da noite",
			"6 e 22 da noite",
			"6 e 23 da noite",
			"6 e 24 da noite",
			"6 e 25 da noite",
			"6 e 26 da noite",
			"6 e 27 da noite",
			"6 e 28 da noite",
			"6 e 29 da noite",
			"6 e 30 da noite",
			"6 e 31 da noite",
			"6 e 32 da noite",
			"6 e 33 da noite",
			"6 e 34 da noite",
			"6 e 35 da noite",
			"6 e 36 da noite",
			"6 e 37 da noite",
			"6 e 38 da noite",
			"6 e 39 da noite",
			"6 e 40 da noite",
			"6 e 41 da noite",
			"6 e 42 da noite",
			"6 e 43 da noite",
			"6 e 44 da noite",
			"6 e 45 da noite",
			"6 e 46 da noite",
			"6 e 47 da noite",
			"6 e 48 da noite",
			"6 e 49 da noite",
			"6 e 50 da noite",
			"6 e 51 da noite",
			"6 e 52 da noite",
			"6 e 53 da noite",
			"6 e 54 da noite",
			"6 e 55 da noite",
			"6 e 56 da noite",
			"6 e 57 da noite",
			"6 e 58 da noite",
			"6 e 59 da noite",
			"7 da noite",
			"7 e 1 da noite",
			"7 e 2 da noite",
			"7 e 3 da noite",
			"7 e 4 da noite",
			"7 e 5 da noite",
			"7 e 6 da noite",
			"7 e 7 da noite",
			"7 e 8 da noite",
			"7 e 9 da noite",
			"7 e 10 da noite",
			"7 e 11 da noite",
			"7 e 12 da noite",
			"7 e 13 da noite",
			"7 e 14 da noite",
			"7 e 15 da noite",
			"7 e 16 da noite",
			"7 e 17 da noite",
			"7 e 18 da noite",
			"7 e 19 da noite",
			"7 e 20 da noite",
			"7 e 21 da noite",
			"7 e 22 da noite",
			"7 e 23 da noite",
			"7 e 24 da noite",
			"7 e 25 da noite",
			"7 e 26 da noite",
			"7 e 27 da noite",
			"7 e 28 da noite",
			"7 e 29 da noite",
			"7 e 30 da noite",
			"7 e 31 da noite",
			"7 e 32 da noite",
			"7 e 33 da noite",
			"7 e 34 da noite",
			"7 e 35 da noite",
			"7 e 36 da noite",
			"7 e 37 da noite",
			"7 e 38 da noite",
			"7 e 39 da noite",
			"7 e 40 da noite",
			"7 e 41 da noite",
			"7 e 42 da noite",
			"7 e 43 da noite",
			"7 e 44 da noite",
			"7 e 45 da noite",
			"7 e 46 da noite",
			"7 e 47 da noite",
			"7 e 48 da noite",
			"7 e 49 da noite",
			"7 e 50 da noite",
			"7 e 51 da noite",
			"7 e 52 da noite",
			"7 e 53 da noite",
			"7 e 54 da noite",
			"7 e 55 da noite",
			"7 e 56 da noite",
			"7 e 57 da noite",
			"7 e 58 da noite",
			"7 e 59 da noite",
			"8 da noite",
			"8 e 1 da noite",
			"8 e 2 da noite",
			"8 e 3 da noite",
			"8 e 4 da noite",
			"8 e 5 da noite",
			"8 e 6 da noite",
			"8 e 7 da noite",
			"8 e 8 da noite",
			"8 e 9 da noite",
			"8 e 10 da noite",
			"8 e 11 da noite",
			"8 e 12 da noite",
			"8 e 13 da noite",
			"8 e 14 da noite",
			"8 e 15 da noite",
			"8 e 16 da noite",
			"8 e 17 da noite",
			"8 e 18 da noite",
			"8 e 19 da noite",
			"8 e 20 da noite",
			"8 e 21 da noite",
			"8 e 22 da noite",
			"8 e 23 da noite",
			"8 e 24 da noite",
			"8 e 25 da noite",
			"8 e 26 da noite",
			"8 e 27 da noite",
			"8 e 28 da noite",
			"8 e 29 da noite",
			"8 e 30 da noite",
			"8 e 31 da noite",
			"8 e 32 da noite",
			"8 e 33 da noite",
			"8 e 34 da noite",
			"8 e 35 da noite",
			"8 e 36 da noite",
			"8 e 37 da noite",
			"8 e 38 da noite",
			"8 e 39 da noite",
			"8 e 40 da noite",
			"8 e 41 da noite",
			"8 e 42 da noite",
			"8 e 43 da noite",
			"8 e 44 da noite",
			"8 e 45 da noite",
			"8 e 46 da noite",
			"8 e 47 da noite",
			"8 e 48 da noite",
			"8 e 49 da noite",
			"8 e 50 da noite",
			"8 e 51 da noite",
			"8 e 52 da noite",
			"8 e 53 da noite",
			"8 e 54 da noite",
			"8 e 55 da noite",
			"8 e 56 da noite",
			"8 e 57 da noite",
			"8 e 58 da noite",
			"8 e 59 da noite",
			"9 da noite",
			"9 e 1 da noite",
			"9 e 2 da noite",
			"9 e 3 da noite",
			"9 e 4 da noite",
			"9 e 5 da noite",
			"9 e 6 da noite",
			"9 e 7 da noite",
			"9 e 8 da noite",
			"9 e 9 da noite",
			"9 e 10 da noite",
			"9 e 11 da noite",
			"9 e 12 da noite",
			"9 e 13 da noite",
			"9 e 14 da noite",
			"9 e 15 da noite",
			"9 e 16 da noite",
			"9 e 17 da noite",
			"9 e 18 da noite",
			"9 e 19 da noite",
			"9 e 20 da noite",
			"9 e 21 da noite",
			"9 e 22 da noite",
			"9 e 23 da noite",
			"9 e 24 da noite",
			"9 e 25 da noite",
			"9 e 26 da noite",
			"9 e 27 da noite",
			"9 e 28 da noite",
			"9 e 29 da noite",
			"9 e 30 da noite",
			"9 e 31 da noite",
			"9 e 32 da noite",
			"9 e 33 da noite",
			"9 e 34 da noite",
			"9 e 35 da noite",
			"9 e 36 da noite",
			"9 e 37 da noite",
			"9 e 38 da noite",
			"9 e 39 da noite",
			"9 e 40 da noite",
			"9 e 41 da noite",
			"9 e 42 da noite",
			"9 e 43 da noite",
			"9 e 44 da noite",
			"9 e 45 da noite",
			"9 e 46 da noite",
			"9 e 47 da noite",
			"9 e 48 da noite",
			"9 e 49 da noite",
			"9 e 50 da noite",
			"9 e 51 da noite",
			"9 e 52 da noite",
			"9 e 53 da noite",
			"9 e 54 da noite",
			"9 e 55 da noite",
			"9 e 56 da noite",
			"9 e 57 da noite",
			"9 e 58 da noite",
			"9 e 59 da noite",
			"10 da noite",
			"10 e 1 da noite",
			"10 e 2 da noite",
			"10 e 3 da noite",
			"10 e 4 da noite",
			"10 e 5 da noite",
			"10 e 6 da noite",
			"10 e 7 da noite",
			"10 e 8 da noite",
			"10 e 9 da noite",
			"10 e 10 da noite",
			"10 e 11 da noite",
			"10 e 12 da noite",
			"10 e 13 da noite",
			"10 e 14 da noite",
			"10 e 15 da noite",
			"10 e 16 da noite",
			"10 e 17 da noite",
			"10 e 18 da noite",
			"10 e 19 da noite",
			"10 e 20 da noite",
			"10 e 21 da noite",
			"10 e 22 da noite",
			"10 e 23 da noite",
			"10 e 24 da noite",
			"10 e 25 da noite",
			"10 e 26 da noite",
			"10 e 27 da noite",
			"10 e 28 da noite",
			"10 e 29 da noite",
			"10 e 30 da noite",
			"10 e 31 da noite",
			"10 e 32 da noite",
			"10 e 33 da noite",
			"10 e 34 da noite",
			"10 e 35 da noite",
			"10 e 36 da noite",
			"10 e 37 da noite",
			"10 e 38 da noite",
			"10 e 39 da noite",
			"10 e 40 da noite",
			"10 e 41 da noite",
			"10 e 42 da noite",
			"10 e 43 da noite",
			"10 e 44 da noite",
			"10 e 45 da noite",
			"10 e 46 da noite",
			"10 e 47 da noite",
			"10 e 48 da noite",
			"10 e 49 da noite",
			"10 e 50 da noite",
			"10 e 51 da noite",
			"10 e 52 da noite",
			"10 e 53 da noite",
			"10 e 54 da noite",
			"10 e 55 da noite",
			"10 e 56 da noite",
			"10 e 57 da noite",
			"10 e 58 da noite",
			"10 e 59 da noite",
			"11 da noite",
			"11 e 1 da noite",
			"11 e 2 da noite",
			"11 e 3 da noite",
			"11 e 4 da noite",
			"11 e 5 da noite",
			"11 e 6 da noite",
			"11 e 7 da noite",
			"11 e 8 da noite",
			"11 e 9 da noite",
			"11 e 10 da noite",
			"11 e 11 da noite",
			"11 e 12 da noite",
			"11 e 13 da noite",
			"11 e 14 da noite",
			"11 e 15 da noite",
			"11 e 16 da noite",
			"11 e 17 da noite",
			"11 e 18 da noite",
			"11 e 19 da noite",
			"11 e 20 da noite",
			"11 e 21 da noite",
			"11 e 22 da noite",
			"11 e 23 da noite",
			"11 e 24 da noite",
			"11 e 25 da noite",
			"11 e 26 da noite",
			"11 e 27 da noite",
			"11 e 28 da noite",
			"11 e 29 da noite",
			"11 e 30 da noite",
			"11 e 31 da noite",
			"11 e 32 da noite",
			"11 e 33 da noite",
			"11 e 34 da noite",
			"11 e 35 da noite",
			"11 e 36 da noite",
			"11 e 37 da noite",
			"11 e 38 da noite",
			"11 e 39 da noite",
			"11 e 40 da noite",
			"11 e 41 da noite",
			"11 e 42 da noite",
			"11 e 43 da noite",
			"11 e 44 da noite",
			"11 e 45 da noite",
			"11 e 46 da noite",
			"11 e 47 da noite",
			"11 e 48 da noite",
			"11 e 49 da noite",
			"11 e 50 da noite",
			"11 e 51 da noite",
			"11 e 52 da noite",
			"11 e 53 da noite",
			"11 e 54 da noite",
			"11 e 55 da noite",
			"11 e 56 da noite",
			"11 e 57 da noite",
			"11 e 58 da noite",
			"11 e 59 da noite",
			"12 da tarde",
			"12 e 1 da tarde",
			"12 e 2 da tarde",
			"12 e 3 da tarde",
			"12 e 4 da tarde",
			"12 e 5 da tarde",
			"12 e 6 da manhã",
			"12 e 7 da tarde",
			"12 e 8 da tarde",
			"12 e 9 da tarde",
			"12 e 10 da tarde",
			"12 e 11 da tarde",
			"12 e 12 da tarde",
			"12 e 13 da tarde",
			"12 e 14 da tarde",
			"12 e 15 da tarde",
			"12 e 16 da tarde",
			"12 e 17 da tarde",
			"12 e 18 da tarde",
			"12 e 19 da tarde",
			"12 e 20 da tarde",
			"12 e 21 da tarde",
			"12 e 22 da tarde",
			"12 e 23 da tarde",
			"12 e 24 da tarde",
			"12 e 25 da tarde",
			"12 e 26 da tarde",
			"12 e 27 da tarde",
			"12 e 28 da tarde",
			"12 e 29 da tarde",
			"12 e 30 da tarde",
			"12 e 31 da tarde",
			"12 e 32 da tarde",
			"12 e 33 da tarde",
			"12 e 34 da tarde",
			"12 e 35 da tarde",
			"12 e 36 da tarde",
			"12 e 37 da tarde",
			"12 e 38 da tarde",
			"12 e 39 da tarde",
			"12 e 40 da tarde",
			"12 e 41 da tarde",
			"12 e 42 da tarde",
			"12 e 43 da tarde",
			"12 e 44 da tarde",
			"12 e 45 da tarde",
			"12 e 46 da tarde",
			"12 e 47 da tarde",
			"12 e 48 da tarde",
			"12 e 49 da tarde",
			"12 e 50 da tarde",
			"12 e 51 da tarde",
			"12 e 52 da tarde",
			"12 e 53 da tarde",
			"12 e 54 da tarde",
			"12 e 55 da tarde",
			"12 e 56 da tarde",
			"12 e 57 da tarde",
			"12 e 58 da tarde",
			"12 e 59 da tarde"
		};

		// Token: 0x0400002F RID: 47
		public string[] AlarmValue = new string[]
		{
			"1:00 AM",
			"1:01 AM",
			"1:02 AM",
			"1:03 AM",
			"1:04 AM",
			"1:05 AM",
			"1:06 AM",
			"1:07 AM",
			"1:08 AM",
			"1:09 AM",
			"1:10 AM",
			"1:11 AM",
			"1:12 AM",
			"1:13 AM",
			"1:14 AM",
			"1:15 AM",
			"1:16 AM",
			"1:17 AM",
			"1:18 AM",
			"1:19 AM",
			"1:20 AM",
			"1:21 AM",
			"1:22 AM",
			"1:23 AM",
			"1:24 AM",
			"1:25 AM",
			"1:26 AM",
			"1:27 AM",
			"1:28 AM",
			"1:29 AM",
			"1:30 AM",
			"1:31 AM",
			"1:32 AM",
			"1:33 AM",
			"1:34 AM",
			"1:35 AM",
			"1:36 AM",
			"1:37 AM",
			"1:38 AM",
			"1:39 AM",
			"1:40 AM",
			"1:41 AM",
			"1:42 AM",
			"1:43 AM",
			"1:44 AM",
			"1:45 AM",
			"1:46 AM",
			"1:47 AM",
			"1:48 AM",
			"1:49 AM",
			"1:50 AM",
			"1:51 AM",
			"1:52 AM",
			"1:53 AM",
			"1:54 AM",
			"1:55 AM",
			"1:56 AM",
			"1:57 AM",
			"1:58 AM",
			"1:59 AM",
			"2:00 AM",
			"2:01 AM",
			"2:02 AM",
			"2:03 AM",
			"2:04 AM",
			"2:05 AM",
			"2:06 AM",
			"2:07 AM",
			"2:08 AM",
			"2:09 AM",
			"2:10 AM",
			"2:11 AM",
			"2:12 AM",
			"2:13 AM",
			"2:14 AM",
			"2:15 AM",
			"2:16 AM",
			"2:17 AM",
			"2:18 AM",
			"2:19 AM",
			"2:20 AM",
			"2:21 AM",
			"2:22 AM",
			"2:23 AM",
			"2:24 AM",
			"2:25 AM",
			"2:26 AM",
			"2:27 AM",
			"2:28 AM",
			"2:29 AM",
			"2:30 AM",
			"2:31 AM",
			"2:32 AM",
			"2:33 AM",
			"2:34 AM",
			"2:35 AM",
			"2:36 AM",
			"2:37 AM",
			"2:38 AM",
			"2:39 AM",
			"2:40 AM",
			"2:41 AM",
			"2:42 AM",
			"2:43 AM",
			"2:44 AM",
			"2:45 AM",
			"2:46 AM",
			"2:47 AM",
			"2:48 AM",
			"2:49 AM",
			"2:50 AM",
			"2:51 AM",
			"2:52 AM",
			"2:53 AM",
			"2:54 AM",
			"2:55 AM",
			"2:56 AM",
			"2:57 AM",
			"2:58 AM",
			"2:59 AM",
			"3:00 AM",
			"3:01 AM",
			"3:02 AM",
			"3:03 AM",
			"3:04 AM",
			"3:05 AM",
			"3:06 AM",
			"3:07 AM",
			"3:08 AM",
			"3:09 AM",
			"3:10 AM",
			"3:11 AM",
			"3:12 AM",
			"3:13 AM",
			"3:14 AM",
			"3:15 AM",
			"3:16 AM",
			"3:17 AM",
			"3:18 AM",
			"3:19 AM",
			"3:20 AM",
			"3:21 AM",
			"3:22 AM",
			"3:23 AM",
			"3:24 AM",
			"3:25 AM",
			"3:26 AM",
			"3:27 AM",
			"3:28 AM",
			"3:29 AM",
			"3:30 AM",
			"3:31 AM",
			"3:32 AM",
			"3:33 AM",
			"3:34 AM",
			"3:35 AM",
			"3:36 AM",
			"3:37 AM",
			"3:38 AM",
			"3:39 AM",
			"3:40 AM",
			"3:41 AM",
			"3:42 AM",
			"3:43 AM",
			"3:44 AM",
			"3:45 AM",
			"3:46 AM",
			"3:47 AM",
			"3:48 AM",
			"3:49 AM",
			"3:50 AM",
			"3:51 AM",
			"3:52 AM",
			"3:53 AM",
			"3:54 AM",
			"3:55 AM",
			"3:56 AM",
			"3:57 AM",
			"3:58 AM",
			"3:59 AM",
			"4:00 AM",
			"4:01 AM",
			"4:02 AM",
			"4:03 AM",
			"4:04 AM",
			"4:05 AM",
			"4:06 AM",
			"4:07 AM",
			"4:08 AM",
			"4:09 AM",
			"4:10 AM",
			"4:11 AM",
			"4:12 AM",
			"4:13 AM",
			"4:14 AM",
			"4:15 AM",
			"4:16 AM",
			"4:17 AM",
			"4:18 AM",
			"4:19 AM",
			"4:20 AM",
			"4:21 AM",
			"4:22 AM",
			"4:23 AM",
			"4:24 AM",
			"4:25 AM",
			"4:26 AM",
			"4:27 AM",
			"4:28 AM",
			"4:29 AM",
			"4:30 AM",
			"4:31 AM",
			"4:32 AM",
			"4:33 AM",
			"4:34 AM",
			"4:35 AM",
			"4:36 AM",
			"4:37 AM",
			"4:38 AM",
			"4:39 AM",
			"4:40 AM",
			"4:41 AM",
			"4:42 AM",
			"4:43 AM",
			"4:44 AM",
			"4:45 AM",
			"4:46 AM",
			"4:47 AM",
			"4:48 AM",
			"4:49 AM",
			"4:50 AM",
			"4:51 AM",
			"4:52 AM",
			"4:53 AM",
			"4:54 AM",
			"4:55 AM",
			"4:56 AM",
			"4:57 AM",
			"4:58 AM",
			"4:59 AM",
			"5:00 AM",
			"5:01 AM",
			"5:02 AM",
			"5:03 AM",
			"5:04 AM",
			"5:05 AM",
			"5:06 AM",
			"5:07 AM",
			"5:08 AM",
			"5:09 AM",
			"5:10 AM",
			"5:11 AM",
			"5:12 AM",
			"5:13 AM",
			"5:14 AM",
			"5:15 AM",
			"5:16 AM",
			"5:17 AM",
			"5:18 AM",
			"5:19 AM",
			"5:20 AM",
			"5:21 AM",
			"5:22 AM",
			"5:23 AM",
			"5:24 AM",
			"5:25 AM",
			"5:26 AM",
			"5:27 AM",
			"5:28 AM",
			"5:29 AM",
			"5:30 AM",
			"5:31 AM",
			"5:32 AM",
			"5:33 AM",
			"5:34 AM",
			"5:35 AM",
			"5:36 AM",
			"5:37 AM",
			"5:38 AM",
			"5:39 AM",
			"5:40 AM",
			"5:41 AM",
			"5:42 AM",
			"5:43 AM",
			"5:44 AM",
			"5:45 AM",
			"5:46 AM",
			"5:47 AM",
			"5:48 AM",
			"5:49 AM",
			"5:50 AM",
			"5:51 AM",
			"5:52 AM",
			"5:53 AM",
			"5:54 AM",
			"5:55 AM",
			"5:56 AM",
			"5:57 AM",
			"5:58 AM",
			"5:59 AM",
			"6:00 AM",
			"6:01 AM",
			"6:02 AM",
			"6:03 AM",
			"6:04 AM",
			"6:05 AM",
			"6:06 AM",
			"6:07 AM",
			"6:08 AM",
			"6:09 AM",
			"6:10 AM",
			"6:11 AM",
			"6:12 AM",
			"6:13 AM",
			"6:14 AM",
			"6:15 AM",
			"6:16 AM",
			"6:17 AM",
			"6:18 AM",
			"6:19 AM",
			"6:20 AM",
			"6:21 AM",
			"6:22 AM",
			"6:23 AM",
			"6:24 AM",
			"6:25 AM",
			"6:26 AM",
			"6:27 AM",
			"6:28 AM",
			"6:29 AM",
			"6:30 AM",
			"6:31 AM",
			"6:32 AM",
			"6:33 AM",
			"6:34 AM",
			"6:35 AM",
			"6:36 AM",
			"6:37 AM",
			"6:38 AM",
			"6:39 AM",
			"6:40 AM",
			"6:41 AM",
			"6:42 AM",
			"6:43 AM",
			"6:44 AM",
			"6:45 AM",
			"6:46 AM",
			"6:47 AM",
			"6:48 AM",
			"6:49 AM",
			"6:50 AM",
			"6:51 AM",
			"6:52 AM",
			"6:53 AM",
			"6:54 AM",
			"6:55 AM",
			"6:56 AM",
			"6:57 AM",
			"6:58 AM",
			"6:59 AM",
			"7:00 AM",
			"7:01 AM",
			"7:02 AM",
			"7:03 AM",
			"7:04 AM",
			"7:05 AM",
			"7:06 AM",
			"7:07 AM",
			"7:08 AM",
			"7:09 AM",
			"7:10 AM",
			"7:11 AM",
			"7:12 AM",
			"7:13 AM",
			"7:14 AM",
			"7:15 AM",
			"7:16 AM",
			"7:17 AM",
			"7:18 AM",
			"7:19 AM",
			"7:20 AM",
			"7:21 AM",
			"7:22 AM",
			"7:23 AM",
			"7:24 AM",
			"7:25 AM",
			"7:26 AM",
			"7:27 AM",
			"7:28 AM",
			"7:29 AM",
			"7:30 AM",
			"7:31 AM",
			"7:32 AM",
			"7:33 AM",
			"7:34 AM",
			"7:35 AM",
			"7:36 AM",
			"7:37 AM",
			"7:38 AM",
			"7:39 AM",
			"7:40 AM",
			"7:41 AM",
			"7:42 AM",
			"7:43 AM",
			"7:44 AM",
			"7:45 AM",
			"7:46 AM",
			"7:47 AM",
			"7:48 AM",
			"7:49 AM",
			"7:50 AM",
			"7:51 AM",
			"7:52 AM",
			"7:53 AM",
			"7:54 AM",
			"7:55 AM",
			"7:56 AM",
			"7:57 AM",
			"7:58 AM",
			"7:59 AM",
			"8:00 AM",
			"8:01 AM",
			"8:02 AM",
			"8:03 AM",
			"8:04 AM",
			"8:05 AM",
			"8:06 AM",
			"8:07 AM",
			"8:08 AM",
			"8:09 AM",
			"8:10 AM",
			"8:11 AM",
			"8:12 AM",
			"8:13 AM",
			"8:14 AM",
			"8:15 AM",
			"8:16 AM",
			"8:17 AM",
			"8:18 AM",
			"8:19 AM",
			"8:20 AM",
			"8:21 AM",
			"8:22 AM",
			"8:23 AM",
			"8:24 AM",
			"8:25 AM",
			"8:26 AM",
			"8:27 AM",
			"8:28 AM",
			"8:29 AM",
			"8:30 AM",
			"8:31 AM",
			"8:32 AM",
			"8:33 AM",
			"8:34 AM",
			"8:35 AM",
			"8:36 AM",
			"8:37 AM",
			"8:38 AM",
			"8:39 AM",
			"8:40 AM",
			"8:41 AM",
			"8:42 AM",
			"8:43 AM",
			"8:44 AM",
			"8:45 AM",
			"8:46 AM",
			"8:47 AM",
			"8:48 AM",
			"8:49 AM",
			"8:50 AM",
			"8:51 AM",
			"8:52 AM",
			"8:53 AM",
			"8:54 AM",
			"8:55 AM",
			"8:56 AM",
			"8:57 AM",
			"8:58 AM",
			"8:59 AM",
			"9:00 AM",
			"9:01 AM",
			"9:02 AM",
			"9:03 AM",
			"9:04 AM",
			"9:05 AM",
			"9:06 AM",
			"9:07 AM",
			"9:08 AM",
			"9:09 AM",
			"9:10 AM",
			"9:11 AM",
			"9:12 AM",
			"9:13 AM",
			"9:14 AM",
			"9:15 AM",
			"9:16 AM",
			"9:17 AM",
			"9:18 AM",
			"9:19 AM",
			"9:20 AM",
			"9:21 AM",
			"9:22 AM",
			"9:23 AM",
			"9:24 AM",
			"9:25 AM",
			"9:26 AM",
			"9:27 AM",
			"9:28 AM",
			"9:29 AM",
			"9:30 AM",
			"9:31 AM",
			"9:32 AM",
			"9:33 AM",
			"9:34 AM",
			"9:35 AM",
			"9:36 AM",
			"9:37 AM",
			"9:38 AM",
			"9:39 AM",
			"9:40 AM",
			"9:41 AM",
			"9:42 AM",
			"9:43 AM",
			"9:44 AM",
			"9:45 AM",
			"9:46 AM",
			"9:47 AM",
			"9:48 AM",
			"9:49 AM",
			"9:50 AM",
			"9:51 AM",
			"9:52 AM",
			"9:53 AM",
			"9:54 AM",
			"9:55 AM",
			"9:56 AM",
			"9:57 AM",
			"9:58 AM",
			"9:59 AM",
			"10:00 AM",
			"10:01 AM",
			"10:02 AM",
			"10:03 AM",
			"10:04 AM",
			"10:05 AM",
			"10:06 AM",
			"10:07 AM",
			"10:08 AM",
			"10:09 AM",
			"10:10 AM",
			"10:11 AM",
			"10:12 AM",
			"10:13 AM",
			"10:14 AM",
			"10:15 AM",
			"10:16 AM",
			"10:17 AM",
			"10:18 AM",
			"10:19 AM",
			"10:20 AM",
			"10:21 AM",
			"10:22 AM",
			"10:23 AM",
			"10:24 AM",
			"10:25 AM",
			"10:26 AM",
			"10:27 AM",
			"10:28 AM",
			"10:29 AM",
			"10:30 AM",
			"10:31 AM",
			"10:32 AM",
			"10:33 AM",
			"10:34 AM",
			"10:35 AM",
			"10:36 AM",
			"10:37 AM",
			"10:38 AM",
			"10:39 AM",
			"10:40 AM",
			"10:41 AM",
			"10:42 AM",
			"10:43 AM",
			"10:44 AM",
			"10:45 AM",
			"10:46 AM",
			"10:47 AM",
			"10:48 AM",
			"10:49 AM",
			"10:50 AM",
			"10:51 AM",
			"10:52 AM",
			"10:53 AM",
			"10:54 AM",
			"10:55 AM",
			"10:56 AM",
			"10:57 AM",
			"10:58 AM",
			"10:59 AM",
			"11:00 AM",
			"11:01 AM",
			"11:02 AM",
			"11:03 AM",
			"11:04 AM",
			"11:05 AM",
			"11:06 AM",
			"11:07 AM",
			"11:08 AM",
			"11:09 AM",
			"11:10 AM",
			"11:11 AM",
			"11:12 AM",
			"11:13 AM",
			"11:14 AM",
			"11:15 AM",
			"11:16 AM",
			"11:17 AM",
			"11:18 AM",
			"11:19 AM",
			"11:20 AM",
			"11:21 AM",
			"11:22 AM",
			"11:23 AM",
			"11:24 AM",
			"11:25 AM",
			"11:26 AM",
			"11:27 AM",
			"11:28 AM",
			"11:29 AM",
			"11:30 AM",
			"11:31 AM",
			"11:32 AM",
			"11:33 AM",
			"11:34 AM",
			"11:35 AM",
			"11:36 AM",
			"11:37 AM",
			"11:38 AM",
			"11:39 AM",
			"11:40 AM",
			"11:41 AM",
			"11:42 AM",
			"11:43 AM",
			"11:44 AM",
			"11:45 AM",
			"11:46 AM",
			"11:47 AM",
			"11:48 AM",
			"11:49 AM",
			"11:50 AM",
			"11:51 AM",
			"11:52 AM",
			"11:53 AM",
			"11:54 AM",
			"11:55 AM",
			"11:56 AM",
			"11:57 AM",
			"11:58 AM",
			"11:59 AM",
			"12:00 AM",
			"12:01 AM",
			"12:02 AM",
			"12:03 AM",
			"12:04 AM",
			"12:05 AM",
			"12:06 AM",
			"12:07 AM",
			"12:08 AM",
			"12:09 AM",
			"12:10 AM",
			"12:11 AM",
			"12:12 AM",
			"12:13 AM",
			"12:14 AM",
			"12:15 AM",
			"12:16 AM",
			"12:17 AM",
			"12:18 AM",
			"12:19 AM",
			"12:20 AM",
			"12:21 AM",
			"12:22 AM",
			"12:23 AM",
			"12:24 AM",
			"12:25 AM",
			"12:26 AM",
			"12:27 AM",
			"12:28 AM",
			"12:29 AM",
			"12:30 AM",
			"12:31 AM",
			"12:32 AM",
			"12:33 AM",
			"12:34 AM",
			"12:35 AM",
			"12:36 AM",
			"12:37 AM",
			"12:38 AM",
			"12:39 AM",
			"12:40 AM",
			"12:41 AM",
			"12:42 AM",
			"12:43 AM",
			"12:44 AM",
			"12:45 AM",
			"12:46 AM",
			"12:47 AM",
			"12:48 AM",
			"12:49 AM",
			"12:50 AM",
			"12:51 AM",
			"12:52 AM",
			"12:53 AM",
			"12:54 AM",
			"12:55 AM",
			"12:56 AM",
			"12:57 AM",
			"12:58 AM",
			"12:59 AM",
			"1:00 PM",
			"1:01 PM",
			"1:02 PM",
			"1:03 PM",
			"1:04 PM",
			"1:05 PM",
			"1:06 PM",
			"1:07 PM",
			"1:08 PM",
			"1:09 PM",
			"1:10 PM",
			"1:11 PM",
			"1:12 PM",
			"1:13 PM",
			"1:14 PM",
			"1:15 PM",
			"1:16 PM",
			"1:17 PM",
			"1:18 PM",
			"1:19 PM",
			"1:20 PM",
			"1:21 PM",
			"1:22 PM",
			"1:23 PM",
			"1:24 PM",
			"1:25 PM",
			"1:26 PM",
			"1:27 PM",
			"1:28 PM",
			"1:29 PM",
			"1:30 PM",
			"1:31 PM",
			"1:32 PM",
			"1:33 PM",
			"1:34 PM",
			"1:35 PM",
			"1:36 PM",
			"1:37 PM",
			"1:38 PM",
			"1:39 PM",
			"1:40 PM",
			"1:41 PM",
			"1:42 PM",
			"1:43 PM",
			"1:44 PM",
			"1:45 PM",
			"1:46 PM",
			"1:47 PM",
			"1:48 PM",
			"1:49 PM",
			"1:50 PM",
			"1:51 PM",
			"1:52 PM",
			"1:53 PM",
			"1:54 PM",
			"1:55 PM",
			"1:56 PM",
			"1:57 PM",
			"1:58 PM",
			"1:59 PM",
			"2:00 PM",
			"2:01 PM",
			"2:02 PM",
			"2:03 PM",
			"2:04 PM",
			"2:05 PM",
			"2:06 PM",
			"2:07 PM",
			"2:08 PM",
			"2:09 PM",
			"2:10 PM",
			"2:11 PM",
			"2:12 PM",
			"2:13 PM",
			"2:14 PM",
			"2:15 PM",
			"2:16 PM",
			"2:17 PM",
			"2:18 PM",
			"2:19 PM",
			"2:20 PM",
			"2:21 PM",
			"2:22 PM",
			"2:23 PM",
			"2:24 PM",
			"2:25 PM",
			"2:26 PM",
			"2:27 PM",
			"2:28 PM",
			"2:29 PM",
			"2:30 PM",
			"2:31 PM",
			"2:32 PM",
			"2:33 PM",
			"2:34 PM",
			"2:35 PM",
			"2:36 PM",
			"2:37 PM",
			"2:38 PM",
			"2:39 PM",
			"2:40 PM",
			"2:41 PM",
			"2:42 PM",
			"2:43 PM",
			"2:44 PM",
			"2:45 PM",
			"2:46 PM",
			"2:47 PM",
			"2:48 PM",
			"2:49 PM",
			"2:50 PM",
			"2:51 PM",
			"2:52 PM",
			"2:53 PM",
			"2:54 PM",
			"2:55 PM",
			"2:56 PM",
			"2:57 PM",
			"2:58 PM",
			"2:59 PM",
			"3:00 PM",
			"3:01 PM",
			"3:02 PM",
			"3:03 PM",
			"3:04 PM",
			"3:05 PM",
			"3:06 PM",
			"3:07 PM",
			"3:08 PM",
			"3:09 PM",
			"3:10 PM",
			"3:11 PM",
			"3:12 PM",
			"3:13 PM",
			"3:14 PM",
			"3:15 PM",
			"3:16 PM",
			"3:17 PM",
			"3:18 PM",
			"3:19 PM",
			"3:20 PM",
			"3:21 PM",
			"3:22 PM",
			"3:23 PM",
			"3:24 PM",
			"3:25 PM",
			"3:26 PM",
			"3:27 PM",
			"3:28 PM",
			"3:29 PM",
			"3:30 PM",
			"3:31 PM",
			"3:32 PM",
			"3:33 PM",
			"3:34 PM",
			"3:35 PM",
			"3:36 PM",
			"3:37 PM",
			"3:38 PM",
			"3:39 PM",
			"3:40 PM",
			"3:41 PM",
			"3:42 PM",
			"3:43 PM",
			"3:44 PM",
			"3:45 PM",
			"3:46 PM",
			"3:47 PM",
			"3:48 PM",
			"3:49 PM",
			"3:50 PM",
			"3:51 PM",
			"3:52 PM",
			"3:53 PM",
			"3:54 PM",
			"3:55 PM",
			"3:56 PM",
			"3:57 PM",
			"3:58 PM",
			"3:59 PM",
			"4:00 PM",
			"4:01 PM",
			"4:02 PM",
			"4:03 PM",
			"4:04 PM",
			"4:05 PM",
			"4:06 PM",
			"4:07 PM",
			"4:08 PM",
			"4:09 PM",
			"4:10 PM",
			"4:11 PM",
			"4:12 PM",
			"4:13 PM",
			"4:14 PM",
			"4:15 PM",
			"4:16 PM",
			"4:17 PM",
			"4:18 PM",
			"4:19 PM",
			"4:20 PM",
			"4:21 PM",
			"4:22 PM",
			"4:23 PM",
			"4:24 PM",
			"4:25 PM",
			"4:26 PM",
			"4:27 PM",
			"4:28 PM",
			"4:29 PM",
			"4:30 PM",
			"4:31 PM",
			"4:32 PM",
			"4:33 PM",
			"4:34 PM",
			"4:35 PM",
			"4:36 PM",
			"4:37 PM",
			"4:38 PM",
			"4:39 PM",
			"4:40 PM",
			"4:41 PM",
			"4:42 PM",
			"4:43 PM",
			"4:44 PM",
			"4:45 PM",
			"4:46 PM",
			"4:47 PM",
			"4:48 PM",
			"4:49 PM",
			"4:50 PM",
			"4:51 PM",
			"4:52 PM",
			"4:53 PM",
			"4:54 PM",
			"4:55 PM",
			"4:56 PM",
			"4:57 PM",
			"4:58 PM",
			"4:59 PM",
			"5:00 PM",
			"5:01 PM",
			"5:02 PM",
			"5:03 PM",
			"5:04 PM",
			"5:05 PM",
			"5:06 PM",
			"5:07 PM",
			"5:08 PM",
			"5:09 PM",
			"5:10 PM",
			"5:11 PM",
			"5:12 PM",
			"5:13 PM",
			"5:14 PM",
			"5:15 PM",
			"5:16 PM",
			"5:17 PM",
			"5:18 PM",
			"5:19 PM",
			"5:20 PM",
			"5:21 PM",
			"5:22 PM",
			"5:23 PM",
			"5:24 PM",
			"5:25 PM",
			"5:26 PM",
			"5:27 PM",
			"5:28 PM",
			"5:29 PM",
			"5:30 PM",
			"5:31 PM",
			"5:32 PM",
			"5:33 PM",
			"5:34 PM",
			"5:35 PM",
			"5:36 PM",
			"5:37 PM",
			"5:38 PM",
			"5:39 PM",
			"5:40 PM",
			"5:41 PM",
			"5:42 PM",
			"5:43 PM",
			"5:44 PM",
			"5:45 PM",
			"5:46 PM",
			"5:47 PM",
			"5:48 PM",
			"5:49 PM",
			"5:50 PM",
			"5:51 PM",
			"5:52 PM",
			"5:53 PM",
			"5:54 PM",
			"5:55 PM",
			"5:56 PM",
			"5:57 PM",
			"5:58 PM",
			"5:59 PM",
			"6:00 PM",
			"6:01 PM",
			"6:02 PM",
			"6:03 PM",
			"6:04 PM",
			"6:05 PM",
			"6:06 PM",
			"6:07 PM",
			"6:08 PM",
			"6:09 PM",
			"6:10 PM",
			"6:11 PM",
			"6:12 PM",
			"6:13 PM",
			"6:14 PM",
			"6:15 PM",
			"6:16 PM",
			"6:17 PM",
			"6:18 PM",
			"6:19 PM",
			"6:20 PM",
			"6:21 PM",
			"6:22 PM",
			"6:23 PM",
			"6:24 PM",
			"6:25 PM",
			"6:26 PM",
			"6:27 PM",
			"6:28 PM",
			"6:29 PM",
			"6:30 PM",
			"6:31 PM",
			"6:32 PM",
			"6:33 PM",
			"6:34 PM",
			"6:35 PM",
			"6:36 PM",
			"6:37 PM",
			"6:38 PM",
			"6:39 PM",
			"6:40 PM",
			"6:41 PM",
			"6:42 PM",
			"6:43 PM",
			"6:44 PM",
			"6:45 PM",
			"6:46 PM",
			"6:47 PM",
			"6:48 PM",
			"6:49 PM",
			"6:50 PM",
			"6:51 PM",
			"6:52 PM",
			"6:53 PM",
			"6:54 PM",
			"6:55 PM",
			"6:56 PM",
			"6:57 PM",
			"6:58 PM",
			"6:59 PM",
			"7:00 PM",
			"7:01 PM",
			"7:02 PM",
			"7:03 PM",
			"7:04 PM",
			"7:05 PM",
			"7:06 PM",
			"7:07 PM",
			"7:08 PM",
			"7:09 PM",
			"7:10 PM",
			"7:11 PM",
			"7:12 PM",
			"7:13 PM",
			"7:14 PM",
			"7:15 PM",
			"7:16 PM",
			"7:17 PM",
			"7:18 PM",
			"7:19 PM",
			"7:20 PM",
			"7:21 PM",
			"7:22 PM",
			"7:23 PM",
			"7:24 PM",
			"7:25 PM",
			"7:26 PM",
			"7:27 PM",
			"7:28 PM",
			"7:29 PM",
			"7:30 PM",
			"7:31 PM",
			"7:32 PM",
			"7:33 PM",
			"7:34 PM",
			"7:35 PM",
			"7:36 PM",
			"7:37 PM",
			"7:38 PM",
			"7:39 PM",
			"7:40 PM",
			"7:41 PM",
			"7:42 PM",
			"7:43 PM",
			"7:44 PM",
			"7:45 PM",
			"7:46 PM",
			"7:47 PM",
			"7:48 PM",
			"7:49 PM",
			"7:50 PM",
			"7:51 PM",
			"7:52 PM",
			"7:53 PM",
			"7:54 PM",
			"7:55 PM",
			"7:56 PM",
			"7:57 PM",
			"7:58 PM",
			"7:59 PM",
			"8:00 PM",
			"8:01 PM",
			"8:02 PM",
			"8:03 PM",
			"8:04 PM",
			"8:05 PM",
			"8:06 PM",
			"8:07 PM",
			"8:08 PM",
			"8:09 PM",
			"8:10 PM",
			"8:11 PM",
			"8:12 PM",
			"8:13 PM",
			"8:14 PM",
			"8:15 PM",
			"8:16 PM",
			"8:17 PM",
			"8:18 PM",
			"8:19 PM",
			"8:20 PM",
			"8:21 PM",
			"8:22 PM",
			"8:23 PM",
			"8:24 PM",
			"8:25 PM",
			"8:26 PM",
			"8:27 PM",
			"8:28 PM",
			"8:29 PM",
			"8:30 PM",
			"8:31 PM",
			"8:32 PM",
			"8:33 PM",
			"8:34 PM",
			"8:35 PM",
			"8:36 PM",
			"8:37 PM",
			"8:38 PM",
			"8:39 PM",
			"8:40 PM",
			"8:41 PM",
			"8:42 PM",
			"8:43 PM",
			"8:44 PM",
			"8:45 PM",
			"8:46 PM",
			"8:47 PM",
			"8:48 PM",
			"8:49 PM",
			"8:50 PM",
			"8:51 PM",
			"8:52 PM",
			"8:53 PM",
			"8:54 PM",
			"8:55 PM",
			"8:56 PM",
			"8:57 PM",
			"8:58 PM",
			"8:59 PM",
			"9:00 PM",
			"9:01 PM",
			"9:02 PM",
			"9:03 PM",
			"9:04 PM",
			"9:05 PM",
			"9:06 PM",
			"9:07 PM",
			"9:08 PM",
			"9:09 PM",
			"9:10 PM",
			"9:11 PM",
			"9:12 PM",
			"9:13 PM",
			"9:14 PM",
			"9:15 PM",
			"9:16 PM",
			"9:17 PM",
			"9:18 PM",
			"9:19 PM",
			"9:20 PM",
			"9:21 PM",
			"9:22 PM",
			"9:23 PM",
			"9:24 PM",
			"9:25 PM",
			"9:26 PM",
			"9:27 PM",
			"9:28 PM",
			"9:29 PM",
			"9:30 PM",
			"9:31 PM",
			"9:32 PM",
			"9:33 PM",
			"9:34 PM",
			"9:35 PM",
			"9:36 PM",
			"9:37 PM",
			"9:38 PM",
			"9:39 PM",
			"9:40 PM",
			"9:41 PM",
			"9:42 PM",
			"9:43 PM",
			"9:44 PM",
			"9:45 PM",
			"9:46 PM",
			"9:47 PM",
			"9:48 PM",
			"9:49 PM",
			"9:50 PM",
			"9:51 PM",
			"9:52 PM",
			"9:53 PM",
			"9:54 PM",
			"9:55 PM",
			"9:56 PM",
			"9:57 PM",
			"9:58 PM",
			"9:59 PM",
			"10:00 PM",
			"10:01 PM",
			"10:02 PM",
			"10:03 PM",
			"10:04 PM",
			"10:05 PM",
			"10:06 PM",
			"10:07 PM",
			"10:08 PM",
			"10:09 PM",
			"10:10 PM",
			"10:11 PM",
			"10:12 PM",
			"10:13 PM",
			"10:14 PM",
			"10:15 PM",
			"10:16 PM",
			"10:17 PM",
			"10:18 PM",
			"10:19 PM",
			"10:20 PM",
			"10:21 PM",
			"10:22 PM",
			"10:23 PM",
			"10:24 PM",
			"10:25 PM",
			"10:26 PM",
			"10:27 PM",
			"10:28 PM",
			"10:29 PM",
			"10:30 PM",
			"10:31 PM",
			"10:32 PM",
			"10:33 PM",
			"10:34 PM",
			"10:35 PM",
			"10:36 PM",
			"10:37 PM",
			"10:38 PM",
			"10:39 PM",
			"10:40 PM",
			"10:41 PM",
			"10:42 PM",
			"10:43 PM",
			"10:44 PM",
			"10:45 PM",
			"10:46 PM",
			"10:47 PM",
			"10:48 PM",
			"10:49 PM",
			"10:50 PM",
			"10:51 PM",
			"10:52 PM",
			"10:53 PM",
			"10:54 PM",
			"10:55 PM",
			"10:56 PM",
			"10:57 PM",
			"10:58 PM",
			"10:59 PM",
			"11:00 PM",
			"11:01 PM",
			"11:02 PM",
			"11:03 PM",
			"11:04 PM",
			"11:05 PM",
			"11:06 PM",
			"11:07 PM",
			"11:08 PM",
			"11:09 PM",
			"11:10 PM",
			"11:11 PM",
			"11:12 PM",
			"11:13 PM",
			"11:14 PM",
			"11:15 PM",
			"11:16 PM",
			"11:17 PM",
			"11:18 PM",
			"11:19 PM",
			"11:20 PM",
			"11:21 PM",
			"11:22 PM",
			"11:23 PM",
			"11:24 PM",
			"11:25 PM",
			"11:26 PM",
			"11:27 PM",
			"11:28 PM",
			"11:29 PM",
			"11:30 PM",
			"11:31 PM",
			"11:32 PM",
			"11:33 PM",
			"11:34 PM",
			"11:35 PM",
			"11:36 PM",
			"11:37 PM",
			"11:38 PM",
			"11:39 PM",
			"11:40 PM",
			"11:41 PM",
			"11:42 PM",
			"11:43 PM",
			"11:44 PM",
			"11:45 PM",
			"11:46 PM",
			"11:47 PM",
			"11:48 PM",
			"11:49 PM",
			"11:50 PM",
			"11:51 PM",
			"11:52 PM",
			"11:53 PM",
			"11:54 PM",
			"11:55 PM",
			"11:56 PM",
			"11:57 PM",
			"11:58 PM",
			"11:59 PM",
			"12:00 PM",
			"12:01 PM",
			"12:02 PM",
			"12:03 PM",
			"12:04 PM",
			"12:05 PM",
			"12:06 PM",
			"12:07 PM",
			"12:08 PM",
			"12:09 PM",
			"12:10 PM",
			"12:11 PM",
			"12:12 PM",
			"12:13 PM",
			"12:14 PM",
			"12:15 PM",
			"12:16 PM",
			"12:17 PM",
			"12:18 PM",
			"12:19 PM",
			"12:20 PM",
			"12:21 PM",
			"12:22 PM",
			"12:23 PM",
			"12:24 PM",
			"12:25 PM",
			"12:26 PM",
			"12:27 PM",
			"12:28 PM",
			"12:29 PM",
			"12:30 PM",
			"12:31 PM",
			"12:32 PM",
			"12:33 PM",
			"12:34 PM",
			"12:35 PM",
			"12:36 PM",
			"12:37 PM",
			"12:38 PM",
			"12:39 PM",
			"12:40 PM",
			"12:41 PM",
			"12:42 PM",
			"12:43 PM",
			"12:44 PM",
			"12:45 PM",
			"12:46 PM",
			"12:47 PM",
			"12:48 PM",
			"12:49 PM",
			"12:50 PM",
			"12:51 PM",
			"12:52 PM",
			"12:53 PM",
			"12:54 PM",
			"12:55 PM",
			"12:56 PM",
			"12:57 PM",
			"12:58 PM",
			"12:59 PM"
		};

		// Token: 0x04000030 RID: 48
		private string[] AlarmValueP = new string[]
		{
			"1 AM",
			"1 e 1 AM",
			"1 e 2 AM",
			"1 e 3 AM",
			"1 e 4 AM",
			"1 e 5 AM",
			"1 e 6 AM",
			"1 e 7 AM",
			"1 e 8 AM",
			"1 e 9 AM",
			"1 e 10 AM",
			"1 e 11 AM",
			"1 e 12 AM",
			"1 e 13 AM",
			"1 e 14 AM",
			"1 e 15 AM",
			"1 e 16 AM",
			"1 e 17 AM",
			"1 e 18 AM",
			"1 e 19 AM",
			"1 e 20 AM",
			"1 e 21 AM",
			"1 e 22 AM",
			"1 e 23 AM",
			"1 e 24 AM",
			"1 e 25 AM",
			"1 e 26 AM",
			"1 e 27 AM",
			"1 e 28 AM",
			"1 e 29 AM",
			"1 e 30 AM",
			"1 e 31 AM",
			"1 e 32 AM",
			"1 e 33 AM",
			"1 e 34 AM",
			"1 e 35 AM",
			"1 e 36 AM",
			"1 e 37 AM",
			"1 e 38 AM",
			"1 e 39 AM",
			"1 e 40 AM",
			"1 e 41 AM",
			"1 e 42 AM",
			"1 e 43 AM",
			"1 e 44 AM",
			"1 e 45 AM",
			"1 e 46 AM",
			"1 e 47 AM",
			"1 e 48 AM",
			"1 e 49 AM",
			" 1 e 50 AM",
			"1 e 51 AM",
			"1 e 52 AM",
			"1 e 53 AM",
			"1 e 54 AM",
			"1 e 55 AM",
			"1 e 56 AM",
			"1 e 57 AM",
			"1 e 58 AM",
			"1 e 59 AM",
			"2 AM",
			"2 e 1 AM",
			"2 e 2 AM",
			"2 e 3 AM",
			"2 e 4 AM",
			"2 e 5 AM",
			"2 e 6 AM",
			"2 e 7 AM",
			"2 e 8 AM",
			"2 e 9 AM",
			"2 e 10 AM",
			"2 e 11 AM",
			"2 e 12 AM",
			"2 e 13 AM",
			"2 e 14 AM",
			"2 e 15 AM",
			"2 e 16 AM",
			"2 e 17 AM",
			"2 e 18 AM",
			"2 e 19 AM",
			"2 e 20 AM",
			"2 e 21 AM",
			"2 e 22 AM",
			"2 e 23 AM",
			"2 e 24 AM",
			"2 e 25 AM",
			"2 e 26 AM",
			"2 e 27 AM",
			"2 e 28 AM",
			"2 e 29 AM",
			"2 e 30 AM",
			"2 e 31 AM",
			"2 e 32 AM",
			"2 e 33 AM",
			"2 e 34 AM",
			"2 e 35 AM",
			"2 e 36 AM",
			"2 e 37 AM",
			"2 e 38 AM",
			"2 e 39 AM",
			"2 e 40 AM",
			"2 e 41 AM",
			"2 e 42 AM",
			"2 e 43 AM",
			"2 e 44 AM",
			"2 e 45 AM",
			"2 e 46 AM",
			"2 e 47 AM",
			"2 e 48 AM",
			"2 e 49 AM",
			" 2 e 50 AM",
			"2 e 51 AM",
			"2 e 52 AM",
			"2 e 53 AM",
			"2 e 54 AM",
			"2 e 55 AM",
			"2 e 56 AM",
			"2 e 57 AM",
			"2 e 58 AM",
			"2 e 59 AM",
			"3 AM",
			"3 e 1 AM",
			"3 e 2 AM",
			"3 e 3 AM",
			"3 e 4 AM",
			"3 e 5 AM",
			"3 e 6 AM",
			"3 e 7 AM",
			"3 e 8 AM",
			"3 e 9 AM",
			"3 e 10 AM",
			"3 e 11 AM",
			"3 e 12 AM",
			"3 e 13 AM",
			"3 e 14 AM",
			"3 e 15 AM",
			"3 e 16 AM",
			"3 e 17 AM",
			"3 e 18 AM",
			"3 e 19 AM",
			"3 e 20 AM",
			"3 e 21 AM",
			"3 e 22 AM",
			"3 e 23 AM",
			"3 e 24 AM",
			"3 e 25 AM",
			"3 e 26 AM",
			"3 e 27 AM",
			"3 e 28 AM",
			"3 e 29 AM",
			"3 e 30 AM",
			"3 e 31 AM",
			"3 e 32 AM",
			"3 e 33 AM",
			"3 e 34 AM",
			"3 e 35 AM",
			"3 e 36 AM",
			"3 e 37 AM",
			"3 e 38 AM",
			"3 e 39 AM",
			"3 e 40 AM",
			"3 e 41 AM",
			"3 e 42 AM",
			"3 e 43 AM",
			"3 e 44 AM",
			"3 e 45 AM",
			"3 e 46 AM",
			"3 e 47 AM",
			"3 e 48 AM",
			"3 e 49 AM",
			" 3 e 50 AM",
			"3 e 51 AM",
			"3 e 52 AM",
			"3 e 53 AM",
			"3 e 54 AM",
			"3 e 55 AM",
			"3 e 56 AM",
			"3 e 57 AM",
			"3 e 58 AM",
			"3 e 59 AM",
			"4 AM",
			"4 e 1 AM",
			"4 e 2 AM",
			"4 e 3 AM",
			"4 e 4 AM",
			"4 e 5 AM",
			"4 e 6 AM",
			"4 e 7 AM",
			"4 e 8 AM",
			"4 e 9 AM",
			"4 e 10 AM",
			"4 e 11 AM",
			"4 e 12 AM",
			"4 e 13 AM",
			"4 e 14 AM",
			"4 e 15 AM",
			"4 e 16 AM",
			"4 e 17 AM",
			"4 e 18 AM",
			"4 e 19 AM",
			"4 e 20 AM",
			"4 e 21 AM",
			"4 e 22 AM",
			"4 e 23 AM",
			"4 e 24 AM",
			"4 e 25 AM",
			"4 e 26 AM",
			"4 e 27 AM",
			"4 e 28 AM",
			"4 e 29 AM",
			"4 e 30 AM",
			"4 e 31 AM",
			"4 e 32 AM",
			"4 e 33 AM",
			"4 e 34 AM",
			"4 e 35 AM",
			"4 e 36 AM",
			"4 e 37 AM",
			"4 e 38 AM",
			"4 e 39 AM",
			"4 e 40 AM",
			"4 e 41 AM",
			"4 e 42 AM",
			"4 e 43 AM",
			"4 e 44 AM",
			"4 e 45 AM",
			"4 e 46 AM",
			"4 e 47 AM",
			"4 e 48 AM",
			"4 e 49 AM",
			" 4 e 50 AM",
			"4 e 51 AM",
			"4 e 52 AM",
			"4 e 53 AM",
			"4 e 54 AM",
			"4 e 55 AM",
			"4 e 56 AM",
			"4 e 57 AM",
			"4 e 58 AM",
			"4 e 59 AM",
			"5 AM",
			"5 e 1 AM",
			"5 e 2 AM",
			"5 e 3 AM",
			"5 e 4 AM",
			"5 e 5 AM",
			"5 e 6 AM",
			"5 e 7 AM",
			"5 e 8 AM",
			"5 e 9 AM",
			"5 e 10 AM",
			"5 e 11 AM",
			"5 e 12 AM",
			"5 e 13 AM",
			"5 e 14 AM",
			"5 e 15 AM",
			"5 e 16 AM",
			"5 e 17 AM",
			"5 e 18 AM",
			"5 e 19 AM",
			"5 e 20 AM",
			"5 e 21 AM",
			"5 e 22 AM",
			"5 e 23 AM",
			"5 e 24 AM",
			"5 e 25 AM",
			"5 e 26 AM",
			"5 e 27 AM",
			"5 e 28 AM",
			"5 e 29 AM",
			"5 e 30 AM",
			"5 e 31 AM",
			"5 e 32 AM",
			"5 e 33 AM",
			"5 e 34 AM",
			"5 e 35 AM",
			"5 e 36 AM",
			"5 e 37 AM",
			"5 e 38 AM",
			"5 e 39 AM",
			"5 e 40 AM",
			"5 e 41 AM",
			"5 e 42 AM",
			"5 e 43 AM",
			"5 e 44 AM",
			"5 e 45 AM",
			"5 e 46 AM",
			"5 e 47 AM",
			"5 e 48 AM",
			"5 e 49 AM",
			" 5 e 50 AM",
			"5 e 51 AM",
			"5 e 52 AM",
			"5 e 53 AM",
			"5 e 54 AM",
			"5 e 55 AM",
			"5 e 56 AM",
			"5 e 57 AM",
			"5 e 58 AM",
			"5 e 59 AM",
			"6 AM",
			"6 e 1 AM",
			"6 e 2 AM",
			"6 e 3 AM",
			"6 e 4 AM",
			"6 e 5 AM",
			"6 e 6 AM",
			"6 e 7 AM",
			"6 e 8 AM",
			"6 e 9 AM",
			"6 e 10 AM",
			"6 e 11 AM",
			"6 e 12 AM",
			"6 e 13 AM",
			"6 e 14 AM",
			"6 e 15 AM",
			"6 e 16 AM",
			"6 e 17 AM",
			"6 e 18 AM",
			"6 e 19 AM",
			"6 e 20 AM",
			"6 e 21 AM",
			"6 e 22 AM",
			"6 e 23 AM",
			"6 e 24 AM",
			"6 e 25 AM",
			"6 e 26 AM",
			"6 e 27 AM",
			"6 e 28 AM",
			"6 e 29 AM",
			"6 e 30 AM",
			"6 e 31 AM",
			"6 e 32 AM",
			"6 e 33 AM",
			"6 e 34 AM",
			"6 e 35 AM",
			"6 e 36 AM",
			"6 e 37 AM",
			"6 e 38 AM",
			"6 e 39 AM",
			"6 e 40 AM",
			"6 e 41 AM",
			"6 e 42 AM",
			"6 e 43 AM",
			"6 e 44 AM",
			"6 e 45 AM",
			"6 e 46 AM",
			"6 e 47 AM",
			"6 e 48 AM",
			"6 e 49 AM",
			" 6 e 50 AM",
			"6 e 51 AM",
			"6 e 52 AM",
			"6 e 53 AM",
			"6 e 54 AM",
			"6 e 55 AM",
			"6 e 56 AM",
			"6 e 57 AM",
			"6 e 58 AM",
			"6 e 59 AM",
			"7 AM",
			"7 e 1 AM",
			"7 e 2 AM",
			"7 e 3 AM",
			"7 e 4 AM",
			"7 e 5 AM",
			"7 e 6 AM",
			"7 e 7 AM",
			"7 e 8 AM",
			"7 e 9 AM",
			"7 e 10 AM",
			"7 e 11 AM",
			"7 e 12 AM",
			"7 e 13 AM",
			"7 e 14 AM",
			"7 e 15 AM",
			"7 e 16 AM",
			"7 e 17 AM",
			"7 e 18 AM",
			"7 e 19 AM",
			"7 e 20 AM",
			"7 e 21 AM",
			"7 e 22 AM",
			"7 e 23 AM",
			"7 e 24 AM",
			"7 e 25 AM",
			"7 e 26 AM",
			"7 e 27 AM",
			"7 e 28 AM",
			"7 e 29 AM",
			"7 e 30 AM",
			"7 e 31 AM",
			"7 e 32 AM",
			"7 e 33 AM",
			"7 e 34 AM",
			"7 e 35 AM",
			"7 e 36 AM",
			"7 e 37 AM",
			"7 e 38 AM",
			"7 e 39 AM",
			"7 e 40 AM",
			"7 e 41 AM",
			"7 e 42 AM",
			"7 e 43 AM",
			"7 e 44 AM",
			"7 e 45 AM",
			"7 e 46 AM",
			"7 e 47 AM",
			"7 e 48 AM",
			"7 e 49 AM",
			" 7 e 50 AM",
			"7 e 51 AM",
			"7 e 52 AM",
			"7 e 53 AM",
			"7 e 54 AM",
			"7 e 55 AM",
			"7 e 56 AM",
			"7 e 57 AM",
			"7 e 58 AM",
			"7 e 59 AM",
			"8 AM",
			"8 e 1 AM",
			"8 e 2 AM",
			"8 e 3 AM",
			"8 e 4 AM",
			"8 e 5 AM",
			"8 e 6 AM",
			"8 e 7 AM",
			"8 e 8 AM",
			"8 e 9 AM",
			"8 e 10 AM",
			"8 e 11 AM",
			"8 e 12 AM",
			"8 e 13 AM",
			"8 e 14 AM",
			"8 e 15 AM",
			"8 e 16 AM",
			"8 e 17 AM",
			"8 e 18 AM",
			"8 e 19 AM",
			"8 e 20 AM",
			"8 e 21 AM",
			"8 e 22 AM",
			"8 e 23 AM",
			"8 e 24 AM",
			"8 e 25 AM",
			"8 e 26 AM",
			"8 e 27 AM",
			"1 e 28 AM",
			"8 e 29 AM",
			"8 e 30 AM",
			"8 e 31 AM",
			"8 e 32 AM",
			"8 e 33 AM",
			"8 e 34 AM",
			"8 e 35 AM",
			"8 e 36 AM",
			"8 e 37 AM",
			"8 e 38 AM",
			"8 e 39 AM",
			"8 e 40 AM",
			"8 e 41 AM",
			"8 e 42 AM",
			"8 e 43 AM",
			"8 e 44 AM",
			"8 e 45 AM",
			"8 e 46 AM",
			"8 e 47 AM",
			"8 e 48 AM",
			"8 e 49 AM",
			" 8 e 50 AM",
			"8 e 51 AM",
			"8 e 52 AM",
			"8 e 53 AM",
			"8 e 54 AM",
			"8 e 55 AM",
			"8 e 56 AM",
			"8 e 57 AM",
			"8 e 58 AM",
			"8 e 59 AM",
			"9 AM",
			"9 e 1 AM",
			"9 e 2 AM",
			"9 e 3 AM",
			"9 e 4 AM",
			"9 e 5 AM",
			"9 e 6 AM",
			"9 e 7 AM",
			"9 e 9 AM",
			"9 e 9 AM",
			"9 e 10 AM",
			"9 e 11 AM",
			"9 e 12 AM",
			"9 e 13 AM",
			"9 e 14 AM",
			"9 e 15 AM",
			"9 e 16 AM",
			"9 e 17 AM",
			"9 e 18 AM",
			"9 e 19 AM",
			"9 e 20 AM",
			"9 e 21 AM",
			"9 e 22 AM",
			"9 e 23 AM",
			"9 e 24 AM",
			"9 e 25 AM",
			"9 e 26 AM",
			"9 e 27 AM",
			"9 e 28 AM",
			"9 e 29 AM",
			"9 e 30 AM",
			"9 e 31 AM",
			"9 e 32 AM",
			"9 e 33 AM",
			"9 e 34 AM",
			"9 e 35 AM",
			"9 e 36 AM",
			"9 e 37 AM",
			"9 e 38 AM",
			"9 e 39 AM",
			"9 e 40 AM",
			"9 e 41 AM",
			"9 e 42 AM",
			"9 e 43 AM",
			"9 e 44 AM",
			"9 e 45 AM",
			"9 e 46 AM",
			"9 e 47 AM",
			"9 e 48 AM",
			"9 e 49 AM",
			" 9 e 50 AM",
			"9 e 51 AM",
			"9 e 52 AM",
			"9 e 53 AM",
			"9 e 54 AM",
			"9 e 55 AM",
			"9 e 56 AM",
			"9 e 57 AM",
			"9 e 58 AM",
			"9 e 59 AM",
			"10 AM",
			"10 e 1 AM",
			"10 e 2 AM",
			"10 e 3 AM",
			"10 e 4 AM",
			"10 e 5 AM",
			"10 e 6 AM",
			"10 e 7 AM",
			"10 e 8 AM",
			"10 e 9 AM",
			"10 e 10 AM",
			"10 e 11 AM",
			"10 e 12 AM",
			"10 e 13 AM",
			"10 e 14 AM",
			"10 e 15 AM",
			"10 e 16 AM",
			"10 e 17 AM",
			"10 e 18 AM",
			"10 e 19 AM",
			"10 e 20 AM",
			"10 e 21 AM",
			"10 e 22 AM",
			"10 e 23 AM",
			"10 e 24 AM",
			"10 e 25 AM",
			"10 e 26 AM",
			"10 e 27 AM",
			"10 e 28 AM",
			"10 e 29 AM",
			"10 e 30 AM",
			"10 e 31 AM",
			"10 e 32 AM",
			"10 e 33 AM",
			"10 e 34 AM",
			"10 e 35 AM",
			"10 e 36 AM",
			"10 e 37 AM",
			"10 e 38 AM",
			"10 e 39 AM",
			"10 e 40 AM",
			"10 e 41 AM",
			"10 e 42 AM",
			"10 e 43 AM",
			"10 e 44 AM",
			"10 e 45 AM",
			"10 e 46 AM",
			"10 e 47 AM",
			"10 e 48 AM",
			"10 e 49 AM",
			" 10 e 50 AM",
			"10 e 51 AM",
			"10 e 52 AM",
			"10 e 53 AM",
			"10 e 54 AM",
			"10 e 55 AM",
			"10 e 56 AM",
			"10 e 57 AM",
			"10 e 58 AM",
			"10 e 59 AM",
			"11 AM",
			"11 e 1 AM",
			"11 e 2 AM",
			"11 e 3 AM",
			"11 e 4 AM",
			"11 e 5 AM",
			"11 e 6 AM",
			"11 e 7 AM",
			"11 e 8 AM",
			"11 e 9 AM",
			"11 e 10 AM",
			"11 e 11 AM",
			"11 e 12 AM",
			"11 e 13 AM",
			"11 e 14 AM",
			"11 e 15 AM",
			"11 e 16 AM",
			"11 e 17 AM",
			"11 e 18 AM",
			"11 e 19 AM",
			"11 e 20 AM",
			"11 e 21 AM",
			"11 e 22 AM",
			"11 e 23 AM",
			"11 e 24 AM",
			"11 e 25 AM",
			"11 e 26 AM",
			"11 e 27 AM",
			"11 e 28 AM",
			"11 e 29 AM",
			"11 e 30 AM",
			"11 e 31 AM",
			"11 e 32 AM",
			"11 e 33 AM",
			"11 e 34 AM",
			"11 e 35 AM",
			"11 e 36 AM",
			"11 e 37 AM",
			"11 e 38 AM",
			"11 e 39 AM",
			"11 e 40 AM",
			"11 e 41 AM",
			"11 e 42 AM",
			"11 e 43 AM",
			"11 e 44 AM",
			"11 e 45 AM",
			"11 e 46 AM",
			"11 e 47 AM",
			"11 e 48 AM",
			"11 e 49 AM",
			"11 e 50 AM",
			"11 e 51 AM",
			"11 e 52 AM",
			"11 e 53 AM",
			"11 e 54 AM",
			"11 e 55 AM",
			"11 e 56 AM",
			"11 e 57 AM",
			"11 e 58 AM",
			"11 e 59 AM",
			"12 AM",
			"12 e 1 AM",
			"12 e 2 AM",
			"12 e 3 AM",
			"12 e 4 AM",
			"12 e 5 AM",
			"12 e 6 AM",
			"12 e 7 AM",
			"12 e 8 AM",
			"12 e 9 AM",
			"12 e 10 AM",
			"12 e 11 AM",
			"12 e 12 AM",
			"12 e 13 AM",
			"12 e 14 AM",
			"12 e 15 AM",
			"12 e 16 AM",
			"12 e 17 AM",
			"12 e 18 AM",
			"12 e 19 AM",
			"12 e 20 AM",
			"12 e 21 AM",
			"12 e 22 AM",
			"12 e 23 AM",
			"12 e 24 AM",
			"12 e 25 AM",
			"12 e 26 AM",
			"12 e 27 AM",
			"12 e 28 AM",
			"12 e 29 AM",
			"12 e 30 AM",
			"12 e 31 AM",
			"12 e 32 AM",
			"12 e 33 AM",
			"12 e 34 AM",
			"12 e 35 AM",
			"12 e 36 AM",
			"12 e 37 AM",
			"12 e 38 AM",
			"12 e 39 AM",
			"12 e 40 AM",
			"12 e 41 AM",
			"12 e 42 AM",
			"12 e 43 AM",
			"12 e 44 AM",
			"12 e 45 AM",
			"12 e 46 AM",
			"12 e 47 AM",
			"12 e 48 AM",
			"12 e 49 AM",
			"12 e 50 AM",
			"12 e 51 AM",
			"12 e 52 AM",
			"12 e 53 AM",
			"12 e 54 AM",
			"12 e 55 AM",
			"12 e 56 AM",
			"12 e 57 AM",
			"12 e 58 AM",
			"12 e 59 AM",
			"1 PM",
			"1 e 1 PM",
			"1 e 2 PM",
			"1 e 3 PM",
			"1 e 4 PM",
			"1 e 5 PM",
			"1 e 6 PM",
			"1 e 7 PM",
			"1 e 8 PM",
			"1 e 9 PM",
			"1 e 10 PM",
			"1 e 11 PM",
			"1 e 12 PM",
			"1 e 13 PM",
			"1 e 14 PM",
			"1 e 15 PM",
			"1 e 16 PM",
			"1 e 17 PM",
			"1 e 18 PM",
			"1 e 19 PM",
			"1 e 20 PM",
			"1 e 21 PM",
			"1 e 22 PM",
			"1 e 23 PM",
			"1 e 24 PM",
			"1 e 25 PM",
			"1 e 26 PM",
			"1 e 27 PM",
			"1 e 28 PM",
			"1 e 29 PM",
			"1 e 30 PM",
			"1 e 31 PM",
			"1 e 32 PM",
			"1 e 33 PM",
			"1 e 34 PM",
			"1 e 35 PM",
			"1 e 36 PM",
			"1 e 37 PM",
			"1 e 38 PM",
			"1 e 39 PM",
			"1 e 40 PM",
			"1 e 41 PM",
			"1 e 42 PM",
			"1 e 43 PM",
			"1 e 44 PM",
			"1 e 45 PM",
			"1 e 46 PM",
			"1 e 47 PM",
			"1 e 48 PM",
			"1 e 49 PM",
			"1 e 50 PM",
			"1 e 51 PM",
			"1 e 52 PM",
			"1 e 53 PM",
			"1 e 54 PM",
			"1 e 55 PM",
			"1 e 56 PM",
			"1 e 57 PM",
			"1 e 58 PM",
			"1 e 59 PM",
			"2 PM",
			"2 e 1 PM",
			"2 e 2 PM",
			"2 e 3 PM",
			"2 e 4 PM",
			"2 e 5 PM",
			"2 e 6 PM",
			"2 e 7 PM",
			"2 e 8 PM",
			"2 e 9 PM",
			"2 e 10 PM",
			"2 e 11 PM",
			"2 e 12 PM",
			"2 e 13 PM",
			"2 e 14 PM",
			"2 e 15 PM",
			"2 e 16 PM",
			"2 e 17 PM",
			"2 e 18 PM",
			"2 e 19 PM",
			"2 e 20 PM",
			"2 e 21 PM",
			"2 e 22 PM",
			"2 e 23 PM",
			"2 e 24 PM",
			"2 e 25 PM",
			"2 e 26 PM",
			"2 e 27 PM",
			"2 e 28 PM",
			"2 e 29 PM",
			"2 e 30 PM",
			"2 e 31 PM",
			"2 e 32 PM",
			"2 e 33 PM",
			"2 e 34 PM",
			"2 e 35 PM",
			"2 e 36 PM",
			"2 e 37 PM",
			"2 e 38 PM",
			"2 e 39 PM",
			"2 e 40 PM",
			"2 e 41 PM",
			"2 e 42 PM",
			"2 e 43 PM",
			"2 e 44 PM",
			"2 e 45 PM",
			"2 e 46 PM",
			"2 e 47 PM",
			"2 e 48 PM",
			"2 e 49 PM",
			" 2 e 50 PM",
			"2 e 51 PM",
			"2 e 52 PM",
			"2 e 53 PM",
			"2 e 54 PM",
			"2 e 55 PM",
			"2 e 56 PM",
			"2 e 57 PM",
			"2 e 58 PM",
			"2 e 59 PM",
			"3 PM",
			"3 e 1 PM",
			"3 e 2 PM",
			"3 e 3 PM",
			"3 e 4 PM",
			"3 e 5 PM",
			"3 e 6 PM",
			"3 e 7 PM",
			"3 e 8 PM",
			"3 e 9 PM",
			"3 e 10 PM",
			"3 e 11 PM",
			"3 e 12 PM",
			"3 e 13 PM",
			"3 e 14 PM",
			"3 e 15 PM",
			"3 e 16 PM",
			"3 e 17 PM",
			"3 e 18 PM",
			"3 e 19 PM",
			"3 e 20 PM",
			"3 e 21 PM",
			"3 e 22 PM",
			"3 e 23 PM",
			"3 e 24 PM",
			"3 e 25 PM",
			"3 e 26 PM",
			"3 e 27 PM",
			"3 e 28 PM",
			"3 e 29 PM",
			"3 e 30 PM",
			"3 e 31 PM",
			"3 e 32 PM",
			"3 e 33 PM",
			"3 e 34 PM",
			"3 e 35 PM",
			"3 e 36 PM",
			"3 e 37 PM",
			"3 e 38 PM",
			"3 e 39 PM",
			"3 e 40 PM",
			"3 e 41 PM",
			"3 e 42 PM",
			"3 e 43 PM",
			"3 e 44 PM",
			"3 e 45 PM",
			"3 e 46 PM",
			"3 e 47 PM",
			"3 e 48 PM",
			"3 e 49 PM",
			"3 e 50 PM",
			"3 e 51 PM",
			"3 e 52 PM",
			"3 e 53 PM",
			"3 e 54 PM",
			"3 e 55 PM",
			"3 e 56 PM",
			"3 e 57 PM",
			"3 e 58 PM",
			"3 e 59 PM",
			"4 PM",
			"4 e 1 PM",
			"4 e 2 PM",
			"4 e 3 PM",
			"4 e 4 PM",
			"4 e 5 PM",
			"4 e 6 PM",
			"4 h 7 PM",
			"4 e 8 PM",
			"4 h 9 PM",
			"4 e 10 PM",
			"4 e 11 PM",
			"4 e 12 PM",
			"4 e 13 PM",
			"4 e 14 PM",
			"4 e 15 PM",
			"4 e 16 PM",
			"4 h 17 PM",
			"4 e 18 PM",
			"4 e 19 PM",
			"4 e 20 PM",
			"4 e 21 PM",
			"4 e 22 PM",
			"4 e 23 PM",
			"4 e 24 PM",
			"4 e 25 PM",
			"4 h 26 PM",
			"4 e 27 PM",
			"4 e 28 PM",
			"4 e 29 PM",
			"4 e 30 PM",
			"4 e 31 PM",
			"4 e 32 PM",
			"4 e 33 PM",
			"4 e 34 PM",
			"4 h 35 PM",
			"4 e 36 PM",
			"4 e 37 PM",
			"4 e 38 PM",
			"4 e 39 PM",
			"4 e 40 PM",
			"4 e 41 PM",
			"4 e 42 PM",
			"4 e 43 PM",
			"4 h 44 PM",
			"4 e 45 PM",
			"4 e 46 PM",
			"4 e 47 PM",
			"4 e 48 PM",
			"4 e 49 PM",
			"4 e 50 PM",
			"4 e 51 PM",
			"4 e 52 PM",
			"4 h 53 PM",
			"4 e 54 PM",
			"4 e 55 PM",
			"4 e 56 PM",
			"4 e 57 PM",
			"4 e 58 PM",
			"4 e 59 PM",
			"5 PM",
			"5 e 1 PM",
			"5 e 2 PM",
			"5 e 3 PM",
			"5 e 4 PM",
			"5 e 5 PM",
			"5 e 6 PM",
			"5 e 7 PM",
			"5 e 8 PM",
			"5 h 9 PM",
			"5 e 10 PM",
			"5 e 11 PM",
			"5 e 12 PM",
			"5 e 13 PM",
			"5 e 14 PM",
			"5 e 15 PM",
			"5 e 16 PM",
			"5 e 17 PM",
			"5 e 18 PM",
			"5 e 19 PM",
			"5 e 20 PM",
			"5 e 21 PM",
			"5 e 22 PM",
			"5 e 23 PM",
			"5 e 24 PM",
			"5 e 25 PM",
			"5 e 26 PM",
			"5 e 27 PM",
			"5 e 28 PM",
			"5 e 29 PM",
			"5 e 30 PM",
			"5 e 31 PM",
			"5 e 32 PM",
			"5 e 33 PM",
			"5 e 34 PM",
			"5 e 35 PM",
			"5 e 36 PM",
			"5 e 37 PM",
			"5 e 38 PM",
			"5 e 39 PM",
			"5 e 40 PM",
			"5 e 41 PM",
			"5 e 42 PM",
			"5 e 43 PM",
			"5 e 44 PM",
			"5 e 45 PM",
			"5 e 46 PM",
			"5 e 47 PM",
			"5 e 48 PM",
			"5 e 49 PM",
			"5 e 50 PM",
			"5 e 51 PM",
			"5 e 52 PM",
			"5 e 53 PM",
			"5 e 54 PM",
			"5 e 55 PM",
			"5 e 56 PM",
			"5 e 57 PM",
			"5 e 58 PM",
			"5 e 59 PM",
			"6 PM",
			"6 e 1 PM",
			"6 e 2 PM",
			"6 e 3 PM",
			"6 e 4 PM",
			"6 e 5 PM",
			"6 e 6 PM",
			"6 e 7 PM",
			"6 e 8 PM",
			"6 h 9 PM",
			"6 e 10 PM",
			"6 e 11 PM",
			"6 e 12 PM",
			"6 e 13 PM",
			"6 e 14 PM",
			"6 e 15 PM",
			"6 e 16 PM",
			"6 e 17 PM",
			"6 e 18 PM",
			"6 e 19 PM",
			"6 e 20 PM",
			"6 e 21 PM",
			"6 e 22 PM",
			"6 e 23 PM",
			"6 e 24 PM",
			"6 e 25 PM",
			"6 e 26 PM",
			"6 e 27 PM",
			"6 e 28 PM",
			"6 e 29 PM",
			"6 e 30 PM",
			"6 e 31 PM",
			"6 e 32 PM",
			"6 e 33 PM",
			"6 e 34 PM",
			"6 e 35 PM",
			"6 e 36 PM",
			"6 e 37 PM",
			"6 e 38 PM",
			"6 e 39 PM",
			"6 e 40 PM",
			"6 e 41 PM",
			"6 e 42 PM",
			"6 e 43 PM",
			"6 e 44 PM",
			"6 e 45 PM",
			"6 e 46 PM",
			"6 e 47 PM",
			"6 e 48 PM",
			"6 e 49 PM",
			" 6 e 50 PM",
			"6 e 51 PM",
			"6 e 52 PM",
			"6 e 53 PM",
			"6 e 54 PM",
			"6 e 55 PM",
			"6 e 56 PM",
			"6 e 57 PM",
			"6 e 58 PM",
			"6 e 59 PM",
			"7 PM",
			"7 e 1 PM",
			"7 e 2 PM",
			"7 e 3 PM",
			"7 e 4 PM",
			"7 e 5 PM",
			"7 e 6 PM",
			"7 e 7 PM",
			"7 e 8 PM",
			"7 e 9 PM",
			"7 e 10 PM",
			"7 e 11 PM",
			"7 e 12 PM",
			"7 e 13 PM",
			"7 e 14 PM",
			"7 e 15 PM",
			"7 e 16 PM",
			"7 e 17 PM",
			"7 e 18 PM",
			"7 e 19 PM",
			"7 e 20 PM",
			"7 e 21 PM",
			"7 e 22 PM",
			"7 e 23 PM",
			"7 e 24 PM",
			"7 e 25 PM",
			"7 e 26 PM",
			"7 e 27 PM",
			"7 e 28 PM",
			"7 e 29 PM",
			"7 e 30 PM",
			"7 e 31 PM",
			"7 e 32 PM",
			"7 e 33 PM",
			"7 e 34 PM",
			"7 e 35 PM",
			"7 e 36 PM",
			"7 e 37 PM",
			"7 e 38 PM",
			"7 e 39 PM",
			"7 e 40 PM",
			"7 e 41 PM",
			"7 e 42 PM",
			"7 e 43 PM",
			"7 e 44 PM",
			"7 e 45 PM",
			"7 e 46 PM",
			"7 e 47 PM",
			"7 e 48 PM",
			"7 e 49 PM",
			"7 e 50 PM",
			"7 e 51 PM",
			"7 e 52 PM",
			"7 e 53 PM",
			"7 e 54 PM",
			"7 e 55 PM",
			"7 e 56 PM",
			"7 e 57 PM",
			"7 e 58 PM",
			"7 e 59 PM",
			"8 PM",
			"8 e 1 PM",
			"8 e 2 PM",
			"8 e 3 PM",
			"8 e 4 PM",
			"8 e 5 PM",
			"8 e 6 PM",
			"8 e 7 PM",
			"8 e 8 PM",
			"8 e 9 PM",
			"8 e 10 PM",
			"8 e 11 PM",
			"8 e 12 PM",
			"8 e 13 PM",
			"8 e 14 PM",
			"8 e 15 PM",
			"8 e 16 PM",
			"8 e 17 PM",
			"8 e 18 PM",
			"8 e 19 PM",
			"8 e 20 PM",
			"8 e 21 PM",
			"8 e 22 PM",
			"8 e 23 PM",
			"8 e 24 PM",
			"8 e 25 PM",
			"8 e 26 PM",
			"8 e 27 PM",
			"8 e 28 PM",
			"8 e 29 PM",
			"8 e 30 PM",
			"8 e 31 PM",
			"8 e 32 PM",
			"8 e 33 PM",
			"8 e 34 PM",
			"8 e 35 PM",
			"8 e 36 PM",
			"8 e 37 PM",
			"8 e 38 PM",
			"8 e 39 PM",
			"8 e 40 PM",
			"8 e 41 PM",
			"8 e 42 PM",
			"8 e 43 PM",
			"8 e 44 PM",
			"8 e 45 PM",
			"8 e 46 PM",
			"8 e 47 PM",
			"8 e 48 PM",
			"8 e 49 PM",
			"8 e 50 PM",
			"8 e 51 PM",
			"8 e 52 PM",
			"8 e 53 PM",
			"8 e 54 PM",
			"8 e 55 PM",
			"8 e 56 PM",
			"8 e 57 PM",
			"8 e 58 PM",
			"8 e 59 PM",
			"9 PM",
			"9 e 1 PM",
			"9 e 2 PM",
			"9 e 3 PM",
			"9 e 4 PM",
			"9 e 5 PM",
			"9 e 6 PM",
			"9 e 7 PM",
			"8 e 9 PM",
			"9 e 9 PM",
			"9 e 10 PM",
			"9 e 11 PM",
			"9 e 12 PM",
			"9 e 13 PM",
			"9 e 14 PM",
			"9 e 15 PM",
			"9 e 16 PM",
			"9 e 17 PM",
			"9 e 18 PM",
			"9 e 19 PM",
			"9 e 20 PM",
			"9 e 21 PM",
			"9 e 22 PM",
			"9 e 23 PM",
			"9 e 24 PM",
			"9 e 25 PM",
			"9 e 26 PM",
			"9 e 27 PM",
			"9 e 28 PM",
			"9 e 29 PM",
			"9 e 30 PM",
			"9 e 31 PM",
			"9 e 32 PM",
			"9 e 33 PM",
			"9 e 34 PM",
			"9 e 35 PM",
			"9 e 36 PM",
			"9 e 37 PM",
			"9 e 38 PM",
			"9 e 39 PM",
			"9 e 40 PM",
			"9 e 41 PM",
			"9 e 42 PM",
			"9 e 43 PM",
			"9 e 44 PM",
			"9 e 45 PM",
			"9 e 46 PM",
			"9 e 47 PM",
			"9 e 48 PM",
			"9 e 49 PM",
			"9 e 50 PM",
			"9 e 51 PM",
			"9 e 52 PM",
			"9 e 53 PM",
			"9 e 54 PM",
			"9 e 55 PM",
			"9 e 56 PM",
			"9 e 57 PM",
			"9 e 58 PM",
			"9 e 59 PM",
			"10 PM",
			"10 e 1 PM",
			"10 e 2 PM",
			"10 e 3 PM",
			"10 e 4 PM",
			"10 e 5 PM",
			"10 e 6 PM",
			"10 e 7 PM",
			"10 e 8 PM",
			"10 e 9 PM",
			"10 e 10 PM",
			"10 e 11 PM",
			"10 e 12 PM",
			"10 e 13 PM",
			"10 e 14 PM",
			"10 e 15 PM",
			"10 e 16 PM",
			"10 e 17 PM",
			"10 e 18 PM",
			"10 e 19 PM",
			"10 e 20 PM",
			"10 e 21 PM",
			"10 e 22 PM",
			"10 e 23 PM",
			"10 e 24 PM",
			"10 e 25 PM",
			"10 e 26 PM",
			"10 e 27 PM",
			"10 e 28 PM",
			"10 e 29 PM",
			"10 e 30 PM",
			"10 e 31 PM",
			"10 e 32 PM",
			"10 e 33 PM",
			"10 e 34 PM",
			"10 e 35 PM",
			"10 e 36 PM",
			"10 e 37 PM",
			"10 e 38 PM",
			"10 e 39 PM",
			"10 e 40 PM",
			"10 e 41 PM",
			"10 e 42 PM",
			"10 e 43 PM",
			"10 e 44 PM",
			"10 e 45 PM",
			"10 e 46 PM",
			"10 e 47 PM",
			"10 e 48 PM",
			"10 e 49 PM",
			"10 e 50 PM",
			"10 e 51 PM",
			"10 e 52 PM",
			"10 e 53 PM",
			"10 e 54 PM",
			"10 e 55 PM",
			"10 e 56 PM",
			"10 e 57 PM",
			"10 e 58 PM",
			"10 e 59 PM",
			"11 PM",
			"11 e 1 PM",
			"11 e 2 PM",
			"11 e 3 PM",
			"11 e 4 PM",
			"11 e 5 PM",
			"11 e 6 PM",
			"11 e 7 PM",
			"11 e 8 PM",
			"11 e 9 PM",
			"11 e 10 PM",
			"11 e 11 PM",
			"11 e 12 PM",
			"11 e 13 PM",
			"11 e 14 PM",
			"11 e 15 PM",
			"11 e 16 PM",
			"11 e 17 PM",
			"11 e 18 PM",
			"11 e 19 PM",
			"11 e 20 PM",
			"11 e 21 PM",
			"11 e 22 PM",
			"11 e 23 PM",
			"11 e 24 PM",
			"11 e 25 PM",
			"11 e 26 PM",
			"11 e 27 PM",
			"11 e 28 PM",
			"11 e 29 PM",
			"11 e 30 PM",
			"11 e 31 PM",
			"11 e 32 PM",
			"11 e 33 PM",
			"11 e 34 PM",
			"11 e 35 PM",
			"11 e 36 PM",
			"11 e 37 PM",
			"11 e 38 PM",
			"11 e 39 PM",
			"11 e 40 PM",
			"11 e 41 PM",
			"11 e 42 PM",
			"11 e 43 PM",
			"11 e 44 PM",
			"11 e 45 PM",
			"11 e 46 PM",
			"11 e 47 PM",
			"11 e 48 PM",
			"11 e 49 PM",
			"11 e 50 PM",
			"11 e 51 PM",
			"11 e 52 PM",
			"11 e 53 PM",
			"11 e 54 PM",
			"11 e 55 PM",
			"11 e 56 PM",
			"11 e 57 PM",
			"11 e 58 PM",
			"11 e 59 PM",
			"12 PM",
			"12 e 1 PM",
			"12 e 2 PM",
			"12 e 3 PM",
			"12 e 4 PM",
			"12 e 5 PM",
			"12 e 6 PM",
			"12 e 7 PM",
			"12 e 8 PM",
			"12 e 9 PM",
			"12 e 10 PM",
			"12 e 11 PM",
			"12 e 12 PM",
			"12 e 13 PM",
			"12 e 14 PM",
			"12 e 15 PM",
			"12 e 16 PM",
			"12 e 17 PM",
			"12 e 18 PM",
			"12 e 19 PM",
			"12 e 20 PM",
			"12 e 21 PM",
			"12 e 22 PM",
			"12 e 23 PM",
			"12 e 24 PM",
			"12 e 25 PM",
			"12 e 26 PM",
			"12 e 27 PM",
			"12 e 28 PM",
			"12 e 29 PM",
			"12 e 30 PM",
			"12 e 31 PM",
			"12 e 32 PM",
			"12 e 33 PM",
			"12 e 34 PM",
			"12 e 35 PM",
			"12 e 36 PM",
			"12 e 37 PM",
			"12 e 38 PM",
			"12 e 39 PM",
			"12 e 40 PM",
			"12 e 41 PM",
			"12 e 42 PM",
			"12 e 43 PM",
			"12 e 44 PM",
			"12 e 45 PM",
			"12 e 46 PM",
			"12 e 47 PM",
			"12 e 48 PM",
			"12 e 49 PM",
			"12 e 50 PM",
			"12 e 51 PM",
			"12 e 52 PM",
			"12 e 53 PM",
			"12 e 54 PM",
			"12 e 55 PM",
			"12 e 56 PM",
			"12 e 57 PM",
			"12 e 58 PM",
			"12 e 59 PM"
		};

		// Token: 0x04000031 RID: 49
		public string[] numberString = new string[]
		{
			"zero",
			"um",
			"dois",
			"três",
			"quatro",
			"cinco",
			"seis",
			"sete",
			"oito",
			"nove",
			"dez",
			"onze",
			"doze",
			"treze",
			"Catorze",
			"quinze",
			"dezeseis",
			"dezesete",
			"dezoito",
			"dezenove",
			"vinte",
			"vinte e um",
			"vinte e dois",
			"vinte e três",
			"vinte e quatro",
			"vinte e cinco",
			"vinte e seis",
			"vinte e sete",
			"vinte e oito",
			"vinte e nove",
			"trinta",
			"trinta e um",
			"trinta e dois",
			"trinta e três",
			"trinta e quatro",
			"trinta e cinco",
			"trinta e seis",
			"trinta e sete",
			"trinta e oito",
			"trinta e nove",
			"quarenta",
			"quarenta um",
			"quarenta dois",
			"quarenta três",
			"quarenta quatro",
			"quarenta cinco",
			"quarenta seis",
			"quarenta sete",
			"quarenta oito",
			"quarenta nove",
			"ciquenta",
			"ciquenta um",
			"ciquenta dois",
			"ciquenta três",
			"ciquenta quatro",
			"ciquenta cinco",
			"ciquenta seis",
			"ciquenta sete",
			"ciquenta oito",
			"ciquenta nove",
			"sessenta",
			"sessenta um",
			"sessenta dois",
			"sessenta três",
			"sessenta quatro",
			"sessenta cinco",
			"sessenta seis",
			"sessenta sete",
			"sessenta oito",
			"sessenta nove",
			"setenta",
			"setenta um",
			"setenta dois",
			"setenta três",
			"setenta quatro",
			"setenta cinco",
			"setenta seis",
			"setenta sete",
			"setenta oito",
			"setenta nove",
			"oitenta",
			"oitenta um",
			"oitenta dois",
			"oitenta três",
			"oitenta quatro",
			"oitenta cinco",
			"oitenta seis",
			"oitenta sete",
			"oitenta oito",
			"oitenta nove",
			"noventa",
			"noventa um",
			"noventa dois",
			"noventa três",
			"noventa quatro",
			"noventa cinco",
			"noventa seis",
			"noventa sete",
			"noventa oito",
			"noventa nove",
			"cem",
			"cento e um",
			"cento e dois",
			"cento e três",
			"cento e quatro",
			"cento e cinco",
			"cento e seis",
			"cento e sete",
			"cento e oito",
			"cento e nove",
			"cento e dez",
			"cento e onze",
			"cento e doze",
			"cento e treze",
			"cento e quatorze ",
			"cento e quinze",
			"cento e dezesseis",
			"cento e dezessete",
			"cento e dezoito",
			"cento e dezenove",
			"cento e vinte",
			"cento e vinte e um",
			"cento e vinte e dois",
			"cento e vinte e três",
			"cento e vinte e quatro",
			"cento e vinte e cinco",
			"cento e vinte e seis",
			"cento e vinte e sete",
			"cento e vinte e oito",
			"cento e vinte e nove",
			"cento e trinta",
			"cento e trinta e um",
			"cento e trinta e dois",
			"cento e trinta e três",
			"cento e trinta e quatro",
			"cento e trinta e cinco",
			"cento e trinta e seis",
			"cento e trinta e sete",
			"cento e trinta e oito",
			"cento e trinta e nove",
			"cento e quarenta",
			"cento e quarenta um",
			"cento e quarenta dois",
			"cento e quarenta três",
			"cento e quarenta quatro",
			"cento e quarenta cinco",
			"cento e quarenta seis",
			"cento e quarenta sete",
			"cento e quarenta oito",
			"cento e quarenta nove",
			"cento e ciquenta",
			"cento e ciquenta um",
			"cento e ciquenta dois",
			"cento e ciquenta três",
			"cento e ciquenta quatro",
			"cento e ciquenta cinco",
			"cento e ciquenta seis",
			"cento e ciquenta sete",
			"cento e ciquenta oito",
			"cento e ciquenta nove",
			"cento e sessenta",
			"cento e sessenta um",
			"cento e sessenta dois",
			"cento e sessenta três",
			"cento e sessenta quatro",
			"cento e sessenta cinco",
			"cento e sessenta seis",
			"cento e sessenta sete",
			"cento e sessenta oito",
			"cento e sessenta nove",
			"cento e setenta",
			"cento e setenta um",
			"cento e setenta dois",
			"cento e setenta três",
			"cento e setenta quatro",
			"cento e setenta cinco",
			"cento e setenta seis",
			"cento e setenta sete",
			"cento e setenta oito",
			"cento e setenta nove",
			"cento e oitenta",
			"cento e oitenta um",
			"cento e oitenta dois",
			"cento e oitenta três",
			"cento e oitenta quatro",
			"cento e oitenta cinco",
			"cento e oitenta seis",
			"cento e oitenta sete",
			"cento e oitenta oito",
			"cento e oitenta nove",
			"cento e noventa",
			"cento e noventa um",
			"cento e noventa dois",
			"cento e noventa três",
			"cento e noventa quatro",
			"cento e noventa cinco",
			"cento e noventa seis",
			"cento e noventa sete",
			"cento e noventa oito",
			"cento e noventa nove",
			"duzentos",
			"duzentos e um",
			"duzentos e dois",
			"duzentos e três",
			"duzentos e quatro",
			"duzentos e cinco",
			"duzentos e seis",
			"duzentos e sete",
			"duzentos e oito",
			"duzentos e nove",
			"duzentos e dez",
			"duzentos e onze",
			"duzentos e doze",
			"duzentos e treze",
			"duzentos e quatorze ",
			"duzentos e quinze",
			"duzentos e dezesseis",
			"duzentos e dezessete",
			"duzentos e dezoito",
			"duzentos e dezenove",
			"duzentos e vinte",
			"duzentos e vinte e um",
			"duzentos e vinte e dois",
			"duzentos e vinte e três",
			"duzentos e vinte e quatro",
			"duzentos e vinte e cinco",
			"duzentos e vinte e seis",
			"duzentos e vinte e sete",
			"duzentos e vinte e oito",
			"duzentos e vinte e nove",
			"duzentos e trinta",
			"duzentos e trinta e um",
			"duzentos e trinta e dois",
			"duzentos e trinta e três",
			"duzentos e trinta e quatro",
			"duzentos e trinta e cinco",
			"duzentos e trinta e seis",
			"duzentos e trinta e sete",
			"duzentos e trinta e oito",
			"duzentos e trinta e nove",
			"duzentos e quarenta",
			"duzentos e quarenta um",
			"duzentos e quarenta dois",
			"duzentos e quarenta três",
			"duzentos e quarenta quatro",
			"duzentos e quarenta cinco",
			"duzentos e quarenta seis",
			"duzentos e quarenta sete",
			"duzentos e quarenta oito",
			"duzentos e quarenta nove",
			"duzentos e ciquenta",
			"duzentos e ciquenta um",
			"duzentos e ciquenta dois",
			"duzentos e ciquenta três",
			"duzentos e ciquenta quatro",
			"duzentos e ciquenta cinco",
			"duzentos e ciquenta seis",
			"duzentos e ciquenta sete",
			"duzentos e ciquenta oito",
			"duzentos e ciquenta nove",
			"duzentos e sessenta",
			"duzentos e sessenta um",
			"duzentos e sessenta dois",
			"duzentos e sessenta três",
			"duzentos e sessenta quatro",
			"duzentos e sessenta cinco",
			"duzentos e sessenta seis",
			"duzentos e sessenta sete",
			"duzentos e sessenta oito",
			"duzentos e sessenta nove",
			"duzentos e setenta",
			"duzentos e setenta um",
			"duzentos e setenta dois",
			"duzentos e setenta três",
			"duzentos e setenta quatro",
			"duzentos e setenta cinco",
			"duzentos e setenta seis",
			"duzentos e setenta sete",
			"duzentos e setenta oito",
			"duzentos e setenta nove",
			"duzentos e oitenta",
			"duzentos e oitenta um",
			"duzentos e oitenta dois",
			"duzentos e oitenta três",
			"duzentos e oitenta quatro",
			"duzentos e oitenta cinco",
			"duzentos e oitenta seis",
			"duzentos e oitenta sete",
			"duzentos e oitenta oito",
			"duzentos e oitenta nove",
			"duzentos e noventa",
			"duzentos e noventa um",
			"duzentos e noventa dois",
			"duzentos e noventa três",
			"duzentos e noventa quatro",
			"duzentos e noventa cinco",
			"duzentos e noventa seis",
			"duzentos e noventa sete",
			"duzentos e noventa oito",
			"duzentos e noventa nove",
			"trezentos",
			"trezentos e um",
			"trezentos e dois",
			"trezentos e três",
			"trezentos e quatro",
			"trezentos e cinco",
			"trezentos e seis",
			"trezentos e sete",
			"trezentos e oito",
			"trezentos e nove",
			"trezentos e dez",
			"trezentos e onze",
			"trezentos e doze",
			"trezentos e treze",
			"trezentos e Catorze",
			"trezentos e quinze",
			"trezentos e dezesseis",
			"trezentos e dezessete",
			"trezentos e dezoito",
			"trezentos e dezenove",
			"trezentos e vinte",
			"trezentos e vinte e um",
			"trezentos e vinte e dois",
			"trezentos e vinte e três",
			"trezentos e vinte e quatro",
			"trezentos e vinte e cinco",
			"trezentos e vinte e seis",
			"trezentos e vinte e sete",
			"trezentos e vinte e oito",
			"trezentos e vinte e nove",
			"trezentos e trinta",
			"trezentos e trinta e um",
			"trezentos e trinta e dois",
			"trezentos e trinta e três",
			"trezentos e trinta e quatro",
			"trezentos e trinta e cinco",
			"trezentos e trinta e seis",
			"trezentos e trinta e sete",
			"trezentos e trinta e oito",
			"trezentos e trinta e nove",
			"trezentos e quarenta",
			"trezentos e quarenta um",
			"trezentos e quarenta dois",
			"trezentos e quarenta três",
			"trezentos e quarenta quatro",
			"trezentos e quarenta cinco",
			"trezentos e quarenta seis",
			"trezentos e quarenta sete",
			"trezentos e quarenta oito",
			"trezentos e quarenta nove",
			"trezentos e ciquenta",
			"trezentos e ciquenta um",
			"trezentos e ciquenta dois",
			"trezentos e ciquenta três",
			"trezentos e ciquenta quatro",
			"trezentos e ciquenta cinco",
			"trezentos e ciquenta seis",
			"trezentos e ciquenta sete",
			"trezentos e ciquenta oito",
			"trezentos e ciquenta nove",
			"trezentos e sessenta",
			"trezentos e sessenta um",
			"trezentos e sessenta dois",
			"trezentos e sessenta três",
			"trezentos e sessenta quatro",
			"trezentos e sessenta cinco",
			"trezentos e sessenta seis",
			"trezentos e sessenta sete",
			"trezentos e sessenta oito",
			"trezentos e sessenta nove",
			"trezentos e setenta",
			"trezentos e setenta um",
			"trezentos e setenta dois",
			"trezentos e setenta três",
			"trezentos e setenta quatro",
			"trezentos e setenta cinco",
			"trezentos e setenta seis",
			"trezentos e setenta sete",
			"trezentos e setenta oito",
			"trezentos e setenta nove",
			"trezentos e oitenta",
			"trezentos e oitenta um",
			"trezentos e oitenta dois",
			"trezentos e oitenta três",
			"trezentos e oitenta quatro",
			"trezentos e oitenta cinco",
			"trezentos e oitenta seis",
			"trezentos e oitenta sete",
			"trezentos e oitenta oito",
			"trezentos e oitenta nove",
			"trezentos e noventa",
			"trezentos e noventa um",
			"trezentos e noventa dois",
			"trezentos e noventa três",
			"trezentos e noventa quatro",
			"trezentos e noventa cinco",
			"trezentos e noventa seis",
			"trezentos e noventa sete",
			"trezentos e noventa oito",
			"trezentos e noventa nove",
			"quatrocentos",
			"quatrocentos e um",
			"quatrocentos e dois",
			"quatrocentos e três",
			"quatrocentos e quatro",
			"quatrocentos e cinco",
			"quatrocentos e seis",
			"quatrocentos e sete",
			"quatrocentos e oito",
			"quatrocentos e nove",
			"quatrocentos e dez",
			"quatrocentos e onze",
			"quatrocentos e doze",
			"quatrocentos e treze",
			"quatrocentos e Catorze",
			"quatrocentos e quinze",
			"quatrocentos e dezesseis",
			"quatrocentos e dezessete",
			"quatrocentos e dezoito",
			"quatrocentos e dezenove",
			"quatrocentos e vinte",
			"quatrocentos e vinte e um",
			"quatrocentos e vinte e dois",
			"quatrocentos e vinte e três",
			"quatrocentos e vinte e quatro",
			"quatrocentos e vinte e cinco",
			"quatrocentos e vinte e seis",
			"quatrocentos e vinte e sete",
			"quatrocentos e vinte e oito",
			"quatrocentos e vinte e nove",
			"quatrocentos e trinta",
			"quatrocentos e trinta e um",
			"quatrocentos e trinta e dois",
			"quatrocentos e trinta e três",
			"quatrocentos e trinta e quatro",
			"quatrocentos e trinta e cinco",
			"quatrocentos e trinta e seis",
			"quatrocentos e trinta e sete",
			"quatrocentos e trinta e oito",
			"quatrocentos e trinta e nove",
			"quatrocentos e quarenta",
			"quatrocentos e quarenta um",
			"quatrocentos e quarenta dois",
			"quatrocentos e quarenta três",
			"quatrocentos e quarenta quatro",
			"quatrocentos e quarenta cinco",
			"quatrocentos e quarenta seis",
			"quatrocentos e quarenta sete",
			"quatrocentos e quarenta oito",
			"quatrocentos e quarenta nove",
			"quatrocentos e ciquenta",
			"quatrocentos e ciquenta um",
			"quatrocentos e ciquenta dois",
			"quatrocentos e ciquenta três",
			"quatrocentos e ciquenta quatro",
			"quatrocentos e ciquenta cinco",
			"quatrocentos e ciquenta seis",
			"quatrocentos e ciquenta sete",
			"quatrocentos e ciquenta oito",
			"quatrocentos e ciquenta nove",
			"quatrocentos e sessenta",
			"quatrocentos e sessenta um",
			"quatrocentos e sessenta dois",
			"quatrocentos e sessenta três",
			"quatrocentos e sessenta quatro",
			"quatrocentos e sessenta cinco",
			"quatrocentos e sessenta seis",
			"quatrocentos e sessenta sete",
			"quatrocentos e sessenta oito",
			"quatrocentos e sessenta nove",
			"quatrocentos e setenta",
			"quatrocentos e setenta um",
			"quatrocentos e setenta dois",
			"quatrocentos e setenta três",
			"quatrocentos e setenta quatro",
			"quatrocentos e setenta cinco",
			"quatrocentos e setenta seis",
			"quatrocentos e setenta sete",
			"quatrocentos e setenta oito",
			"quatrocentos e setenta nove",
			"quatrocentos e oitenta",
			"quatrocentos e oitenta um",
			"quatrocentos e oitenta dois",
			"quatrocentos e oitenta três",
			"quatrocentos e oitenta quatro",
			"quatrocentos e oitenta cinco",
			"quatrocentos e oitenta seis",
			"quatrocentos e oitenta sete",
			"quatrocentos e oitenta oito",
			"quatrocentos e oitenta nove",
			"quatrocentos e noventa",
			"quatrocentos e noventa um",
			"quatrocentos e noventa dois",
			"quatrocentos e noventa três",
			"quatrocentos e noventa quatro",
			"quatrocentos e noventa cinco",
			"quatrocentos e noventa seis",
			"quatrocentos e noventa sete",
			"quatrocentos e noventa oito",
			"quatrocentos e noventa nove",
			"quinhentos",
			"quinhentos e um",
			"quinhentos e dois",
			"quinhentos e três",
			"quinhentos e quatro",
			"quinhentos e cinco",
			"quinhentos e seis",
			"quinhentos e sete",
			"quinhentos e oito",
			"quinhentos e nove",
			"quinhentos e dez",
			"quinhentos e onze",
			"quinhentos e doze",
			"quinhentos e treze",
			"quinhentos e Catorze",
			"quinhentos e quinze",
			"quinhentos e dezesseis",
			"quinhentos e dezessete",
			"quinhentos e dezoito",
			"quinhentos e dezenove",
			"quinhentos e vinte",
			"quinhentos e vinte e um",
			"quinhentos e vinte e dois",
			"quinhentos e vinte e três",
			"quinhentos e vinte e quatro",
			"quinhentos e vinte e cinco",
			"quinhentos e vinte e seis",
			"quinhentos e vinte e sete",
			"quinhentos e vinte e oito",
			"quinhentos e vinte e nove",
			"quinhentos e trinta",
			"quinhentos e trinta e um",
			"quinhentos e trinta e dois",
			"quinhentos e trinta e três",
			"quinhentos e trinta e quatro",
			"quinhentos e trinta e cinco",
			"quinhentos e trinta e seis",
			"quinhentos e trinta e sete",
			"quinhentos e trinta e oito",
			"quinhentos e trinta e nove",
			"quinhentos e quarenta",
			"quinhentos e quarenta um",
			"quinhentos e quarenta dois",
			"quinhentos e quarenta três",
			"quinhentos e quarenta quatro",
			"quinhentos e quarenta cinco",
			"quinhentos e quarenta seis",
			"quinhentos e quarenta sete",
			"quinhentos e quarenta oito",
			"quinhentos e quarenta nove",
			"quinhentos e ciquenta",
			"quinhentos e ciquenta um",
			"quinhentos e ciquenta dois",
			"quinhentos e ciquenta três",
			"quinhentos e ciquenta quatro",
			"quinhentos e ciquenta cinco",
			"quinhentos e ciquenta seis",
			"quinhentos e ciquenta sete",
			"quinhentos e ciquenta oito",
			"quinhentos e ciquenta nove",
			"quinhentos e sessenta",
			"quinhentos e sessenta um",
			"quinhentos e sessenta dois",
			"quinhentos e sessenta três",
			"quinhentos e sessenta quatro",
			"quinhentos e sessenta cinco",
			"quinhentos e sessenta seis",
			"quinhentos e sessenta sete",
			"quinhentos e sessenta oito",
			"quinhentos e sessenta nove",
			"quinhentos e setenta",
			"quinhentos e setenta um",
			"quinhentos e setenta dois",
			"quinhentos e setenta três",
			"quinhentos e setenta quatro",
			"quinhentos e setenta cinco",
			"quinhentos e setenta seis",
			"quinhentos e setenta sete",
			"quinhentos e setenta oito",
			"quinhentos e setenta nove",
			"quinhentos e oitenta",
			"quinhentos e oitenta um",
			"quinhentos e oitenta dois",
			"quinhentos e oitenta três",
			"quinhentos e oitenta quatro",
			"quinhentos e oitenta cinco",
			"quinhentos e oitenta seis",
			"quinhentos e oitenta sete",
			"quinhentos e oitenta oito",
			"quinhentos e oitenta nove",
			"quinhentos e noventa",
			"quinhentos e noventa um",
			"quinhentos e noventa dois",
			"quinhentos e noventa três",
			"quinhentos e noventa quatro",
			"quinhentos e noventa cinco",
			"quinhentos e noventa seis",
			"quinhentos e noventa sete",
			"quinhentos e noventa oito",
			"quinhentos e noventa nove",
			"seiscentos",
			"seiscentos e um",
			"seiscentos e dois",
			"seiscentos e três",
			"seiscentos e quatro",
			"seiscentos e cinco",
			"seiscentos e seis",
			"seiscentos e sete",
			"seiscentos e oito",
			"seiscentos e nove",
			"seiscentos e dez",
			"seiscentos e onze",
			"seiscentos e doze",
			"seiscentos e treze",
			"seiscentos e Catorze",
			"seiscentos e quinze",
			"seiscentos e dezesseis",
			"seiscentos e dezessete",
			"seiscentos e dezoito",
			"seiscentos e dezenove",
			"seiscentos e vinte",
			"seiscentos e vinte e um",
			"seiscentos e vinte e dois",
			"seiscentos e vinte e três",
			"seiscentos e vinte e quatro",
			"seiscentos e vinte e cinco",
			"seiscentos e vinte e seis",
			"seiscentos e vinte e sete",
			"seiscentos e vinte e oito",
			"seiscentos e vinte e nove",
			"seiscentos e trinta",
			"seiscentos e trinta e um",
			"seiscentos e trinta e dois",
			"seiscentos e trinta e três",
			"seiscentos e trinta e quatro",
			"seiscentos e trinta e cinco",
			"seiscentos e trinta e seis",
			"seiscentos e trinta e sete",
			"seiscentos e trinta e oito",
			"seiscentos e trinta e nove",
			"seiscentos e quarenta",
			"seiscentos e quarenta um",
			"seiscentos e quarenta dois",
			"seiscentos e quarenta três",
			"seiscentos e quarenta quatro",
			"seiscentos e quarenta cinco",
			"seiscentos e quarenta seis",
			"seiscentos e quarenta sete",
			"seiscentos e quarenta oito",
			"seiscentos e quarenta nove",
			"seiscentos e ciquenta",
			"seiscentos e ciquenta um",
			"seiscentos e ciquenta dois",
			"seiscentos e ciquenta três",
			"seiscentos e ciquenta quatro",
			"seiscentos e ciquenta cinco",
			"seiscentos e ciquenta seis",
			"seiscentos e ciquenta sete",
			"seiscentos e ciquenta oito",
			"seiscentos e ciquenta nove",
			"seiscentos e sessenta",
			"seiscentos e sessenta um",
			"seiscentos e sessenta dois",
			"seiscentos e sessenta três",
			"seiscentos e sessenta quatro",
			"seiscentos e sessenta cinco",
			"seiscentos e sessenta seis",
			"seiscentos e sessenta sete",
			"seiscentos e sessenta oito",
			"seiscentos e sessenta nove",
			"seiscentos e setenta",
			"seiscentos e setenta um",
			"seiscentos e setenta dois",
			"seiscentos e setenta três",
			"seiscentos e setenta quatro",
			"seiscentos e setenta cinco",
			"seiscentos e setenta seis",
			"seiscentos e setenta sete",
			"seiscentos e setenta oito",
			"seiscentos e setenta nove",
			"seiscentos e oitenta",
			"seiscentos e oitenta um",
			"seiscentos e oitenta dois",
			"seiscentos e oitenta três",
			"seiscentos e oitenta quatro",
			"seiscentos e oitenta cinco",
			"seiscentos e oitenta seis",
			"seiscentos e oitenta sete",
			"seiscentos e oitenta oito",
			"seiscentos e oitenta nove",
			"seiscentos e noventa",
			"seiscentos e noventa um",
			"seiscentos e noventa dois",
			"seiscentos e noventa três",
			"seiscentos e noventa quatro",
			"seiscentos e noventa cinco",
			"seiscentos e noventa seis",
			"seiscentos e noventa sete",
			"seiscentos e noventa oito",
			"seiscentos e noventa nove",
			"setecentos",
			"setecentos e um",
			"setecentos e dois",
			"setecentos e três",
			"setecentos e quatro",
			"setecentos e cinco",
			"setecentos e seis",
			"setecentos e sete",
			"setecentos e oito",
			"setecentos e nove",
			"setecentos e dez",
			"setecentos e onze",
			"setecentos e doze",
			"setecentos e treze",
			"setecentos e Catorze",
			"setecentos e quinze",
			"setecentos e dezesseis",
			"setecentos e dezessete",
			"setecentos e dezoito",
			"setecentos e dezenove",
			"setecentos e vinte",
			"setecentos e vinte e um",
			"setecentos e vinte e dois",
			"setecentos e vinte e três",
			"setecentos e vinte e quatro",
			"setecentos e vinte e cinco",
			"setecentos e vinte e seis",
			"setecentos e vinte e sete",
			"setecentos e vinte e oito",
			"setecentos e vinte e nove",
			"setecentos e trinta",
			"setecentos e trinta e um",
			"setecentos e trinta e dois",
			"setecentos e trinta e três",
			"setecentos e trinta e quatro",
			"setecentos e trinta e cinco",
			"setecentos e trinta e seis",
			"setecentos e trinta e sete",
			"setecentos e trinta e oito",
			"setecentos e trinta e nove",
			"setecentos e quarenta",
			"setecentos e quarenta um",
			"setecentos e quarenta dois",
			"setecentos e quarenta três",
			"setecentos e quarenta quatro",
			"setecentos e quarenta cinco",
			"setecentos e quarenta seis",
			"setecentos e quarenta sete",
			"setecentos e quarenta oito",
			"setecentos e quarenta nove",
			"setecentos e ciquenta",
			"setecentos e ciquenta um",
			"setecentos e ciquenta dois",
			"setecentos e ciquenta três",
			"setecentos e ciquenta quatro",
			"setecentos e ciquenta cinco",
			"setecentos e ciquenta seis",
			"setecentos e ciquenta sete",
			"setecentos e ciquenta oito",
			"setecentos e ciquenta nove",
			"setecentos e sessenta",
			"setecentos e sessenta um",
			"setecentos e sessenta dois",
			"setecentos e sessenta três",
			"setecentos e sessenta quatro",
			"setecentos e sessenta cinco",
			"setecentos e sessenta seis",
			"setecentos e sessenta sete",
			"setecentos e sessenta oito",
			"setecentos e sessenta nove",
			"setecentos e setenta",
			"setecentos e setenta um",
			"setecentos e setenta dois",
			"setecentos e setenta três",
			"setecentos e setenta quatro",
			"setecentos e setenta cinco",
			"setecentos e setenta seis",
			"setecentos e setenta sete",
			"setecentos e setenta oito",
			"setecentos e setenta nove",
			"setecentos e oitenta",
			"setecentos e oitenta um",
			"setecentos e oitenta dois",
			"setecentos e oitenta três",
			"setecentos e oitenta quatro",
			"setecentos e oitenta cinco",
			"setecentos e oitenta seis",
			"setecentos e oitenta sete",
			"setecentos e oitenta oito",
			"setecentos e oitenta nove",
			"setecentos e noventa",
			"setecentos e noventa um",
			"setecentos e noventa dois",
			"setecentos e noventa três",
			"setecentos e noventa quatro",
			"setecentos e noventa cinco",
			"setecentos e noventa seis",
			"setecentos e noventa sete",
			"setecentos e noventa oito",
			"setecentos e noventa nove",
			"oitocentos",
			"oitocentos e um",
			"oitocentos e dois",
			"oitocentos e três",
			"oitocentos e quatro",
			"oitocentos e cinco",
			"oitocentos e seis",
			"oitocentos e sete",
			"oitocentos e oito",
			"oitocentos e nove",
			"oitocentos e dez",
			"oitocentos e onze",
			"oitocentos e doze",
			"oitocentos e treze",
			"oitocentos e Catorze",
			"oitocentos e quinze",
			"oitocentos e dezesseis",
			"oitocentos e dezessete",
			"oitocentos e dezoito",
			"oitocentos e dezenove",
			"oitocentos e vinte",
			"oitocentos e vinte e um",
			"oitocentos e vinte e dois",
			"oitocentos e vinte e três",
			"oitocentos e vinte e quatro",
			"oitocentos e vinte e cinco",
			"oitocentos e vinte e seis",
			"oitocentos e vinte e sete",
			"oitocentos e vinte e oito",
			"oitocentos e vinte e nove",
			"oitocentos e trinta",
			"oitocentos e trinta e um",
			"oitocentos e trinta e dois",
			"oitocentos e trinta e três",
			"oitocentos e trinta e quatro",
			"oitocentos e trinta e cinco",
			"oitocentos e trinta e seis",
			"oitocentos e trinta e sete",
			"oitocentos e trinta e oito",
			"oitocentos e trinta e nove",
			"oitocentos e quarenta",
			"oitocentos e quarenta um",
			"oitocentos e quarenta dois",
			"oitocentos e quarenta três",
			"oitocentos e quarenta quatro",
			"oitocentos e quarenta cinco",
			"oitocentos e quarenta seis",
			"oitocentos e quarenta sete",
			"oitocentos e quarenta oito",
			"oitocentos e quarenta nove",
			"oitocentos e ciquenta",
			"oitocentos e ciquenta um",
			"oitocentos e ciquenta dois",
			"oitocentos e ciquenta três",
			"oitocentos e ciquenta quatro",
			"oitocentos e ciquenta cinco",
			"oitocentos e ciquenta seis",
			"oitocentos e ciquenta sete",
			"oitocentos e ciquenta oito",
			"oitocentos e ciquenta nove",
			"oitocentos e sessenta",
			"oitocentos e sessenta um",
			"oitocentos e sessenta dois",
			"oitocentos e sessenta três",
			"oitocentos e sessenta quatro",
			"oitocentos e sessenta cinco",
			"oitocentos e sessenta seis",
			"oitocentos e sessenta sete",
			"oitocentos e sessenta oito",
			"oitocentos e sessenta nove",
			"oitocentos e setenta",
			"oitocentos e setenta um",
			"oitocentos e setenta dois",
			"oitocentos e setenta três",
			"oitocentos e setenta quatro",
			"oitocentos e setenta cinco",
			"oitocentos e setenta seis",
			"oitocentos e setenta sete",
			"oitocentos e setenta oito",
			"oitocentos e setenta nove",
			"oitocentos e oitenta",
			"oitocentos e oitenta um",
			"oitocentos e oitenta dois",
			"oitocentos e oitenta três",
			"oitocentos e oitenta quatro",
			"oitocentos e oitenta cinco",
			"oitocentos e oitenta seis",
			"oitocentos e oitenta sete",
			"oitocentos e oitenta oito",
			"oitocentos e oitenta nove",
			"oitocentos e noventa",
			"oitocentos e noventa um",
			"oitocentos e noventa dois",
			"oitocentos e noventa três",
			"oitocentos e noventa quatro",
			"oitocentos e noventa cinco",
			"oitocentos e noventa seis",
			"oitocentos e noventa sete",
			"oitocentos e noventa oito",
			"oitocentos e noventa nove",
			"novecentos",
			"novecentos e um",
			"novecentos e dois",
			"novecentos e três",
			"novecentos e quatro",
			"novecentos e cinco",
			"novecentos e seis",
			"novecentos e sete",
			"novecentos e oito",
			"novecentos e nove",
			"novecentos e dez",
			"novecentos e onze",
			"novecentos e doze",
			"novecentos e treze",
			"novecentos e Catorze",
			"novecentos e quinze",
			"novecentos e dezesseis",
			"novecentos e dezessete",
			"novecentos e dezoito",
			"novecentos e dezenove",
			"novecentos e vinte",
			"novecentos e vinte e um",
			"novecentos e vinte e dois",
			"novecentos e vinte e três",
			"novecentos e vinte e quatro",
			"novecentos e vinte e cinco",
			"novecentos e vinte e seis",
			"novecentos e vinte e sete",
			"novecentos e vinte e oito",
			"novecentos e vinte e nove",
			"novecentos e trinta",
			"novecentos e trinta e um",
			"novecentos e trinta e dois",
			"novecentos e trinta e três",
			"novecentos e trinta e quatro",
			"novecentos e trinta e cinco",
			"novecentos e trinta e seis",
			"novecentos e trinta e sete",
			"novecentos e trinta e oito",
			"novecentos e trinta e nove",
			"novecentos e quarenta",
			"novecentos e quarenta um",
			"novecentos e quarenta dois",
			"novecentos e quarenta três",
			"novecentos e quarenta quatro",
			"novecentos e quarenta cinco",
			"novecentos e quarenta seis",
			"novecentos e quarenta sete",
			"novecentos e quarenta oito",
			"novecentos e quarenta nove",
			"novecentos e ciquenta",
			"novecentos e ciquenta um",
			"novecentos e ciquenta dois",
			"novecentos e ciquenta três",
			"novecentos e ciquenta quatro",
			"novecentos e ciquenta cinco",
			"novecentos e ciquenta seis",
			"novecentos e ciquenta sete",
			"novecentos e ciquenta oito",
			"novecentos e ciquenta nove",
			"novecentos e sessenta",
			"novecentos e sessenta um",
			"novecentos e sessenta dois",
			"novecentos e sessenta três",
			"novecentos e sessenta quatro",
			"novecentos e sessenta cinco",
			"novecentos e sessenta seis",
			"novecentos e sessenta sete",
			"novecentos e sessenta oito",
			"novecentos e sessenta nove",
			"novecentos e setenta",
			"novecentos e setenta um",
			"novecentos e setenta dois",
			"novecentos e setenta três",
			"novecentos e setenta quatro",
			"novecentos e setenta cinco",
			"novecentos e setenta seis",
			"novecentos e setenta sete",
			"novecentos e setenta oito",
			"novecentos e setenta nove",
			"novecentos e oitenta",
			"novecentos e oitenta um",
			"novecentos e oitenta dois",
			"novecentos e oitenta três",
			"novecentos e oitenta quatro",
			"novecentos e oitenta cinco",
			"novecentos e oitenta seis",
			"novecentos e oitenta sete",
			"novecentos e oitenta oito",
			"novecentos e oitenta nove",
			"novecentos e noventa",
			"novecentos e noventa um",
			"novecentos e noventa dois",
			"novecentos e noventa três",
			"novecentos e noventa quatro",
			"novecentos e noventa cinco",
			"novecentos e noventa seis",
			"novecentos e noventa sete",
			"novecentos e noventa oito",
			"novecentos e noventa nove",
			"mil",
			"mil e um",
			"mil e dois",
			"mil e três",
			"mil e quatro",
			"mil e cinco",
			"mil e seis",
			"mil e sete",
			"mil e oito",
			"mil e nove",
			"mil e dez",
			"mil onze",
			"mil e doze",
			"mil e treze",
			"mil e Catorze",
			"mil e quinze",
			"mil e dezeseis",
			"mil e dezesete",
			"mil e dezoito",
			"mil e dezenove",
			"mil e vinte",
			"mil e vinte e um",
			"mil e vinte e dois",
			"mil e vinte e três",
			"mil e vinte e quatro",
			"mil e vinte e cinco",
			"mil e vinte e seis",
			"vinte e sete",
			"mil e vinte e oito",
			"mil e vinte e nove",
			"mil e trinta",
			"mil e trinta e um",
			"mil e trinta e dois",
			"mil e trinta e três",
			"mil e trinta e quatro",
			"mil e trinta e cinco",
			"mil e trinta e seis",
			"mil e trinta e sete",
			"mil e trinta e oito",
			"mil e trinta e nove",
			"mil e quarenta",
			"mil e quarenta um",
			"mil e quarenta dois",
			"mil e quarenta três",
			"mil e quarenta quatro",
			"mil e quarenta cinco",
			"mil e quarenta seis",
			"mil e quarenta sete",
			"mil e quarenta oito",
			"mil e quarenta nove",
			"mil e ciquenta",
			"mil e ciquenta um",
			"mil e ciquenta dois",
			"mil e ciquenta três",
			"mil e ciquenta quatro",
			"mil e ciquenta cinco",
			"mil e ciquenta seis",
			"mil e ciquenta sete",
			"mil e ciquenta oito",
			"mil e ciquenta nove",
			"mil e sessenta",
			"mil e sessenta um",
			"mil e sessenta dois",
			"mil e sessenta três",
			"mil e sessenta quatro",
			"mil e sessenta cinco",
			"mil e sessenta seis",
			"mil e sessenta sete",
			"mil e sessenta oito",
			"mil e sessenta nove",
			"mil e setenta",
			"mil e setenta um",
			"mil e setenta dois",
			"mil e setenta três",
			"mil e setenta quatro",
			"mil e setenta cinco",
			"mil e setenta seis",
			"mil e setenta sete",
			"mil e setenta oito",
			"mil e setenta nove",
			"mil e oitenta",
			"mil e oitenta um",
			"mil e oitenta e dois",
			"mil e oitenta três",
			"mil e oitenta quatro",
			"mil e oitenta cinco",
			"mil e oitenta seis",
			"mil e oitenta sete",
			"mil e oitenta oito",
			"mil e oitenta nove",
			"mil e noventa",
			"mil e noventa um",
			"mil e noventa dois",
			"mil e noventa três",
			"mil e noventa quatro",
			"mil e noventa cinco",
			"mil e noventa seis",
			"mil e noventa sete",
			"mil e noventa oito",
			"mil e noventa nove",
			"mil e cem",
			"mil cento e um",
			"mil cento e dois",
			"mil cento e três",
			"mil cento e quatro",
			"mil cento e cinco",
			"mil cento e seis",
			"mil cento e sete",
			"mil cento e oito",
			"mil cento e nove",
			"mil cento e dez",
			"mil cento e onze",
			"mil cento e doze",
			"mil cento e treze",
			"mil cento e quatorze ",
			"mil cento e quinze",
			"mil cento e dezesseis",
			"mil cento e dezessete",
			"mil cento e dezoito",
			"mil cento e dezenove",
			"mil cento e vinte",
			"mil cento e vinte e um",
			"mil cento e vinte e dois",
			"mil cento e vinte e três",
			"mil cento e vinte e quatro",
			"mil cento e vinte e cinco",
			"mil cento e vinte e seis",
			"mil cento e vinte e sete",
			"mil cento e vinte e oito",
			"mil cento e vinte e nove",
			"mil cento e trinta",
			"mil cento e trinta e um",
			"mil cento e trinta e dois",
			"mil cento e trinta e três",
			"mil cento e trinta e quatro",
			"mil cento e trinta e cinco",
			"mil cento e trinta e seis",
			"mil cento e trinta e sete",
			"mil cento e trinta e oito",
			"mil cento e trinta e nove",
			"mil cento e quarenta",
			"mil cento e quarenta um",
			"mil cento e quarenta dois",
			"mil cento e quarenta três",
			"mil cento e quarenta quatro",
			"mil cento e quarenta cinco",
			"mil cento e quarenta seis",
			"mil cento e quarenta sete",
			"mil cento e quarenta oito",
			"mil cento e quarenta nove",
			"mil cento e ciquenta",
			"mil cento e ciquenta um",
			"mil cento e ciquenta dois",
			"mil cento e ciquenta três",
			"mil cento e ciquenta quatro",
			"mil cento e ciquenta cinco",
			"mil cento e ciquenta seis",
			"mil cento e ciquenta sete",
			"mil cento e ciquenta oito",
			"mil cento e ciquenta nove",
			"mil cento e sessenta",
			"mil cento e sessenta um",
			"mil cento e sessenta dois",
			"mil cento e sessenta três",
			"mil cento e sessenta quatro",
			"mil cento e sessenta cinco",
			"mil cento e sessenta seis",
			"mil cento e sessenta sete",
			"mil cento e sessenta oito",
			"mil cento e sessenta nove",
			"mil cento e setenta",
			"mil cento e setenta um",
			"mil cento e setenta dois",
			"mil cento e setenta três",
			"mil cento e setenta quatro",
			"mil cento e setenta cinco",
			"mil cento e setenta seis",
			"mil cento e setenta sete",
			"mil cento e setenta oito",
			"mil cento e setenta nove",
			"mil cento e oitenta",
			"mil cento e oitenta um",
			"mil cento e oitenta dois",
			"mil cento e oitenta três",
			"mil cento e oitenta quatro",
			"mil cento e oitenta cinco",
			"mil cento e oitenta seis",
			"mil cento e oitenta sete",
			"mil cento e oitenta oito",
			"mil cento e oitenta nove",
			"mil cento e noventa",
			"mil cento e noventa um",
			"mil cento e noventa dois",
			"mil cento e noventa três",
			"mil cento e noventa quatro",
			"mil cento e noventa cinco",
			"mil cento e noventa seis",
			"mil cento e noventa sete",
			"mil cento e noventa oito",
			"mil cento e noventa nove",
			"mil e duzentos",
			"mil duzentos e um",
			"mil duzentos e dois",
			"mil duzentos e três",
			"mil duzentos e quatro",
			"mil duzentos e cinco",
			"mil duzentos e seis",
			"mil duzentos e sete",
			"mil duzentos e oito",
			"mil duzentos e nove",
			"mil duzentos e dez",
			"mil duzentos e onze",
			"mil duzentos e doze",
			"mil duzentos e treze",
			"mil duzentos e quatorze ",
			"mil duzentos e quinze",
			"mil duzentos e dezesseis",
			"mil duzentos e dezessete",
			"mil duzentos e dezoito",
			"mil duzentos e dezenove",
			"mil duzentos e vinte",
			"mil duzentos e vinte e um",
			"mil duzentos e vinte e dois",
			"mil duzentos e vinte e três",
			"mil duzentos e vinte e quatro",
			"mil duzentos e vinte e cinco",
			"mil duzentos e vinte e seis",
			"mil duzentos e vinte e sete",
			"mil duzentos e vinte e oito",
			"mil duzentos e vinte e nove",
			"mil duzentos e trinta",
			"mil duzentos e trinta e um",
			"mil duzentos e trinta e dois",
			"mil duzentos e trinta e três",
			"mil duzentos e trinta e quatro",
			"mil duzentos e trinta e cinco",
			"mil duzentos e trinta e seis",
			"mil duzentos e trinta e sete",
			"mil duzentos e trinta e oito",
			"mil duzentos e trinta e nove",
			"mil duzentos e quarenta",
			"mil duzentos e quarenta um",
			"mil duzentos e quarenta dois",
			"mil duzentos e quarenta três",
			"mil duzentos e quarenta quatro",
			"mil duzentos e quarenta cinco",
			"mil duzentos e quarenta seis",
			"mil duzentos e quarenta sete",
			"mil duzentos e quarenta oito",
			"mil duzentos e quarenta nove",
			"mil duzentos e ciquenta",
			"mil duzentos e ciquenta um",
			"mil duzentos e ciquenta dois",
			"mil duzentos e ciquenta três",
			"mil duzentos e ciquenta quatro",
			"mil duzentos e ciquenta cinco",
			"mil duzentos e ciquenta seis",
			"mil duzentos e ciquenta sete",
			"mil duzentos e ciquenta oito",
			"mil duzentos e ciquenta nove",
			"mil duzentos e sessenta",
			"mil duzentos e sessenta um",
			"mil duzentos e sessenta dois",
			"mil duzentos e sessenta três",
			"mil duzentos e sessenta quatro",
			"mil duzentos e sessenta cinco",
			"mil duzentos e sessenta seis",
			"mil duzentos e sessenta sete",
			"mil duzentos e sessenta oito",
			"mil duzentos e sessenta nove",
			"mil duzentos e setenta",
			"mil duzentos e setenta um",
			"mil duzentos e setenta dois",
			"mil duzentos e setenta três",
			"mil duzentos e setenta quatro",
			"mil duzentos e setenta cinco",
			"mil duzentos e setenta seis",
			"mil duzentos e setenta sete",
			"mil duzentos e setenta oito",
			"mil duzentos e setenta nove",
			"mil duzentos e oitenta",
			"mil duzentos e oitenta um",
			"mil duzentos e oitenta dois",
			"mil duzentos e oitenta três",
			"mil duzentos e oitenta quatro",
			"mil duzentos e oitenta cinco",
			"mil duzentos e oitenta seis",
			"mil duzentos e oitenta sete",
			"mil duzentos e oitenta oito",
			"mil duzentos e oitenta nove",
			"mil duzentos e noventa",
			"mil duzentos e noventa um",
			"mil duzentos e noventa dois",
			"mil duzentos e noventa três",
			"mil duzentos e noventa quatro",
			"mil duzentos e noventa cinco",
			"mil duzentos e noventa seis",
			"mil duzentos e noventa sete",
			"mil duzentos e noventa oito",
			"mil duzentos e noventa nove",
			"mil trezentos",
			"mil trezentos e um",
			"mil trezentos e dois",
			"mil trezentos e três",
			"mil trezentos e quatro",
			"mil trezentos e cinco",
			"mil trezentos e seis",
			"mil trezentos e sete",
			"mil trezentos e oito",
			"mil trezentos e nove",
			"mil trezentos e dez",
			"mil trezentos e onze",
			"mil trezentos e doze",
			"mil trezentos e treze",
			"mil trezentos e Catorze",
			"mil trezentos e quinze",
			"mil trezentos e dezesseis",
			"mil trezentos e dezessete",
			"mil trezentos e dezoito",
			"mil trezentos e dezenove",
			"mil trezentos e vinte",
			"mil trezentos e vinte e um",
			"mil trezentos e vinte e dois",
			"mil trezentos e vinte e três",
			"mil trezentos e vinte e quatro",
			"mil trezentos e vinte e cinco",
			"mil trezentos e vinte e seis",
			"mil trezentos e vinte e sete",
			"mil trezentos e vinte e oito",
			"mil trezentos e vinte e nove",
			"mil trezentos e trinta",
			"mil trezentos e trinta e um",
			"mil trezentos e trinta e dois",
			"mil trezentos e trinta e três",
			"mil trezentos e trinta e quatro",
			"mil trezentos e trinta e cinco",
			"mil trezentos e trinta e seis",
			"mil trezentos e trinta e sete",
			"mil trezentos e trinta e oito",
			"mil trezentos e trinta e nove",
			"mil trezentos e quarenta",
			"mil trezentos e quarenta um",
			"mil trezentos e quarenta dois",
			"mil trezentos e quarenta três",
			"mil trezentos e quarenta quatro",
			"mil trezentos e quarenta cinco",
			"mil trezentos e quarenta seis",
			"mil trezentos e quarenta sete",
			"mil trezentos e quarenta oito",
			"mil trezentos e quarenta nove",
			"mil trezentos e ciquenta",
			"mil trezentos e ciquenta um",
			"mil trezentos e ciquenta dois",
			"mil trezentos e ciquenta três",
			"mil trezentos e ciquenta quatro",
			"mil trezentos e ciquenta cinco",
			"mil trezentos e ciquenta seis",
			"mil trezentos e ciquenta sete",
			"mil trezentos e ciquenta oito",
			"mil trezentos e ciquenta nove",
			"mil trezentos e sessenta",
			"mil trezentos e sessenta um",
			"mil trezentos e sessenta dois",
			"mil trezentos e sessenta três",
			"mil trezentos e sessenta quatro",
			"mil trezentos e sessenta cinco",
			"mil trezentos e sessenta seis",
			"mil trezentos e sessenta sete",
			"mil trezentos e sessenta oito",
			"mil trezentos e sessenta nove",
			"mil trezentos e setenta",
			"mil trezentos e setenta um",
			"mil trezentos e setenta dois",
			"mil trezentos e setenta três",
			"mil trezentos e setenta quatro",
			"mil trezentos e setenta cinco",
			"mil trezentos e setenta seis",
			"mil trezentos e setenta sete",
			"mil trezentos e setenta oito",
			"mil trezentos e setenta nove",
			"mil trezentos e oitenta",
			"mil trezentos e oitenta um",
			"mil trezentos e oitenta dois",
			"mil trezentos e oitenta três",
			"mil trezentos e oitenta quatro",
			"mil trezentos e oitenta cinco",
			"mil trezentos e oitenta seis",
			"mil trezentos e oitenta sete",
			"mil trezentos e oitenta oito",
			"mil trezentos e oitenta nove",
			"mil trezentos e noventa",
			"mil trezentos e noventa um",
			"mil trezentos e noventa dois",
			"mil trezentos e noventa três",
			"mil trezentos e noventa quatro",
			"mil trezentos e noventa cinco",
			"mil trezentos e noventa seis",
			"mil trezentos e noventa sete",
			"mil trezentos e noventa oito",
			"mil trezentos e noventa nove",
			"mil quatrocentos",
			"mil quatrocentos e um",
			"mil quatrocentos e dois",
			"mil quatrocentos e três",
			"mil quatrocentos e quatro",
			"mil quatrocentos e cinco",
			"mil quatrocentos e seis",
			"mil quatrocentos e sete",
			"mil quatrocentos e oito",
			"mil quatrocentos e nove",
			"mil quatrocentos e dez",
			"mil quatrocentos e onze",
			"mil quatrocentos e doze",
			"mil quatrocentos e treze",
			"mil quatrocentos e Catorze",
			"mil quatrocentos e quinze",
			"mil quatrocentos e dezesseis",
			"mil quatrocentos e dezessete",
			"mil quatrocentos e dezoito",
			"mil quatrocentos e dezenove",
			"mil quatrocentos e vinte",
			"mil quatrocentos e vinte e um",
			"mil quatrocentos e vinte e dois",
			"mil quatrocentos e vinte e três",
			"mil quatrocentos e vinte e quatro",
			"mil quatrocentos e vinte e cinco",
			"mil quatrocentos e vinte e seis",
			"mil quatrocentos e vinte e sete",
			"mil quatrocentos e vinte e oito",
			"mil quatrocentos e vinte e nove",
			"mil quatrocentos e trinta",
			"mil quatrocentos e trinta e um",
			"mil quatrocentos e trinta e dois",
			"mil quatrocentos e trinta e três",
			"mil quatrocentos e trinta e quatro",
			"mil quatrocentos e trinta e cinco",
			"mil quatrocentos e trinta e seis",
			"mil quatrocentos e trinta e sete",
			"mil quatrocentos e trinta e oito",
			"mil quatrocentos e trinta e nove",
			"mil quatrocentos e quarenta",
			"mil quatrocentos e quarenta um",
			"mil quatrocentos e quarenta dois",
			"mil quatrocentos e quarenta três",
			"mil quatrocentos e quarenta quatro",
			"mil quatrocentos e quarenta cinco",
			"mil quatrocentos e quarenta seis",
			"mil quatrocentos e quarenta sete",
			"mil quatrocentos e quarenta oito",
			"mil quatrocentos e quarenta nove",
			"mil quatrocentos e ciquenta",
			"mil quatrocentos e ciquenta um",
			"mil quatrocentos e ciquenta dois",
			"mil quatrocentos e ciquenta três",
			"mil quatrocentos e ciquenta quatro",
			"mil quatrocentos e ciquenta cinco",
			"mil quatrocentos e ciquenta seis",
			"mil quatrocentos e ciquenta sete",
			"mil quatrocentos e ciquenta oito",
			"mil quatrocentos e ciquenta nove",
			"mil quatrocentos e sessenta",
			"mil quatrocentos e sessenta um",
			"mil quatrocentos e sessenta dois",
			"mil quatrocentos e sessenta três",
			"mil quatrocentos e sessenta quatro",
			"mil quatrocentos e sessenta cinco",
			"mil quatrocentos e sessenta seis",
			"mil quatrocentos e sessenta sete",
			"mil quatrocentos e sessenta oito",
			"mil quatrocentos e sessenta nove",
			"mil quatrocentos e setenta",
			"mil quatrocentos e setenta um",
			"mil quatrocentos e setenta dois",
			"mil quatrocentos e setenta três",
			"mil quatrocentos e setenta quatro",
			"mil quatrocentos e setenta cinco",
			"mil quatrocentos e setenta seis",
			"mil quatrocentos e setenta sete",
			"mil quatrocentos e setenta oito",
			"mil quatrocentos e setenta nove",
			"mil quatrocentos e oitenta",
			"mil quatrocentos e oitenta um",
			"mil quatrocentos e oitenta dois",
			"mil quatrocentos e oitenta três",
			"mil quatrocentos e oitenta quatro",
			"mil quatrocentos e oitenta cinco",
			"mil quatrocentos e oitenta seis",
			"mil quatrocentos e oitenta sete",
			"mil quatrocentos e oitenta oito",
			"mil quatrocentos e oitenta nove",
			"mil quatrocentos e noventa",
			"mil quatrocentos e noventa um",
			"mil quatrocentos e noventa dois",
			"mil quatrocentos e noventa três",
			"mil quatrocentos e noventa quatro",
			"mil quatrocentos e noventa cinco",
			"mil quatrocentos e noventa seis",
			"mil quatrocentos e noventa sete",
			"mil quatrocentos e noventa oito",
			"mil quatrocentos e noventa nove",
			"mil quinhentos",
			"mil quinhentos e um",
			"mil quinhentos e dois",
			"mil quinhentos e três",
			"mil quinhentos e quatro",
			"mil quinhentos e cinco",
			"mil quinhentos e seis",
			"mil quinhentos e sete",
			"mil quinhentos e oito",
			"mil quinhentos e nove",
			"mil quinhentos e dez",
			"mil quinhentos e onze",
			"mil quinhentos e doze",
			"mil quinhentos e treze",
			"mil quinhentos e Catorze",
			"mil quinhentos e quinze",
			"mil quinhentos e dezesseis",
			"mil quinhentos e dezessete",
			"mil quinhentos e dezoito",
			"mil quinhentos e dezenove",
			"mil quinhentos e vinte",
			"mil quinhentos e vinte e um",
			"mil quinhentos e vinte e dois",
			"mil quinhentos e vinte e três",
			"mil quinhentos e vinte e quatro",
			"mil quinhentos e vinte e cinco",
			"mil quinhentos e vinte e seis",
			"mil quinhentos e vinte e sete",
			"mil quinhentos e vinte e oito",
			"mil quinhentos e vinte e nove",
			"mil quinhentos e trinta",
			"mil quinhentos e trinta e um",
			"mil quinhentos e trinta e dois",
			"mil quinhentos e trinta e três",
			"mil quinhentos e trinta e quatro",
			"mil quinhentos e trinta e cinco",
			"mil quinhentos e trinta e seis",
			"mil quinhentos e trinta e sete",
			"mil quinhentos e trinta e oito",
			"mil quinhentos e trinta e nove",
			"mil quinhentos e quarenta",
			"mil quinhentos e quarenta um",
			"mil quinhentos e quarenta dois",
			"mil quinhentos e quarenta três",
			"mil quinhentos e quarenta quatro",
			"mil quinhentos e quarenta cinco",
			"mil quinhentos e quarenta seis",
			"mil quinhentos e quarenta sete",
			"mil quinhentos e quarenta oito",
			"mil quinhentos e quarenta nove",
			"mil quinhentos e ciquenta",
			"mil quinhentos e ciquenta um",
			"mil quinhentos e ciquenta dois",
			"mil quinhentos e ciquenta três",
			"mil quinhentos e ciquenta quatro",
			"mil quinhentos e ciquenta cinco",
			"mil quinhentos e ciquenta seis",
			"mil quinhentos e ciquenta sete",
			"mil quinhentos e ciquenta oito",
			"mil quinhentos e ciquenta nove",
			"mil quinhentos e sessenta",
			"mil quinhentos e sessenta um",
			"mil quinhentos e sessenta dois",
			"mil quinhentos e sessenta três",
			"mil quinhentos e sessenta quatro",
			"mil quinhentos e sessenta cinco",
			"mil quinhentos e sessenta seis",
			"mil quinhentos e sessenta sete",
			"mil quinhentos e sessenta oito",
			"mil quinhentos e sessenta nove",
			"mil quinhentos e setenta",
			"mil quinhentos e setenta um",
			"mil quinhentos e setenta dois",
			"mil quinhentos e setenta três",
			"mil quinhentos e setenta quatro",
			"mil quinhentos e setenta cinco",
			"mil quinhentos e setenta seis",
			"mil quinhentos e setenta sete",
			"mil quinhentos e setenta oito",
			"mil quinhentos e setenta nove",
			"mil quinhentos e oitenta",
			"mil quinhentos e oitenta um",
			"mil quinhentos e oitenta dois",
			"mil quinhentos e oitenta três",
			"mil quinhentos e oitenta quatro",
			"mil quinhentos e oitenta cinco",
			"mil quinhentos e oitenta seis",
			"mil quinhentos e oitenta sete",
			"mil quinhentos e oitenta oito",
			"mil quinhentos e oitenta nove",
			"mil quinhentos e noventa",
			"mil quinhentos e noventa um",
			"mil quinhentos e noventa dois",
			"mil quinhentos e noventa três",
			"mil quinhentos e noventa quatro",
			"mil quinhentos e noventa cinco",
			"mil quinhentos e noventa seis",
			"mil quinhentos e noventa sete",
			"mil quinhentos e noventa oito",
			"mil quinhentos e noventa nove",
			"mil seiscentos",
			"mil seiscentos e um",
			"mil seiscentos e dois",
			"mil seiscentos e três",
			"mil seiscentos e quatro",
			"mil seiscentos e cinco",
			"mil seiscentos e seis",
			"mil seiscentos e sete",
			"mil seiscentos e oito",
			"mil seiscentos e nove",
			"mil seiscentos e dez",
			"mil seiscentos e onze",
			"mil seiscentos e doze",
			"mil seiscentos e treze",
			"mil seiscentos e Catorze",
			"mil seiscentos e quinze",
			"mil seiscentos e dezesseis",
			"mil seiscentos e dezessete",
			"mil seiscentos e dezoito",
			"mil seiscentos e dezenove",
			"mil seiscentos e vinte",
			"mil seiscentos e vinte e um",
			"mil seiscentos e vinte e dois",
			"mil seiscentos e vinte e três",
			"mil seiscentos e vinte e quatro",
			"mil seiscentos e vinte e cinco",
			"mil seiscentos e vinte e seis",
			"mil seiscentos e vinte e sete",
			"mil seiscentos e vinte e oito",
			"mil seiscentos e vinte e nove",
			"mil seiscentos e trinta",
			"mil seiscentos e trinta e um",
			"mil seiscentos e trinta e dois",
			"mil seiscentos e trinta e três",
			"mil seiscentos e trinta e quatro",
			"mil seiscentos e trinta e cinco",
			"mil seiscentos e trinta e seis",
			"mil seiscentos e trinta e sete",
			"mil seiscentos e trinta e oito",
			"mil seiscentos e trinta e nove",
			"mil seiscentos e quarenta",
			"mil seiscentos e quarenta um",
			"mil seiscentos e quarenta dois",
			"mil seiscentos e quarenta três",
			"mil seiscentos e quarenta quatro",
			"mil seiscentos e quarenta cinco",
			"mil seiscentos e quarenta seis",
			"mil seiscentos e quarenta sete",
			"mil seiscentos e quarenta oito",
			"mil seiscentos e quarenta nove",
			"mil seiscentos e ciquenta",
			"mil seiscentos e ciquenta um",
			"mil seiscentos e ciquenta dois",
			"mil seiscentos e ciquenta três",
			"mil seiscentos e ciquenta quatro",
			"mil seiscentos e ciquenta cinco",
			"mil seiscentos e ciquenta seis",
			"mil seiscentos e ciquenta sete",
			"mil seiscentos e ciquenta oito",
			"mil seiscentos e ciquenta nove",
			"mil seiscentos e sessenta",
			"mil seiscentos e sessenta um",
			"mil seiscentos e sessenta dois",
			"mil seiscentos e sessenta três",
			"mil seiscentos e sessenta quatro",
			"mil seiscentos e sessenta cinco",
			"mil seiscentos e sessenta seis",
			"mil seiscentos e sessenta sete",
			"mil seiscentos e sessenta oito",
			"mil seiscentos e sessenta nove",
			"mil seiscentos e setenta",
			"mil seiscentos e setenta um",
			"mil seiscentos e setenta dois",
			"mil seiscentos e setenta três",
			"mil seiscentos e setenta quatro",
			"mil seiscentos e setenta cinco",
			"mil seiscentos e setenta seis",
			"mil seiscentos e setenta sete",
			"mil seiscentos e setenta oito",
			"mil seiscentos e setenta nove",
			"mil seiscentos e oitenta",
			"mil seiscentos e oitenta um",
			"mil seiscentos e oitenta dois",
			"mil seiscentos e oitenta três",
			"mil seiscentos e oitenta quatro",
			"mil seiscentos e oitenta cinco",
			"mil seiscentos e oitenta seis",
			"mil seiscentos e oitenta sete",
			"mil seiscentos e oitenta oito",
			"mil seiscentos e oitenta nove",
			"mil seiscentos e noventa",
			"mil seiscentos e noventa um",
			"mil seiscentos e noventa dois",
			"mil seiscentos e noventa três",
			"mil seiscentos e noventa quatro",
			"mil seiscentos e noventa cinco",
			"mil seiscentos e noventa seis",
			"mil seiscentos e noventa sete",
			"mil seiscentos e noventa oito",
			"mil seiscentos e noventa nove",
			"mil setecentos",
			"mil setecentos e um",
			"mil setecentos e dois",
			"mil setecentos e três",
			"mil setecentos e quatro",
			"mil setecentos e cinco",
			"mil setecentos e seis",
			"mil setecentos e sete",
			"mil setecentos e oito",
			"mil setecentos e nove",
			"mil setecentos e dez",
			"mil setecentos e onze",
			"mil setecentos e doze",
			"mil setecentos e treze",
			"mil setecentos e Catorze",
			"mil setecentos e quinze",
			"mil setecentos e dezesseis",
			"mil setecentos e dezessete",
			"mil setecentos e dezoito",
			"mil setecentos e dezenove",
			"mil setecentos e vinte",
			"mil setecentos e vinte e um",
			"mil setecentos e vinte e dois",
			"mil setecentos e vinte e três",
			"mil setecentos e vinte e quatro",
			"mil setecentos e vinte e cinco",
			"mil setecentos e vinte e seis",
			"mil setecentos e vinte e sete",
			"mil setecentos e vinte e oito",
			"mil setecentos e vinte e nove",
			"mil setecentos e trinta",
			"mil setecentos e trinta e um",
			"mil setecentos e trinta e dois",
			"mil setecentos e trinta e três",
			"mil setecentos e trinta e quatro",
			"mil setecentos e trinta e cinco",
			"mil setecentos e trinta e seis",
			"mil setecentos e trinta e sete",
			"mil setecentos e trinta e oito",
			"mil setecentos e trinta e nove",
			"mil setecentos e quarenta",
			"mil setecentos e quarenta um",
			"mil setecentos e quarenta dois",
			"mil setecentos e quarenta três",
			"mil setecentos e quarenta quatro",
			"mil setecentos e quarenta cinco",
			"mil setecentos e quarenta seis",
			"mil setecentos e quarenta sete",
			"mil setecentos e quarenta oito",
			"mil setecentos e quarenta nove",
			"mil setecentos e ciquenta",
			"mil setecentos e ciquenta um",
			"mil setecentos e ciquenta dois",
			"mil setecentos e ciquenta três",
			"mil setecentos e ciquenta quatro",
			"mil setecentos e ciquenta cinco",
			"mil setecentos e ciquenta seis",
			"mil setecentos e ciquenta sete",
			"mil setecentos e ciquenta oito",
			"mil setecentos e ciquenta nove",
			"mil setecentos e sessenta",
			"mil setecentos e sessenta um",
			"mil setecentos e sessenta dois",
			"mil setecentos e sessenta três",
			"mil setecentos e sessenta quatro",
			"mil setecentos e sessenta cinco",
			"mil setecentos e sessenta seis",
			"mil setecentos e sessenta sete",
			"mil setecentos e sessenta oito",
			"mil setecentos e sessenta nove",
			"mil setecentos e setenta",
			"mil setecentos e setenta um",
			"mil setecentos e setenta dois",
			"mil setecentos e setenta três",
			"mil setecentos e setenta quatro",
			"mil setecentos e setenta cinco",
			"mil setecentos e setenta seis",
			"mil setecentos e setenta sete",
			"mil setecentos e setenta oito",
			"mil setecentos e setenta nove",
			"mil setecentos e oitenta",
			"mil setecentos e oitenta um",
			"mil setecentos e oitenta dois",
			"mil setecentos e oitenta três",
			"mil setecentos e oitenta quatro",
			"mil setecentos e oitenta cinco",
			"mil setecentos e oitenta seis",
			"mil setecentos e oitenta sete",
			"mil setecentos e oitenta oito",
			"mil setecentos e oitenta nove",
			"mil setecentos e noventa",
			"mil setecentos e noventa um",
			"mil setecentos e noventa dois",
			"mil setecentos e noventa três",
			"mil setecentos e noventa quatro",
			"mil setecentos e noventa cinco",
			"mil setecentos e noventa seis",
			"mil setecentos e noventa sete",
			"mil setecentos e noventa oito",
			"mil setecentos e noventa nove",
			"mil oitocentos",
			"mil oitocentos e um",
			"mil oitocentos e dois",
			"mil oitocentos e três",
			"mil oitocentos e quatro",
			"mil oitocentos e cinco",
			"mil oitocentos e seis",
			"mil oitocentos e sete",
			"mil oitocentos e oito",
			"mil oitocentos e nove",
			"mil oitocentos e dez",
			"mil oitocentos e onze",
			"mil oitocentos e doze",
			"mil oitocentos e treze",
			"mil oitocentos e Catorze",
			"mil oitocentos e quinze",
			"mil oitocentos e dezesseis",
			"mil oitocentos e dezessete",
			"mil oitocentos e dezoito",
			"mil oitocentos e dezenove",
			"mil oitocentos e vinte",
			"mil oitocentos e vinte e um",
			"mil oitocentos e vinte e dois",
			"mil oitocentos e vinte e três",
			"mil oitocentos e vinte e quatro",
			"mil oitocentos e vinte e cinco",
			"mil oitocentos e vinte e seis",
			"mil oitocentos e vinte e sete",
			"mil oitocentos e vinte e oito",
			"mil oitocentos e vinte e nove",
			"mil oitocentos e trinta",
			"mil oitocentos e trinta e um",
			"mil oitocentos e trinta e dois",
			"mil oitocentos e trinta e três",
			"mil oitocentos e trinta e quatro",
			"mil oitocentos e trinta e cinco",
			"mil oitocentos e trinta e seis",
			"mil oitocentos e trinta e sete",
			"mil oitocentos e trinta e oito",
			"mil oitocentos e trinta e nove",
			"mil oitocentos e quarenta",
			"mil oitocentos e quarenta um",
			"mil oitocentos e quarenta dois",
			"mil oitocentos e quarenta três",
			"mil oitocentos e quarenta quatro",
			"mil oitocentos e quarenta cinco",
			"mil oitocentos e quarenta seis",
			"mil oitocentos e quarenta sete",
			"mil oitocentos e quarenta oito",
			"mil oitocentos e quarenta nove",
			"mil oitocentos e ciquenta",
			"mil oitocentos e ciquenta um",
			"mil oitocentos e ciquenta dois",
			"mil oitocentos e ciquenta três",
			"mil oitocentos e ciquenta quatro",
			"mil oitocentos e ciquenta cinco",
			"mil oitocentos e ciquenta seis",
			"mil oitocentos e ciquenta sete",
			"mil oitocentos e ciquenta oito",
			"mil oitocentos e ciquenta nove",
			"mil oitocentos e sessenta",
			"mil oitocentos e sessenta um",
			"mil oitocentos e sessenta dois",
			"mil oitocentos e sessenta três",
			"mil oitocentos e sessenta quatro",
			"mil oitocentos e sessenta cinco",
			"mil oitocentos e sessenta seis",
			"mil oitocentos e sessenta sete",
			"mil oitocentos e sessenta oito",
			"mil oitocentos e sessenta nove",
			"mil oitocentos e setenta",
			"mil oitocentos e setenta um",
			"mil oitocentos e setenta dois",
			"mil oitocentos e setenta três",
			"mil oitocentos e setenta quatro",
			"mil oitocentos e setenta cinco",
			"mil oitocentos e setenta seis",
			"mil oitocentos e setenta sete",
			"mil oitocentos e setenta oito",
			"mil oitocentos e setenta nove",
			"mil oitocentos e oitenta",
			"mil oitocentos e oitenta um",
			"mil oitocentos e oitenta dois",
			"mil oitocentos e oitenta três",
			"mil oitocentos e oitenta quatro",
			"mil oitocentos e oitenta cinco",
			"mil oitocentos e oitenta seis",
			"mil oitocentos e oitenta sete",
			"mil oitocentos e oitenta oito",
			"mil oitocentos e oitenta nove",
			"mil oitocentos e noventa",
			"mil oitocentos e noventa um",
			"mil oitocentos e noventa dois",
			"mil oitocentos e noventa três",
			"mil oitocentos e noventa quatro",
			"mil oitocentos e noventa cinco",
			"mil oitocentos e noventa seis",
			"mil oitocentos e noventa sete",
			"mil oitocentos e noventa oito",
			"mil oitocentos e noventa nove",
			"mil novecentos",
			"mil novecentos e um",
			"mil novecentos e dois",
			"mil novecentos e três",
			"mil novecentos e quatro",
			"mil novecentos e cinco",
			"mil novecentos e seis",
			"mil novecentos e sete",
			"mil novecentos e oito",
			"mil novecentos e nove",
			"mil novecentos e dez",
			"mil novecentos e onze",
			"mil novecentos e doze",
			"mil novecentos e treze",
			"mil novecentos e Catorze",
			"mil novecentos e quinze",
			"mil novecentos e dezesseis",
			"mil novecentos e dezessete",
			"mil novecentos e dezoito",
			"mil novecentos e dezenove",
			"mil novecentos e vinte",
			"mil novecentos e vinte e um",
			"mil novecentos e vinte e dois",
			"mil novecentos e vinte e três",
			"mil novecentos e vinte e quatro",
			"mil novecentos e vinte e cinco",
			"mil novecentos e vinte e seis",
			"mil novecentos e vinte e sete",
			"mil novecentos e vinte e oito",
			"mil novecentos e vinte e nove",
			"mil novecentos e trinta",
			"mil novecentos e trinta e um",
			"mil novecentos e trinta e dois",
			"mil novecentos e trinta e três",
			"mil novecentos e trinta e quatro",
			"mil novecentos e trinta e cinco",
			"mil novecentos e trinta e seis",
			"mil novecentos e trinta e sete",
			"mil novecentos e trinta e oito",
			"mil novecentos e trinta e nove",
			"mil novecentos e quarenta",
			"mil novecentos e quarenta um",
			"mil novecentos e quarenta dois",
			"mil novecentos e quarenta três",
			"mil novecentos e quarenta quatro",
			"mil novecentos e quarenta cinco",
			"mil novecentos e quarenta seis",
			"mil novecentos e quarenta sete",
			"mil novecentos e quarenta oito",
			"mil novecentos e quarenta nove",
			"mil novecentos e ciquenta",
			"mil novecentos e ciquenta um",
			"mil novecentos e ciquenta dois",
			"mil novecentos e ciquenta três",
			"mil novecentos e ciquenta quatro",
			"mil novecentos e ciquenta cinco",
			"mil novecentos e ciquenta seis",
			"mil novecentos e ciquenta sete",
			"mil novecentos e ciquenta oito",
			"mil novecentos e ciquenta nove",
			"mil novecentos e sessenta",
			"mil novecentos e sessenta um",
			"mil novecentos e sessenta dois",
			"mil novecentos e sessenta três",
			"mil novecentos e sessenta quatro",
			"mil novecentos e sessenta cinco",
			"mil novecentos e sessenta seis",
			"mil novecentos e sessenta sete",
			"mil novecentos e sessenta oito",
			"mil novecentos e sessenta nove",
			"mil novecentos e setenta",
			"mil novecentos e setenta um",
			"mil novecentos e setenta dois",
			"mil novecentos e setenta três",
			"mil novecentos e setenta quatro",
			"mil novecentos e setenta cinco",
			"mil novecentos e setenta seis",
			"mil novecentos e setenta sete",
			"mil novecentos e setenta oito",
			"mil novecentos e setenta nove",
			"mil novecentos e oitenta",
			"mil novecentos e oitenta um",
			"mil novecentos e oitenta dois",
			"mil novecentos e oitenta três",
			"mil novecentos e oitenta quatro",
			"mil novecentos e oitenta cinco",
			"mil novecentos e oitenta seis",
			"mil novecentos e oitenta sete",
			"mil novecentos e oitenta oito",
			"mil novecentos e oitenta nove",
			"mil novecentos e noventa",
			"mil novecentos e noventa um",
			"mil novecentos e noventa dois",
			"mil novecentos e noventa três",
			"mil novecentos e noventa quatro",
			"mil novecentos e noventa cinco",
			"mil novecentos e noventa seis",
			"mil novecentos e noventa sete",
			"mil novecentos e noventa oito",
			"mil novecentos e noventa nove"
		};

		// Token: 0x04000032 RID: 50
		public string[] fraseAlarma = new string[]
		{
			"alarme as",
			"alarme para as",
			"desperta-me as",
			"por alarme as",
			"por o alarme as",
			"estabelecer alarme as",
			"estabelecer alarme para as",
			"configurar alarme as",
			"configurar alarme para as",
			"ativar alarme as",
			"ativar alarm para as",
			"programar alarme as",
			"programar alarme para as",
			Settings.Default.cfgAi + " alarme as",
			Settings.Default.cfgAi + " alarme para as",
			Settings.Default.cfgAi + " estabelecer alarmer as",
			Settings.Default.cfgAi + " estabelecer alarme para as",
			Settings.Default.cfgAi + " configurar alarme as",
			Settings.Default.cfgAi + " configurar alarme para as",
			Settings.Default.cfgAi + " ativar alarme as",
			Settings.Default.cfgAi + " ativar alarme para as",
			Settings.Default.cfgAi + " programar alarme as",
			Settings.Default.cfgAi + " programar alarme para as"
		};

		// Token: 0x04000033 RID: 51
		public string[] recoDia = new string[]
		{
			"hoje",
			"amanhã",
			"amanhã à tarde",
			"amanhã à noite",
			"tarde",
			"noite",
			"segunda",
			"segunda de manhã",
			"segunda à tarde",
			"segunda à noite",
			"terça",
			"terça de manhã",
			"Terça à tarde",
			"Terça à noite",
			"Quarta",
			"Quarta de manhã",
			"Quarta à tarde",
			"Quarta à noite",
			"Quinta",
			"Quinta de manhã",
			"Quinta à tarde",
			"Quinta à noite",
			"Sexta",
			"Sexta de manhã",
			"Sexta à tarde",
			"Sexta à noite",
			"Sábado",
			"Sábado de manhã",
			"Sábado à tarde",
			"Sábado à noite",
			"Domingo",
			"Domingo de manhã",
			"Domingo à tarde",
			"Domingo à noite"
		};

		// Token: 0x04000034 RID: 52
		public string[] Saludos = new string[]
		{
			"Bom Dia",
			"Boa tarde",
			"Boa noite"
		};

		// Token: 0x04000035 RID: 53
		public string[] frasesExtras = new string[]
		{
			"abrir tocador",
			"reproduza uma música",
			"reproduzir a faixa",
			"reproduzir música",
			"pausar a faixa",
			"pausar a música",
			"próxima faixa",
			"próxima música",
			"faixa anterior",
			"música anterior",
			"repetir a faixa",
			"repetir a música",
			"trilha muda",
			"silenciar a música",
			"silenciar",
			"cancelar",
			"confirmar",
			"parar",
			"cancelar a leitura",
			"pare de ler",
			"lá apenas",
			"lá em cima apenas",
			"não guardar",
			"aceitar",
			"okey",
			"sim",
			"não"
		};

		// Token: 0x04000036 RID: 54
		public string[] cmdDictado = new string[]
		{
			"desativar ditado",
			"arroba",
			"abrir interrogação",
			"fechar interrogação",
			"abrir exclamação",
			"fechar exclamação",
			"abrir citações",
			"fechar citações",
			"numero um",
			"numero dois",
			"numero três",
			"numero quatro",
			"numero cinco",
			"numero seis",
			"numero sete",
			"numero oito",
			"numero nove",
			"numero zero",
			"desfazer",
			"refazer",
			"selecionar tudo",
			"selecionar documento inteiro",
			"buscar",
			"procure em documento",
			"mudar fonte",
			"alterar fonte para documento",
			"novo documento",
			"abrir um novo documento",
			"fechar documento",
			"salve o documento",
			"verificar ortografia",
			"verificar ortografia no documento",
			"converter para negrito",
			"texto em negrito",
			"palavra em negrito",
			"converter para itálico",
			"texto em itálico",
			"palavra em itálico",
			"sublinhar a palavra",
			"sublinhar texto",
			"imprima o documento",
			"vista preliminar",
			"ver pré-visualização",
			"remover personagem",
			"excluir caractere",
			"remover caractere da esquerda",
			"excluir palavra",
			"excluir a palavra",
			"excluir palavra da esquerda",
			"remover o caractere da direita",
			"excluir palavra da direita",
			"inserir página",
			"pular a página",
			"nova página",
			"inserir campo",
			"novo campo",
			"mude para maiúsculas",
			"converter para minúsculas",
			"vá para a palavra anterior",
			"vá para a próxima palavra",
			"selecione a palavra anterior",
			"selecione a próxima palavra",
			"vá para o começo da linha",
			"vá para o fim da linha",
			"vá para o início do documento",
			"vá para o final do documento",
			"selecionar linha",
			"inserir nota de rodapé",
			"inserir comentário",
			"inserir hiperlink",
			"nova linha",
			"novo parágrafo",
			"pressione a guia",
			"pressione espaço",
			"criar novo documento",
			"ativar negrito",
			"ficar em itálico",
			"ativar sublinhado",
			"ativar o sublinhado",
			"desativar sublinhado",
			"subir linha",
			"descer linha",
			"voltar um caractere",
			"desfazer isso",
			"selecionar fila",
			"vá para o início da fila",
			"vá para o fim da fila",
			"voltar um espaço",
			"copiar texto",
			"colar texto",
			"recortar texto",
			"pressione enter",
			"selecione a palavra",
			"apagar isso",
			"desativar negrito",
			"desativar itálico",
			"desativar sublinhado"
		};

		// Token: 0x04000037 RID: 55
		private string[] nameAI = new string[]
		{
			Settings.Default.cfgAi,
			"até " + Settings.Default.cfgAi,
			"tchau " + Settings.Default.cfgAi,
			"até logo " + Settings.Default.cfgAi,
			"pare de me ouvir" + Settings.Default.cfgAi,
			"nos vemos " + Settings.Default.cfgAi
		};

		// Token: 0x04000038 RID: 56
		private G1NewsItem[] newsFromG1;

		// Token: 0x04000039 RID: 57
		private int newsIndex = 0;

		// Token: 0x0400003A RID: 58
		private Cuentas cont = new Cuentas();

		// Token: 0x0400003B RID: 59
		private SpeechRecognitionEngine _reco = new SpeechRecognitionEngine();

		// Token: 0x0400003C RID: 60
		private SpeechSynthesizer Fala = new SpeechSynthesizer();

		// Token: 0x0400003D RID: 61
		private RecognitionResult SpeechResult;

		// Token: 0x0400003E RID: 62
		private DispatcherTimer TimerRec = new DispatcherTimer();

		// Token: 0x0400003F RID: 63
		private NotifyIcon nicon = new NotifyIcon();

		// Token: 0x04000040 RID: 64
		private Recordatorio recd;

		// Token: 0x04000041 RID: 65
		private VentanaInfo vinfo;

		// Token: 0x04000042 RID: 66
		private Interface intf;

		// Token: 0x04000043 RID: 67
		private Comandos cmds;

		// Token: 0x04000044 RID: 68
		private Alarm alarm;

		// Token: 0x04000045 RID: 69
		private MainWindow vk;

		// Token: 0x04000046 RID: 70
		private Datos dts;

		// Token: 0x04000047 RID: 71
		private MediaPlay mpl;

		// Token: 0x04000048 RID: 72
		private MovieData md;

		// Token: 0x04000049 RID: 73
		private string speech;

		// Token: 0x0400004A RID: 74
		private string cfgUser;

		// Token: 0x0400004B RID: 75
		private Process[] processesR;

		// Token: 0x0400004C RID: 76
		private OleDbConnection con;

		// Token: 0x0400004D RID: 77
		private OleDbConnection cone;

		// Token: 0x0400004E RID: 78
		private int iHandleR;

		// Token: 0x0400004F RID: 79
		private IntPtr HandleID = (IntPtr)0;

		// Token: 0x04000050 RID: 80
		private bool alarmaSet = false;

		// Token: 0x04000051 RID: 81
		private string EstadoRec = "";

		// Token: 0x04000052 RID: 82
		private string Capture = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\CapturaAv.png";

		// Token: 0x04000053 RID: 83
		private int LoadTimerRecOff = 0;

		// Token: 0x04000054 RID: 84
		private bool LoadReponder = true;

		// Token: 0x04000055 RID: 85
		private bool LoadCbOpacidad = false;

		// Token: 0x04000056 RID: 86
		private bool LoadCmdSearch = true;

		// Token: 0x04000057 RID: 87
		private bool LoadCmdCal = true;

		// Token: 0x04000058 RID: 88
		private bool LoadCmdAlar = true;

		// Token: 0x04000059 RID: 89
		private bool LoadtecladoOn = true;

		// Token: 0x0400005A RID: 90
		private string estado;

		// Token: 0x0400005B RID: 91
		private bool activeSerialPort = false;

		// Token: 0x0400005C RID: 92
		private string Ejecutar;

		// Token: 0x0400005D RID: 93
		private string Respuesta;

		// Token: 0x0400005E RID: 94
		private string[] multiRespuesta;

		// Token: 0x0400005F RID: 95
		private string[] confirmarCmd;

		// Token: 0x04000060 RID: 96
		private bool cmdConfirmado = false;

		// Token: 0x04000061 RID: 97
		private bool completarBusqueda = false;

		// Token: 0x04000062 RID: 98
		private bool EnableDictado = false;

		// Token: 0x04000063 RID: 99
		private bool lecturaEnable = false;

		// Token: 0x04000064 RID: 100
		private bool EnableShutdown = false;

		// Token: 0x04000065 RID: 101
		private bool ventanaOpen = false;

		// Token: 0x04000066 RID: 102
		private int cuentas = 0;

		// Token: 0x04000067 RID: 103
		private string palabraTemporalBusqueda = string.Empty;

		// Token: 0x04000068 RID: 104
		private string nuevaFraseU = string.Empty;

		// Token: 0x04000069 RID: 105
		private bool clicSlider = false;

		// Token: 0x0400006A RID: 106
		private readonly DispatcherTimer Timerlbl = new DispatcherTimer();

		// Token: 0x0400006B RID: 107
		private DispatcherTimer Timer = new DispatcherTimer();

		// Token: 0x0400006C RID: 108
		private DispatcherTimer Timer2 = new DispatcherTimer();

		// Token: 0x0400006D RID: 109
		private DispatcherTimer Timer3 = new DispatcherTimer();

		// Token: 0x0400006E RID: 110
		private DispatcherTimer TimerFlip = new DispatcherTimer();

		// Token: 0x0400006F RID: 111
		private DispatcherTimer TimerImap = new DispatcherTimer();

		// Token: 0x04000070 RID: 112
		private readonly DispatcherTimer TimerMicroOff = new DispatcherTimer();

		// Token: 0x04000071 RID: 113
		private DispatcherTimer TimerAviso = new DispatcherTimer();

		// Token: 0x04000072 RID: 114
		private DispatcherTimer TimerSetAlarm = new DispatcherTimer();

		// Token: 0x04000073 RID: 115
		private ImapClient clientImapG;

		// Token: 0x04000074 RID: 116
		private ImapClient clientImapO;

		// Token: 0x04000075 RID: 117
		private ImapClient clientImapY;

		// Token: 0x04000076 RID: 118
		private ImapClient clientImapE;

		// Token: 0x04000077 RID: 119
		private int contadorImap = 0;

		// Token: 0x04000078 RID: 120
		private int contadorNotificaciones = 0;

		// Token: 0x04000079 RID: 121
		private int contadorTimer = 10;

		// Token: 0x0400007A RID: 122
		private int TimerRecOFF = 0;

		// Token: 0x0400007B RID: 123
		private int contadorTimer3 = 110;

		// Token: 0x0400007C RID: 124
		private int cantidadCorreos = 0;

		// Token: 0x0400007D RID: 125
		private int cantidadAnteriorCorreos = 0;

		// Token: 0x0400007E RID: 126
		private bool bollCantidadAnterioresCorreos = false;

		// Token: 0x0400007F RID: 127
		private bool ShutdownTimer = false;

		// Token: 0x04000080 RID: 128
		private bool lecturaCorreo = false;

		// Token: 0x04000081 RID: 129
		private bool lecturaFacebook = false;

		// Token: 0x04000082 RID: 130
		private bool CargaDeFacebook = false;

		// Token: 0x04000083 RID: 131
		private bool CargaDecorreo = false;

		// Token: 0x04000084 RID: 132
		private bool condicionB = false;

		// Token: 0x04000085 RID: 133
		private bool google = false;

		// Token: 0x04000086 RID: 134
		private bool youtube = false;

		// Token: 0x04000087 RID: 135
		private bool yahoo = false;

		// Token: 0x04000088 RID: 136
		private bool wikipedia = false;

		// Token: 0x04000089 RID: 137
		private bool facebook = false;

		// Token: 0x0400008A RID: 138
		private bool twitter = false;

		// Token: 0x0400008B RID: 139
		private bool speechGmailTrue = false;

		// Token: 0x0400008C RID: 140
		private bool speechOutloockTrue = false;

		// Token: 0x0400008D RID: 141
		private bool speechEmailTrue = false;

		// Token: 0x0400008E RID: 142
		private bool speechYahooTrue = false;

		// Token: 0x0400008F RID: 143
		private bool checkGmail = false;

		// Token: 0x04000090 RID: 144
		private bool checkOutlook = false;

		// Token: 0x04000091 RID: 145
		private bool checkEmail = false;

		// Token: 0x04000092 RID: 146
		private bool checkYahoo = false;

		// Token: 0x04000093 RID: 147
		private List<string> MsgList = new List<string>();

		// Token: 0x04000094 RID: 148
		private List<string> MsgLink = new List<string>();

		// Token: 0x04000095 RID: 149
		private List<string> MsgListF = new List<string>();

		// Token: 0x04000096 RID: 150
		private List<string> MsgLinkF = new List<string>();

		// Token: 0x04000097 RID: 151
		private List<string> pubDateF = new List<string>();

		// Token: 0x04000098 RID: 152
		private List<string> MsgListInfo = new List<string>();

		// Token: 0x04000099 RID: 153
		private List<string> MsgLinkInfo = new List<string>();

		// Token: 0x0400009A RID: 154
		private List<string> MsgFace = new List<string>();

		// Token: 0x0400009B RID: 155
		private List<string> MsgSpeechOutlook = new List<string>();

		// Token: 0x0400009C RID: 156
		private List<string> MsgSpeechGmail = new List<string>();

		// Token: 0x0400009D RID: 157
		private List<string> MsgSpeechYahoo = new List<string>();

		// Token: 0x0400009E RID: 158
		private List<string> MsgSpeechEmail = new List<string>();

		// Token: 0x0400009F RID: 159
		private List<string> listActivarJarvis = new List<string>();

		// Token: 0x040000A0 RID: 160
		private List<string> listActivarResp = new List<string>();

		// Token: 0x040000A1 RID: 161
		private List<string> listDesactivarJarvis = new List<string>();

		// Token: 0x040000A2 RID: 162
		private List<string> listDesactivarResp = new List<string>();

		// Token: 0x040000A3 RID: 163
		private int MsgsUnreadGmail = 0;

		// Token: 0x040000A4 RID: 164
		private int MsgsUnreadOutlook = 0;

		// Token: 0x040000A5 RID: 165
		private int MsgsUnreadYahoo = 0;

		// Token: 0x040000A6 RID: 166
		private int MsgsUnreadEmail = 0;

		// Token: 0x040000A7 RID: 167
		private int gmailNum = 0;

		// Token: 0x040000A8 RID: 168
		private int outlookNum = 0;

		// Token: 0x040000A9 RID: 169
		private int yahooNum = 0;

		// Token: 0x040000AA RID: 170
		private int emailNum = 0;

		// Token: 0x040000AB RID: 171
		private bool fromLecturaGmail = false;

		// Token: 0x040000AC RID: 172
		private bool fromLecturaOutlook = false;

		// Token: 0x040000AD RID: 173
		private bool fromLecturaYahoo = false;

		// Token: 0x040000AE RID: 174
		private bool fromLecturaEmail = false;

		// Token: 0x040000AF RID: 175
		private int EmailNum = 0;

		// Token: 0x040000B0 RID: 176
		private int FacebookNum = 0;

		// Token: 0x040000B1 RID: 177
		private int FaceMsgNum = 0;

		// Token: 0x040000B2 RID: 178
		private string nuevaFraseB;

		// Token: 0x040000B3 RID: 179
		private string nombreUsuarioSistema = SystemInformation.UserName;

		// Token: 0x040000B4 RID: 180
		private Random rnd = new Random();

		// Token: 0x040000B5 RID: 181
		private int ranNum;

		// Token: 0x040000B6 RID: 182
		private CDRecordatorio sRec;

		// Token: 0x040000B7 RID: 183
		private int totalMensages;

		// Token: 0x040000B8 RID: 184
		private int msgsRecibidos;

		// Token: 0x040000B9 RID: 185
		private string fechaExacta;

		// Token: 0x040000BA RID: 186
		private string horaExacta;

		// Token: 0x040000BB RID: 187
		private List<string> listAllRec = new List<string>();

		// Token: 0x040000BC RID: 188
		private int contadorflip = 5;

		// Token: 0x040000BD RID: 189
		private string flip = "Start";

		// Token: 0x040000BE RID: 190
		private bool confirRecord = false;

		// Token: 0x040000BF RID: 191
		private string diaR = null;

		// Token: 0x040000C0 RID: 192
		private string horaR = null;

		// Token: 0x040000C1 RID: 193
		private string textoR = null;

		// Token: 0x040000C2 RID: 194
		private bool diaDef = false;

		// Token: 0x040000C3 RID: 195
		private bool horaVer = false;

		// Token: 0x040000C4 RID: 196
		private bool enableSerial = false;

		// Token: 0x040000C5 RID: 197
		private bool setHoraR = false;

		// Token: 0x040000C6 RID: 198
		private int tiempoAviso = 0;

		// Token: 0x040000C7 RID: 199
		private Gramaticas gr = new Gramaticas();

		// Token: 0x040000C8 RID: 200
		private string NAME_TABLA_SOCIALES = "ComandosSociales";

		// Token: 0x040000C9 RID: 201
		private string NAME_TABLA_CARPETAS = "ComandosCarpetas";

		// Token: 0x040000CA RID: 202
		private string NAME_TABLA_APLICACIONES = "ComandosAplicaciones";

		// Token: 0x040000CB RID: 203
		private string NAME_TABLA_WEBS = "ComandosPaginasWebs";

		// Token: 0x040000CC RID: 204
		private string NAME_TABLA_DEFAULT = "ComandosDefecto";

		// Token: 0x040000CD RID: 205
		private ArrayList comandosArray = new ArrayList();

		// Token: 0x040000CE RID: 206
		private ArrayList comandosEspanol1 = new ArrayList();

		// Token: 0x040000CF RID: 207
		private ArrayList comandosIngles1 = new ArrayList();

		// Token: 0x040000D0 RID: 208
		private Grammar gfrasesEx;

		// Token: 0x040000D1 RID: 209
		private Grammar gsaludos;

		// Token: 0x040000D2 RID: 210
		private Grammar gListaComandoA;

		// Token: 0x040000D3 RID: 211
		private Grammar gListaComandoC;

		// Token: 0x040000D4 RID: 212
		private Grammar gListaComandoS;

		// Token: 0x040000D5 RID: 213
		private Grammar gListaComandoP;

		// Token: 0x040000D6 RID: 214
		private Grammar gALLComandos;

		// Token: 0x040000D7 RID: 215
		private Grammar gALLComandosS;

		// Token: 0x040000D8 RID: 216
		private Grammar gSerial;

		// Token: 0x040000D9 RID: 217
		private Grammar cmdKey;

		// Token: 0x040000DA RID: 218
		private const byte VK_VOLUME_MUTE = 173;

		// Token: 0x040000DB RID: 219
		private const byte VK_VOLUME_DOWN = 174;

		// Token: 0x040000DC RID: 220
		private const byte VK_VOLUME_UP = 175;

		// Token: 0x040000DD RID: 221
		private const uint KEYEVENTF_EXTENDEDKEY = 1U;

		// Token: 0x040000DE RID: 222
		private const uint KEYEVENTF_KEYUP = 2U;

		// Token: 0x040000E5 RID: 229
		internal AVJARVIS Window;

		// Token: 0x040000E6 RID: 230
		internal Grid LayoutRoot;

		// Token: 0x040000E7 RID: 231
		internal Grid gridJarvis;

		// Token: 0x040000E8 RID: 232
		internal System.Windows.Controls.TabControl tabControle;

		// Token: 0x040000E9 RID: 233
		internal System.Windows.Controls.ProgressBar pbMicro;

		// Token: 0x040000EA RID: 234
		internal System.Windows.Controls.Label lblSpeech;

		// Token: 0x040000EB RID: 235
		internal Slider sliderRec;

		// Token: 0x040000EC RID: 236
		internal System.Windows.Shapes.Rectangle rect;

		// Token: 0x040000ED RID: 237
		internal System.Windows.Controls.Label lblPrecision;

		// Token: 0x040000EE RID: 238
		internal System.Windows.Controls.ComboBox cbNum;

		// Token: 0x040000EF RID: 239
		internal System.Windows.Controls.ComboBox cbTipo;

		// Token: 0x040000F0 RID: 240
		internal System.Windows.Controls.Label lblMicro;

		// Token: 0x040000F1 RID: 241
		internal System.Windows.Controls.Button btnPrueba;

		// Token: 0x040000F2 RID: 242
		internal TextBlock tbVersion;

		// Token: 0x040000F3 RID: 243
		internal System.Windows.Controls.Button btnDonar;

		// Token: 0x040000F4 RID: 244
		internal System.Windows.Controls.Button btnAtualiza;

		// Token: 0x040000F5 RID: 245
		internal Grid skin1;

		// Token: 0x040000F6 RID: 246
		internal Ellipse ElipseColor1;

		// Token: 0x040000F7 RID: 247
		internal Ellipse ElipseColor2;

		// Token: 0x040000F8 RID: 248
		internal Ellipse ElipseColorHud;

		// Token: 0x040000F9 RID: 249
		internal Ellipse ElipseColorHud2;

		// Token: 0x040000FA RID: 250
		internal System.Windows.Controls.Button btnJarvis;

		// Token: 0x040000FB RID: 251
		internal System.Windows.Controls.Button btnFiltro;

		// Token: 0x040000FC RID: 252
		internal Grid gridSlider;

		// Token: 0x040000FD RID: 253
		internal System.Windows.Shapes.Path anillo;

		// Token: 0x040000FE RID: 254
		internal System.Windows.Shapes.Path bar1;

		// Token: 0x040000FF RID: 255
		internal System.Windows.Shapes.Path bar2;

		// Token: 0x04000100 RID: 256
		internal System.Windows.Shapes.Path bar3;

		// Token: 0x04000101 RID: 257
		internal System.Windows.Shapes.Path bar4;

		// Token: 0x04000102 RID: 258
		internal System.Windows.Shapes.Path bar5;

		// Token: 0x04000103 RID: 259
		internal System.Windows.Shapes.Path bar6;

		// Token: 0x04000104 RID: 260
		internal System.Windows.Shapes.Path bar7;

		// Token: 0x04000105 RID: 261
		internal System.Windows.Shapes.Path bar8;

		// Token: 0x04000106 RID: 262
		internal System.Windows.Shapes.Path bar9;

		// Token: 0x04000107 RID: 263
		internal Grid skin2;

		// Token: 0x04000108 RID: 264
		internal System.Windows.Shapes.Rectangle rectSkin2;

		// Token: 0x04000109 RID: 265
		internal System.Windows.Controls.ProgressBar pbMicro2;

		// Token: 0x0400010A RID: 266
		internal System.Windows.Controls.Label lblSpeech2;

		// Token: 0x0400010B RID: 267
		internal System.Windows.Controls.Button btnJarvis2;

		// Token: 0x0400010C RID: 268
		internal System.Windows.Controls.Button btnCmd;

		// Token: 0x0400010D RID: 269
		internal Popup popupJarvis;

		// Token: 0x0400010E RID: 270
		internal System.Windows.Controls.CheckBox cbSound;

		// Token: 0x0400010F RID: 271
		internal System.Windows.Controls.CheckBox cbTopMost;

		// Token: 0x04000110 RID: 272
		internal System.Windows.Controls.Button btnClosepp;

		// Token: 0x04000111 RID: 273
		internal System.Windows.Controls.ComboBox cbskin;

		// Token: 0x04000112 RID: 274
		internal System.Windows.Controls.Button btnTraining;

		// Token: 0x04000113 RID: 275
		internal Slider sliderVolumen;

		// Token: 0x04000114 RID: 276
		internal System.Windows.Controls.Label label_Copy;

		// Token: 0x04000115 RID: 277
		internal System.Windows.Controls.CheckBox cbOpacidad;

		// Token: 0x04000116 RID: 278
		private bool _contentLoaded;
	}
}
