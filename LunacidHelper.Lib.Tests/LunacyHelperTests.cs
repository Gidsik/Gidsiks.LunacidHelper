using Gidsiks.LunacidHelper.Lib;

namespace LunacidHelper.Lib.Tests;

public class LunacyHelperTests
{
	[Theory]
	[InlineData(0, 1.0)]
	[InlineData(50, 1.17)]
	[InlineData(100, 1.47)]
	public void SpellDmgMultTest(int lunacy, float expectedMult)
	{
		var actual = LunacyHelper.GetSpellDmgMultBasedOnLunacy(lunacy);
		Assert.Equal(expectedMult, actual, 0.01f);
	}

	[Theory]
	[InlineData(10, 10, new int[] { 2026, 1, 31 }, 80f)]
	[InlineData(10, 100, new int[] { 2026, 1, 31 }, 8f)]
	[InlineData(10, 100, new int[] { 2026, 2, 4 }, 5.55f)]
	[InlineData(10, 100, new int[] { 2026, 2, 10 }, 1.45f)]
	[InlineData(10, 100, new int[] { 2026, 2, 15 }, 0.06f)]
	[InlineData(10, 100, new int[] { 2026, 2, 16 }, 0f)]
	public void LunacyGainTest(int spellCost, int maxMP, int[] date, float expectedLunacyGain)
	{
		var actual = LunacyHelper.GetLunacyGainBySpellCastByDate(spellCost, maxMP, new DateOnly(date[0], date[1], date[2]));
		Assert.Equal(expectedLunacyGain, actual, 0.01f);
	}
}
