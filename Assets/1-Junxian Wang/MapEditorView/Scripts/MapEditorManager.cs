using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public enum PaintMode
{
	Brush,
	Line,
	Box
}

public class MapEditorManager : MonoBehaviour
{
	[SerializeField] Transform gridTransform;
	public Tile eraser;
	[SerializeField] MapEditorViewUIManager UIManager;

	Tile[] tileAssets;
	List<ClassroomTilemap> classroomTilemaps = new List<ClassroomTilemap>();

	Tilemap previewMap;
	Tilemap defaultMap;

	Tile builderSelected;
	Tilemap tilemapSelected;
	string mapSelected;

	[HideInInspector] public Vector2 MousePos;
	Vector3Int mouseGridPosLast;
	Vector3Int mouseGridPosCurr;
	bool isMouseHolding;
	Vector3Int mouseGridPosHoldStart;

	int roomCount = 0;
	public ClassroomTilemap VisibleRoomLayer;

	bool isEraser;

	PaintMode paintModeSelected = PaintMode.Brush;

	static ClassroomTilemap[] defaultClassroomTilemaps =
	{
		new ClassroomTilemap(
			name: "Floor", 
			layerOrder: 1),

		new ClassroomTilemap(
			name: "Wall", 
			layerOrder: 2, 
			isCollidable: true),

		new ClassroomTilemap(
			name: "Listener Chair", 
			layerOrder: 2, 
			isTrigger: true,
			triggerTag: "Listener Chair"),

		new ClassroomTilemap(
			name: "Presenter Chair",
			layerOrder: 2,
			isTrigger: true,
			triggerTag: "Presenter Chair")
	};

	public List<ClassroomTilemap> ClassroomTilemaps
	{
		get { return classroomTilemaps; }
	}

	public Tile[] TileAssets
	{
		get { return tileAssets; }
	}

	public Tilemap TilemapSelected
	{
		get { return tilemapSelected; }
		set { tilemapSelected = value; }
	}

	private void Awake()
	{
		// Load all tile assets
		tileAssets = Resources.LoadAll<Tile>("Tiles");

		InitializeTilemaps();
		NewMap();
		tilemapSelected = defaultMap;
	}

	private void Update()
	{
		// When some builder tile is selected
		if (builderSelected != null)
		{
			Vector3Int gridPos = getMouseGridPos();

			// On mouse cell change
			if (gridPos != mouseGridPosCurr)
			{
				// Update last and current cell
				mouseGridPosLast = mouseGridPosCurr;
				mouseGridPosCurr = gridPos;

				// Update preview of tiles
				updatePreview();

				// Update paint for each change when brush mode is selected
				updateBrushPaint();
			}
		}
	}

	public void SelectBuilder(Tile builder)
	{
		builderSelected = builder;
		updatePreview();
		isEraser = false;
	}

	public void SelectLayer(int index)
	{
		SelectLayer(classroomTilemaps[index]);
	}

	public void SelectLayer(ClassroomTilemap tilemap)
	{
		tilemapSelected = tilemap.Tilemap;
	}

	public void SelectPaintMode(PaintMode mode)
	{
		paintModeSelected = mode;
	}

	public void SelectEraser()
	{
		SelectBuilder(eraser);
		isEraser = true;
	}

	private void updatePreview()
	{
		// Always update pointer preview
		previewMap.SetTile(mouseGridPosLast, null);
		drawTile(mouseGridPosCurr, previewMap);

		// Update box preview
		if (paintModeSelected == PaintMode.Box && isMouseHolding)
		{
			previewMap.ClearAllTiles();
			drawBox(previewMap); // Draw box ending at current mouse pos
		}

		// Update line preveiw
		if (paintModeSelected == PaintMode.Line && isMouseHolding)
		{
			previewMap.ClearAllTiles();
			drawLine(previewMap); // Draw line ending at current mouse pos
		}
	}

	private void updateBrushPaint()
	{
		if (paintModeSelected == PaintMode.Brush && isMouseHolding)
		{
			drawTile(mouseGridPosCurr, tilemapSelected);
		}
	}

	/*
	 * Handles drawing action
	 *	Brush mode:
	 *		Records button hold state
	 *	Line mode:
	 *		Same as box mode
	 *	Box mode:
	 *		Record hold start pos
	 *		Draws box when hold ends
	 */
	public void PaintStartHandler()
	{
		if (builderSelected != null && !EventSystem.current.IsPointerOverGameObject())
		{
			isMouseHolding = true;

			mouseGridPosHoldStart = getMouseGridPos();

			// Draw tile as soon as it is pressed down, as in update function 
			// tile is only drawn after the cursor's cell position is changed.
			if (paintModeSelected == PaintMode.Brush)
			{
				drawTile(mouseGridPosCurr, tilemapSelected);
			}
		}
		else
		{
			isMouseHolding = false;
		}
	}

	public void PaintEndHandler()
	{
		if (isMouseHolding == true)
		{
			// If paint mode is not brush (meaning multiple preview tiles present)
			if (paintModeSelected != PaintMode.Brush)
			{
				previewMap.ClearAllTiles(); // Clear all currently drawn preview tiles
			}

			if (paintModeSelected == PaintMode.Box) 
			{
				if (builderSelected != null && !EventSystem.current.IsPointerOverGameObject())
				{
					drawBox(tilemapSelected); // Draw box ending at current mouse pos
				}
			}

			if (paintModeSelected == PaintMode.Line)
			{
				if (builderSelected != null && !EventSystem.current.IsPointerOverGameObject())
				{
					drawLine(tilemapSelected); // Draw line ending at current mouse pos
				}
			}
		}

		isMouseHolding = false;
	}

