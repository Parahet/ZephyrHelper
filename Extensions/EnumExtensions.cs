using System;

namespace ZephyrHelper.Extensions
{
	class TextAttribute : Attribute
	{
		public string Text;
		public TextAttribute(string text)
		{
			Text = text;
		}
	}
	class BookShortNameAttribute : Attribute
	{
		public string BookShortName;
		public BookShortNameAttribute(string bookShortName)
		{
			BookShortName = bookShortName;
		}
	}

	public static class EnumExtensions
	{
		public static string ToText(this Enum enumeration)
		{
			var memberInfo = enumeration.GetType().GetMember(enumeration.ToString());
			if (memberInfo.Length > 0)
			{
				object[] attributes = memberInfo[0].GetCustomAttributes(typeof(TextAttribute), false);
				if (attributes.Length > 0)
				{
					return ((TextAttribute)attributes[0]).Text;
				}
			}
			return enumeration.ToString();
		}
		public static string ToBookShortName(this Enum enumeration)
		{
			var memberInfo = enumeration.GetType().GetMember(enumeration.ToString());
			if (memberInfo.Length > 0)
			{
				object[] attributes = memberInfo[0].GetCustomAttributes(typeof(BookShortNameAttribute), false);
				if (attributes.Length > 0)
				{
					return ((BookShortNameAttribute)attributes[0]).BookShortName;
				}
			}
			return enumeration.ToString();
		}
	}
}
