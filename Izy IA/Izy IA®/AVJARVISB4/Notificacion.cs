using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace AVJARVISB4
{
	// Token: 0x0200001E RID: 30
	public class Notificacion : Window, IComponentConnector
	{
		// Token: 0x0600022D RID: 557 RVA: 0x0003B2B8 File Offset: 0x000394B8
		public Notificacion()
		{
			this.InitializeComponent();
			Rect workArea = SystemParameters.WorkArea;
			base.Left = workArea.Right - base.Width;
			base.Top = workArea.Bottom - base.Height;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000286C File Offset: 0x00000A6C
		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0003B304 File Offset: 0x00039504
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/A V®;component/notificacion.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0003B33C File Offset: 0x0003953C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.Window = (Notificacion)target;
				break;
			case 2:
				this.LayoutRoot = (Grid)target;
				break;
			case 3:
				this.lblTituloS = (Label)target;
				break;
			case 4:
				this.lblTitulo = (Label)target;
				break;
			case 5:
				this.RecR = (Rectangle)target;
				break;
			case 6:
				this.btnClose = (Button)target;
				this.btnClose.Click += this.btnClose_Click;
				break;
			case 7:
				this.txtNota = (TextBlock)target;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x040002A8 RID: 680
		internal Notificacion Window;

		// Token: 0x040002A9 RID: 681
		internal Grid LayoutRoot;

		// Token: 0x040002AA RID: 682
		internal Label lblTituloS;

		// Token: 0x040002AB RID: 683
		internal Label lblTitulo;

		// Token: 0x040002AC RID: 684
		internal Rectangle RecR;

		// Token: 0x040002AD RID: 685
		internal Button btnClose;

		// Token: 0x040002AE RID: 686
		internal TextBlock txtNota;

		// Token: 0x040002AF RID: 687
		private bool _contentLoaded;
	}
}
