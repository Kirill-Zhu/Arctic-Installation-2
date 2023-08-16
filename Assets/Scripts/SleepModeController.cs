using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SleepModeController : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private Sprite _sleepModeSprite;
    [SerializeField] private List<Animator> _animatorList;
    public Sprite SleepModeSprite { get { return _sleepModeSprite;} }

    private void Awake()
    {
        foreach (var anim in _animatorList)
            anim.speed = _gameSettings.IconsAnimationSpeed;
    }
    public void EnterToSleepMode()
    {

        gameObject.SetActive(true);

        foreach (var anim in _animatorList)      
            anim.SetTrigger("Fade In");                 

        
    }
    public void ExitSleepMode()
    {
        gameObject.SetActive(false);
    }
}
