using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawn : MonoBehaviour {
  private GameObject spawn;
  // Start is called before the first frame update
  void Start() {
    spawn = GameObject.FindGameObjectWithTag("Spawn");
  }

  // Update is called once per frame
  void Update() {}

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player") && spawn) {
      other.GetComponent<Player>().onHit();
      other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
      other.transform.position = spawn.transform.position;
    }
  }
}
