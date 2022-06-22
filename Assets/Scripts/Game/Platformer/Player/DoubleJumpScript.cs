using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpScript : JumpBehaviourScript{

	public bool doubleJump = false;
	public override Vector2 Jump(){
		if(CheckGroundUnderPlayer()) doubleJump = false;
		dir.x = rb.velocity.x;
		dir.y = rb.velocity.y;
		if(CheckGroundUnderPlayer() || doubleJump){
			dir.y = jumpPower;
			if(!doubleJump){
				doubleJump = true;
			}else{
				doubleJump = false;
			}
		}
		return dir;
	}

}
