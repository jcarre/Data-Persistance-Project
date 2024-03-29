using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string playerName;
    public string highScoreName;
    public int highScore;
    public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        // Make the scene persistant and avoids creating new if already exists = SINGLETON
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
        if (MenuManager.Instance.highScore > 0)
        {
            highScoreText.text = "Current high score : " + MenuManager.Instance.highScoreName + " : " + MenuManager.Instance.highScore;
            highScoreText.gameObject.SetActive(true);
        }
            
        Debug.Log("Awake - " + System.Environment.NewLine
        + "playerName : " + MenuManager.Instance.playerName + System.Environment.NewLine
        + "highScoreName : " + MenuManager.Instance.highScoreName + System.Environment.NewLine
        + "highScore : " + MenuManager.Instance.highScore);
    }

    /** Saving & Loading Data **/

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public string highScoreName;
        public int highScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.highScore = highScore;
        data.highScoreName = playerName;

        string json = JsonUtility.ToJson(data);
    
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            highScore = data.highScore;
            highScoreName = data.highScoreName;
        }
    }
}
