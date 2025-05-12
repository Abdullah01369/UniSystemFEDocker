namespace UniSystemFE.Areas.Academician.Models
{
    public class TextToSpeechRequest
    {
        public string Text { get; init; }  

        public TextToSpeechRequest(string text)
        {
            Text = text;
        }
    }
}
