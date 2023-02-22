using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _arbre, _player, _proie;
    [SerializeField] private ScriptableObjectTest _scenario;

    public PlayerPrefs _score;

    public Scene _scene;

    private Rigidbody _rigidbody;
    //private int ScoreValue;

    //public static Player instance;

    private float mouvx, mouvy, _speed;

    private GameObject[] listeproie, listearbre;

    private void Awake()
    {
        //instance= this;
        //ScoreValue = PlayerPrefs.GetInt("_score", 0);
        //_scoreText.text = ("Score : " + ScoreValue).ToString();

        _scenario.miseAZero();
        //_scenario.nouveauScore= 0;
        //Debug.Log("Dans awake du level1: _scoreText.text= " + _scoreText.text + " ScoreValue= " + ScoreValue);
        //Debug.Log("Dans awake du level1: _scoreText.text= " + _scoreText.text + " _scenario.nouveauScore= " + _scenario.nouveauScore);
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _speed = _scenario.vitesseJoueur;

        _scene = SceneManager.GetActiveScene();

        //Debug.Log("La scène active est '" + _scene.name + "'. index= " + _scene.buildIndex + " Vitesse = " + _speed);

        if (_scene.buildIndex == 1)
        {
            // On s'assure que les listes sont vides
            //_scenario.caracteristiqueArbre.Clear();
            //_scenario.caracteristiqueProie.Clear();
            //Debug.Log("Level 1, aprés clear de caracteristique arbre caracteristiqueArbre.Count= " + _scenario.caracteristiqueArbre.Count);
        }

        if (_scene.buildIndex== 2)
        {
            transform.position = _scenario.positionPlayer;

            // On charge les arbres
            retabliForet(_arbre);

            // On crée et on lance les proies
            for(int i= 0; i < 8; i++)
            {
                _scenario.creerObjet(_proie, new Vector3(Random.Range(-10f, 10f), Random.Range(1f, 5f), Random.Range(-10f, 10f)),
                    Quaternion.identity, new Vector3(1,1,1) );
            }
        }

        if (_scene.buildIndex == 3)
        {
            //_scenario.NbArbre = _scenario.NbProie = 0;

            //listeproie = GameObject.FindGameObjectsWithTag("Proie");

            // On charge les arbres
            retabliForet(_arbre);

            lanceProies(_proie);

            Debug.Log("Level 3. Nombre de proies= " + _scenario.caracteristiqueProie.Count + " Nombre arbre " + 
                _scenario.caracteristiqueArbre.Count);

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
            //_rigidbody.velocity=  new Vector3 (mouvx * _speed * Time.fixedDeltaTime, 0f, mouvy * _speed * Time.fixedDeltaTime);

        }

        // Remise en jeu du players
        if (transform.position.y <= -2)
        {
            transform.position= new Vector3(0,5,0);
        }

        if (Input.touchCount >0)
        {
            Touch touch= Input.GetTouch(0);

            //Debug.Log("Surface touché");

            //if (touch.phase == UnityEngine.TouchPhase.Began)
            //{
            //    Debug.Log("Surface touché, Began : " + touch.position);
            //}
            //if (touch.phase == UnityEngine.TouchPhase.Ended)
            //{
            //    Debug.Log("Surface touché, End : " + touch.position);
            //}
            //if (touch.phase == UnityEngine.TouchPhase.Moved)
            //{
            //    Debug.Log("Surface touché, on bouge : " + touch.position);
            //}

            Debug.Log("Delta Position " + touch.deltaPosition);
            _rigidbody.AddForce(touch.deltaPosition.x * _speed * Time.fixedDeltaTime, 0f, touch.deltaPosition.y * _speed * Time.fixedDeltaTime);
            //_rigidbody.velocity= new Vector3(touch.deltaPosition.x * _speed * Time.fixedDeltaTime, 0f,
            //  touch.deltaPosition.y * _speed * Time.fixedDeltaTime);

        }

        if ( (_scenario.NbProie <= 0) && (_scene.buildIndex== 3) )
        {
            SceneManager.LoadScene("LaFin");

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
            //_scenario.NbArbre++;

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

            _scenario.detruitObjet(collision.gameObject);

            updateScore();
        }

    }
    private void updateScore()
    {
        //ScoreValue= PlayerPrefs.GetInt("_score");
        //ScoreValue++;
        _scenario.nouveauScore++;
        //Debug.Log("updateScore scoreValue= " + ScoreValue);
        //PlayerPrefs.SetInt("_score", ScoreValue);

        _scoreText.text = "Score : " + _scenario.nouveauScore.ToString();

        //Debug.Log("updateScore après ++ " + ScoreValue);
        if (_scenario.nouveauScore == 8)
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

        else if (_scenario.nouveauScore == 16)
        {
            _scenario.positionPlayer = transform.position;
            _scenario.caracteristiqueArbre.Clear();
            _scenario.caracteristiqueProie.Clear();

            listeproie = GameObject.FindGameObjectsWithTag("Proie");
            listearbre = GameObject.FindGameObjectsWithTag("Arbre");

            foreach (GameObject el in listeproie)
            {
                //Debug.Log("objet: " + el.tag + " Nom de l'objet " + el.name);
                EnregistrementObjet(el, el.transform.position, el.transform.rotation, el.transform.localScale);
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

    private void OnApplicationQuit()
    {
        // Détruire la clé PlayerPref
        //PlayerPrefs.DeleteKey("_score");
        //Debug.Log("La clé doit être détruite ");
        //_scoreText.text = "";
        //_scenario.nouveauScore= 0;
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

        //Debug.Log("On rentre dans retabliForet caracteristiqueArbre.Count= " + _scenario.caracteristiqueArbre.Count);

        for (int i = 0; i < _scenario.caracteristiqueArbre.Count; i++)
        {
            _scenario.creerObjet(arbre, _scenario.caracteristiqueArbre[i].position, _scenario.caracteristiqueArbre[i].rotation, 
                _scenario.caracteristiqueArbre[i].scale);
        }

        //Debug.Log("retabliForest : arbre= " + nbarbre + " caracteristiqueArbre.Count= " + caracteristiqueArbre.Count);
    }
    public void lanceProies(GameObject proie)
    {
        //int nbarbre = 0;

        //Debug.Log("On lance les proies caracteristiqueProie.Count= " + _scenario.caracteristiqueProie.Count);

        for (int i = 0; i < _scenario.caracteristiqueProie.Count; i++)
        {
            //chargementObjet(arbre, caracteristiqueArbre[i].position, caracteristiqueArbre[i].rotation);
            _scenario.creerObjet(proie, _scenario.caracteristiqueProie[i].position, _scenario.caracteristiqueProie[i].rotation,
                _scenario.caracteristiqueProie[i].scale);

        }

        //Debug.Log("retabliForest : arbre= " + nbarbre + " caracteristiqueArbre.Count= " + caracteristiqueArbre.Count);
    }



}
