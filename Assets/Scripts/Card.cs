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

        card_effect[] effect_arr; //효과

        public card_info(card_category _category, card_property _property,string _name,string _explain,card_effect[] _effect_arr)
        {
            category = _category;
            property = _property;
            card_name = _name;
            card_explain = _explain;
            effect_arr = _effect_arr;
        }

        public card_effect[] return_effect_arr()
        {
            return effect_arr;
        }
    }

    struct card_effect //카드 효과
    {
        card_effect_param effect_param;     //효과 계열
        int effect_idx;       //효과 번호

        public card_effect(card_effect_param _effect_param, int _effect_idx)
        {
            effect_param = _effect_param;
            effect_idx = _effect_idx;
        }

        public card_effect_param return_effect_param()
        {
            return effect_param;
        }

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

    #region 카드 사용 함수

    void active_card()
    {
        //인덱스 값을 활용해서 효과 발동
        card_effect[] card_effect_arr = info.return_effect_arr();

        for(int i = 0; i < card_effect_arr.Length; i++)
        {
            card_effect_param effect_param = card_effect_arr[i].return_effect_param();
            int effect_idx = card_effect_arr[i].return_effect_idx();

            //card_effect 클래스를 따로 만들어서 그 클래스를 통해 효과 발동시키는 구조로 변경
            Debug.Log(i + "번째 효과 발동");

            active_card_effect(effect_param,effect_idx);
        }
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

        card_effect[] effect_arr = { };

        switch (card_idx)
        {
            case 0:
                card_name = "테스트용 0번카드";
                card_explain = "이 카드는 제작자가 처음 만들었습니다";
                category = card_category.attack;
                property = card_property.action;

                effect_arr = new card_effect[2];
                effect_arr[0] = new card_effect(card_effect_param.damage, 0);
                effect_arr[1] = new card_effect(card_effect_param.remove, 0);
                break;

            default:
                Debug.Log("error 53987 - 잘못된 인덱스 값");
                break;
        }

        //card_effect[] ef = { new card_effect(), new card_effect() };

        info = new card_info(category,property,card_name,card_explain,effect_arr);
    }

    #endregion

    #region 카드 효과 적용 함수
    void active_card_effect(card_effect_param effect_param, int effect_idx)
    {
        switch (effect_param)
        {
            case card_effect_param.damage:

                switch (effect_idx)
                {
                    case 0:
                        //0번 카드
                        Debug.Log("0번 데미지 효과 발동");
                        break;
                }

                break;

            case card_effect_param.remove:

                switch (effect_idx)
                {
                    case 0:
                        //0번 카드
                        Debug.Log("0번 제거 효과 발동");
                        break;
                }

                break;
        }
    }

    #endregion

    private void Awake()
    {
        //card_init();
    }
}
