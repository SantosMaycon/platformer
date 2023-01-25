using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour {
  [SerializeField] private float health;
  [SerializeField] private float speed;
  [SerializeField] private float cooldownToAttack;
  [SerializeField] private float distanceToAttack;
  [SerializeField] private float sightDistance;
  [SerializeField] private float timeToTurn;
  [SerializeField] private LayerMask playerLayer;
  private Rigidbody2D rigidbody2d;
  private BoxCollider2D boxCollider2d;
  private Animator animator;
  private float _timeToTurn;
  private bool isAttack = false;
  private bool isFollow;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
    boxCollider2d = GetComponent<BoxCollider2D>();
    animator = transform.GetChild(0).GetComponent<Animator>();
    _timeToTurn = timeToTurn;
  }

  // Update is called once per frame
  void Update() {
    onTurn();
  }
  void FixedUpdate() {
    onPatrol();
    animator.SetBool("isRun", rigidbody2d.velocity.x != 0);
  }

  void onTurn() {
    transform.localScale = new Vector3(Mathf.Sign(speed), 1f, 1f);

    if(rigidbody2d.velocity.x == 0f && _timeToTurn >= 0 && !isAttack) {
      _timeToTurn -= Time.deltaTime;
    }

    if (_timeToTurn <= 0) {
      _timeToTurn = timeToTurn;
      transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
      speed = -speed;
    }
  }

  void onPatrol() {
    var hit = Physics2D.Raycast(boxCollider2d.bounds.center, new Vector2(Mathf.Sign(speed), 0f), sightDistance, playerLayer);
    // Debug.DrawRay(boxCollider2d.bounds.center, new Vector2(Mathf.Sign(speed), 0f) * sightDistance, hit ? Color.yellow : Color.white);
    
    if (hit) {
      if (!isFollow) {
        StartCoroutine(startToFollow());
      }
      
      if (Vector2.Distance(transform.position, hit.transform.position) <= distanceToAttack) {
        rigidbody2d.velocity = Vector2.zero; 
        if (!isAttack ) {
          isAttack = true;
          StartCoroutine(timeToAttack(hit.collider));
        }
      } else if (isAttack){
        stopAttack();
      }
    } else {
      rigidbody2d.velocity = Vector2.zero;
      if (isFollow) {
        stopAttack();
      }
    }
  }

  void stopAttack() {
    isFollow = false;
    isAttack = false;
  }

  void attack(Collider2D other) {
    animator.SetTrigger("attacking");
    other.gameObject.GetComponent<Player>().onHit();
  }
  public void onHit() {
    animator.SetTrigger("hit");

    if (--health <= 0) {
      animator.SetTrigger("death");
      rigidbody2d.velocity = Vector2.zero;
      Destroy(gameObject, 1f);
    }
  }

  IEnumerator timeToAttack(Collider2D other) {
    yield return new WaitForSeconds(0.5f);
    if (isAttack) attack(other);

    yield return new WaitForSeconds(cooldownToAttack);
    if (isAttack) {
      StartCoroutine(timeToAttack(other));
    }
  }

  IEnumerator startToFollow() {
    isFollow = true;
    yield return new WaitForSeconds(0.5f);
    rigidbody2d.velocity = new Vector2(speed, 0f);
  }
}
