using System;
using System.ComponentModel;

[Serializable]
[Description("gold")]
public class Gold 
{
	public int gold;

	public static Gold actualGold;

	public Gold(int gold)
	{
		this.gold = gold;
	}

	public string Print()
	{
		return string.Format("{0}", gold);
	}
}
