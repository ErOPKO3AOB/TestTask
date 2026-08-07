namespace Assets.Scripts.Inputs.UI
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.InputSystem.OnScreen;

    public class DynamicJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [Header("References")]
        [SerializeField] private RectTransform joystickRoot;   // JoystickRoot
        [SerializeField] private OnScreenStick stick;          // компонент на Handle

        [Header("Settings")]
        [SerializeField] private float movementRange = 80f;    // должно совпадать с Movement Range у OnScreenStick
        [SerializeField] private bool hideOnRelease = true;    // true = SetActive(false), false = плавный возврат
        [SerializeField] private float returnSpeed = 12f;      // скорость возврата (чем больше — тем быстрее)

        private RectTransform area;
        private Canvas canvas;
        private Camera uiCam;

        private Vector2 startPos;          // начальная позиция JoystickRoot
        private Vector2 pointerDownLocal;  // точка, где появился джойстик
        private bool isDragging;
        private Coroutine returnRoutine;

        private void Awake()
        {
            area = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            uiCam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

            startPos = joystickRoot.anchoredPosition;

            if (hideOnRelease)
                joystickRoot.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    area, eventData.position, uiCam, out Vector2 localPoint))
                return;

            // Останавливаем возврат, если он шёл
            if (returnRoutine != null)
            {
                StopCoroutine(returnRoutine);
                returnRoutine = null;
            }

            // Ставим джойстик в точку касания
            joystickRoot.anchoredPosition = localPoint;
            pointerDownLocal = localPoint;

            if (hideOnRelease)
                joystickRoot.gameObject.SetActive(true);

            isDragging = true;

            // Передаём событие встроенному стику
            stick.OnPointerDown(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragging) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                area, eventData.position, uiCam, out Vector2 localPoint);

            Vector2 delta = localPoint - pointerDownLocal;

            // Если палец ушёл дальше movementRange — двигаем весь джойстик
            if (delta.magnitude > movementRange)
            {
                Vector2 excess = delta.normalized * (delta.magnitude - movementRange);
                pointerDownLocal += excess;
                joystickRoot.anchoredPosition = pointerDownLocal;
            }

            stick.OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isDragging) return;

            isDragging = false;
            stick.OnPointerUp(eventData);

            if (hideOnRelease)
            {
                joystickRoot.gameObject.SetActive(false);
                joystickRoot.anchoredPosition = startPos; // сразу возвращаем на место
            }
            else
            {
                // Плавный возврат на стартовую позицию
                returnRoutine = StartCoroutine(ReturnToStart());
            }
        }

        private IEnumerator ReturnToStart()
        {
            while (Vector2.Distance(joystickRoot.anchoredPosition, startPos) > 0.5f)
            {
                joystickRoot.anchoredPosition = Vector2.Lerp(
                    joystickRoot.anchoredPosition,
                    startPos,
                    returnSpeed * Time.unscaledDeltaTime);

                yield return null;
            }

            joystickRoot.anchoredPosition = startPos;
            returnRoutine = null;
        }
    }
}