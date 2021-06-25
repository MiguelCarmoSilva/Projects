var toggleGUI : boolean;
 
  function Start() { 
 toggleGUI = false;
 }
 
  function OnTriggerEnter (other : Collider) {
  toggleGUI = true;
  }
  
  function OnTriggerExit (other : Collider) {
  toggleGUI = false;
  }
  
  function OnGUI () {
  if (toggleGUI == true)
  GUI.Label (Rect (650, 550, 400, 20), "Nothing weird socializing this way, after all we are ninja's");
  }