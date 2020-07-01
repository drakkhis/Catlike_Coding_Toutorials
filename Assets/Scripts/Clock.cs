using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private Transform hoursTransform, minutesTransform, secondsTransform;
    private int degreesPerHour = 30;
    private int degreesPerMinute = 6;
    private int degreesPerSecond = 6;

    // Start is called before the first frame update
    void Update()
	{
		TimeSpan time = DateTime.Now.TimeOfDay;
		hoursTransform.localRotation =
			Quaternion.Euler(0f, (float)time.TotalHours * degreesPerHour, 0f);
		minutesTransform.localRotation =
			Quaternion.Euler(0f, (float)time.TotalMinutes * degreesPerMinute, 0f);
		secondsTransform.localRotation =
			Quaternion.Euler(0f, (float)time.TotalSeconds * degreesPerSecond, 0f);
	}
}
