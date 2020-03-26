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
    rush = 1, //속공
    counter = 2, //반격
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
    public card_info _info;

    #endregion

    #region 카드 데이터 함수
    void card_init()
    { 
        card_idx = _card_idx;
        info = _info;
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
