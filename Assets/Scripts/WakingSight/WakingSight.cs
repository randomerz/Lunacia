﻿using System.Collections;
using System.Collections.Generic;
using Pubsub;
using UnityEngine;

public class WakingSight : MonoBehaviour
{
    public int activeMode = 0;
    public float maxScale = 10f;
    private bool changingMode = false;
	private Animator animator;
    public Rigidbody2D nullzone;
    private bool inNZ = false;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        nullzone = GameObject.FindGameObjectWithTag("NullZone").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire2") && !changingMode && inNZ == false) {
            // Toggle through modes
            if (activeMode == 0)
            {
                StartCoroutine(changeMode(1));
            }
            else if (activeMode == 1)
            {
                StartCoroutine(changeMode(0));
            }
        }
        else if (activeMode == 1 && inNZ == true && !changingMode) {
            print(":)");
            StartCoroutine(changeMode(0));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "NullZone")
        {
            inNZ = true;
            if (activeMode == 1)
            {
                StartCoroutine(changeMode(1));
            }
            print("hi");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "NullZone")
        {
            inNZ = false;
            print("bye");
        }
    }

    IEnumerator changeMode(int mode) {
        if (mode != activeMode && !changingMode) {
            // Flag mode changing so coroutine can't be called twice simultaneously
            changingMode = true;
			MessageBroker.Instance.Raise(new WakingSightModeEventArgs(mode));
            activeMode = mode;
            float scalarStep = maxScale/30;
            if (mode == 0) {
				
				animator.SetBool("WakingSightOn", false);

                // Set to max scale and step down
                // Vector3 scale = new Vector3(maxScale, maxScale, 0);
                // Vector3 step = new Vector3(-maxScale/30, -maxScale/30, 0);
                // while (transform.localScale.x > 0) {
                //     scale += step;
                //     transform.localScale = scale;
                //     yield return new WaitForSeconds(0.1f);
                // }
                // transform.localScale = Vector3.zero;
            } else if (mode == 1) {

				animator.SetBool("WakingSightOn", true);

                // Set scale to zero and step up
                // Vector3 scale = Vector3.zero;
                // Vector3 step = new Vector3(maxScale/30, maxScale/30, 0);
                // while (transform.localScale.x < maxScale) {
                //     scale += step;
                //     transform.localScale = scale;
                //     yield return new WaitForSeconds(0.1f);
                // }
                // transform.localScale = new Vector3(maxScale, maxScale, 0);
            }
			yield return null;
            changingMode = false;
        }
    }

//    void OnTriggerEnter2D(Collider2D other) {
//       
 //   }
}
