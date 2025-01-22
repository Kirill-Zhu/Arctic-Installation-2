using NaughtyAttributes;
using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[System.Serializable]   
public class ImageManagerAlter : MonoBehaviour
{

    public string _fileName;
    public GameObject imagePrefab; // ������ ��� �����������
    public RectTransform scrollViewContent; // ��������� ��� ����������� � ScrollView

    public List<string> imagePaths = new List<string>(); // ������ ����������� �����������
    public GameObject selectedImage; // ��������� ����������� ��� ��������
    public ImageManagerAlter imageManager;
    public ImageData.Factory factory;
    [Inject]
    public void Construct(ImageManagerAlter imageManager, ImageData.Factory factory) {
        this.imageManager = imageManager;
        this.factory = factory;
    }
    private void Awake() {
      
    }


    private void Start()
    {
       // LoadImages(); // ��������� ����������� ��� ������
    }
    private void Update() {
        if(Input.GetKeyUp(KeyCode.Space)) {
            LoadImage();
        }
        if(Input.GetKeyDown(KeyCode.Delete)) {
            RemoveSelectedImage();
        }
    }

    public void LoadImage()
    {
        var extensions = new[] {
            new ExtensionFilter("Images", "jpg", "png", "jpeg")
        };
        // ��������� ������ ������ ������
        string[] paths = StandaloneFileBrowser.OpenFilePanel("������ ������ ��������", "", extensions, false);
        if (paths.Length > 0)
        {
            foreach (string path in paths)
            {
                AddImage(path); // ��������� �����������
            }
        }
    }
    [Button("Create Factory Image")]
    public void CreatFactoryImage() {
       
         
        ImageData imageData = factory.Create(scrollViewContent, this);
    }
    private void AddImage(string path)
    {
        GameObject newImage = Instantiate(imagePrefab, scrollViewContent); // ������� ����� Image
        ImageData imageData = factory.Create(scrollViewContent, this);
       // var tmpImageData = newImage.AddComponent<ImageData>();

        var imageComponent = newImage.GetComponent<Image>();

        // ��������� �������� �� �����
        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        imageComponent.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        // ��������� ���� � ����������� � ����������
        // var imageData = newImage.AddComponent<ImageData>();
        imageData.imagePath = path;

        // ��������� ���� � ����������� � ������
        imagePaths.Add(path);
        SaveImages(); // ��������� ����
        imageComponent.SetNativeSize();
    }

    public void SelectImage(GameObject image)
    {
        // ����� �����������
        if (selectedImage != null)
        {
            // ������� ��������� � ����������� �����������
            selectedImage.GetComponent<Image>().color = Color.white;
        }

        selectedImage = image;
        // ����������, ��� ����������� ������� (��������, ������ ����)
        selectedImage.GetComponent<Image>().color = Color.yellow;
    }

    public void RemoveSelectedImage()
    {
        if (selectedImage != null)
        {
            string pathToRemove = selectedImage.GetComponent<ImageData>().imagePath;
            imagePaths.Remove(pathToRemove); // ������� ���� �� ������
            Destroy(selectedImage); // ������� ������ �� ScrollView
            selectedImage = null; // ���������� �����

            SaveImages(); // ��������� ���������
        }
    }

    private void SaveImages()
    {
        string path = Application.persistentDataPath + _fileName;
        File.WriteAllLines(path, imagePaths); // ��������� ���� � ��������� ����
    }

    public void LoadImages()
    {
        string path = Application.persistentDataPath  + _fileName;
        if (File.Exists(path))
        {
            string[] paths = File.ReadAllLines(path);
            foreach (string imgPath in paths)
            {
                AddImage(imgPath); // ��������� ����������� �� �����
            }
        }
    }
    public int ImagesCount() {
        string path = Application.persistentDataPath + _fileName;
        if (File.Exists(path)) {
            string[] paths = File.ReadAllLines(path);
            return path.Length;
        } else
            return 0;
    }
}
