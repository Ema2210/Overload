using static UnityEngine.RuleTile;
using UnityEngine.Tilemaps;
using UnityEngine;

public class CustomRuleTile : RuleTile
{
    /*
    public enum TileType
    {
        Empty,
        Full,
    }

    public TileType tileType;

    public override bool RuleMatch(int neighbor, TileBase other)
    {
        if (other is RuleOverrideTile)
            other = (other as RuleOverrideTile).m_InstanceTile;

        // Check if the neighbor is a tile of the same type (Full or Empty)
        switch (neighbor)
        {
            case TilingRule.Neighbor.This:
                {
                    return other is CustomRuleTile
                        && (other as CustomRuleTile).tileType == this.tileType;
                }
            case TilingRule.Neighbor.NotThis:
                {
                    return !(other is CustomRuleTile
                        && (other as CustomRuleTile).tileType == this.tileType);
                }
        }

        return base.RuleMatch(neighbor, other);
    }*/
}

