using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHand : MonoBehaviour {

    #region 손패 데이터값
    List<Card> now_hand_card_list = new List<Card>();   //지금 들고있는 카드 배열
    int hand_card_cnt;      //들고있는 카드 수
    int hand_card_cnt_limit = 5;

    Deck user_deck;     //init과정에서 덱 클래스 가져와줄것
    CardSelect cardSelect;

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

    void active_card_inHand(int select_num)
    {
        if (select_num >= now_hand_card_list.Count)
        {
            Debug.Log("error 34789 - 사용 가능한 카드가 없음");
            return;
        }

        Card temp_card = now_hand_card_list[select_num];

        Debug.Log("손에서 " + select_num + "번째 카드 효과 발동");

        temp_card.SendMessage("active_card");

        delete_card_inHand(select_num);
    }

    void delete_card_inHand(int select_num)
    {
        Debug.Log("손에서 " + select_num + "번째 카드 삭제");

        Card temp_card = now_hand_card_list[select_num];

        Destroy(temp_card.gameObject);

        now_hand_card_list.RemoveAt(select_num);
    }

    #endregion

    #region 손패 카드 출력 함수

    void output_inHand()
    {
        //카드 데이터 출력 함수
    }

    #endregion

    void abandon_card_inHand(int[] abandon_card_num_arr)
    {
        if(hand_card_cnt > hand_card_cnt_limit)
        {
            for(int i = 0; i < abandon_card_num_arr.Length; i++)
            {
                delete_card_inHand(abandon_card_num_arr[i]);
            }
        }
        else
        {
            Debug.Log("손패 수 이상 없음");
        }
    }

    void abandon_card_but_event()
    {
        int[] temp_card_num_arr = cardSelect.return_select_card_num_arr();

        abandon_card_inHand(temp_card_num_arr);
    }

    void abandon_card_start_event()
    {
        cardSelect.but_func_insert(abandon_card_but_event);

        int select_card_cnt = hand_card_cnt - hand_card_cnt_limit;
        cardSelect.SendMessage("card_select_start", select_card_cnt);
    }

    void inhand_init()
    {
        user_deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
        cardSelect = GameObject.FindGameObjectWithTag("CardSelect").GetComponent<CardSelect>();
    }

    private void Awake()
    {
        inhand_init();
    }
}
