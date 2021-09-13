using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject playerSprite;
    [SerializeField] GameObject playerSpriteParent;
	[SerializeField] InGameNormalViewEvents sceneEvents;
    [SerializeField] int chairTriggerSceneListener;
    [SerializeField] int chairTriggerScenePresenter;
	[SerializeField] InGameNormalViewUIManager uiManager;
    Animator animator;
    public bool IsInChairPresenter, IsInChairListener;
	public Vector2 MovementThisFrame;
	
	// Start is called before the first frame update
	void Start()
    {
        animator = playerSprite.GetComponent<Animator>();
        gameObject.transform.position = DataPersistentSystem.Instance.PlayerLastPos;
    }

	private void FixedUpdate()
	{
		/* Movement */
		gameObject.transform.Translate(MovementThisFrame * Time.deltaTime * speed);
	}

	// FixedUpdate is called once per frame, before the Update function
	void Update()
	{
		/* Change facing */
		if (MovementThisFrame.magnitude > 0)
		{
			playerSpriteParent.transform.rotation = Quaternion.LookRotation(Vector3.forward, MovementThisFrame);
		}

		/* Animation */
		animator.SetFloat("speed", MovementThisFrame.magnitude);
	}


	public void OnInteract()
	{
		if (IsInChairListener)
		{
			sceneEvents.TransitToSceneRecordPosition(chairTriggerSceneListener);
		}
		else if (IsInChairPresenter)
		{
			sceneEvents.TransitToSceneRecordPosition(chairTriggerScenePresenter);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Listener Chair"))
		{
			IsInChairListener = true;
		}
		else if (collision.gameObject.CompareTag("Presenter Chair"))
		{
			IsInChairPresenter = true;
		}
		else if (collision.gameObject.CompareTag("Room"))
		{
			uiManager.ChangeRoomText(int.Parse(collision.gameObject.name));
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Listener Chair"))
		{
			IsInChairListener = false;
		}
		else if (collision.gameObject.CompareTag("Presenter Chair"))
		{
			IsInChairPresenter = false;
		}
		else if (collision.gameObject.CompareTag("Room"))
		{
			uiManager.ChangeRoomText(-1);
		}
	}

}
