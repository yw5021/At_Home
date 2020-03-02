using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    #region 카드 데이터 관련 구조체

    struct card_info
    {
        card_category category; //종류
        card_property property; //속성
        string card_name;   //이름
        string card_explain;    //설명

        card_effect[] effect; //효과

        public int[] return_effect_idx_arr()
        {
            int[] idx_arr = new int[effect.Length];

            for(int i=0; i < effect.Length; i++)
            {
                idx_arr[i] = effect[i].return_effect_idx();
            }

            return idx_arr;
        }
    }

    struct card_effect //카드 효과
    {
        card_effect_param effect_param;     //효과 계열
        int effect_idx;       //효과 번호

        public int return_effect_idx()
        {
            return effect_idx;
        }
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

    #region 카드 데이터
    int card_idx;
    card_info info;

    #endregion

    #region 카드 데이터 함수
    void card_init()
    {
        //매니저쪽에 인덱스 값 별로 카드 구조를 설정 그것을 가져오는걸로

    }
    #endregion

    #region 카드 사용 함수

    void active_card(Card select_card)
    {
        //인덱스 값을 활용해서 효과 발동
        int[] card_effect_idx = select_card.info.return_effect_idx_arr();

        for(int i = 0; i < card_effect_idx.Length; i++)
        {
            int effect_idx = card_effect_idx[i];

            //card_effect 클래스를 따로 만들어서 그 클래스를 통해 효과 발동시키는 구조로 변경
            Debug.Log(effect_idx + "번 효과 발동");
        }
    }

    #endregion
}
