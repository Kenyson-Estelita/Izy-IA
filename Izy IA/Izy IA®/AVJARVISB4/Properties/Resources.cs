using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace AVJARVISB4.Properties
{
	// Token: 0x02000020 RID: 32
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000234 RID: 564 RVA: 0x00002862 File Offset: 0x00000A62
		internal Resources()
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0003B41C File Offset: 0x0003961C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("AVJARVISB4.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0003B464 File Offset: 0x00039664
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00002E65 File Offset: 0x00001065
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x040002B0 RID: 688
		private static ResourceManager resourceMan;

		// Token: 0x040002B1 RID: 689
		private static CultureInfo resourceCulture;
	}
}
