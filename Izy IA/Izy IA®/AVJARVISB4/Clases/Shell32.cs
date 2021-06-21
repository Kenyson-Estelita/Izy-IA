using System;
using System.Runtime.InteropServices;

namespace AVJARVISB4.Clases
{
	// Token: 0x02000023 RID: 35
	public class Shell32
	{
		// Token: 0x06000317 RID: 791 RVA: 0x0003C428 File Offset: 0x0003A628
		public static void limpiarPapelera()
		{
			try
			{
				Shell32.SHEmptyRecycleBin(IntPtr.Zero, null, Shell32.RecycleFlags.SHERB_NOCONFIRMATION);
			}
			catch
			{
			}
		}

		// Token: 0x06000318 RID: 792
		[DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
		private static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, Shell32.RecycleFlags dwFlags);

		// Token: 0x02000024 RID: 36
		private enum RecycleFlags : uint
		{
			// Token: 0x040002B6 RID: 694
			SHERB_NOCONFIRMATION = 1U,
			// Token: 0x040002B7 RID: 695
			SHERB_NOPROGRESSUI,
			// Token: 0x040002B8 RID: 696
			SHERB_NOSOUND = 4U
		}
	}
}
