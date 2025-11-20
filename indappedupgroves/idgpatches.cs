using System;
using HarmonyLib;
using InDappledGroves.Blocks;
using Vintagestory.API.Common;

namespace indappedupgroves;

public class IdgPatches
{
    [HarmonyPatch]
    [HarmonyPatchCategory("indappedupgroves")]
    internal static class IdgFixPatches
    {
        private static readonly Type WorkstationType = AccessTools.TypeByName("InDappledGroves.Blocks.IDGWorkstation");
        
        [HarmonyPriority(Priority.Low)]
        [HarmonyPatch("InDappledGroves.Blocks.IDGWorkstation", "OnBlockInteractStart")]
        public static bool OnBlockInteractStart(object __instance, ref bool __result, IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (WorkstationType.IsInstanceOfType(__instance))
            {
                var beworkstation = AccessTools.Field(WorkstationType, "beworkstation");
                var parent = WorkstationType.BaseType!;
                var baseInteract = AccessTools.Method(parent, "OnBlockInteractStart");
                var resolvedbe = world.BlockAccessor.GetBlockEntity(blockSel.Position);
                beworkstation.SetValue(__instance, resolvedbe);
                if (resolvedbe == null)
                {
                    var result = (bool)baseInteract.Invoke(__instance, [world, byPlayer, blockSel.Position]); 
                    __result = result;
                    return false;
                }
            }
            

            __result = true;
            return false;
        }
    }
}