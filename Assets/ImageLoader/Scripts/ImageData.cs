using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ImageData: MonoBehaviour
{
    public string imagePath;
    [SerializeField] private RectTransform _parentTransform;
    [SerializeField] private ImageManagerAlter _imageManager;
    private Button _button;
    [Inject]
    public void Construct(RectTransform parenTransform, ImageManagerAlter imageManager) {
        _parentTransform = parenTransform;
        _imageManager = imageManager;

    }
    private void Awake() {
    
        _button = GetComponent<Button>();
        transform.SetParent(_parentTransform, false);
        _button.onClick.AddListener(()=>_imageManager.SelectImage(this.gameObject));
    }
    public class Factory : PlaceholderFactory<RectTransform, ImageManagerAlter, ImageData> { }

}
