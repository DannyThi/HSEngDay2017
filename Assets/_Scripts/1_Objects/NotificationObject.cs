using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NotificationObject : TimedObject, IComparable<NotificationObject> {

	public string notificationName;

	public NotificationObject(float time, string notificationName) : base(time) {
		this.notificationName = notificationName;
	}

	public int CompareTo(NotificationObject other) {
		return base.CompareTo (other);
	}
}
