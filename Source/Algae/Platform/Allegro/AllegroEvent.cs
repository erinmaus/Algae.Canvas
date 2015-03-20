using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	enum AllegroEventType : uint
	{
		ALLEGRO_EVENT_JOYSTICK_AXIS               =  1,
		ALLEGRO_EVENT_JOYSTICK_BUTTON_DOWN        =  2,
		ALLEGRO_EVENT_JOYSTICK_BUTTON_UP          =  3,
		ALLEGRO_EVENT_JOYSTICK_CONFIGURATION      =  4,

		ALLEGRO_EVENT_KEY_DOWN                    = 10,
		ALLEGRO_EVENT_KEY_CHAR                    = 11,
		ALLEGRO_EVENT_KEY_UP                      = 12,

		ALLEGRO_EVENT_MOUSE_AXES                  = 20,
		ALLEGRO_EVENT_MOUSE_BUTTON_DOWN           = 21,
		ALLEGRO_EVENT_MOUSE_BUTTON_UP             = 22,
		ALLEGRO_EVENT_MOUSE_ENTER_DISPLAY         = 23,
		ALLEGRO_EVENT_MOUSE_LEAVE_DISPLAY         = 24,
		ALLEGRO_EVENT_MOUSE_WARPED                = 25,

		ALLEGRO_EVENT_TIMER                       = 30,

		ALLEGRO_EVENT_DISPLAY_EXPOSE              = 40,
		ALLEGRO_EVENT_DISPLAY_RESIZE              = 41,
		ALLEGRO_EVENT_DISPLAY_CLOSE               = 42,
		ALLEGRO_EVENT_DISPLAY_LOST                = 43,
		ALLEGRO_EVENT_DISPLAY_FOUND               = 44,
		ALLEGRO_EVENT_DISPLAY_SWITCH_IN           = 45,
		ALLEGRO_EVENT_DISPLAY_SWITCH_OUT          = 46,
		ALLEGRO_EVENT_DISPLAY_ORIENTATION         = 47
	}

	[StructLayout(LayoutKind.Sequential)]
	struct AllegroEventHeader
	{
		public uint type;
		public IntPtr source;
		public double timestamp;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct AllegroDisplayEvent
	{
		public AllegroEventHeader header;
		public int x, y;
		public int width, height;
		public int orientation;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct AllegroJoystickEvent
	{
		public AllegroEventHeader header;
		public IntPtr id;
		public int stick;
		public int axis;
		public float pos;
		public int button;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct AllegroKeyboardEvent
	{
		public AllegroEventHeader header;
		public IntPtr display;
		public int keycode;
		public uint unichar;
		public uint modifiers;
		public int repeat;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct AllegroMouseEvent
	{
		public AllegroEventHeader header;
		public IntPtr display;
		public int x, y, z, w;
		public int dx, dy, dz, dw;
		public uint button;
		public float pressure;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct AllegroTimerEvent
	{
		public AllegroEventHeader header;
		public long count;
		public double error;
	}

	[StructLayout(LayoutKind.Explicit)]
	struct AllegroEvent
	{
		[FieldOffset(0)]
		public uint type;

		[FieldOffset(0)]
		public AllegroEventHeader any;

		[FieldOffset(0)]
		public AllegroDisplayEvent display;

		[FieldOffset(0)]
		public AllegroJoystickEvent joystick;

		[FieldOffset(0)]
		public AllegroKeyboardEvent keyboard;

		[FieldOffset(0)]
		public AllegroMouseEvent mouse;

		[FieldOffset(0)]
		public AllegroTimerEvent timer;
	}
}
