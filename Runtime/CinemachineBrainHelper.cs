using Cinemachine;
using UnityEngine;

namespace ThomasBrown.Cinemachine
{
    /// <summary>
    /// Adds additional functionality and events to the <see cref="CinemachineBrain"/>
    /// </summary>
    public class CinemachineBrainHelper : MonoBehaviour
    {
        /// <summary>
        /// THe cinemachine brain this class will help with
        /// </summary>
        [Tooltip("The cinemachine brain this class will help.")]
        public CinemachineBrain cinemachineBrain;
        /// <summary>
        /// Invoked when the camera has completely blended from one camera to another. Signature = toCamera, fromCamera.
        /// </summary>
        [Space]
        [Tooltip("Invoked when the camera has completely blended from one camera to another. Signature = toCamera, fromCamera.")]
        public CinemachineBrain.VcamActivatedEvent onCameraBlendCompleteEvent;
        
        /// <summary>
        /// In initial camera we were blending from. In other words the camera that started the blend sequence. 
        /// </summary>
        private ICinemachineCamera fromCamera;
        /// <summary>
        /// The polling rate at which we will check if the IsBlending flag is set to false.
        /// </summary>
        private const float CHECK_BRAIN_IS_BLENDING_REPEAT_RATE = 0.1f;
        
        private void Awake()
        {
            cinemachineBrain.m_CameraActivatedEvent.AddListener(OnCameraActivatedEventHandler);
        }
        
        /// <summary>
        /// Attaches to <see cref="cinemachineBrain"/> in order to spawn our <see cref="onCameraBlendCompleteEvent"/>
        /// when the cameras are finished blending (brain.IsBlending = false).
        /// </summary>
        /// <param name="toCamera">The camera we blended to.</param>
        /// <param name="fromCamera">The camera we blended away from.</param>
        private void OnCameraActivatedEventHandler(ICinemachineCamera toCamera, ICinemachineCamera fromCamera)
        {
            if (fromCamera == null)
            {
                return;
            }

            var oldFromCamera = this.fromCamera;
            this.fromCamera = fromCamera;
            if (oldFromCamera != null)
            {
                return;
            }
            InvokeRepeating(nameof(CheckBrainIsBlending),0,CHECK_BRAIN_IS_BLENDING_REPEAT_RATE);
        }

        /// <summary>
        /// Invoked Repeatedly when our <see cref="cinemachineBrain"/> <see cref="m_CameraActivatedEvent"/> has fired
        /// in order to poll <see cref="cinemachineBrain.IsBlending"/> to find when the blend has been completed.
        /// </summary>
        private void CheckBrainIsBlending()
        {
            if (!cinemachineBrain.IsBlending)
            {
                CancelInvoke(nameof(CheckBrainIsBlending));
                onCameraBlendCompleteEvent?.Invoke(cinemachineBrain.ActiveVirtualCamera,fromCamera);
                fromCamera = null;
            }
        }
    }
}