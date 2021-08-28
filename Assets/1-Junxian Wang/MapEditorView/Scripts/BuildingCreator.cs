using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public enum PaintMode
{
	Brush,
	Line,
	Box
}

public class BuildingCreator : MonoBehaviour
{
	[SerializeField] Transform gridTransform;
	[SerializeField] BuildingObjectBase eraser;
	[SerializeField] List<BuildingObjectBase> buildings;
	[SerializeField] List<BuildingObjectCategory> categories;
	PlayerControl controls;
	Camera camera_;

	Tilemap previewMap;
	Tilemap defaultMap;

	Tile previewTile;
	Vector2 mousePos;
	Vector3Int mouseGridPosLast;
	Vector3Int mouseGridPosCurr;
	bool isMouseHolding;
	Vector3Int mouseGridPosHoldStart;

	BuildingObjectBase builderSelected;
	PaintMode paintModeSelected = PaintMode.Brush;

	public List<BuildingObjectCategory> Categories
	{
		get { return categories; }
	}

	public List<BuildingObjectBase> Buildings
	{
		get { return buildings; }
	}

	private void Awake()
	{
		controls = new PlayerControl();
		controls.MapEditor.MousePosChange.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
		controls.MapEditor.ClearSelection.performed += ctx => SelectBuilder(null);
		controls.MapEditor.Paint.started += ctx => paintStartHandler();
		controls.MapEditor.Paint.canceled += ctx => paintEndHandler();

		camera_ = Camera.main;
	}

	private void Start()
	{
		InitializeTilemaps();
	}

	private void Update()
	{
		// When some buildable is selected
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

	public void SelectBuilder(BuildingObjectBase builder)
	{
		builderSelected = builder;
		previewTile = builder != null ? builder.Tile : null;
		updatePreview();
	}

	public void SelectPaintMode(PaintMode mode)
	{
		paintModeSelected = mode;
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
			drawTile(mouseGridPosCurr, Tilemap);
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
				drawTile(mouseGridPosCurr, Tilemap);
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
					drawBox(Tilemap); // Draw box ending at current mouse pos
				}
			}

			if (paintModeSelected == PaintMode.Line)
			{
				if (builderSelected != null && !EventSystem.current.IsPointerOverGameObject())
				{
					drawLine(Tilemap); // Draw line ending at current mouse pos
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
			foreach (BuildingObjectCategory c in categories)
			{
				c.Tilemap.SetTile(pos, null);
			}

		}
		// Normal drawing
		else
		{
			map.SetTile(pos, previewTile);
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
		Vector3 worldPos = camera_.ScreenToWorldPoint(mousePos);
		return previewMap.WorldToCell(worldPos);
	}

	private Tilemap Tilemap
	{
		get
		{
			if (builderSelected != null && builderSelected.Category != null && builderSelected.Category.Tilemap != null)
			{
				return builderSelected.Category.Tilemap;
			}
			else
			{
				return defaultMap;
			}
		}
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
		foreach (BuildingObjectCategory category in categories)
		{
			category.TilemapObject.transform.SetParent(gridTransform);
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
