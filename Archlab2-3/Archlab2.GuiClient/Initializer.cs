namespace Archlab2.GuiClient
{
    public class Initializer
    {
        public ChatViewModel ChatViewModel { get; }
        public UsersViewModel UsersViewModel { get; }
        public ConnectionViewModel ConnectionViewModel { get; }

        public Initializer() {
            var model = new ChatModel();
            ChatViewModel = new(model);
            UsersViewModel = new(model);
            ConnectionViewModel = new(model);
        }
    }
}
