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
  GUI.Label (Rect (650, 100, 500, 1000), "Shadow!\nIt seems we have news of the Gaulet's location.\nI need you to retrieve it as soon as possible\nYou can travel using our space ship.... yes we have a space ship...");
  }