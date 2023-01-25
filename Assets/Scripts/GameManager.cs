using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  [SerializeField] private int score;
  [SerializeField] Text scoreText;
  public static GameManager instance;

  private void Awake() {
    instance = this;

    DontDestroyOnLoad(this);

    if (instance) {
      Destroy(gameObject);
    } else {
      instance = this;
    }

    if (PlayerPrefs.GetInt("score") > 0) {
      score = PlayerPrefs.GetInt("score");
      scoreText.text = score.ToString();  
    }
  }

  // Start is called before the first frame update
  void Start(){}

  // Update is called once per frame
  void Update() {}

  public void GetCoin() {
    scoreText.text = (++score).ToString();
    PlayerPrefs.SetInt("score", score);
  }

  public void ChangeScene(string scene) {
    SceneManager.LoadScene(scene);
  }
}
