using UnityEngine;
using System.Collections;
using Frame;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ModuleManager.Instance.OpenMoudule<PanelMainModule, PanelMainController, PanelMainData>("PanelMain");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
