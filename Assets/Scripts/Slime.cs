using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {
  [SerializeField] private float speed;
  [SerializeField] private LayerMask levelLayer;
  private Rigidbody2D _rigidbody2D;
  private BoxCollider2D _boxCollider2D;
  // Start is called before the first frame update
  void Start() {
    _boxCollider2D = GetComponent<BoxCollider2D>();
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _rigidbody2D.velocity = new Vector2(speed, _rigidbody2D.velocity.y);
  }

  // Update is called once per frame
  private void FixedUpdate() {
    if (hitTheWall()) {
      _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x * -1f, _rigidbody2D.velocity.y);
      transform.localScale = new Vector3(Mathf.Sign(_rigidbody2D.velocity.x), 1f, 1f);
    }
  }

  private bool hitTheWall() {
    return Physics2D.Raycast(_boxCollider2D.bounds.center, new Vector2(Mathf.Sign(_rigidbody2D.velocity.x), 0f), 0.5f, levelLayer);
  }
}
