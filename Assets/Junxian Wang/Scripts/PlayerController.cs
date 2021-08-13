using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject playerSprite;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
	{
		/* Movement control */
		float horiInput = Input.GetAxis("Horizontal");
		float vertInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horiInput * Time.fixedDeltaTime * speed, vertInput * Time.fixedDeltaTime * speed, 0);

        /* Change facing */
        playerSprite.transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);
        gameObject.transform.Translate(movement);

        /* Animation */
        animator.SetFloat("speed", movement.magnitude);
    }
}
