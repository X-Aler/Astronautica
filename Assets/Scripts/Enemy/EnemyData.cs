using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class EnemyData : MonoBehaviour
{
    public List<EnemySpriteMap> sprites;
    public SpriteRenderer render;

    public Sprite GetSprite(EnemyStateType state, DirectionType direction)
    {
        return sprites.FirstOrDefault(x => x.state == state && x.direction == direction)?.sprite;
    }
}
