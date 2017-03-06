﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using System.Net.Mail;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour
{
	PlayerPrefsDAO playerPrefsDAO;

	void Start ()
	{
		playerPrefsDAO = new PlayerPrefsDAO ();
		if (playerPrefsDAO.checkAccount ()) {
			nextScene ();
		}
	}
		
	void Update ()
	{
		
	}

	private bool validateName (string name)
	{
		bool valid = false;
		if (name != "") {
			valid = true;
		}
		return valid;
	}

	private bool validateEmail (string email)
	{
		bool valid = false;
		if (email != "") {
			valid = Regex.IsMatch (email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

		}
		return valid;
	}

	public void validateNameFieldUI (string name)
	{
		bool validName = validateName (name);
		ColorBlock cb = GameObject.Find ("NameFieldUI").GetComponent<InputField> ().colors;
		Color errorColor = Color.red;
		if (!validName) {
			cb.highlightedColor = errorColor;
			cb.normalColor = errorColor;
		} else {
			cb.highlightedColor = Color.white;
			cb.normalColor = Color.white;
		}
		GameObject.Find ("NameFieldUI").GetComponent<InputField> ().colors = cb;
	}

	public void validateEmailFieldUI (string email)
	{
		bool validEmail = validateEmail (email);
		ColorBlock cb = GameObject.Find ("EmailFieldUI").GetComponent<InputField> ().colors;
		Color errorColor = Color.red;
		if (!validEmail) {
			cb.highlightedColor = errorColor;
			cb.normalColor = errorColor;
		} else {
			cb.highlightedColor = Color.white;
			cb.normalColor = Color.white;
		}
		GameObject.Find ("EmailFieldUI").GetComponent<InputField> ().colors = cb;
	}

	public void saveDataAndContinue ()
	{
		InputField nameField = GameObject.Find ("NameFieldUI").GetComponent<InputField> ();
		InputField emailField = GameObject.Find ("EmailFieldUI").GetComponent<InputField> ();
		playerPrefsDAO = new PlayerPrefsDAO ();
		if (validateName (nameField.text) && validateEmail (emailField.text)) {
			playerPrefsDAO.setName (nameField.text);
			playerPrefsDAO.setEmail (emailField.text);
			nextScene ();
		} else {
			validateNameFieldUI (nameField.text);
			validateEmailFieldUI (emailField.text);
		}
	}

	private void nextScene(){
		SceneLoader sceneLoader = new SceneLoader ();
		sceneLoader.LoadNewSceneGO ();
	}
}
