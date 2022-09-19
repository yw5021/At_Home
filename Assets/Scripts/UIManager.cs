using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {

    Canvas canvas;
    GraphicRaycaster gr;
    PointerEventData ped;

    bool is_Drag = false;

    GameObject go_now_select_card;
    bool is_active_card;
    Vector3 test;
    Vector3 test2;

    public Animation anim;

    void card_enlarge()
    {
        //이미지 받아서 화면에 큰 이미지로 표시
        //Debug.Log("이미지 확대");
    }

    void card_user_interface()
    {

    }

    public static IEnumerator Animation_Wating(Animation animation, string name)
    {
        animation.Play(name);
        //animator.SetTrigger(trigger);
        while (animation.isPlaying)
        {
            //AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);
            //if (animInfo.normalizedTime >= 1.0f)
            //{
            //    break;
            //}
            //Debug.Log("진행중");
            yield return null;
        }

        Debug.Log("애니메이션 끝");

        yield return null;
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        gr = canvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);

        //StartCoroutine(Animation_Wating(anim, "New Animation"));
    }

    private void Update()
    {
        //card_user_interface();

        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);

        if (results.Count > 0)
        {
            if (results[0].gameObject.tag == "CardImage")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("카드 선택됨");

                    go_now_select_card = results[0].gameObject;
                    Image temp_image = go_now_select_card.GetComponent<Image>();
                    InHand hand = GameObject.FindGameObjectWithTag("InHand").GetComponent<InHand>();
                    is_active_card = hand.is_Active_Card(temp_image);

                    if (hand._is_active_inhand)
                    {
                        if (!is_active_card)
                        {
                            test = go_now_select_card.transform.position;
                            test2 = go_now_select_card.transform.rotation.eulerAngles;

                            is_Drag = true;
                        }
                        else
                        {
                            GameObject.FindGameObjectWithTag("InHand").GetComponent<InHand>().active_card_select_inHand(temp_image);
                        }
                    }
                    else
                    {
                        hand.card_num_check(temp_image);
                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (is_Drag)
            {
                go_now_select_card.GetComponent<RectTransform>().anchoredPosition = ped.position - new Vector2(Screen.width/2,Screen.height/2);

                go_now_select_card.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            if (is_Drag)
            {
                Debug.Log("카드 선택 끝");

                go_now_select_card.transform.position = test;
                go_now_select_card.transform.rotation = Quaternion.Euler(test2);

                is_Drag = false;

                //일단 임시로
                if (ped.position.y > 300)
                {
                    Image temp_image = go_now_select_card.GetComponent<Image>();

                    GameObject.FindGameObjectWithTag("InHand").GetComponent<InHand>().active_card_select_inHand(temp_image);
                }
                else
                {
                    go_now_select_card = results[0].gameObject;
                    Image temp_image = go_now_select_card.GetComponent<Image>();
                    InHand hand = GameObject.FindGameObjectWithTag("InHand").GetComponent<InHand>();

                    hand.card_num_check(temp_image);
                }
            }
        }
    }
}
