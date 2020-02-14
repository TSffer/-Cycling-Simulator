using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// © Glitch Squirrel Design 2017
// All right reserved.

public class ScreenBehaviour : MonoBehaviour {

    // Array of your screens;
    private GameObject[] screens;

    // Start screenl
	public string startScreen = null;

	private void Start()
    {
        // Find all of your screens. Don't forget add tag "Screen" to your screens;
        screens = GameObject.FindGameObjectsWithTag("Screen");

        // Set start screen;
        SetScreen(startScreen);
    }

    // Public interface for buttons or access outside;
	public void SetScreen(string screen)
	{
		StartCoroutine(ChangeScreen(screen));
	}

    // Screen chsnge proccess;
	public IEnumerator ChangeScreen(string newScreen)
	{
        // Block input;
		gameObject.GetComponent<CanvasGroup>().interactable = false;

        // Hide the canvas;
        Animator animator = gameObject.GetComponent<Animator>();
        animator.SetBool("Hide", true);

        // Wait until the animation ends;
        yield return new WaitForSeconds(1);

        // Hide all screens;
		foreach (GameObject screen in screens)
		{
			screen.GetComponent<CanvasGroup>().alpha = 0;
			screen.GetComponent<CanvasGroup>().interactable = false;
			screen.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}

        // Make visible active screen;
		GameObject activeScreen = GameObject.Find(newScreen);
		activeScreen.GetComponent<CanvasGroup>().alpha = 1;
		activeScreen.GetComponent<CanvasGroup>().interactable = true;
		activeScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Show the canvas;
        animator.SetBool("Hide", false);

        // Make input available;
		gameObject.GetComponent<CanvasGroup>().interactable = true;
	}
}

// © Glitch Squirrel Design 2017
// All right reserved.