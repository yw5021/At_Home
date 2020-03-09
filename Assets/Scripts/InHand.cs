using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHand : MonoBehaviour {

    #region 손패 데이터값
    List<Card> now_hand_card_list = new List<Card>();   //지금 들고있는 카드 배열
    int hand_card_cnt;      //들고있는 카드 수

    Deck user_deck;     //init과정에서 덱 클래스 가져와줄것

    #endregion

    #region 손패 카드 드로우 함수

    void draw_card_inHand()
    {
        Card temp_card = user_deck.draw_event();

        if (temp_card == null)
        {
            Debug.Log("error 25256 - 카드 뽑기 실패");
            return;
        }

        now_hand_card_list.Add(temp_card);

        hand_card_cnt++;
    }

    #endregion

    #region 손패 카드 사용 함수
    public void active_card_select_inHand(int select_num)
    {
        Debug.Log("손에서 " + select_num + "번째 카드 사용");

        active_card_inHand(select_num);
    }

    void active_card_inHand(int num)
    {
        if (num >= now_hand_card_list.Count)
        {
            Debug.Log("error 34789 - 사용 가능한 카드가 없음");
            return;
        }

        Card temp_card = now_hand_card_list[num];

        Debug.Log("손에서 " + num + "번째 카드 효과 발동");

        temp_card.SendMessage("active_card");

        delete_card_inHand(num);
    }

    void delete_card_inHand(int num)
    {
        Debug.Log("손에서 " + num + "번째 카드 삭제");

        Card temp_card = now_hand_card_list[num];

        Destroy(temp_card.gameObject);

        now_hand_card_list.RemoveAt(num);
    }

    #endregion

    #region 손패 카드 출력 함수

    void output_inHand()
    {
        //카드 데이터 출력 함수
    }

    #endregion

    void inhand_init()
    {
        user_deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
    }

    private void Awake()
    {
        inhand_init();
    }
}
