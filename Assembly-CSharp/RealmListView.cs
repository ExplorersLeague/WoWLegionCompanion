using System;
using bnet.protocol;
using UnityEngine;
using UnityEngine.UI;

public class RealmListView : MonoBehaviour
{
	private void Start()
	{
		this.m_titleText.font = GeneralHelpers.LoadFancyFont();
		this.m_titleText.text = StaticDB.GetString("REALM_SELECTION", null);
		this.m_cancelText.font = GeneralHelpers.LoadStandardFont();
		this.m_cancelText.text = StaticDB.GetString("CANCEL", null);
	}

	private void Update()
	{
	}

	private void OnEnable()
	{
		if (Main.instance)
		{
		}
	}

	public void ClearBnRealmList()
	{
		BnLoginButton[] componentsInChildren = this.loginListContents.transform.GetComponentsInChildren<BnLoginButton>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Object.DestroyImmediate(componentsInChildren[i].gameObject);
		}
		BnGameAccountButton[] componentsInChildren2 = this.loginListContents.transform.GetComponentsInChildren<BnGameAccountButton>(true);
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			Object.DestroyImmediate(componentsInChildren2[j].gameObject);
		}
	}

	public void AddGameAccountButton(EntityId gameAccount, string name, bool isBanned, bool isSuspended)
	{
		BnLoginButton[] componentsInChildren = this.loginListContents.GetComponentsInChildren<BnLoginButton>();
		int num = (componentsInChildren == null) ? 0 : componentsInChildren.Length;
		GameObject gameObject = Object.Instantiate<GameObject>(this.gameAccountButtonPrefab);
		gameObject.transform.SetParent(this.loginListContents.transform, false);
		BnGameAccountButton component = gameObject.GetComponent<BnGameAccountButton>();
		component.SetInfo(gameAccount, name, isBanned, isSuspended);
		FancyEntrance component2 = gameObject.GetComponent<FancyEntrance>();
		component2.m_timeToDelayEntrance = this.m_listItemInitialEntranceDelay + this.m_listItemEntranceDelay * (float)num;
		component2.Activate();
	}

	public void AddBnLoginButton(string realmName, ulong realmAddress, string subRegion, int characterCount, bool online)
	{
		BnLoginButton[] componentsInChildren = this.loginListContents.GetComponentsInChildren<BnLoginButton>();
		int num = (componentsInChildren == null) ? 0 : componentsInChildren.Length;
		GameObject gameObject = Object.Instantiate<GameObject>(this.bnLoginListItemPrefab);
		gameObject.transform.SetParent(this.loginListContents.transform, false);
		BnLoginButton component = gameObject.GetComponent<BnLoginButton>();
		component.SetInfo(realmAddress, realmName, subRegion, characterCount, online);
		FancyEntrance component2 = gameObject.GetComponent<FancyEntrance>();
		component2.m_timeToDelayEntrance = this.m_listItemInitialEntranceDelay + this.m_listItemEntranceDelay * (float)num;
		component2.Activate();
	}

	public void UpdateLoginButton(ulong realmAddress, bool online)
	{
		BnLoginButton[] componentsInChildren = this.loginListContents.GetComponentsInChildren<BnLoginButton>();
		if (componentsInChildren == null)
		{
			return;
		}
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].GetRealmAddress() == realmAddress)
			{
				componentsInChildren[i].SetOnline(online);
				return;
			}
		}
	}

	public bool BnLoginButtonExists(ulong realmAddress)
	{
		BnLoginButton[] componentsInChildren = this.loginListContents.GetComponentsInChildren<BnLoginButton>();
		if (componentsInChildren == null)
		{
			return false;
		}
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].GetRealmAddress() == realmAddress)
			{
				return true;
			}
		}
		return false;
	}

	public void SetRealmListTitle()
	{
		this.m_titleText.text = StaticDB.GetString("REALM_SELECTION", null);
	}

	public void SetGameAccountTitle()
	{
		this.m_titleText.text = StaticDB.GetString("ACCOUNT_SELECTION", null);
	}

	public GameObject loginListItemPrefab;

	public GameObject bnLoginListItemPrefab;

	public GameObject gameAccountButtonPrefab;

	public GameObject loginListContents;

	public float m_listItemInitialEntranceDelay;

	public float m_listItemEntranceDelay;

	public Text m_titleText;

	public Text m_cancelText;
}
