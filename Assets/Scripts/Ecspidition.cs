using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName ="Custom/Ecspidition")]
public class Ecspidition : ScriptableObject
{
    public Sprite BackImage;
    public Sprite BackImage2;
    public Sprite NameImage;
    public List<GameObject> MainTextPrefabs;
    [Header("bubbles")]
    public List<Sprite> BubbleMenuPhotos;

    public List<GameObject> BubbleList0;
    public List<GameObject> BubbleList1;
    public List<GameObject> BubbleList2;
    public List<GameObject> BubbleList3;
    public List<GameObject> BubbleList4;

    public string fileName= "/TestDataPath.txt";
    public List<string> imagePaths;
    [Header("Display 2")]
    public Sprite display2BackSprite;
    public List<GameObject> GetBubbleList(int index)
    {
        Dictionary<int, List<GameObject>> dict = new Dictionary<int, List<GameObject>>() { [0] = BubbleList0, [1] = BubbleList1, [2] = BubbleList2, [3] = BubbleList3, [4] = BubbleList4 };
        return dict[index];
    }

}
