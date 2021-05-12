// GENERATED AUTOMATICALLY FROM 'Assets/Misc/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player3D"",
            ""id"": ""eaa3574a-9512-4cf5-a3a2-378c117d6816"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""1a471bde-d9c7-4000-a3f1-a887119b0ffd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""c4cab8d7-b457-42fb-b66e-3a68b6f5ad7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""687abf6e-0c18-4ed8-8446-79ba44106a0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""0dd146c3-a4f7-40a8-98b9-a9f56e619580"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""af14340b-3723-4a9e-948c-18d28af1ad2f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""330cb91c-9af6-4f6d-816e-2bfd77976bcb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f941fc0d-a7b9-415e-8281-025d55ca9492"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(pressPoint=0.2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98dd15aa-a1a9-41b0-bd29-fa9b1fd6631e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97632f91-3f96-4412-b2ac-43625a156e74"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e35a2074-78e3-4c49-96ac-275b64a2732b"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Move"",
                    ""id"": ""ee891ca9-39f4-4dee-8133-6bed967865e9"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold"",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9ed8202a-7641-489d-884c-4a3481bdb062"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""451fc6ed-b492-4673-8cfa-02c46ebb2e3f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""571557e7-bb45-429c-9e7f-88c2bfa4453e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a43e6143-da1e-4933-b9dc-a1e40cf5564d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ad599d63-5f86-4699-b398-adc9921b0681"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player2D"",
            ""id"": ""021d4ec3-5d24-4fbc-afac-fc81621f88b6"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""de54283c-085d-4df2-8159-39dd7b61d5fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d4985ece-3720-42ce-8eb0-954676fbfcfd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""60685d19-f5f3-48f2-9615-9e4db3091800"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""9499b114-1968-4bd3-baf6-ef77a8802e95"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""0bd6d6b2-941f-4287-b501-2f8fcb8c59c2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b782e3e1-f4ef-4460-a5db-499372b6d9af"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0220a6f4-86f9-4a72-8ed5-9a2da5e6144f"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bfb404b-1147-428b-86f1-7d5c0b9df767"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3accc7a-54f9-485f-8c08-77259688148a"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Move"",
                    ""id"": ""1b0ec336-bd27-455c-93c5-c26377c69afe"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold"",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f4f0edb8-6fde-425a-b1f2-d5855e4edb93"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6733461f-0d96-48d4-acf5-11b8d31f14ce"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3237e56e-f79b-4587-acf4-2d1d35e8971a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4e736613-8199-480c-a375-76bb81f4d2aa"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Developer"",
            ""id"": ""f58aab1f-6824-47f3-b507-5d2b76eba604"",
            ""actions"": [
                {
                    ""name"": ""Toggle Console"",
                    ""type"": ""Button"",
                    ""id"": ""54401d6c-f188-4049-bbe2-fe564e5a5026"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""87f866ff-2174-4661-853a-4bd65b83c1c5"",
                    ""path"": ""<Keyboard>/slash"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Toggle Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player3D
        m_Player3D = asset.FindActionMap("Player3D", throwIfNotFound: true);
        m_Player3D_Move = m_Player3D.FindAction("Move", throwIfNotFound: true);
        m_Player3D_Jump = m_Player3D.FindAction("Jump", throwIfNotFound: true);
        m_Player3D_Shoot = m_Player3D.FindAction("Shoot", throwIfNotFound: true);
        m_Player3D_Interact = m_Player3D.FindAction("Interact", throwIfNotFound: true);
        m_Player3D_Dash = m_Player3D.FindAction("Dash", throwIfNotFound: true);
        m_Player3D_MouseLook = m_Player3D.FindAction("MouseLook", throwIfNotFound: true);
        // Player2D
        m_Player2D = asset.FindActionMap("Player2D", throwIfNotFound: true);
        m_Player2D_Move = m_Player2D.FindAction("Move", throwIfNotFound: true);
        m_Player2D_Jump = m_Player2D.FindAction("Jump", throwIfNotFound: true);
        m_Player2D_Shoot = m_Player2D.FindAction("Shoot", throwIfNotFound: true);
        m_Player2D_Interact = m_Player2D.FindAction("Interact", throwIfNotFound: true);
        m_Player2D_Dash = m_Player2D.FindAction("Dash", throwIfNotFound: true);
        // Developer
        m_Developer = asset.FindActionMap("Developer", throwIfNotFound: true);
        m_Developer_ToggleConsole = m_Developer.FindAction("Toggle Console", throwIfNotFound: true);
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

    // Player3D
    private readonly InputActionMap m_Player3D;
    private IPlayer3DActions m_Player3DActionsCallbackInterface;
    private readonly InputAction m_Player3D_Move;
    private readonly InputAction m_Player3D_Jump;
    private readonly InputAction m_Player3D_Shoot;
    private readonly InputAction m_Player3D_Interact;
    private readonly InputAction m_Player3D_Dash;
    private readonly InputAction m_Player3D_MouseLook;
    public struct Player3DActions
    {
        private @InputMaster m_Wrapper;
        public Player3DActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player3D_Move;
        public InputAction @Jump => m_Wrapper.m_Player3D_Jump;
        public InputAction @Shoot => m_Wrapper.m_Player3D_Shoot;
        public InputAction @Interact => m_Wrapper.m_Player3D_Interact;
        public InputAction @Dash => m_Wrapper.m_Player3D_Dash;
        public InputAction @MouseLook => m_Wrapper.m_Player3D_MouseLook;
        public InputActionMap Get() { return m_Wrapper.m_Player3D; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player3DActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer3DActions instance)
        {
            if (m_Wrapper.m_Player3DActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Player3DActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Player3DActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Player3DActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_Player3DActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Player3DActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Player3DActionsCallbackInterface.OnJump;
                @Shoot.started -= m_Wrapper.m_Player3DActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_Player3DActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_Player3DActionsCallbackInterface.OnShoot;
                @Interact.started -= m_Wrapper.m_Player3DActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_Player3DActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_Player3DActionsCallbackInterface.OnInteract;
                @Dash.started -= m_Wrapper.m_Player3DActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_Player3DActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_Player3DActionsCallbackInterface.OnDash;
                @MouseLook.started -= m_Wrapper.m_Player3DActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_Player3DActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_Player3DActionsCallbackInterface.OnMouseLook;
            }
            m_Wrapper.m_Player3DActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
            }
        }
    }
    public Player3DActions @Player3D => new Player3DActions(this);

    // Player2D
    private readonly InputActionMap m_Player2D;
    private IPlayer2DActions m_Player2DActionsCallbackInterface;
    private readonly InputAction m_Player2D_Move;
    private readonly InputAction m_Player2D_Jump;
    private readonly InputAction m_Player2D_Shoot;
    private readonly InputAction m_Player2D_Interact;
    private readonly InputAction m_Player2D_Dash;
    public struct Player2DActions
    {
        private @InputMaster m_Wrapper;
        public Player2DActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player2D_Move;
        public InputAction @Jump => m_Wrapper.m_Player2D_Jump;
        public InputAction @Shoot => m_Wrapper.m_Player2D_Shoot;
        public InputAction @Interact => m_Wrapper.m_Player2D_Interact;
        public InputAction @Dash => m_Wrapper.m_Player2D_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Player2D; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player2DActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer2DActions instance)
        {
            if (m_Wrapper.m_Player2DActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Player2DActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Player2DActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Player2DActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_Player2DActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Player2DActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Player2DActionsCallbackInterface.OnJump;
                @Shoot.started -= m_Wrapper.m_Player2DActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_Player2DActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_Player2DActionsCallbackInterface.OnShoot;
                @Interact.started -= m_Wrapper.m_Player2DActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_Player2DActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_Player2DActionsCallbackInterface.OnInteract;
                @Dash.started -= m_Wrapper.m_Player2DActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_Player2DActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_Player2DActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_Player2DActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public Player2DActions @Player2D => new Player2DActions(this);

    // Developer
    private readonly InputActionMap m_Developer;
    private IDeveloperActions m_DeveloperActionsCallbackInterface;
    private readonly InputAction m_Developer_ToggleConsole;
    public struct DeveloperActions
    {
        private @InputMaster m_Wrapper;
        public DeveloperActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToggleConsole => m_Wrapper.m_Developer_ToggleConsole;
        public InputActionMap Get() { return m_Wrapper.m_Developer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DeveloperActions set) { return set.Get(); }
        public void SetCallbacks(IDeveloperActions instance)
        {
            if (m_Wrapper.m_DeveloperActionsCallbackInterface != null)
            {
                @ToggleConsole.started -= m_Wrapper.m_DeveloperActionsCallbackInterface.OnToggleConsole;
                @ToggleConsole.performed -= m_Wrapper.m_DeveloperActionsCallbackInterface.OnToggleConsole;
                @ToggleConsole.canceled -= m_Wrapper.m_DeveloperActionsCallbackInterface.OnToggleConsole;
            }
            m_Wrapper.m_DeveloperActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ToggleConsole.started += instance.OnToggleConsole;
                @ToggleConsole.performed += instance.OnToggleConsole;
                @ToggleConsole.canceled += instance.OnToggleConsole;
            }
        }
    }
    public DeveloperActions @Developer => new DeveloperActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IPlayer3DActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnMouseLook(InputAction.CallbackContext context);
    }
    public interface IPlayer2DActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
    public interface IDeveloperActions
    {
        void OnToggleConsole(InputAction.CallbackContext context);
    }
}
