using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class SceneContextInstaller : MonoInstaller
{
    [SerializeField] private ImageManagerAlter _imageManager;
    [SerializeField] private GameObject _imagePrefab;
    public override void InstallBindings() {
        Container.Bind<ImageManagerAlter>().FromInstance(_imageManager).AsSingle();
        Container.BindFactory<RectTransform, ImageManagerAlter, ImageData, ImageData.Factory>().FromComponentInNewPrefab(_imagePrefab);
    }
}
