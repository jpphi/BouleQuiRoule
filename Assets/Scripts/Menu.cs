//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private ScriptableObjectTest _soMenu;

    [SerializeField] private GameObject _sliderPlayer;
    [SerializeField] private GameObject _sliderProie;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Menu !" + _slider);
        //_somenu.vitesseJoueur = ;
        _soMenu.vitesseProie= _sliderProie.GetComponent<Slider>().value;
        _soMenu.vitesseJoueur= _sliderPlayer.GetComponent<Slider>().value;

    }

    // Update is called once per frame
    public void entrer()
    {
        Debug.Log("Entrée !");
        SceneManager.LoadScene("Level_1");
    }


    public void changeVitessePlayer()
    {
        //Debug.Log("Changement de la vitesse" + _slider.GetComponent<Slider>().value);

        // _scrollbar.GetComponent<Scrollbar>().value
        _soMenu.vitesseJoueur = _sliderPlayer.GetComponent<Slider>().value;
        Debug.Log("changeVitessePlayer ! Vitesse joueur= " + _soMenu.vitesseJoueur);

    }
    public void changeVitesseProie()
    {
        //Debug.Log("Changement de la vitesse" + _slider.GetComponent<Slider>().value);

        // _scrollbar.GetComponent<Scrollbar>().value
        _soMenu.vitesseProie = _sliderProie.GetComponent<Slider>().value;
        Debug.Log("changeVitesseProie ! Vitesse Proie= " + _soMenu.vitesseProie);

    }
}
