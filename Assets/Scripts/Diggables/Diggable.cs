using UnityEngine;

[CreateAssetMenu(fileName = "NewDiggable", menuName = "Diggable/Create New Diggable", order = 0)]
public class Diggable : ScriptableObject
{
    [field: SerializeField] public TileType TileType { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public DiggableCatergoryEnum Category { get; private set; }
    [field: SerializeField] public float ValuePerUnit { get; private set; }
    [field: SerializeField] public Sprite UISprite { get; private set; }
}


