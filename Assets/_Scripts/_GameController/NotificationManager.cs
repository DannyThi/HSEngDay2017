using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

	public List<NotificationObject> notifications;

	public delegate void Notification(string name);
	public static event Notification eventNotification;

	void Start() {
		notifications.Sort ();
	}

	void Update() {
		if (notifications.Count != 0) {
			if (Time.time >= notifications [0].spawnTime) {
				if (eventNotification != null) {
					eventNotification (notifications [0].notificationName);
					notifications.RemoveAt (0);
				}
			}
		}
	}

}
