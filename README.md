#Jarvis : Connect your voice to Constellation

This package permits you to install a voice recognition on your sentinels.

##Installation

Declare Jarvis package on your sentinel, as follows :

	<package name="Jarvis" filename="Jarvis.zip" enable="true" credential="DeveloperAccess">
		<settings>
			<setting key="User" value="<Your name>" />
			<setting key="Grammar" value="<Path to your XML grammar file>" />
     		</settings>
	</package>

##Usage

Once configured and launched, you just need to say “Bonjour Jarvis” or “Salut Jarvis” to start interacting with voice recognition.
In case it succeeds, Jarvis will answer you. Otherwise, he’ll remain quiet.

Then, you just need to request one of the options in your grammar to get the result.
The voice-recognized message is sent to the group JarvisSpeech on your Constellation.

The sent data is organized as follows :

	Text = e.Result.Text,
	Confidence = e.Result.Confidence,
	SemanticValue = e.Result.Semantics.Count > 0 ? e.Result.Semantics.ToDictionary(sv => sv.Key, sv => sv.Value.Value) : new Dictionary<string, object>() { { "RootKey", (object)	e.Result.Semantics.Value.ToString() } },
	Words = e.Result.Words.Select(w => w.Text).ToList()

* **Text :** The corresponding sentence in the grammar with the pronounced sentence
Confidence : Represents the degree of confidence regarding the grammar sentence the voice recognizer understood.
* **SemanticValue :** A set of key/values from the grammar, included in the pronounced sentence.
* **Words :** All the words contained in the pronounced sentence.

In order to receive this object, you need to implement the *SpeechReceive(object response)* in your AI. Example below :

	[MessageCallback(IsHidden = true)]
	public void SpeechReceive(object response)
	{
		Response obj = JsonConvert.DeserializeObject<Response>(response.ToString());
		String semanticValue = (string)obj.SemanticValue["constellation"];
	}

Response.cs

	public class Response
	{
		public String Text { get; set; }
		public double Confidence { get; set; }
		public Dictionary<string, object> SemanticValue { get; set; }
		public List<String> Words { get; set; }
}

* **[MessageCallback(IsHidden = true)] :** Permits to avoid this function from being invoked outside of the voice recognition context.

In parallel to voice recognition, voice synthesis was also implemented, so Jarvis can vocally answer to requests, thanks to the *Speak(string text)* method.

##Technical Diagram

![Jarvis logo](https://sc-cdn.scaleengine.net/i/9b5a32e344c04569735c5311f6af9992.png "jarvis logo")

##MessageCallbacks

* **SpeechReceive(object response) :** This method is called by the Jarvis package when a voice recognition succeeded. All packages attached to the “*JarvisSpeech*” group which implemented the *SpeechReceive* method will receive the result in response.

* **Speak(string text) :** This method enables Jarvis to pronounce given sentences (voice synthesis).

* **EnableRecognition(bool enable) :** This method permits to enable or disable voice recognition at your wish.











