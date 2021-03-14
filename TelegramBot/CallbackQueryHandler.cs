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
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Начало")
                    {
                        InlineMenu inlineMenu = new InlineMenu();
                        inlineMenu.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    break;
                case (int) InlinePanelStep.Catalog1:
                    break;
                case (int) InlinePanelStep.Catalog2:
                    break;
                case (int) InlinePanelStep.Catalog3:
                    break;
                case (int) InlinePanelStep.Catalog4:
                    break;
                case (int) InlinePanelStep.CatPrice:
                    break;
                case (int) InlinePanelStep.Company:
                    break;
                case (int) InlinePanelStep.Contacts:
                    break;
                case (int) InlinePanelStep.Discount:
                    break;
                case (int) InlinePanelStep.InfoBrands:
                    break;
                case (int) InlinePanelStep.ListBrands:
                    break;
                case (int) InlinePanelStep.Major:
                    break;
                case (int) InlinePanelStep.Menu:
                    break;
                case (int) InlinePanelStep.News:
                    break;
                case (int) InlinePanelStep.PriceList1:
                    break;
                case (int) InlinePanelStep.PriceList2:
                    break;
                case (int) InlinePanelStep.PriceList3:
                    break;
                case (int) InlinePanelStep.Reason1:
                    break;
                case (int) InlinePanelStep.Reason2:
                    break;
                case (int) InlinePanelStep.Reason3:
                    break;
                case (int) InlinePanelStep.Reason4:
                    break;
                case (int) InlinePanelStep.Reason5:
                    break;
                case (int) InlinePanelStep.Reason6:
                    break;
                case (int) InlinePanelStep.Reason7:
                    break;
                case (int) InlinePanelStep.Reason8:
                    break;
                case (int) InlinePanelStep.Reasons:
                    break;
                case (int) InlinePanelStep.Short:
                    break;
                case (int) InlinePanelStep.Timetable:
                    break;
                case (int) InlinePanelStep.TypesProduct:
                    break;
                default:
                    break;
            }
            
        }
    }
}