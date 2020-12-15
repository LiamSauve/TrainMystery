using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrainMystery
{
    public enum GameOverType
    {
        timeout = 0,
        murderer,
        shotNothing,
        success,
        failed
    }

    public class GameOver : TimeScaleIndependentUpdate
    {
        public Image bg;
        public TMP_Text gameOverText;
        public GameOverType gameOverType = GameOverType.timeout;

        // text anim stuff
        public float FadeSpeed = 0.2F;
        public int RolloverCharacterSpread = 10;
        public Color ColorTint;

        [TextArea(15, 20)]
        public string MurdererText = string.Empty;
        [TextArea(15, 20)]
        public string SuccesfulText = string.Empty;
        [TextArea(15, 20)]
        public string FailureText = string.Empty;
        [TextArea(15, 20)]
        public string ShotNothingText = string.Empty;

        protected override void Update()
        {
            base.Update();
        }

        public void Begin_TimeOut(string murdererName)
        {
            gameOverType = GameOverType.timeout;
            gameObject.SetActive(true);

            var t = string.Format(FailureText, murdererName);
            gameOverText.text = t;

            Blackout();
        }

        public void Begin_Success(string murdererName)
        {
            gameOverType = GameOverType.murderer;
            gameObject.SetActive(true);

            var t = string.Format(SuccesfulText, murdererName);
            gameOverText.text = t;

            Blackout();
        }

        public void Begin_Murderer(string victimName)
        {
            gameOverType = GameOverType.murderer;
            gameObject.SetActive(true);

            var t = string.Format(MurdererText, victimName);
            gameOverText.text = t;

            Blackout();
        }
        
        public void Begin_ShotNothing()
        {
            gameOverType = GameOverType.shotNothing;
            gameObject.SetActive(true);

            gameOverText.text = ShotNothingText;
            Blackout();
        }

        private void Blackout()
        {
            if(gameOverType == GameOverType.murderer)
            {
                bg.DOColor(Color.black, 2f)
                    .SetDelay(1)
                    .OnComplete(ShowEndText);
            }

            if (gameOverType == GameOverType.timeout)
            {
                bg.DOColor(Color.black, 2f)
                    .SetDelay(1)
                    .OnComplete(ShowEndText);
            }

            if (gameOverType == GameOverType.shotNothing)
            {
                bg.DOColor(Color.black, 2f)
                    .SetDelay(1)
                    .OnComplete(ShowEndText);
            }
        }

        // figure out how to show jail cell text

        private void ShowEndText()
        {
            StartCoroutine(AnimateVertexColors());
        }

        private void ShowEndText_AndFadeOut()
        {
            StartCoroutine(AnimateVertexColors());
            bg.DOColor(Color.black, 3f)
                .SetDelay(1);
        }

        private void HideGameOverText()
        {
            gameOverText.color = Color.white;
            gameOverText.DOColor(Color.clear, 2f)
                .OnComplete(CloseGame);
        }

        private void CloseGame()
        {
            DOVirtual.DelayedCall(3, () => Application.Quit() );
        }

        IEnumerator AnimateVertexColors()
        {
            // Need to force the text object to be generated so we have valid data to work with right from the start.
            gameOverText.ForceMeshUpdate();

            TMP_TextInfo textInfo = gameOverText.textInfo;
            Color32[] newVertexColors;

            int currentCharacter = 0;
            int startingCharacterRange = currentCharacter;
            bool isRangeMax = false;

            while (!isRangeMax)
            {
                int characterCount = textInfo.characterCount;

                // Spread should not exceed the number of characters.
                byte fadeSteps = (byte)Mathf.Max(1, 255 / RolloverCharacterSpread);


                for (int i = startingCharacterRange; i < currentCharacter + 1; i++)
                {
                    // Skip characters that are not visible
                    if (!textInfo.characterInfo[i].isVisible) continue;

                    // Get the index of the material used by the current character.
                    int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                    // Get the vertex colors of the mesh used by this text element (character or sprite).
                    newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                    // Get the index of the first vertex used by this text element.
                    int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                    // Get the current character's alpha value.
                    byte alpha = (byte)Mathf.Clamp(newVertexColors[vertexIndex + 0].a + fadeSteps, 0, 255);

                    // Set new alpha values.
                    newVertexColors[vertexIndex + 0].a = alpha;
                    newVertexColors[vertexIndex + 1].a = alpha;
                    newVertexColors[vertexIndex + 2].a = alpha;
                    newVertexColors[vertexIndex + 3].a = alpha;

                    // Tint vertex colors
                    // Note: Vertex colors are Color32 so we need to cast to Color to multiply with tint which is Color.
                    newVertexColors[vertexIndex + 0] = (Color)newVertexColors[vertexIndex + 0] * Color.white;
                    newVertexColors[vertexIndex + 1] = (Color)newVertexColors[vertexIndex + 1] * Color.white;
                    newVertexColors[vertexIndex + 2] = (Color)newVertexColors[vertexIndex + 2] * Color.white;
                    newVertexColors[vertexIndex + 3] = (Color)newVertexColors[vertexIndex + 3] * Color.white;

                    if (alpha >= 255)
                    {
                        startingCharacterRange += 1;

                        if (startingCharacterRange == characterCount)
                        {
                            // Update mesh vertex data one last time.
                            gameOverText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                            yield return new WaitForSeconds(1.0f);

                            // Reset the text object back to original state.
                            //introText.ForceMeshUpdate();

                            yield return new WaitForSeconds(2.0f);

                            // Reset our counters.
                            currentCharacter = 0;
                            startingCharacterRange = 0;
                            isRangeMax = true; // Would end the coroutine.
                        }
                    }
                }

                // Upload the changed vertex colors to the Mesh.
                gameOverText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                if (currentCharacter + 1 < characterCount) currentCharacter += 1;

                yield return new WaitForSeconds(0.075f - FadeSpeed * 0.01f);
            }

            // end
            HideGameOverText();
        }
    }
}