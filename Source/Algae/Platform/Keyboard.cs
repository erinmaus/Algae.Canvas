using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommaExcess.Algae.Platform
{
	/// <summary>
	/// A key code, used by events, etc.
	/// </summary>
	public enum KeyCode : uint
	{
		/// <summary>
		/// The 'A' key.
		/// </summary>
		A = AllegroKeyCode.ALLEGRO_KEY_A,

		/// <summary>
		/// The 'B' key.
		/// </summary>
		B = AllegroKeyCode.ALLEGRO_KEY_B,

		/// <summary>
		/// The 'C' key.
		/// </summary>
		C = AllegroKeyCode.ALLEGRO_KEY_C,

		/// <summary>
		/// The 'D' key.
		/// </summary>
		D = AllegroKeyCode.ALLEGRO_KEY_D,

		/// <summary>
		/// The 'E' key.
		/// </summary>
		E = AllegroKeyCode.ALLEGRO_KEY_E,

		/// <summary>
		/// The 'F' key.
		/// </summary>
		F = AllegroKeyCode.ALLEGRO_KEY_F,

		/// <summary>
		/// The 'G' key.
		/// </summary>
		G = AllegroKeyCode.ALLEGRO_KEY_G,

		/// <summary>
		/// The 'H' key.
		/// </summary>
		H = AllegroKeyCode.ALLEGRO_KEY_H,

		/// <summary>
		/// The 'I' key.
		/// </summary>
		I = AllegroKeyCode.ALLEGRO_KEY_I,

		/// <summary>
		/// The 'J' key.
		/// </summary>
		J = AllegroKeyCode.ALLEGRO_KEY_J,

		/// <summary>
		/// The 'K' key.
		/// </summary>
		K = AllegroKeyCode.ALLEGRO_KEY_K,

		/// <summary>
		/// The 'L' key.
		/// </summary>
		L = AllegroKeyCode.ALLEGRO_KEY_L,

		/// <summary>
		/// The 'M' key.
		/// </summary>
		M = AllegroKeyCode.ALLEGRO_KEY_M,

		/// <summary>
		/// The 'N' key.
		/// </summary>
		N = AllegroKeyCode.ALLEGRO_KEY_N,

		/// <summary>
		/// The 'O' key.
		/// </summary>
		O = AllegroKeyCode.ALLEGRO_KEY_O,

		/// <summary>
		/// The 'P' key.
		/// </summary>
		P = AllegroKeyCode.ALLEGRO_KEY_P,

		/// <summary>
		/// The 'Q' key.
		/// </summary>
		Q = AllegroKeyCode.ALLEGRO_KEY_Q,

		/// <summary>
		/// The 'R' key.
		/// </summary>
		R = AllegroKeyCode.ALLEGRO_KEY_R,

		/// <summary>
		/// The 'S' key.
		/// </summary>
		S = AllegroKeyCode.ALLEGRO_KEY_S,

		/// <summary>
		/// The 'T' key.
		/// </summary>
		T = AllegroKeyCode.ALLEGRO_KEY_T,

		/// <summary>
		/// The 'U' key.
		/// </summary>
		U = AllegroKeyCode.ALLEGRO_KEY_U,

		/// <summary>
		/// The 'V' key.
		/// </summary>
		V = AllegroKeyCode.ALLEGRO_KEY_V,

		/// <summary>
		/// The 'W' key.
		/// </summary>
		W = AllegroKeyCode.ALLEGRO_KEY_W,

		/// <summary>
		/// The 'X' key.
		/// </summary>
		X = AllegroKeyCode.ALLEGRO_KEY_X,

		/// <summary>
		/// The 'Y' key.
		/// </summary>
		Y = AllegroKeyCode.ALLEGRO_KEY_Y,

		/// <summary>
		/// The 'Z' key.
		/// </summary>
		Z = AllegroKeyCode.ALLEGRO_KEY_Z,

		/// <summary>
		/// The '0' key.
		/// </summary>
		Zero = AllegroKeyCode.ALLEGRO_KEY_0,

		/// <summary>
		/// The '1' key.
		/// </summary>
		One = AllegroKeyCode.ALLEGRO_KEY_1,

		/// <summary>
		/// The '2' key.
		/// </summary>
		Two = AllegroKeyCode.ALLEGRO_KEY_2,

		/// <summary>
		/// The '3' key.
		/// </summary>
		Three = AllegroKeyCode.ALLEGRO_KEY_3,

		/// <summary>
		/// The '4' key.
		/// </summary>
		Four = AllegroKeyCode.ALLEGRO_KEY_4,

		/// <summary>
		/// The '5' key.
		/// </summary>
		Five = AllegroKeyCode.ALLEGRO_KEY_5,

		/// <summary>
		/// The '6' key.
		/// </summary>
		Six = AllegroKeyCode.ALLEGRO_KEY_6,

		/// <summary>
		/// The '7' key.
		/// </summary>
		Seven = AllegroKeyCode.ALLEGRO_KEY_7,

		/// <summary>
		/// The '8' key.
		/// </summary>
		Eight = AllegroKeyCode.ALLEGRO_KEY_8,

		/// <summary>
		/// The '9' key.
		/// </summary>
		Nine = AllegroKeyCode.ALLEGRO_KEY_9,

		/// <summary>
		/// The '0' key on the number pad.
		/// </summary>
		PadZero = AllegroKeyCode.ALLEGRO_KEY_PAD_0,

		/// <summary>
		/// The '1' key on the number pad.
		/// </summary>
		PadOne = AllegroKeyCode.ALLEGRO_KEY_PAD_1,

		/// <summary>
		/// The '2' key on the number pad.
		/// </summary>
		PadTwo = AllegroKeyCode.ALLEGRO_KEY_PAD_2,

		/// <summary>
		/// The '3' key on the number pad.
		/// </summary>
		PadThree = AllegroKeyCode.ALLEGRO_KEY_PAD_3,

		/// <summary>
		/// The '4' key on the number pad.
		/// </summary>
		PadFour = AllegroKeyCode.ALLEGRO_KEY_PAD_4,

		/// <summary>
		/// The '5' key on the number pad.
		/// </summary>
		PadFive = AllegroKeyCode.ALLEGRO_KEY_PAD_5,

		/// <summary>
		/// The '6' key on the number pad.
		/// </summary>
		PadSix = AllegroKeyCode.ALLEGRO_KEY_PAD_6,

		/// <summary>
		/// The '7' key on the number pad.
		/// </summary>
		PadSeven = AllegroKeyCode.ALLEGRO_KEY_PAD_7,

		/// <summary>
		/// The '8' key on the number pad.
		/// </summary>
		PadEight = AllegroKeyCode.ALLEGRO_KEY_PAD_8,

		/// <summary>
		/// The '9' key on the number pad.
		/// </summary>
		PadNine = AllegroKeyCode.ALLEGRO_KEY_PAD_9,

		/// <summary>
		/// The 'F1' key.
		/// </summary>
		FunctionOne = AllegroKeyCode.ALLEGRO_KEY_F1,

		/// <summary>
		/// The 'F2' key.
		/// </summary>
		FunctionTwo = AllegroKeyCode.ALLEGRO_KEY_F2,

		/// <summary>
		/// The 'F3' key.
		/// </summary>
		FunctionThree = AllegroKeyCode.ALLEGRO_KEY_F3,

		/// <summary>
		/// The 'F4' key.
		/// </summary>
		FunctionFour = AllegroKeyCode.ALLEGRO_KEY_F4,

		/// <summary>
		/// The 'F5' key.
		/// </summary>
		FunctionFive = AllegroKeyCode.ALLEGRO_KEY_F5,

		/// <summary>
		/// The 'F6' key.
		/// </summary>
		FunctionSix = AllegroKeyCode.ALLEGRO_KEY_F6,

		/// <summary>
		/// The 'F7' key.
		/// </summary>
		FunctionSeven = AllegroKeyCode.ALLEGRO_KEY_F7,

		/// <summary>
		/// The 'F8' key.
		/// </summary>
		FunctionEight = AllegroKeyCode.ALLEGRO_KEY_F8,

		/// <summary>
		/// The 'F9' key.
		/// </summary>
		FunctionNine = AllegroKeyCode.ALLEGRO_KEY_F9,

		/// <summary>
		/// The 'F10' key.
		/// </summary>
		FunctionTen = AllegroKeyCode.ALLEGRO_KEY_F10,

		/// <summary>
		/// The 'F11' key.
		/// </summary>
		FunctionEleven = AllegroKeyCode.ALLEGRO_KEY_F11,

		/// <summary>
		/// The 'F12' key.
		/// </summary>
		FunctionTwelve = AllegroKeyCode.ALLEGRO_KEY_F12,

		/// <summary>
		/// The escape key.
		/// </summary>
		Escape = AllegroKeyCode.ALLEGRO_KEY_ESCAPE,

		/// <summary>
		/// The tilde key.
		/// </summary>
		Tilde = AllegroKeyCode.ALLEGRO_KEY_TILDE,

		/// <summary>
		/// The minus key.
		/// </summary>
		Minus = AllegroKeyCode.ALLEGRO_KEY_MINUS,

		/// <summary>
		/// The equals key.
		/// </summary>
		Equals = AllegroKeyCode.ALLEGRO_KEY_EQUALS,

		/// <summary>
		/// The backspace key.
		/// </summary>
		Backspace = AllegroKeyCode.ALLEGRO_KEY_BACKSPACE,

		/// <summary>
		/// The tab key.
		/// </summary>
		Tab = AllegroKeyCode.ALLEGRO_KEY_TAB,

		/// <summary>
		/// The open brace key.
		/// </summary>
		OpenBrace = AllegroKeyCode.ALLEGRO_KEY_OPENBRACE,

		/// <summary>
		/// The close brace key.
		/// </summary>
		CloseBrace = AllegroKeyCode.ALLEGRO_KEY_CLOSEBRACE,

		/// <summary>
		/// The enter key.
		/// </summary>
		Enter = AllegroKeyCode.ALLEGRO_KEY_ENTER,

		/// <summary>
		/// The semicolon key.
		/// </summary>
		Semicolon = AllegroKeyCode.ALLEGRO_KEY_SEMICOLON,

		/// <summary>
		/// The quote key.
		/// </summary>
		Quote = AllegroKeyCode.ALLEGRO_KEY_QUOTE,

		/// <summary>
		/// The backslash key.
		/// </summary>
		Backslash = AllegroKeyCode.ALLEGRO_KEY_BACKSLASH,

		// No need; it's peculiar and shouldn't be used directly
		//Backslash2 = AllegroInternalKeyCode.ALLEGRO_KEY_BACKSLASH2,

		/// <summary>
		/// The comma key.
		/// </summary>
		Comma = AllegroKeyCode.ALLEGRO_KEY_COMMA,

		/// <summary>
		/// The full stop key.
		/// </summary>
		FullStop = AllegroKeyCode.ALLEGRO_KEY_FULLSTOP,

		/// <summary>
		/// The slash key.
		/// </summary>
		Slash = AllegroKeyCode.ALLEGRO_KEY_SLASH,

		/// <summary>
		/// The space key.
		/// </summary>
		Space = AllegroKeyCode.ALLEGRO_KEY_SPACE,

		/// <summary>
		/// The insert key.
		/// </summary>
		Insert = AllegroKeyCode.ALLEGRO_KEY_INSERT,

		/// <summary>
		/// The delete key.
		/// </summary>
		Delete = AllegroKeyCode.ALLEGRO_KEY_DELETE,

		/// <summary>
		/// The home key.
		/// </summary>
		Home = AllegroKeyCode.ALLEGRO_KEY_HOME,

		/// <summary>
		/// The end key.
		/// </summary>
		End = AllegroKeyCode.ALLEGRO_KEY_END,

		/// <summary>
		/// The page up key.
		/// </summary>
		PageUp = AllegroKeyCode.ALLEGRO_KEY_PGUP,

		/// <summary>
		/// The page down key.
		/// </summary>
		PageDown = AllegroKeyCode.ALLEGRO_KEY_PGDN,

		/// <summary>
		/// The left arrow key.
		/// </summary>
		ArrowLeft = AllegroKeyCode.ALLEGRO_KEY_LEFT,

		/// <summary>
		/// The right arrow key.
		/// </summary>
		ArrowRight = AllegroKeyCode.ALLEGRO_KEY_RIGHT,

		/// <summary>
		/// The up arrow key.
		/// </summary>
		ArrowUp = AllegroKeyCode.ALLEGRO_KEY_UP,

		/// <summary>
		/// The down arrow key.
		/// </summary>
		ArrowDown = AllegroKeyCode.ALLEGRO_KEY_DOWN,

		/// <summary>
		/// The slash number pad key.
		/// </summary>
		PadSlash = AllegroKeyCode.ALLEGRO_KEY_PAD_SLASH,

		/// <summary>
		/// The asterick number pad key.
		/// </summary>
		PadAsterick = AllegroKeyCode.ALLEGRO_KEY_PAD_ASTERISK,

		/// <summary>
		/// The minus number pad key.
		/// </summary>
		PadMinus = AllegroKeyCode.ALLEGRO_KEY_PAD_MINUS,

		/// <summary>
		/// The plus number pad key.
		/// </summary>
		PadPlus = AllegroKeyCode.ALLEGRO_KEY_PAD_PLUS,

		/// <summary>
		/// The delete number pad key.
		/// </summary>
		PadDelete = AllegroKeyCode.ALLEGRO_KEY_PAD_DELETE,

		/// <summary>
		/// The enter number pad key.
		/// </summary>
		PadEnter = AllegroKeyCode.ALLEGRO_KEY_PAD_ENTER,

		/// <summary>
		/// The print screen key.
		/// </summary>
		PrintScreen = AllegroKeyCode.ALLEGRO_KEY_PRINTSCREEN,

		/// <summary>
		/// The pause key.
		/// </summary>
		Pause = AllegroKeyCode.ALLEGRO_KEY_PAUSE,

		// The few rarely used (ALLEGRO_KEY_ABNT_C1 to ALLEGRO_KEY_SEMICOLON2) in Allegro are left out here

		/// <summary>
		/// The command key.
		/// </summary>
		Command = AllegroKeyCode.ALLEGRO_KEY_COMMAND,

		/// <summary>
		/// Any key from here to KeyModifiers are unused and can be specified by the application.
		/// </summary>
		Unused = AllegroKeyCode.ALLEGRO_KEY_UNKNOWN,

		/// <summary>
		/// The left shift key.
		/// </summary>
		LeftShift = AllegroKeyCode.ALLEGRO_KEY_LSHIFT,

		/// <summary>
		/// The right shift key.
		/// </summary>
		RightShift = AllegroKeyCode.ALLEGRO_KEY_RSHIFT,

		/// <summary>
		/// The left control key.
		/// </summary>
		LeftControl = AllegroKeyCode.ALLEGRO_KEY_LCTRL,

		/// <summary>
		/// The right control key.
		/// </summary>
		RightControl = AllegroKeyCode.ALLEGRO_KEY_RCTRL,

		/// <summary>
		/// The alt key.
		/// </summary>
		Alt = AllegroKeyCode.ALLEGRO_KEY_ALT,

		/// <summary>
		/// ???
		/// </summary>
		AltGr = AllegroKeyCode.ALLEGRO_KEY_ALTGR,

		/// <summary>
		/// The left windows key.
		/// </summary>
		LeftWindows = AllegroKeyCode.ALLEGRO_KEY_LWIN,

		/// <summary>
		/// The right windows key.
		/// </summary>
		RightWindows = AllegroKeyCode.ALLEGRO_KEY_RWIN,

		/// <summary>
		/// The menu key.
		/// </summary>
		Menu = AllegroKeyCode.ALLEGRO_KEY_MENU,

		/// <summary>
		/// The scroll lock key.
		/// </summary>
		ScrollLock = AllegroKeyCode.ALLEGRO_KEY_SCROLLLOCK,

		/// <summary>
		/// The num lock key.
		/// </summary>
		NumLock = AllegroKeyCode.ALLEGRO_KEY_NUMLOCK,

		/// <summary>
		/// The caps lock key.
		/// </summary>
		CapsLock = AllegroKeyCode.ALLEGRO_KEY_CAPSLOCK,

		/// <summary>
		/// The maximum amount of keys. Do not go over this.
		/// </summary>
		Maximum = AllegroKeyCode.ALLEGRO_KEY_MAX
	}

	/// <summary>
	/// Enumeration for the possible key modifiers.
	/// </summary>
	[Flags]
	public enum KeyModifier : uint
	{
		/// <summary>
		/// No modifiers.
		/// </summary>
		None = 0,

		/// <summary>
		/// The shift modifier.
		/// </summary>
		Shift = AllegroKeyModifier.ALLEGRO_KEYMOD_SHIFT,

		/// <summary>
		/// The control modifier.
		/// </summary>
		Control = AllegroKeyModifier.ALLEGRO_KEYMOD_CTRL,

		/// <summary>
		/// The alt modifier.
		/// </summary>
		Alt = AllegroKeyModifier.ALLEGRO_KEYMOD_ALT,

		/// <summary>
		/// The left Windows key modifier.
		/// </summary>
		LeftWindow = AllegroKeyModifier.ALLEGRO_KEYMOD_LWIN,

		/// <summary>
		/// The right Windows key modifier.
		/// </summary>
		RightWindow = AllegroKeyModifier.ALLEGRO_KEYMOD_RWIN,

		/// <summary>
		/// The menu modifier.
		/// </summary>
		Menu = AllegroKeyModifier.ALLEGRO_KEYMOD_MENU,

		/// <summary>
		/// The ALT GR modifier.
		/// </summary>
		AltGr = AllegroKeyModifier.ALLEGRO_KEYMOD_ALTGR,

		/// <summary>
		/// The command modifier.
		/// </summary>
		Command = AllegroKeyModifier.ALLEGRO_KEYMOD_COMMAND,

		/// <summary>
		/// The scroll lock modifier.
		/// </summary>
		ScrollLock = AllegroKeyModifier.ALLEGRO_KEYMOD_SCROLLLOCK,

		/// <summary>
		/// The num lock modifier.
		/// </summary>
		NumLock = AllegroKeyModifier.ALLEGRO_KEYMOD_NUMLOCK,

		/// <summary>
		/// The caps lock modifier.
		/// </summary>
		CapsLock = AllegroKeyModifier.ALLEGRO_KEYMOD_CAPSLOCK,

		/// <summary>
		/// When an ALT sequence is taking place.
		/// </summary>
		AltSequence = AllegroKeyModifier.ALLEGRO_KEYMOD_INALTSEQ,
	}

	/// <summary>
	/// A class that provides keyboard services.
	/// </summary>
	public abstract class Keyboard : IInitializable, IDisposable
	{
		/// <summary>
		/// Gets if the keyboard was initialized.
		/// </summary>
		public bool IsInitialized
		{
			get;
			protected set;
		}

		/// <summary>
		/// Initializes the keyboard to a default state.
		/// </summary>
		public abstract void Initialize();

		/// <summary>
		/// Disposes of all resources allocated by the keyboard.
		/// </summary>
		public abstract void Dispose();
	}
}
