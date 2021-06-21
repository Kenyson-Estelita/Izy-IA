using System;
using System.Runtime.InteropServices;

namespace AVJARVISB4
{
	// Token: 0x02000014 RID: 20
	public class Win32
	{
		// Token: 0x060000FF RID: 255
		[DllImport("User32.dll")]
		public static extern int FindWindow(string strClassName, string strWindowName);

		// Token: 0x06000100 RID: 256
		[DllImport("User32.dll")]
		public static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string strClassName, string strWindowName);

		// Token: 0x06000101 RID: 257
		[DllImport("User32.dll")]
		public static extern int SendMessage(int hWnd, int Msg, int wParam, string lParam);

		// Token: 0x06000102 RID: 258
		[DllImport("User32.dll")]
		public static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

		// Token: 0x0400012D RID: 301
		public const int WM_COMMAND = 273;

		// Token: 0x0400012E RID: 302
		public const int WM_COMMAND_PROTEUS = 273;
	}
}
