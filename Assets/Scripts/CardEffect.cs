using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour {

    List<Card> active_card_list = new List<Card>();

    #region 카드 사용 함수
    void active_card_list_add(Card card)
    {
        active_card_list.Add(card);
    }

    void active_card()
    {
        for (int i = 0; i < active_card_list.Count; i++)
        {
            Card card = active_card_list[i];

            card_info info = card.return_card_info();

            //인덱스 값을 활용해서 효과 발동
            int[] card_effect_idx_arr = info.return_effect_idx_arr();

            for (int j = 0; j < card_effect_idx_arr.Length; j++)
            {
                int effect_idx = card_effect_idx_arr[j];

                //card_effect 클래스를 따로 만들어서 그 클래스를 통해 효과 발동시키는 구조로 변경
                Debug.Log(j + "번째 효과 발동");

                active_card_effect(effect_idx);
            }
        }

        calc_card_effect();
        result_card_effect();

        
        GameManager.gameManager.SendMessage("next_phase");
    }

    #endregion

    #region 카드 효과 적용 함수
    //어떤 카드효과가 들어왔는지 확인하는 부분
    void active_card_effect(int effect_idx)
    {
        switch (effect_idx)
        {
            case 0:
                Debug.Log("0번 효과 발동 - 이 효과는 아무 효과도 없지만 두번 발동합니다");
                break;

            default:
                Debug.Log("error 92847 - 잘못된 효과 번호 입니다.");
                break;
        }
    }

    //들어온 카드 효과들을 계산하는 부분
    void calc_card_effect()
    {

    }

    //실제 효과들이 적용되는 부분
    void result_card_effect()
    {

    }
    #endregion
}
