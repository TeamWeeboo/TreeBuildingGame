using Gameplay.Simulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

namespace Gameplay.Progression {

	public class SandstormController:MonoBehaviour {

	public static SandstormController instance;

		[field: SerializeField] public float[] difficultyList { get; private set; }
		[SerializeField] int[] rewardSuccessList;
		[SerializeField] int[] rewardFailList;
		[Tooltip("两个沙尘暴之间的间隔")]
		[SerializeField] float sandstormInterval;
		[Tooltip("每个沙尘暴的持续时间")]
		[SerializeField] float sandstormDuration;

		[SerializeField] GameObject[] sandstormVolumes;
		[SerializeField] GameObject[] sandstormCGs;
		[SerializeField] GameObject[] successCGs;
		[SerializeField] GameObject[] failCGs;

		[SerializeField] GameObject win;
		[SerializeField] GameObject lose;

		int cgStage;
		int failStreak;
		int winStreak;

		public int currentDifficulty { get; private set; }
		float timeAfterSandstorm;
		public int satisfiedDifficulty { get; private set; }
		public bool satisfiyCurrentDifficulty { get; private set; }

		

		private void Start() {
		instance = this;
			win.SetActive(false);
			lose.SetActive(false);
		}

		private void Update() {
			CountDown();
			timeAfterSandstorm+=Time.deltaTime;
			satisfiedDifficulty=-1;
			for(int i = 0;i<difficultyList.Length;i++) {
				if(TreeGrowth.totalCoverage>difficultyList[i]) satisfiedDifficulty=i;
			}
			satisfiyCurrentDifficulty=satisfiedDifficulty>=currentDifficulty;

			foreach(var i in sandstormVolumes) i.SetActive(false);
			foreach(var i in sandstormCGs) i.SetActive(false);
			foreach(var i in successCGs) i.SetActive(false);
			foreach(var i in failCGs) i.SetActive(false);

			if(timeAfterSandstorm>sandstormInterval+sandstormDuration) {
				UpdateCGMode();
			} else if(timeAfterSandstorm>sandstormInterval) {
				sandstormVolumes[satisfiedDifficulty+1].SetActive(true);
			} else {
			}
		}

		bool mouseDownPrevious;
		private void UpdateCGMode() {
			if(cgStage==-1) cgStage=0;
			if(!mouseDownPrevious&&Input.GetMouseButton(0)) {
				cgStage++;

				if(satisfiedDifficulty>=difficultyList.Length-1) {
					winStreak++;
					if(winStreak>=2) win.SetActive(true);
				} else winStreak=0;

			}
			mouseDownPrevious=Input.GetMouseButton(0);

			switch(cgStage) {
			case 0:
				Time.timeScale=0;
				sandstormCGs[satisfiedDifficulty+1].SetActive(true);
				break;
			case 1:
				Time.timeScale=0;
				if(satisfiyCurrentDifficulty) successCGs[currentDifficulty].SetActive(true);
				else failCGs[currentDifficulty].SetActive(true);
				break;
			case 2:
				Time.timeScale=1;
				timeAfterSandstorm=0;
				if(satisfiyCurrentDifficulty) {
					Game.instance.money+=rewardSuccessList[currentDifficulty];
					currentDifficulty++;
					failStreak=0;
				} else {
					Game.instance.money+=rewardFailList[currentDifficulty];
					failStreak++;
				}
				if(currentDifficulty>=difficultyList.Length) currentDifficulty=difficultyList.Length-1;
				if(failStreak>=3) lose.SetActive(true);

				cgStage=-1;
				break;
			}

		}


		//我改的
		public TextMeshProUGUI Text;
		private float timer;
		void CountDown(){
			timer = sandstormInterval - timeAfterSandstorm;
			if(timer > 0){
					Text.text = $"沙尘暴倒计时\nSandstorm Countdown\n{timer.ToString("F0")} s";
			}else 	Text.text = "沙尘暴突击中\nSandstorm is coming!";
		}
		
	}

}