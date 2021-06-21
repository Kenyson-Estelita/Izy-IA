using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace AVJARVISB4.Clases
{
	// Token: 0x02000026 RID: 38
	public class User32
	{
		// Token: 0x0600031D RID: 797 RVA: 0x0003C8B4 File Offset: 0x0003AAB4
		public static void cambiarVentana()
		{
			User32.handleID = User32.GetForegroundWindow();
			User32.handleID2 = User32.handleID;
			do
			{
				User32.handleID2 = User32.GetWindow(User32.handleID2, 2U);
			}
			while (!User32.IsWindowVisible(User32.handleID2));
			User32.PostMessage(User32.handleID, 274U, 61504, 0);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0003C910 File Offset: 0x0003AB10
		public static void desktopVentanas()
		{
			User32.namesVentanas.Clear();
			User32.pVentanas.Clear();
			bool flag = User32.EnumDesktopWindows(IntPtr.Zero, delegate(IntPtr hWnd, int lParam)
			{
				StringBuilder stringBuilder = new StringBuilder(255);
				User32.GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity + 1);
				string text = stringBuilder.ToString();
				bool flag2 = User32.IsWindowVisible(hWnd) && !string.IsNullOrEmpty(text) && text != "Program Manager";
				if (flag2)
				{
					User32.pVentanas.Add(hWnd);
					User32.namesVentanas.Add(text);
				}
				return true;
			}, IntPtr.Zero);
			if (flag)
			{
				VentanaInfo ventanaInfo = new VentanaInfo();
				foreach (string newItem in User32.namesVentanas)
				{
					ventanaInfo.lbItems.Items.Add(newItem);
				}
				ventanaInfo.Show();
			}
		}

		// Token: 0x0600031F RID: 799
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool EnumDesktopWindows(IntPtr hDesktop, User32.EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

		// Token: 0x06000320 RID: 800 RVA: 0x0003C9CC File Offset: 0x0003ABCC
		private static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processID)
		{
			List<IntPtr> intPtrs = new List<IntPtr>();
			User32.EnumThreadDelegate lpfn = delegate(IntPtr hWnd, IntPtr param)
			{
				string daClassName = User32.GetDaClassName(hWnd);
				bool flag = daClassName != null;
				if (flag)
				{
					bool flag2 = daClassName == "ExploreWClass";
					if (flag2)
					{
						intPtrs.Add(hWnd);
					}
					else
					{
						bool flag3 = daClassName == "CabinetWClass";
						if (flag3)
						{
							intPtrs.Add(hWnd);
						}
					}
				}
				return true;
			};
			foreach (object obj in Process.GetProcessById(processID).Threads)
			{
				ProcessThread processThread = (ProcessThread)obj;
				User32.EnumThreadWindows(processThread.Id, lpfn, IntPtr.Zero);
			}
			return intPtrs;
		}

		// Token: 0x06000321 RID: 801
		[DllImport("user32.dll")]
		private static extern bool EnumThreadWindows(int dwThreadId, User32.EnumThreadDelegate lpfn, IntPtr lParam);

		// Token: 0x06000322 RID: 802
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x06000323 RID: 803
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

		// Token: 0x06000324 RID: 804 RVA: 0x0003CA68 File Offset: 0x0003AC68
		private static Process GetActiveProcess()
		{
			return User32.GetProcessByHandle(User32.GetForegroundWindow());
		}

		// Token: 0x06000325 RID: 805
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		// Token: 0x06000326 RID: 806 RVA: 0x0003CA84 File Offset: 0x0003AC84
		private static string GetDaClassName(IntPtr hWnd)
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			bool flag = User32.GetClassName(hWnd, stringBuilder, stringBuilder.Capacity) == 0;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000327 RID: 807
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x06000328 RID: 808 RVA: 0x0003CAC4 File Offset: 0x0003ACC4
		private static Process GetProcessByHandle(IntPtr hwnd)
		{
			Process result;
			try
			{
				uint processId;
				User32.GetWindowThreadProcessId(hwnd, out processId);
				result = Process.GetProcessById((int)processId);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000329 RID: 809
		[DllImport("user32.dll")]
		private static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);

		// Token: 0x0600032A RID: 810
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

		// Token: 0x0600032B RID: 811
		[DllImport("user32.dll")]
		private static extern int GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		// Token: 0x0600032C RID: 812
		[DllImport("user32.dll")]
		private static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x0600032D RID: 813 RVA: 0x0003CB04 File Offset: 0x0003AD04
		public static void maximizaExplorer()
		{
			User32.handleID = User32.GetForegroundWindow();
			bool flag = User32.handleID.ToInt32() != 0;
			if (flag)
			{
				User32.SendMessage(User32.handleID, 274U, 61488, 0);
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0003CB48 File Offset: 0x0003AD48
		[STAThread]
		public static IntPtr minimizarExplorer()
		{
			User32.handleID = User32.GetForegroundWindow();
			bool flag = User32.handleID.ToInt32() != 0;
			if (flag)
			{
				User32.SendMessage(User32.handleID, 274U, 61472, 0);
			}
			return User32.handleID;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0003CB94 File Offset: 0x0003AD94
		public static void minimizarVentana()
		{
			Process activeProcess = User32.GetActiveProcess();
			bool flag = activeProcess != null;
			if (flag)
			{
				User32.nameProcess = activeProcess.ProcessName;
			}
			bool flag2 = User32.nameProcess != "explorer";
			if (flag2)
			{
				User32.minimizeApp();
			}
			else
			{
				User32.minimizarExplorer();
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0003CBE4 File Offset: 0x0003ADE4
		public static void minimizeApp()
		{
			foreach (Process process in Process.GetProcesses())
			{
				bool flag = process.ProcessName == User32.nameProcess;
				if (flag)
				{
					User32.hWnd = process.MainWindowHandle.ToInt32();
					User32.ShowWindow(User32.hWnd, 6);
				}
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0003CC48 File Offset: 0x0003AE48
		public static string nombreProcesoNoExe()
		{
			Process activeProcess = User32.GetActiveProcess();
			bool flag = activeProcess != null;
			if (flag)
			{
				User32.nameProcess = activeProcess.ProcessName;
			}
			return User32.nameProcess;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0003CC7C File Offset: 0x0003AE7C
		public static bool nombreVentanaClase()
		{
			try
			{
				User32.handleID = User32.GetForegroundWindow();
				User32.handleID2 = User32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "#32770", null);
				User32.handleID3 = User32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "NUIDialog", null);
				User32.handleID4 = User32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "NativeHWNDHost", null);
				bool flag = User32.handleID == User32.handleID2;
				if (flag)
				{
					User32.GetIgualFin = true;
				}
				else
				{
					bool flag2 = User32.handleID != User32.handleID3;
					if (flag2)
					{
						User32.GetIgualFin = false;
					}
					else
					{
						User32.GetIgualFin = true;
					}
				}
			}
			catch (Exception)
			{
				User32.GetIgualFin = false;
			}
			return User32.GetIgualFin;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0003CD4C File Offset: 0x0003AF4C
		public static bool nombreVentanaCSave()
		{
			try
			{
				User32.handleID = User32.GetForegroundWindow();
				User32.handleID2 = User32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "#32770", null);
				StringBuilder stringBuilder = new StringBuilder(255);
				bool flag = User32.handleID != User32.handleID2;
				if (flag)
				{
					User32.GetIgualFin = false;
				}
				else
				{
					User32.GetIgualFin = true;
				}
			}
			catch (Exception)
			{
				User32.GetIgualFin = false;
			}
			return User32.GetIgualFin;
		}

		// Token: 0x06000334 RID: 820
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool PostMessage(IntPtr hWnd, uint msg, int WPARAM, int LPARAM);

		// Token: 0x06000335 RID: 821 RVA: 0x0003CDD8 File Offset: 0x0003AFD8
		public static void restaurarVentana(IntPtr handle)
		{
			bool flag = User32.handleID.ToInt32() != 0;
			if (flag)
			{
				User32.SendMessage(handle, 274U, 61728, 0);
			}
		}

		// Token: 0x06000336 RID: 822
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(int hWnd, int Msg, int wParam, int lParam);

		// Token: 0x06000337 RID: 823
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

		// Token: 0x06000338 RID: 824 RVA: 0x00003695 File Offset: 0x00001895
		public static void SetMonitorOFF()
		{
			User32.SendMessage(65535, 274, 61808, 2);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000036AE File Offset: 0x000018AE
		public static void SetMonitorON()
		{
			User32.SendMessage(65535, 274, 61808, -1);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000036C7 File Offset: 0x000018C7
		public static void SetMonitorSTANDBY()
		{
			User32.SendMessage(65535, 274, 61808, 1);
		}

		// Token: 0x0600033B RID: 827
		[DllImport("User32")]
		private static extern int ShowWindow(int hwnd, int nCmdShow);

		// Token: 0x040002D2 RID: 722
		private const int SC_MONITORPOWER = 61808;

		// Token: 0x040002D3 RID: 723
		private const int WM_SYSCOMMAND = 274;

		// Token: 0x040002D4 RID: 724
		private const int HWND_BROADCAST = 65535;

		// Token: 0x040002D5 RID: 725
		private const int SW_HIDE = 0;

		// Token: 0x040002D6 RID: 726
		private const int SW_SHOWNORMAL = 1;

		// Token: 0x040002D7 RID: 727
		private const int SW_NORMAL = 1;

		// Token: 0x040002D8 RID: 728
		private const int SW_SHOWMINIMIZED = 2;

		// Token: 0x040002D9 RID: 729
		private const int SW_SHOWMAXIMIZED = 3;

		// Token: 0x040002DA RID: 730
		private const int SW_MAXIMIZE = 3;

		// Token: 0x040002DB RID: 731
		private const int SW_SHOWNOACTIVATE = 4;

		// Token: 0x040002DC RID: 732
		private const int SW_SHOW = 5;

		// Token: 0x040002DD RID: 733
		private const int SW_MINIMIZE = 6;

		// Token: 0x040002DE RID: 734
		private const int SW_SHOWMINNOACTIVE = 7;

		// Token: 0x040002DF RID: 735
		private const int SW_SHOWNA = 8;

		// Token: 0x040002E0 RID: 736
		private const int SW_RESTORE = 9;

		// Token: 0x040002E1 RID: 737
		private const int SW_SHOWDEFAULT = 10;

		// Token: 0x040002E2 RID: 738
		private static string nameProcess;

		// Token: 0x040002E3 RID: 739
		private static int hWnd;

		// Token: 0x040002E4 RID: 740
		private static List<IntPtr> pVentanas = new List<IntPtr>();

		// Token: 0x040002E5 RID: 741
		private static List<string> namesVentanas = new List<string>();

		// Token: 0x040002E6 RID: 742
		private static bool GetIgualFin = false;

		// Token: 0x040002E7 RID: 743
		private const int SC_MINIMIZE = 61472;

		// Token: 0x040002E8 RID: 744
		private const int SC_MAXIMIZE = 61488;

		// Token: 0x040002E9 RID: 745
		private const int SC_RESTORE = 61728;

		// Token: 0x040002EA RID: 746
		public const int SC_NEXTWINDOW = 61504;

		// Token: 0x040002EB RID: 747
		private const int SC_PREVWINDOW = 61520;

		// Token: 0x040002EC RID: 748
		private const int SC_HOTKEY = 61776;

		// Token: 0x040002ED RID: 749
		private const int SC_CLOSE = 61536;

		// Token: 0x040002EE RID: 750
		private const int SC_HSCROLL = 61568;

		// Token: 0x040002EF RID: 751
		private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

		// Token: 0x040002F0 RID: 752
		public const uint TOPMOST_FLAGS = 3U;

		// Token: 0x040002F1 RID: 753
		private static IntPtr handleID;

		// Token: 0x040002F2 RID: 754
		private static IntPtr handleID2;

		// Token: 0x040002F3 RID: 755
		private static IntPtr handleID3;

		// Token: 0x040002F4 RID: 756
		private static IntPtr handleID4;

		// Token: 0x02000027 RID: 39
		// (Invoke) Token: 0x0600033D RID: 829
		public delegate bool EnumDelegate(IntPtr hWnd, int lParam);

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x06000341 RID: 833
		private delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

		// Token: 0x02000029 RID: 41
		private enum MonitorState
		{
			// Token: 0x040002F6 RID: 758
			ON = -1,
			// Token: 0x040002F7 RID: 759
			STANDBY = 1,
			// Token: 0x040002F8 RID: 760
			OFF
		}
	}
}
