using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TrainMystery
{
    public class Introduction : TimeScaleIndependentUpdate
    {
        //public GameObject introductionGameObject;
        public Image bg;
        public TMP_Text title;
        public TMP_Text credits;
        public TMP_Text introText;

        public float FadeSpeed = 0.2F;
        public int RolloverCharacterSpread = 10;
        public Color ColorTint;

        protected override void Update()
        {
            base.Update();
        }

        public void Begin()
        {
            ShowTitle();
        }

        private void ShowTitle()
        {
            title.DOColor(Color.white, 2f)
                .SetDelay(3)
                .OnComplete(ShowCredits);
        }

        private void ShowCredits()
        {
            credits.DOColor(Color.white, 2f)
                .SetDelay(2f)
                .OnComplete(HideTitleAndCredits);
        }

        private void HideTitleAndCredits()
        {
            title.DOColor(Color.black, 2f)
                .SetDelay(3)
                .OnComplete(ShowMainText);
            credits.DOColor(Color.black, 4f)
                .SetDelay(2f);
        }

        private void ShowMainText()
        {
            StartCoroutine(AnimateVertexColors());
        }
        
        private void HideMainText()
        {
            introText.color = Color.white;

            introText.DOColor(Color.clear, 2f)
                .OnComplete(FadeFromBlack);
        }

        private void FadeFromBlack()
        {
            title.gameObject.SetActive(false);
            credits.gameObject.SetActive(false);
            introText.gameObject.SetActive(false);

            bg.DOColor(Color.clear, 2f)
                .SetDelay(1)
                .OnComplete(EndAll);
        }

        private void EndAll()
        {
            TrainMysteryGameManager.Instance.EndIntroduction();
            gameObject.SetActive(false);
        }

        IEnumerator AnimateVertexColors()
        {
            // Need to force the text object to be generated so we have valid data to work with right from the start.
            introText.ForceMeshUpdate();

            TMP_TextInfo textInfo = introText.textInfo;
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
                            introText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

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
                introText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                if (currentCharacter + 1 < characterCount) currentCharacter += 1;

                yield return new WaitForSeconds(0.075f - FadeSpeed * 0.01f);
            }

            HideMainText();
        }
    }
}
