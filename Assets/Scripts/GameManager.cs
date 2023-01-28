using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  [SerializeField] private int score;
  [SerializeField] Text scoreText;
  [SerializeField] Image[] hearts;
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

    if (PlayerPrefs.GetInt("health") > 0) {
      SetHeartsOnHub(PlayerPrefs.GetInt("health"));
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

  public void SetHeartsOnHub(int health) {
    int index = 0;
    foreach (var heart in hearts) {
      heart.enabled = index < health ? true : false;
      index++;
    }
    PlayerPrefs.SetInt("health", health);
  }
}
