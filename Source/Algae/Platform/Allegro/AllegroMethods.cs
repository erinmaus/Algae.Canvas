using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	static class AllegroMethods
	{
		public const string AllegroLibrary = "allegro-5.1.dll";

		public const int Version = 0x5010900;

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static int al_install_system(int version, IntPtr atExit);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_uninstall_system();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static byte al_install_keyboard();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_uninstall_keyboard();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_get_keyboard_event_source();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static byte al_install_mouse();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_uninstall_mouse();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_get_mouse_event_source();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static byte al_install_joystick();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_uninstall_joystick();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_get_joystick_event_source();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static byte al_reconfigure_joysticks();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static byte al_get_joystick_active(IntPtr joy);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_get_joystick_name(IntPtr joy);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_get_joystick_stick_name(IntPtr joy, int stick);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_get_joystick_axis_name(IntPtr joy, int stick, int axis);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_get_joystick_button_name(IntPtr joy, int button);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_create_display(int width, int height);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_destroy_display(IntPtr display);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static int al_acknowledge_resize(IntPtr display);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_set_new_display_option(int option, int value, int importance);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_set_new_display_flags(int flags);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static int al_set_display_flag(IntPtr display, int flag, int toggle);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static int al_resize_display(IntPtr display, int width, int height);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_set_window_position(IntPtr display, int x, int y);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static int al_get_display_flags(IntPtr display);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static int al_get_display_option(IntPtr display, int option);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static int al_get_display_width(IntPtr display);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static int al_get_display_height(IntPtr display);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_flip_display();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_set_target_backbuffer(IntPtr display);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_get_display_event_source(IntPtr display);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_create_timer(double interval);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_destroy_timer(IntPtr timer);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_start_timer(IntPtr timer);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_stop_timer(IntPtr timer);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_set_timer_speed(IntPtr timer, double newInterval);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_get_timer_event_source(IntPtr timer);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_create_event_queue();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_destroy_event_queue(IntPtr queue);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_flush_event_queue(IntPtr queue);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_register_event_source(IntPtr queue, IntPtr source);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_wait_for_event(IntPtr queue, ref AllegroEvent e);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static byte al_is_event_queue_empty(IntPtr queue);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static double al_get_time();

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_get_standard_path(int id);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_path_cstr(IntPtr path, char delim);

		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static void al_destroy_path(IntPtr path);

#if WINDOWS
		[DllImport(AllegroLibrary, CallingConvention = CallingConvention.Cdecl)]
		public extern static IntPtr al_get_win_window_handle(IntPtr handle);
#endif
	}
}
