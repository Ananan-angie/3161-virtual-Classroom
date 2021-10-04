using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class TransparentWindow : MonoBehaviour
{
	/*
	 * Make sure to make the following configurations in the Unity Editor
	 *		1. Set Camera background to be all 0s for R G B A
	 *		2. Deselect the option that uses D3D11 Flip Model Swapchain
	 *		3. Make it full screen (optional)
	 */
	private struct MARGINS
	{
		public int cxLeftWidth;
		public int cxRightWidth;
		public int cyTopHeight;
		public int cyBottomHeight;
	}

	[DllImport("user32.dll")]
	private static extern IntPtr GetActiveWindow();

	[DllImport("user32.dll")]
	private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

	[DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
	static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);

	[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
	private static extern int SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int cx, int cy, int uFlags);

	[DllImport("Dwmapi.dll")]
	private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

	const int GWL_STYLE = -16;
	const int GWL_EXSTYLE = -20;
	const uint WS_POPUP = 0x80000000;
	const uint WS_VISIBLE = 0x10000000;
	static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
	const uint WS_EX_LAYERED = 0x00080000;
	const uint WS_EX_TRANSPARNET = 0x00000020;

	const int LWA_COLORKEY = 1;

	private IntPtr hwnd;

	void Start()
	{
#if !UNITY_EDITOR

		// Get window handle
		hwnd = GetActiveWindow();

		// Set the window transparnet
		var margins = new MARGINS() { cxLeftWidth = -1 };
		DwmExtendFrameIntoClientArea(hwnd, ref margins);

		// Remove window borders (not needed if application is full screen)
		SetWindowLong(hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);

		// Make window always on top (Set argument 5, 6 to window size if windowed)
		SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, Screen.width, Screen.height, 0);

		// Click through settings

			// Option A & C: All part of the window is click thorugh
			SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARNET);

			// Option B: Only part with a certain color is click-through and transparent, else fully opaque and not click-through
			//SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED); // Set window to be layered
			//SetLayeredWindowAttributes(hwnd, 0, 0, LWA_COLORKEY); // Set all pixels with color (0, 0, 0, 0) to fully transparent, otherwise fully opaque
#endif

		// Click through won't close the window
		Application.runInBackground = true;
	}

	// Option C: Only part with collider is not click-through, allow translucent colours
	private void Update()
	{
		SetClickThrough(!IsPointerOverUI());
	}

	private void SetClickThrough(bool clickthrough)
	{
		if (clickthrough)
			SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARNET); // Set window to be click thorugh
		else 
			SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED); // Set window to be not click thorugh
	}

	public static bool IsPointerOverUI()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return true;
		}
		else
		{
			PointerEventData pe = new PointerEventData(EventSystem.current);
			pe.position = Input.mousePosition;
			List<RaycastResult> hits = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pe, hits);
			return hits.Count > 0;
		}
	}
}