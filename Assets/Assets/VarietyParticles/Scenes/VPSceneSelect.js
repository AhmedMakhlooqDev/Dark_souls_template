#pragma strict

var VPselGridInt : int = -1;
var VPselStrings : String[] = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"];

	
	function OnGUI ()
	{
		VPselGridInt = GUI.SelectionGrid (Rect (Screen.width /2 -150, Screen.height * 0.9, 300, 50), VPselGridInt, VPselStrings, 11);
			
		 if (VPselGridInt == 0){
             Application.LoadLevel("Scene1");
         }
         
         if (VPselGridInt == 1){
             Application.LoadLevel("Scene2");
         }
         
         if (VPselGridInt == 2){
             Application.LoadLevel("Scene3");
         }
         
         if (VPselGridInt == 3){
             Application.LoadLevel("Scene4");
         }
         
         if (VPselGridInt == 4){
             Application.LoadLevel("Scene5");
         }
         
         if (VPselGridInt == 5){
             Application.LoadLevel("Scene6");
         }
         
         if (VPselGridInt == 6){
             Application.LoadLevel("Scene7");
         }
         
         if (VPselGridInt == 7){
             Application.LoadLevel("Scene8");
         }
         
         if (VPselGridInt == 8){
             Application.LoadLevel("Scene9");
         }   
         
         if (VPselGridInt == 9){
             Application.LoadLevel("Scene10");
         }
         
         if (VPselGridInt == 10){
             Application.LoadLevel("Scene11");
         }
	}