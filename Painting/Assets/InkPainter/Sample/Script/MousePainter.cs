using UnityEngine;

namespace Es.InkPainter.Sample
{
	public class MousePainter : MonoBehaviour
	{
		/// <summary>
		/// Types of methods used to paint.
		/// </summary>
		[System.Serializable]
		private enum UseMethodType
		{
			RaycastHitInfo,
			WorldPoint,
			NearestSurfacePoint,
			DirectUV,
		}

		public Brush brush;

        // 패인트 밀도를 지정 (0 ~ 100)
        [SerializeField]
        private float paintDense = 80;

        // 패인트 1step size (0.0f ~ 1.0f)
        [SerializeField]
        private float paintDist = 0.01f;

        [SerializeField]
		private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;

		[SerializeField]
		bool erase = false;

        // 참 시간 상한값
        private float denseDelay = 1f;

        private float startTimer, distMesureTimer = 0.0f;
        private bool skip = false;


        private void Start()
        {
            // 거짓 시간 상한값
            paintDense = 10f;
            paintDist = 0.01f;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                distMesureTimer = Time.time - startTimer;

                // timer
                // | * |   |    => non-skip
                if (distMesureTimer < (paintDist * paintDense * Time.fixedDeltaTime))
                    skip = true;
                // |   | * |    => skip
                else if (distMesureTimer < (denseDelay * paintDense * Time.fixedDeltaTime))
                    skip = false;
                // |   |   | *  => reMesure
                else
                    startTimer = Time.time;
            }
        }

        private void Update()
		{
            if (Input.GetMouseButtonDown(0))
                startTimer = Time.time;

			if(Input.GetMouseButton(0))
			{
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				bool success = true;
				RaycastHit hitInfo;
				if(Physics.Raycast(ray, out hitInfo))
				{
					var paintObject = hitInfo.transform.GetComponent<InkCanvas>();

                    if (paintObject != null)
                    {
                        if (skip)
                            return;

                        switch (useMethodType)
                        {
                            case UseMethodType.RaycastHitInfo:
                                success = erase ? paintObject.Erase(brush, hitInfo) : paintObject.Paint(brush, hitInfo);
                                break;

                            case UseMethodType.WorldPoint:
                                success = erase ? paintObject.Erase(brush, hitInfo.point) : paintObject.Paint(brush, hitInfo.point);
                                break;

                            case UseMethodType.NearestSurfacePoint:
                                success = erase ? paintObject.EraseNearestTriangleSurface(brush, hitInfo.point) : paintObject.PaintNearestTriangleSurface(brush, hitInfo.point);
                                break;

                            case UseMethodType.DirectUV:
                                if (!(hitInfo.collider is MeshCollider))
                                    Debug.LogWarning("Raycast may be unexpected if you do not use MeshCollider.");
                                success = erase ? paintObject.EraseUVDirect(brush, hitInfo.textureCoord) : paintObject.PaintUVDirect(brush, hitInfo.textureCoord);
                                break;
                        }
                    }
					if(!success)
						Debug.LogError("Failed to paint.");
				}
			}
		}

		public void OnGUI()
		{
			if(GUILayout.Button("Reset"))
			{
				foreach(var canvas in FindObjectsOfType<InkCanvas>())
					canvas.ResetPaint();
			}
		}

        public void setDense(float d)
        {
            paintDense = d;
        }

        public float getDense()
        {
            return paintDense;
        }

        public void setDist(float d)
        {
            paintDist = 1.0f - d;
        }

        public float getDist()
        {
            return paintDist;
        }

        public void setErase(bool s)
        {
            erase = s;
        }

        public bool getErase()
        {
            return erase;
        }

    }
}