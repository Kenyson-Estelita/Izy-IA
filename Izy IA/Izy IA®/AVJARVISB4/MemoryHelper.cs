using System;
using System.Runtime.InteropServices;

namespace AVJARVISB4
{
	// Token: 0x02000012 RID: 18
	public static class MemoryHelper
	{
		// Token: 0x060000FC RID: 252
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GlobalMemoryStatusEx(ref MemoryHelper.MEMORYSTATUSEX lpBuffer);

		// Token: 0x060000FD RID: 253 RVA: 0x000317C4 File Offset: 0x0002F9C4
		public static double GetGlobalMemoryStatusEX()
		{
			MemoryHelper.MEMORYSTATUSEX memorystatusex = new MemoryHelper.MEMORYSTATUSEX
			{
				dwLength = (uint)Marshal.SizeOf(typeof(MemoryHelper.MEMORYSTATUSEX))
			};
			MemoryHelper.GlobalMemoryStatusEx(ref memorystatusex);
			return memorystatusex.ullTotalPhys;
		}

		// Token: 0x02000013 RID: 19
		internal struct MEMORYSTATUSEX
		{
			// Token: 0x04000124 RID: 292
			internal uint dwLength;

			// Token: 0x04000125 RID: 293
			internal uint dwMemoryLoad;

			// Token: 0x04000126 RID: 294
			internal ulong ullTotalPhys;

			// Token: 0x04000127 RID: 295
			internal ulong ullAvailPhys;

			// Token: 0x04000128 RID: 296
			internal ulong ullTotalPageFile;

			// Token: 0x04000129 RID: 297
			internal ulong ullAvailPageFile;

			// Token: 0x0400012A RID: 298
			internal ulong ullTotalVirtual;

			// Token: 0x0400012B RID: 299
			internal ulong ullAvailVirtual;

			// Token: 0x0400012C RID: 300
			internal ulong ullAvailExtendedVirtual;
		}
	}
}
