using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    #region 카드 데이터 관련 구조체

    struct card_info
    {
        int card_idx;
        card_category category; //종류
        card_property property; //속성
        string card_name;   //이름
        string card_explain;    //설명

        card_effect[] effect; //효과
    }

    struct card_effect //카드 효과
    {
        card_effect_param effect_param;     //효과 계열
        int effect_param_idx;       //효과 번호
    }

    struct damage_effect_param //데미지 계열 구조
    {
        int effect_param_idx;
        damage_target effect_target;  //데미지 타겟
        int effect_value;      //데미지 양
    }

    struct remove_effect_param  //효과 제거 계열 구조
    {
        int effect_param_idx;
        remove_effect_target effect_target; //무효화 타겟
    }

    #endregion

    #region 카드 데이터 관련 열거형

    enum card_category  //카드 종류
    {
        attack = 1, //공격
        defense = 2, //방어
        trap = 3, //함정
    }

    enum card_property  //카드 속성
    {
        equipment = 1, //장치
        tool = 2,  //도구
        action = 3, //돌발
    }

    enum card_effect_param  //효과 계열
    {
        damage = 1,
        remove = 2
    }

    enum damage_target
    {
        hp = 1,
        deck = 2,
    }

    enum remove_effect_target
    {
        equipment = 1, //장치
        tool = 2,  //도구
        action = 3, //돌발
    }

    #endregion

    private void Awake()
    {
        //카드 효과 정리해서 구조체 만들어서 넣기

    }

}
