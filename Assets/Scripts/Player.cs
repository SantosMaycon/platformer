using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField] private int health;
  [SerializeField] private float speed;
  [SerializeField] private float jumpForce;
  [SerializeField] private int amountOfJump;
  [SerializeField] private float attackArea;
  [SerializeField] private LayerMask enemyLayer;
  [SerializeField] private LayerMask groundLayer;
  private Rigidbody2D rigidbody2d;
  private CapsuleCollider2D capsuleCollider2d;
  private Animator animator;
  private Transform _pointOfAttack;
  private int _amountOfJump;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    capsuleCollider2d = GetComponent<CapsuleCollider2D>();
    animator = transform.GetChild(0).GetComponent<Animator>();
    _pointOfAttack = transform.GetChild(1).GetComponent<Transform>();
    _amountOfJump = amountOfJump;
  }

  // Update is called once per frame
  void Update() {
    jump();
    attack();
  }
 
  void FixedUpdate() {
    move();
    if (isGrounded() && rigidbody2d.velocity.y < 0.1f) {
      animator.SetBool("isJump", false);
      amountOfJump = _amountOfJump;
    }
  }

  void move() {
    float horizontal = Input.GetAxis("Horizontal");
    
    if (horizontal != 0) {
      rigidbody2d.velocity = new Vector2(horizontal * speed, rigidbody2d.velocity.y);
      transform.localScale = new Vector2(Mathf.Sign(horizontal), transform.localScale.y);
    } else {
      rigidbody2d.velocity = new Vector2(0f, rigidbody2d.velocity.y);
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

  private bool isGrounded() {
    return Physics2D.Raycast(capsuleCollider2d.bounds.center, Vector2.down, 0.4f, groundLayer);
  }

  void attack() {
    if(Input.GetButtonDown("Fire1") && amountOfJump == _amountOfJump && _pointOfAttack) {
      animator.SetTrigger("attacking");
      Collider2D hit = Physics2D.OverlapCircle(_pointOfAttack.position, attackArea, enemyLayer);

      if (hit) {
        if (hit.CompareTag("Slime")) {
          hit.GetComponent<Slime>().onHit();
        } 

        if (hit.CompareTag("Goblin")) {
          hit?.GetComponent<Goblin>().onHit();
        }
      }
    }
  }

  public void onHit() {
    animator.SetTrigger("hit");

    if (--health <= 0) {
      animator.SetTrigger("death");
      Destroy(gameObject, 0.5f);
    }
  }

  private void OnDrawGizmos() {
    if (_pointOfAttack) {
      Gizmos.DrawWireSphere(_pointOfAttack.position, attackArea);
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.layer == 9) {
      onHit();
    }

    if (other.CompareTag("Coin")) {
      GameManager.instance.GetCoin();
      other.GetComponent<Animator>().SetTrigger("pick");
      Destroy(other.gameObject, 1f);
    }
  }
}
