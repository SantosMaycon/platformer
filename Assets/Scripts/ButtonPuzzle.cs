using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour {
  private Animator animator;
  private Transform pointOfCollision;
  [SerializeField] private GameObject wall;
  [SerializeField] private float radiusSize;
  [SerializeField] LayerMask collsionLayer;
  // Start is called before the first frame update
  void Start() {
    animator = GetComponent<Animator>();
    pointOfCollision = transform.GetChild(0).transform;
  }

  // Update is called once per frame
  void Update() {}

  void FixedUpdate() {
    onPressed();  
  }

  void onPressed() {
    Collider2D hit = Physics2D.OverlapCircle(pointOfCollision.position, radiusSize, collsionLayer);

    animator.SetBool("Pressed", hit);
    
    if (wall) {
      wall.GetComponent<Animator>().SetBool("Pressed", hit);
    }
  }

  private void OnDrawGizmos() {
    if (pointOfCollision) {
      Gizmos.DrawWireSphere(pointOfCollision.position, radiusSize);
    }
  }
}
