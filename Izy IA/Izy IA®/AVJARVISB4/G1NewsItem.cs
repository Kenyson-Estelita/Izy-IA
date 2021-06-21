using System;

namespace AVJARVISB4
{
	// Token: 0x0200000F RID: 15
	public class G1NewsItem
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000027E6 File Offset: 0x000009E6
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x000027EE File Offset: 0x000009EE
		public string Title { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000027F7 File Offset: 0x000009F7
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000027FF File Offset: 0x000009FF
		public string URL { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002808 File Offset: 0x00000A08
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00002810 File Offset: 0x00000A10
		public string Text { get; set; }

		// Token: 0x060000F6 RID: 246 RVA: 0x00002819 File Offset: 0x00000A19
		public G1NewsItem(string title, string text)
		{
			this.Title = title;
			this.Text = text;
		}
	}
}
