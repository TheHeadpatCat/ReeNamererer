using System.Collections.Generic;

using Dalamud.Game.Gui.NamePlate;

namespace ReeNamererer;


// Parts of this are taken from the "Pseudonamingway" Plugin
internal static class NamePlates
{
	public static Plugin Plugin;
	
	private static void OnNamePlateUpdate(INamePlateUpdateContext context, IReadOnlyList<INamePlateUpdateHandler> handlers)
	{
        foreach (var handler in handlers)
		{
			if (handler.GameObject == null)
				continue;

            foreach (var entry in Plugin.Configuration.Entries)
            {
				if (entry.IDs.Count == 0)
					continue;

				if (entry.ContainsID(handler.GameObject.BaseId))
				{
					handler.Name = entry.NickName;
					if(entry.IsTitleEnabled) handler.Title = entry.Title;
				}
            }
		}
	}

	internal static void LanguageChanged(string langCode)
    {
        if (langCode == "en")
			Init(langCode);
		else
			Dispose();
    }

    internal static void Init(string langCode)
	{
		Service.NamePlateGui.OnNamePlateUpdate += OnNamePlateUpdate;
		Service.NamePlateGui.RequestRedraw();
	}

	internal static void Dispose()
	{
		Service.NamePlateGui.OnNamePlateUpdate -= OnNamePlateUpdate;
		Service.NamePlateGui.RequestRedraw();
	}
}
