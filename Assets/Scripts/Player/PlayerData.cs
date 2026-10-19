using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerData : MonoBehaviour
{
    public List<PlayerSpriteMapping> characterSprites;
    public SpriteRenderer characterRenderer;

    public Sprite GetSprite(PlayerStateType spriteType, DirectionType directionType)
    {
        return characterSprites.FirstOrDefault(x => x.spriteType == spriteType && x.directionType == directionType)?.sprite;
    }
}
