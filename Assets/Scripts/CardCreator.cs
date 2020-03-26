using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreator : MonoBehaviour {

    public GameObject Card_Prefab;

    public Card Card_Create(int idx)
    {
        //카드 오브젝트 생성해서
        GameObject Go_Draw_Card = Instantiate(Card_Prefab);

        Card Draw_Card = Go_Draw_Card.GetComponent<Card>();

        //카드쪽에서 init해주는 부분 가져올 것
        Draw_Card._card_idx = idx;

        Draw_Card._info = card_info_init(idx);

        Draw_Card.SendMessage("card_init");

        return Draw_Card;
    }

    card_info card_info_init(int card_idx)
    {
        Debug.Log(card_idx + "번 카드가 생성됨");

        card_category category = card_category.rush;
        card_property property = card_property.action;
        string card_name = "";
        string card_explain = "";

        int[] effect_idx_arr = { };

        switch (card_idx)
        {
            case 0:
                card_name = "테스트용 0번카드";
                card_explain = "이 카드는 제작자가 처음 만들었습니다 / 아무런 효과도 없는 효과가 2번 발동합니다.";
                category = card_category.rush;
                property = card_property.action;

                effect_idx_arr = new int[2];
                effect_idx_arr[0] = 0;
                effect_idx_arr[1] = 0;
                break;

            case 1:
                card_name = "인류가 만든 최악의 지뢰";
                card_explain = "";
                category = card_category.trap;
                property = card_property.equipment;

                effect_idx_arr = new int[3];
                effect_idx_arr[0] = 41;
                effect_idx_arr[1] = 1;
                effect_idx_arr[2] = 31;
                break;

            case 2:
                card_name = "총총걸음";
                card_explain = "";
                category = card_category.counter;
                property = card_property.tool;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 44;
                break;

            case 3:
                card_name = "매우 어려운 수학책";
                card_explain = "";
                category = card_category.trap;
                property = card_property.tool;

                effect_idx_arr = new int[2];
                effect_idx_arr[0] = 31;
                effect_idx_arr[1] = 2;
                break;

            case 4:
                card_name = "지구의 주인은 고양이다!";
                card_explain = "";
                category = card_category.trap;
                property = card_property.action;

                effect_idx_arr = new int[2];
                effect_idx_arr[0] = 2;
                effect_idx_arr[1] = 21;
                break;

            case 5:
                card_name = "고양이와 화합";
                card_explain = "";
                category = card_category.rush;
                property = card_property.action;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 12;
                break;

            case 6:
                card_name = "특공대 드라마";
                card_explain = "";
                category = card_category.trap;
                property = card_property.equipment;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 42;
                break;

            case 7:
                card_name = "명석한 두뇌";
                card_explain = "";
                category = card_category.counter;
                property = card_property.action;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 45;
                break;

            case 8:
                card_name = "맛있는 초콜릿 파운드 케이크";
                card_explain = "";
                category = card_category.trap;
                property = card_property.equipment;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 31;
                break;

            case 9:
                card_name = "건포도도 나쁘지 않아";
                card_explain = "";
                category = card_category.counter;
                property = card_property.tool;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 44;
                break;

            case 10:
                card_name = "퍽퍽한 빵";
                card_explain = "";
                category = card_category.trap;
                property = card_property.action;

                effect_idx_arr = new int[2];
                effect_idx_arr[0] = 31;
                effect_idx_arr[1] = 1;
                break;

            case 11:
                card_name = "테슬롸봇";
                card_explain = "";
                category = card_category.trap;
                property = card_property.equipment;

                effect_idx_arr = new int[2];
                effect_idx_arr[0] = 32;
                effect_idx_arr[1] = 21;
                break;

            default:
                Debug.Log("error 53987 - 잘못된 인덱스 값");
                break;
        }
        card_info info = new card_info(category, property, card_name, card_explain, effect_idx_arr);

        return info;
    }
    
    #region 카드 정보값 처리 함수
    /*
    void card_info_init()
    {
        Debug.Log(card_idx + "번 카드가 생성됨");

        card_category category = card_category.rush;
        card_property property = card_property.action;
        string card_name = "";
        string card_explain = "";

        int[] effect_idx_arr = { };

        switch (card_idx)
        {
            case 0:
                card_name = "테스트용 0번카드";
                card_explain = "이 카드는 제작자가 처음 만들었습니다 / 아무런 효과도 없는 효과가 2번 발동합니다.";
                category = card_category.rush;
                property = card_property.action;

                effect_idx_arr = new int[2];
                effect_idx_arr[0] = 0;
                effect_idx_arr[1] = 0;
                break;

            case 1:
                card_name = "인류가 만든 최악의 지뢰";
                card_explain = "";
                category = card_category.trap;
                property = card_property.equipment;

                effect_idx_arr = new int[3];
                effect_idx_arr[0] = 41;
                effect_idx_arr[1] = 1;
                effect_idx_arr[2] = 31;
                break;

            case 2:
                card_name = "총총걸음";
                card_explain = "";
                category = card_category.counter;
                property = card_property.tool;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 44;
                break;

            case 3:
                card_name = "매우 어려운 수학책";
                card_explain = "";
                category = card_category.trap;
                property = card_property.tool;

                effect_idx_arr = new int[2];
                effect_idx_arr[0] = 31;
                effect_idx_arr[1] = 2;
                break;

            case 4:
                card_name = "지구의 주인은 고양이다!";
                card_explain = "";
                category = card_category.trap;
                property = card_property.action;

                effect_idx_arr = new int[2];
                effect_idx_arr[0] = 2;
                effect_idx_arr[1] = 21;
                break;

            case 5:
                card_name = "고양이와 화합";
                card_explain = "";
                category = card_category.rush;
                property = card_property.action;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 12;
                break;

            case 6:
                card_name = "특공대 드라마";
                card_explain = "";
                category = card_category.trap;
                property = card_property.equipment;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 42;
                break;

            case 7:
                card_name = "명석한 두뇌";
                card_explain = "";
                category = card_category.counter;
                property = card_property.action;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 45;
                break;

            case 8:
                card_name = "맛있는 초콜릿 파운드 케이크";
                card_explain = "";
                category = card_category.trap;
                property = card_property.equipment;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 31;
                break;

            case 9:
                card_name = "건포도도 나쁘지 않아";
                card_explain = "";
                category = card_category.counter;
                property = card_property.tool;

                effect_idx_arr = new int[1];
                effect_idx_arr[0] = 44;
                break;

            case 10:
                card_name = "퍽퍽한 빵";
                card_explain = "";
                category = card_category.trap;
                property = card_property.action;

                effect_idx_arr = new int[2];
                effect_idx_arr[0] = 31;
                effect_idx_arr[1] = 1;
                break;

            default:
                Debug.Log("error 53987 - 잘못된 인덱스 값");
                break;
        }
        info = new card_info(category, property, card_name, card_explain, effect_idx_arr);
    }
    */
    #endregion
    
}
