using System.Collections;
using System.Collections.Generic;
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
	public int a;

    // Start is called before the first frame update
    void Start()
    {
        animator = playerSprite.GetComponent<Animator>();
        gameObject.transform.position = DataPersistentSystem.SharedInstance.PlayerLastPos;
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		/* Movement control */
		float horiInput = Input.GetAxis("Horizontal");
		float vertInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horiInput * Time.fixedDeltaTime * speed, vertInput * Time.fixedDeltaTime * speed, 0);
        gameObject.transform.Translate(movement);

		/* Change facing */
		if (movement.magnitude > 0)
		{
            playerSpriteParent.transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);
		}

		/* Animation */
		animator.SetFloat("speed", movement.magnitude);
    }

	private void Update()
	{
		/* Sit down action */
		if (Input.GetKeyDown(KeyCode.E))
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
}
