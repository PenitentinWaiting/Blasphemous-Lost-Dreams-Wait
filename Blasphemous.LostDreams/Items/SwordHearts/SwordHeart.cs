﻿using Blasphemous.Framework.Items;
using UnityEngine;

namespace Blasphemous.LostDreams.Items.SwordHearts;

internal class SwordHeart : ModSwordHeart
{
    private readonly EffectOnEquip _effect;

    public bool IsEquipped => _effect.IsEquipped;

    public SwordHeart(EffectOnEquip effect)
    {
        Id = effect.GetType().Name;
        AddEffect(_effect = effect);
    }

    protected override string Id { get; }

    protected override string Name => Main.LostDreams.LocalizationHandler.Localize(Id + ".n");

    protected override string Description => Main.LostDreams.LocalizationHandler.Localize(Id + ".d");

    protected override string Lore => Main.LostDreams.LocalizationHandler.Localize(Id + ".l");

    protected override Sprite Picture => Main.LostDreams.FileHandler.LoadDataAsSprite($"swords/{Id}.png", out Sprite picture) ? picture : null;

    protected override bool CarryOnStart => false;

    protected override bool PreserveInNGPlus => true;

    protected override bool AddToPercentCompletion => true;

    protected override bool AddInventorySlot => true;
}
