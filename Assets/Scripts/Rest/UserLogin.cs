using System;
using System.ComponentModel;

[Serializable]
[Description("login")]
public class UserLogin 
{
	public int userId;
	public string name;

	public static UserLogin actualUser;

	public UserLogin(int userId, string name)
	{
		this.userId = userId;
		this.name = name;
	}

	public string Print()
	{
		return string.Format("{0}, {1}", userId, name);
	}
}
