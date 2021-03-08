using Telegram.Bot.Args;

namespace TelegramBot.InlinePanels
{
    public abstract class InlinePanel
    {
        public void RunCreatingProcess(MessageEventArgs messageEventArgs)
        {
            CreateInlinePanel(messageEventArgs);
            SaveMessageId();
            DeleteOldPanel();
        }
        
        
        protected abstract void CreateInlinePanel(MessageEventArgs messageEventArgs);


        private void SaveMessageId()
        {
            
        }
        
        private void DeleteOldPanel()
        {
            
        }

    }
}