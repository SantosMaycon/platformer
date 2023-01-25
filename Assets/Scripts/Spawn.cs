using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
  void Awake() {
    GameObject player = GameObject.FindGameObjectWithTag("Player"); 

    if (player) {
      player.transform.position = transform.position;
    }
  }
}
