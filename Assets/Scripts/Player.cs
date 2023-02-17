//using System.Collections;
//using System.Collections.Generic;
//using System.Collections;
//using Unity.VisualScripting;
//using static UnityEditor.PlayerSettings;
//using static UnityEditor.PlayerSettings;
//using UnityEngine.UIElements;

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    public Scene _scene;

    public static Player instance;

    private float mouvx, mouvy;
    public float _sliderValue;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _scene = SceneManager.GetActiveScene();

        //Debug.Log("La sc�ne active est '" + _scene.name + "'. index= " + _scene.buildIndex +
        //    " Vitesse = " + _speed);

        //_scenario.chargementObjet(_player, new Vector3(0, 3, 0), Quaternion.identity);
        if (_scene.buildIndex == 0)
        {
            // On s'assure que les listes sont vides
            _scenario.caracteristiqueArbre.Clear();
            _scenario.caracteristiqueProie.Clear();
            //Debug.Log("Level 1, apr�s clear de caracteristique arbre caracteristiqueArbre.Count= " + _scenario.caracteristiqueArbre.Count);

            //PlayerPrefs.SetString("_score", "Score: " + ScoreValue);
            _scoreText.text = PlayerPrefs.GetString("Score : " + ScoreValue);
            //Debug.Log("_scoreText.text= " + _scoreText.text);
        }


        if (_scene.buildIndex== 1)
        {
            transform.position = _scenario.positionPlayer;

            // On charge les arbres
            _scenario.retabliForet(_arbre);

            // On cr�e et on lance les proies
            for(int i= 0; i < 8; i++)
            {
                _scenario.chargementObjet(_proie, new Vector3(Random.Range(-10f, 10f), Random.Range(1f, 5f), Random.Range(-10f, 10f)), 
                    Quaternion.identity);

            }
            _scenario.lancerLesProies(_proie);
        }

    }

    void Update()
    {
        //if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) 
        //{
        //    _rigidbody.AddForce(Input.GetAxis("Horizontal") * _speed * Time.fixedDeltaTime, 0f, Input.GetAxis("Vertical") * _speed * Time.fixedDeltaTime);
        //}

        if (mouvx != 0f || mouvy != 0f)
        {
            _rigidbody.AddForce(mouvx * _speed * Time.fixedDeltaTime, 0f, mouvy * _speed * Time.fixedDeltaTime);
        }

        if (transform.position.y <= -2)
        {
            transform.position= new Vector3(0,5,0);
        }
    }

    public void OnMove(InputValue _input)
    {
        mouvx = _input.Get<Vector2>().x;
        mouvy = _input.Get<Vector2>().y;

        //Debug.Log("Input: x= " + mouvx + "Input: y= " + mouvy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target_Trigger"))
        {
            //Debug.Log("Target_Trigger touch� position = " + other.transform.position);

            _scenario.chargementObjet(_arbre, other.transform.position, other.transform.rotation);

            Destroy(other.gameObject);

            updateScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("On collision enter tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Target") || collision.gameObject.CompareTag("Proie"))
        {
            //Debug.Log("Target touch� position = " + collision.transform.position);

            _scenario.chargementObjet(_arbre, collision.transform.position, collision.transform.rotation);

            Destroy(collision.gameObject);

            updateScore();
        }

    }
    private void updateScore()
    {
        ScoreValue++;
        PlayerPrefs.SetString("_score", "Score: " + ScoreValue);
        _scoreText.text = PlayerPrefs.GetString("_score");

        //Debug.Log("updateScore apr�s ++ " + ScoreValue);
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
        //Debug.Log("Changement de la vitesse" + _slider.GetComponent<Slider>().value);

        // _scrollbar.GetComponent<Scrollbar>().value
        _speed= _slider.GetComponent<Slider>().value;  
    }

    private void OnApplicationQuit()
    {
        // D�truire la cl� PlayerPref
        PlayerPrefs.DeleteKey("_score");
    }
}
