using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CommaExcess.Algae.Platform
{
	class AlgaeJoystickInfo : JoystickInfo
	{
		public override bool IsActive
		{
			get { return AllegroMethods.al_get_joystick_active(Handle) != 0; }
		}

		public override string Name
		{
			get { return Marshal.PtrToStringAnsi(AllegroMethods.al_get_joystick_name(Handle)); }
		}

		public AlgaeJoystickInfo(JoystickHandle handle)
		{
			Handle = handle;
		}

		public override string GetStickName(int stick)
		{
			return Marshal.PtrToStringAnsi(AllegroMethods.al_get_joystick_stick_name(Handle, stick));
		}

		public override string GetStickAxisName(int stick, int axis)
		{
			return Marshal.PtrToStringAnsi(AllegroMethods.al_get_joystick_axis_name(Handle, stick, axis));
		}

		public override string GetButtonName(int button)
		{
			return Marshal.PtrToStringAnsi(AllegroMethods.al_get_joystick_button_name(Handle, button));
		}
	}
}
