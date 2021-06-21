using System;
using System.Runtime.InteropServices;

namespace AVJARVISB4.Clases
{
	// Token: 0x0200002C RID: 44
	public class Winm
	{
		// Token: 0x0600034A RID: 842 RVA: 0x000036EC File Offset: 0x000018EC
		public static void abrirBandeja()
		{
			Winm.mciSendString("set CDAudio door open", "", 127, IntPtr.Zero);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00003706 File Offset: 0x00001906
		public static void cerrarBandeja()
		{
			Winm.mciSendString("set CDAudio door closed", "", 127, IntPtr.Zero);
		}

		// Token: 0x0600034C RID: 844
		[DllImport("winmm.dll")]
		private static extern int mciSendString(string command, string buffer, int bufferSize, IntPtr hwndCallback);
	}
}
