using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WoWCompanionApp
{
	public class DialogFactory : Singleton<DialogFactory>
	{
		private Canvas MainCanvas
		{
			get
			{
				if (this._mainCanvas == null)
				{
					GameObject gameObject = GameObject.FindGameObjectWithTag(DialogFactory.MainCanvasName);
					this._mainCanvas = gameObject.GetComponent<Canvas>();
				}
				return this._mainCanvas;
			}
		}

		private Canvas GameCanvas
		{
			get
			{
				if (this._gameCanvas == null)
				{
					GameObject gameObject = GameObject.FindGameObjectWithTag(DialogFactory.GameCanvasName);
					if (gameObject != null)
					{
						this._gameCanvas = gameObject.GetComponent<Canvas>();
					}
				}
				return this._gameCanvas;
			}
		}

		private void Start()
		{
			SceneManager.sceneLoaded += delegate(Scene scene, LoadSceneMode loadSceneMode)
			{
			};
		}

		private void ClearAllPanels()
		{
			for (int i = 0; i < this.m_currentDialogs.Count; i++)
			{
				Object.Destroy(this.m_currentDialogs[i]);
			}
		}

		public MissionDialog CreateMissionDialog(int missionID)
		{
			if (this.m_missionDialog == null)
			{
				this.m_missionDialog = Object.Instantiate<MissionDialog>(this.m_missionDialogPrefab, this.GameCanvas.GetComponentInChildren<GamePanel>().m_missionListPanel.gameObject.transform, false);
				this.m_missionDialog.gameObject.name = "MissionDialog";
			}
			this.m_missionDialog.gameObject.SetActive(true);
			this.m_missionDialog.SetMission(missionID);
			return this.m_missionDialog;
		}

		public void CloseMissionDialog()
		{
			if (this.m_missionDialog == null)
			{
				return;
			}
			this.m_missionDialog.gameObject.SetActive(false);
		}

		public ActivationConfirmationDialog CreateChampionActivationConfirmationDialog(FollowerDetailView followerDetailView)
		{
			ActivationConfirmationDialog activationConfirmationDialog = Object.Instantiate<ActivationConfirmationDialog>(this.m_activationConfirmationDialogPrefab, this.GameCanvas.transform, false);
			activationConfirmationDialog.gameObject.name = "ActivationConfirmationDialog";
			activationConfirmationDialog.Show(followerDetailView);
			return activationConfirmationDialog;
		}

		public DeactivationConfirmationDialog CreateChampionDeactivationConfirmationDialog(FollowerDetailView followerDetailView)
		{
			DeactivationConfirmationDialog deactivationConfirmationDialog = Object.Instantiate<DeactivationConfirmationDialog>(this.m_deactivationConfirmationDialogPrefab, this.GameCanvas.transform, false);
			deactivationConfirmationDialog.gameObject.name = "DeactivationConfirmationDialog";
			deactivationConfirmationDialog.Show(followerDetailView);
			return deactivationConfirmationDialog;
		}

		private static readonly string MainCanvasName = "MainCanvas";

		private static readonly string GameCanvasName = "GameCanvas";

		private Canvas _mainCanvas;

		private Canvas _gameCanvas;

		public BaseDialog m_baseDialogPrefab;

		public MissionDialog m_missionDialogPrefab;

		private MissionDialog m_missionDialog;

		public ActivationConfirmationDialog m_activationConfirmationDialogPrefab;

		public DeactivationConfirmationDialog m_deactivationConfirmationDialogPrefab;

		private List<GameObject> m_currentDialogs = new List<GameObject>();
	}
}
