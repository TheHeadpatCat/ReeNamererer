using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace ReeNamererer;

internal class Service
{
    [PluginService] internal static INamePlateGui NamePlateGui { get; set; } = null!;
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; set; } = null!;

    internal static void Init(IDalamudPluginInterface dalamud) => dalamud.Create<Service>();
}
