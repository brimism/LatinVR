namespace VRTK.Examples
{
    using UnityEngine;

    public class PointTo : MonoBehaviour
    {
        public VRTK_InteractableObject linkedObject;
        public string boolName;
				public float pointToTime;
        public bool needsTimeIndicator;
        public GameObject pointToPopup;

				private float startTime = -1f;
				private bool pointing = false;
				private GameManagerScript gm;
        private PointingIndicator pi;

				void Start(){
						gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
            if(pointToTime>0f){

            }
				}

        protected virtual void Update()
        {
						if(pointing){
								if(Time.time>startTime+pointToTime){
									gm.pointToBox = true;
                  gm.UpdateValue(boolName, true);
									gm.Reevaluate();
								}
						}
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
						startTime = Time.time;
						pointing = true;
            if(pointToPopup!=null){
              pointToPopup.SetActive(true);
            }
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {
						pointing = false;
            gm.UpdateValue(boolName, false);
        }

        protected virtual void OnEnable()
        {
            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
                linkedObject.InteractableObjectUnused += InteractableObjectUnused;
            }
        }

        protected virtual void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
                linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
            }
        }
    }
}
