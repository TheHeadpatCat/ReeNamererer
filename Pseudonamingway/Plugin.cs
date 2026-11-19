using Dalamud.Interface.Windowing;
using Dalamud.Plugin;

namespace ReeNamererer;

public sealed class Plugin : IDalamudPlugin
{
	public readonly Configuration Configuration;
	public readonly WindowSystem WindowSystem = new("ReeNamererer");
	public readonly MainWindow MainWindow;

	public Plugin(IDalamudPluginInterface dalamud)
	{
        Service.Init(dalamud);

		if(Service.PluginInterface.GetPluginConfig() != null)
            Configuration = (Configuration)Service.PluginInterface.GetPluginConfig()!;
		else
			Configuration = new Configuration();
		Service.PluginInterface.SavePluginConfig(Configuration);

        NamePlates.Plugin = this;
		NamePlates.Init(dalamud.UiLanguage);

		dalamud.LanguageChanged += NamePlates.LanguageChanged;

		MainWindow = new("ReeNamererer Configuration",this);
		WindowSystem.AddWindow(MainWindow);

        Service.PluginInterface.UiBuilder.Draw += DrawUI;

        Service.PluginInterface.UiBuilder.OpenMainUi += ToggleMainUI;
	}

    private void DrawUI() => WindowSystem.Draw();

    private void ToggleMainUI()
    {
		MainWindow.Toggle();
    }

    public void Dispose()
	{
		NamePlates.Dispose();
		WindowSystem.RemoveAllWindows();
		MainWindow?.Dispose();
	}
}
