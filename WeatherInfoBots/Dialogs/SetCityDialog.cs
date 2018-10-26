using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;

namespace WeatherInfoBots.Dialogs
{
    public class SetCityDialog : WaterfallDialog
    {
        public SetCityDialog(string dialogId, IEnumerable<WaterfallStep> steps = null) : base(dialogId, steps)
        {
            AddStep(async (stepContext, cancellationToken) =>
            {
                return await stepContext.PromptAsync("choicePrompt",
                    new PromptOptions
                    {
                        Prompt = stepContext.Context.Activity.CreateReply("[MainDialog] I'm banking 🤖{Environment.NewLine}Would you like to check balance or make payment?"),
                        Choices = new[] { new Choice { Value = "Check balance" }, new Choice { Value = "Make payment" } }.ToList()
                    });
            });
            AddStep(async (stepContext, cancellationToken) =>
            {
                var response = (stepContext.Result as FoundChoice)?.Value;

                //if (response == "Check balance")
                //{
                //    return await stepContext.BeginDialogAsync(CheckBalanceDialog.Id);
                //}

                //if (response == "Make payment")
                //{
                //    return await stepContext.BeginDialogAsync(MakePaymentDialog.Id);
                //}

                return await stepContext.NextAsync();
            });

            AddStep(async (stepContext, cancellationToken) => { return await stepContext.ReplaceDialogAsync(Id); });
        }

        public static string Id => "setCityDialog";

        public static SetCityDialog Instance { get; } = new SetCityDialog(Id);
    }
}
