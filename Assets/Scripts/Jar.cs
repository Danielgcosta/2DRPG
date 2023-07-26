using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jar : MonoBehaviour
{
	public GameObject jar;
	public bool playerInRange;	
	private Animator animator;
	private Rigidbody2D myRigidbody;
	
    void Start(){
		animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update(){
		if(Input.GetKeyDown(KeyCode.Space) && playerInRange){
			StartCoroutine(breakJar());
		}      
    }
	
	private IEnumerator breakJar(){
		animator.SetBool("attacked",true);
		yield return new WaitForSeconds(.3f);
		jar.SetActive(false);
	}
		
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			playerInRange=true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("Player")){
			playerInRange=false;
		}
	}
}
