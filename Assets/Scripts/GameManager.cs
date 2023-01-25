using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
  [SerializeField] private int score;
  [SerializeField] Text scoreText;
  public static GameManager instance;

  private void Awake() {
    instance = this;
  }

  // Start is called before the first frame update
  void Start(){}

  // Update is called once per frame
  void Update() {}

  public void GetCoin() {
    scoreText.text = (++score).ToString();
  }
}
