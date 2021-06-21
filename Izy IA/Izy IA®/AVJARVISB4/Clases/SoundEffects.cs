using System;
using System.Media;
using System.Windows;

namespace AVJARVISB4.Clases
{
	// Token: 0x02000025 RID: 37
	public static class SoundEffects
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0003C45C File Offset: 0x0003A65C
		public static void Somefects()
		{
			try
			{
				SoundEffects.sonido.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Speech On.wav";
				SoundEffects.sonidoRec.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\recorMessage.wav";
				SoundEffects.Hover_skipe1.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Hover_skipe1.wav";
				SoundEffects.Hover_comandos.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Hover_comandos.wav";
				SoundEffects.Hover_import_export.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Hover_import_export.wav";
				SoundEffects.ClickClose_menu_hide.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\ClickClose_menu_hide.wav";
				SoundEffects.ClickOpen_import_export.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\ClickOpen_import_export.wav";
				SoundEffects.sonidoAlarm.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\pianobeat.wav";
				SoundEffects.Show_Window.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Show_Window.wav";
				SoundEffects.ClickOpen_menu_show.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\ClickOpen_menu_show.wav";
				SoundEffects.ClickOpen_fields.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\vintage_radio_button_006.wav";
				SoundEffects.Click_settings_arrow.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Click_settings_arrow.wav";
				SoundEffects.Hover_moving_arc.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Hover_moving_arc.wav";
				SoundEffects.Hover_moving_arc_zone_changed.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Hover_moving_arc_zone_changed.wav";
				SoundEffects.Hover_main_button.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Hover_main_button.wav";
				SoundEffects.Stopped_Listening.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Stopped_Listening.wav";
				SoundEffects.ClickOpen_moving_arc.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\ClickOpen_moving_arc.wav";
				SoundEffects.ClickOpen_select_number.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\ClickOpen_select_number.wav";
				SoundEffects.ShotgunShell_Land_Concrete_02.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\ShotgunShell_Land_Concrete_02.wav";
				SoundEffects.ShotgunShell_Land_Lino_03.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\ShotgunShell_Land_Lino_03.wav";
				SoundEffects.handbag_fasten_003.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\handbag_fasten_003.wav";
				SoundEffects.short_med_003.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\short_med_003.wav";
				SoundEffects.Grit_course_hardsurface_scatter.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Grit_course_hardsurface_scatter.wav";
				SoundEffects.Keyboard.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Keyboard.wav";
				SoundEffects.Whip.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "Sonidos\\Whip.wav";
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				MessageBox.Show(ex2.Message, "AV - Efeitos Sonoros", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}

		// Token: 0x040002B9 RID: 697
		public static SoundPlayer sonido = new SoundPlayer();

		// Token: 0x040002BA RID: 698
		public static SoundPlayer sonidoRec = new SoundPlayer();

		// Token: 0x040002BB RID: 699
		public static SoundPlayer Hover_skipe1 = new SoundPlayer();

		// Token: 0x040002BC RID: 700
		public static SoundPlayer Hover_comandos = new SoundPlayer();

		// Token: 0x040002BD RID: 701
		public static SoundPlayer Hover_import_export = new SoundPlayer();

		// Token: 0x040002BE RID: 702
		public static SoundPlayer ClickClose_menu_hide = new SoundPlayer();

		// Token: 0x040002BF RID: 703
		public static SoundPlayer ClickOpen_import_export = new SoundPlayer();

		// Token: 0x040002C0 RID: 704
		public static SoundPlayer sonidoAlarm = new SoundPlayer();

		// Token: 0x040002C1 RID: 705
		public static SoundPlayer Show_Window = new SoundPlayer();

		// Token: 0x040002C2 RID: 706
		public static SoundPlayer ClickOpen_menu_show = new SoundPlayer();

		// Token: 0x040002C3 RID: 707
		public static SoundPlayer ClickOpen_fields = new SoundPlayer();

		// Token: 0x040002C4 RID: 708
		public static SoundPlayer Click_settings_arrow = new SoundPlayer();

		// Token: 0x040002C5 RID: 709
		public static SoundPlayer Hover_moving_arc = new SoundPlayer();

		// Token: 0x040002C6 RID: 710
		public static SoundPlayer Hover_moving_arc_zone_changed = new SoundPlayer();

		// Token: 0x040002C7 RID: 711
		public static SoundPlayer Hover_main_button = new SoundPlayer();

		// Token: 0x040002C8 RID: 712
		public static SoundPlayer Stopped_Listening = new SoundPlayer();

		// Token: 0x040002C9 RID: 713
		public static SoundPlayer ClickOpen_moving_arc = new SoundPlayer();

		// Token: 0x040002CA RID: 714
		public static SoundPlayer ClickOpen_select_number = new SoundPlayer();

		// Token: 0x040002CB RID: 715
		public static SoundPlayer ShotgunShell_Land_Concrete_02 = new SoundPlayer();

		// Token: 0x040002CC RID: 716
		public static SoundPlayer ShotgunShell_Land_Lino_03 = new SoundPlayer();

		// Token: 0x040002CD RID: 717
		public static SoundPlayer handbag_fasten_003 = new SoundPlayer();

		// Token: 0x040002CE RID: 718
		public static SoundPlayer short_med_003 = new SoundPlayer();

		// Token: 0x040002CF RID: 719
		public static SoundPlayer Grit_course_hardsurface_scatter = new SoundPlayer();

		// Token: 0x040002D0 RID: 720
		public static SoundPlayer Keyboard = new SoundPlayer();

		// Token: 0x040002D1 RID: 721
		public static SoundPlayer Whip = new SoundPlayer();
	}
}
