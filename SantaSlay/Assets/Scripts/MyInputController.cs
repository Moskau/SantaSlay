using UnityEngine;
using System.Collections;

public class MyInputController : MonoBehaviour
{

    private float roll;
    private float pitch;
    public float maxValue = 20F;

	private float rollCenter;
	private float pitchCenter;

    // Use this for initialization
    void Start()
    {
        //Debug.Log("My input script opened by:" + gameObject.name);
        roll = 0;
        pitch = 0;
		rollCenter = 0;
		pitchCenter = 0;
    }

    // Update is called once per frame
    void Update()
    {

		if (SixenseInput.Controllers[0].Enabled) //SixenseInput.Controllers[1] != null && 
		{
			foreach (SixenseButtons button in System.Enum.GetValues(typeof(SixenseButtons)))
			{
				if (SixenseInput.Controllers[0].GetButtonDown(button))
				{
					if (button.ToString() == "ONE")
					{
						//Debug.Log("Zerooed");
						rollCenter = SixenseInput.Controllers[0].Rotation[2];
						pitchCenter = SixenseInput.Controllers[0].Rotation[0];
					}
				}
			}
		}

		if (SixenseInput.Controllers[0].Enabled) //SixenseInput.Controllers[0] != null && 
        {
        	roll = (SixenseInput.Controllers[0].Rotation[2]-rollCenter) * -20.0F;
        	pitch = (SixenseInput.Controllers[0].Rotation[0]-pitchCenter) * 10.0F;
			if(roll > 1.0F){
				roll = 1.0F;
			}else if(roll < -1.0F){
				roll = -1.0F;
			}
			if(pitch > 1.0F){
				pitch = 1.0F;
			}else if(pitch < -1.0F){
				pitch = -1.0F;
			}
			roll = roll * maxValue;
			pitch = pitch * maxValue;
    	}
        else
        {
            roll = maxValue * Input.GetAxis("Horizontal");
            pitch = maxValue * Input.GetAxis("Vertical");
        }
    }

    public float GetPitchAngle()
    {
        //returns a normalized vector describing the sleigh rotation
        return (pitch);
    }

    public float GetRollAngle()
    {
        //returns a normalized vector describing the sleigh rotation
        return (roll);
    }

    public float GetMaxValue()
    {
        return maxValue;
    }
}
