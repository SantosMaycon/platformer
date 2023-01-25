using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
  [SerializeField] private string sceneName;

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player") && sceneName != "") {
      GameManager.instance.ChangeScene(sceneName);
    }
  }
}
