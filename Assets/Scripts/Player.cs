using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField] private float speed;
  [SerializeField] private float jumpForce;
  [SerializeField] private int amountOfJump;
  private Rigidbody2D rigidbody2d;
  private Animator animator;
  private int _amountOfJump;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    animator = transform.GetChild(0).GetComponent<Animator>();
    _amountOfJump = amountOfJump;
  }

  // Update is called once per frame
  void Update() {
    jump();
  }
 
  void FixedUpdate() {
    move();
  }

  void move() {
    float horizontal = Input.GetAxis("Horizontal");
    
    if (horizontal != 0) {
      rigidbody2d.velocity = new Vector2(horizontal * speed, rigidbody2d.velocity.y);
      transform.localScale = new Vector2(Mathf.Sign(horizontal), transform.localScale.y);
    }

    animator.SetBool("isRun", horizontal != 0);
  }

  void jump() {
    if (Input.GetButtonDown("Jump") && amountOfJump > 0) {
      rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
      animator.SetBool("isJump", true);
      amountOfJump--;
    }
  }


  private void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.layer == 8) {
      animator.SetBool("isJump", false);
      amountOfJump = _amountOfJump;
    }
  }
}
