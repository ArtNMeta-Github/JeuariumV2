using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimationHashes;

public class PatrolNPC : NPCBase
{
    public Transform[] patrolPoints;
    public Animator animator;

    protected State currState;
    public Transform player;

    public PatrolState patrolState;
    public IdleState idleState;
    public GreetingPlayerState greetingState;

    public bool inGreeting = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        patrolState = new PatrolState(this);
        idleState = new IdleState(this);
        greetingState = new GreetingPlayerState(this);

        SwtichState(patrolState);
    }
    public void SwtichState(State state)
    {
        currState?.Exit();
        currState = state;
        currState?.Enter();
    }
    public override void GreetingPlayer(Transform player)
    {
        if (inGreeting)
            return;

        this.player = player;
        SwtichState(greetingState);
    }

    private void Update()
    {
        currState?.Update();
    }
}

public class PatrolState : Temp
{
    public PatrolState(PatrolNPC npc) : base(npc)   
    {
        transform = npc.transform;
    }
    Vector3 currDest;

    int currDestIdx = -1;

    Transform transform;

    public override void Enter()
    {
        SetDest();
        patrolNPC.animator.SetTrigger(hashWalking);

        transform.LookAt(currDest);
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        Vector3 dir = currDest - transform.position;
        dir.Normalize();
        transform.Translate(0.8f * Time.deltaTime * dir, Space.World);

        //FaceDirection(dir);

        if (Funcs.DistanceIgnoreY(currDest,transform.position) < 0.01f)
        {
            patrolNPC.SwtichState(patrolNPC.idleState);
        }
    }

    private void FaceDirection(Vector3 dir)
    {
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(dir),
            Time.deltaTime * 0.1f);
    }
    void SetDest()
    {
        int length = patrolNPC.patrolPoints.Length;
        currDestIdx = ++currDestIdx % length;

        currDest = patrolNPC.patrolPoints[currDestIdx].position;
    }
}

public class IdleState : Temp
{
    public IdleState(PatrolNPC npc) : base(npc) { }

    float idleStartTime;

    public override void Enter()
    {
        idleStartTime = Time.time;
        patrolNPC.animator.SetTrigger(hashIdle);
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        if(Time.time - idleStartTime > 3f)
        {
            patrolNPC.SwtichState(patrolNPC.patrolState);
        }
    }
}

public class GreetingPlayerState : Temp
{
    public GreetingPlayerState(PatrolNPC npc) : base(npc) { }
    Transform player;
    Transform transform;

    float timer;

    public override void Enter()
    {
        patrolNPC.inGreeting = true;

        player = patrolNPC.player;
        transform = patrolNPC.transform;

        timer = 0f;

        Vector3 dir = player.position - transform.position;
        Vector3 crossProduct = Vector3.Cross(transform.forward, dir);
        float dotProduct = Vector3.Dot(crossProduct, Vector3.up);
        patrolNPC.animator.SetTrigger(dotProduct > 0 ? hashRight : hashLeft);
    }

    public override void Exit()
    {
        patrolNPC.inGreeting = false;
    }

    public override void Update()
    {     
        Vector3 dir = player.position - transform.position;

        if (Vector3.Angle(transform.forward, dir) >= 5f)
        {
            dir = player.position - transform.position;
            dir.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
        }
        else
        {
            if(timer == 0f)
                patrolNPC.animator.SetTrigger(hashGreeting);

            timer += Time.deltaTime;
        }

        if (timer > 5f)
            patrolNPC.SwtichState(patrolNPC.idleState);
    }
}

public abstract class Temp : State
{
    public Temp(PatrolNPC npc) => patrolNPC = npc;

    protected PatrolNPC patrolNPC;
}