using System.Globalization;
using UnityEngine.UI;
using UnityEngine;


public class Compass : MonoBehaviour
{
	public RawImage compassImage;
	public Transform player;
	public Text compassDirectionText;

	public void Update()
	{
		//Get a handle on the Image's uvRect
		compassImage.uvRect = new Rect(player.localEulerAngles.y / 360, 0, 1, 1);

		// Get a copy of your forward vector
		Vector3 forward = player.transform.forward;

		// Zero out the y component of your forward vector to only get the direction in the X,Z plane
		forward.y = 0;

		//Clamp our angles to only 5 degree increments
		float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;
		headingAngle = 5 * (Mathf.RoundToInt(headingAngle / 5.0f));

		if (headingAngle > 180 && headingAngle <= 360)
			headingAngle = headingAngle - 360;

		//Convert float to int for switch
		int displayangle;
		displayangle = Mathf.RoundToInt(headingAngle);



		//Set the text of Compass Degree Text to the clamped value, but change it to the letter if it is a True direction
		switch (displayangle)
		{
		case 0:
			compassDirectionText.text = "N";
			break;
		case 45:
			compassDirectionText.text = "NE";
			break;
		case 90:
			compassDirectionText.text = "E";
			break;
		case 135:
			compassDirectionText.text = "SE";
			break;
		case 180:
			compassDirectionText.text = "S";
			break;
		case -135:
			compassDirectionText.text = "SW";
			break;
		case -90:
			compassDirectionText.text = "W";
			break;
		case -45:
			compassDirectionText.text = "NW";
			break;
		default:
			compassDirectionText.text = headingAngle.ToString(CultureInfo.InvariantCulture);
			break;
		}
	}
}