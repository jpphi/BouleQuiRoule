using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int ScoreValue;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _speed= 100f;
    [SerializeField] private GameObject _arbre, _player, _proie;
    [SerializeField] private ScriptableObjectTest _scenario;
    [SerializeField] private GameObject _slider;

    public PlayerPrefs _score;

    public Scene _scene;

    //public static Player instance;

    private float mouvx, mouvy;
    public float _sliderValue;

    private GameObject[] listeproie, listearbre;

    private void Awake()
    {
        //instance= this;
        ScoreValue = PlayerPrefs.GetInt("_score", 0);
        PlayerPrefs.SetInt("_score", ScoreValue);

        _scoreText.text = ("Score : " + ScoreValue).ToString();

        Debug.Log("Dans awake du level1: _scoreText.text= " + _scoreText.text + " ScoreValue= " + ScoreValue);

    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _scene = SceneManager.GetActiveScene();

        //Debug.Log("La scène active est '" + _scene.name + "'. index= " + _scene.buildIndex +
        //    " Vitesse = " + _speed);

        if (_scene.buildIndex == 0)
        {
            // On s'assure que les listes sont vides
            //_scenario.caracteristiqueArbre.Clear();
            //_scenario.caracteristiqueProie.Clear();
            //Debug.Log("Level 1, aprés clear de caracteristique arbre caracteristiqueArbre.Count= " + _scenario.caracteristiqueArbre.Count);
            _scenario.NbArbre = _scenario.NbProie = 0;

        }

        if (_scene.buildIndex== 1)
        {
            transform.position = _scenario.positionPlayer;
            _scenario.NbArbre = _scenario.NbProie = 0;

            // On charge les arbres
            retabliForet(_arbre);

            // On crée et on lance les proies
            for(int i= 0; i < 8; i++)
            {
                _scenario.creerObjet(_proie, new Vector3(Random.Range(-10f, 10f), Random.Range(1f, 5f), Random.Range(-10f, 10f)),
                    Quaternion.identity, new Vector3(1,1,1) );

                _scenario.NbProie++;

            }
            //_scenario.lancerLesProies(_proie);
        }

        if (_scene.buildIndex == 2)
        {
            _scenario.NbArbre = _scenario.NbProie = 0;

            //listeproie = GameObject.FindGameObjectsWithTag("Proie");

            // On charge les arbres
            retabliForet(_arbre);

            lanceProies(_proie);

            Debug.Log("Level 3. Nombre de proies= " + _scenario.NbProie + " Nombre arbre " + _scenario.NbArbre);
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
            //Debug.Log("Target_Trigger touché position = " + other.transform.position);

            _scenario.creerObjet(_arbre, other.transform.position, other.transform.rotation, new Vector3(1,1,1));
            _scenario.NbArbre++;

            _scenario.detruitObjet(other.gameObject);

            updateScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("On collision enter tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Target") || collision.gameObject.CompareTag("Proie"))
        {
            //Debug.Log("Target touché position = " + collision.transform.position);

            _scenario.creerObjet(_arbre, collision.transform.position, collision.transform.rotation, new Vector3(1, 1, 1));
            _scenario.NbArbre++;

            _scenario.detruitObjet(collision.gameObject);

            updateScore();
        }

    }
    private void updateScore()
    {
        ScoreValue= PlayerPrefs.GetInt("_score");
        ScoreValue++;
        Debug.Log("updateScore scoreValue= " + ScoreValue);
        PlayerPrefs.SetInt("_score", ScoreValue);

        _scoreText.text = "Score : " + ScoreValue.ToString();
        Debug.Log("_scoreText.text= " + _scoreText.text);

        //Debug.Log("updateScore après ++ " + ScoreValue);
        if (ScoreValue == 8)
        {
            _scenario.caracteristiqueArbre.Clear();
            _scenario.positionPlayer = transform.position;

            listearbre = GameObject.FindGameObjectsWithTag("Arbre");

            foreach (GameObject el in listearbre)
            {
                //Debug.Log("objet: " + el.tag + " Nom de l'objet " + el.name);
                EnregistrementObjet(el, el.transform.position, el.transform.rotation, el.transform.localScale);
            }

            SceneManager.LoadScene("Level_2");
            //Debug.Log("Chargement de la scene 2.  SceneManager.GetActiveScene()" + SceneManager.GetActiveScene().name + " " +
            //    SceneManager.GetActiveScene().buildIndex);
        }

        else if (ScoreValue == 16)
        {
            _scenario.positionPlayer = transform.position;
            _scenario.caracteristiqueArbre.Clear();
            _scenario.caracteristiqueProie.Clear();

            listeproie = GameObject.FindGameObjectsWithTag("Proie");
            listearbre = GameObject.FindGameObjectsWithTag("Arbre");

            foreach (GameObject p in listeproie)
            {
                //Debug.Log("objet: " + el.tag + " Nom de l'objet " + el.name);
                EnregistrementObjet(p, p.transform.position, p.transform.rotation, p.transform.localScale);
            }


            foreach (GameObject el in listearbre)
            {
                //Debug.Log("objet: " + el.tag + " Nom de l'objet " + el.name);
                EnregistrementObjet(el, el.transform.position, el.transform.rotation, el.transform.localScale);
            }

            SceneManager.LoadScene("Level_3");
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
        // Détruire la clé PlayerPref
        PlayerPrefs.DeleteKey("_score");
        Debug.Log("La clé doit être détruite ");
        _scoreText.text = "";
    }

    public void EnregistrementObjet(GameObject obj, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        //Debug.Log("Tag objet= " + obj.tag + " doit être chargé en  " + pos + " rotation " + rot);
        if (obj.CompareTag("Proie"))
        {
            _scenario.caracteristiqueProie.Add(new CaracteristiqueProie
            {
                position = pos,
                rotation = rot,
                scale = scale,
                //numeroProie= _soproie.IndiceProix,
            });

        }
        if (obj.CompareTag("Arbre"))
        {
            //Debug.Log("On créer un arbre à la position pos= " + pos);
            _scenario.caracteristiqueArbre.Add(new CaracteristiqueArbre
            {
                position = pos,
                rotation = rot,
                scale = scale,
                //numero= indexArbre++
            });

        }


    }

    public void retabliForet(GameObject arbre)
    {
        //int nbarbre = 0;

        Debug.Log("On rentre dans retabliForet caracteristiqueArbre.Count= " + _scenario.caracteristiqueArbre.Count);

        for (int i = 0, NbArbre= 0; (i < _scenario.caracteristiqueArbre.Count) && (i < _scenario.maxArbre); i++, NbArbre++)
        {
            _scenario.creerObjet(arbre, _scenario.caracteristiqueArbre[i].position, _scenario.caracteristiqueArbre[i].rotation, 
                _scenario.caracteristiqueArbre[i].scale);
        }

        //Debug.Log("retabliForest : arbre= " + nbarbre + " caracteristiqueArbre.Count= " + caracteristiqueArbre.Count);
    }
    public void lanceProies(GameObject proie)
    {
        //int nbarbre = 0;

        Debug.Log("On lance les proies caracteristiqueProie.Count= " + _scenario.caracteristiqueProie.Count);

        for (int i = 0, NbProie= 0; (i < _scenario.caracteristiqueProie.Count) && (i < _scenario.maxProie); i++, NbProie++)
        {
            //chargementObjet(arbre, caracteristiqueArbre[i].position, caracteristiqueArbre[i].rotation);
            _scenario.creerObjet(proie, _scenario.caracteristiqueProie[i].position, _scenario.caracteristiqueProie[i].rotation,
                _scenario.caracteristiqueProie[i].scale);

        }

        //Debug.Log("retabliForest : arbre= " + nbarbre + " caracteristiqueArbre.Count= " + caracteristiqueArbre.Count);
    }



}
