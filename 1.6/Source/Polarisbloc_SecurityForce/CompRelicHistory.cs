using RimWorld;
using Verse;

namespace Polarisbloc_SecurityForce;

public class CompProperties_RelicHistory : CompProperties
{
	public CompProperties_RelicHistory()
	{
		compClass = typeof(CompRelicHistory);
	}
}

public class CompRelicHistory : ThingComp
{
	private bool wasRelic;

	public bool IsOrWasRelic
	{
		get
		{
			RememberCurrentRelic();
			return wasRelic;
		}
	}

	private void RememberCurrentRelic()
	{
		if (!wasRelic && ReliquaryUtility.IsRelic(parent))
		{
			wasRelic = true;
		}
	}

	public override void PostExposeData()
	{
		// This comp is intentionally listed before CompStyleable in the def. A relic
		// precept removed by reformation is not part of the saved Ideology anymore,
		// so its reference cannot survive loading (and vanilla may also clear invalid
		// style-precept references while saving). Capture the status first.
		if (Scribe.mode == LoadSaveMode.Saving)
		{
			RememberCurrentRelic();
		}

		Scribe_Values.Look(ref wasRelic, "wasRelic", defaultValue: false);
	}
}

public static class RelicHistoryUtility
{
	public static bool IsOrWasRelic(this Thing thing)
	{
		CompRelicHistory history = thing.TryGetComp<CompRelicHistory>();
		return history?.IsOrWasRelic ?? ReliquaryUtility.IsRelic(thing);
	}
}
