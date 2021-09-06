using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public enum PaintMode
{
	Brush,
	Line,
	Box
}

public class BuildingCreator : MonoBehaviour
{
	[SerializeField] Transform gridTransform;
	[SerializeField] Tile eraser;

	Tile[] tileAssets;
	TilemapConstructor[] tilemapConstructors;

	PlayerControl controls;

	Tilemap previewMap;
	Tilemap defaultMap;

	Tile builderSelected;
	Tilemap tilemapSelected;

	Vector2 mousePos;
	Vector3Int mouseGridPosLast;
	Vector3Int mouseGridPosCurr;
	bool isMouseHolding;
	Vector3Int mouseGridPosHoldStart;

	PaintMode paintModeSelected = PaintMode.Brush;

	public TilemapConstructor[] TilemapConstructors
	{
		get { return tilemapConstructors; }
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
		controls = new PlayerControl();
		controls.MapEditor.MousePosChange.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
		controls.MapEditor.ClearSelection.performed += ctx => SelectBuilder(null);
		controls.MapEditor.Paint.started += ctx => paintStartHandler();
		controls.MapEditor.Paint.canceled += ctx => paintEndHandler();

		// Load all tile assets
		tileAssets = Resources.LoadAll<Tile>("Tiles");

		// Load all tilemap constructor assets
		tilemapConstructors = Resources.LoadAll<TilemapConstructor>("Scriptables/TilemapConstructors");
	}

	private void Start()
	{
		InitializeTilemaps();
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
	}

	public void SelectCategory(int index)
	{
		tilemapSelected = tilemapConstructors[index].Tilemap;
	}

	public void SelectPaintMode(PaintMode mode)
	{
		paintModeSelected = mode;
	}

	public void SelectEraser()
	{
		SelectBuilder(eraser);
	}

	public void ClearTilemaps()
	{
		foreach (TilemapConstructor t in tilemapConstructors)
		{
			t.Tilemap.ClearAllTiles();
		}
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
	private void paintStartHandler()
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

	private void paintEndHandler()
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
		if (builderSelected == eraser && map != previewMap)
		{
			foreach (TilemapConstructor c in tilemapConstructors)
			{
				Debug.Log("1");
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
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
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

		// Initialize category's maps
		foreach (TilemapConstructor c in tilemapConstructors)
		{
			c.CreateTilemap();
			c.TilemapObject.transform.SetParent(gridTransform);
		}
	}

	private void OnEnable()
	{
		controls.Enable();
	}

	private void OnDisable()
	{
		controls.Disable();
	}
}
