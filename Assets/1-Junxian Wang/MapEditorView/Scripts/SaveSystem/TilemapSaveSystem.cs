using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Tilemaps;

public static class TilemapSaveSystem
{
	public static void Save(TilemapConstructor[] tilemapConstructors)
	{
        if (tilemapConstructors.Length > 0)
		{
            foreach (TilemapConstructor c in tilemapConstructors)
            {
                SaveTilemap(c.name, c.Tilemap);
            }
        }
        else
		{
            throw new System.Exception("Tile map constructors not initallized, need to create (load) tile maps using this object first");
		}
	}

    public static void Load(TilemapConstructor[] tilemapConstructors)
	{
        if (tilemapConstructors.Length > 0)
        {
            foreach (TilemapConstructor c in tilemapConstructors)
            {
                if (c.TilemapObject == null) c.CreateTilemap();
                LoadTilemap(c.name, c.Tilemap);
            }
        }
    }

    public static void SaveTilemap(string saveName, Tilemap tilemap)
    {
        List<TileSaveData> TileDataList = new List<TileSaveData>();

        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            var lPos = new Vector3Int(pos.x, pos.y, pos.z);

            if (!tilemap.HasTile(lPos)) continue;

            TileSaveData tileData = new TileSaveData()
            {
                cellPos = lPos,
                worldPos = tilemap.CellToWorld(lPos),
                tileBaseName = tilemap.GetTile(lPos).name,
            };

            TileDataList.Add(tileData);
        }
        SaveTileData(saveName, TileDataList);
    }

    public static void LoadTilemap(string saveName, Tilemap tilemap)
    {
        tilemap.ClearAllTiles();
        List<TileSaveData> TileDataList = LoadTileData(saveName);
        SetTilemap(tilemap, TileDataList);
    }

    public static void SetTilemap(Tilemap tileMap, List<TileSaveData> tileSaveDataList)
    {
        Tile[] tileAsset = Resources.LoadAll<Tile>("Tiles");

        foreach (TileSaveData tile in tileSaveDataList)
        {
            for (int i = 0; i <= tileAsset.Length; i++)
            {
                if (tileAsset[i].name == tile.tileBaseName)
                {
                    tileMap.SetTile(tile.cellPos, tileAsset[i]);
                    break;
                }
            }
        }
        Resources.UnloadUnusedAssets();
    }


    public static void SaveTileData(string saveName, List<TileSaveData> tilesToSave)
    {
        string path = Path.Combine(Application.persistentDataPath, "Saves/Tilemaps");

        if(!Directory.Exists(path)){
            Directory.CreateDirectory (path);
        }

        path = Path.Combine(path, saveName + ".humble");   

        string saveJson = JsonHelper.ToJson(tilesToSave.ToArray(), true);

        File.WriteAllText(path, saveJson);
    }

    public static List<TileSaveData> LoadTileData(string saveName)
    {
        string path = Path.Combine(Application.persistentDataPath, "Saves/Tilemaps");
        path = Path.Combine(path, saveName + ".humble");

        if (File.Exists(path))
        {
            string loadJson = File.ReadAllText(path);
            
            List<TileSaveData> loadTiles = JsonHelper.FromJson<TileSaveData>(loadJson).ToList();

            return loadTiles;
        } 
        else
        {
            Debug.LogError("Save file not found.");
            return null;
        }
    }

    public static void DeleteAllData()
    {
        string path = Path.Combine(Application.persistentDataPath, "Saves/Tilemaps");
        DirectoryInfo directory = new DirectoryInfo(path);
        directory.Delete(true);
    }
}
