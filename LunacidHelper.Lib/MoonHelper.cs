namespace Gidsiks.LunacidHelper.Lib;

public static class MoonHelper
{
	private const float MoonCycleDays = 29.531f;

	public static float GetMoonMultNow() => GetMoonMultByDate(DateOnly.FromDateTime(DateTime.Now));
	public static float GetMoonMultByDate(DateOnly date)
	{
		int firstFullMoonDay = GetFirstFullMoonDay(date);

		float moonPhase = MathF.Abs((date.Month - 1) - (date.DayOfYear - firstFullMoonDay) / MoonCycleDays);
		moonPhase = moonPhase switch
		{
			< 0.06f => 0f,
			> 0.94f => 0f,
			_ => moonPhase
		};

		float multCoef = MathF.Pow(Math.Abs(moonPhase - 0.5f) * 2f, 2);
		float moonMult = Single.Lerp(0f, 10f, multCoef);

		return moonMult;
	}


	public static MoonPhases GetMoonPhaseNow() => GetMoonPhaseByDate(DateOnly.FromDateTime(DateTime.Now));
	public static MoonPhases GetMoonPhaseByDate(DateOnly date)
	{
		int firstFullMoonDay = GetFirstFullMoonDay(date);

		int daysSinceCycleStart = date.DayOfYear - firstFullMoonDay;
		if (daysSinceCycleStart < 0) daysSinceCycleStart += (int)MoonCycleDays;
		float daysInCycle = daysSinceCycleStart % MoonCycleDays;
		float cyclePercent = daysInCycle / MoonCycleDays;

		var moonPhase = cyclePercent switch
		{
			< 0.060f => MoonPhases.FullMoon,
			< 0.185f => MoonPhases.Aging1,
			< 0.310f => MoonPhases.Aging2,
			< 0.435f => MoonPhases.Aging3,
			< 0.560f => MoonPhases.NewMoon,
			< 0.685f => MoonPhases.Growing1,
			< 0.810f => MoonPhases.Growing2,
			< 0.940f => MoonPhases.Growing3,
			> 0.940f => MoonPhases.FullMoon,
			_ => MoonPhases.FullMoon,
		};

		return moonPhase;
	}

	private static int GetFirstFullMoonDay(DateOnly date)
		=> date.Year switch
		{
			2020 => 10,
			2021 => 28,
			2022 => 17,
			2023 => 6,
			2024 => 25,
			2025 => 13,
			2026 => 3,
			2027 => 22,
			2028 => 11,
			2029 => 30,
			2030 => 19,
			_ => 0
		};

}

public enum MoonPhases
{
	NewMoon,
	Growing1,
	Growing2,
	Growing3,
	FullMoon,
	Aging1,
	Aging2,
	Aging3,
}
