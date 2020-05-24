using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpContainer : MonoBehaviour
{
    [SerializeField]
    GameObject hpHeartPrefab = null;

    Health playerHp;
    GameObject[] hpHearts;
    // Start is called before the first frame update
    void Start()
    {
        playerHp = PlayerInputs.player.GetComponent<Health>();
        if (playerHp == null) playerHp = new Health(); // default to 1 hp
        Debug.Log("Player HP: " + playerHp);
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
