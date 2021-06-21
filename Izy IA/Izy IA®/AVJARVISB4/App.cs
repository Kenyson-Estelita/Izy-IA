using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace AVJARVISB4
{
	// Token: 0x0200001F RID: 31
	public class App : Application
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00002E50 File Offset: 0x00001050
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			base.StartupUri = new Uri("AVJARVIS.xaml", UriKind.Relative);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0003B3F8 File Offset: 0x000395F8
		[STAThread]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public static void Main()
		{
			App app = new App();
			app.InitializeComponent();
			app.Run();
		}
	}
}
