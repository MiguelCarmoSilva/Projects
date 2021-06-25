var TS : float;
var paused : boolean;

function Start () {

TS = Time.timeScale;//1
}

function Update () {
if (Input.GetKeyDown("p")){
if(!paused){
paused = true;
}else{
paused = false;
}
}

if (paused){
Time.timeScale = 0.0;
TS = Time.timeScale;	
}
if (!paused){
Time.timeScale = 1.0;
TS = Time.timeScale;
   }


}