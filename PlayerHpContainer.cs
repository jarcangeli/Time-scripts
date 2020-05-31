using UnityEngine;
using UnityEngine.UI;

public class PlayerHpContainer : MonoBehaviour
{
    [SerializeField]
    GameObject hpHeartPrefab = null;

    Health playerHp;
    GameObject[] hpHearts;
    [SerializeField] Color emptyColor = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        playerHp = PlayerInputs.player.GetComponent<Health>();
        if (playerHp == null) playerHp = new Health(); // default to 1 hp
        hpHearts = GenerateHeartObjects();

        DrawHeartObjects();
    }

    GameObject[] GenerateHeartObjects()
    {
        float size = hpHeartPrefab.GetComponent<RectTransform>().rect.width;
        GameObject[] newHpHearts = new GameObject[playerHp.maxHealth];
        for (int i = 0; i < newHpHearts.Length; ++i)
        {
            GameObject hpHeart = Instantiate(hpHeartPrefab, transform);
            hpHeart.transform.localPosition = new Vector3(size * (i - 0.5f), 0f, 0f);
            newHpHearts[i] = hpHeart;

            GameObject emptyHpHeart = Instantiate(hpHeartPrefab, transform);
            emptyHpHeart.name = "EmptyHpHeart";
            emptyHpHeart.transform.localPosition = new Vector3(size * (i - 0.5f), 0f, 0f);
            emptyHpHeart.transform.localPosition += Vector3.forward;
            emptyHpHeart.GetComponent<Image>().color = emptyColor;
        }
        return newHpHearts;
    }

    void DrawHeartObjects()
    {
        for (int i = 0; i < hpHearts.Length; ++i)
        {
            if (i >= playerHp.currHealth)
            {
                hpHearts[i].SetActive(false);
            }
            else
            {
                hpHearts[i].SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        DrawHeartObjects();
    }
}
