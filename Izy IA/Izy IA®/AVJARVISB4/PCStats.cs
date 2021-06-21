using System;
using System.Diagnostics;

namespace AVJARVISB4
{
	// Token: 0x02000011 RID: 17
	public static class PCStats
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x0003175C File Offset: 0x0002F95C
		public static double GetCPUUsage()
		{
			return (double)PCStats.cpu.NextValue();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0003177C File Offset: 0x0002F97C
		public static float GetFreeMemory()
		{
			return PCStats.ramAvailable.NextValue();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00031798 File Offset: 0x0002F998
		public static double GetTotalMemory()
		{
			return MemoryHelper.GetGlobalMemoryStatusEX() / 1024.0 / 1024.0;
		}

		// Token: 0x04000122 RID: 290
		private static PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");

		// Token: 0x04000123 RID: 291
		private static PerformanceCounter ramAvailable = new PerformanceCounter("Memory", "Available MBytes");
	}
}
