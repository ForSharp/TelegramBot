using System;
using Telegram.Bot.Args;
using TelegramBot.InlinePanels;

namespace TelegramBot
{
    public static class CallbackQueryHandler
    {
        public static void HandleCallbackQuery(string sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var stepId = DataBaseContext.GetStepId(callbackQueryEventArgs);
            
            if (stepId == (int) InlinePanelStep.Brands)
            {
                
            }

            switch (stepId)
            {
                case (int) InlinePanelStep.Brands:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Catalog1:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Catalog2:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Catalog3:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Catalog4:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.CatPrice:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Company:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Contacts:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Discount:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.InfoBrands:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.ListBrands:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Major:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Menu:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.News:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.PriceList1:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.PriceList2:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.PriceList3:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Reason1:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Reason2:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Reason3:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Reason4:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Reason5:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Reason6:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Reason7:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Reason8:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Reasons:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Short:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Timetable:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.TypesProduct:
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                default:
                    break;
            }
            
        }

        private static void GoToBeginning(CallbackQueryEventArgs callbackQueryEventArgs)
        {
            if (callbackQueryEventArgs.CallbackQuery.Data == "Начало")
            {
                InlineMenu inlineMenu = new InlineMenu();
                inlineMenu.RunCreatingProcess(callbackQueryEventArgs);
            }
        }
    }
}