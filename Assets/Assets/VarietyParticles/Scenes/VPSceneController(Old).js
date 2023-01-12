#pragma strict

function Start () {

}

function Update () {

		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
			Application.LoadLevel(0);
        }

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Application.LoadLevel(1);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
        {
			Application.LoadLevel(2);
        }

		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			Application.LoadLevel(3);
		}

		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			Application.LoadLevel(4);
		}

		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			Application.LoadLevel(5);
		}

		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			Application.LoadLevel(6);
		}

		if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			Application.LoadLevel(7);
		}

//		if (Input.GetKeyDown(KeyCode.Alpha9))
//		{
//			Application.LoadLevel(8);
//		}
}