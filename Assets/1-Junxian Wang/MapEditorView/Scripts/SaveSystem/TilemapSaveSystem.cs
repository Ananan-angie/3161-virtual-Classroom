using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Tilemaps;

public static class TilemapSaveSystem
{
    public static string DefaultSavePath = Path.Combine(Application.persistentDataPath, "Saves/Maps");
    public static string Suffix_Map = "mapdata";
    public static string Suffix_Tilemap = "tiledata";

	public static void Save(string mapPath, string mapName, ClassroomTilemap[] classroomTilemaps)
	{
        string path = Path.Combine(mapPath, mapName);
        // Clear the save folder of that map name
        if (Directory.Exists(path))
		{
            Directory.Delete(path, true);
		}

        // Save the map in the folder
        if (classroomTilemaps.Length > 0)
		{
            foreach (ClassroomTilemap c in classroomTilemaps)
            {
                SaveTilemap(path, c.Tilemap);
            }
            string json = JsonUtilityArrayHelper.ToJson(classroomTilemaps);
            SaveJSON(path, "map." + Suffix_Map, json);
        }
        else
		{
            throw new System.Exception("Zero input given");
		}
	}

    public static ClassroomTilemap[] Load(string mapPath, Transform parent = null)
	{
        string json = LoadJSON(mapPath, "map." + Suffix_Map);

        ClassroomTilemap[] classroomTilemaps = JsonUtilityArrayHelper.FromJson<ClassroomTilemap>(json);

        if (classroomTilemaps.Length > 0)
        {
            foreach (ClassroomTilemap c in classroomTilemaps)
            {
                c.CreateTilemap(parent, true);
                LoadTilemap(mapPath, c.Tilemap);
            }
        }
        else
		{
            Debug.LogWarning($"WARNING: No map data was collected for map path {mapPath}.");
		}
        
        return classroomTilemaps;
    }

    public static void SaveTilemap(string mapPath, Tilemap tilemap)
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
        SaveJSON(mapPath, tilemap.name + "." + Suffix_Tilemap, json);
    }

    public static void LoadTilemap(string mapPath, Tilemap tilemap)
    {
        tilemap.ClearAllTiles();
        string json = LoadJSON(mapPath, tilemap.name + "." + Suffix_Tilemap);
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

    private static void SaveJSON(string folderPath, string fileNameWithSuffix, string JSON)
	{
        if (!Directory.Exists(folderPath))
		{
            Directory.CreateDirectory(folderPath);
		}
        string path = Path.Combine(folderPath, fileNameWithSuffix);
        File.WriteAllText(path, JSON);
    }

    private static string LoadJSON(string folderPath, string fileNameWithSuffix)
    {
        string path = Path.Combine(folderPath, fileNameWithSuffix);
        return File.ReadAllText(path);
    }

    public static void DeleteAllData()
    {
        DirectoryInfo directory = new DirectoryInfo(DefaultSavePath);
        directory.Delete(true);
    }
}

