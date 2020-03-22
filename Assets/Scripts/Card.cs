using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 카드 데이터 관련 구조체

public struct card_info
{
    card_category category; //종류
    card_property property; //속성
    string card_name;   //이름
    string card_explain;    //설명

    int[] effect_idx_arr; //효과

    public card_info(card_category _category, card_property _property, string _name, string _explain, int[] _effect_idx_arr)
    {
        category = _category;
        property = _property;
        card_name = _name;
        card_explain = _explain;
        effect_idx_arr = _effect_idx_arr;
    }

    public string return_card_name()
    {
        return card_name;
    }

    public string return_card_explain()
    {
        return card_explain;
    }

    public card_category return_card_category()
    {
        return category;
    }

    public card_property return_card_property()
    {
        return property;
    }

    public int[] return_effect_idx_arr()
    {
        return effect_idx_arr;
    }
}

#endregion

#region 카드 데이터 관련 열거형

public enum card_category  //카드 종류
{
    attack = 1, //공격
    defense = 2, //방어
    trap = 3, //함정
}

public enum card_property  //카드 속성
{
    equipment = 1, //장치
    tool = 2,  //도구
    action = 3, //돌발
}

#endregion

public class Card : MonoBehaviour {

    #region 카드 데이터
    int card_idx;
    public int _card_idx;

    card_info info;

    #endregion

    #region 카드 데이터 함수
    void card_init()
    { 
        card_idx = _card_idx;

        card_info_init();
    }
    #endregion

    #region 카드 정보값 처리 함수

    void card_info_init()
    {
        Debug.Log(card_idx + "번 카드가 생성됨");

        card_category category = card_category.attack;
        card_property property = card_property.action;
        string card_name = "";
        string card_explain = "";

        int[] effect_idx_arr = { };

        switch (card_idx)
        {
            case 0:
                card_name = "테스트용 0번카드";
                card_explain = "이 카드는 제작자가 처음 만들었습니다";
                category = card_category.attack;
                property = card_property.action;

                effect_idx_arr = new int[2];
                effect_idx_arr[0] = 0;
                effect_idx_arr[1] = 0;
                break;

            default:
                Debug.Log("error 53987 - 잘못된 인덱스 값");
                break;
        }
        info = new card_info(category,property,card_name,card_explain,effect_idx_arr);
    }

    #endregion

    void use_card()
    {
        CardEffect cardEffect = GameObject.FindGameObjectWithTag("CardEffect").GetComponent<CardEffect>();

        Card card = gameObject.GetComponent<Card>();

        cardEffect.SendMessage("active_card_enqueue", card);
    }

    public card_info return_card_info()
    {
        return info;
    }
}
