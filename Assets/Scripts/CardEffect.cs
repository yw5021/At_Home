using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour {

    Queue<Card> active_card_queue = new Queue<Card>();

    bool ignore_equipment = false;    //장치
    bool ignore_tool = false;      //도구
    bool ignore_action = false;  //돌발

    int player_damage = 0;
    int player_heal = 0;
    int deck_damage = 0;
    int forced_move_turn = 0;
    int ban_move_turn = 0;
    int ban_use_card_turn = 0;

    #region 카드 사용 함수
    void active_card_enqueue(Card card)
    {
        Debug.Log(card.name + "가 추가됨");
        active_card_queue.Enqueue(card);
    }

    void active_card()
    {
        int now_cnt = active_card_queue.Count;

        for (int i = 0; i < now_cnt; i++)
        {
            Debug.Log(active_card_queue.Count + "장 있음");

            Card card = active_card_queue.Dequeue();

            card_info info = card.return_card_info();

            if (effect_ban_check(info))
            {
                continue;
            }

            //인덱스 값을 활용해서 효과 발동
            int[] card_effect_idx_arr = info.return_effect_idx_arr();

            for (int j = 0; j < card_effect_idx_arr.Length; j++)
            {
                int effect_idx = card_effect_idx_arr[j];

                Debug.Log(j + "번째 효과 발동 : " + effect_idx + "번 효과");

                active_card_effect(effect_idx);
            }
        }

        result_card_effect();

        GameManager.gameManager.SendMessage("next_phase");
    }

    #endregion

    bool effect_ban_check(card_info info)
    {
        //효과 무시 체크
        card_category category = info.return_card_category();

        if (category == card_category.trap)
        {
            card_property property = info.return_card_property();

            switch (property)
            {
                case card_property.action:

                    if (ignore_action)
                    {
                        return true;
                    }
                    break;

                case card_property.equipment:

                    if (ignore_equipment)
                    {
                        return true;
                    }
                    break;

                case card_property.tool:

                    if (ignore_tool)
                    {
                        return true;
                    }
                    break;
            }
        }

        return false;
    }

    #region 카드 효과 적용 함수
    //어떤 카드효과가 들어왔는지 확인하는 부분
    void active_card_effect(int effect_idx)
    {
        switch (effect_idx)
        {
            case 0:
                Debug.Log("0번 효과 발동 - 이 효과는 아무 효과도 없지만 두번 발동합니다");
                break;

            //1~ : 플레이어 데미지 계열
            
            //1뎀
            case 1:
                player_damage += 1;
                break;
            
            //2뎀
            case 2:
                player_damage += 2;
                break;

            //11~ : 플레이어 회복 계열

            //1힐
            case 11:
                player_heal += 1;
                break;

            //2힐
            case 12:
                player_heal += 2;
                break;

            //21~ : 덱 데미지 계열

            //1뎀
            case 21:
                deck_damage += 1;
                break;

            //2뎀
            case 22:
                deck_damage += 2;
                break;

            //31~ : ~~불가 계열

            //이동불가 1턴
            case 31:
                ban_move_turn += 1;
                break;

            //이동불가 2턴
            case 32:
                ban_move_turn += 2;
                break;

            //카드사용불가 1턴
            case 33:
                ban_use_card_turn += 1;
                break;
            
            //카드사용불가 2턴
            case 34:
                ban_use_card_turn += 2;
                break;

            //41~ : 강제이동, 무시 계열

            //강제이동 1턴전
            case 41:
                forced_move_turn += 1;
                break;

            //강제이동 2턴전
            case 42:
                forced_move_turn += 2;
                break;
            
            //돌발 무시
            case 43:
                ignore_action = true;
                break;

            //장치 무시
            case 44:
                ignore_equipment = true;
                break;

            //도구 무시
            case 45:
                ignore_tool = true;
                break;


            default:
                Debug.Log("error 92847 - 잘못된 효과 번호 입니다.");
                break;
        }
    }

    //실제 효과들이 적용되는 부분
    void result_card_effect()
    {
        if(player_damage > player_heal)
        {
            int calc_damage = player_damage - player_heal;

            GameManager.gameManager.SendMessage("message_player_damage", calc_damage);
        }
        else if(player_heal > player_damage)
        {
            int calc_heal = player_heal - player_damage;

            GameManager.gameManager.SendMessage("message_player_heal", calc_heal);
        }

        if(deck_damage > 0)
        {
            GameManager.gameManager.SendMessage("message_deck_damage", deck_damage);
        }

        if(forced_move_turn > 0)
        {
            GameManager.gameManager.SendMessage("message_forced_move", forced_move_turn);
        }

        if(ban_move_turn > 0)
        {
            GameManager.gameManager.SendMessage("message_ban_move", ban_move_turn);
        }

        if(ban_use_card_turn > 0)
        {
            GameManager.gameManager.SendMessage("message_ban_use_card", ban_use_card_turn);
        }
    }
    #endregion
}
