using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tileset
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private Tilemap baseMap;

        [SerializeField] private List<TileBase> baseSprites;
        [SerializeField] private int sizeX = 20;
        [SerializeField] private int sizeY = 20;

#if UNITY_EDITOR
        [InspectorButton("GenerateMap")]
        public bool clickMe;

        private void GenerateMap()
        {
            baseMap.ClearAllTiles();
            for (int x = -sizeX; x < sizeX; x++)
            {
                for (int y = -sizeY; y < sizeY; y++)
                {
                    baseMap.SetTile(new Vector3Int(x, y), baseSprites[Random.Range(0, baseSprites.Count)]);
                }
            }
        
        }
#endif
    }
}
