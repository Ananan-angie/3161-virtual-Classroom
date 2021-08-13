using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject playerSprite;
    [SerializeField] GameObject playerAnimatorParent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = playerAnimatorParent.GetComponent<Animator>();
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
			playerSprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);

		}


		/* Animation */
		animator.SetFloat("speed", movement.magnitude);
    }
}
