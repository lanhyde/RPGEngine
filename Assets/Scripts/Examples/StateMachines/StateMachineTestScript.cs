using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineTestScript : MonoBehaviour {
    private Miner miner;
    private MinerWife wife;

    public float waitTime = 1f;
    private float timer = 0f;
	// Use this for initialization
	void Start () {
        GameObject minerGameObj = new GameObject();
        minerGameObj.name = "Miner_Bob";
        miner = minerGameObj.AddComponent<Miner>();
        GameObject wifeGameObj = new GameObject();
        wifeGameObj.name = "Wife_Elsa";
        wife = wifeGameObj.AddComponent<MinerWife>();

        EntityManager.Instance.RegisterEntity(miner);
        EntityManager.Instance.RegisterEntity(wife);
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            timer = 0f;
            miner.Refresh(Time.deltaTime);
            wife.Refresh(Time.deltaTime);

            MessageDispatcher.Instance.DispatchDelayedMessage();
        }
	}
}
