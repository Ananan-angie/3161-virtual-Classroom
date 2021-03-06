// GENERATED AUTOMATICALLY FROM 'Assets/Input Control/PlayerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""18531443-9654-4fe7-9ee7-a967726a7685"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""769eb785-3db8-4a3b-977a-bc39149bea89"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""e5554378-bcec-4986-ab04-6f9e107186f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WSAD"",
                    ""id"": ""67448997-4ca3-4acb-bf15-032fc3c51c84"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2032c5a7-9833-4e8c-8879-b7170357838c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0f3b0443-a4de-4cd9-9625-f3c6bc037a6a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ce890581-9534-4456-b977-7fb134984bfd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""45d8e7a6-8550-4aaa-9f0e-e17f5e499fad"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WSAD"",
                    ""id"": ""5347df36-6ba0-49e9-9495-f20f6f41ce55"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0d4145e7-ec42-4dd4-8181-29deda11fb7c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e3df9851-9348-457d-88dd-54ca4acb0e63"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cc5148a5-ae76-4dfd-b976-3a4c4ebd9ba3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""da6aedb2-15c2-445f-800d-4721e40bfffc"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6c79d31f-a6ae-490f-b26a-e2403b4433ad"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""3fce7684-8c57-4dec-b4eb-27c58dd2f76a"",
            ""actions"": [
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""03833d85-b1ac-4314-8712-52ef15e3525d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Chating"",
                    ""type"": ""Button"",
                    ""id"": ""8ffef716-8029-4c43-a4de-215a2322c822"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c1057c00-23bc-4d11-bcb0-5f1a5078031f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9bfc97bb-ba15-40d6-8c1e-be260686b464"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Chating"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MapEditor"",
            ""id"": ""d2eb0fd0-4da7-4398-a097-e6b7f575513b"",
            ""actions"": [
                {
                    ""name"": ""Paint"",
                    ""type"": ""Button"",
                    ""id"": ""69db8e31-96f0-49c0-accd-05f925b90f63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ClearSelection"",
                    ""type"": ""Button"",
                    ""id"": ""6fa67707-15cf-4c42-b741-2372854077f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosChange"",
                    ""type"": ""Value"",
                    ""id"": ""580ed227-6439-4df7-b76f-70531e4b3349"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fdaf92b1-d69a-4b83-b459-15c8c95c31aa"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Paint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0242e67-8be2-43a8-95e0-cc110ee2473e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClearSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40408524-3dcf-4d0e-98a9-c680fca8cf78"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosChange"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""New control scheme"",
            ""bindingGroup"": ""New control scheme"",
            ""devices"": []
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Interact = m_Gameplay.FindAction("Interact", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Back = m_UI.FindAction("Back", throwIfNotFound: true);
        m_UI_Chating = m_UI.FindAction("Chating", throwIfNotFound: true);
        // MapEditor
        m_MapEditor = asset.FindActionMap("MapEditor", throwIfNotFound: true);
        m_MapEditor_Paint = m_MapEditor.FindAction("Paint", throwIfNotFound: true);
        m_MapEditor_ClearSelection = m_MapEditor.FindAction("ClearSelection", throwIfNotFound: true);
        m_MapEditor_MousePosChange = m_MapEditor.FindAction("MousePosChange", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Interact;
    public struct GameplayActions
    {
        private @PlayerControl m_Wrapper;
        public GameplayActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Interact => m_Wrapper.m_Gameplay_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Back;
    private readonly InputAction m_UI_Chating;
    public struct UIActions
    {
        private @PlayerControl m_Wrapper;
        public UIActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Back => m_Wrapper.m_UI_Back;
        public InputAction @Chating => m_Wrapper.m_UI_Chating;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Back.started -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnBack;
                @Chating.started -= m_Wrapper.m_UIActionsCallbackInterface.OnChating;
                @Chating.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnChating;
                @Chating.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnChating;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Chating.started += instance.OnChating;
                @Chating.performed += instance.OnChating;
                @Chating.canceled += instance.OnChating;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // MapEditor
    private readonly InputActionMap m_MapEditor;
    private IMapEditorActions m_MapEditorActionsCallbackInterface;
    private readonly InputAction m_MapEditor_Paint;
    private readonly InputAction m_MapEditor_ClearSelection;
    private readonly InputAction m_MapEditor_MousePosChange;
    public struct MapEditorActions
    {
        private @PlayerControl m_Wrapper;
        public MapEditorActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Paint => m_Wrapper.m_MapEditor_Paint;
        public InputAction @ClearSelection => m_Wrapper.m_MapEditor_ClearSelection;
        public InputAction @MousePosChange => m_Wrapper.m_MapEditor_MousePosChange;
        public InputActionMap Get() { return m_Wrapper.m_MapEditor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapEditorActions set) { return set.Get(); }
        public void SetCallbacks(IMapEditorActions instance)
        {
            if (m_Wrapper.m_MapEditorActionsCallbackInterface != null)
            {
                @Paint.started -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnPaint;
                @Paint.performed -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnPaint;
                @Paint.canceled -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnPaint;
                @ClearSelection.started -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnClearSelection;
                @ClearSelection.performed -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnClearSelection;
                @ClearSelection.canceled -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnClearSelection;
                @MousePosChange.started -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnMousePosChange;
                @MousePosChange.performed -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnMousePosChange;
                @MousePosChange.canceled -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnMousePosChange;
            }
            m_Wrapper.m_MapEditorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Paint.started += instance.OnPaint;
                @Paint.performed += instance.OnPaint;
                @Paint.canceled += instance.OnPaint;
                @ClearSelection.started += instance.OnClearSelection;
                @ClearSelection.performed += instance.OnClearSelection;
                @ClearSelection.canceled += instance.OnClearSelection;
                @MousePosChange.started += instance.OnMousePosChange;
                @MousePosChange.performed += instance.OnMousePosChange;
                @MousePosChange.canceled += instance.OnMousePosChange;
            }
        }
    }
    public MapEditorActions @MapEditor => new MapEditorActions(this);
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get
        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnBack(InputAction.CallbackContext context);
        void OnChating(InputAction.CallbackContext context);
    }
    public interface IMapEditorActions
    {
        void OnPaint(InputAction.CallbackContext context);
        void OnClearSelection(InputAction.CallbackContext context);
        void OnMousePosChange(InputAction.CallbackContext context);
    }
}
