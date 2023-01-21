using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField] private float speed;
  private Rigidbody2D rigidbody2d;
  // Start is called before the first frame update
  void Start() {
    rigidbody2d = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update() {}
 
  void FixedUpdate() {
    move();
  }

  void move() {
    float horizontal = Input.GetAxis("Horizontal");
    
    if (horizontal != 0) {
      rigidbody2d.velocity = new Vector2(horizontal * speed, rigidbody2d.velocity.y);
      transform.localScale = new Vector2(Mathf.Sign(horizontal), transform.localScale.y);
    }
  }
}
