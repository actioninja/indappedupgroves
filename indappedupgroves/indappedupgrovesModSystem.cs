using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.Common;

namespace indappedupgroves;

public class InDappedUpGrovesModSystem : ModSystem
{
    private Harmony? _harmony;
    
    // Called on server and client
    // Useful for registering block/entity classes on both sides
    public override void Start(ICoreAPI api)
    {
        Mod.Logger.Notification("Ay idg dap me up");
        if (Harmony.HasAnyPatches(Mod.Info.ModID)) return;
        _harmony = new Harmony(Mod.Info.ModID);
        _harmony.PatchCategory(Mod.Info.ModID);
    }

    public override void Dispose()
    {
        _harmony?.UnpatchAll(Mod.Info.ModID);
    }
}