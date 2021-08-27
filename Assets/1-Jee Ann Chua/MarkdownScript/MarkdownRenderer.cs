using TMPro;
using UnityEngine;
using System.IO;
using System.Linq;

namespace LogicUI.FancyTextRendering
{
    [RequireComponent(typeof(TMP_Text))]
    public class MarkdownRenderer : MonoBehaviour
    {
        public string Source;


        TMP_Text _TextMesh;
        public TMP_Text TextMesh
        {
            get
            {
                if (_TextMesh == null)
                    _TextMesh = GetComponent<TMP_Text>();

                return _TextMesh;
            }
        }

        private void OnValidate()
        {
                string readFromFilePath = Application.dataPath + "/1-Jee Ann Chua/Scenes/test.txt";
                string[] fileLines = File.ReadAllLines(readFromFilePath);
        
                Source = string.Join("\n", fileLines);
            RenderText();
        }


        public MarkdownRenderingSettings RenderSettings = MarkdownRenderingSettings.Default;

        private void RenderText()
        {
            Markdown.RenderToTextMesh(Source, TextMesh, RenderSettings);
        }
    }
}