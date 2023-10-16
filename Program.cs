using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using NCalc;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;

var botClient = new TelegramBotClient("6677557956:AAGJ2o55sJJxO_YtNZK61vJ1NB4KlOqxVWo");

using CancellationTokenSource cts = new();


ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>() 
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Message is not { } message)
    {
        return;
    }

    if (message.Text is not { } messageText)
    {
        return;
    }

    var chatId = message.Chat.Id;


    if (messageText.StartsWith("help"))
    {
        var inLineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("test")
            }

        });
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "I'm Optimus Prime, I'll help you save the world. Click calculate and go!!!:",
            replyMarkup: inLineKeyboard,
            cancellationToken: cancellationToken);
    }
    else if (messageText.StartsWith("test"))
    {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("help"),
                InlineKeyboardButton.WithCallbackData("test")
            }
        });

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Choose what you want to do:",
            replyMarkup: inlineKeyboard,
            cancellationToken: cancellationToken);
    }
    else if (messageText.StartsWith(""))
    {
        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("help"),
                InlineKeyboardButton.WithCallbackData("test")
            }
        });

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Choose what you want to do:",
            replyMarkup: inlineKeyboard,
            cancellationToken: cancellationToken);
    }
   /* botClient.AnswerCallbackQueryAsync("heloCollbec");*/

    /* async (sender, callbackQueryEventArgs) =>
     {
         var callbackQuery = callbackQueryEventArgs.CallbackQuery;
         var chatId = callbackQuery.Message.Chat.Id;
         var data = callbackQuery.Data;

         if (data == "help")
         {
             await botClient.SendTextMessageAsync(
                 chatId: chatId,
                 text: "Тут ви можете отримати допомогу.");
         }
         else if (data == "calculate")
         {
             await botClient.SendTextMessageAsync(
                 chatId: chatId,
                 text: "Введіть математичний вираз для обчислення:");
         }
     };*/
    /*Message massage = botClient.AnswerCallbackQueryAsync(string ca*/
   
       
}

static string test( )
    {
    Console.WriteLine("resalt");
    return "resalt";

}


static double CalculateExpression(string expression)
{
    try
    {
        Expression expr = new Expression(expression);

        
        if (IsUnsafeExpression(expression))
        {
            throw new Exception("Self-destruct in 4...3...2...");
        }

        expr.Parameters["pi"] = Math.PI; 
        object result = expr.Evaluate();

        if (result is int intValue)
        {
            return (double)intValue; 
        }
        else if (result is double doubleValue)
        {
            return doubleValue;
        }
        else
        {
            throw new Exception("I'm smart, but not by how much");
        }
    }
    catch (Exception ex)
    {
        throw new Exception("Houston we have a problem!!!: " + ex.Message);
    }
}



static bool IsUnsafeExpression(string expression)
{
    
    return expression.Contains("DELETE", StringComparison.OrdinalIgnoreCase);
}


Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}
