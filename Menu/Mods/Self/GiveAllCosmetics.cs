using GorillaNetworking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Menu.Mods.Self
{
    public class GiveAllCosmetics
    {
        public static void Run()
        {
            if (CosmeticsController.hasInstance)
            {
                foreach (CosmeticsController.CosmeticItem cosmetic in CosmeticsController.instance.allCosmetics)
                {
                    if (!GorillaTagger.Instance.offlineVRRig.concatStringOfCosmeticsAllowed.Contains(cosmetic.itemName))
                        GorillaTagger.Instance.offlineVRRig.concatStringOfCosmeticsAllowed += cosmetic.itemName;

                    if (!CosmeticsController.instance.unlockedCosmetics.Contains(cosmetic))
                        CosmeticsController.instance.unlockedCosmetics.Add(cosmetic);

                    if (cosmetic.itemCategory == CosmeticsController.CosmeticCategory.Arms)
                        if (!CosmeticsController.instance.unlockedArms.Contains(cosmetic))
                            CosmeticsController.instance.unlockedArms.Add(cosmetic);

                    if (cosmetic.itemCategory == CosmeticsController.CosmeticCategory.Back)
                        if (!CosmeticsController.instance.unlockedBacks.Contains(cosmetic))
                            CosmeticsController.instance.unlockedBacks.Add(cosmetic);

                    if (cosmetic.itemCategory == CosmeticsController.CosmeticCategory.Badge)
                        if (!CosmeticsController.instance.unlockedBadges.Contains(cosmetic))
                            CosmeticsController.instance.unlockedBadges.Add(cosmetic);

                    if (cosmetic.itemCategory == CosmeticsController.CosmeticCategory.Chest)
                        if (!CosmeticsController.instance.unlockedChests.Contains(cosmetic))
                            CosmeticsController.instance.unlockedChests.Add(cosmetic);

                    if (cosmetic.itemCategory == CosmeticsController.CosmeticCategory.Face)
                        if (!CosmeticsController.instance.unlockedFaces.Contains(cosmetic))
                            CosmeticsController.instance.unlockedFaces.Add(cosmetic);

                    if (cosmetic.itemCategory == CosmeticsController.CosmeticCategory.Fur)
                        if (!CosmeticsController.instance.unlockedFurs.Contains(cosmetic))
                            CosmeticsController.instance.unlockedFurs.Add(cosmetic);

                    if (cosmetic.itemCategory == CosmeticsController.CosmeticCategory.Hat)
                        if (!CosmeticsController.instance.unlockedHats.Contains(cosmetic))
                            CosmeticsController.instance.unlockedHats.Add(cosmetic);

                    if (cosmetic.itemCategory == CosmeticsController.CosmeticCategory.Pants)
                        if (!CosmeticsController.instance.unlockedPants.Contains(cosmetic))
                            CosmeticsController.instance.unlockedPants.Add(cosmetic);

                    if (cosmetic.itemCategory == CosmeticsController.CosmeticCategory.Paw)
                        if (!CosmeticsController.instance.unlockedPaws.Contains(cosmetic))
                            CosmeticsController.instance.unlockedPaws.Add(cosmetic);

                    if (cosmetic.itemCategory == CosmeticsController.CosmeticCategory.Shirt)
                        if (!CosmeticsController.instance.unlockedShirts.Contains(cosmetic))
                            CosmeticsController.instance.unlockedShirts.Add(cosmetic);
                }
            }
        }
    }
}
