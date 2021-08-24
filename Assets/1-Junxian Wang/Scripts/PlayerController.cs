using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject playerSprite;
    [SerializeField] GameObject playerSpriteParent;
    [SerializeField] int chairTriggerSceneListener;
    [SerializeField] int chairTriggerScenePresenter;
    Animator animator;
    public bool isInChairPresenter, isInChairListener;

	PlayerControl controls;
	Vector2 movement;
	

	private void Awake()
	{
		// Setup player control
		controls = new PlayerControl();
		controls.Gameplay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
		controls.Gameplay.Move.canceled += ctx => movement = Vector2.zero;
		controls.Gameplay.Interact.performed += ctx => interactHandler();
		
	}

	// Start is called before the first frame update
	void Start()
    {
        animator = playerSprite.GetComponent<Animator>();
        gameObject.transform.position = DataPersistentSystem.SharedInstance.PlayerLastPos;
    }

    // FixedUpdate is called once per frame, before the Update function
    void Update()
	{
		/* Movement */	
        gameObject.transform.Translate(movement * Time.deltaTime * speed);

		/* Change facing */
		if (movement.magnitude > 0)
		{
            playerSpriteParent.transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);
		}

		/* Animation */
		animator.SetFloat("speed", movement.magnitude);
    }

	void interactHandler()
	{
		if (isInChairListener)
		{
			DataPersistentSystem.SharedInstance.LastScene = SceneManager.GetActiveScene().buildIndex;
			DataPersistentSystem.SharedInstance.PlayerLastPos = gameObject.transform.position;
			SceneManager.LoadScene(chairTriggerSceneListener);
		}
		else if (isInChairPresenter)
		{
			DataPersistentSystem.SharedInstance.LastScene = SceneManager.GetActiveScene().buildIndex;
			DataPersistentSystem.SharedInstance.PlayerLastPos = gameObject.transform.position;
			SceneManager.LoadScene(chairTriggerScenePresenter);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Listener Chair"))
		{
			isInChairListener = true;
		}
		else if (collision.gameObject.CompareTag("Presenter Chair"))
		{
			isInChairPresenter = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Listener Chair"))
		{
			isInChairListener = false;
		}
		else if (collision.gameObject.CompareTag("Presenter Chair"))
		{
			isInChairPresenter = false;
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
