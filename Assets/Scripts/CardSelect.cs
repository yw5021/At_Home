using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour {

    List<Card> card_list;

    public Button confirm_but;

    int[] select_card_num_arr;
    int select_card_cnt;

    int now_select_card_cnt = 0;

    void card_image_insert()
    {
        Debug.Log("실행 되었다");
    }
    
    public void test()
    {
        but_func_insert(card_image_insert);
    }

    void delete_listener()
    {
        confirm_but.onClick.RemoveAllListeners();
    }

    public void but_func_insert(System.Action func)
    {
        //버튼에 받은 함수포인터로 클릭 이벤트 지정
        confirm_but.onClick.AddListener(() => func());
        confirm_but.onClick.AddListener(() => delete_listener());
    }

    void card_select_start(int card_cnt)
    {
        //뽑을 카드에 대한 정보 ~~~
        select_card_cnt = card_cnt;

        select_card_num_arr = new int[select_card_cnt];
    }

    public void select_card(int select_num)
    {
        //ui 쪽은 바깥에서 건드리는걸로
        if(now_select_card_cnt > select_card_cnt)
        {
            Debug.Log("error 72157 - 더 이상 선택할 수 없음");
            return;
        }

        select_card_num_arr[now_select_card_cnt] = select_num;

        now_select_card_cnt++;
    }
    
    public void deselect_card(int select_num)
    {
        if(now_select_card_cnt <= 0)
        {
            Debug.Log("error 98321 - 취소할 대상이 없음");
            return;
        }

        select_card_num_arr[now_select_card_cnt] = -1;

        now_select_card_cnt--;
    }

    public int[] return_select_card_num_arr()
    {
        int[] temp_arr = select_card_num_arr;

        select_card_num_arr = new int[0];

        return temp_arr;
    }
}
