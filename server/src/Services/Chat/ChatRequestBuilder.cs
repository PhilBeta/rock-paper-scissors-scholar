using System.Text.Json.Serialization;

namespace supper_tool_api {
    public class ChatRequestBuilder {

        private List<ChatMessage> _messages = new List<ChatMessage>();

        public void AddUserMessage(string message) {
            AddMessage("user", message);
        }
        public void AddSystemMessage(string message) {
            AddMessage("system", message);
        }

        private void AddMessage(string user, string message){
            //If the previous message has the same user, append this message as a new line.
            if (_messages.Count > 0 && _messages[_messages.Count-1].Role == user)
                _messages[_messages.Count-1].Content += Environment.NewLine + message;
            else
                this._messages.Add(new ChatMessage{Role=user, Content=message});
        }

        public ChatMessage[] Build(){
            return _messages.ToArray();
        }

    }
}