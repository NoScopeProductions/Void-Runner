using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {
        
        public enum RotationState {BOTTOM, TOP, LEFT, RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, TOP_LEFT, TOP_RIGHT};
        
        public static RotationState rotation;
        public float tweenTime;
		public float rotateTime;
      //  private Vector3 newPos; //to determine how it should position itself in relation to the player
        // Use this for initialization
        void Start () {
                rotation = RotationState.BOTTOM;
        }
        
        // Update is called once per frame
        void Update () {
                /*
                 * There is one if-statement for each side of the octagon (so 8)
                 * Each if statement only checks if the player is within the area required for that side of the tube*/
                if(valueBetween(Player.playerPos.x, -35f, -14f) && Player.playerPos.y <= -14f) { //area for bottom left portion of the cube
                        if(rotation != RotationState.BOTTOM_LEFT) { //continue only if we aren't already in the bottom left state
                                iTween.RotateTo(gameObject, iTween.Hash("z", -45, "time", tweenTime)); //Tween the camera
                                //Reposition the player so he doesn't accidentally go through the tube, depending on which state the player is coming from he position will be different
                                //Thankfully, we can only come into a state from two sides, so those are the only checks we need
                                if(rotation == RotationState.BOTTOM) Player.instance.setPos(-14f,-34.5f);
                                else if(rotation == RotationState.LEFT) Player.instance.setPos(-34.5f, -14f);
                                //Set the rotation state to bottom left so we don't continously call these tweens.
                                rotation = RotationState.BOTTOM_LEFT;
                                //rotate the player model
								iTween.RotateTo(Player.instance.gameObject, iTween.Hash("time", rotateTime, "z", -45));
                        }
                }
                //Same as above, for bottom right
                if(valueBetween(Player.playerPos.x, 14f, 34.5f) && Player.playerPos.y <= -14f) {
                        if(rotation != RotationState.BOTTOM_RIGHT) {
                                iTween.RotateTo(gameObject, iTween.Hash("z", 45, "time", tweenTime));
                                if(rotation == RotationState.BOTTOM) Player.instance.setPos(14f,-34.5f);
                                else if(rotation == RotationState.RIGHT) Player.instance.setPos(34.5f, -14f);
                                rotation = RotationState.BOTTOM_RIGHT;
								iTween.RotateTo(Player.instance.gameObject, iTween.Hash("time", rotateTime, "z", 45));
                        }
                }
                //Same as above, for bottom
                if(valueBetween(Player.playerPos.x, -14f, 14f) && Player.playerPos.y <= -34.5f) {
                        if(rotation != RotationState.BOTTOM) {
                                iTween.RotateTo(gameObject, iTween.Hash("z", 0, "time", tweenTime));
                                if(rotation == RotationState.BOTTOM_LEFT) Player.instance.setPos(-14f,-34.5f);
                                else if(rotation == RotationState.BOTTOM_RIGHT) Player.instance.setPos(14f, -34.5f);
                                rotation = RotationState.BOTTOM;
								iTween.RotateTo(Player.instance.gameObject, iTween.Hash("time", rotateTime, "z", 0));
                        }
                }
                //Same as above, for left
                if(Player.playerPos.x <= -34.5f && valueBetween(Player.playerPos.y, -14f, 14f)) {
                        if(rotation != RotationState.LEFT) {
                                iTween.RotateTo(gameObject, iTween.Hash("z", -90, "time", tweenTime));
                                if(rotation == RotationState.BOTTOM_LEFT) Player.instance.setPos(-34.5f, -14f);
                                else if(rotation == RotationState.TOP_LEFT) Player.instance.setPos(-34.5f, 14f);
                                rotation = RotationState.LEFT;
								iTween.RotateTo(Player.instance.gameObject, iTween.Hash("time", rotateTime, "z", -90));
                        }
                }
                
                //Same as above, for right
                if(Player.playerPos.x >= 34.5f && valueBetween(Player.playerPos.y, -14f, 14f)) {
                        if(rotation != RotationState.RIGHT) {
                                iTween.RotateTo(gameObject, iTween.Hash("z", 90, "time", tweenTime));
                                if(rotation == RotationState.BOTTOM_RIGHT) Player.instance.setPos(34.5f, -14f);
                                else if(rotation == RotationState.TOP_RIGHT) Player.instance.setPos(34.5f, 14f);
                                rotation = RotationState.RIGHT;
								iTween.RotateTo(Player.instance.gameObject, iTween.Hash("time", rotateTime, "z", 90));
                        }
                }
                //Same as above, for bottom top right
                if(valueBetween(Player.playerPos.x, 14f, 34.5f) && Player.playerPos.y >= 14f) {
                        if(rotation != RotationState.TOP_RIGHT) {
                                iTween.RotateTo(gameObject, iTween.Hash("z", 135, "time", tweenTime));
                                if(rotation == RotationState.RIGHT) Player.instance.setPos(34.5f, 14f);
                                else if(rotation == RotationState.TOP) Player.instance.setPos(14f, 34.5f);
                                rotation = RotationState.TOP_RIGHT;
								iTween.RotateTo(Player.instance.gameObject, iTween.Hash("time", rotateTime, "z", 135));
                        }
                }
                //Same as above, for top left
                if(valueBetween(Player.playerPos.x, -34.5f, -14f) && Player.playerPos.y >= 14f) {
                        if(rotation != RotationState.TOP_LEFT) {
                                iTween.RotateTo(gameObject, iTween.Hash("z", -135, "time", tweenTime));
                                if(rotation == RotationState.TOP) Player.instance.setPos(-14f, 34.5f);
                                else if(rotation == RotationState.LEFT) Player.instance.setPos(-34.5f, 14f);
                                rotation = RotationState.TOP_LEFT;
								iTween.RotateTo(Player.instance.gameObject, iTween.Hash("time", rotateTime, "z", -135));
                        }
                }
                //Same as above, for top
                if(valueBetween(Player.playerPos.x, -14f, 14f) && Player.playerPos.y >= 34.5f) {
                        if(rotation != RotationState.TOP) {
                                iTween.RotateTo(gameObject, iTween.Hash("z", 180, "time", tweenTime));
                                if(rotation == RotationState.TOP_LEFT) Player.instance.setPos(-14f,34.5f);
                                else if(rotation == RotationState.TOP_RIGHT) Player.instance.setPos(14f, 34.5f);
                                rotation = RotationState.TOP;
								iTween.RotateTo(Player.instance.gameObject, iTween.Hash("time", rotateTime, "z", 180));
                        }
                }
        }
        
        //for making the update checks a little easier
        private bool valueBetween(float x, float min, float max) {
                return (x >= min && x <= max);
        }
}