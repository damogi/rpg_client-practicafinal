using System;
using System.ComponentModel;

namespace UnityRest
{
	public static class UnityRestUtils 
	{
		public static string GetDescription(Type type)
		{
			object[] attrs = type.GetCustomAttributes (true);
		
			if (attrs != null && attrs.Length > 0)
				return ((DescriptionAttribute)attrs[0]).Description;
			else
				return null;
		}

		public static bool HasErrorStatusCode (long statusCode)
		{
			return statusCode >= 400;
		}
	}
}