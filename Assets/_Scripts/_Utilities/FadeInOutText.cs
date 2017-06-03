using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInOutText : MonoBehaviour {
	
	public float delayFade = 7; 
	public float fadeTime = 1;

	private TMP_Text m_TextComponent;
	//private TextMeshPro m_TextComponent;
	private float alpha = 0.0f;
	private TMP_TextInfo textInfo;
	private Color32 characterColor;

	void Awake()
	{
		m_TextComponent = GetComponentInChildren<TMP_Text> ();
		//m_TextComponent = GetComponent<TMP_Text> ();
		//m_TextComponent = GetComponent<TextMeshPro> ();
		if (m_TextComponent == null) {
			Debug.Log (name + ": No m_TextComponent");
		}
	}

	void Start() {
		m_TextComponent.overrideColorTags = false;
		textInfo = m_TextComponent.textInfo;
		characterColor = m_TextComponent.color;
		StartCoroutine (StartFadeIn ());
	}

	IEnumerator StartFadeIn() {
		yield return new WaitForEndOfFrame ();
		FadeIn ();
		yield return new WaitForSeconds (delayFade);
		while (true) {
			FadeIn ();
			yield return new WaitForEndOfFrame ();
		}
	}

	public void FadeIn() {
		alpha = Mathf.Lerp (alpha, 255, Time.deltaTime / fadeTime);
		characterColor = new Color32 ((byte)characterColor.r, (byte)characterColor.g, (byte)characterColor.g, (byte)alpha);

		for (int i = 0; i < textInfo.characterCount; i++) {
			int materialIndex = textInfo.characterInfo [i].materialReferenceIndex;
			Color32[] newVertexColors = textInfo.meshInfo [materialIndex].colors32;
			int vertexIndex = textInfo.characterInfo [i].vertexIndex;

			newVertexColors [vertexIndex + 0] = characterColor;
			newVertexColors [vertexIndex + 1] = characterColor;
			newVertexColors [vertexIndex + 2] = characterColor;
			newVertexColors [vertexIndex + 3] = characterColor;
		}

		m_TextComponent.UpdateVertexData (TMP_VertexDataUpdateFlags.Colors32);
	}

//	void Update() {
//		alpha = Mathf.Lerp (alpha, 255, Time.deltaTime / fadeTime);
//		characterColor = new Color32((byte)characterColor.r, (byte)characterColor.g, (byte)characterColor.g, (byte)alpha);
//
//		for (int i = 0; i < textInfo.characterCount; i++) {
//			int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
//			Color32[] newVertexColors = textInfo.meshInfo[materialIndex].colors32;
//			int vertexIndex = textInfo.characterInfo[i].vertexIndex;
//
//			newVertexColors[vertexIndex + 0] = characterColor;
//			newVertexColors[vertexIndex + 1] = characterColor;
//			newVertexColors[vertexIndex + 2] = characterColor;
//			newVertexColors[vertexIndex + 3] = characterColor;
//		}
//
//		m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
//	}
//
}