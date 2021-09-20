using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorViewInputController : InputController
{
	[SerializeField] PlayerController playerController;
	[SerializeField] MapEditorManager mapEditorManager;

	protected override void Awake()
	{
		base.Awake();

		Controls.MapEditor.MousePosChange.performed += ctx => mapEditorManager.MousePos = ctx.ReadValue<Vector2>();
		Controls.MapEditor.ClearSelection.performed += ctx => mapEditorManager.SelectBuilder(null);
		Controls.MapEditor.Paint.started += ctx => mapEditorManager.PaintStartHandler();
		Controls.MapEditor.Paint.canceled += ctx => mapEditorManager.PaintEndHandler();

		Controls.UI.Back.performed += ctx => SharedUtilities.Instance.BackToLastScene();
		Controls.Gameplay.Move.performed += ctx => playerController.MovementThisFrame = ctx.ReadValue<Vector2>();
		Controls.Gameplay.Move.canceled += ctx => playerController.MovementThisFrame = Vector2.zero;
		Controls.Gameplay.Interact.performed += ctx => playerController.OnInteract();
	}

}
