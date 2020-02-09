﻿using System;
using System.Globalization;

namespace oyasumi_lazer
{
	// thanks to https://github.com/exys228/osu-stuff/blob/master/osu!patch/Common.cs
	public static class XConsole
	{
		public const string PAD = "    "; // four spaces

		public static int WriteLine()
		{
			Console.WriteLine();
			return 1;
		}

		public static int WriteLine(object value) =>
			WriteColored(value.ToString(), true);

		public static int WriteLine(string value) =>
			WriteColored(value, true);

		public static int WriteLine(string value, params object[] args) =>
			WriteColored(string.Format(value, args), true);

		public static int Write(string value) =>
			WriteColored(value);

		public static int Write(string value, params object[] args) =>
			WriteColored(string.Format(value, args));

		private static int WriteColored(string value, bool newLine = false)
		{
			var str = string.Empty;
			var prevForeColor = Console.ForegroundColor;
			var prevBackColor = Console.BackgroundColor;

			foreach (var @char in value)
			{
				if (@char >= 0x10 && @char <= 0x1F)
				{
					if (!string.IsNullOrEmpty(str))
					{
						Console.Write(str);
						str = string.Empty;
					}

					Console.ForegroundColor = (ConsoleColor)(@char & 0x0F);
				}
				else if (@char >= 0x80 && @char <= 0x8F)
				{
					if (!string.IsNullOrEmpty(str))
					{
						Console.Write(str);
						str = string.Empty;
					}

					Console.BackgroundColor = (ConsoleColor)(@char & 0x0F);
				}
				else if (@char == 0x01)
				{
					if (!string.IsNullOrEmpty(str))
					{
						Console.Write(str);
						str = string.Empty;
					}

					Console.ResetColor();
				}
				else str += @char;
			}

			if (newLine)
				Console.WriteLine(str);
			else
				Console.Write(str);

			Console.ForegroundColor = prevForeColor;
			Console.BackgroundColor = prevBackColor;

			return 1;
		}

		public static string Fatal(string value) =>
			"\u0084\u001F[F]\u0001\u0014 " + "[" + DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo) + "]: " + value + "\u0001";

		public static string Error(string value) =>
			"[\u0014E\u0001] " + "[" + DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo) + "]: " + value;

		public static string Info(string value) =>
			"\u0001[\u0019I\u0001] " + "[" + DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo) + "]: " + value;

		public static string Warn(string value) =>
			"[\u0016W\u0001] " + " [" + DateTime.Now.ToString("T", DateTimeFormatInfo.InvariantInfo) + "] " + value;

		public static int PrintFatal(string value, bool newLine) =>
			WriteColored(Fatal(value), newLine);

		public static int PrintError(string value, bool newLine) =>
			WriteColored(Error(value), newLine);

		public static int PrintInfo(string value, bool newLine) =>
			WriteColored(Info(value), newLine);

		public static int PrintWarn(string value, bool newLine) =>
			WriteColored(Warn(value), newLine);

		public static int PrintFatal(string value, params object[] args) =>
			WriteLine(Fatal(value), args);

		public static int PrintError(string value, params object[] args) =>
			WriteLine(Error(value), args);

		public static int PrintInfo(string value, params object[] args) =>
			WriteLine(Info(value), args);

		public static int PrintWarn(string value, params object[] args) =>
			WriteLine(Warn(value), args);
	}
}
