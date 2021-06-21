using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace AVJARVISB4
{
	// Token: 0x0200000E RID: 14
	public static class G1FeedNews
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0003163C File Offset: 0x0002F83C
		public static G1NewsItem[] GetNews()
		{
			List<G1NewsItem> list = new List<G1NewsItem>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load("http://g1.globo.com/dynamo/rss2.xml");
			foreach (object obj in xmlDocument.GetElementsByTagName("item"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				G1NewsItem item = new G1NewsItem(xmlNode["title"].InnerText, G1FeedNews.RemoveTags(xmlNode["description"].InnerText));
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000316F4 File Offset: 0x0002F8F4
		private static string RemoveTags(string text)
		{
			return Regex.Replace(text, "<.*?>", string.Empty);
		}
	}
}
