//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.UI;
//using static UnityEditor.PlayerSettings;
//using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int ScoreValue = 0;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _speed= 100f;
    [SerializeField] private GameObject _arbre, _player, _proie;
    [SerializeField] private ScriptableObjectTest _scenario;
    [SerializeField] private GameObject _slider;

    public PlayerPrefs _score;


    //private int indice = 0;

    //private bool tour1 = true;

    public Scene _scene;

    public static Player instance;

    private float mouvx, mouvy;
    public float _sliderValue;

    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    instance = this;
    //    DontDestroyOnLoad(gameObject);
    //}

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _scene = SceneManager.GetActiveScene();

        //Debug.Log("La scène active est '" + _scene.name + "'. index= " + _scene.buildIndex);

        //_scenario.chargementObjet(_player, new Vector3(0, 3, 0), Quaternion.identity);
        if (_scene.buildIndex == 0)
        {
            // On s'assure que les listes sont vides
            _scenario.position.Clear();
            _scenario.rotation.Clear();
            _scoreText.text = PlayerPrefs.GetString("_score", "Score: " + ScoreValue);
            //Debug.Log("_scoreText.text= " + _scoreText.text);
        }


        if (_scene.buildIndex== 1)
        {
            transform.position = _scenario.positionPlayer;

            // On charge les arbres
            for (int i= 0; i< _scenario.position.Count; i++)
            {
                //Debug.Log("Chargement des arbres. i= " + i + " _scenario.position.Count= " + _scenario.position.Count +
                //    "  _scenario.rotation.Count= " + _scenario.rotation.Count);
                _scenario.chargementObjet(_arbre, _scenario.position[i], _scenario.rotation[i]);
            }

            // On lance les proies
            for (int i = 0; i < _scenario.position.Count; i++)
            {
                //Debug.Log("Chargement des arbres. i= " + i + " _scenario.position.Count= " + _scenario.position.Count +
                //    "  _scenario.rotation.Count= " + _scenario.rotation.Count);
                _scenario.chargementObjet(_proie, _scenario.position[i], _scenario.rotation[i]);
            }

        }

    }

    void Update()
    {
        //if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) 
        //{
        //    _rigidbody.AddForce(Input.GetAxis("Horizontal") * _speed * Time.fixedDeltaTime, 0f, Input.GetAxis("Vertical") * _speed * Time.fixedDeltaTime);
        //}
        _rigidbody.AddForce(mouvx * _speed * Time.fixedDeltaTime, 0f, mouvy * _speed * Time.fixedDeltaTime);
    }

    public void OnMove(InputValue _input)
    {
        mouvx = _input.Get<Vector2>().x;
        mouvy = _input.Get<Vector2>().y;

        Debug.Log("Input: x= " + mouvx + "Input: y= " + mouvy);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {

            _scenario.position.Add(other.transform.position);
            _scenario.rotation.Add(other.transform.rotation);
            // Debug.Log("Target_Trigger touché position = " + other.transform.position);
            Destroy(other.gameObject);
            Instantiate(_arbre, other.transform.position, other.transform.rotation);
            updateScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("On collision enter tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Target"))
        {
            _scenario.position.Add(collision.transform.position);
            _scenario.rotation.Add(collision.transform.rotation);
            // Debug.Log("Target touché position = " + collision.transform.position);
            Destroy(collision.gameObject);
            Instantiate(_arbre, collision.transform.position, collision.transform.rotation);
            //_scenario.indiceArbre++;

            //_scenario.affichePositionRotation();
            updateScore();
        }
        else if (collision.gameObject.CompareTag("Proie"))
        {
            _scenario.position.Add(collision.transform.position);
            _scenario.rotation.Add(collision.transform.rotation);
            //Debug.Log("Proie touché position = " + collision.transform.position);
            Destroy(collision.gameObject);
            Instantiate(_arbre, collision.transform.position, collision.transform.rotation);
            //_scenario.indiceArbre++;

            //_scenario.affichePositionRotation();
            updateScore();
        }

    }
    private void updateScore()
    {
        ScoreValue++;
        PlayerPrefs.SetString("_score", "Score: " + ScoreValue);
        _scoreText.text = PlayerPrefs.GetString("_score");

        //Debug.Log("updateScore après ++ " + ScoreValue);
        if(ScoreValue== 8)
        {
            _scenario.positionPlayer = transform.position;
            SceneManager.LoadScene("Level_2");
            //Debug.Log("Chargement de la scene 2.  SceneManager.GetActiveScene()" + SceneManager.GetActiveScene().name + " " +
            //    SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void changeVitesse()
    {
        Debug.Log("Changement de la vitesse" + _slider.GetComponent<Slider>().value);

        // _scrollbar.GetComponent<Scrollbar>().value
        _speed= _slider.GetComponent<Slider>().value;  
    }
}
