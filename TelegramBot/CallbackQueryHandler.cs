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
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Список брендов")
                    {
                        InlineListBrands inlineListBrands = new InlineListBrands();
                        inlineListBrands.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Инфо")
                    {
                        InlineInfoBrands inlineInfoBrands = new InlineInfoBrands();
                        inlineInfoBrands.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Catalog1:
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Назад")
                    {
                        InlineCatPrice inlineCatPrice = new InlineCatPrice();
                        inlineCatPrice.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Далее")
                    {
                        InlineCatalog2 inlineCatalog2 = new InlineCatalog2();
                        inlineCatalog2.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Catalog2:
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Назад")
                    {
                        InlineCatalog1 inlineCatalog1 = new InlineCatalog1();
                        inlineCatalog1.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Далее")
                    {
                        InlineCatalog3 inlineCatalog3 = new InlineCatalog3();
                        inlineCatalog3.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Catalog3:
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Назад")
                    {
                        InlineCatalog2 inlineCatalog2 = new InlineCatalog2();
                        inlineCatalog2.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Далее")
                    {
                        InlineCatalog4 inlineCatalog4 = new InlineCatalog4();
                        inlineCatalog4.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Catalog4:
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Назад")
                    {
                        InlineCatalog3 inlineCatalog3 = new InlineCatalog3();
                        inlineCatalog3.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.CatPrice:
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Каталоги продукции")
                    {
                        InlineCatalog1 inlineCatalog1 = new InlineCatalog1();
                        inlineCatalog1.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Прайс листы")
                    {
                        InlinePriceList1 inlinePriceList1 = new InlinePriceList1();
                        inlinePriceList1.RunCreatingProcess(callbackQueryEventArgs);
                    }
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
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Назад")
                    {
                        InlineBrands inlineBrands = new InlineBrands();
                        inlineBrands.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.ListBrands:
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Каталоги и прайсы")
                    {
                        InlineCatPrice inlineCatPrice = new InlineCatPrice();
                        inlineCatPrice.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Назад")
                    {
                        InlineBrands inlineBrands = new InlineBrands();
                        inlineBrands.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Major:
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Виды продукции")
                    {
                        InlineTypesProduct inlineTypesProduct = new InlineTypesProduct();
                        inlineTypesProduct.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Коротко о компании")
                    {
                        InlineShort inlineShort = new InlineShort();
                        inlineShort.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Причины работать с нами")
                    {
                        InlineReasons inlineReasons = new InlineReasons();
                        inlineReasons.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Расписание рейсов")
                    {
                        InlineTimetable inlineTimetable = new InlineTimetable();
                        inlineTimetable.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    GoToBeginning(callbackQueryEventArgs);
                    break;
                case (int) InlinePanelStep.Menu:
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Главная")
                    {
                        InlineMajor inlineMajor = new InlineMajor();
                        inlineMajor.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Бренды")
                    {
                        InlineBrands inlineBrands = new InlineBrands();
                        inlineBrands.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Каталоги и прайсы")
                    {
                        InlineCatPrice inlineCatPrice = new InlineCatPrice();
                        inlineCatPrice.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Акции")
                    {
                        InlineDiscount inlineDiscount = new InlineDiscount();
                        inlineDiscount.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "О компании")
                    {
                        InlineCompany inlineCompany = new InlineCompany();
                        inlineCompany.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Новости")
                    {
                        InlineNews inlineNews = new InlineNews();
                        inlineNews.RunCreatingProcess(callbackQueryEventArgs);
                    }
                    if (callbackQueryEventArgs.CallbackQuery.Data == "Контакты")
                    {
                        InlineContacts inlineContacts = new InlineContacts();
                        inlineContacts.RunCreatingProcess(callbackQueryEventArgs);
                    }
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