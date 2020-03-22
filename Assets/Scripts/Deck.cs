using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {

    #region 덱 데이터
    public GameObject card_prefab;
    public Text draw_cnt_text;

    int[] user_deck_idx_arr; //유저가 드로우 가능한 카드
    Card draw_card; //현재 드로우한 카드

    int draw_cnt; //드로우 가능한 카드 갯수
    public int _draw_cnt;

    #endregion

    #region 드로우 함수
    //랜덤으로 덱에서 카드한장 뽑아서 draw_card에 저장
    bool random_draw(int[] deck)
    {
        int deck_card_cnt;
        int draw_card_num;

        deck_card_cnt = deck.Length;

        draw_card_num = Random.Range(0, deck_card_cnt);

        //카드 오브젝트 생성해서
        GameObject go_draw_card = Instantiate(card_prefab);

        draw_card = go_draw_card.GetComponent<Card>();

        //카드 인덱스값 넣어주고 init 돌려줌
        draw_card.GetComponent<Card>()._card_idx = deck[draw_card_num];

        draw_card.SendMessage("card_init");

        return true;    //드로우에 성공하면 true
    }

    //밖에서 실행시켜서 랜덤 드로우된 카드 데이터 반환
    public Card draw_event()
    {
        if (draw_cnt <= 0)
        {
            Debug.Log("error 98623 - 뽑을 수 있는 카드 수가 없음");
            return null; //뽑을수 있는 카드가 없으면 뽑을 수 없게
        }

        //카드 한장 랜덤으로 뽑고
        bool draw_ok = random_draw(user_deck_idx_arr);

        if (draw_ok)
        {
            //드로우 카운트 줄여주고
            draw_cnt--;

            output_draw_cnt();

            //뽑은 카드 데이터를 반환
            return draw_card;
        }

        else
        {
            Debug.Log("error 98792 - 드로우 실패 Deck클래스 확인");
            return null;
        }
    }
    #endregion

    #region 드로우 카드수 함수
    void check_draw_cnt()
    {
        if(draw_cnt <= 0)
        {
            Debug.Log("게임 오버");
        }
    }
    #endregion

    #region 데이터 출력 함수
    void output_user_deck()
    {
        Debug.Log("유저 덱 ui 생성");
    }

    void output_draw_cnt()
    {
        draw_cnt_text.text = "남은 카드 수 : " + draw_cnt;
    }

    #endregion

    void deck_init()
    {
        draw_cnt = _draw_cnt;

        output_draw_cnt();

        //임시로 한칸만(구현)
        user_deck_idx_arr = new int[1];
        for (int i = 0; i < user_deck_idx_arr.Length; i++)
        {
            user_deck_idx_arr[i] = i;
        }
    }

    void deck_apply_damage(int Damage)
    {
        draw_cnt -= Damage;

        check_draw_cnt();

        //바뀐 데이터값 출력(구현)

    }

    private void Awake()
    {
        deck_init();
    }
}
