var quit=false;
var music : AudioSource;

function OnMouseEnter () {
	GetComponent.<Renderer>().material.color = Color.black;
}
function OnMouseExit () {
	GetComponent.<Renderer>().material.color = Color.white;
}

function OnMouseUp () {
	if (quit == true){
	Application.Quit();}
	else{
	Application.LoadLevel(1);}
}
function Update () {
if (Input.GetKey(KeyCode.Escape)){
	Application.Quit();}
}

function SetGameVolume (vol: float){
	music.volume = vol;
}