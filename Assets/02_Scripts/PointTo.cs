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

				void Awake(){
						gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
            if(needsTimeIndicator){
                pi = transform.Find("PointingIndicator").GetComponent<PointingIndicator>();
                pi.gameObject.SetActive(false);
            }
				}

        protected virtual void Update()
        {
						if(pointing){
								if(Time.time>startTime+pointToTime){
                  if(pointToPopup!=null){
                    pointToPopup.SetActive(true);
                  }
                  if(needsTimeIndicator){
                    pi.StopExpanding();
                    pi.gameObject.SetActive(false);
                  }
                  pointing = false;
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
            if(needsTimeIndicator){
              pi.gameObject.SetActive(true);
              pi.StartExpanding(pointToTime);
            }
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {
						pointing = false;
            gm.UpdateValue(boolName, false);
            if(pointToPopup!=null){
              pointToPopup.SetActive(true);
            }
            if(needsTimeIndicator){
              pi.StopExpanding();
              pi.gameObject.SetActive(false);
            }

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
