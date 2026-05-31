namespace Gidsiks.LunacidHelper.Lib;

public static class XpHelper
{
	public static float GetXPMultNow(int curLunacy) => GetXPMultByDate(curLunacy, DateOnly.FromDateTime(DateTime.Now));
	public static float GetXPMultByDate(int curLunacy, DateOnly date)
	{
		float moonMult = MoonHelper.GetMoonMultByDate(date);
		float XPMult = 1f + (moonMult / 10f) * (curLunacy / 50f);
		return XPMult;
	}

	public static float GainXpNow(int baseXPGain = 100, int curLunacy = 0, int curLvl = 1, int curXP = 0) => GainXpByDate(baseXPGain, DateOnly.FromDateTime(DateTime.Now), curLunacy, curLvl, curXP);
	public static float GainXpByDate(int baseXPGain = 100, DateOnly date = default, int curLunacy = 0, int curLvl = 1, int curXP = 0)
	{
		if (date == default) date = DateOnly.FromDateTime(DateTime.Now);

		float virtualLvl = curLvl + curXP / 100f;
		float XPMult = GetXPMultByDate(curLunacy, date);

		int moonXPGain = Convert.ToInt32(MathF.Round(baseXPGain * XPMult, 0));

		var actualXPGain = 35f * MathF.Pow(moonXPGain / virtualLvl, 1.25f);
		if (virtualLvl > 50)
		{
			actualXPGain /= MathF.Pow(virtualLvl, 0.1f);
		}
		actualXPGain = MathF.Round(actualXPGain, 0);

		int xpGain = Convert.ToInt32(actualXPGain);
		return xpGain;
	}
}
