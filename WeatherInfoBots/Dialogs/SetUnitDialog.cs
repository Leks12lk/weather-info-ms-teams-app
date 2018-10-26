using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;

namespace WeatherInfoBots.Dialogs
{
    public class SetUnitDialog : WaterfallDialog
    {
        public SetUnitDialog(string dialogId, IEnumerable<WaterfallStep> steps = null) : base(dialogId, steps)
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
            //AddStep(async (stepContext, cancellationToken) =>
            //{
            //    var response = (stepContext.Result as FoundChoice)?.Value;

            //    //if (response == "Check balance")
            //    //{
            //    //    return await stepContext.BeginDialogAsync(CheckBalanceDialog.Id);
            //    //}

            //    //if (response == "Make payment")
            //    //{
            //    //    return await stepContext.BeginDialogAsync(MakePaymentDialog.Id);
            //    //}

            //    return await stepContext.NextAsync();
            //});

            AddStep(async (stepContext, cancellationToken) =>
            {
                var state = await (stepContext.Context.TurnState["WeatherInfoBotAccessors"] as WeatherInfoBotAccessors).WeatherInfoState.GetAsync(stepContext.Context, () => new WeatherInfoState());
                await stepContext.Context.SendActivityAsync("Thank you, I got the weather info for you 💸");
                return await stepContext.EndDialogAsync();
            });
        }

        public static string Id => "setUnitDialog";

        public static SetUnitDialog Instance { get; } = new SetUnitDialog(Id);
    }
}
