using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;
using AVJARVISB4.Clases;
using AVJARVISB4.Properties;
using MailKit.Net.Imap;

namespace AVJARVISB4
{
	// Token: 0x0200001B RID: 27
	public class Cuentas : Window, IComponentConnector
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00037FC0 File Offset: 0x000361C0
		public Cuentas()
		{
			this.InitializeComponent();
			this.loadDatos();
			this.userGmail.TextChanged += new TextChangedEventHandler(this.loginGmail);
			this.passGmail.PasswordChanged += this.loginGmail;
			this.userOutlook.TextChanged += new TextChangedEventHandler(this.loginOutlook);
			this.passOutlook.PasswordChanged += this.loginOutlook;
			this.userYahoo.TextChanged += new TextChangedEventHandler(this.loginYahoo);
			this.passYahoo.PasswordChanged += this.loginYahoo;
			this.userEmail.TextChanged += new TextChangedEventHandler(this.loginEmail);
			this.passEmail.PasswordChanged += this.loginEmail;
			this.servidor.TextChanged += new TextChangedEventHandler(this.loginEmail);
			this.userChat.TextChanged += new TextChangedEventHandler(this.loginChat);
			this.passChat.PasswordChanged += this.loginChat;
			this.cbSsl.Checked += this.SSLChanged;
			this.cbSsl.Unchecked += this.SSLChanged;
			this.btnGmail.Click += this.btnGmail_Click;
			this.btnOutlook.Click += this.btnOutlook_Click;
			this.btnYahoo.Click += this.btnYahoo_Click;
			this.btnChat.Click += this.btnChat_Click;
			this.btnRss.Click += this.btnRss_Click;
			this.btnEmail.Click += this.btnEmail_Click;
			this.cbGmail.Checked += this.saveGmail;
			this.cbGmail.Unchecked += this.saveGmail;
			this.cbOutlook.Checked += this.saveOutlook;
			this.cbOutlook.Unchecked += this.saveOutlook;
			this.cbYahoo.Checked += this.saveYahoo;
			this.cbYahoo.Unchecked += this.saveYahoo;
			this.cbChat.Checked += this.saveChat;
			this.cbChat.Unchecked += this.saveChat;
			this.cbRss.Checked += this.saveRss;
			this.cbRss.Unchecked += this.saveRss;
			this.cbEmail.Checked += this.saveEmail;
			this.cbEmail.Unchecked += this.saveEmail;
			this.rutaRss.TextChanged += this.rutaRss_TextChanged;
			this.servidorGmail.DropDownClosed += this.servidorGmail_DropDownClosed;
			this.timerFace.Interval = new TimeSpan(0, 0, 1, 0);
			this.LayoutRoot.Opacity = Settings.Default.opacidad;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00002C36 File Offset: 0x00000E36
		private void btnChat_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://developers.facebook.com/docs/chat");
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00038338 File Offset: 0x00036538
		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.ClickClose_menu_hide.Play();
			base.Close();
			bool flag = this.client != null;
			if (flag)
			{
				this.client.Dispose();
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00038374 File Offset: 0x00036574
		private void btnEmail_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				SoundEffects.ShotgunShell_Land_Lino_03.Play();
				this.client = new ImapClient();
				ImapClient imapClient = this.client;
				string hostName = this.servidor.Text.Trim();
				int port = Convert.ToInt32(this.puerto.Text);
				bool cbSll = Settings.Default.cbSll;
				imapClient.Connect(hostName, port, cbSll, default(CancellationToken));
				this.client.AuthenticationMechanisms.Remove("XOAUTH");
				ImapClient imapClient2 = this.client;
				string userName = this.userEmail.Text.Trim();
				string password = this.passEmail.Password.Trim();
				imapClient2.Authenticate(userName, password, default(CancellationToken));
				MessageBox.Show("Conexão bem-sucedida", "SERVIÇO EMAIL", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				Settings.Default.cntEmailOK = true;
			}
			catch (Exception)
			{
				MessageBox.Show("Dados inválidos", "SERVIÇO EMAIL", MessageBoxButton.OK, MessageBoxImage.Hand);
				Settings.Default.cntEmailOK = false;
			}
			Settings.Default.userEmail = this.userEmail.Text.Trim();
			Settings.Default.passEmail = this.passEmail.Password.Trim();
			Settings.Default.servidorEmail = this.servidor.Text.Trim();
			Settings.Default.puertoEmail = this.puerto.Text.Trim();
			Settings.Default.Save();
			ImapClient imapClient3 = this.client;
			imapClient3.Disconnect(true, default(CancellationToken));
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00002C44 File Offset: 0x00000E44
		private void btnEnableGmail_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://www.google.com/settings/security/lesssecureapps");
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00002C52 File Offset: 0x00000E52
		private void btnEnableyahoo_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://login.yahoo.com/account/security?.scrumb=");
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00038520 File Offset: 0x00036720
		private void btnGmail_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.servidorGmail.Text == "Imap";
			if (flag)
			{
				try
				{
					SoundEffects.ShotgunShell_Land_Lino_03.Play();
					this.client = new ImapClient();
					ImapClient imapClient = this.client;
					imapClient.Connect("imap.gmail.com", 993, true, default(CancellationToken));
					this.client.AuthenticationMechanisms.Remove("XOAUTH");
					ImapClient imapClient2 = this.client;
					string userName = this.userGmail.Text.Trim();
					string password = this.passGmail.Password.Trim();
					imapClient2.Authenticate(userName, password, default(CancellationToken));
					MessageBox.Show("Conexão bem-sucedida", "SERVIÇO GMAIL", MessageBoxButton.OK, MessageBoxImage.Asterisk);
					Settings.Default.cntGmailOK = true;
					Settings.Default.cntGmailAtomOK = false;
					Settings.Default.servidorGmail = "Imap";
				}
				catch (Exception)
				{
					MessageBox.Show("Dados inválidos", "SERVIÇO GMAIL", MessageBoxButton.OK, MessageBoxImage.Hand);
					Settings.Default.cntGmailOK = false;
				}
				Settings.Default.Save();
				ImapClient imapClient3 = this.client;
				imapClient3.Disconnect(true, default(CancellationToken));
				this.client.Dispose();
			}
			else
			{
				bool flag2 = this.servidorGmail.Text == "Atom";
				if (flag2)
				{
					try
					{
						SoundEffects.ShotgunShell_Land_Lino_03.Play();
						string url = "https://mail.google.com/mail/feed/atom";
						XmlUrlResolver xmlResolver = new XmlUrlResolver
						{
							Credentials = new NetworkCredential(this.userGmail.Text.Trim(), this.passGmail.Password.Trim())
						};
						XmlTextReader reader = new XmlTextReader(url)
						{
							XmlResolver = xmlResolver
						};
						XNamespace.Get("http://purl.org/atom/ns#");
						XDocument.Load(reader);
						MessageBox.Show("Conexão bem-sucedida", "SERVIÇO GMAIL", MessageBoxButton.OK, MessageBoxImage.Asterisk);
						Settings.Default.cntGmailAtomOK = true;
						Settings.Default.cntGmailOK = false;
						Settings.Default.servidorGmail = "Atom";
					}
					catch (Exception)
					{
						MessageBox.Show("Dados inválidos", "SERVIÇO GMAIL", MessageBoxButton.OK, MessageBoxImage.Hand);
						Settings.Default.cntGmailAtomOK = false;
					}
					Settings.Default.Save();
				}
			}
			Settings.Default.userGmail = this.userGmail.Text.Trim();
			Settings.Default.passGmail = this.passGmail.Password.Trim();
			Settings.Default.Save();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000387C8 File Offset: 0x000369C8
		private void btnIcoChat_Click(object sender, RoutedEventArgs e)
		{
			this.blocCuentas.Visibility = Visibility.Hidden;
			this.gmail.Visibility = Visibility.Hidden;
			this.outlook.Visibility = Visibility.Hidden;
			this.yahoo.Visibility = Visibility.Hidden;
			this.facebookChat.Visibility = Visibility.Visible;
			this.facebookRss.Visibility = Visibility.Hidden;
			this.email.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00038834 File Offset: 0x00036A34
		private void btnIcoEmail_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.handbag_fasten_003.Play();
			this.blocCuentas.Visibility = Visibility.Hidden;
			this.gmail.Visibility = Visibility.Hidden;
			this.outlook.Visibility = Visibility.Hidden;
			this.yahoo.Visibility = Visibility.Hidden;
			this.facebookChat.Visibility = Visibility.Hidden;
			this.facebookRss.Visibility = Visibility.Hidden;
			this.email.Visibility = Visibility.Visible;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000388A8 File Offset: 0x00036AA8
		private void btnIcoGmail_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.handbag_fasten_003.Play();
			this.blocCuentas.Visibility = Visibility.Hidden;
			this.gmail.Visibility = Visibility.Visible;
			this.outlook.Visibility = Visibility.Hidden;
			this.yahoo.Visibility = Visibility.Hidden;
			this.facebookChat.Visibility = Visibility.Hidden;
			this.facebookRss.Visibility = Visibility.Hidden;
			this.email.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0003891C File Offset: 0x00036B1C
		private void btnIcoOutlook_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.handbag_fasten_003.Play();
			this.blocCuentas.Visibility = Visibility.Hidden;
			this.gmail.Visibility = Visibility.Hidden;
			this.outlook.Visibility = Visibility.Visible;
			this.yahoo.Visibility = Visibility.Hidden;
			this.facebookChat.Visibility = Visibility.Hidden;
			this.facebookRss.Visibility = Visibility.Hidden;
			this.email.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00038990 File Offset: 0x00036B90
		private void btnIcoRss_Click(object sender, RoutedEventArgs e)
		{
			this.blocCuentas.Visibility = Visibility.Hidden;
			this.gmail.Visibility = Visibility.Hidden;
			this.outlook.Visibility = Visibility.Hidden;
			this.yahoo.Visibility = Visibility.Hidden;
			this.facebookChat.Visibility = Visibility.Hidden;
			this.facebookRss.Visibility = Visibility.Visible;
			this.email.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000389FC File Offset: 0x00036BFC
		private void btnIcoYahoo_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.handbag_fasten_003.Play();
			this.blocCuentas.Visibility = Visibility.Hidden;
			this.gmail.Visibility = Visibility.Hidden;
			this.outlook.Visibility = Visibility.Hidden;
			this.yahoo.Visibility = Visibility.Visible;
			this.facebookChat.Visibility = Visibility.Hidden;
			this.facebookRss.Visibility = Visibility.Hidden;
			this.email.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00002938 File Offset: 0x00000B38
		private void btnMini_Click(object sender, RoutedEventArgs e)
		{
			SoundEffects.Hover_main_button.Play();
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00038A70 File Offset: 0x00036C70
		private void btnOutlook_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				SoundEffects.ShotgunShell_Land_Lino_03.Play();
				this.client = new ImapClient();
				ImapClient imapClient = this.client;
				imapClient.Connect("imap-mail.outlook.com", 993, true, default(CancellationToken));
				this.client.AuthenticationMechanisms.Remove("XOAUTH");
				ImapClient imapClient2 = this.client;
				string userName = this.userOutlook.Text.Trim();
				string password = this.passOutlook.Password.Trim();
				imapClient2.Authenticate(userName, password, default(CancellationToken));
				MessageBox.Show("Conexão bem-sucedida.", "SERVIÇO OUTLOOK", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				Settings.Default.cntOulookOK = true;
			}
			catch (Exception)
			{
				MessageBox.Show("Dados inválidos ", " SERVIÇO OUTLOOK", MessageBoxButton.OK, MessageBoxImage.Hand);
				Settings.Default.cntOulookOK = false;
			}
			Settings.Default.Save();
			Settings.Default.userOutlook = this.userOutlook.Text.Trim();
			Settings.Default.passOutlook = this.passOutlook.Password.Trim();
			Settings.Default.Save();
			ImapClient imapClient3 = this.client;
			imapClient3.Disconnect(true, default(CancellationToken));
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00002C60 File Offset: 0x00000E60
		private void btnRss_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://www.facebook.com/notifications");
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00038BC8 File Offset: 0x00036DC8
		private void btnYahoo_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				SoundEffects.ShotgunShell_Land_Lino_03.Play();
				this.client = new ImapClient();
				ImapClient imapClient = this.client;
				imapClient.Connect("imap.mail.yahoo.com", 993, true, default(CancellationToken));
				this.client.AuthenticationMechanisms.Remove("XOAUTH");
				ImapClient imapClient2 = this.client;
				string userName = this.userYahoo.Text.Trim();
				string password = this.passYahoo.Password.Trim();
				imapClient2.Authenticate(userName, password, default(CancellationToken));
				MessageBox.Show("Conexão bem-sucedida.", "SERVIÇO YAHOO", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				Settings.Default.cntYahooOK = true;
			}
			catch (Exception)
			{
				MessageBox.Show("Dados inválidos", "SERVIÇO YAHOO", MessageBoxButton.OK, MessageBoxImage.Hand);
				Settings.Default.cntYahooOK = false;
			}
			Settings.Default.userYahoo = this.userYahoo.Text.Trim();
			Settings.Default.passYahoo = this.passYahoo.Password.Trim();
			Settings.Default.Save();
			ImapClient imapClient3 = this.client;
			imapClient3.Disconnect(true, default(CancellationToken));
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00002C6E File Offset: 0x00000E6E
		private void CheckCanLoginChat()
		{
			this.btnChat.IsEnabled = (!string.IsNullOrEmpty(this.userChat.Text) && !string.IsNullOrEmpty(this.passChat.Password));
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00038D14 File Offset: 0x00036F14
		private void CheckCanLoginEmail()
		{
			this.btnEmail.IsEnabled = (!string.IsNullOrEmpty(this.servidor.Text) && !string.IsNullOrEmpty(this.userEmail.Text) && !string.IsNullOrEmpty(this.passEmail.Password));
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00002CA5 File Offset: 0x00000EA5
		private void CheckCanLoginGmail()
		{
			this.btnGmail.IsEnabled = (!string.IsNullOrEmpty(this.userGmail.Text) && !string.IsNullOrEmpty(this.passGmail.Password));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00002CDC File Offset: 0x00000EDC
		private void CheckCanLoginOutlook()
		{
			this.btnOutlook.IsEnabled = (!string.IsNullOrEmpty(this.userOutlook.Text) && !string.IsNullOrEmpty(this.passOutlook.Password));
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00002D13 File Offset: 0x00000F13
		private void CheckCanLoginYahoo()
		{
			this.btnYahoo.IsEnabled = (!string.IsNullOrEmpty(this.userYahoo.Text) && !string.IsNullOrEmpty(this.passYahoo.Password));
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00002D4A File Offset: 0x00000F4A
		private void EnablebutonFace()
		{
			this.btnChat.IsEnabled = true;
			this.timerFace.Stop();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00038D68 File Offset: 0x00036F68
		private void loadDatos()
		{
			SoundEffects.Somefects();
			this.cbGmail.IsChecked = new bool?(Settings.Default.checkGmail);
			this.cbOutlook.IsChecked = new bool?(Settings.Default.checkOutlook);
			this.cbYahoo.IsChecked = new bool?(Settings.Default.checkYahoo);
			this.cbEmail.IsChecked = new bool?(Settings.Default.checkEmail);
			this.cbChat.IsChecked = new bool?(Settings.Default.checkChat);
			this.cbRss.IsChecked = new bool?(Settings.Default.checkRss);
			this.userGmail.Text = Settings.Default.userGmail;
			this.passGmail.Password = Settings.Default.passGmail;
			bool flag = !(this.userGmail.Text == "") && this.passGmail.Password != "";
			if (flag)
			{
				this.btnGmail.IsEnabled = true;
			}
			this.userOutlook.Text = Settings.Default.userOutlook;
			this.passOutlook.Password = Settings.Default.passOutlook;
			bool flag2 = !(this.userOutlook.Text == "") && this.passOutlook.Password != "";
			if (flag2)
			{
				this.btnOutlook.IsEnabled = true;
			}
			this.userYahoo.Text = Settings.Default.userYahoo;
			this.passYahoo.Password = Settings.Default.passYahoo;
			bool flag3 = !(this.userYahoo.Text == "") && this.passYahoo.Password != "";
			if (flag3)
			{
				this.btnYahoo.IsEnabled = true;
			}
			this.userChat.Text = Settings.Default.userChat;
			this.passChat.Password = Settings.Default.passChat;
			bool flag4 = !(this.userChat.Text == "") && this.passChat.Password != "";
			if (flag4)
			{
				this.btnChat.IsEnabled = true;
			}
			this.rutaRss.Text = Settings.Default.rutaRss;
			this.userEmail.Text = Settings.Default.userEmail;
			this.passEmail.Password = Settings.Default.passEmail;
			bool flag5 = !(this.userEmail.Text == "") && this.passEmail.Password != "";
			if (flag5)
			{
				this.btnEmail.IsEnabled = true;
			}
			this.servidor.Text = Settings.Default.servidorEmail;
			this.puerto.Text = Settings.Default.puertoEmail;
			this.cbSsl.IsChecked = new bool?(Settings.Default.cbSll);
			this.servidorGmail.Items.Add("Atom");
			this.servidorGmail.Items.Add("Imap");
			this.servidorGmail.Text = Settings.Default.servidorGmail;
			this.servidorOutlook.Items.Add("Imap");
			this.servidorYahoo.Items.Add("Imap");
			this.servidorOutlook.Text = "Imap";
			this.servidorYahoo.Text = "Imap";
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00002D66 File Offset: 0x00000F66
		private void loginChat(object sender, RoutedEventArgs e)
		{
			this.CheckCanLoginChat();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00002D70 File Offset: 0x00000F70
		private void loginEmail(object sender, RoutedEventArgs e)
		{
			this.CheckCanLoginEmail();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00002D7A File Offset: 0x00000F7A
		private void loginGmail(object sender, RoutedEventArgs e)
		{
			this.CheckCanLoginGmail();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00002D84 File Offset: 0x00000F84
		private void loginOutlook(object sender, RoutedEventArgs e)
		{
			this.CheckCanLoginOutlook();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00002D8E File Offset: 0x00000F8E
		private void loginYahoo(object sender, RoutedEventArgs e)
		{
			this.CheckCanLoginYahoo();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00039134 File Offset: 0x00037334
		private void rutaRss_TextChanged(object sender, TextChangedEventArgs e)
		{
			Settings.Default.rutaRss = this.rutaRss.Text.Trim();
			bool flag = Settings.Default.rutaRss == null;
			if (flag)
			{
				Settings.Default.cntRssOK = false;
			}
			else
			{
				Settings.Default.cntRssOK = true;
			}
			Settings.Default.Save();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00039198 File Offset: 0x00037398
		private void saveChat(object sender, RoutedEventArgs e)
		{
			Settings.Default.checkChat = (this.cbChat.IsChecked != null && this.cbChat.IsChecked.Value);
			bool flag = Settings.Default.checkChat && !Settings.Default.cntChatOK;
			if (flag)
			{
				this.cbChat.IsChecked = new bool?(false);
				Settings.Default.checkChat = false;
				MessageBox.Show("Você deve primeiro configurar a conta e testar a conexão ", " AV - Contas", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			Settings.Default.Save();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0003923C File Offset: 0x0003743C
		private void saveEmail(object sender, RoutedEventArgs e)
		{
			Settings.Default.checkEmail = (this.cbEmail.IsChecked != null && this.cbEmail.IsChecked.Value);
			bool flag = Settings.Default.checkEmail && !Settings.Default.cntEmailOK;
			if (flag)
			{
				this.cbEmail.IsChecked = new bool?(false);
				Settings.Default.checkEmail = false;
				MessageBox.Show("Você deve primeiro configurar a conta e testar a conexão", "SERVIÇO EMAIL", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			Settings.Default.Save();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000392E0 File Offset: 0x000374E0
		private void saveGmail(object sender, RoutedEventArgs e)
		{
			Settings.Default.checkGmail = (this.cbGmail.IsChecked != null && this.cbGmail.IsChecked.Value);
			bool flag = !Settings.Default.checkGmail;
			bool flag2 = !flag && !Settings.Default.cntGmailOK && !Settings.Default.cntGmailAtomOK;
			bool flag3 = flag2;
			if (flag3)
			{
				this.cbGmail.IsChecked = new bool?(false);
				Settings.Default.checkGmail = false;
				MessageBox.Show("Você deve primeiro configurar a conta e testar a conexão", "SERVIÇO GMAIL", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			Settings.Default.Save();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000393A0 File Offset: 0x000375A0
		private void saveOutlook(object sender, RoutedEventArgs e)
		{
			Settings.Default.checkOutlook = (this.cbOutlook.IsChecked != null && this.cbOutlook.IsChecked.Value);
			bool flag = Settings.Default.checkOutlook && !Settings.Default.cntOulookOK;
			if (flag)
			{
				this.cbOutlook.IsChecked = new bool?(false);
				Settings.Default.checkOutlook = false;
				MessageBox.Show("Você deve primeiro configurar a conta e testar a conexão", "SERVIÇO OUTLOOK", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			Settings.Default.Save();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00039444 File Offset: 0x00037644
		private void saveRss(object sender, RoutedEventArgs e)
		{
			Settings.Default.checkRss = (this.cbRss.IsChecked != null && this.cbRss.IsChecked.Value);
			bool flag = Settings.Default.checkRss && !Settings.Default.cntRssOK;
			if (flag)
			{
				this.cbRss.IsChecked = new bool?(false);
				Settings.Default.checkRss = false;
				MessageBox.Show("Você deve inserir o endereço RSS primeiro", "SERVIÇO FACEBOOK", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			Settings.Default.Save();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000394E8 File Offset: 0x000376E8
		private void saveYahoo(object sender, RoutedEventArgs e)
		{
			Settings.Default.checkYahoo = (this.cbYahoo.IsChecked != null && this.cbYahoo.IsChecked.Value);
			bool flag = Settings.Default.checkYahoo && !Settings.Default.cntYahooOK;
			if (flag)
			{
				this.cbYahoo.IsChecked = new bool?(false);
				Settings.Default.checkYahoo = false;
				MessageBox.Show("Você deve primeiro configurar a conta e testar a conexão", "SERVIÇO YAHOO", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			Settings.Default.Save();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0003958C File Offset: 0x0003778C
		private void servidorGmail_DropDownClosed(object sender, EventArgs e)
		{
			Settings.Default.servidorGmail = this.servidorGmail.Text;
			Settings.Default.Save();
			this.btnGmail.IsEnabled = true;
			MessageBox.Show("Teste a conexão antes de sair de ", "SERVIÇO GMAIL", MessageBoxButton.OK, MessageBoxImage.Asterisk);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000395DC File Offset: 0x000377DC
		private void SSLChanged(object sender, RoutedEventArgs e)
		{
			Settings.Default.cbSll = (this.cbSsl.IsChecked != null && this.cbSsl.IsChecked.Value);
			Settings.Default.Save();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000020F8 File Offset: 0x000002F8
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00002D98 File Offset: 0x00000F98
		private void btnIcoGmail_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.short_med_003.Play();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00002D98 File Offset: 0x00000F98
		private void btnIcoOutlook_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.short_med_003.Play();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00002D98 File Offset: 0x00000F98
		private void btnIcoYahoo_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.short_med_003.Play();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00002D98 File Offset: 0x00000F98
		private void btnIcoEmail_MouseEnter(object sender, MouseEventArgs e)
		{
			SoundEffects.short_med_003.Play();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0003962C File Offset: 0x0003782C
		private void passGmail_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.btnGmail_Click(this, new RoutedEventArgs());
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00039658 File Offset: 0x00037858
		private void passOutlook_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.btnOutlook_Click(this, new RoutedEventArgs());
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00039684 File Offset: 0x00037884
		private void passYahoo_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.btnYahoo_Click(this, new RoutedEventArgs());
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000396B0 File Offset: 0x000378B0
		private void passEmail_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.btnEmail_Click(this, new RoutedEventArgs());
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000396DC File Offset: 0x000378DC
		private void userGmail_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.passGmail.Focus();
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00039708 File Offset: 0x00037908
		private void userOutlook_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.passOutlook.Focus();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00039734 File Offset: 0x00037934
		private void userYahoo_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.passYahoo.Focus();
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00039760 File Offset: 0x00037960
		private void userEmail_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.passEmail.Focus();
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0003978C File Offset: 0x0003798C
		private void servidor_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.puerto.Focus();
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000397B8 File Offset: 0x000379B8
		private void puerto_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.userEmail.Focus();
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000397E4 File Offset: 0x000379E4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/A V®;component/cuentas.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0003981C File Offset: 0x00037A1C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.Window = (Cuentas)target;
				this.Window.MouseLeftButtonDown += this.Window_MouseLeftButtonDown;
				break;
			case 2:
				this.aniLinea_BeginStoryboard = (BeginStoryboard)target;
				break;
			case 3:
				this.aniLinea_BeginStoryboard1 = (BeginStoryboard)target;
				break;
			case 4:
				this.aniLinea_BeginStoryboard2 = (BeginStoryboard)target;
				break;
			case 5:
				this.aniLinea_BeginStoryboard3 = (BeginStoryboard)target;
				break;
			case 6:
				this.aniLinea_BeginStoryboard4 = (BeginStoryboard)target;
				break;
			case 7:
				this.aniLinea_BeginStoryboard5 = (BeginStoryboard)target;
				break;
			case 8:
				this.aniLinea_BeginStoryboard6 = (BeginStoryboard)target;
				break;
			case 9:
				this.LayoutRoot = (Grid)target;
				break;
			case 10:
				this.Selector = (Grid)target;
				break;
			case 11:
				this.imgHud = (Image)target;
				break;
			case 12:
				this.lbl1 = (Label)target;
				break;
			case 13:
				this.lbl2 = (Label)target;
				break;
			case 14:
				this.btnClose = (Button)target;
				this.btnClose.Click += this.btnClose_Click;
				break;
			case 15:
				this.btnMini = (Button)target;
				this.btnMini.Click += this.btnMini_Click;
				break;
			case 16:
				this.btnIcoChat = (Button)target;
				this.btnIcoChat.Click += this.btnIcoChat_Click;
				break;
			case 17:
				this.btnIcoRss = (Button)target;
				this.btnIcoRss.Click += this.btnIcoRss_Click;
				break;
			case 18:
				this.btnIcoGmail = (Button)target;
				this.btnIcoGmail.Click += this.btnIcoGmail_Click;
				this.btnIcoGmail.MouseEnter += this.btnIcoGmail_MouseEnter;
				break;
			case 19:
				this.btnIcoOutlook = (Button)target;
				this.btnIcoOutlook.Click += this.btnIcoOutlook_Click;
				this.btnIcoOutlook.MouseEnter += this.btnIcoOutlook_MouseEnter;
				break;
			case 20:
				this.btnIcoYahoo = (Button)target;
				this.btnIcoYahoo.Click += this.btnIcoYahoo_Click;
				this.btnIcoYahoo.MouseEnter += this.btnIcoYahoo_MouseEnter;
				break;
			case 21:
				this.btnIcoEmail = (Button)target;
				this.btnIcoEmail.Click += this.btnIcoEmail_Click;
				this.btnIcoEmail.MouseEnter += this.btnIcoEmail_MouseEnter;
				break;
			case 22:
				this.cbGmail = (CheckBox)target;
				break;
			case 23:
				this.cbOutlook = (CheckBox)target;
				break;
			case 24:
				this.cbYahoo = (CheckBox)target;
				break;
			case 25:
				this.cbChat = (CheckBox)target;
				break;
			case 26:
				this.cbRss = (CheckBox)target;
				break;
			case 27:
				this.cbEmail = (CheckBox)target;
				break;
			case 28:
				this.recAnim = (Rectangle)target;
				break;
			case 29:
				this.blocCuentas = (TextBlock)target;
				break;
			case 30:
				this.gmail = (Grid)target;
				break;
			case 31:
				this.userGmail = (TextBox)target;
				this.userGmail.KeyDown += this.userGmail_KeyDown;
				break;
			case 32:
				this.passGmail = (PasswordBox)target;
				this.passGmail.KeyDown += this.passGmail_KeyDown;
				break;
			case 33:
				this.servidorGmail = (ComboBox)target;
				break;
			case 34:
				this.btnGmail = (Button)target;
				break;
			case 35:
				this.btnEnableGmail = (Button)target;
				this.btnEnableGmail.Click += this.btnEnableGmail_Click;
				break;
			case 36:
				this.outlook = (Grid)target;
				break;
			case 37:
				this.userOutlook = (TextBox)target;
				this.userOutlook.KeyDown += this.userOutlook_KeyDown;
				break;
			case 38:
				this.passOutlook = (PasswordBox)target;
				this.passOutlook.KeyDown += this.passOutlook_KeyDown;
				break;
			case 39:
				this.servidorOutlook = (ComboBox)target;
				break;
			case 40:
				this.btnOutlook = (Button)target;
				break;
			case 41:
				this.yahoo = (Grid)target;
				break;
			case 42:
				this.userYahoo = (TextBox)target;
				this.userYahoo.KeyDown += this.userYahoo_KeyDown;
				break;
			case 43:
				this.passYahoo = (PasswordBox)target;
				this.passYahoo.KeyDown += this.passYahoo_KeyDown;
				break;
			case 44:
				this.servidorYahoo = (ComboBox)target;
				break;
			case 45:
				this.btnYahoo = (Button)target;
				break;
			case 46:
				this.btnEnableyahoo = (Button)target;
				this.btnEnableyahoo.Click += this.btnEnableyahoo_Click;
				break;
			case 47:
				this.facebookChat = (Grid)target;
				break;
			case 48:
				this.userChat = (TextBox)target;
				break;
			case 49:
				this.passChat = (PasswordBox)target;
				break;
			case 50:
				this.btnChat = (Button)target;
				break;
			case 51:
				this.facebookRss = (Grid)target;
				break;
			case 52:
				this.rutaRss = (TextBox)target;
				break;
			case 53:
				this.btnRss = (Button)target;
				break;
			case 54:
				this.email = (Grid)target;
				break;
			case 55:
				this.userEmail = (TextBox)target;
				this.userEmail.KeyDown += this.userEmail_KeyDown;
				break;
			case 56:
				this.passEmail = (PasswordBox)target;
				this.passEmail.KeyDown += this.passEmail_KeyDown;
				break;
			case 57:
				this.btnEmail = (Button)target;
				break;
			case 58:
				this.servidor = (TextBox)target;
				this.servidor.KeyDown += this.servidor_KeyDown;
				break;
			case 59:
				this.puerto = (TextBox)target;
				this.puerto.KeyDown += this.puerto_KeyDown;
				break;
			case 60:
				this.cbSsl = (CheckBox)target;
				break;
			case 61:
				this.path = (Path)target;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x04000222 RID: 546
		private ImapClient client = null;

		// Token: 0x04000223 RID: 547
		private DispatcherTimer timerFace = new DispatcherTimer();

		// Token: 0x04000224 RID: 548
		internal Cuentas Window;

		// Token: 0x04000225 RID: 549
		internal BeginStoryboard aniLinea_BeginStoryboard;

		// Token: 0x04000226 RID: 550
		internal BeginStoryboard aniLinea_BeginStoryboard1;

		// Token: 0x04000227 RID: 551
		internal BeginStoryboard aniLinea_BeginStoryboard2;

		// Token: 0x04000228 RID: 552
		internal BeginStoryboard aniLinea_BeginStoryboard3;

		// Token: 0x04000229 RID: 553
		internal BeginStoryboard aniLinea_BeginStoryboard4;

		// Token: 0x0400022A RID: 554
		internal BeginStoryboard aniLinea_BeginStoryboard5;

		// Token: 0x0400022B RID: 555
		internal BeginStoryboard aniLinea_BeginStoryboard6;

		// Token: 0x0400022C RID: 556
		internal Grid LayoutRoot;

		// Token: 0x0400022D RID: 557
		internal Grid Selector;

		// Token: 0x0400022E RID: 558
		internal Image imgHud;

		// Token: 0x0400022F RID: 559
		internal Label lbl1;

		// Token: 0x04000230 RID: 560
		internal Label lbl2;

		// Token: 0x04000231 RID: 561
		internal Button btnClose;

		// Token: 0x04000232 RID: 562
		internal Button btnMini;

		// Token: 0x04000233 RID: 563
		internal Button btnIcoChat;

		// Token: 0x04000234 RID: 564
		internal Button btnIcoRss;

		// Token: 0x04000235 RID: 565
		internal Button btnIcoGmail;

		// Token: 0x04000236 RID: 566
		internal Button btnIcoOutlook;

		// Token: 0x04000237 RID: 567
		internal Button btnIcoYahoo;

		// Token: 0x04000238 RID: 568
		internal Button btnIcoEmail;

		// Token: 0x04000239 RID: 569
		internal CheckBox cbGmail;

		// Token: 0x0400023A RID: 570
		internal CheckBox cbOutlook;

		// Token: 0x0400023B RID: 571
		internal CheckBox cbYahoo;

		// Token: 0x0400023C RID: 572
		internal CheckBox cbChat;

		// Token: 0x0400023D RID: 573
		internal CheckBox cbRss;

		// Token: 0x0400023E RID: 574
		internal CheckBox cbEmail;

		// Token: 0x0400023F RID: 575
		internal Rectangle recAnim;

		// Token: 0x04000240 RID: 576
		internal TextBlock blocCuentas;

		// Token: 0x04000241 RID: 577
		internal Grid gmail;

		// Token: 0x04000242 RID: 578
		internal TextBox userGmail;

		// Token: 0x04000243 RID: 579
		internal PasswordBox passGmail;

		// Token: 0x04000244 RID: 580
		internal ComboBox servidorGmail;

		// Token: 0x04000245 RID: 581
		internal Button btnGmail;

		// Token: 0x04000246 RID: 582
		internal Button btnEnableGmail;

		// Token: 0x04000247 RID: 583
		internal Grid outlook;

		// Token: 0x04000248 RID: 584
		internal TextBox userOutlook;

		// Token: 0x04000249 RID: 585
		internal PasswordBox passOutlook;

		// Token: 0x0400024A RID: 586
		internal ComboBox servidorOutlook;

		// Token: 0x0400024B RID: 587
		internal Button btnOutlook;

		// Token: 0x0400024C RID: 588
		internal Grid yahoo;

		// Token: 0x0400024D RID: 589
		internal TextBox userYahoo;

		// Token: 0x0400024E RID: 590
		internal PasswordBox passYahoo;

		// Token: 0x0400024F RID: 591
		internal ComboBox servidorYahoo;

		// Token: 0x04000250 RID: 592
		internal Button btnYahoo;

		// Token: 0x04000251 RID: 593
		internal Button btnEnableyahoo;

		// Token: 0x04000252 RID: 594
		internal Grid facebookChat;

		// Token: 0x04000253 RID: 595
		internal TextBox userChat;

		// Token: 0x04000254 RID: 596
		internal PasswordBox passChat;

		// Token: 0x04000255 RID: 597
		internal Button btnChat;

		// Token: 0x04000256 RID: 598
		internal Grid facebookRss;

		// Token: 0x04000257 RID: 599
		internal TextBox rutaRss;

		// Token: 0x04000258 RID: 600
		internal Button btnRss;

		// Token: 0x04000259 RID: 601
		internal Grid email;

		// Token: 0x0400025A RID: 602
		internal TextBox userEmail;

		// Token: 0x0400025B RID: 603
		internal PasswordBox passEmail;

		// Token: 0x0400025C RID: 604
		internal Button btnEmail;

		// Token: 0x0400025D RID: 605
		internal TextBox servidor;

		// Token: 0x0400025E RID: 606
		internal TextBox puerto;

		// Token: 0x0400025F RID: 607
		internal CheckBox cbSsl;

		// Token: 0x04000260 RID: 608
		internal Path path;

		// Token: 0x04000261 RID: 609
		private bool _contentLoaded;

		// Token: 0x0200001C RID: 28
		// (Invoke) Token: 0x060001EF RID: 495
		private delegate void MyDelegado();
	}
}
