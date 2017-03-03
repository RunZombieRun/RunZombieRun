using System.Collections;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Player_Controller : MonoBehaviour {
    static public Player_Controller get;

	public enum PLAYERSTATE { Left, Right, Center, Slide, Jump}
    PLAYERSTATE state;
    PLAYERSTATE previusState;

    [Header("Необходимые компоненты")]
    [SerializeField]
    Rigidbody playerRigibody; 
    [SerializeField]
    Collider playerCollider;
    //без анимации будет работать
    Animation animationComp;
   
    Vector3 newPos;
    Vector3 previusPos;
    Vector3 leftLinePosition;
    Vector3 rightLinePosition;
    Vector3 centerLinePosition;
    bool isChangingLane = false;
    

    [Header("Анимация движений")]
    [TooltipAttribute("Анимация будет пригрываться, если подключена")]
    public AnimationClip moveLeftAnimation;
    public AnimationClip moveRightAnimation;
    public AnimationClip slideAnimation;
    public AnimationClip jumpAnimation;
    [SpaceAttribute(10)]

    [Header("Точки лайнов")]
    public Transform leftLine;
    public Transform rightLine;
    public Transform centerLine;
    [SpaceAttribute(10)]
    [Header("Переменные для настройки персонажа")]
    [TooltipAttribute("Скорость перемещения влево и вправо")]
    public float movingSpeed = 10f;
    
    [TooltipAttribute("Сила прыжка")]
    public float jumpForce = 5f;
    
    [TooltipAttribute("Длительность прыжка")]
    public float jumpTime = 1f;
 
    [Header("Минимальная дистанция для свайпа")]
	public float minSwipeDistY = 0.5f;
	public float minSwipeDistX = 0.5f;	
	//начальная позиция для свайпа
    private Vector2 startPos;
    //возможные типы свайпа
	enum SWIPETYPE {non, swipeUP, swipeDOWN, swipeLEFT, swipeRIGHT}
    //возможные сохраняем в статическую переменную с типом свайпа
	SWIPETYPE swipeType;

    void Awake()
    {
        get = this;
    }
    void Start()
    {
        //начальная установка состояний персонажа и скриптов
        CheckLinePosition(leftLine, rightLine, centerLine);
        state = PLAYERSTATE.Center;
        transform.position = newPos = centerLinePosition; 
        //кэшируем playerRigibody
        //настройки playerRigibody не меняем
        playerRigibody.useGravity = false;
        playerRigibody.angularDrag = 0f;
        playerCollider.isTrigger = false;
        //кэшируем анимацию, если есть
    	animationComp = GetComponent<Animation>();
        //ХЗ ДЛЯ ЧЕГО ЭТО
        Physics.gravity = new Vector3(0, -50, 0); 

    }

    void OnDisable()
    {
        //Restor gravity, все еще ХЗ
        Physics.gravity = new Vector3(0, -9.8f, 0);
    }
    // Update is called once per frame
    void Update()
    {
        CharacterController();
#if UNITY_ANDROID
        SwipeDetection(); //запускаем детектор в апдейте, если билд под андрои
     #endif
    }


    public void Left()
    {
        //из правого края в центр
        if (state == PLAYERSTATE.Right)
        {
            newPos = centerLinePosition;
            state = PLAYERSTATE.Center;
            //из центра в левый край
        }
        else if (state == PLAYERSTATE.Center)
        {
            newPos = leftLinePosition;
            state = PLAYERSTATE.Left;
        }
        //проигрываем анимацию
        if (moveLeftAnimation != null)
        {
            animationComp.Play(moveLeftAnimation.name);
        }
    }
  public  void Right()
    {
        //из левого края в центр
        if (state == PLAYERSTATE.Left)
        {
            newPos = centerLinePosition;
            state = PLAYERSTATE.Center;
            //из центра в правый край
        }
        else if (state == PLAYERSTATE.Center)
        {
            newPos = rightLinePosition;
            state = PLAYERSTATE.Right;
        }
        //анимация
        if (moveRightAnimation != null)
        {
            animationComp.Play(moveRightAnimation.name);
        }
    }
    public void Up()
    {
        //если нажата клавиша, прыгаем
        StartCoroutine(PlayerJump(jumpTime, jumpForce));
        //выключаем гравитацию после карутины
        playerRigibody.useGravity = false;
    }
    
    void CharacterController(){
        //перемещение влево при нажатии клавиши
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || swipeType == SWIPETYPE.swipeLEFT)
        {
            Left();
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || swipeType == SWIPETYPE.swipeRIGHT)
        {
            Right();
		}
		//если изменилась позиция и мы не перемещаем персонажа в данный момент
        if(transform.position != newPos && !isChangingLane) {
            //меняем маркер состояния перемещения
            isChangingLane = true;
            //перемещаем лерпом
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * movingSpeed);
            //меняем маркер состояния перемещения
            isChangingLane = false;
        }
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || swipeType == SWIPETYPE.swipeUP) && state != PLAYERSTATE.Jump){

            Up();
        }
        if((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftShift) || swipeType == SWIPETYPE.swipeDOWN) && state != PLAYERSTATE.Slide){
            //если нажата клавиша, двигаемся вперед;
            PlayerSlide();
        }
        
    }
    //проверка назначенных лайнов
    void CheckLinePosition(Transform leftLine, Transform rightLine, Transform centerLine){
        //если все точки лайнов указаны в редакторе
        if(leftLine && rightLine && centerLine != null){
            //поочереди берем позииции из каждой соостветствующей точки, сохраняем их в формате Vector3
            leftLinePosition = leftLine.position;
            rightLinePosition = rightLine.position;
            centerLinePosition = centerLine.position;
        } else {
            //вывоодим сообщение об ошибке
            Debug.LogError("НЕ НАЗНАЧЕНЫ ТОЧКИ ПЕРЕМЕЩЕНИЯ ПО ЛИНИЯМ");
        }
    }

    //метод прыжка, на вход идет время прыжка и сила прыжка
    IEnumerator PlayerJump(float jumpTime, float jumpForce)
    {
        //сохраняем предыдущую позицию
        previusState = state;
        //выставляем состояние прыжка
        state = PLAYERSTATE.Jump;

        //запускаем анимацию, если взможно
        if(jumpAnimation != null){
			GetComponentInChildren<Animator>().SetTrigger("Jump");
		}
        //запускаем цикл пока состяние прыжка активно
        while (state == PLAYERSTATE.Jump)
        {
            //прыгаем с силой прыжка вверх
            if(!playerRigibody.useGravity){
               playerRigibody.AddForce(new Vector2(0f, jumpForce));
            } else {
                Debug.LogError("Use Gravity включен");
            }
            //проверяем счетчик времени
            jumpTime -= Time.fixedDeltaTime;
            //если время прыжка истекло
            if (jumpTime  <= 0){
                //приземляемся засчет гравитации
                playerRigibody.useGravity = true;
                //меняем стейт
                state = previusState;
            }
            //ждем апдейт
            yield return new WaitForFixedUpdate();
        }
    }
    //метод слайда, на вход идет время скрость, дальность слайда
    //переделать
    void PlayerSlide(){
        previusState = state;
        state = PLAYERSTATE.Slide;
        if(slideAnimation != null){
				animationComp.Play(slideAnimation.name);
                state = previusState;
		} else {
            Debug.LogError("Отсутствует анимация слайда!");
        }
    }

    void SwipeDetection(){
    
        if (Input.touchCount > 0){
			Touch touch = Input.touches[0];
        
			switch (touch.phase) 	
			{	
			case TouchPhase.Began:
				startPos = touch.position;
                Debug.LogError("Свайп еще не определился!");
				break;
                
                
			case TouchPhase.Ended:
                int swipeNumber = 0;
				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                if (swipeDistVertical > minSwipeDistY) 	
				{
                	float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
                    if (swipeValue > 0)//верхний свайg
                    {
                        swipeNumber = 1;
                        SwipeTypeDetection(swipeType, swipeNumber);
                        //return swipeType = SWIPETYPE.swipeUP;
                    }	
					else if (swipeValue < 0)//нижний свайп
                    {
                        swipeNumber = 2;
                        SwipeTypeDetection(swipeType, swipeNumber);
                        //return swipeType = SWIPETYPE.swipeDOWN;
                    }
                    else 
                    {
                        Debug.LogError("Что то пошло не оперделение свайпа Вверх / Вниз!");
                    }   
				}
				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
                if (swipeDistHorizontal > minSwipeDistX) 
                {
                    float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
                    if (swipeValue > 0)//правый свайп
                    {
                        swipeNumber = 3;
                        SwipeTypeDetection(swipeType, swipeNumber);
                        //return swipeType = SWIPETYPE.swipeRIGHT;
                    }
                    
                    else if (swipeValue < 0)//левый свайп
                    {
                        swipeNumber = 4;
                        SwipeTypeDetection(swipeType, swipeNumber);
                        //return swipeType = SWIPETYPE.swipeLEFT;
                    } 
                    else 
                    {
                        Debug.LogError("Что то пошло не оперделение свайпа Лево / Право!");
                    }
               }
               break;
			}
		} else {
             Debug.LogError("Что то пошло не так в конце!");
        }
    }

    SWIPETYPE SwipeTypeDetection(SWIPETYPE swipeType, int swipeNumber){
        switch (swipeNumber)
        {
            case 1:
                return swipeType = SWIPETYPE.swipeUP;
                break;
            case 2:
                return swipeType = SWIPETYPE.swipeDOWN;
                break;
            case 3:
                return swipeType = SWIPETYPE.swipeRIGHT;
                break;
            case 4:
                return swipeType = SWIPETYPE.swipeLEFT;
                break;
            default:
                return swipeType = SWIPETYPE.non;
        }
    }
}
