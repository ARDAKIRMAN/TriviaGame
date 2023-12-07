using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistentManagers : Singleton<PersistentManagers>
{
	public GameSettings settings;

	public Reader reader;
	public SceneManager scene;
	public QuestionManager question;
	public Fader fader;
	public DialogueManager dialogue;
	public ConnectionManager connection;
	public EventSystemManager eventSystem;
	public FXManager fx;
	public SoundManager sound;

	public override void Awake()
	{
		base.Awake();
		if (!FindObjectOfType<Reader>()) Instantiate(reader, Instance.transform);
		if (!FindObjectOfType<SceneManager>()) Instantiate(scene, Instance.transform);
		if (!FindObjectOfType<Fader>()) Instantiate(fader, Instance.transform);
		if (!FindObjectOfType<DialogueManager>()) Instantiate(dialogue, Instance.transform);
		if (!FindObjectOfType<ConnectionManager>()) Instantiate(connection, Instance.transform);
		if (!FindObjectOfType<QuestionManager>()) Instantiate(question, Instance.transform);
		if (!FindObjectOfType<SoundManager>()) Instantiate(sound, Instance.transform);
		if (!FindObjectOfType<EventSystemManager>()) Instantiate(eventSystem, Instance.transform);
		if (!FindObjectOfType<FXManager>()) Instantiate(fx, Instance.transform);
	}
}
