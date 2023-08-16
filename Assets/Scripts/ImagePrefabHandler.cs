using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ImagePrefabHandler : MonoBehaviour
{
    [SerializeField] private bool _showImage;
    [SerializeField] private bool _showText;
    private Image _image;
    private TextMeshProUGUI _text;
    private void Start()
    {
        
        _image = GetComponent<Image>();

        if (!_showImage)
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
        else
            _image.color = new Color(_image.color.r, _image.color.g,_image.color.b, 1);
       
        
        if (transform.childCount > 0)
        {
            _text = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            
            if (_showText)
                _text.gameObject.SetActive(true);
            else
                _text.gameObject.SetActive(false);
        }
                   
        
    }
}
