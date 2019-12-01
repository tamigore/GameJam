using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealthVisual : MonoBehaviour
{
    [SerializeField] private Sprite heartSpriteFull;
    [SerializeField] private Sprite heartSpriteEmpty;
    [SerializeField] private Sprite heartSpriteHalf;
    [SerializeField] private Sprite heartSpriteQuarter;
    [SerializeField] private Sprite heartSpriteThreeQuarter;
    private List<HeartImage> heartImageList;
    public int playerNumber = 1;
    private HearthHealthSystem hearthHealthSystem;

    private void Awake()
    {
        heartImageList = new List<HeartImage>();
    }
    private void Start()
    {
        HearthHealthSystem hearthHealthSystem = new HearthHealthSystem(4);
        SetHeartHealthSystem(hearthHealthSystem);
    }

    public void SetHeartHealthSystem(HearthHealthSystem hearthHealthSystem)
    {
        this.hearthHealthSystem = hearthHealthSystem;

        List<HearthHealthSystem.Heart> heartList = hearthHealthSystem.GetHeartList();
        Vector2 heartAnchoredPosition = new Vector2(0, 0);
        for (int i = 0; i < heartList.Count; i++)
        {
            HearthHealthSystem.Heart heart = heartList[i];
            CreateHeartImage(heartAnchoredPosition).SetHeartFragments(heart.GetFragmentAmount());
            heartAnchoredPosition += new Vector2(30, 0);
        }
        hearthHealthSystem.OnDamaged += HearthHealthSystem_OnDamaged;


    }

    private void HearthHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        List<HearthHealthSystem.Heart> heartList = hearthHealthSystem.GetHeartList();
        for (int i = 0; i < heartImageList.Count; i++){
            HeartImage heartImage = heartImageList[i];
            HearthHealthSystem.Heart heart = heartList[i];
            heartImage.SetHeartFragments(heart.GetFragmentAmount());
        }
    }



    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        //create Game Object
        GameObject heartGameObject = new GameObject("Heart", typeof(Image));

        //Set as child of the object
        heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = Vector3.zero;

        //Locate and size heart
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);

        //Set heart sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heartSpriteFull;

        HeartImage heartImage = new HeartImage(this, heartImageUI);
        heartImageList.Add(heartImage);

        return heartImage;
    }
    public class HeartImage
    {
        private Image heartImage;
        private HeartHealthVisual heartHealthVisual;

        public HeartImage(HeartHealthVisual heartHealthVisual, Image heartImage)
        {
            this.heartHealthVisual = heartHealthVisual;
            this.heartImage = heartImage;
        }

        public void SetHeartFragments(int fragments)
        {
            switch(fragments)
            {
                case 0: heartImage.sprite = heartHealthVisual.heartSpriteEmpty; break;
                case 1: heartImage.sprite = heartHealthVisual.heartSpriteQuarter; break;
                case 2: heartImage.sprite = heartHealthVisual.heartSpriteHalf; break;
                case 3: heartImage.sprite = heartHealthVisual.heartSpriteThreeQuarter; break;
                case 4: heartImage.sprite = heartHealthVisual.heartSpriteFull; break;
            }
        }

    }
}
