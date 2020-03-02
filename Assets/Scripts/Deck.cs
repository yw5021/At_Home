using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    #region 덱 데이터

    Card[] user_deck_arr; //유저가 드로우 가능한 카드
    Card draw_card; //현재 드로우한 카드

    int draw_cnt; //드로우 가능한 카드 갯수

    #endregion

    #region 드로우 함수
    //랜덤으로 덱에서 카드한장 뽑아서 draw_card에 저장
    bool random_draw(Card[] deck)
    {
        int deck_card_cnt;
        int draw_card_num;

        deck_card_cnt = deck.Length;

        draw_card_num = Random.Range(0, deck_card_cnt);

        draw_card = deck[draw_card_num];

        return true;    //드로우에 성공하면 true
    }

    //밖에서 실행시켜서 랜덤 드로우된 카드 데이터 반환
    public Card draw_event()
    {
        //카드 한장 랜덤으로 뽑고
        bool draw_ok = random_draw(user_deck_arr);

        if (draw_ok)
        {
            //드로우 카운트 줄여주고
            draw_cnt--;

            //뽑은 카드 데이터를 반환
            return draw_card;
        }

        else
        {
            Debug.Log("드로우 실패 - Deck클래스 확인");
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
        Debug.Log("유저 드로우 가능 카드 수 ui 생성");
    }

    #endregion
}
