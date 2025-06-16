using UnityEngine; 
using System.Collections;

public class SandalyeKodu : MonoBehaviour { 
public GameObject character; 
Animator anim; 
bool isWalkingTowards = false; 
bool sittingOn = false;

void OnMouseDown() 
{
if(!sittingOn)
{
	anim.SetTrigger("isWalking"); 
	isWalkingTowards = true;
}
}

// Use this for initialization 
void Start () {
anim = character.GetComponent<Animator>();
} 
// Update is called once per 
void Update () 
{ 
if (isWalkingTowards)
{
Vector3 targetDir;
targetDir = new Vector3(transform.position.x - character.transform.position.x, 
0f,
transform.position.z - character.transform.position.z);

Quaternion rot = Quaternion.LookRotation(targetDir);
character.transform.rotation = Quaternion.Slerp(character.transform.rotation, rot, 0.05f);
character.transform.Translate(Vector3.forward * 0.01f);

if(Vector3.Distance(character.transform.position, this.transform.position) < 0.6)
{
anim.SetTrigger("isSitting");
//turn character around to align forward vector 
//with object's vector 
character.transform.rotation = this.transform.rotation;

isWalkingTowards = false;
sittingOn = true;
}
}
}
}