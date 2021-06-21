using System;
using MailKit;

namespace AVJARVISB4
{
	// Token: 0x02000010 RID: 16
	internal class MessageInfo
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00031718 File Offset: 0x0002F918
		public MessageInfo(IMessageSummary summary)
		{
			this.Summary = summary;
			bool flag = summary.Flags != null;
			if (flag)
			{
				this.Flags = summary.Flags.Value;
			}
		}

		// Token: 0x04000120 RID: 288
		public readonly IMessageSummary Summary;

		// Token: 0x04000121 RID: 289
		public MessageFlags Flags;
	}
}
