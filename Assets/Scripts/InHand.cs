using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InHand : MonoBehaviour {

    #region 손패 데이터값
    public Button[] active_card_but_arr;

    List<Card> now_hand_card_list = new List<Card>();   //지금 들고있는 카드 배열
    List<int> active_card_num_list = new List<int>();
    int hand_card_cnt;      //들고있는 카드 수
    int hand_card_cnt_limit = 5;

    bool is_ban_use_card = false;
    int ban_use_card_turn;

    bool is_active_inhand = false;

    Deck user_deck;     //init과정에서 덱 클래스 가져와줄것
    CardSelect cardSelect;

    #endregion

    void turn_start()
    {
        GameManager.gameManager.SendMessage("now_inhand_state", is_ban_use_card);
    }

    void turn_end()
    {
        if (is_ban_use_card)
        {
            ban_use_card_turn--;

            if(ban_use_card_turn <= 0)
            {
                is_ban_use_card = false;
            }
        }
    }

    void ban_use_card(int turn)
    {
        is_ban_use_card = true;

        ban_use_card_turn = turn;
    }

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

        card_list_update();
    }

    #endregion

    #region 손패 카드 사용 함수
    public void active_card_select_inHand(int select_num)
    {
        if (!is_active_inhand)
        {
            Debug.Log("현재 카드 사용 불가");
            return;
        }

        Debug.Log("손에서 " + select_num + "번째 카드 사용");

        active_card_num_list.Add(select_num);
    }

    public void cancel_active_card_select_inHand(int select_num)
    {
        if (!active_card_num_list.Contains(select_num))
        {
            Debug.Log("error 65248 - 사용한 카드가 아님 버튼인자 확인");
            return;
        }

        Debug.Log("손에서 " + select_num + "번째 카드 사용취소");

        active_card_num_list.Remove(select_num);
    }

    void active_card_inHand(int select_num)
    {
        if (select_num >= now_hand_card_list.Count)
        {
            Debug.Log("error 34789 - 사용 가능한 카드가 없음");
            return;
        }

        Card temp_card = now_hand_card_list[select_num];

        Debug.Log("손에서 " + select_num + "번째 카드 사용처리 성공");

        temp_card.SendMessage("use_card");
    }

    void delete_card_inHand(int select_num)
    {
        Debug.Log("손에서 " + select_num + "번째 카드 삭제");

        Card temp_card = now_hand_card_list[select_num];

        Destroy(temp_card.gameObject);

        now_hand_card_list.Remove(temp_card);

        hand_card_cnt--;

        card_list_update();
    }

    public void conf_active_card_inHand()
    {
        is_active_inhand = false;

        for (int i = 0; i < active_card_num_list.Count; i++)
        {
            active_card_inHand(active_card_num_list[i]);
        }

        for (int j = 0; j < active_card_num_list.Count; j++)
        {
            delete_card_inHand(active_card_num_list[j]);
        }

        active_card_num_list = new List<int>();

        GameManager.gameManager.SendMessage("progress_wait_end");
    }

    #endregion

    #region 손패 카드 출력 함수

    void select_card_start()
    {
        //이쪽 부근 코드 나중에 정리(구현)
        Debug.Log("카드 선택 시작");
        is_active_inhand = true;

        card_list_update();
    }

    void card_list_update()
    {
        int now_card_cnt = now_hand_card_list.Count;

        if(now_card_cnt > hand_card_cnt_limit + 4)
        {
            Debug.Log("error 32620 - ui 이상의 카드 수 감지");
        }
        else if(now_card_cnt > hand_card_cnt_limit)
        {
            now_card_cnt = hand_card_cnt_limit;
        }

        Debug.Log("손에 " + now_card_cnt + "장 있음");

        for (int i = 0; i < hand_card_cnt_limit; i++)
        {
            active_card_but_arr[i].gameObject.SetActive(false);
        }

        for (int j = 0; j < now_card_cnt; j++)
        {
            active_card_but_arr[j].gameObject.SetActive(true);
        }
    }

    #endregion

    void abandon_card_inHand(List<int> abandon_card_num_list)
    {
        if(hand_card_cnt > hand_card_cnt_limit)
        {
            for(int i = 0; i < abandon_card_num_list.Count; i++)
            {
                delete_card_inHand(abandon_card_num_list[i]);
            }
        }
        else
        {
            Debug.Log("손패 수 이상 없음");
        }

        GameManager.gameManager.SendMessage("next_phase","abandon_card_hand");
    }

    void abandon_card_but_event()
    {
        List<int> temp_card_num_list = cardSelect.return_select_card_num_list();

        abandon_card_inHand(temp_card_num_list);
    }

    void abandon_card_start_event()
    {
        int select_card_cnt = hand_card_cnt - hand_card_cnt_limit;

        if (select_card_cnt > 0)
        {
            Debug.Log(select_card_cnt + "장 버리기 이벤트 실행");
            cardSelect.card_select_start(abandon_card_but_event, select_card_cnt);
        }
        else
        {
            GameManager.gameManager.SendMessage("next_phase","abandon_card_start");
        }
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

    //나중에 뺼것(구현)
    private void Start()
    {
        draw_card_inHand();
        draw_card_inHand();
        draw_card_inHand();
    }
}
