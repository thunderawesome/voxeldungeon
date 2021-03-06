using System.Collections;
using UnityEngine;

namespace Battlerock
{
    public class CustomAnimator : MonoBehaviour
    {
        #region Public Variables

        /// <summary>
        /// Array of gameobjects that will be
        /// used as "frames" in the animation.
        /// </summary>
        public GameObject[] frames;

        /// <summary>
        /// Current frame (index) that the
        /// animation is on.
        /// </summary>
        public int currentFrame = 0;

        public bool isPlaying = false;
        public bool playOnAwake = true;
        public bool loop = true;
        public bool pingPong = false;

        /// <summary>
        /// How quickly to move through the animation.
        /// </summary>
        public float interval = 0.1f;

        #endregion

        #region Private Variables

        private int direction = 1;

        private IEnumerator m_coroutine;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // No frames already set up
            if (frames == null || frames.Length == 0)
            {
                // Only find the transform children not their descendants. Also, excludes the current transform.
                Transform[] tempFrames = transform.GetComponentsOnlyInChildren<Transform>();

                // Set the array to the same size as the result above
                frames = new GameObject[tempFrames.Length];

                // Loop through and add the frames to the array
                for (int i = 0; i < tempFrames.Length; i++)
                {
                    frames[i] = tempFrames[i].gameObject;
                }
            }

            if (playOnAwake == true)
            {
                Play();
            }
        }

        private void OnEnable()
        {
            // Only want to assign it here if playOnAwake is not true and no coroutine has been assigned yet.
            if (m_coroutine == null)
            {
                m_coroutine = Animate();
            }

            //Play();
            StartCoroutine(m_coroutine);
        }

        private void OnDestroy()
        {
            Reset();
            StopCoroutine(Animate());
        }

        private void OnDisable()
        {
            Reset();
            StopCoroutine(Animate());
        }

        #endregion

        #region Private Methods

        private IEnumerator Animate()
        {
            if (frames.Length <= 0 || isPlaying == false)
            {
                Debug.LogWarningFormat("Either there are no frames to animate (Frames: {0}) OR animation is not being played (isPlaying: {1}). Exiting Coroutine early.", frames.Length, isPlaying);
                yield break;
            }

            while (isPlaying == true)
            {
                yield return new WaitForSecondsRealtime(interval);

                currentFrame += direction;

                // Last frame and is moving from first frame to last frame
                if (direction == 1 && currentFrame == frames.Length)
                {
                    if (pingPong == true && loop == false)
                    {
                        direction = -1;

                        if (currentFrame == 0)
                        {
                            pingPong = false;
                            Reset();
                            yield break;
                        }
                    }
                    else if (pingPong == true && loop == true)
                    {
                        direction = -1;
                    }
                    else if (pingPong == false && loop == true)
                    {
                        currentFrame = 0;
                        direction = 1;
                    }
                    else if (pingPong == false && loop == false)
                    {
                        ClampToLastFrame();
                        yield break;
                    }
                }
                else if (direction == -1 && currentFrame == 0)
                {
                    direction = 1;
                    if (loop == false)
                    {
                        Reset();
                        yield break;
                    }
                }

                //if (currentFrame > 0)
                //{
                //    frames[currentFrame-1].SetActive(false);
                //}

                int thisFrame = 0;
                for (int i = 0; i < frames.Length; i++)
                {
                    frames[i].SetActive(false);
                    if (currentFrame == thisFrame)
                    {
                        frames[i].SetActive(true);
                    }

                    thisFrame++;
                }

                // Without this, the while loop would go on indefinitely and crash unity.
                yield return null;
            }
        }

        public void Play()
        {
            isPlaying = true;
            if (m_coroutine == null)
            {
                m_coroutine = Animate();
                StartCoroutine(m_coroutine);
            }
        }

        public void Pause()
        {
            isPlaying = false;
            StopCoroutine(m_coroutine);
            m_coroutine = null;
        }

        public void Reset()
        {
            isPlaying = false;
            if (m_coroutine != null)
            {
                currentFrame = 0;

                //StopCoroutine(m_coroutine);

                //m_coroutine = null;

                // Loops through and disables all Frames and enables the first frame.
                for (int i = 0; i < frames.Length; i++)
                {
                    frames[i].SetActive(false);
                }

                frames[currentFrame].SetActive(true);
            }
        }

        private void ClampToLastFrame()
        {
            isPlaying = false;
            currentFrame = frames.Length - 1;
            // Loops through and disables all Frames and enables the first frame.
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i].SetActive(false);
            }

            frames[currentFrame].SetActive(true);
        }

        #endregion


    }
}