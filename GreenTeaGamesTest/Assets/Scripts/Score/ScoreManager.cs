using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Xml.Linq;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Header("End screen")]
    [SerializeField] private TextMeshProUGUI _scoreTextEndScreen;   // Would probably be more efficient to reuse the in game text than doing this
    [SerializeField] private TextMeshProUGUI _highscoreText;

    private static ScoreManager _instance;
    public static ScoreManager Instance { get => _instance; }

    private int _score;
    private int _highScore;

    // XML file location
    private string _fileLocation;
    private string _fileName;
    private string _path;

    private XDocument _xmlDoc;

    private bool _newHighScore;

    // Start is called before the first frame update
    void Start()
    {
        _fileLocation = Application.dataPath + "/";
        _fileName = "SaveData.xml";
        _path = _fileLocation + _fileName;

        _newHighScore = false;

        _instance = this;
        GetHighScore();
    }

    /// <summary>
    /// Updates the game score
    /// </summary>
    public void UpdateScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
        _scoreTextEndScreen.text = "Score: " + _score.ToString();

        if (_score > _highScore)
        {
            _highScore = _score;
            UpdateHighScore();
            _newHighScore = true;
        }
    }

    /// <summary>
    /// Returns the highscore or creates a new file to hold it if it doesnt exist
    /// </summary>
    private void GetHighScore()
    {
        // Looking to catch a file not found exeption
        try
        {
            _xmlDoc = XDocument.Load(_path);
        }
        catch (System.Exception)
        {
            // In which case we make a new xml file
            CreateXMLFile();
        }

        _highScore = int.Parse(_xmlDoc.Element("HighScore").Element("Value").Value);
        _highscoreText.text = "Highscore: " + _highScore.ToString();
    }

    /// <summary>
    /// Creates a new XML file
    /// </summary>
    private void CreateXMLFile()
    {
        _xmlDoc = new XDocument();

        XElement highscore = new XElement("HighScore", new XElement("Value", 0));

        _xmlDoc.Add(highscore);

        _xmlDoc.Save(_path);

        GetHighScore();
    }

    /// <summary>
    /// Updates the XML
    /// </summary>
    private void UpdateHighScore()
    {
        _highscoreText.text = "Highscore: " + _highScore.ToString();

        _xmlDoc.Element("HighScore").Element("Value").Value = _highScore.ToString();
        _xmlDoc.Save(_path);
    }

    public bool IsNewHighscore { get => _newHighScore; }
    public GameObject GetScoreText { get => _scoreText.gameObject; }
}
