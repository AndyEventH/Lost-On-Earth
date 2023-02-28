using UnityEngine.UI;

using UnityEngine;
using System.Collections;

// attach to UI Text component (with the full text already there)

public class UITextTypeWriter : MonoBehaviour
{

	Text txt;
	string story;
	public bool isDone;

	[Header("Audio")]
	[SerializeField] AudioSource UISource;

	private void Awake()
	{
		txt = GetComponent<Text>();
		story = txt.text;
		txt.text = "";
	}

	private IEnumerator PlayText()
	{
		foreach (char c in story)
		{
			if (!isDone)
			{
				txt.text += c;
			}
			yield return new WaitForSecondsRealtime(0.045f);
		}
		UISource.Stop();
		isDone = true;
	}
	public void StartWriting()
    {
		UISource.Play();
		StartCoroutine(PlayText());
	}
	public void FinishWriting()
	{
		UISource.Stop();
		isDone = true;
		txt.text = story;
	}
}