	private void drawTile(Vector3Int pos, Tilemap map)
	{
		// Handle eraser
		if (isEraser && map != previewMap)
		{
			foreach (ClassroomTilemap c in classroomTilemaps)
			{
				c.Tilemap.SetTile(pos, null);
			}
		}
		// Normal drawing
		else
		{
			map.SetTile(pos, builderSelected);
		}
	}

	private void drawBox(int x1, int y1, int x2, int y2, Tilemap map)
	{
		for (int x = x1; x <= x2; x++)
		{
			for (int y = y1; y <= y2; y++)
			{
				drawTile(new Vector3Int(x, y, 0), map);
			}
		}
	}

	private void drawBox(Tilemap map)
	{
		Vector3Int endPos = getMouseGridPos();
		int x1 = Mathf.Min(mouseGridPosHoldStart.x, endPos.x);
		int y1 = Mathf.Min(mouseGridPosHoldStart.y, endPos.y);
		int x2 = Mathf.Max(mouseGridPosHoldStart.x, endPos.x);
		int y2 = Mathf.Max(mouseGridPosHoldStart.y, endPos.y);
		drawBox(x1, y1, x2, y2, map);
	}

	private void drawLine(Tilemap map)
	{
		Vector3Int endPos = getMouseGridPos();
		int x1 = Mathf.Min(mouseGridPosHoldStart.x, endPos.x);
		int y1 = Mathf.Min(mouseGridPosHoldStart.y, endPos.y);
		int x2 = Mathf.Max(mouseGridPosHoldStart.x, endPos.x);
		int y2 = Mathf.Max(mouseGridPosHoldStart.y, endPos.y);
		bool isLineHorizontal = x2 - x1 >= y2 - y1;
		if (isLineHorizontal)
		{
			drawBox(x1, mouseGridPosHoldStart.y, x2, mouseGridPosHoldStart.y, map);
		}
		else
		{
			drawBox(mouseGridPosHoldStart.x, y1, mouseGridPosHoldStart.x, y2, map);
		}
	}

	private Vector3Int getMouseGridPos()
	{
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(MousePos);
		return previewMap.WorldToCell(worldPos);
	}

	private void InitializeTilemaps()
	{
		GameObject obj;
		TilemapRenderer tr;

		// Add preview map
		obj = new GameObject("Tilemap_Preview");
		previewMap = obj.AddComponent<Tilemap>();
		tr = obj.AddComponent<TilemapRenderer>();
		tr.sortingOrder = 99;
		obj.transform.SetParent(gridTransform);

		// Add default map
		obj = new GameObject("Tilemap_Default");
		defaultMap = obj.AddComponent<Tilemap>();
		tr = obj.AddComponent<TilemapRenderer>();
		tr.sortingOrder = 0;
		obj.transform.SetParent(gridTransform);
	}

	public void NewMap(string mapName = "NewMap")
	{
		// Remove all currently created tilemaps
		foreach (ClassroomTilemap c in classroomTilemaps)
		{
			Destroy(c.TilemapObject);
			c.ResetTilemapObject();
		}

		// Reinitialize default tilemaps
		foreach (ClassroomTilemap c in defaultClassroomTilemaps)
		{
			c.CreateTilemap(gridTransform);
		}
		classroomTilemaps = defaultClassroomTilemaps.ToList();

		UIManager.UpdateCategoryDropdown();

		ChangeMapName(mapName);
	}

	public void LoadMap(string mapPath)
	{
		// Try to load map
		ClassroomTilemap[] loadMap;
		try
		{
			loadMap = TilemapSaveSystem.Load(mapPath, gridTransform);
		}
		catch (DirectoryNotFoundException)
		{
			Debug.Log($"Map {Path.GetFileName(mapPath)} is not found.");
			return;
		}

		// Remove all currently created tilemaps
		foreach (ClassroomTilemap c in classroomTilemaps)
		{
			Destroy(c.TilemapObject);
		}

		classroomTilemaps = loadMap.ToList();
		UIManager.UpdateCategoryDropdown();

		ChangeMapName(Path.GetFileName(mapPath));

		Debug.Log($"Loaded map {Path.GetFileName(mapPath)}");
	}

	public void SaveMap(string mapPath)
	{
		TilemapSaveSystem.Save(mapPath, mapSelected, classroomTilemaps.ToArray());
		Debug.Log($"Saved map {Path.GetFileName(mapPath)}");
	}

	public void ChangeMapName(string mapName)
	{
		mapSelected = mapName;
		UIManager.SetMapText(mapName);
	}

	public void CreateNewRoom()
	{
		roomCount++;

		ClassroomTilemap room = new ClassroomTilemap(
			name: "Room " + roomCount,
			layerOrder: 2,
			isTrigger: true,
			triggerTag: "Room",
			roomID: roomCount);

		classroomTilemaps.Add(room);
		room.CreateTilemap(gridTransform);

		UIManager.UpdateDropdownRooms();

		room.TilemapObject.SetActive(false);
	}

	public void SelectRoom(ClassroomTilemap room)
	{
		SelectLayer(room);
		SelectBuilder(eraser);

		try
		{
			VisibleRoomLayer.TilemapObject.SetActive(false);
		}
		catch (Exception) { }

		VisibleRoomLayer = room;
		room.TilemapObject.SetActive(true);
	}
}
