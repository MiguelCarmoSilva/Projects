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
  GUI.Label (Rect (650, 200, 200, 20), "I've been summoned");
  }