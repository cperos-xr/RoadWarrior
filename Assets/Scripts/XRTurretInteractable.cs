using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace UnityEngine.XR.Interaction.Toolkit.Interactables
{
    [AddComponentMenu("XR/XR Turret Interactable")]
    public class XRTurretInteractable : XRBaseInteractable
    {
        [SerializeField]
        private Transform m_AttachTransform;

        /// <summary>
        /// The attachment point that Unity uses on this Interactable.
        /// </summary>
        public Transform attachTransform
        {
            get => m_AttachTransform;
            set => m_AttachTransform = value;
        }

        protected override void Awake()
        {
            base.Awake();
            // Ensure attachTransform is set; default to this object's transform if none provided
            if (m_AttachTransform == null)
                m_AttachTransform = transform;
        }

        /// <summary>
        /// Override this method to return the specific attachment point
        /// for the interactable when selected.
        /// </summary>
        /// <param name="interactor">The interactor initiating the selection.</param>
        /// <returns>The appropriate attachment Transform.</returns>
        public override Transform GetAttachTransform(IXRInteractor interactor)
        {
            return attachTransform != null ? attachTransform : base.GetAttachTransform(interactor);
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            base.OnSelectEntering(args);
            // Optionally, add code here to set position/rotation based on interactor attach point.
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
            // Reset position/rotation if needed on exit.
        }
    }
}
