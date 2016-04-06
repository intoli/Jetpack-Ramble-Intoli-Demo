using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject objToFollow;

    private float offsetX;
    private float introOffsetX;
    private float playOffsetX;

    private float currentTime = 0.0f;
    private float transitionTime = 2.0f;

    private MouseController mouseController;

    public void Start()
    {
        var height = Camera.main.orthographicSize * 2;
        var screenWidth = Camera.main.aspect * height;
        this.introOffsetX = 0.0f;
        this.playOffsetX = screenWidth / 4;

        this.mouseController = (MouseController) this.objToFollow.GetComponentInChildren(typeof(MouseController));
    }

    public void Update()
    {
        if(mouseController.intro) {
            this.offsetX = this.introOffsetX;
        }
        else {
            if(this.currentTime >= this.transitionTime) {
                this.offsetX = this.playOffsetX;
            }
            else {
                this.currentTime += Time.deltaTime;
                this.offsetX = this.introOffsetX + (this.currentTime / this.transitionTime) * (this.playOffsetX - this.introOffsetX);
                this.offsetX = Mathf.Clamp(this.offsetX, this.introOffsetX, this.playOffsetX);
            }
        }

        var currentPosition = this.transform.position;
        currentPosition.x = this.objToFollow.transform.position.x
            + this.offsetX;
        this.transform.position = currentPosition;
    }
}
