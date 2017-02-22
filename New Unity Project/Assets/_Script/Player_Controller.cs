using System.Collections;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Player_Controller : MonoBehaviour {
    static public Player_Controller get;

	public enum PLAYERSTATE { Left, Right, Center, Jump, Slide }
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
    
    [TooltipAttribute("Длительность слайда")]
    public float slideTime = 3f;
    
    [TooltipAttribute("Дальность слайда")]
    public float slideDistance = -5f;
    
    [TooltipAttribute("Скорость слайда")]
    public float slideSpeed = 3f;
     

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
    }

    void CharacterController(){
        //перемещение влево при нажатии клавиши
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            //из правого края в центр
			if (state == PLAYERSTATE.Right)
			{
				newPos = centerLinePosition;
				state = PLAYERSTATE.Center;
            //из центра в левый край
			} else if (state == PLAYERSTATE.Center) {
				newPos = leftLinePosition;
				state = PLAYERSTATE.Left;
			}
            //проигрываем анимацию
			if(moveLeftAnimation != null){
				animationComp.Play(moveLeftAnimation.name);
			}
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {	
            //из левого края в центр
			if (state == PLAYERSTATE.Left)
			{
				newPos = centerLinePosition;
				state = PLAYERSTATE.Center;
            //из центра в правый край
			} else if (state == PLAYERSTATE.Center) {
				newPos = rightLinePosition;
				state = PLAYERSTATE.Right;
			}
            //анимация
			if(moveRightAnimation != null){
				animationComp.Play(moveRightAnimation.name);
			}
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
        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && state != PLAYERSTATE.Jump){
            //если нажата клавиша, прыгаем
            StartCoroutine(PlayerJump(jumpTime, jumpForce));
            //выключаем гравитацию после карутины
            playerRigibody.useGravity = false;
            
        }
        if((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftShift)) && state != PLAYERSTATE.Slide){
            playerCollider.isTrigger = true;
            //если нажата клавиша, двигаемся вперед;
            PlayerSlide(slideDistance, slideSpeed);
            playerCollider.isTrigger = false;

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
    void PlayerSlide(float slideDistance, float slideSpeed){

        previusState = state;
        state = PLAYERSTATE.Slide;
        previusPos = transform.position;
        newPos = new Vector3(transform.position.x, transform.position.y, slideDistance);
        if(slideAnimation != null){
				animationComp.Play(slideAnimation.name);
		}
        if (newPos != previusPos && state == PLAYERSTATE.Slide){
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * slideSpeed);
            state = previusState;
        }
            
    }
}
