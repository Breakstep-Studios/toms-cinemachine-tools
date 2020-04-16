using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace ThomasBrown.Cinemachine
{
    /// <summary>
    /// Simplifies the events fired from <see cref="CinemachineBrainHelper"/> And <see cref="CinemachineBrain"/>
    /// in order for them to work with NodeCanvas FSM
    /// </summary>
    [RequireComponent(typeof(CinemachineBrainHelper))]
    public class SimplifiedCinemachineBrainHelper : MonoBehaviour
    {
        /// <summary>Event with a ICinemachineCamera parameter</summary>
        [Serializable] public class SimplifiedVcamActivatedEvent : UnityEvent<ICinemachineCamera> {}
        
        /// <summary>
        /// The <see cref="CinemachineBrainHelper"/> class from which to fire simplified events from (Blend Complete).
        /// </summary>
        [Tooltip("The <see cinemachineBrainHelper class from which to fire simplified events from (Blend Complete).")]
        public CinemachineBrainHelper cinemachineBrainHelper;
        /// <summary>
        /// The <see cref="CinemachineBrain"/> class from which to fire simplified event from (Activate events).
        /// </summary>
        [Tooltip("The CinemachineBrain class from which to fire simplified event from (Activate events).")]
        public CinemachineBrain cinemachineBrain;
        /// <summary>
        /// Fires when the <see cref="CinemachineBrainHelper"/> fires it's OnCameraBlendCompleteEvent returning blended to camera.
        /// </summary>
        [Space]
        [Tooltip("Fires when the CinemachineBrainHelper fires it's OnCameraBlendCompleteEvent returning blended to camera.")]
        public SimplifiedVcamActivatedEvent onCameraBlendToCompleteEvent;
        /// <summary>
        /// Fires when the <see cref="CinemachineBrainHelper"/> fires it's OnCameraBlendCompleteEvent returning blended from camera.
        /// </summary>
        [Tooltip("Fires when the CinemachineBrainHelper fires it's OnCameraBlendCompleteEvent returning blended from camera.")]
        public SimplifiedVcamActivatedEvent onCameraBlendFromCompleteEvent;
        /// <summary>
        /// Fires when the CinemachineBrain fires it's OnCameraActivatedEvent returning activated to camera
        /// </summary>
        [Space]
        [Tooltip("Fires when the CinemachineBrain fires it's OnCameraActivatedEvent returning activated to camera")]
        public SimplifiedVcamActivatedEvent onCameraActivatedToEvent;
        /// <summary>
        /// Fires when the CinemachineBrain fires it's OnCameraActivatedEvent returning activated from camera
        /// </summary>
        [Space]
        [Tooltip("Fires when the CinemachineBrain fires it's OnCameraActivatedEvent returning activated from camera")]
        public SimplifiedVcamActivatedEvent onCameraActivatedFromEvent;
        
        private void Awake()
        {
            cinemachineBrainHelper.onCameraBlendCompleteEvent.AddListener(OnCameraBlendCompleteEventHandler);
            cinemachineBrain.m_CameraActivatedEvent.AddListener(OnCameraBlendStartEventHandler);
        }

        /// <summary>
        /// Listener for the <see cref="CinemachineBrain.m_CameraActivatedEvent"/>
        /// </summary>
        /// <param name="toCamera">Camera activated we're moving to.</param>
        /// <param name="fromCamera">Camera deactivated moving from</param>
        private void OnCameraBlendStartEventHandler(ICinemachineCamera toCamera, ICinemachineCamera fromCamera)
        {
            onCameraActivatedToEvent?.Invoke(toCamera);
            onCameraActivatedFromEvent?.Invoke(fromCamera);
        }

        /// <summary>
        /// Listener for the <see cref="CinemachineBrainHelper.onCameraBlendCompleteEvent"/>
        /// </summary>
        /// <param name="toCamera">Camera blended to.</param>
        /// <param name="fromCamera">Camera blended from.</param>
        private void OnCameraBlendCompleteEventHandler(ICinemachineCamera toCamera, ICinemachineCamera fromCamera)
        {
            onCameraBlendToCompleteEvent?.Invoke(toCamera);
            onCameraBlendFromCompleteEvent?.Invoke(fromCamera);
        } 
    }
}