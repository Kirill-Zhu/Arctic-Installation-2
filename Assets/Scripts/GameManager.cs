using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private PageController _pageController;
    [SerializeField] private GameObject _sleepModeMeu;
    [SerializeField] private float _timeToGoSleep = 90;
    public float _timer;
    private bool _isInSleepMode;
    
    void Start()
    {
        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
        // Check if additional displays are available and activate each.

        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        _timeToGoSleep = _gameSettings.SecToSleepMode;
        _timer = _timeToGoSleep;
    }
    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            _isInSleepMode = false;
        }           
        else
            _timer = 0;

        if (Input.touchCount > 0|| Input.GetMouseButton(0))
            _timer = _timeToGoSleep;

        if (_timer == 0)
        {
            if (!_isInSleepMode)
                EnterSleepMode();
            

        }

        
    }
    private void EnterSleepMode()
    {
        _isInSleepMode = true;
        if(!_sleepModeMeu.gameObject.activeInHierarchy)
        {
            Debug.Log("Go Sleep Mode");
            _pageController.EnterSleepMode();
        }
            
    }
    
}
