using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
	public GameObject slime;
	private Rigidbody2D myRigidbody;
	public Transform slimeTransform;
    public float speed;
	private Vector3 change;
	
	public Animator animator;
	public bool playerInRange;
	
	public int health;	
	
    void Start(){
		animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
		slimeTransform = GetComponent<Transform>();
		health = 2;
    }

    void FixedUpdate(){
		if(Input.GetKeyDown(KeyCode.Space) && playerInRange){
			if(slime.activeInHierarchy){
				animator.SetBool("isIdle",false);
				health = health - 1;
				if(health<=0){
					StartCoroutine(deathAnimation());
				}
				else{
					StartCoroutine(hurtAnimation());
				}					
			}
		}
		else{			
			UpdateAnimationAndMove();		
			StartCoroutine(idleAnimation());
		}
    }

	void UpdateAnimationAndMove(){
		change = new Vector3(Random.Range(-1,1),Random.Range(-1,1),0);
		MoveCharacter();
		StartCoroutine(moveAnimation());
	}
	
	void MoveCharacter(){
		myRigidbody.MovePosition(transform.position+change*speed*Time.deltaTime);
	}
	
	private IEnumerator moveAnimation(){
		animator.SetBool("isMoving",true);
		yield return new WaitForSeconds(1f);
		animator.SetBool("isMoving",false);
	}
	
	private IEnumerator deathAnimation(){
		animator.SetBool("isDead",true);
		yield return new WaitForSeconds(.7f);
		slime.SetActive(false);
	}
	
	private IEnumerator hurtAnimation(){
		animator.SetBool("isHurt",true);
		yield return new WaitForSeconds(.4f);
		animator.SetBool("isHurt",false);		
	}
	
	private IEnumerator idleAnimation(){
		animator.SetBool("isIdle",true);
		yield return new WaitForSeconds(.3f);
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			playerInRange=true;
			//Debug.Log("Player in Range");
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("Player")){
			playerInRange=false;
			//Debug.Log("Player not in Range");
		}
	}
}
