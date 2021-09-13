using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Tilemaps;

public static class TilemapSaveSystem
{
    public static string SavePath = Path.Combine(Application.persistentDataPath, "Saves/Maps");

	public static void Save(string mapName, ClassroomTilemap[] classroomTilemaps)
	{
        // Clear the save folder of that map name
        string savePath = Path.Combine(SavePath, mapName);
        if (Directory.Exists(savePath))
		{
            Directory.Delete(savePath, true);
		}

        // Save the map in the folder
        if (classroomTilemaps.Length > 0)
		{
            foreach (ClassroomTilemap c in classroomTilemaps)
            {
                SaveTilemap(mapName, c.Tilemap);
            }
            string json = JsonUtilityArrayHelper.ToJson(classroomTilemaps);
            SaveJSON(mapName, "map.mapdata", json);
        }
        else
		{
            throw new System.Exception("Zero input given");
		}
	}

    public static ClassroomTilemap[] Load(string mapName, Transform parent = null)
	{
        string json = LoadJSON(mapName, "map.mapdata");

        ClassroomTilemap[] classroomTilemaps = JsonUtilityArrayHelper.FromJson<ClassroomTilemap>(json);

        if (classroomTilemaps.Length > 0)
        {
            foreach (ClassroomTilemap c in classroomTilemaps)
            {
                c.CreateTilemap(parent, true);
                LoadTilemap(mapName, c.Tilemap);
            }
        }
        else
		{
            Debug.LogWarning($"WARNING: No map data was collected for map {mapName}.");
		}
        
        return classroomTilemaps;
    }

    public static void SaveTilemap(string mapName, Tilemap tilemap)
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

        string json = JsonUtilityArrayHelper.ToJson(TileDataList.ToArray(), true);
        SaveJSON(mapName, tilemap.name + ".tiledata", json);
    }

    public static void LoadTilemap(string mapName, Tilemap tilemap)
    {
        tilemap.ClearAllTiles();
        string json = LoadJSON(mapName, tilemap.name + ".tiledata");
        List<TileSaveData> TileDataList = JsonUtilityArrayHelper.FromJson<TileSaveData>(json).ToList();
        SetTilemap(tilemap, TileDataList);
    }

    public static void SetTilemap(Tilemap tilemap, List<TileSaveData> tileSaveDataList)
    {
        Tile[] tileAsset = Resources.LoadAll<Tile>("Tiles");

        foreach (TileSaveData tile in tileSaveDataList)
        {
            for (int i = 0; i <= tileAsset.Length; i++)
            {
                if (tileAsset[i].name == tile.tileBaseName)
                {
                    tilemap.SetTile(tile.cellPos, tileAsset[i]);
                    break;
                }
            }
        }
        Resources.UnloadUnusedAssets();
    }

    private static void SaveJSON(string folderName, string fileNameWithSuffix, string JSON)
	{
        string path = Path.Combine(SavePath, folderName); 
        if (!Directory.Exists(path))
		{
            Directory.CreateDirectory(path);
		}
        path = Path.Combine(path, fileNameWithSuffix);

        File.WriteAllText(path, JSON);
    }

    private static string LoadJSON(string folderName, string fileNameWithSuffix)
    {
        string path = Path.Combine(SavePath, folderName, fileNameWithSuffix);

        return File.ReadAllText(path);
    }

    public static void DeleteAllData()
    {
        string path = Path.Combine(Application.persistentDataPath, "Saves/Tilemaps");
        DirectoryInfo directory = new DirectoryInfo(path);
        directory.Delete(true);
    }
}

