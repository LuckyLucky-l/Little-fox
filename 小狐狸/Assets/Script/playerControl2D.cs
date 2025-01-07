using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XLua;

public class LuaTrigger2D{
    public LuaFunction OnTriggerEnter;
    public LuaFunction OnTriggerStay;
    public LuaFunction OnTriggerExit;
    public LuaFunction PickupItem;
}
public class playerControl2D : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;//Ser方便Bug在unity显示组件
    private Animator anim;

    public Collider2D coll;
    public Collider2D DisColl;
    public Transform CellingCheck, GroundCheck;
    //public AudioSource jumpAudio, hurtAudio, cherryAudio;

    public float speed;
    public float JumpForce;
    public LayerMask ground;

    //public Transform groundCheck;

    public int Cherry;
    private bool isHurt, isJump;//默认false
    private bool isGround;
    private int extraJump;

    bool jumpPressed;//后续加强跳跃手感//

    public Text CherryNum;
    LuaTrigger2D lua2D;

    // Start is called before the first frame update
   void Awake(){
        LuaMgr.GetInstance().Init();
        LuaMgr.GetInstance().DoLuaFile("main");
   }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lua2D=LuaMgr.GetInstance().Global.Get<LuaTrigger2D>("TriggerDetector");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, ground);
        newJump();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && extraJump > 0)
        {
            jumpPressed = true;
        }
        //Jump();
        Crouch();
        CherryInfo C=CherryNumberMgr.Instance.LoadCherry();
        this.Cherry=C.cherry;
        CherryNum.text = this.Cherry.ToString();
        //newJump();
    }

    void Movement()//移动
    {
        /*float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");//返回-1，0，1的值
        //角色移动
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }*/
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        anim.SetFloat("running", Mathf.Abs(horizontalMove));
        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);

        }
        
    }
    //切换动画效果
    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        else if (!coll.IsTouchingLayers(ground) && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }

        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }


        else if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
        
    }
    //碰撞触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集物品
        if (collision.tag == "collection")
        {
            collision.gameObject.GetComponent<Collider2D>().enabled=false;
            lua2D.OnTriggerEnter.Call(collision);
            //collision.tag = "null";
            //cherryAudio.Play();
            SoundMananger.instance.CherryAudio();
            //Destroy(collision.gameObject);
            //Cherry += 1;
            collision.GetComponent<Animator>().Play("isGot");
            //CherryNum.text = Cherry.ToString();
        }
        if (collision.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 1f);//停止1s重新加载当前场景
        }

    }
    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //collision.gameObject.tag = "null";
            //创建父级的实体
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {

                //Destroy(collision.gameObject);
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, JumpForce * 40 * Time.fixedDeltaTime);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                //hurtAudio.Play();
                SoundMananger.instance.HurtAudio();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                //hurtAudio.Play();
                SoundMananger.instance.HurtAudio();
                isHurt = true;
            }
        }
    }

    //蹲下
    void Crouch()
    {
        if (!Physics2D.OverlapCircle(CellingCheck.position,0.2f,ground))//角色在点celling圆范围0.2f头上没有ground开使执行
        {

            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                DisColl.enabled = false;
            }
            else
            {
                anim.SetBool("crouching", false);
                DisColl.enabled = true;
            }
        }
    }

    /*//角色跳跃
    void Jump()
    {
        if (Input.GetButton("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce * Time.deltaTime);
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }

    }*/

    //新的角色跳跃
    void newJump()
    {
        /*if (isGround)
        {
            extraJump = 2;
            isJump = false;
        }
        if (Input.GetButtonDown("Jump") && extraJump > 0)
        {
            rb.velocity = Vector2.up * JumpForce;//new Vector2(0,1)
            extraJump--;
            SoundMananger.instance.JumpAudio();
            anim.SetBool("jumping", true);
        }
        if (Input.GetButtonDown("Jump") && extraJump == 0 && isGround)
        {
            rb.velocity = Vector2.up * JumpForce;
            SoundMananger.instance.JumpAudio();
            anim.SetBool("jumping", true);
        }*/

        if (isGround)
        {
            extraJump = 2;
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            extraJump--;
            jumpPressed = false;
            SoundMananger.instance.JumpAudio();
            anim.SetBool("jumping", true);
        }
        else if (jumpPressed && extraJump > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            extraJump--;
            jumpPressed = false;
            SoundMananger.instance.JumpAudio();
            anim.SetBool("jumping", true);
        }

    }


    //重置场景
    void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void CherryCount()
    {
        Cherry += 1;
    }

}
