using UnityEngine;
using UnityEngine.Animations;
using VContainer;

namespace Assets.Scripts.Health.UI
{
    public class HealthBarHUD : HealthBarUI
    {
        private void Start()
        {
            var cam = Camera.main;
            if (cam == null) return;

            GetComponent<Canvas>().worldCamera = cam;

            var lookAt = gameObject.AddComponent<LookAtConstraint>();

            lookAt.AddSource(new ConstraintSource
            {
                sourceTransform = cam.transform,
                weight = 1f
            });

            // Важно: указываем, какая ось должна смотреть на камеру
            lookAt.rotationAtRest = transform.eulerAngles;
            lookAt.rotationOffset = Vector3.zero;
            lookAt.constraintActive = true;
        }
    }
}
