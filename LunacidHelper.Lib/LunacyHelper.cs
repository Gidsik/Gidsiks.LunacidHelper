namespace Gidsiks.LunacidHelper.Lib;

public static class LunacyHelper
{
	public static float GetLunacyGainBySpellCastNow(int spellCost = 10, int maxMP = 10) => GetLunacyGainBySpellCastByDate(spellCost, maxMP, DateOnly.FromDateTime(DateTime.Now));
	public static float GetLunacyGainBySpellCastByDate(int spellCost = 10, int maxMP = 10, DateOnly date = default)
	{
		if (date == default) date = DateOnly.FromDateTime(DateTime.Now);

		float moonMult = MoonHelper.GetMoonMultByDate(date);
		return (float)spellCost / (float)maxMP * 8f * moonMult;
	}

	public static float GetSpellDmgMultBasedOnLunacy(float lunacy)
	{
		float baseSpellDmg = 85f;
		float lunacyBasedBonus = MathF.Pow(lunacy, 1.5f) * 0.04f;
		return 1 + lunacyBasedBonus / baseSpellDmg;
	}
}
