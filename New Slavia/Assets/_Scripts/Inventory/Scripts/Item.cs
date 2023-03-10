using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType {
        Apple,
        BullHeart,
        ShadowInABottle,
        BirchLeaves,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() {
        switch (itemType) {
            default:
            case ItemType.Apple: return ItemAssets.Instance.AppleSprite;
            case ItemType.BullHeart: return ItemAssets.Instance.BullHeartSprite;
            case ItemType.ShadowInABottle: return ItemAssets.Instance.ShadowInABottleSprite;
            case ItemType.BirchLeaves: return ItemAssets.Instance.BirchLeavesSprite;
        }
    }

    public bool IsStackable() {
        switch (itemType) {
            default:
            case ItemType.Apple:
            case ItemType.BullHeart:
            case ItemType.ShadowInABottle:
            case ItemType.BirchLeaves:
                return true;
        }
    }
}
