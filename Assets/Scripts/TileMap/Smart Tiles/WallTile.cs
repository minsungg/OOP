using Referencing.Scriptable_Assets;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TileMap.Smart_Tiles
{
	[CreateAssetMenu]
	public class WallTile : ScriptableTileBase
	{
		public SpriteSlot[] spriteSlots;

		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			if (spriteSlots.Length < 13) return;
			int mask = (HasWall(position + Vector3Int.up, tilemap) ? 1 : 0)
				+ (HasWall(position + Vector3Int.right, tilemap) ? 2 : 0)
				+ (HasWall(position + Vector3Int.down, tilemap) ? 4 : 0)
				+ (HasWall(position + Vector3Int.left, tilemap) ? 8 : 0);

			int mask2 = (!HasWall(position + new Vector3Int(1, 1, 0), tilemap) ? 1 : 0)
				+ (!HasWall(position + new Vector3Int(1, -1, 0), tilemap) ? 2 : 0)
				+ (!HasWall(position + new Vector3Int(-1, -1, 0), tilemap) ? 4 : 0)
				+ (!HasWall(position + new Vector3Int(-1, 1, 0), tilemap) ? 8 : 0);

			SpriteSlot slot = spriteSlots[4];
			switch (mask)
			{
				case  3: slot = spriteSlots[6]; break;
				case  6: slot = spriteSlots[0]; break;
				case  7: slot = spriteSlots[3]; break;
				case  9: slot = spriteSlots[8]; break;
				case 11: slot = spriteSlots[7]; break;
				case 12: slot = spriteSlots[2]; break;
				case 13: slot = spriteSlots[5]; break;
				case 14: slot = spriteSlots[1]; break;
				case 15:
					switch (mask2)
					{
						case 1: slot = spriteSlots[11]; break;
						case 2: slot = spriteSlots[9]; break;
						case 4: slot = spriteSlots[10]; break;
						case 8: slot = spriteSlots[12]; break;
					}
					break;
			}

			if (slot.sprites.Count > 0)
			{
				Random.InitState(position.GetHashCode());
				int total = slot.sprites.Sum(x => x.probability);
           
				int[] indices = new int[total];
				int spriteIndex = 0;
				int indiceIndex = 0;
				foreach (var s in slot.sprites)
				{
					for (int index = 0; index < s.probability; index++)
						indices[indiceIndex++] = spriteIndex;
					spriteIndex++;
				}
				int random = Mathf.FloatToHalf(Random.value*total);
				int finalIndex = indices[Mathf.Clamp(random%total, 0, total - 1)];
				tileData.sprite = slot.sprites[Mathf.Clamp(finalIndex, 0, slot.sprites.Count-1)].sprite;
			}
			tileData.flags = TileFlags.LockAll;
			tileData.colliderType = mask != 15 ? Tile.ColliderType.Grid : Tile.ColliderType.None;
		}

		public override void RefreshTile(Vector3Int position, ITilemap tilemap)
		{
			foreach (var p in new BoundsInt(-1,-1, 0, 3, 3, 1).allPositionsWithin)
			{
				tilemap.RefreshTile(position + p);	
			}
		}

		public bool HasWall(Vector3Int position, ITilemap tilemap)
		{
			return tilemap.GetTile(position) == this;
		}

		[System.Serializable]
		public class SpriteSlot {
			public List<SpriteSlotItem> sprites; 
		}

		[System.Serializable]
		public class SpriteSlotItem
		{
			public Sprite sprite;
			public int probability = 1;
		}
	}
}
