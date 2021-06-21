using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AVJARVISB4.Resources
{
	// Token: 0x02000022 RID: 34
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class Resource1
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0003C3A0 File Offset: 0x0003A5A0
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00003664 File Offset: 0x00001864
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resource1.resourceCulture;
			}
			set
			{
				Resource1.resourceCulture = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0003C3B8 File Offset: 0x0003A5B8
		internal static Icon x
		{
			get
			{
				return (Icon)Resource1.ResourceManager.GetObject("x", Resource1.resourceCulture);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0003C3E4 File Offset: 0x0003A5E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resource1.resourceMan == null;
				if (flag)
				{
					Resource1.resourceMan = new ResourceManager("AVJARVISB4.Resources.Resource1", typeof(Resource1).Assembly);
				}
				return Resource1.resourceMan;
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00002862 File Offset: 0x00000A62
		internal Resource1()
		{
		}

		// Token: 0x040002B3 RID: 691
		private static ResourceManager resourceMan;

		// Token: 0x040002B4 RID: 692
		private static CultureInfo resourceCulture;
	}
}
