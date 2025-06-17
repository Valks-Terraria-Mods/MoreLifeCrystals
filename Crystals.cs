using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace MoreLifeCrystals;

public class Config : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [DefaultValue(20)]
    public int LifeCrystalsAmount;
}

public class Crystals : ModSystem
{
    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
    {
        int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Life Crystals"));

        if (ShiniesIndex != -1)
        {
            tasks.Insert(ShiniesIndex + 1, new PassLegacy(nameof(MoreLifeCrystals), LifeCrystals));
        }
    }

    private void LifeCrystals(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Adding even more life crystals";

        int amount = ModContent.GetInstance<Config>().LifeCrystalsAmount;

        for (int n = 0; n < amount; n++)
        {
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(50, Main.maxTilesX - 50);
                int y = WorldGen.genRand.Next((int)GenVars.rockLayerLow, Main.maxTilesY);

                _ = WorldGen.AddLifeCrystal(x, y);
            }
        }
    }
}
