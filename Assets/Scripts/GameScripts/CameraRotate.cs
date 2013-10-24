using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {
        
        public enum RotationState {BOTTOM, TOP, LEFT, RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, TOP_LEFT, TOP_RIGHT};
        
        public static RotationState rotation;
        public float tweenTime;
        private Vector3 newPos; //to determine how it should position itself in relation to the player
        // Use this for initialization
        void Start () {
                rotation = RotationState.BOTTOM;
                newPos = new Vector3();
        }
        
        // Update is called once per frame
        void Update () {
                newPos = transform.localPosition;
                /*
                 * There is one if-statement for each side of the octagon (so 8)
                 * Each if statement only checks if the player is within the area required for that side of the tube*/
                if(valueBetween(Player.playerPos.x, -35f, -14f) && Player.playerPos.y <= -14f) { //area for bottom left portion of the cube
                        if(rotation != RotationState.BOTTOM_LEFT) { //continue only if we aren't already in the bottom left state
                                newPos.x = 4f;
                                newPos.y = 4f;
                                iTween.RotateTo(gameObject, iTween.Hash("z", -45, "time", tweenTime)); //Tween the camera
                                iTween.MoveTo(gameObject, iTween.Hash("x", newPos.x, "y", newPos.y, "time", tweenTime, "islocal", true));
                                //Reposition the player so he doesn't accidentally go through the tube, depending on which state the player is coming from he position will be different
                                //Thankfully, we can only come into a state from two sides, so those are the only checks we need
                                if(rotation == RotationState.BOTTOM) Player.instance.setPos(-14f,-34.5f);
                                else if(rotation == RotationState.LEFT) Player.instance.setPos(-34.5f, -14f);
                                //Set the rotation state to bottom left so we don't continously call these tweens.
                                rotation = RotationState.BOTTOM_LEFT;
                                //Offset the camera position so it doesn't look weird on certain sides
                        }
                }
                //Same as above, for bottom right
                if(valueBetween(Player.playerPos.x, 14f, 34.5f) && Player.playerPos.y <= -14f) {
                        if(rotation != RotationState.BOTTOM_RIGHT) {
                                newPos.x = -4f;
                                newPos.y = 4f;
                                iTween.RotateTo(gameObject, iTween.Hash("z", 45, "time", tweenTime));
                                iTween.MoveTo(gameObject, iTween.Hash("x", newPos.x, "y", newPos.y, "time", tweenTime, "islocal", true));
                                if(rotation == RotationState.BOTTOM) Player.instance.setPos(14f,-34.5f);
                                else if(rotation == RotationState.RIGHT) Player.instance.setPos(34.5f, -14f);
                                rotation = RotationState.BOTTOM_RIGHT;
                        }
                }
                //Same as above, for bottom
                if(valueBetween(Player.playerPos.x, -14f, 14f) && Player.playerPos.y <= -34.5f) {
                        if(rotation != RotationState.BOTTOM) {
                                newPos.x = 0f;
                                newPos.y = 4f;
                                iTween.RotateTo(gameObject, iTween.Hash("z", 0, "time", tweenTime));
                                iTween.MoveTo(gameObject, iTween.Hash("x", newPos.x, "y", newPos.y, "time", tweenTime, "islocal", true));
                                if(rotation == RotationState.BOTTOM_LEFT) Player.instance.setPos(-14f,-34.5f);
                                else if(rotation == RotationState.BOTTOM_RIGHT) Player.instance.setPos(14f, -34.5f);
                                rotation = RotationState.BOTTOM;
                        }
                }
                //Same as above, for left
                if(Player.playerPos.x <= -34.5f && valueBetween(Player.playerPos.y, -14f, 14f)) {
                        if(rotation != RotationState.LEFT) {
                                newPos.x = 4f;
                                newPos.y = 0f;
                                iTween.RotateTo(gameObject, iTween.Hash("z", -90, "time", tweenTime));
                                iTween.MoveTo(gameObject, iTween.Hash("x", newPos.x, "y", newPos.y, "time", tweenTime, "islocal", true));
                                if(rotation == RotationState.BOTTOM_LEFT) Player.instance.setPos(-34.5f, -14f);
                                else if(rotation == RotationState.TOP_LEFT) Player.instance.setPos(-34.5f, 14f);
                                rotation = RotationState.LEFT;
                        }
                }
                
                //Same as above, for right
                if(Player.playerPos.x >= 34.5f && valueBetween(Player.playerPos.y, -14f, 14f)) {
                        if(rotation != RotationState.RIGHT) {
                                newPos.x = -4f;
                                newPos.y = 0f;
                                iTween.RotateTo(gameObject, iTween.Hash("z", 90, "time", tweenTime));
                                iTween.MoveTo(gameObject, iTween.Hash("x", newPos.x, "y", newPos.y, "time", tweenTime, "islocal", true));
                                if(rotation == RotationState.BOTTOM_RIGHT) Player.instance.setPos(34.5f, -14f);
                                else if(rotation == RotationState.TOP_RIGHT) Player.instance.setPos(34.5f, 14f);
                                rotation = RotationState.RIGHT;
                        }
                }
                //Same as above, for bottom top right
                if(valueBetween(Player.playerPos.x, 14f, 34.5f) && Player.playerPos.y >= 14f) {
                        if(rotation != RotationState.TOP_RIGHT) {
                                newPos.x = -4f;
                                newPos.y = -4f;
                                iTween.RotateTo(gameObject, iTween.Hash("z", 135, "time", tweenTime));
                                iTween.MoveTo(gameObject, iTween.Hash("x", newPos.x, "y", newPos.y, "time", tweenTime, "islocal", true));
                                if(rotation == RotationState.RIGHT) Player.instance.setPos(34.5f, 14f);
                                else if(rotation == RotationState.TOP) Player.instance.setPos(14f, 34.5f);
                                rotation = RotationState.TOP_RIGHT;
                        }
                }
                //Same as above, for top left
                if(valueBetween(Player.playerPos.x, -34.5f, -14f) && Player.playerPos.y >= 14f) {
                        if(rotation != RotationState.TOP_LEFT) {
                                newPos.x = 4f;
                                newPos.y = -4f;
                                iTween.RotateTo(gameObject, iTween.Hash("z", -135, "time", tweenTime));
                                iTween.MoveTo(gameObject, iTween.Hash("x", newPos.x, "y", newPos.y, "time", tweenTime, "islocal", true));
                                if(rotation == RotationState.TOP) Player.instance.setPos(-14f, 34.5f);
                                else if(rotation == RotationState.LEFT) Player.instance.setPos(-34.5f, 14f);
                                rotation = RotationState.TOP_LEFT;
                        }
                }
                //Same as above, for top
                if(valueBetween(Player.playerPos.x, -14f, 14f) && Player.playerPos.y >= 34.5f) {
                        if(rotation != RotationState.TOP) {
                                newPos.x = 0f;
                                newPos.y = -4f;
                                iTween.RotateTo(gameObject, iTween.Hash("z", 180, "time", tweenTime));
                                iTween.MoveTo(gameObject, iTween.Hash("x", newPos.x, "y", newPos.y, "time", tweenTime, "islocal", true));
                                if(rotation == RotationState.TOP_LEFT) Player.instance.setPos(-14f,34.5f);
                                else if(rotation == RotationState.TOP_RIGHT) Player.instance.setPos(14f, 34.5f);
                                rotation = RotationState.TOP;
                        }
                }
        }
        
        //for making the update checks a little easier
        private bool valueBetween(float x, float min, float max) {
                return (x >= min && x <= max);
        }
}