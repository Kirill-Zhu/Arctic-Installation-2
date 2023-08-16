using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum LedokolName
{
    Sibiryakov = 0,
    Krasin = 1,
    Chelyskin = 2,
    Litke = 3,
    SeverniyPulyus1 = 4,
    Vankuver = 5,
    Sedov = 6
}
public class PageController : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    [Header("Sleep Mode Properties")]
    [SerializeField] private SleepModeController _sleepModeController;
    [SerializeField] private CanvasGroup _mainMenucanvasGroup;
    [Header("Ecspidition")]
    public List<Ecspidition> _ecspiditionList;
    public Ecspidition _curentEcspidition;
    [Header("main menu")]
    [SerializeField] private Image _backImage;
    [SerializeField] private Image _nameImage;
    [SerializeField] private Slider _slider;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Scrollbar _scrollbar;

    [SerializeField] private GameObject _imageContainer;
 
    [Header("Bubble Properties")]
    [SerializeField] private GameObject _bubbleContainer;
    [SerializeField] private Image _bubbleUpPanel;
    [SerializeField] private Image _bubbleUpImage;

    
    [SerializeField] private float _imageFadeSec=1;
    
    [SerializeField] private Button _backbutton;
    [SerializeField] private Button _homeButton;
    [Header("Display 2")]
    [SerializeField] private CanvasGroup _displya2CanvasGroup;
    [SerializeField] private Image _displya2Image;

    private Coroutine _coroutine;
    public bool _canStartCoroutine = true;

    private void Awake()
    {
        _imageFadeSec = _gameSettings.FadeBetwenPagesSec;

    }
    public void OpenMainPage(int index)
    {
        _curentEcspidition = _ecspiditionList[index];
        HideHomeButton();
        if (_canStartCoroutine)
        {
            if(_coroutine!=null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(ChangeMainImages());
        }
            

       
    }
  
    IEnumerator ChangeMainImages()
    {
        _canStartCoroutine = false;
      
        _displya2CanvasGroup.alpha = 0;


        _mainMenucanvasGroup.alpha = 1;
        _backImage.GetComponent<CanvasGroup>().alpha = 0;
        _nameImage.GetComponent<CanvasGroup>().alpha =0;
        _imageContainer.GetComponent<CanvasGroup>().alpha = 0;
        
        //Change content

        SetMainMenuImages();
        ExitSleepMode();
        ChangeDisplay2Image(_curentEcspidition.display2BackSprite);
        
        _canStartCoroutine = true;
        
        //Fade In

        float fadeInIterations = 20;
        for (int i = 0; i <= fadeInIterations; i++)
        {
            
            _displya2CanvasGroup.alpha = i / fadeInIterations;
            _backImage.GetComponent<CanvasGroup>().alpha = i / fadeInIterations;
            _nameImage.GetComponent<CanvasGroup>().alpha = i / fadeInIterations;
            _imageContainer.GetComponent<CanvasGroup>().alpha = i / fadeInIterations;
            yield return new WaitForSeconds(_imageFadeSec / fadeInIterations);
        }
    }
    private void SetMainMenuImages()
    {
        //Kostyl
         Vector2 tmpPos = new Vector2(-539, 32);
        _imageContainer.GetComponent<RectTransform>().SetLocalPositionAndRotation(tmpPos, Quaternion.identity);
        //End Kostyl
        
        _backImage.sprite = _curentEcspidition.BackImage;
        _nameImage.sprite = _curentEcspidition.NameImage;

        _backImage.SetNativeSize();
        _nameImage.SetNativeSize();

        if (_curentEcspidition.MainTextPrefabs.Count < 2)
        {
            _scrollRect.GetComponent<ScrollRectHandler>().CanDrag(false);
           
            _imageContainer.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            Vector2 size = new Vector2(1738, 532);
            _imageContainer.GetComponent<RectTransform>().sizeDelta = size;
            _scrollRect.horizontalNormalizedPosition = 0;
            _slider.value = 0;
            _slider.gameObject.SetActive(false);
            _scrollbar.value = 0;

        }
        else
        {
            _scrollRect.GetComponent<ScrollRectHandler>().CanDrag(true);

            _scrollRect.horizontalNormalizedPosition = 0;
            _scrollbar.value = 0;
            _slider.value = 0;
            _imageContainer.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            _slider.gameObject.SetActive(true);

        } 
            foreach (Transform child in _imageContainer.transform)
               
            Destroy(child.gameObject);
        
        GameObject obj = new GameObject();
        
        for (int i = 0; i < _curentEcspidition.MainTextPrefabs.Count; i++)
        {
            GameObject tmpObj = Instantiate(_curentEcspidition.MainTextPrefabs[i], _imageContainer.transform);
            Image image;

            if (tmpObj.GetComponent<Image>())
                image = tmpObj.GetComponent<Image>();
            else
                image = tmpObj.AddComponent<Image>();

            image.SetNativeSize();
        }

        foreach (Transform t in _bubbleContainer.transform)
            Destroy(t.gameObject);

        for (int i = 0; i < _curentEcspidition.BubbleMenuPhotos.Count; i++)
        {
            GameObject tmpobj = Instantiate(obj, _bubbleContainer.transform);
            Image img = tmpobj.AddComponent<Image>();
            img.sprite = _curentEcspidition.BubbleMenuPhotos[i];
            img.SetNativeSize();

            var animator = tmpobj.AddComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/BubbleController");
            animator.speed = _gameSettings.IconsAnimationSpeed;

            Button butt = tmpobj.AddComponent<Button>();
            butt.onClick.AddListener(() => OpenBuble(tmpobj.transform.GetSiblingIndex()));

        }
        Destroy(obj);

        _scrollRect.horizontalNormalizedPosition = 0;
        _scrollbar.value = 0;
        _slider.value = 0;
    }
   
 
    public void OpenBuble(int index)
    {
        _scrollRect.GetComponent<ScrollRectHandler>().CanDrag(true);
       
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _imageContainer.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;


        //Kositl
        Vector2 tmpPos = new Vector2(-539, 220);
        _imageContainer.GetComponent<RectTransform>().SetLocalPositionAndRotation(tmpPos, Quaternion.identity);
        //End Kostyl
        
        ShowHomeButton();

        _scrollRect.horizontal = true;
        _slider.gameObject.SetActive(true);
  
        Debug.Log("Open Bubble" + index);
        _nameImage.GetComponent<CanvasGroup>().alpha = 0;

        foreach (Transform child in _imageContainer.transform)
            Destroy(child.gameObject);
        List<GameObject> list = _curentEcspidition.GetBubbleList(index);

        if (list.Count < 2)
        {

             _scrollRect.GetComponent<ScrollRectHandler>().CanDrag(false);
          
            _imageContainer.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            Vector2 size = new Vector2(1738, 532);
            _imageContainer.GetComponent<RectTransform>().sizeDelta = size;
            _scrollRect.horizontalNormalizedPosition = 0;
            _slider.value = 0;
            _slider.gameObject.SetActive(false);
            _scrollbar.value = 0;
        }
        else
        {
            _scrollRect.GetComponent<ScrollRectHandler>().CanDrag(true);

            _scrollRect.horizontalNormalizedPosition = 0;
            _scrollbar.value = 0;
            _slider.value = 0;
            _imageContainer.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            _slider.gameObject.SetActive(true);
        }   
        


            for (int i = 0; i < list.Count; i++)
            {

                GameObject tmpObj = Instantiate(list[i], _imageContainer.transform);
                Image img = tmpObj.GetComponent<Image>();

                img.SetNativeSize();
                //Button butt = img.gameObject.AddComponent<Button>();
                //butt.onClick.AddListener(() => BubbleUp(img.sprite));
            }

        _scrollRect.horizontalNormalizedPosition = 0;
        _scrollbar.value = 0;
        _slider.value = 0;

    }

    //public void BubbleUp(Sprite sprite)
    //{
    //    _bubbleUpPanel.raycastTarget = true;
    //    _bubbleUpPanel.GetComponent<CanvasGroup>().alpha = 1;
    //    _bubbleUpImage.sprite = sprite;
    //    _bubbleUpImage.SetNativeSize();
    //    _bubbleUpImage.GetComponent<CanvasGroup>().alpha = 1;
    //    _bubbleUpImage.GetComponent<RectTransform>().sizeDelta *= 1.3f;
    //}
    //public void BubbleDown()
    //{
    //    _bubbleUpPanel.raycastTarget = false;
    //    _bubbleUpPanel.GetComponent<CanvasGroup>().alpha = 0;
    //    _bubbleUpImage.GetComponent<CanvasGroup>().alpha = 0;

    //}
    public void EnterSleepMode()
    {
        Debug.Log("Enter Sleep mode");
        if(_canStartCoroutine)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(GoSleep());
        }      
    }
    private IEnumerator GoSleep()
    {
       

        _canStartCoroutine = false;


        _mainMenucanvasGroup.alpha = 0;
        _sleepModeController.GetComponent<CanvasGroup>().alpha = 0;
        _nameImage.GetComponent<CanvasGroup>().alpha = 0;
        _displya2CanvasGroup.alpha = 0;
        _backImage.GetComponent<CanvasGroup>().alpha = 0;
        _nameImage.GetComponent<CanvasGroup>().alpha = 0;
        _imageContainer.GetComponent<CanvasGroup>().alpha = 0;
        _canStartCoroutine = true;
        _sleepModeController.EnterToSleepMode();
        ChangeDisplay2Image(_sleepModeController.SleepModeSprite);
        float fadeInIterations = 20;
        for (int i = 0; i <= fadeInIterations; i++)
        {
            _sleepModeController.GetComponent<CanvasGroup>().alpha = i / fadeInIterations;
            _displya2CanvasGroup.alpha = i / fadeInIterations;
            yield return new WaitForSeconds(_imageFadeSec / fadeInIterations);
        }
        
    }
    public void ExitSleepMode()
    {
        _sleepModeController.ExitSleepMode();
       
    }
    private void ChangeDisplay2Image(Sprite sprite)
    {
        _displya2Image.sprite = sprite;
    }
    public void BackButton()
    {


        if (_canStartCoroutine)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(ChangeMainImages());
        }
        _backbutton.onClick.RemoveAllListeners();
        HideHomeButton();
    }
    private void HideHomeButton()
    {
        Debug.Log("Hide home Button");
        _homeButton.gameObject.SetActive(false);
        _backbutton.onClick.RemoveAllListeners();
        _backbutton.onClick.AddListener(EnterSleepMode);
       
    }
    private void ShowHomeButton()
    {
        Debug.Log("Show home button");
        _homeButton.gameObject.SetActive(true);
        _backbutton.onClick.RemoveAllListeners();
        _backbutton.onClick.AddListener(BackButton);
    }
   
}
