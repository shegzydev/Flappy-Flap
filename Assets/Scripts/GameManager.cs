using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    bool began;
    int level;
    int score;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] Button Next, Prev, StartButton;

    [SerializeField] GameObject MenuScreen;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        scoreText.text = "0";
        level = PlayerPrefs.GetInt("level");

        Next.onClick.AddListener(() =>
        {
            level += 1;
            level %= 7;
            PlayerPrefs.SetInt("level", level);
            save();
        });
        Prev.onClick.AddListener(() =>
        {
            level -= 1;
            if (level < 0) level = 6;
            PlayerPrefs.SetInt("level", level);
            save();
        });
        StartButton.onClick.AddListener(() =>
        {
            if (began) return;
            MenuScreen.SetActive(false);
            began = true;
        });
    }

    void Update()
    {
        
    }

    public bool Began
    {
        get
        {
            return began;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
    }

    public void save()
    {
        PlayerPrefs.Save();
    }

    public int AddScore(int s)
    {
        score += s;
        scoreText.text = score.ToString();
        return score;
    }
}
